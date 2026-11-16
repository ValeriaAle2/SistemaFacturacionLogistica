using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using SistemaFacturacionLogistica.Models;
using SistemaFacturacionLogistica.Services;

namespace SistemaFacturacionLogistica
{
    internal sealed class HistorialForm : Form
    {
        private readonly PersistenciaLotesService _persistencia;
        private readonly Func<string, string> _texto;
        private readonly CultureInfo _cultura;
        private readonly DataGridView _lotes = CrearTabla();
        private readonly DataGridView _paquetes = CrearTabla();
        private readonly DataGridView _errores = CrearTabla();
        private readonly Label _estado = new Label { Dock = DockStyle.Fill, AutoSize = true };
        private readonly Button _editar;
        private readonly Button _eliminar;
        private readonly Button _registrarRecepcion;

        public HistorialForm(PersistenciaLotesService persistencia, Func<string, string> texto, CultureInfo cultura)
        {
            _persistencia = persistencia;
            _texto = texto;
            _cultura = cultura;

            Text = _texto("Historial");
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            MinimizeBox = false;
            ClientSize = new Size(1000, 540);
            MinimumSize = new Size(640, 420);

            AgregarColumnas(_lotes, "LoteId", "FechaHora", "Validos", "Erroneos", "TotalLote");
            AgregarColumnas(_paquetes, "Codigo", "Cliente", "Zona", "Recepcion", "Peso", "ValorDeclarado", "Subtotal",
                "RecargoZona", "Penalizacion", "Seguro", "Impuesto", "Total");
            AgregarColumnas(_errores, "NumeroLinea", "Linea", "Motivo");

            _lotes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _paquetes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            _paquetes.Columns["Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _paquetes.Columns["Total"].MinimumWidth = 90;
            _errores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _errores.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            _errores.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            var detalle = new TabControl { Dock = DockStyle.Fill };
            detalle.TabPages.Add(new TabPage(_texto("Procesados")));
            detalle.TabPages[0].Controls.Add(_paquetes);
            detalle.TabPages.Add(new TabPage(_texto("Erroneos")));
            detalle.TabPages[1].Controls.Add(_errores);
            _paquetes.CurrentCellChanged += (sender, args) => ActualizarBotonRecepcion();
            _paquetes.VisibleChanged += (sender, args) => ActualizarBotonRecepcion();

            var cerrar = new Button
            {
                Text = _texto("Cerrar"),
                AutoSize = true,
                Anchor = AnchorStyles.Right,
                DialogResult = DialogResult.Cancel
            };
            CancelButton = cerrar;
            _editar = new Button { Text = _texto("ModificarLote"), AutoSize = true, Enabled = false };
            _eliminar = new Button { Text = _texto("EliminarLote"), AutoSize = true, Enabled = false };
            _registrarRecepcion = new Button { Text = _texto("RegistrarRecepcion"), AutoSize = true, Enabled = false };
            _editar.Click += ModificarLote;
            _eliminar.Click += EliminarLote;
            _registrarRecepcion.Click += RegistrarRecepcion;
            var acciones = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };
            acciones.Controls.AddRange(new Control[] { cerrar, _eliminar, _editar, _registrarRecepcion });

            var contenido = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(10)
            };
            contenido.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            contenido.RowStyles.Add(new RowStyle(SizeType.Percent, 42));
            contenido.RowStyles.Add(new RowStyle(SizeType.Percent, 58));
            contenido.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            contenido.Controls.Add(_estado, 0, 0);
            contenido.Controls.Add(_lotes, 0, 1);
            contenido.Controls.Add(detalle, 0, 2);
            contenido.Controls.Add(acciones, 0, 3);
            Controls.Add(contenido);

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarHistorial();
        }

