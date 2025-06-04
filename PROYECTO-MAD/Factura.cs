using PROYECTO_MAD.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout;
using iText.Kernel.Pdf.Colorspace;

namespace PROYECTO_MAD
{
    public partial class Factura : Form
    {
        List<EntServicios> m_servicios;
        EntFactura m_factura;

        public Factura()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Factura_Load(object sender, EventArgs e)
        {
            m_factura = new EnlaceDB().GetFactura(Reservacion.m_actual);
            m_servicios = new EnlaceDB().GetFacturaServicios(Reservacion.m_actual);

            dateTimePicker2.Value = m_factura.FechaCreacion;
            dateTimePicker1.Value = Reservacion.m_reservacionActual.Entrada;
            dateTimePicker3.Value = Reservacion.m_reservacionActual.Salida;
            textBox6.Text = Reservacion.m_cliente.CorreoElectronico;
            textBox2.Text = Program.m_usuario.Nombre;
            comboBox1.Text = m_factura.FormaPago;
            textBox1.Text = Reservacion.m_hotel.NombreHotel;
            textBox3.Text = Reservacion.m_hotel.Locacion;
            textBox7.Text = Reservacion.m_cliente.Nombre;
            textBox5.Text = Reservacion.m_cliente.Ciudad;
            textBox4.Text = Reservacion.m_hotel.Ciudad;

            // Hospedaje
            foreach (EntHabitacion _hab in (new EnlaceDB()).getHabitacionesHotel(Reservacion.m_hotel.NombreHotel))
            {
                if (_hab.Reservacion != Reservacion.m_reservacionActual.CodReservacion) { continue; }

                dataGridView1.Rows.Add();
                DataGridViewRow l_row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                l_row.Cells["Cantidad"].Value = Reservacion.m_reservacionActual.Salida.Day - Reservacion.m_reservacionActual.Entrada.Day;
                l_row.Cells["Desc"].Value = "Hospedaje en " + _hab.NoHabitacion + " para " + _hab.Hospedaje + " personas";
                l_row.Cells["PrecioUnitario"].Value = "$ " + _hab.Precio;
                l_row.Cells["Total"].Value = "$ " + _hab.Precio * _hab.Hospedaje * (Reservacion.m_reservacionActual.Salida.Day - Reservacion.m_reservacionActual.Entrada.Day);
            }

            foreach (EntServicios _servicio in m_servicios)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow l_row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                l_row.Cells["Cantidad"].Value = "1";
                l_row.Cells["Desc"].Value = _servicio.Nombre;
                l_row.Cells["PrecioUnitario"].Value = "$ " + _servicio.Precio;
                l_row.Cells["Total"].Value = "$ " + _servicio.Precio;
            }

            // Anticipo
            dataGridView1.Rows.Add();
            DataGridViewRow l_row2 = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
            l_row2.Cells["Cantidad"].Value = "1";
            l_row2.Cells["Desc"].Value = "Anticipo Aplicado";
            l_row2.Cells["PrecioUnitario"].Value = "$ " + -Reservacion.m_reservacionActual.Anticipo;
            l_row2.Cells["Total"].Value = "$ " + -m_factura.Anticipo;

            // Descuento
            dataGridView1.Rows.Add();
            DataGridViewRow l_row3 = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
            l_row3.Cells["Cantidad"].Value = "1";
            l_row3.Cells["Desc"].Value = m_factura.NombreDescuento;
            l_row3.Cells["PrecioUnitario"].Value = "% " + m_factura.Descuento;
            l_row3.Cells["Total"].Value = "$ " + -((m_factura.PrecioInicial + m_factura.PrecioServicios - m_factura.Anticipo) * (m_factura.Descuento / 100));

            textBox8.Text = "$ " + m_factura.PrecioTotal.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                saveFileDialog.Title = "Guardar archivo PDF";
                saveFileDialog.FileName = "factura.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    PdfWriter l_writer = new PdfWriter(filePath);
                    PdfDocument l_pdf = new PdfDocument(l_writer);

                    Document l_document = new Document(l_pdf, PageSize.LETTER);
                    l_document.SetMargins(60, 20, 55, 20);

                    // <----------- [Line 01] ----------->
                    string l_prf01 = Reservacion.m_reservacionActual.HotelNombre;
                    var l_parraf01 = new Paragraph(l_prf01);
                    l_parraf01.SetTextAlignment(TextAlignment.LEFT);
                    l_parraf01.SetFontSize(20);
                    l_parraf01.SetFontColor(iText.Kernel.Colors.DeviceRgb.RED);
                    l_document.Add(l_parraf01);

