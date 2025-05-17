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
    public partial class Factura : Form
    {
        List<EntServicios> m_servicios;
        EntReservacion m_reservacion;
        EntClientes m_cliente;
        EntFactura m_factura;
        Hotel m_hotel;

        public Factura()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Factura_Load(object sender, EventArgs e)
        {
            m_hotel = new EnlaceDB().getHotel(Reservacion.m_instance.comboBox3.Text);
            m_reservacion = new EnlaceDB().getReservacion(Reservacion.m_actual);
            m_cliente = new EnlaceDB().getCliente(Reservacion.m_cliente);
            m_factura = new EnlaceDB().GetFactura(Reservacion.m_actual);
            m_servicios = new EnlaceDB().GetFacturaServicios(Reservacion.m_actual);

            dateTimePicker2.Value = m_factura.FechaCreacion;
            textBox6.Text = m_cliente.CorreoElectronico;
            textBox2.Text = Program.m_usuario.Nombre;
            textBox11.Text = m_factura.FormaPago;
            textBox1.Text = m_hotel.NombreHotel;
            textBox3.Text = m_hotel.Locacion;
            textBox7.Text = m_cliente.Nombre;
            textBox5.Text = m_cliente.Ciudad;
            textBox4.Text = m_hotel.Ciudad;

            dataGridView1.Rows.Add();
            DataGridViewRow l_row1 = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
            l_row1.Cells["Cantidad"].Value = m_reservacion.Dias;
            l_row1.Cells["Desc"].Value = "Noches de Hospedaje en " + m_reservacion.TipoHabitacionNombre;
            l_row1.Cells["PrecioUnitario"].Value = "$ " + m_reservacion.PrecioNoche;
            l_row1.Cells["Total"].Value = "$ " + (m_reservacion.PrecioNoche * m_reservacion.Dias);

            foreach (EntServicios _servicio in m_servicios)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow l_row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                l_row.Cells["Cantidad"].Value = "1";
                l_row.Cells["Desc"].Value = _servicio.Nombre;
                l_row.Cells["PrecioUnitario"].Value = "$ " + _servicio.Precio;
                l_row.Cells["Total"].Value = "$ " + _servicio.Precio;
            }

            textBox8.Text = "$ " + m_factura.PrecioTotal.ToString();
        }
    }
}
