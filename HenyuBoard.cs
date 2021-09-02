using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KeyBoard.Identifcadores;

namespace SmartApp
{
    public partial class HenyuBoard : Form
    {

        // VARIABLES DE RECEPCION DSDE LA VENTANA PRINCIPAL

        private bool bNivel = false;
        private string sProdep = "";
        private string sMostrarOp;

        // VARIABLES DE ENVIO HACIA LA VENTANA PRINCIPAL

        private int iIndex_tipo;
        private int iIndex_indice;
        private int iIndex_nivel;
        private int iIndex_prodep;

        public HenyuBoard()
        {
            InitializeComponent();
            InicializarEventos2();
            InicializarComboBox2();
        }

        private void InicializarEventos2()
        {
            this.bt_total.Click += new System.EventHandler(this.EventoEnviarDato);
            this.btn_esc.Click += new System.EventHandler(this.EventoCancelar);
            this.bt_cor.Click += new System.EventHandler(this.EventoLimpiar);

            this.btn_teclado_guardar.Click += new System.EventHandler(this.EventoEnviarDato);
            this.btn_teclado_limpiar.Click += new System.EventHandler(this.EventoLimpiar);
            this.btn_teclado_cancelar.Click += new System.EventHandler(this.EventoCancelar);

            this.bt_alt.Click += new System.EventHandler(this.EventoNivel);
            this.bt_dep.Click += new System.EventHandler(this.EventoDepartamento);
            this.bt_plu.Click += new System.EventHandler(this.EventoPLU);

             this.bt_a.Click += new System.EventHandler(this.EventoKey1);
             this.bt_b.Click += new System.EventHandler(this.EventoKey2);
             this.bt_c.Click += new System.EventHandler(this.EventoKey3);
             this.bt_d.Click += new System.EventHandler(this.EventoKey4);
             this.bt_e.Click += new System.EventHandler(this.EventoKey5);
             this.bt_f.Click += new System.EventHandler(this.EventoKey6);

            this.bt_g.Click += new System.EventHandler(this.EventoKey7);
            this.bt_h.Click += new System.EventHandler(this.EventoKey8);
            this.bt_i.Click += new System.EventHandler(this.EventoKey9);
            this.bt_j.Click += new System.EventHandler(this.EventoKey10);
            this.bt_k.Click += new System.EventHandler(this.EventoKey11);
            this.bt_l.Click += new System.EventHandler(this.EventoKey12);

            this.bt_m.Click += new System.EventHandler(this.EventoKey13);
            this.bt_n.Click += new System.EventHandler(this.EventoKey14);
            this.bt_o.Click += new System.EventHandler(this.EventoKey15);
            this.bt_p.Click += new System.EventHandler(this.EventoKey16);
            this.bt_q.Click += new System.EventHandler(this.EventoKey17);
            this.bt_r.Click += new System.EventHandler(this.EventoKey18);

            this.bt_s.Click += new System.EventHandler(this.EventoKey19);
            this.bt_t.Click += new System.EventHandler(this.EventoKey20);
            this.bt_u.Click += new System.EventHandler(this.EventoKey21);
            this.bt_v.Click += new System.EventHandler(this.EventoKey22);
            this.bt_w.Click += new System.EventHandler(this.EventoKey23);
            this.bt_x.Click += new System.EventHandler(this.EventoKey24);

            this.bt_y.Click += new System.EventHandler(this.EventoKey25);
            this.bt_z.Click += new System.EventHandler(this.EventoKey26);
            this.bt_na.Click += new System.EventHandler(this.EventoKey27);
            this.bt_op1.Click += new System.EventHandler(this.EventoKey28);
            this.bt_op2.Click += new System.EventHandler(this.EventoKey29);
            this.bt_op3.Click += new System.EventHandler(this.EventoKey30);

            this.cb_tipo_tecla.SelectedIndexChanged += new System.EventHandler(this.cb_tipo_tecla_SelectedIndexChanged); // Asigna el rango de Intems Correcto Para DEP y PLU

            // this.bt_1.Click += new System.EventHandler(this.EventKey1);
            // this.bt_2.Click += new System.EventHandler(this.EventKey2);
            // this.bt_3.Click += new System.EventHandler(this.EventKey3);

            //this.bt_4.Click += new System.EventHandler(this.EventKey4);
            //this.bt_5.Click += new System.EventHandler(this.EventKey5);
            //this.bt_6.Click += new System.EventHandler(this.EventKey6);

            //this.bt_7.Click += new System.EventHandler(this.EventKey7);
            //this.bt_8.Click += new System.EventHandler(this.EventKey8);
            //this.bt_9.Click += new System.EventHandler(this.EventKey9);

            //this.bt_0.Click += new System.EventHandler(this.EventKey0);

            
        }