                    // <----------- [Line 02] ----------->
                    var l_parraf02 = new Paragraph("Folio Interno:" + m_factura.NoFactura.ToString()); l_document.Add(l_parraf02);

                    // <----------- [Line 03] ----------->
                    Table l_table01 = new Table(new float[] { 1, 1, 1 });
                    l_table01.SetWidth(UnitValue.CreatePercentValue(100));

                    l_table01.AddHeaderCell(new Cell().Add(new Paragraph("Fecha").SetTextAlignment(TextAlignment.CENTER)));
                    l_table01.AddHeaderCell(new Cell().Add(new Paragraph("Datos del Emisor").SetTextAlignment(TextAlignment.CENTER)));
                    l_table01.AddHeaderCell(new Cell().Add(new Paragraph("Datos del Receptor").SetTextAlignment(TextAlignment.CENTER)));

                    l_table01.AddCell(new Cell().Add(new Paragraph(dateTimePicker2.Value.ToString("f")).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)));
                    l_table01.AddCell(new Cell().Add(new Paragraph(textBox2.Text + "\n" + textBox3.Text + "\n" + textBox4.Text).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)));
                    l_table01.AddCell(new Cell().Add(new Paragraph(textBox7.Text + "\n" + textBox6.Text + "\n" + textBox5.Text).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)));

                    l_document.Add(l_table01);

                    // <----------- [Line 03] ----------->
                    Table l_table02 = new Table(new float[] { 1, 1, 1, 1 });
                    l_table02.SetWidth(UnitValue.CreatePercentValue(100));

                    l_table02.AddHeaderCell(new Cell().Add(new Paragraph("Cantidad").SetTextAlignment(TextAlignment.CENTER)));
                    l_table02.AddHeaderCell(new Cell().Add(new Paragraph("Descripcion").SetTextAlignment(TextAlignment.CENTER)));
                    l_table02.AddHeaderCell(new Cell().Add(new Paragraph("Precio Unitario").SetTextAlignment(TextAlignment.CENTER)));
                    l_table02.AddHeaderCell(new Cell().Add(new Paragraph("Total").SetTextAlignment(TextAlignment.CENTER)));

                    string l_ListaCantidades = "";
                    string l_ListaDescripciones = "";
                    string l_ListaUnitarios = "";
                    string l_ListaTotales = "";

                    foreach (DataGridViewRow _row in dataGridView1.Rows) {
                        if (_row.IsNewRow) { continue; }

                        l_ListaCantidades += _row.Cells["Cantidad"].Value + "\n";
                        l_ListaDescripciones += _row.Cells["Desc"].Value + "\n";
                        l_ListaUnitarios += "" + _row.Cells["PrecioUnitario"].Value + "\n";
                        l_ListaTotales += "" + _row.Cells["Total"].Value + "\n";
                    }
                    l_ListaCantidades = l_ListaCantidades.Remove(l_ListaCantidades.Length - 1);
                    l_ListaDescripciones = l_ListaDescripciones.Remove(l_ListaDescripciones.Length - 1);
                    l_ListaUnitarios = l_ListaUnitarios.Remove(l_ListaUnitarios.Length - 1);
                    l_ListaTotales = l_ListaTotales.Remove(l_ListaTotales.Length - 1);


                    for (int i = 0; i < 5; i++) { l_ListaCantidades += "\n"; }
                    for (int i = 0; i < 5; i++) { l_ListaDescripciones += "\n"; }
                    for (int i = 0; i < 5; i++) { l_ListaUnitarios += "\n"; }
                    for (int i = 0; i < 5; i++) { l_ListaTotales += "\n"; }

                    l_table02.AddCell(new Cell().Add(new Paragraph(l_ListaCantidades).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)));
                    l_table02.AddCell(new Cell().Add(new Paragraph(l_ListaDescripciones).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)));
                    l_table02.AddCell(new Cell().Add(new Paragraph(l_ListaUnitarios).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)));
                    l_table02.AddCell(new Cell().Add(new Paragraph(l_ListaTotales).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)));

                    l_document.Add(l_table02);

                    // <----------- [Line 02] ----------->
                    var l_parraf03 = new Paragraph("Total: $ " + m_factura.PrecioTotal).SetTextAlignment(TextAlignment.RIGHT); l_document.Add(l_parraf03);
                    var l_parraf04 = new Paragraph("Forma de Pago: " + comboBox1.Text); l_document.Add(l_parraf04);

                    //
                    l_document.Close();

                    MessageBox.Show(this, "Recibo Guardado con Exito!", "Informacion");
                }
            }
        }
    }
}
