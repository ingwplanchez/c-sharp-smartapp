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
    public partial class KeyboardForm : Form
    {

        // VARIABLES DE RECEPCION DSDE LA VENTANA PRINCIPAL

        private bool bNivel = true;
        private string sProdep = "";
        private string sMostrarOp; 

        // VARIABLES DE ENVIO HACIA LA VENTANA PRINCIPAL

        private int iIndex_tipo;
        private int iIndex_indice;
        private int iIndex_nivel;
        private int iIndex_prodep;

        
        public KeyboardForm()
        {
            InitializeComponent();
            InicializarEventos2();
            InicializarComboBox2();
        }

        private void InicializarEventos2()
        {
            this.btn_key_efectivo.Click += new System.EventHandler(this.EventoEnviarDato); 
            this.btn_key_c.Click += new System.EventHandler(this.EventoCancelar);

            this.btn_teclado_guardar.Click += new System.EventHandler(this.EventoEnviarDato);
            this.btn_teclado_limpiar.Click += new System.EventHandler(this.EventoLimpiar);
            this.btn_teclado_cancelar.Click += new System.EventHandler(this.EventoCancelar);

            this.btn_alt_nivel.Click += new System.EventHandler(this.EventoNivel); 
            this.btn_key_dep.Click += new System.EventHandler(this.EventoDepartamento);
            this.btn_key_plu.Click += new System.EventHandler(this.EventoPLU);

            this.cb_tipo_tecla.SelectedIndexChanged += new System.EventHandler(this.cb_tipo_tecla_SelectedIndexChanged); // Asigna el rango de Intems Correcto Para DEP y PLU

            //this.btn_Key1.Click += new System.EventHandler(this.EventoKey1);
            //this.btn_Key2.Click += new System.EventHandler(this.EventoKey2);
            //this.btn_Key3.Click += new System.EventHandler(this.EventoKey3);

            //this.btn_Key4.Click += new System.EventHandler(this.EventoKey4);
            //this.btn_Key5.Click += new System.EventHandler(this.EventoKey5);
            //this.btn_Key6.Click += new System.EventHandler(this.EventoKey6);

            //this.btn_Key7.Click += new System.EventHandler(this.EventoKey7);
            //this.btn_Key8.Click += new System.EventHandler(this.EventoKey8);
            //this.btn_Key9.Click += new System.EventHandler(this.EventoKey9);
            //this.btn_Key0.Click += new System.EventHandler(this.EventoKey0);

        }

        public void InicializarComboBox2()
        {
            cb_tipo_tecla.Items.AddRange(Identificadores.ID_TIPO_TECLA); cb_tipo_tecla.SelectedIndex = 0;
            this.SetNumerosItems(3, cd_indice_asociado);
            cb_nivel_ad.Items.AddRange(Identificadores.ID_TIPO_NIVEL); cb_nivel_ad.SelectedIndex = 0;
            this.SetNumerosItems(1, cb_tecla_ad);
        }

        public void SetNumerosItems(byte bTipo, ComboBox cb)
        {
            int iLen = 100;

            if (bTipo == 1) iLen = 3000;
            else if (bTipo == 2) iLen = 15;
            else if (bTipo == 3) iLen = 9;
            for (int i = 1; i < iLen; i++)
            {
                cb.Items.Add(Convert.ToString(i));
            }
            cb.SelectedIndex = 0;
        }

        private void EventoTecladoNumerico(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void EventoEnviarDato(object sender, EventArgs e)
        {
            /*
            if (string.IsNullOrEmpty(txtTeclaN1.Text))
            {
                MessageBox.Show(this, "No se puede enviar campo vacio. El rango definido es de 0 - 99 Departamentos", "SmartKey App: Informacíon.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
             * 
             * // index = int.Parse(txtTeclaN1.Text);
            */

            try
            {
                iIndex_tipo = cb_tipo_tecla.SelectedIndex;
                iIndex_indice = cd_indice_asociado.SelectedIndex;
                iIndex_nivel = cb_nivel_ad.SelectedIndex;
                iIndex_prodep = cb_tecla_ad.SelectedIndex;
                //iIndex_prodep = int.Parse(sProdep);

                cb_tipo_tecla.SelectedIndex = 0;
                cd_indice_asociado.SelectedIndex = 0;
                cb_nivel_ad.SelectedIndex = 0;
                cb_tecla_ad.SelectedIndex = 0;
                sProdep = "0";
                this.Close();
            }
            catch { }           
        }
        
        private void EventoLimpiar(object sender, EventArgs e)
        {
            if (cb_tipo_tecla.SelectedIndex==1) 
            {
                cb_tipo_tecla.SelectedIndex = 1; 
            }
            else
            { 
                cb_tipo_tecla.SelectedIndex = 0;
            }
           
            cd_indice_asociado.SelectedIndex = 0;
            cb_nivel_ad.SelectedIndex = 0;
            cb_tecla_ad.SelectedIndex = 0;

            sProdep = "0";
            iIndex_prodep = int.Parse(sProdep);
            cb_tecla_ad.SelectedIndex = iIndex_prodep;
            this.pictureBox1.Image = global::SmartApp.Properties.Resources.PLU19;
        }
        
        private void EventoCancelar(object sender, EventArgs e)
        {
            iIndex_tipo = 0;
            iIndex_indice = 0;
            iIndex_nivel = 0;
            iIndex_prodep = 0;
                
            cb_tipo_tecla.SelectedIndex = 0;
            cd_indice_asociado.SelectedIndex = 0;
            cb_nivel_ad.SelectedIndex = 0;
            cb_tecla_ad.SelectedIndex = 0;
            sProdep = "0";
            this.pictureBox1.Image = global::SmartApp.Properties.Resources.PLU19;
            this.Close();
        }

        private void EventoNivel(object sender, EventArgs e)
        {
            if (bNivel)
            {
                cb_nivel_ad.SelectedIndex = 1;
                //dataGridView1.Enabled = false;
                //dataGridView2.Enabled = true;
                bNivel = false;
            }

            else 
            {
                cb_nivel_ad.SelectedIndex = 0;
                //dataGridView1.Enabled = true;
                //dataGridView2.Enabled = false;
                bNivel = true;
            }
        }

        private void EventoDepartamento(object sender, EventArgs e)
        {
            cb_tipo_tecla.SelectedIndex = 1;
            this.cb_tecla_ad.Items.Clear();
            this.SetNumerosItems(0, cb_tecla_ad);
        }

        private void EventoPLU(object sender, EventArgs e)
        {
            cb_tipo_tecla.SelectedIndex = 0;
            this.cb_tecla_ad.Items.Clear();
            this.SetNumerosItems(1, cb_tecla_ad);
        }

        private void EventoKey1(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "1";
                iIndex_prodep = int.Parse(sProdep);
                //iIndex_prodep = Convert.ToInt16(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
      
            }
            catch { }
        }

        private void EventoKey2(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "2";
                //textBox1.Text = sProdep;
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventoKey3(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "3";
                //textBox1.Text = sProdep;
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventoKey4(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "4";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }

        }

        private void EventoKey5(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "5";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventoKey6(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "6";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventoKey7(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "7";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventoKey8(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "8";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventoKey9(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "9";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventoKey0(object sender, EventArgs e)
        {

            try
            {
                sProdep = sProdep + "0";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        // Metodos GET , SET para intecambio de datos entre formularios

        public string MostrarOp
        {
            set
            {
                sMostrarOp = value;
                //groupBox2.Text = mostrarOp;
                //lblOperacion.Text = mostrarOp;
            }
        }

        // Retorna Index Seleccionado

        public int RetornarIndexTipo
        {
            get { return iIndex_tipo; }
        }

        public int RetornarIndexIndice
        {
            get { return iIndex_indice; }
        }

        public int RetornarIndexNivel
        {
            get { return iIndex_nivel; }
        }

        public int RetornarIndexProDep
        {
            get { return iIndex_prodep; }
        }

        private void btn_key_plu19_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::SmartApp.Properties.Resources.PLU19;
            iIndex_indice = 0;
            cd_indice_asociado.SelectedIndex = iIndex_indice;
        }

        private void btn_key_plu210_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::SmartApp.Properties.Resources.PLU210;
            iIndex_indice = 1;
            cd_indice_asociado.SelectedIndex = iIndex_indice;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::SmartApp.Properties.Resources.PLU311;
            iIndex_indice = 2;
            cd_indice_asociado.SelectedIndex = iIndex_indice;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::SmartApp.Properties.Resources.PLU412;
            iIndex_indice = 3;
            cd_indice_asociado.SelectedIndex = iIndex_indice;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::SmartApp.Properties.Resources.PLU513;
            iIndex_indice = 4;
            cd_indice_asociado.SelectedIndex = iIndex_indice;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::SmartApp.Properties.Resources.PLU614;
            iIndex_indice = 5;
            cd_indice_asociado.SelectedIndex = iIndex_indice;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::SmartApp.Properties.Resources.PLU715;
            iIndex_indice = 6;
            cd_indice_asociado.SelectedIndex = iIndex_indice;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::SmartApp.Properties.Resources.PLU816;
            iIndex_indice = 7;
            cd_indice_asociado.SelectedIndex = iIndex_indice;
        }

        private void cb_tipo_tecla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_tipo_tecla.SelectedIndex == 1)
            {
                this.cb_tecla_ad.Items.Clear();
                this.SetNumerosItems(0, cb_tecla_ad);
            }

            else
            {
                this.cb_tecla_ad.Items.Clear();
                this.SetNumerosItems(1, cb_tecla_ad);
            }
        }

        private void KeyboardForm_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.NumPad1) {

            //    textBox1.Text = textBox1.Text + "1";
            //}
        }
    }
}
