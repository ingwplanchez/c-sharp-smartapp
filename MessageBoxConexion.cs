using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KeyBoard.Controladores;
using KeyBoard.Excepciones;

namespace SmartApp
{
    public partial class MessageBoxConexion : Form
    {        
        static MessageBoxConexion msgBox;

        static GestorPuertoSerial gestor = null;

        //[CAPTURA]

        //public String[] substrings = null;

        public static String HW="";

        //[CAPTURA]

        private void CargarPuertos() 
        {
            cb_puertos.Items.Clear();
            cb_puertos.Items.AddRange(GestorPuertoSerial.ObtenerPuertos());
            cb_puertos.SelectedIndex = 0;
        }

        public MessageBoxConexion()
        {
            InitializeComponent();
            CargarPuertos();
        }

        public static GestorPuertoSerial ShowBox(Form form)
        {
            int offsetx = 0;
            int offsety = 0;

            msgBox = new MessageBoxConexion();

            offsetx = (form.Height - msgBox.Height) / 2;
            offsety = (form.Width  - msgBox.Width) / 2;
           
            msgBox.ShowDialog();

            return gestor;
        }

        public static int Ver()
        {

            if (HW.CompareTo("SKY")==0)
            { return 1; }

            else { return 2; }
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                gestor = new GestorPuertoSerial(cb_puertos.SelectedItem.ToString());
                gestor.Conectar();
                /*
                    Comando de Modo PC. 
                */
                switch (gestor.Escribir("T")) 
                {

                    case EnumRespuestas.RESP_ACK:
                    {
                        switch (gestor.Escribir("V"))
                        {
                            case EnumRespuestas.RESP_OBTERNER_STRING:
                                
                                //while (gestor.Escribir("V") == EnumRespuestas.RESP_OBTERNER_STRING)
                                //{
                                    String resp = gestor.ObtenerRespuesta();
                                    String[] substrings = resp.Split('\n');

                                    HW = substrings[0];

                                    //break;
                                //}
                                break;

                            case EnumRespuestas.RESP_TIMEOUT:
                            case EnumRespuestas.RESP_NACK:
                                throw new SmartAppException("Error en conexión con el teclado", typeMessge.SMARTKEY_MSG_ERROR);
                        }
                    } break;

                    case EnumRespuestas.RESP_TIMEOUT:
                    case EnumRespuestas.RESP_NACK:
                        throw new SmartAppException("Error en conexión con el teclado", typeMessge.SMARTKEY_MSG_ERROR);

                }               
            }
            catch (SmartAppException ex)
            {
                //gestor.Cerrar();
                gestor = null;
                ManejarMensaje(this, ex);
            }
            finally
            {
                msgBox.Dispose();
            }
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

        private void btn_actualizar_Click(object sender, EventArgs e)
        {
            CargarPuertos(); 
        }
    }
}
