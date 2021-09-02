using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KeyBoard.Configuracion;
using KeyBoard.Identifcadores;
using KeyBoard.Excepciones;
using KeyBoard.Controladores;

namespace SmartApp
{
    public partial class Principal : Form
    {

        private GestorGUI gestor;
        private GestorPuertoSerial gestorSerial = null;
        KeyboardForm myKeyboard = new KeyboardForm();
        HenyuBoard myHenyuboard = new HenyuBoard();

        public static String HW = "";
        public int TECLADO;

        public Principal()
        {
            InitializeComponent();

            tc_principal.DrawItem += new DrawItemEventHandler(tc_opciones_DrawItem);
            
            InicializarEventos();

            InicializarComboBox();

            gestor = new GestorGUI();
            
            DeshabilitarControlTab(true);
 
        }

        private void InicializarComboBox() 
        {
            cb_tasas_dep.Items.AddRange(Identificadores.ID_TASA); cb_tasas_dep.SelectedIndex = 0;

            cb_tipo_tecla.Items.AddRange(Identificadores.ID_TIPO_TECLA); cb_tipo_tecla.SelectedIndex = 0;
            this.SetNumerosItems(3, cd_indice_asociado);
            cb_nivel_ad.Items.AddRange(Identificadores.ID_TIPO_NIVEL); cb_nivel_ad.SelectedIndex = 0;
            this.SetNumerosItems(1, cb_tecla_ad);

            this.SetNumerosItems(0, cb_tecla_dep);
            this.SetNumerosItems(1, cb_tecla_plu);
            this.SetNumerosItems(0, cb_departamento_plu);

            this.SetNumerosItems(1, cb_inicio_descarga);
            this.SetNumerosItems(1, cb_fin_descarga);
            cb_tipo_descarga.Items.AddRange(Identificadores.ID_TIPO_TECLA); cb_tipo_descarga.SelectedIndex = 0;
        }

        private void SetNumerosItems(byte bTipo, ComboBox cb) 
        {
            int iLen = 100;

            if (bTipo == 1) iLen = 3001;
            else if (bTipo == 2) iLen = 15;
            else if (bTipo == 3) iLen = 61;
            else iLen = 100;
            for (int i = 1; i < iLen; i++) 
            {
                cb.Items.Add(Convert.ToString(i));            
            }
            cb.SelectedIndex = 0;
        }

