namespace SistemaFacturacionLogistica
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer _componentes = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (_componentes != null))
            {
                _componentes.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this._encabezado = new System.Windows.Forms.GroupBox();
            this._iconoCamion = new System.Windows.Forms.PictureBox();
            this._selectorIdioma = new System.Windows.Forms.ComboBox();
            this._etiquetaIdioma = new System.Windows.Forms.Label();
            this._subtitulo = new System.Windows.Forms.Label();
            this._titulo = new System.Windows.Forms.Label();
            this._capturaLote = new System.Windows.Forms.GroupBox();
            this._tablaErrores = new System.Windows.Forms.DataGridView();
            this._columnaLinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._columnaMotivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._botonHistorial = new System.Windows.Forms.Button();
            this._botonExportar = new System.Windows.Forms.Button();
            this._botonProcesar = new System.Windows.Forms.Button();
            this._formatoCaptura = new System.Windows.Forms.Label();
            this._entradaLote = new System.Windows.Forms.TextBox();
            this._instrucciones = new System.Windows.Forms.Label();
            this._grupoPaquetes = new System.Windows.Forms.GroupBox();
            this._tablaPaquetes = new System.Windows.Forms.DataGridView();
            this._columnaCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._columnaCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._columnaZona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._columnaPeso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._columnaValorDeclarado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._columnaTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._detallePaquete = new System.Windows.Forms.GroupBox();
            this._etiquetaValorDeclarado = new System.Windows.Forms.Label();
            this._etiquetaCodigo = new System.Windows.Forms.Label();
            this._etiquetaCliente = new System.Windows.Forms.Label();
            this._codigoDetalle = new System.Windows.Forms.Label();
            this._etiquetaZona = new System.Windows.Forms.Label();
            this._clienteDetalle = new System.Windows.Forms.Label();
            this._etiquetaPeso = new System.Windows.Forms.Label();
            this._zonaDetalle = new System.Windows.Forms.Label();
            this._pesoDetalle = new System.Windows.Forms.Label();
            this._valorDeclaradoDetalle = new System.Windows.Forms.Label();
            this._resumenLote = new System.Windows.Forms.GroupBox();
            this._importeLote = new System.Windows.Forms.Label();
            this._etiquetaImporteLote = new System.Windows.Forms.Label();
            this._conteoErroneos = new System.Windows.Forms.Label();
            this._conteoValidos = new System.Windows.Forms.Label();
            this._etiquetaValidos = new System.Windows.Forms.Label();
            this._etiquetaErroneos = new System.Windows.Forms.Label();
            this._pieFormulario = new System.Windows.Forms.GroupBox();
            this._tarifas = new System.Windows.Forms.Label();
            this._creditoEquipo = new System.Windows.Forms.Label();
            this._etiquetaSubtotal = new System.Windows.Forms.Label();
            this._importePaquete = new System.Windows.Forms.Label();
            this._etiquetaImportePaquete = new System.Windows.Forms.Label();
            this._etiquetaIva = new System.Windows.Forms.Label();
            this._etiquetaCostoFijo = new System.Windows.Forms.Label();
            this._etiquetaSeguro = new System.Windows.Forms.Label();
            this._etiquetaPenalizacion = new System.Windows.Forms.Label();
            this._etiquetaRecargo = new System.Windows.Forms.Label();
            this._recargoDetalle = new System.Windows.Forms.Label();
            this._subtotalDetalle = new System.Windows.Forms.Label();
            this._costoFijoDetalle = new System.Windows.Forms.Label();
            this._ivaDetalle = new System.Windows.Forms.Label();
            this._seguroDetalle = new System.Windows.Forms.Label();
            this._penalizacionDetalle = new System.Windows.Forms.Label();
            this._desgloseFactura = new System.Windows.Forms.GroupBox();
            this._menuContextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._encabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._iconoCamion)).BeginInit();
            this._capturaLote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tablaErrores)).BeginInit();
            this._grupoPaquetes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tablaPaquetes)).BeginInit();
            this._detallePaquete.SuspendLayout();
            this._resumenLote.SuspendLayout();
            this._pieFormulario.SuspendLayout();
            this._desgloseFactura.SuspendLayout();
            this.SuspendLayout();
            // 
            // _encabezado
            // 
            this._encabezado.BackColor = System.Drawing.Color.SteelBlue;
            this._encabezado.Controls.Add(this._iconoCamion);
            this._encabezado.Controls.Add(this._selectorIdioma);
            this._encabezado.Controls.Add(this._etiquetaIdioma);
            this._encabezado.Controls.Add(this._subtitulo);
            this._encabezado.Controls.Add(this._titulo);
            this._encabezado.Location = new System.Drawing.Point(-9, 0);
            this._encabezado.Name = "_encabezado";
            this._encabezado.Size = new System.Drawing.Size(1370, 105);
            this._encabezado.TabIndex = 0;
            this._encabezado.TabStop = false;
            // 
            // _iconoCamion
            // 
            this._iconoCamion.Image = ((System.Drawing.Image)(resources.GetObject("_iconoCamion.Image")));
            this._iconoCamion.Location = new System.Drawing.Point(20, 9);
            this._iconoCamion.Name = "_iconoCamion";
            this._iconoCamion.Size = new System.Drawing.Size(72, 89);
            this._iconoCamion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._iconoCamion.TabIndex = 1;
            this._iconoCamion.TabStop = false;
            // 
            // _selectorIdioma
            // 
            this._selectorIdioma.FormattingEnabled = true;
            this._selectorIdioma.Location = new System.Drawing.Point(1185, 57);
            this._selectorIdioma.Name = "_selectorIdioma";
            this._selectorIdioma.Size = new System.Drawing.Size(138, 24);
            this._selectorIdioma.TabIndex = 2;
            this._selectorIdioma.SelectedIndexChanged += new System.EventHandler(this.CambiarIdioma);
            // 
            // _etiquetaIdioma
            // 
            this._etiquetaIdioma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._etiquetaIdioma.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this._etiquetaIdioma.Image = ((System.Drawing.Image)(resources.GetObject("_etiquetaIdioma.Image")));
            this._etiquetaIdioma.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._etiquetaIdioma.Location = new System.Drawing.Point(1163, 18);
            this._etiquetaIdioma.Name = "_etiquetaIdioma";
            this._etiquetaIdioma.Size = new System.Drawing.Size(181, 28);
            this._etiquetaIdioma.TabIndex = 1;
            this._etiquetaIdioma.Text = "Idioma/Language:";
            this._etiquetaIdioma.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _subtitulo
            // 
            this._subtitulo.AutoSize = true;
            this._subtitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._subtitulo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this._subtitulo.Location = new System.Drawing.Point(100, 57);
            this._subtitulo.Name = "_subtitulo";
            this._subtitulo.Size = new System.Drawing.Size(330, 25);
            this._subtitulo.TabIndex = 3;
            this._subtitulo.Text = "Procesamiento y Auditoria de Envíos";
            // 
            // _titulo
            // 
            this._titulo.AutoSize = true;
            this._titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._titulo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this._titulo.Location = new System.Drawing.Point(98, 18);
            this._titulo.Name = "_titulo";
            this._titulo.Size = new System.Drawing.Size(530, 38);
            this._titulo.TabIndex = 0;
            this._titulo.Text = "Sistema de Facturacion Logistíca";
            // 
            // _capturaLote
            // 
            this._capturaLote.Controls.Add(this._tablaErrores);
            this._capturaLote.Controls.Add(this._botonHistorial);
            this._capturaLote.Controls.Add(this._botonExportar);
            this._capturaLote.Controls.Add(this._botonProcesar);
            this._capturaLote.Controls.Add(this._formatoCaptura);
            this._capturaLote.Controls.Add(this._entradaLote);
            this._capturaLote.Controls.Add(this._instrucciones);
            this._capturaLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._capturaLote.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._capturaLote.Location = new System.Drawing.Point(12, 123);
            this._capturaLote.Name = "_capturaLote";
            this._capturaLote.Size = new System.Drawing.Size(405, 404);
            this._capturaLote.TabIndex = 1;
            this._capturaLote.TabStop = false;
            this._capturaLote.Text = "Ingreso de Lote de Paquetes";
            // 
            // _tablaErrores
            // 
            this._tablaErrores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tablaErrores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._tablaErrores.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this._tablaErrores.BackgroundColor = System.Drawing.SystemColors.Control;
            this._tablaErrores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._tablaErrores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._tablaErrores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._columnaLinea,
            this._columnaMotivo});
            this._tablaErrores.Location = new System.Drawing.Point(4, 286);
            this._tablaErrores.Name = "_tablaErrores";
            this._tablaErrores.RowHeadersWidth = 51;
            this._tablaErrores.RowTemplate.Height = 24;
            this._tablaErrores.Size = new System.Drawing.Size(397, 110);
            this._tablaErrores.TabIndex = 8;
            // 
            // _columnaLinea
            // 
            this._columnaLinea.HeaderText = "Linea";
            this._columnaLinea.MinimumWidth = 6;
            this._columnaLinea.Name = "_columnaLinea";
            // 
            // _columnaMotivo
            // 
            this._columnaMotivo.HeaderText = "Motivo del Error";
            this._columnaMotivo.MinimumWidth = 6;
            this._columnaMotivo.Name = "_columnaMotivo";
            // 
            // _botonHistorial
            // 
            this._botonHistorial.BackColor = System.Drawing.Color.SandyBrown;
            this._botonHistorial.Location = new System.Drawing.Point(266, 241);
            this._botonHistorial.Name = "_botonHistorial";
            this._botonHistorial.Size = new System.Drawing.Size(133, 37);
            this._botonHistorial.TabIndex = 0;
            this._botonHistorial.Text = "Historial de lotes";
            this._botonHistorial.UseVisualStyleBackColor = false;
            this._botonHistorial.Click += new System.EventHandler(this.AbrirHistorial);
            // 
            // _botonExportar
            // 
            this._botonExportar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this._botonExportar.Location = new System.Drawing.Point(134, 241);
            this._botonExportar.Name = "_botonExportar";
            this._botonExportar.Size = new System.Drawing.Size(126, 37);
            this._botonExportar.TabIndex = 7;
            this._botonExportar.Text = "Exportar CSV";
            this._botonExportar.UseVisualStyleBackColor = false;
            // 
            // _botonProcesar
            // 
            this._botonProcesar.BackColor = System.Drawing.Color.LightGreen;
            this._botonProcesar.Location = new System.Drawing.Point(4, 241);
            this._botonProcesar.Name = "_botonProcesar";
            this._botonProcesar.Size = new System.Drawing.Size(124, 37);
            this._botonProcesar.TabIndex = 6;
            this._botonProcesar.Text = "Procesar Lote";
            this._botonProcesar.UseVisualStyleBackColor = false;
            this._botonProcesar.Click += new System.EventHandler(this.ProcesarLote);
            // 
            // _formatoCaptura
            // 
            this._formatoCaptura.AutoSize = true;
            this._formatoCaptura.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._formatoCaptura.Location = new System.Drawing.Point(6, 52);
            this._formatoCaptura.Name = "_formatoCaptura";
            this._formatoCaptura.Size = new System.Drawing.Size(248, 16);
            this._formatoCaptura.TabIndex = 5;
            this._formatoCaptura.Text = "(Formato: COD;Cliente;Zona;Peso;Valor)";
            // 
            // _entradaLote
            // 
            this._entradaLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._entradaLote.Location = new System.Drawing.Point(4, 81);
            this._entradaLote.Multiline = true;
            this._entradaLote.Name = "_entradaLote";
            this._entradaLote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._entradaLote.Size = new System.Drawing.Size(395, 150);
            this._entradaLote.TabIndex = 4;
            this._entradaLote.WordWrap = false;
            // 
            // _instrucciones
            // 
            this._instrucciones.AutoSize = true;
            this._instrucciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._instrucciones.Location = new System.Drawing.Point(6, 27);
            this._instrucciones.Name = "_instrucciones";
            this._instrucciones.Size = new System.Drawing.Size(261, 16);
            this._instrucciones.TabIndex = 3;
            this._instrucciones.Text = "Pegue o escriba los registros de paquetes";
            // 
            // _grupoPaquetes
            // 
            this._grupoPaquetes.Controls.Add(this._tablaPaquetes);
            this._grupoPaquetes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._grupoPaquetes.Location = new System.Drawing.Point(423, 123);
            this._grupoPaquetes.Name = "_grupoPaquetes";
            this._grupoPaquetes.Size = new System.Drawing.Size(597, 404);
            this._grupoPaquetes.TabIndex = 2;
            this._grupoPaquetes.TabStop = false;
            this._grupoPaquetes.Text = "Paquetes Procesados";
            // 
            // _tablaPaquetes
            // 
            this._tablaPaquetes.AllowUserToOrderColumns = true;
            this._tablaPaquetes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tablaPaquetes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._tablaPaquetes.BackgroundColor = System.Drawing.SystemColors.Window;
            this._tablaPaquetes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._tablaPaquetes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._columnaCliente,
            this._columnaCodigo,
            this._columnaZona,
            this._columnaPeso,
            this._columnaValorDeclarado,
            this._columnaTotal});
            this._tablaPaquetes.Location = new System.Drawing.Point(8, 21);
            this._tablaPaquetes.Name = "_tablaPaquetes";
            this._tablaPaquetes.ReadOnly = true;
            this._tablaPaquetes.RowHeadersWidth = 51;
            this._tablaPaquetes.RowTemplate.Height = 24;
            this._tablaPaquetes.Size = new System.Drawing.Size(583, 375);
            this._tablaPaquetes.TabIndex = 0;
            // 
            // _columnaCliente
            // 
            this._columnaCliente.HeaderText = "Cliente";
            this._columnaCliente.MinimumWidth = 6;
            this._columnaCliente.Name = "_columnaCliente";
            this._columnaCliente.ReadOnly = true;
            // 
            // _columnaCodigo
            // 
            this._columnaCodigo.HeaderText = "Código";
            this._columnaCodigo.MinimumWidth = 6;
            this._columnaCodigo.Name = "_columnaCodigo";
            this._columnaCodigo.ReadOnly = true;
            // 
            // _columnaZona
            // 
            this._columnaZona.HeaderText = "Zona";
            this._columnaZona.MinimumWidth = 6;
            this._columnaZona.Name = "_columnaZona";
            this._columnaZona.ReadOnly = true;
            // 
            // _columnaPeso
            // 
            this._columnaPeso.HeaderText = "Peso (kg)";
            this._columnaPeso.MinimumWidth = 6;
            this._columnaPeso.Name = "_columnaPeso";
            this._columnaPeso.ReadOnly = true;
            // 
            // _columnaValorDeclarado
            // 
            this._columnaValorDeclarado.HeaderText = "Valor Decl.";
            this._columnaValorDeclarado.MinimumWidth = 6;
            this._columnaValorDeclarado.Name = "_columnaValorDeclarado";
            this._columnaValorDeclarado.ReadOnly = true;
            // 
            // _columnaTotal
            // 
            this._columnaTotal.HeaderText = "Total";
            this._columnaTotal.MinimumWidth = 6;
            this._columnaTotal.Name = "_columnaTotal";
            this._columnaTotal.ReadOnly = true;
            // 
            // _detallePaquete
            // 
            this._detallePaquete.Controls.Add(this._etiquetaValorDeclarado);
            this._detallePaquete.Controls.Add(this._etiquetaCodigo);
            this._detallePaquete.Controls.Add(this._etiquetaCliente);
            this._detallePaquete.Controls.Add(this._codigoDetalle);
            this._detallePaquete.Controls.Add(this._etiquetaZona);
            this._detallePaquete.Controls.Add(this._clienteDetalle);
            this._detallePaquete.Controls.Add(this._etiquetaPeso);
            this._detallePaquete.Controls.Add(this._zonaDetalle);
            this._detallePaquete.Controls.Add(this._pesoDetalle);
            this._detallePaquete.Controls.Add(this._valorDeclaradoDetalle);
            this._detallePaquete.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._detallePaquete.Location = new System.Drawing.Point(1028, 123);
            this._detallePaquete.Name = "_detallePaquete";
            this._detallePaquete.Size = new System.Drawing.Size(317, 212);
            this._detallePaquete.TabIndex = 0;
            this._detallePaquete.TabStop = false;
            this._detallePaquete.Text = "Detalle del Paquete";
            // 
            // _etiquetaValorDeclarado
            // 
            this._etiquetaValorDeclarado.AutoSize = true;
            this._etiquetaValorDeclarado.Location = new System.Drawing.Point(6, 172);
            this._etiquetaValorDeclarado.Name = "_etiquetaValorDeclarado";
            this._etiquetaValorDeclarado.Size = new System.Drawing.Size(125, 16);
            this._etiquetaValorDeclarado.TabIndex = 4;
            this._etiquetaValorDeclarado.Text = "Valor Declarado:";
            // 
            // _etiquetaCodigo
            // 
            this._etiquetaCodigo.AutoSize = true;
            this._etiquetaCodigo.Location = new System.Drawing.Point(6, 28);
            this._etiquetaCodigo.Name = "_etiquetaCodigo";
            this._etiquetaCodigo.Size = new System.Drawing.Size(61, 16);
            this._etiquetaCodigo.TabIndex = 2;
            this._etiquetaCodigo.Text = "Codigo:";
            // 
            // _etiquetaCliente
            // 
            this._etiquetaCliente.AutoSize = true;
            this._etiquetaCliente.Location = new System.Drawing.Point(6, 63);
            this._etiquetaCliente.Name = "_etiquetaCliente";
            this._etiquetaCliente.Size = new System.Drawing.Size(59, 16);
            this._etiquetaCliente.TabIndex = 1;
            this._etiquetaCliente.Text = "Cliente:";
            // 
            // _codigoDetalle
            // 
            this._codigoDetalle.BackColor = System.Drawing.SystemColors.HighlightText;
            this._codigoDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._codigoDetalle.Location = new System.Drawing.Point(217, 27);
            this._codigoDetalle.Name = "_codigoDetalle";
            this._codigoDetalle.Size = new System.Drawing.Size(90, 22);
            this._codigoDetalle.TabIndex = 20;
            // 
            // _etiquetaZona
            // 
            this._etiquetaZona.AutoSize = true;
            this._etiquetaZona.Location = new System.Drawing.Point(6, 99);
            this._etiquetaZona.Name = "_etiquetaZona";
            this._etiquetaZona.Size = new System.Drawing.Size(46, 16);
            this._etiquetaZona.TabIndex = 0;
            this._etiquetaZona.Text = "Zona:";
            // 
            // _clienteDetalle
            // 
            this._clienteDetalle.BackColor = System.Drawing.SystemColors.HighlightText;
            this._clienteDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._clienteDetalle.Location = new System.Drawing.Point(217, 62);
            this._clienteDetalle.Name = "_clienteDetalle";
            this._clienteDetalle.Size = new System.Drawing.Size(90, 22);
            this._clienteDetalle.TabIndex = 19;
            // 
            // _etiquetaPeso
            // 
            this._etiquetaPeso.AutoSize = true;
            this._etiquetaPeso.Location = new System.Drawing.Point(6, 135);
            this._etiquetaPeso.Name = "_etiquetaPeso";
            this._etiquetaPeso.Size = new System.Drawing.Size(78, 16);
            this._etiquetaPeso.TabIndex = 3;
            this._etiquetaPeso.Text = "Peso (kg):";
            // 
            // _zonaDetalle
            // 
            this._zonaDetalle.BackColor = System.Drawing.SystemColors.HighlightText;
            this._zonaDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._zonaDetalle.Location = new System.Drawing.Point(217, 98);
            this._zonaDetalle.Name = "_zonaDetalle";
            this._zonaDetalle.Size = new System.Drawing.Size(90, 22);
            this._zonaDetalle.TabIndex = 18;
            // 
            // _pesoDetalle
            // 
            this._pesoDetalle.BackColor = System.Drawing.SystemColors.HighlightText;
            this._pesoDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pesoDetalle.Location = new System.Drawing.Point(217, 134);
            this._pesoDetalle.Name = "_pesoDetalle";
            this._pesoDetalle.Size = new System.Drawing.Size(90, 22);
            this._pesoDetalle.TabIndex = 17;
            // 
            // _valorDeclaradoDetalle
            // 
            this._valorDeclaradoDetalle.BackColor = System.Drawing.SystemColors.HighlightText;
            this._valorDeclaradoDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._valorDeclaradoDetalle.Location = new System.Drawing.Point(217, 171);
            this._valorDeclaradoDetalle.Name = "_valorDeclaradoDetalle";
            this._valorDeclaradoDetalle.Size = new System.Drawing.Size(90, 22);
            this._valorDeclaradoDetalle.TabIndex = 16;
            // 
            // _resumenLote
            // 
            this._resumenLote.Controls.Add(this._importeLote);
            this._resumenLote.Controls.Add(this._etiquetaImporteLote);
            this._resumenLote.Controls.Add(this._conteoErroneos);
            this._resumenLote.Controls.Add(this._conteoValidos);
            this._resumenLote.Controls.Add(this._etiquetaValidos);
            this._resumenLote.Controls.Add(this._etiquetaErroneos);
            this._resumenLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._resumenLote.Location = new System.Drawing.Point(12, 533);
            this._resumenLote.Name = "_resumenLote";
            this._resumenLote.Size = new System.Drawing.Size(1002, 99);
            this._resumenLote.TabIndex = 0;
            this._resumenLote.TabStop = false;
            this._resumenLote.Text = "Resumen del Lote";
            // 
            // _importeLote
            // 
            this._importeLote.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this._importeLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._importeLote.Location = new System.Drawing.Point(866, 36);
            this._importeLote.Name = "_importeLote";
            this._importeLote.Size = new System.Drawing.Size(130, 40);
            this._importeLote.TabIndex = 13;
            this._importeLote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _etiquetaImporteLote
            // 
            this._etiquetaImporteLote.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this._etiquetaImporteLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._etiquetaImporteLote.Image = ((System.Drawing.Image)(resources.GetObject("_etiquetaImporteLote.Image")));
            this._etiquetaImporteLote.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._etiquetaImporteLote.Location = new System.Drawing.Point(711, 36);
            this._etiquetaImporteLote.Name = "_etiquetaImporteLote";
            this._etiquetaImporteLote.Size = new System.Drawing.Size(158, 40);
            this._etiquetaImporteLote.TabIndex = 12;
            this._etiquetaImporteLote.Text = "Total del Lote";
            this._etiquetaImporteLote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _conteoErroneos
            // 
            this._conteoErroneos.BackColor = System.Drawing.Color.Salmon;
            this._conteoErroneos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._conteoErroneos.Location = new System.Drawing.Point(554, 36);
            this._conteoErroneos.Name = "_conteoErroneos";
            this._conteoErroneos.Size = new System.Drawing.Size(90, 40);
            this._conteoErroneos.TabIndex = 11;
            this._conteoErroneos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _conteoValidos
            // 
            this._conteoValidos.BackColor = System.Drawing.Color.LightGreen;
            this._conteoValidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._conteoValidos.Location = new System.Drawing.Point(191, 36);
            this._conteoValidos.Name = "_conteoValidos";
            this._conteoValidos.Size = new System.Drawing.Size(85, 40);
            this._conteoValidos.TabIndex = 9;
            this._conteoValidos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _etiquetaValidos
            // 
            this._etiquetaValidos.BackColor = System.Drawing.Color.LightGreen;
            this._etiquetaValidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._etiquetaValidos.Image = ((System.Drawing.Image)(resources.GetObject("_etiquetaValidos.Image")));
            this._etiquetaValidos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._etiquetaValidos.Location = new System.Drawing.Point(6, 36);
            this._etiquetaValidos.Name = "_etiquetaValidos";
            this._etiquetaValidos.Size = new System.Drawing.Size(189, 40);
            this._etiquetaValidos.TabIndex = 6;
            this._etiquetaValidos.Text = "Paquetes Validos";
            this._etiquetaValidos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _etiquetaErroneos
            // 
            this._etiquetaErroneos.BackColor = System.Drawing.Color.Salmon;
            this._etiquetaErroneos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._etiquetaErroneos.Image = ((System.Drawing.Image)(resources.GetObject("_etiquetaErroneos.Image")));
            this._etiquetaErroneos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._etiquetaErroneos.Location = new System.Drawing.Point(351, 36);
            this._etiquetaErroneos.Name = "_etiquetaErroneos";
            this._etiquetaErroneos.Size = new System.Drawing.Size(206, 40);
            this._etiquetaErroneos.TabIndex = 10;
            this._etiquetaErroneos.Text = "Paquetes Erroneos";
            this._etiquetaErroneos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _pieFormulario
            // 
            this._pieFormulario.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this._pieFormulario.Controls.Add(this._tarifas);
            this._pieFormulario.Controls.Add(this._creditoEquipo);
            this._pieFormulario.Location = new System.Drawing.Point(0, 638);
            this._pieFormulario.Name = "_pieFormulario";
            this._pieFormulario.Size = new System.Drawing.Size(1361, 41);
            this._pieFormulario.TabIndex = 0;
            this._pieFormulario.TabStop = false;
            // 
            // _tarifas
            // 
            this._tarifas.AutoSize = true;
            this._tarifas.Location = new System.Drawing.Point(13, 17);
            this._tarifas.Name = "_tarifas";
            this._tarifas.Size = new System.Drawing.Size(306, 16);
            this._tarifas.TabIndex = 4;
            this._tarifas.Text = "Precio Base/Kg: 15.50 | IVA: 16% | Costo Fijo: 50.00";
            // 
            // _creditoEquipo
            // 
            this._creditoEquipo.AutoSize = true;
            this._creditoEquipo.Location = new System.Drawing.Point(1211, 17);
            this._creditoEquipo.Name = "_creditoEquipo";
            this._creditoEquipo.Size = new System.Drawing.Size(124, 16);
            this._creditoEquipo.TabIndex = 5;
            this._creditoEquipo.Text = "Equipo Strive v1.0.0";
            // 
            // _etiquetaSubtotal
            // 
            this._etiquetaSubtotal.AutoSize = true;
            this._etiquetaSubtotal.Location = new System.Drawing.Point(6, 33);
            this._etiquetaSubtotal.Name = "_etiquetaSubtotal";
            this._etiquetaSubtotal.Size = new System.Drawing.Size(144, 16);
            this._etiquetaSubtotal.TabIndex = 6;
            this._etiquetaSubtotal.Text = "Subtotal (por peso):";
            // 
            // _importePaquete
            // 
            this._importePaquete.BackColor = System.Drawing.SystemColors.HighlightText;
            this._importePaquete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._importePaquete.Location = new System.Drawing.Point(217, 246);
            this._importePaquete.Name = "_importePaquete";
            this._importePaquete.Size = new System.Drawing.Size(90, 22);
            this._importePaquete.TabIndex = 13;
            // 
            // _etiquetaImportePaquete
            // 
            this._etiquetaImportePaquete.AutoSize = true;
            this._etiquetaImportePaquete.Location = new System.Drawing.Point(6, 247);
            this._etiquetaImportePaquete.Name = "_etiquetaImportePaquete";
            this._etiquetaImportePaquete.Size = new System.Drawing.Size(106, 16);
            this._etiquetaImportePaquete.TabIndex = 12;
            this._etiquetaImportePaquete.Text = "Total a Pagar:";
            // 
            // _etiquetaIva
            // 
            this._etiquetaIva.AutoSize = true;
            this._etiquetaIva.Location = new System.Drawing.Point(6, 212);
            this._etiquetaIva.Name = "_etiquetaIva";
            this._etiquetaIva.Size = new System.Drawing.Size(35, 16);
            this._etiquetaIva.TabIndex = 11;
            this._etiquetaIva.Text = "IVA:";
            // 
            // _etiquetaCostoFijo
            // 
            this._etiquetaCostoFijo.AutoSize = true;
            this._etiquetaCostoFijo.Location = new System.Drawing.Point(6, 177);
            this._etiquetaCostoFijo.Name = "_etiquetaCostoFijo";
            this._etiquetaCostoFijo.Size = new System.Drawing.Size(146, 16);
            this._etiquetaCostoFijo.TabIndex = 10;
            this._etiquetaCostoFijo.Text = "Costo Fijo de Envío:";
            // 
            // _etiquetaSeguro
            // 
            this._etiquetaSeguro.AutoSize = true;
            this._etiquetaSeguro.Location = new System.Drawing.Point(6, 140);
            this._etiquetaSeguro.Name = "_etiquetaSeguro";
            this._etiquetaSeguro.Size = new System.Drawing.Size(96, 16);
            this._etiquetaSeguro.TabIndex = 7;
            this._etiquetaSeguro.Text = "Seguro (2%):";
            // 
            // _etiquetaPenalizacion
            // 
            this._etiquetaPenalizacion.AutoSize = true;
            this._etiquetaPenalizacion.Location = new System.Drawing.Point(6, 104);
            this._etiquetaPenalizacion.Name = "_etiquetaPenalizacion";
            this._etiquetaPenalizacion.Size = new System.Drawing.Size(202, 16);
            this._etiquetaPenalizacion.TabIndex = 8;
            this._etiquetaPenalizacion.Text = "Penalización (peso > 20 kg):";
            // 
            // _etiquetaRecargo
            // 
            this._etiquetaRecargo.AutoSize = true;
            this._etiquetaRecargo.Location = new System.Drawing.Point(6, 68);
            this._etiquetaRecargo.Name = "_etiquetaRecargo";
            this._etiquetaRecargo.Size = new System.Drawing.Size(137, 16);
            this._etiquetaRecargo.TabIndex = 9;
            this._etiquetaRecargo.Text = "Recargo por Zona:";
            // 
            // _recargoDetalle
            // 
            this._recargoDetalle.BackColor = System.Drawing.SystemColors.HighlightText;
            this._recargoDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._recargoDetalle.Location = new System.Drawing.Point(217, 67);
            this._recargoDetalle.Name = "_recargoDetalle";
            this._recargoDetalle.Size = new System.Drawing.Size(90, 22);
            this._recargoDetalle.TabIndex = 14;
            // 
            // _subtotalDetalle
            // 
            this._subtotalDetalle.BackColor = System.Drawing.SystemColors.HighlightText;
            this._subtotalDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._subtotalDetalle.Location = new System.Drawing.Point(217, 32);
            this._subtotalDetalle.Name = "_subtotalDetalle";
            this._subtotalDetalle.Size = new System.Drawing.Size(90, 22);
            this._subtotalDetalle.TabIndex = 15;
            // 
            // _costoFijoDetalle
            // 
            this._costoFijoDetalle.BackColor = System.Drawing.SystemColors.HighlightText;
            this._costoFijoDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._costoFijoDetalle.Location = new System.Drawing.Point(217, 176);
            this._costoFijoDetalle.Name = "_costoFijoDetalle";
            this._costoFijoDetalle.Size = new System.Drawing.Size(90, 22);
            this._costoFijoDetalle.TabIndex = 21;
            // 
            // _ivaDetalle
            // 
            this._ivaDetalle.BackColor = System.Drawing.SystemColors.HighlightText;
            this._ivaDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._ivaDetalle.Location = new System.Drawing.Point(217, 211);
            this._ivaDetalle.Name = "_ivaDetalle";
            this._ivaDetalle.Size = new System.Drawing.Size(90, 22);
            this._ivaDetalle.TabIndex = 22;
            // 
            // _seguroDetalle
            // 
            this._seguroDetalle.BackColor = System.Drawing.SystemColors.HighlightText;
            this._seguroDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._seguroDetalle.Location = new System.Drawing.Point(217, 139);
            this._seguroDetalle.Name = "_seguroDetalle";
            this._seguroDetalle.Size = new System.Drawing.Size(90, 22);
            this._seguroDetalle.TabIndex = 23;
            // 
            // _penalizacionDetalle
            // 
            this._penalizacionDetalle.BackColor = System.Drawing.SystemColors.HighlightText;
            this._penalizacionDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._penalizacionDetalle.Location = new System.Drawing.Point(217, 103);
            this._penalizacionDetalle.Name = "_penalizacionDetalle";
            this._penalizacionDetalle.Size = new System.Drawing.Size(90, 22);
            this._penalizacionDetalle.TabIndex = 24;
            // 
            // _desgloseFactura
            // 
            this._desgloseFactura.Controls.Add(this._importePaquete);
            this._desgloseFactura.Controls.Add(this._ivaDetalle);
            this._desgloseFactura.Controls.Add(this._etiquetaImportePaquete);
            this._desgloseFactura.Controls.Add(this._etiquetaSubtotal);
            this._desgloseFactura.Controls.Add(this._etiquetaIva);
            this._desgloseFactura.Controls.Add(this._penalizacionDetalle);
            this._desgloseFactura.Controls.Add(this._seguroDetalle);
            this._desgloseFactura.Controls.Add(this._etiquetaRecargo);
            this._desgloseFactura.Controls.Add(this._etiquetaCostoFijo);
            this._desgloseFactura.Controls.Add(this._costoFijoDetalle);
            this._desgloseFactura.Controls.Add(this._etiquetaSeguro);
            this._desgloseFactura.Controls.Add(this._etiquetaPenalizacion);
            this._desgloseFactura.Controls.Add(this._recargoDetalle);
            this._desgloseFactura.Controls.Add(this._subtotalDetalle);
            this._desgloseFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._desgloseFactura.Location = new System.Drawing.Point(1028, 341);
            this._desgloseFactura.Name = "_desgloseFactura";
            this._desgloseFactura.Size = new System.Drawing.Size(317, 291);
            this._desgloseFactura.TabIndex = 3;
            this._desgloseFactura.TabStop = false;
            this._desgloseFactura.Text = "Paquetes Procesados";
            // 
            // _menuContextual
            // 
            this._menuContextual.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._menuContextual.Name = "_menuContextual";
            this._menuContextual.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 675);
            this.Controls.Add(this._desgloseFactura);
            this.Controls.Add(this._pieFormulario);
            this.Controls.Add(this._resumenLote);
            this.Controls.Add(this._detallePaquete);
            this.Controls.Add(this._grupoPaquetes);
            this.Controls.Add(this._capturaLote);
            this.Controls.Add(this._encabezado);
            this.Name = "Form1";
            this.Text = "Sistema de facturacion logística";
            this._encabezado.ResumeLayout(false);
            this._encabezado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._iconoCamion)).EndInit();
            this._capturaLote.ResumeLayout(false);
            this._capturaLote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tablaErrores)).EndInit();
            this._grupoPaquetes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._tablaPaquetes)).EndInit();
            this._detallePaquete.ResumeLayout(false);
            this._detallePaquete.PerformLayout();
            this._resumenLote.ResumeLayout(false);
            this._pieFormulario.ResumeLayout(false);
            this._pieFormulario.PerformLayout();
            this._desgloseFactura.ResumeLayout(false);
            this._desgloseFactura.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _encabezado;
        private System.Windows.Forms.Label _subtitulo;
        private System.Windows.Forms.ComboBox _selectorIdioma;
        private System.Windows.Forms.Label _etiquetaIdioma;
        private System.Windows.Forms.Label _titulo;
        private System.Windows.Forms.PictureBox _iconoCamion;
        private System.Windows.Forms.GroupBox _capturaLote;
        private System.Windows.Forms.GroupBox _grupoPaquetes;
        private System.Windows.Forms.GroupBox _detallePaquete;
        private System.Windows.Forms.GroupBox _resumenLote;
        private System.Windows.Forms.GroupBox _pieFormulario;
        private System.Windows.Forms.Button _botonHistorial;
        private System.Windows.Forms.Label _instrucciones;
        private System.Windows.Forms.Label _etiquetaSubtotal;
        private System.Windows.Forms.Label _etiquetaValorDeclarado;
        private System.Windows.Forms.Label _etiquetaPeso;
        private System.Windows.Forms.Label _etiquetaZona;
        private System.Windows.Forms.Label _etiquetaCliente;
        private System.Windows.Forms.Label _etiquetaCodigo;
        private System.Windows.Forms.Label _etiquetaValidos;
        private System.Windows.Forms.Label _tarifas;
        private System.Windows.Forms.Label _creditoEquipo;
        private System.Windows.Forms.Label _importePaquete;
        private System.Windows.Forms.Label _etiquetaImportePaquete;
        private System.Windows.Forms.Label _etiquetaIva;
        private System.Windows.Forms.Label _etiquetaCostoFijo;
        private System.Windows.Forms.Label _etiquetaSeguro;
        private System.Windows.Forms.Label _etiquetaPenalizacion;
        private System.Windows.Forms.Label _etiquetaRecargo;
        private System.Windows.Forms.Label _codigoDetalle;
        private System.Windows.Forms.Label _clienteDetalle;
        private System.Windows.Forms.Label _zonaDetalle;
        private System.Windows.Forms.Label _pesoDetalle;
        private System.Windows.Forms.Label _valorDeclaradoDetalle;
        private System.Windows.Forms.Label _subtotalDetalle;
        private System.Windows.Forms.Label _recargoDetalle;
        private System.Windows.Forms.Label _penalizacionDetalle;
        private System.Windows.Forms.Label _seguroDetalle;
        private System.Windows.Forms.Label _ivaDetalle;
        private System.Windows.Forms.Label _costoFijoDetalle;
        private System.Windows.Forms.GroupBox _desgloseFactura;
        private System.Windows.Forms.DataGridView _tablaPaquetes;
        private System.Windows.Forms.DataGridViewTextBoxColumn _columnaCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn _columnaCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn _columnaZona;
        private System.Windows.Forms.DataGridViewTextBoxColumn _columnaPeso;
        private System.Windows.Forms.DataGridViewTextBoxColumn _columnaValorDeclarado;
        private System.Windows.Forms.DataGridViewTextBoxColumn _columnaTotal;
        private System.Windows.Forms.Label _conteoValidos;
        private System.Windows.Forms.Label _conteoErroneos;
        private System.Windows.Forms.Label _etiquetaErroneos;
        private System.Windows.Forms.Label _importeLote;
        private System.Windows.Forms.Label _etiquetaImporteLote;
        private System.Windows.Forms.ContextMenuStrip _menuContextual;
        private System.Windows.Forms.TextBox _entradaLote;
        private System.Windows.Forms.Label _formatoCaptura;
        private System.Windows.Forms.Button _botonExportar;
        private System.Windows.Forms.Button _botonProcesar;
        private System.Windows.Forms.DataGridView _tablaErrores;
        private System.Windows.Forms.DataGridViewTextBoxColumn _columnaLinea;
        private System.Windows.Forms.DataGridViewTextBoxColumn _columnaMotivo;
        private System.ComponentModel.IContainer components;
    }
}