        private void CargarHistorial(long? seleccionarId = null)
        {
            _lotes.CurrentCellChanged -= SeleccionarLote;
            _lotes.Rows.Clear();
            _paquetes.Rows.Clear();
            _errores.Rows.Clear();
            _editar.Enabled = _eliminar.Enabled = false;

            try
            {
                int seleccion = 0;
                foreach (ResumenLote lote in _persistencia.ConsultarHistorial())
                {
                    int fila = _lotes.Rows.Add(lote.Id, lote.FechaUtc.ToLocalTime().ToString("g", _cultura),
                        lote.Paquetes, lote.Errores, Dinero(lote.Total));
                    _lotes.Rows[fila].Tag = lote;
                    if (lote.Id == seleccionarId)
                        seleccion = fila;
                }

                if (_lotes.Rows.Count > 0)
                {
                    _lotes.CurrentCell = _lotes.Rows[seleccion].Cells[0];
                    _lotes.CurrentCellChanged += SeleccionarLote;
                    SeleccionarLote(this, EventArgs.Empty);
                }
                else
                {
                    _estado.Text = _texto("HistorialVacio");
                }
            }
            catch (Exception ex) when (EsErrorDatos(ex))
            {
                _estado.Text = _texto("ErrorHistorial");
            }
        }

        private void SeleccionarLote(object sender, EventArgs e)
        {
            _paquetes.Rows.Clear();
            _errores.Rows.Clear();
            var lote = _lotes.CurrentRow?.Tag as ResumenLote;
            _editar.Enabled = _eliminar.Enabled = lote != null;
            if (lote == null)
                return;

            try
            {
                ResultadoLote resultado = _persistencia.ConsultarDetalle(lote);
                _estado.Text = "";
                foreach (Paquete paquete in resultado.Paquetes)
                {
                    string zona = _texto("Zona") + " " + paquete.Zona[paquete.Zona.Length - 1];
                    int fila = _paquetes.Rows.Add(paquete.Codigo, paquete.Cliente, zona,
                        paquete.FechaRecepcionUtc?.ToLocalTime().ToString("g", _cultura) ?? _texto("RecepcionPendiente"),
                        paquete.Peso.ToString("0.########", _cultura), Dinero(paquete.ValorDeclarado),
                        Dinero(paquete.Subtotal), Dinero(paquete.RecargoZona), Dinero(paquete.Penalizacion),
                        Dinero(paquete.Seguro), Dinero(paquete.Impuesto), Dinero(paquete.Total));
                    _paquetes.Rows[fila].Tag = paquete;
                }

                foreach (ErrorProcesamiento error in resultado.Errores)
                    _errores.Rows.Add(error.NumeroLinea, error.Linea, _texto(error.Motivo));

                ActualizarBotonRecepcion();
            }
            catch (Exception ex) when (EsErrorDatos(ex))
            {
                _estado.Text = _texto("ErrorHistorial");
            }
        }

        private void ActualizarBotonRecepcion()
        {
            if (_registrarRecepcion != null)
            {
                var paquete = _paquetes.CurrentRow?.Tag as Paquete;
                _registrarRecepcion.Enabled = _paquetes.Visible && paquete != null &&
                    !paquete.FechaRecepcionUtc.HasValue;
            }
        }