        private void tc_opciones_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tc_principal.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tc_principal.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Black);
                g.FillRectangle(Brushes.Gray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", (float)12.0, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Far;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawImage(il_opciones.Images[e.Index], new Point(5, e.Index*50+7));
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void InicializarEventos()
        {        
            this.btn_guardar_dep.Click += new System.EventHandler(this.EventoGuardarDepartamento);
            this.btn_guardar_producto.Click += new System.EventHandler(this.EventoGuardarProducto);
            this.btn_guardar_acceso.Click += new System.EventHandler(this.EventoGuardarAccesoDirecto);
            this.btn_guardar_mensaje.Click += new System.EventHandler(this.EventoMensajeComercial);
            this.btn_descarga.Click += new System.EventHandler(this.EventoDescargas);

            this.btn_teclado_acceso_dir.Click += new System.EventHandler(this.EventoTecladoAcceso);
            this.btn_guardar_lote.Click += new System.EventHandler(this.EventoGuardarLoteAcc);
            this.btn_borrar_lote_plu.Click += new System.EventHandler(this.EventoBorrarLotePlu);
            this.btn_borrar_lote.Click += new System.EventHandler(this.EventoBorrarLoteAcc);

            this.cb_precio_maximo_dep.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventoEnteroDecimal);
            this.cb_precio_plu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventoEnteroDecimal);
            this.tb_precio_dep.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventoEnteroDecimal);
            this.cb_existencia_plu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventoEnteroDecimal);

            this.cb_precio_maximo_dep.TextChanged += new System.EventHandler(this.cb_precio_maximo_dep_TextChanged);
            this.cb_precio_plu.TextChanged += new System.EventHandler(this.cb_precio_plu_TextChanged);
            this.tb_precio_dep.TextChanged += new System.EventHandler(this.tb_precio_dep_TextChanged);
            this.cb_existencia_plu.TextChanged += new System.EventHandler(this.tb_existencia_plu_TextChanged);
            
            cb_inicio_descarga.Enabled = false;
            cb_fin_descarga.Enabled = false;
        }

        private void EventoGuardarLoteAcc(object sender, EventArgs e)
        {
            int indice = 18;
            try
            {
            gestor.ProgramarAcceso(new String[5] { cb_tipo_tecla.SelectedItem.ToString(), 
                                                   indice.ToString(),
                                                   cb_nivel_ad.SelectedItem.ToString(),
                                                   cb_tecla_ad.SelectedItem.ToString(),
                                                   TECLADO.ToString()});
            }

            catch (SmartAppException ex)
            {
                ManejarMensaje(this, ex);
            } 
        }

        private void EventoBorrarLoteAcc(object sender, EventArgs e)
        {
            //string myString;
            int indice = 20;
            try
            {
                gestor.ProgramarAcceso(new String[5] { cb_tipo_tecla.SelectedItem.ToString(), 
                                                   indice.ToString(),
                                                   cb_nivel_ad.SelectedItem.ToString(),
                                                   cb_tecla_ad.SelectedItem.ToString(),
                                                   TECLADO.ToString()});
            }

            catch (SmartAppException ex)
            {
                ManejarMensaje(this, ex);
            } 
        }

        private void EventoBorrarLotePlu(object sender, EventArgs e)
        {
            int indice = 3002;
            try
            {
                gestor.ProgramarProducto(new String[5]{ cb_precio_plu.Text,
                                                    cb_nombre_plu.Text,
                                                    cb_codigo_plu.Text,
                                                    indice.ToString(),
                                                    cb_departamento_plu.SelectedItem.ToString()});
            }

            catch (SmartAppException ex)
            {
                ManejarMensaje(this, ex);
            }
        }

        private void EventoTecladoAcceso(object sender, EventArgs e) {
            //int resp = 0;
            try
            {

                cb_tipo_tecla.Enabled = true;
                cd_indice_asociado.Enabled = true;
                cb_nivel_ad.Enabled = true;
                cb_tecla_ad.Enabled = true;

                //[VERSION]

                //[VERSION]
                //TECLADO = 2;

                if (TECLADO == 1) {
                    myKeyboard.ShowDialog();

                    cb_tipo_tecla.SelectedIndex = myKeyboard.RetornarIndexTipo;
                    cd_indice_asociado.SelectedIndex = myKeyboard.RetornarIndexIndice;
                    cb_nivel_ad.SelectedIndex = myKeyboard.RetornarIndexNivel;
                    cb_tecla_ad.SelectedIndex = myKeyboard.RetornarIndexProDep;
                }
                else
                {
                    myHenyuboard.ShowDialog();

                    cb_tipo_tecla.SelectedIndex = myHenyuboard.RetornarIndexTipo;
                    cd_indice_asociado.SelectedIndex = myHenyuboard.RetornarIndexIndice;
                    cb_nivel_ad.SelectedIndex = myHenyuboard.RetornarIndexNivel;
                    cb_tecla_ad.SelectedIndex = myHenyuboard.RetornarIndexProDep;

                }

                

            }
            catch (SmartAppException ex)
            {
                ManejarMensaje(this, ex);
            } 
        }

        private void EventoGuardarProducto(object sender, EventArgs e) 
        {

            //wplanchez
            if (cb_precio_plu.Text == "0,00")
            {
                MessageBox.Show("El precio debe ser mayor a 0.00", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

           

            try
            {

                gestor.ProgramarProducto(new String[6]{ cb_precio_plu.Text,
                                                    cb_nombre_plu.Text,
                                                    cb_codigo_plu.Text,
                                                    cb_tecla_plu.SelectedItem.ToString(),
                                                    cb_departamento_plu.SelectedItem.ToString(),
                                                    cb_existencia_plu.Text});
            }
            catch (SmartAppException ex) 
            {
                ManejarMensaje(this, ex);
            } 

        }

        private void EventoGuardarDepartamento(object sender, EventArgs e)
        {

            //wplanchez
            if (cb_precio_maximo_dep.Text == "0,00" || tb_precio_dep.Text == "0,00") {
                MessageBox.Show("El precio debe ser mayor a 0.00", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                gestor.ProgramarDepartamento(new String[5] {   cb_tasas_dep.SelectedIndex.ToString(),
                                                               tb_precio_dep.Text,
                                                               tb_nombre_dep.Text,
                                                               cb_tecla_dep.SelectedItem.ToString(),
                                                               cb_precio_maximo_dep.Text});
            }
            catch (SmartAppException ex)
            {
                ManejarMensaje(this, ex);
            } 
        }

        public int TipoTeclado()
        {

            return TECLADO;

        }

        private void EventoGuardarAccesoDirecto(object sender, EventArgs e)
        {
            try
            {
                gestor.ProgramarAcceso(new String[5] { cb_tipo_tecla.SelectedItem.ToString(), 
                                                       cd_indice_asociado.SelectedItem.ToString(),
                                                       cb_nivel_ad.SelectedItem.ToString(),
                                                       cb_tecla_ad.SelectedItem.ToString(),
                                                       TECLADO.ToString() });
            }
            catch (SmartAppException ex)
            {
                ManejarMensaje(this, ex);
            } 
        }

        private void EventoMensajeComercial(object sender, EventArgs e)
        {
            try
            {
                gestor.ProgramarMensajeComercial(new String[1] { tbm_mensaje_comercial.Text });
            }
            catch (SmartAppException ex)
            {
                ManejarMensaje(this, ex); 
            } 
        }

        private void EventoDescargas(object sender, EventArgs e)
        {
            
            String  PathDirectory = "";

            openFileDialog1.Reset();
            saveFileDialog1.Reset();

            openFileDialog1.DefaultExt = "xml";
            saveFileDialog1.DefaultExt = "xml";

            try
            {
                if (radioButton1.Checked == true)
                {
                    // Abre un  cuadro de Dialogo y Permite buscar el directorio y el archivo a subir

                    openFileDialog1.ShowDialog();
                    PathDirectory = openFileDialog1.FileName;
                    if (PathDirectory != "")
                    {
                        gestor.ProgramarSubir(cb_tipo_descarga.SelectedIndex, PathDirectory);
                    }
                }
                else
                {
                    // Abre un  cuadro de Dialogo y Permite buscar el directorio donde se va a guardar el archivo

                    saveFileDialog1.ShowDialog();
                    PathDirectory = saveFileDialog1.FileName;

                    if (PathDirectory != "")
                    {

                        gestor.ProgramarDescarga(new String[4] { cb_tipo_descarga.SelectedItem.ToString(), cb_inicio_descarga.SelectedItem.ToString(), cb_fin_descarga.SelectedItem.ToString(), PathDirectory });
                        
                    }
                }
      
                
            }
            catch (SmartAppException ex)
            {
                ManejarMensaje(this, ex);
            } 
        }

        private void EventoSubirParametros(object sender, EventArgs e) {
     
        }


        private void ManejarMensaje(Form form, SmartAppException e) 
        {
            MessageBoxIcon enumBoxIcon = MessageBoxIcon.Information;

            switch (e.TypeMessage)
            {
                case typeMessge.SMARTKEY_MSG_ERROR: enumBoxIcon = MessageBoxIcon.Error; break;
                case typeMessge.SMARTKEY_MSG_WARNING: enumBoxIcon = MessageBoxIcon.Warning; break;
                case typeMessge.SMARTKEY_MSG_OK: enumBoxIcon = MessageBoxIcon.Information; break;
            }
            
            MessageBox.Show(form, e.Message, "SmartKey App: Informacíon.", MessageBoxButtons.OK, enumBoxIcon);
        }

        private void DeshabilitarControlTab(Boolean op)
        {
            if (op)
            {
                tc_principal.Enabled = false;
            }
            else
            {
                tc_principal.Enabled = true;
            }
        }

        private void Principal_Shown(object sender, EventArgs e)
        {            
            if ((gestorSerial = MessageBoxConexion.ShowBox(this)) != null)
            {
                DeshabilitarControlTab(false);

                //int AQUI =  MessageBoxConexion.Ver();
                TECLADO = MessageBoxConexion.Ver();
                /*
                    Se envia el gestor serial creado, en el incio. 
                */
                gestor.GestorSerial = gestorSerial;
            }
        }

        private void conexiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gestorSerial != null) 
            {
                MessageBox.Show(this, "Ya existe una Conexión Abierta.", "SmartKey App: Informacíon.", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                
                return;
            }

            if ((gestorSerial = MessageBoxConexion.ShowBox(this)) != null)
            { 
                    DeshabilitarControlTab(false);
                    /*
                        Se envia el gestor serial creado, en el incio. 
                    */
                    gestor.GestorSerial = gestorSerial;
           }
        }

        private void cerrarConexiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gestorSerial != null)
            {
                gestorSerial.Escribir("S");
                gestorSerial.Cerrar();
                gestorSerial = null;
                DeshabilitarControlTab(true);
            }
            else 
            {
                MessageBox.Show(this, "No hay una Conexión Activa.", "SmartKey App: Informacíon.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cerrarAplicacion(object sender, EventArgs e)
        {
            Application.Exit();
        }

  
        // Descargar archivo
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton p = (RadioButton)sender;
            if (p.Checked)
            {
                cb_inicio_descarga.Enabled = true;
                cb_fin_descarga.Enabled = true;
            }
        }

        // Subir Archivo
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton p = (RadioButton)sender;
            if (p.Checked)
            {
                cb_inicio_descarga.Enabled = false;
                cb_fin_descarga.Enabled = false;
            }
        }

        private void EventoEnteroDecimal(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 )
            {
                e.Handled = true;
                return;
            }
        }

        public static void Moneda(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;

            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");

                if (n.Equals("")) 
                { 
                    n = ""; 
                }
                n = n.PadLeft(3, '0');
                if (n.Length > 3 & n.Substring(0, 1) == "0") 
                { 
                    n = n.Substring(1, n.Length - 1); 
                }

                v = Convert.ToDouble(n) / 100;
                
                txt.Text = string.Format("{0:F2}", v);
                txt.SelectionStart = txt.Text.Length;
            }

            catch (Exception)
            {
                
            }
        }

        public static void Moneda2(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;

            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");

                if (n.Equals(""))
                {
                    n = "";
                }
                n = n.PadLeft(4, '0');
                if (n.Length > 3 & n.Substring(0, 1) == "0")
                {
                    n = n.Substring(1, n.Length - 1);
                }

                v = Convert.ToDouble(n) / 1000;

                txt.Text = string.Format("{0:F3}", v);
                txt.SelectionStart = txt.Text.Length;
            }

            catch (Exception)
            {

            }
        }

        private void tb_precio_dep_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref tb_precio_dep);
        }

        private void cb_precio_maximo_dep_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref cb_precio_maximo_dep);
        }

        private void cb_precio_plu_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref cb_precio_plu);
        }

        private void tb_existencia_plu_TextChanged(object sender, EventArgs e) {
            Moneda2(ref cb_existencia_plu);
        }

        private void cb_tipo_tecla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_tipo_tecla.SelectedIndex == 1)
            {
                this.cb_tecla_ad.Items.Clear();
                this.SetNumerosItems(1, cb_tecla_ad);
            }

            else
            {
                this.cb_tecla_ad.Items.Clear();
                this.SetNumerosItems(0, cb_tecla_ad);
            }
        }

        private void btn_guardar_producto_Click(object sender, EventArgs e)
        {

        }

        private void cb_precio_plu_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
