using System;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using SistemaFacturacionLogistica.Models;
using SistemaFacturacionLogistica.Services;

namespace SistemaFacturacionLogistica
{
    public partial class Form1 : Form
    {
        private readonly IdiomaService _idioma = new IdiomaService();
        private readonly ProcesadorPaquetesService _procesador = new ProcesadorPaquetesService();
        private readonly CsvService _csv = new CsvService();
        private readonly PersistenciaLotesService _persistencia = new PersistenciaLotesService();

        private Configuracion _config;
        private CalculoFacturaService _calculo;
        private ResultadoLote _lote = new ResultadoLote(
            Array.Empty<Paquete>(), Array.Empty<ErrorProcesamiento>(), 0);
        private int _indiceIdioma = -1;
        private int _alturaMaximaErrores;
        private CultureInfo _cultura = CultureInfo.GetCultureInfo("es-MX");

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode)
                return;

            PrepararControles();
            _selectorIdioma.SelectedIndex = 0;
            try
            {
                _config = new ConfiguracionService().Cargar();
                _calculo = new CalculoFacturaService(_config);
                ActualizarTarifas();
                _botonProcesar.Enabled = _indiceIdioma >= 0;
            }
            catch (FileNotFoundException)
            {
                MostrarError("ErrorJson");
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException ||
                ex is JsonException || ex is InvalidDataException)
            {
                MostrarError("ErrorConfiguracion");
            }
        }

        private void PrepararControles()
        {
            ConfigurarTabla(_tablaPaquetes);
            ConfigurarTabla(_tablaErrores);
            _alturaMaximaErrores = _tablaErrores.Height;
            _tablaErrores.ScrollBars = ScrollBars.Vertical;
            _tablaErrores.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            _tablaErrores.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            _botonProcesar.Enabled = false;
            _botonExportar.Enabled = false;
            _botonExportar.Click += ExportarCsv;
            _tablaPaquetes.CurrentCellChanged += (sender, args) => MostrarDetalle();

            _selectorIdioma.DropDownStyle = ComboBoxStyle.DropDownList;
            _selectorIdioma.Items.Clear();
            _selectorIdioma.Items.AddRange(new object[] { "Español", "English" });
        }

        private static void ConfigurarTabla(DataGridView tabla)
        {
            tabla.ReadOnly = true;
            tabla.AllowUserToAddRows = false;
            tabla.AllowUserToDeleteRows = false;
            tabla.MultiSelect = false;
            tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn columna in tabla.Columns)
                columna.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void ProcesarLote(object sender, EventArgs e)
        {
            try
            {
                _lote = _procesador.Procesar(_entradaLote.Text, _calculo);
            }
            catch (OverflowException)
            {
                MostrarError("TotalLoteFueraDeRango");
                return;
            }

            ActualizarResultados();
            try
            {
                _persistencia.Guardar(_lote, _config);
            }
            catch (Exception ex) when (ex is SQLiteException || ex is IOException ||
                ex is UnauthorizedAccessException || ex is DllNotFoundException || ex is BadImageFormatException)
            {
                MostrarError("ErrorGuardarLote");
            }
        }

        private void ActualizarResultados()
        {
            var seleccionado = _tablaPaquetes.CurrentRow?.Tag as Paquete;
            _tablaPaquetes.Rows.Clear();
            _tablaErrores.Rows.Clear();

            foreach (Paquete paquete in _lote.Paquetes)
            {
                int fila = _tablaPaquetes.Rows.Add(paquete.Cliente, paquete.Codigo, MostrarZona(paquete),
                    paquete.Peso.ToString("0.########", _cultura), Dinero(paquete.ValorDeclarado),
                    Dinero(paquete.Total));
                _tablaPaquetes.Rows[fila].Tag = paquete;
                if (ReferenceEquals(paquete, seleccionado))
                    _tablaPaquetes.CurrentCell = _tablaPaquetes.Rows[fila].Cells[0];
            }

            _tablaErrores.Height = _alturaMaximaErrores;
            foreach (ErrorProcesamiento error in _lote.Errores)
                _tablaErrores.Rows.Add(error.NumeroLinea.ToString(_cultura) + ": " + error.Linea, Texto(error.Motivo));

            int alturaContenido = _tablaErrores.ColumnHeadersHeight +
                _tablaErrores.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            _tablaErrores.Height = Math.Min(_alturaMaximaErrores, alturaContenido);

            _conteoValidos.Text = _lote.Paquetes.Count.ToString(_cultura);
            _conteoErroneos.Text = _lote.Errores.Count.ToString(_cultura);
            _importeLote.Text = Dinero(_lote.Total);
            _botonExportar.Enabled = _lote.Paquetes.Count > 0 || _lote.Errores.Count > 0;
            MostrarDetalle();
        }

        private void MostrarDetalle()
        {
            var paquete = _tablaPaquetes.CurrentRow?.Tag as Paquete;
            _codigoDetalle.Text = paquete?.Codigo ?? "";
            _clienteDetalle.Text = paquete?.Cliente ?? "";
            _zonaDetalle.Text = paquete == null ? "" : MostrarZona(paquete);
            _pesoDetalle.Text = paquete?.Peso.ToString("0.########", _cultura) ?? "";
            _valorDeclaradoDetalle.Text = paquete == null ? "" : Dinero(paquete.ValorDeclarado);
            _subtotalDetalle.Text = paquete == null ? "" : Dinero(paquete.Subtotal);
            _recargoDetalle.Text = paquete == null ? "" : Dinero(paquete.RecargoZona);
            _penalizacionDetalle.Text = paquete == null ? "" : Dinero(paquete.Penalizacion);
            _seguroDetalle.Text = paquete == null ? "" : Dinero(paquete.Seguro);
            _costoFijoDetalle.Text = paquete == null ? "" : Dinero(_config.CostoFijoEnvio);
            _ivaDetalle.Text = paquete == null ? "" : Dinero(paquete.Impuesto);
            _importePaquete.Text = paquete == null ? "" : Dinero(paquete.Total);
        }

        private void ExportarCsv(object sender, EventArgs e)
        {
            using (var dialogo = new SaveFileDialog
            {
                Title = Texto("SeleccioneArchivo"),
                Filter = Texto("FiltroCsv") + " (*.csv)|*.csv",
                DefaultExt = "csv",
                FileName = Texto("NombreCsv") + ".csv"
            })
            {
                try
                {
                    if (dialogo.ShowDialog(this) != DialogResult.OK)
                        return;

                    _csv.Exportar(dialogo.FileName, _lote, _config);
                    MessageBox.Show(this, Texto("ExportacionExitosa"), Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException ||
                    ex is ArgumentException || ex is NotSupportedException)
                {
                    MostrarError("ErrorExportacion");
                }
            }
        }

        private void AbrirHistorial(object sender, EventArgs e)
        {
            using (var historial = new HistorialForm(_persistencia, Texto, _cultura))
                historial.ShowDialog(this);
        }

        private void CambiarIdioma(object sender, EventArgs e)
        {
            if (_selectorIdioma.SelectedIndex < 0 || _selectorIdioma.SelectedIndex == _indiceIdioma)
                return;

            bool ingles = _selectorIdioma.SelectedIndex == 1;
            try
            {
                _idioma.CargarIdioma(ingles ? "EN" : "ES");
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is JsonException)
            {
                _selectorIdioma.SelectedIndex = _indiceIdioma;
                MostrarError("ErrorIdioma");
                return;
            }

            _indiceIdioma = _selectorIdioma.SelectedIndex;
            _botonProcesar.Enabled = _calculo != null;
            _cultura = CultureInfo.GetCultureInfo(ingles ? "en-US" : "es-MX");
            Text = _titulo.Text = Texto("Titulo");
            _subtitulo.Text = Texto("Subtitulo");
            _capturaLote.Text = Texto("Ingreso");
            _instrucciones.Text = Texto("Instrucciones");
            _creditoEquipo.Text = Texto("Equipo");
            _formatoCaptura.Text = Texto("Formato");
            _grupoPaquetes.Text = Texto("Procesados");
            _detallePaquete.Text = Texto("Detalle");
            _resumenLote.Text = Texto("Resumen");
            _desgloseFactura.Text = Texto("Desglose");
            _botonProcesar.Text = Texto("Procesar");
            _botonExportar.Text = Texto("Exportar");
            _botonHistorial.Text = Texto("Historial");

            _etiquetaCodigo.Text = Texto("Codigo") + ":";
            _etiquetaCliente.Text = Texto("Cliente") + ":";
            _etiquetaZona.Text = Texto("Zona") + ":";
            _etiquetaPeso.Text = Texto("Peso") + " (kg):";
            _etiquetaValorDeclarado.Text = Texto("ValorDeclarado") + ":";
            _etiquetaSubtotal.Text = Texto("Subtotal") + ":";
            _etiquetaRecargo.Text = Texto("RecargoZona") + ":";
            _etiquetaPenalizacion.Text = Texto("Penalizacion") + ":";
            _etiquetaSeguro.Text = Texto("Seguro") + " (2%):";
            _etiquetaCostoFijo.Text = Texto("CostoFijo") + ":";
            _etiquetaIva.Text = Texto("Impuesto") + ":";
            _etiquetaImportePaquete.Text = Texto("TotalPagar") + ":";

            _etiquetaValidos.Text = Texto("Validos");
            _etiquetaErroneos.Text = Texto("Erroneos");
            _etiquetaImporteLote.Text = Texto("TotalLote");
            _columnaCliente.HeaderText = Texto("Cliente");
            _columnaCodigo.HeaderText = Texto("Codigo");
            _columnaZona.HeaderText = Texto("Zona");
            _columnaPeso.HeaderText = Texto("Peso") + " (kg)";
            _columnaValorDeclarado.HeaderText = Texto("ValorDeclarado");
            _columnaTotal.HeaderText = Texto("Total");
            _columnaLinea.HeaderText = Texto("Linea");
            _columnaMotivo.HeaderText = Texto("Motivo");

            ActualizarTarifas();
            ActualizarResultados();
        }

        private void ActualizarTarifas()
        {
            _tarifas.Text = _config == null ? "" : string.Format(_cultura, "{0}: {1} | {2}: {3:P0} | {4}: {5}",
                Texto("PrecioBase"), Dinero(_config.PrecioBaseKg), Texto("Impuesto"), _config.Iva,
                Texto("CostoFijo"), Dinero(_config.CostoFijoEnvio));
        }

        private string Texto(string clave) => _idioma.ObtenerTexto(clave);
        private string Dinero(decimal importe) => importe.ToString("N2", _cultura);

        private string MostrarZona(Paquete paquete)
        {
            return Texto("Zona") + " " + paquete.Zona[paquete.Zona.Length - 1];
        }

        private void MostrarError(string clave)
        {
            MessageBox.Show(this, Texto(clave), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