        private void RegistrarRecepcion(object sender, EventArgs e)
        {
            if (!(_lotes.CurrentRow?.Tag is ResumenLote lote) ||
                !(_paquetes.CurrentRow?.Tag is Paquete paquete) || paquete.FechaRecepcionUtc.HasValue)
                return;

            string mensaje = string.Format(_cultura, _texto("ConfirmarRecepcion"), paquete.Codigo, paquete.Cliente);
            if (MessageBox.Show(this, mensaje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            try
            {
                _persistencia.RegistrarRecepcion(lote, paquete);
                SeleccionarLote(this, EventArgs.Empty);
            }
            catch (Exception ex) when (EsErrorDatos(ex))
            {
                MostrarAccionFallida(this, "ErrorRecepcion");
                SeleccionarLote(this, EventArgs.Empty);
            }
        }

        private void ModificarLote(object sender, EventArgs e)
        {
            if (!(_lotes.CurrentRow?.Tag is ResumenLote lote))
                return;

            try
            {
                ResultadoLote original = _persistencia.ConsultarDetalle(lote);
                Configuracion tarifas = _persistencia.ConsultarTarifas(lote);
                using (var editor = new Form
                {
                    Text = _texto("ModificarLote"),
                    StartPosition = FormStartPosition.CenterParent,
                    ClientSize = new Size(680, 350),
                    MinimumSize = new Size(475, 275),
                    ShowInTaskbar = false,
                    MinimizeBox = false
                })
                {
                    var entrada = new TextBox
                    {
                        Dock = DockStyle.Fill,
                        Multiline = true,
                        AcceptsReturn = true,
                        ScrollBars = ScrollBars.Both,
                        WordWrap = false,
                        MaxLength = int.MaxValue,
                        Text = PrepararTexto(original)
                    };
                    string textoInicial = entrada.Text;
                    var guardar = new Button { Text = _texto("GuardarCambios"), AutoSize = true };
                    var cancelar = new Button { Text = _texto("Cancelar"), AutoSize = true, DialogResult = DialogResult.Cancel };
                    var acciones = new FlowLayoutPanel
                    {
                        Dock = DockStyle.Bottom,
                        AutoSize = true,
                        FlowDirection = FlowDirection.RightToLeft
                    };
                    acciones.Controls.AddRange(new Control[] { cancelar, guardar });
                    var formato = new Label { Dock = DockStyle.Top, AutoSize = true, Text = _texto("Formato") };
                    editor.Controls.Add(entrada);
                    editor.Controls.Add(acciones);
                    editor.Controls.Add(formato);
                    editor.CancelButton = cancelar;
                    guardar.Click += (origen, evento) =>
                    {
                        try
                        {
                            if (entrada.Text != textoInicial)
                            {
                                ResultadoLote resultado = new ProcesadorPaquetesService()
                                    .Procesar(entrada.Text, new CalculoFacturaService(tarifas));
                                _persistencia.Actualizar(lote, resultado);
                            }
                            editor.DialogResult = DialogResult.OK;
                        }
                        catch (InvalidOperationException)
                        {
                            MostrarAccionFallida(editor, "PaqueteRecibidoProtegido");
                        }
                        catch (Exception ex) when (EsErrorDatos(ex))
                        {
                            MostrarAccionFallida(editor, ex is OverflowException
                                ? "TotalLoteFueraDeRango" : "ErrorModificarLote");
                        }
                    };

                    if (editor.ShowDialog(this) == DialogResult.OK)
                        CargarHistorial(lote.Id);
                }
            }
            catch (Exception ex) when (EsErrorDatos(ex))
            {
                MostrarAccionFallida(this, "ErrorModificarLote");
            }
        }

        private void EliminarLote(object sender, EventArgs e)
        {
            if (!(_lotes.CurrentRow?.Tag is ResumenLote lote))
                return;

            string mensaje = string.Format(_cultura, _texto("ConfirmarEliminarLote"), lote.Id);
            if (MessageBox.Show(this, mensaje, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            try
            {
                _persistencia.Eliminar(lote);
                CargarHistorial();
            }
            catch (Exception ex) when (EsErrorDatos(ex))
            {
                MostrarAccionFallida(this, "ErrorEliminarLote");
            }
        }

        private static string PrepararTexto(ResultadoLote lote)
        {
            var lineas = new List<string>();
            int paquete = 0;
            int error = 0;
            while (paquete < lote.Paquetes.Count || error < lote.Errores.Count)
            {
                if (error < lote.Errores.Count &&
                    (lote.Errores[error].NumeroLinea == lineas.Count + 1 || paquete == lote.Paquetes.Count))
                {
                    lineas.Add(lote.Errores[error++].Linea);
                }
                else
                {
                    Paquete actual = lote.Paquetes[paquete++];
                    lineas.Add(string.Join(";", actual.Codigo, actual.Cliente, actual.Zona,
                        actual.Peso.ToString(CultureInfo.InvariantCulture),
                        actual.ValorDeclarado.ToString(CultureInfo.InvariantCulture)));
                }
            }

            return string.Join(Environment.NewLine, lineas) + Environment.NewLine;
        }

        private void MostrarAccionFallida(IWin32Window ventana, string clave)
        {
            MessageBox.Show(ventana, _texto(clave), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static bool EsErrorDatos(Exception ex)
        {
            return ex is SQLiteException || ex is IOException || ex is UnauthorizedAccessException ||
                ex is FormatException || ex is OverflowException || ex is InvalidDataException ||
                ex is DllNotFoundException ||
                ex is BadImageFormatException;
        }

        private static DataGridView CrearTabla()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = SystemColors.Window,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
        }

        private void AgregarColumnas(DataGridView tabla, params string[] claves)
        {
            foreach (string clave in claves)
            {
                tabla.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = clave,
                    HeaderText = clave == "Peso" ? _texto(clave) + " (kg)" : _texto(clave),
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });
            }
        }

        private string Dinero(decimal importe) => importe.ToString("N2", _cultura);
    }
}
