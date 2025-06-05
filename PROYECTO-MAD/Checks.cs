using PROYECTO_MAD.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROYECTO_MAD
{
    public partial class Checks : Form
    {
        public List<EntServicios> m_servicios;

        public Checks()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Cliente
            textBox2.Text = Reservacion.m_cliente.Nombre.Trim() + " " + Reservacion.m_cliente.ApellidoMaterno.Trim() + " " + Reservacion.m_cliente.ApellidoPaterno.Trim();
            textBox3.Text = Reservacion.m_cliente.Ciudad;
            textBox4.Text = Reservacion.m_cliente.Pais;
            textBox5.Text = Reservacion.m_cliente.Estado;
            textBox7.Text = Reservacion.m_cliente.RFC;
            textBox8.Text = Reservacion.m_cliente.TelCasa;
            textBox9.Text = Reservacion.m_cliente.TelCelular;
            textBox10.Text = Reservacion.m_cliente.CorreoElectronico;
            dateTimePicker1.Value = Reservacion.m_cliente.FechaNacimiento;
            comboBox1.Text = Reservacion.m_cliente.EstadoCivil;

            // Hotel
            textBox18.Text = Reservacion.m_hotel.NombreHotel;
            textBox17.Text = Reservacion.m_hotel.Ciudad;
            textBox16.Text = Reservacion.m_hotel.Pais;
            textBox15.Text = Reservacion.m_hotel.Estado;
            textBox14.Text = Reservacion.m_hotel.NoPisos.ToString();
            textBox13.Text = Reservacion.m_hotel.Locacion;
            textBox12.Text = Reservacion.m_hotel.CodHotel.ToString();
            dateTimePicker2.Value = Reservacion.m_hotel.FechaInicio;
            comboBox2.SelectedIndex = Reservacion.m_hotel.ZonaTuristica ? 1 : 0;

            listBox2.Items.Add("Domicilio: " + Reservacion.m_hotel.Locacion);
            listBox2.Items.Add("Numero de Pisos: " + Reservacion.m_hotel.NoPisos);
            listBox2.Items.Add("Servicios: ");
            foreach (string _serv in Reservacion.m_hotel.Servicios.Split(',')) {
                listBox2.Items.Add(_serv);
            }

            // Reservacion
            dateTimePicker3.Value = Reservacion.m_reservacionActual.Entrada;
            dateTimePicker4.Value = Reservacion.m_reservacionActual.Salida;
            textBox11.Text = Reservacion.m_reservacionActual.Anticipo.ToString();

            // Check In
            foreach (EntHabitacion _hab in (new EnlaceDB()).getHabitacionesHotel(Reservacion.m_hotel.NombreHotel)) {
                if (_hab.Reservacion != Reservacion.m_reservacionActual.CodReservacion) { continue; }

                dataGridView2.Rows.Add();
                DataGridViewRow l_row = dataGridView2.Rows[dataGridView2.Rows.Count - 1];
                l_row.Cells["Id"].Value = _hab.Codigo;
                l_row.Cells["Numero"].Value = _hab.NoHabitacion;
                l_row.Cells["Precio"].Value = _hab.Precio;
                l_row.Cells["Hospedaje"].Value = _hab.Hospedaje;
            }

            // Check Out
            m_servicios = new EnlaceDB().getServicios(Reservacion.m_reservacionActual.Hotel);

            listBox1.Items.Clear();
            foreach (EntServicios _serv in m_servicios) {
                listBox1.Items.Add(_serv.Nombre + ": " + _serv.Precio + "$");
            }

            if (Reservacion.m_reservacionActual.CheckIn.Year == 1) {
                groupBox2.Enabled = true;
            } else if (Reservacion.m_reservacionActual.CheckOut.Year == 1) {
                groupBox1.Enabled = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult l_editar = MessageBox.Show(this, "Quieres realizar el Check Out de Esta Reservacion?", "Advertencia", MessageBoxButtons.YesNo);
            if (l_editar == DialogResult.Yes)
            {
                string l_servicios = "";
                foreach (string _serv in listBox1.SelectedItems){ l_servicios += _serv.Split(':')[0] + ","; }
                if (l_servicios.Length > 0) { l_servicios = l_servicios.Remove(l_servicios.Length - 1); }

                string texto6 = textBox6.Text;
                string texto1 = textBox1.Text;

                //if (Reservacion.m_reservacionActual.Entrada.DayOfYear > DateTime.Now.DayOfYear) { MessageBox.Show(this, "No puede Realizar el Check-Out hasta que tu reservacion comience.", "Validacion"); return; }
                //if (Reservacion.m_reservacionActual.Salida.DayOfYear < DateTime.Now.DayOfYear) { MessageBox.Show(this, "Su dia de Salida ya Vencio.\nNo puede Realizar el Check-In", "Validacion"); return; }

                if (texto6.Length <= 0 || texto1.Length <= 0)
                {
                    MessageBox.Show(this, "Debes llenar los campos de Descuento", "Error");
                }else
                {
                    new EnlaceDB().CheckOut(Reservacion.m_actual, DateTime.Now, decimal.Parse(textBox6.Text), textBox1.Text, Program.m_usuario.NoNomina);
                    if (l_servicios.Length > 0) { new EnlaceDB().FacturarServicios(Reservacion.m_actual, l_servicios); }

                    MessageBox.Show(this, "Check Out Realizada con Exito!!!", "Informacion");

                    Reservacion.m_reservacionActual.CheckOut = DateTime.Now;
                }

               

                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            if (Reservacion.m_reservacionActual.Entrada.DayOfYear > DateTime.Now.DayOfYear) { MessageBox.Show(this, "No puede Realizar el Check-In hasta el Dia de Entrada.", "Validacion"); return; }
            if (Reservacion.m_reservacionActual.Entrada.DayOfYear < DateTime.Now.DayOfYear) { MessageBox.Show(this, "Su dia de Entrada ya Vencio.\nNo puede Realizar el Check-In", "Validacion"); return; }
        
            if (MessageBox.Show(this, "Quiere Realizar el Check-In de esta Reservacion?", "Informacion", MessageBoxButtons.YesNo) != DialogResult.Yes) { return; }

            new EnlaceDB().CheckIn(Reservacion.m_reservacionActual.CodReservacion, DateTime.Now, Program.m_usuario.NoNomina);
            MessageBox.Show(this, "Check-In Realizado con Exito!!!", "Informacion");

            Reservacion.m_reservacionActual.CheckIn = DateTime.Now;

            this.Close();
        }
    }
}