        public void InicializarComboBox2()
        {
            cb_tipo_tecla.Items.AddRange(Identificadores.ID_TIPO_TECLA); cb_tipo_tecla.SelectedIndex = 0;
            this.SetNumerosItems(3, cd_indice_asociado);
            cb_nivel_ad.Items.AddRange(Identificadores.ID_TIPO_NIVEL); cb_nivel_ad.SelectedIndex = 0;
            this.SetNumerosItems(1, cb_tecla_ad);
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
            if (cb_tipo_tecla.SelectedIndex == 1)
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
            this.pictureBox1.Image = global::SmartApp.Properties.Resources.A;
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
            this.pictureBox1.Image = global::SmartApp.Properties.Resources.A;
            this.Close();
        }

        private void EventoNivel(object sender, EventArgs e)
        {
            if (bNivel==false)
            {
                cb_nivel_ad.SelectedIndex = 1;
                //dataGridView1.Enabled = false;
                //dataGridView2.Enabled = true;
                bNivel = true;
            }

            else
            {
                cb_nivel_ad.SelectedIndex = 0;
                //dataGridView1.Enabled = true;
                //dataGridView2.Enabled = false;
                bNivel = false;
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

        public void SetNumerosItems(byte bTipo, ComboBox cb)
        {
            int iLen = 100;

            if (bTipo == 1) iLen = 3001;
            else if (bTipo == 2) iLen = 60;
            else if (bTipo == 3) iLen = 61;
            for (int i = 1; i < iLen; i++)
            {
                cb.Items.Add(Convert.ToString(i));
            }
            cb.SelectedIndex = 0;
        }

        private void EventKey1(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "1";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;

            }
            catch { }
        }

        private void EventKey2(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "2";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventKey3(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "3";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventKey4(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "4";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }

        }

        private void EventKey5(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "5";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventKey6(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "6";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventKey7(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "7";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventKey8(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "8";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventKey9(object sender, EventArgs e)
        {
            try
            {
                sProdep = sProdep + "9";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }

        private void EventKey0(object sender, EventArgs e)
        {

            try
            {
                sProdep = sProdep + "0";
                iIndex_prodep = int.Parse(sProdep);
                cb_tecla_ad.SelectedIndex = iIndex_prodep;
            }
            catch { }
        }


        private void EventoKey1(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.A;
                iIndex_indice = 0;
                if(bNivel)
                cd_indice_asociado.SelectedIndex = iIndex_indice+30;
                else
                cd_indice_asociado.SelectedIndex = iIndex_indice;

            }
            catch { }
        }

        private void EventoKey2(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.B;
                iIndex_indice = 1;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey3(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.C;
                iIndex_indice = 2;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey4(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.D;
                iIndex_indice = 3;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey5(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.E;
                iIndex_indice = 4;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey6(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.F;
                iIndex_indice = 5;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey7(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.G;
                iIndex_indice = 6;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey8(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.H;
                iIndex_indice = 7;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey9(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.I;
                iIndex_indice = 8;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey10(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.J;
                iIndex_indice = 9;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey11(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.K;
                iIndex_indice = 10;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey12(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.L;
                iIndex_indice = 11;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey13(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.M;
                iIndex_indice = 12;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey14(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.N;
                iIndex_indice = 13;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey15(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.O;
                iIndex_indice = 14;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey16(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.P;
                iIndex_indice = 15;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey17(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.Q;
                iIndex_indice = 16;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey18(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.R;
                iIndex_indice = 17;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey19(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.S;
                iIndex_indice = 18;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey20(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.T;
                iIndex_indice = 19;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey21(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.U;
                iIndex_indice = 20;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey22(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.V;
                iIndex_indice = 21;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey23(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.W;
                iIndex_indice = 22;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey24(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.X;
                iIndex_indice = 23;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey25(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.Y;
                iIndex_indice = 24;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey26(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.Z;
                iIndex_indice = 25;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey27(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.CALC;
                iIndex_indice = 26;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey28(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.OP1;
                iIndex_indice = 27;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey29(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.OP2;
                iIndex_indice = 28;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        private void EventoKey30(object sender, EventArgs e)
        {
            try
            {
                this.pictureBox1.Image = global::SmartApp.Properties.Resources.OP3;
                iIndex_indice = 29;
                if (bNivel)
                    cd_indice_asociado.SelectedIndex = iIndex_indice + 30;
                else
                    cd_indice_asociado.SelectedIndex = iIndex_indice;
            }
            catch { }
        }

        // Retorna Index Seleccionado

        public string MostrarOp
        {
            set
            {
                sMostrarOp = value;
                //groupBox2.Text = mostrarOp;
                //lblOperacion.Text = mostrarOp;
            }
        }

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void HenyuBoard_Load(object sender, EventArgs e)
        {

        }
    }
}
