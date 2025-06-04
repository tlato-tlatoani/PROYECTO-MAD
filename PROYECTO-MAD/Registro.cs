using PROYECTO_MAD.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTO_MAD.Resources
{
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InicioDeSesion.m_instance.Show();
            this.Close();
        }

        private void Registro_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Correo Electronico.", "Validacion"); return; }
            if (textBox1.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar una Contraseña.", "Validacion"); return; }
            if (textBox3.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Nombre.", "Validacion"); return; }
            if (textBox7.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Telefono de Casa.", "Validacion"); return; }
            if (textBox4.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Apellido Paterno.", "Validacion"); return; }
            if (textBox5.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Apellido Materno.", "Validacion"); return; }
            if (textBox8.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Telefono Celular.", "Validacion"); return; }
            if (textBox6.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Numero de Nomina.", "Validacion"); return; }

            if (!Regex.IsMatch(textBox1.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{8,}$")) {
                MessageBox.Show(this, "La contraseña debe tener minimo 8 caracteres, 1 caracter especial, 1 minúscula y 1 mayúsucula", "Formato Incorrecto");
                return;
            }

            int l_nomina = -1;
            if (!int.TryParse(textBox6.Text, out l_nomina)) { MessageBox.Show(this, "El Numero de Nomina debe ser un Numero Valido.", "Validacion"); return; }

            if (dateTimePicker1.Value.Year < DateTime.Now.Year - 100 || dateTimePicker1.Value.Year > DateTime.Now.Year) { MessageBox.Show(this, "Debe Colocar una Fecha de Nacimiento Valida.", "Validacion"); return; }
            if (dateTimePicker1.Value.Year > DateTime.Now.Year - 17) { MessageBox.Show(this, "Debe ser Mayor de Edad.", "Validacion"); return; }

            if (MessageBox.Show(this, "Quiere Registrarse con estos Datos?", "Informacion", MessageBoxButtons.YesNo) != DialogResult.Yes) { return; }

            Usuario l_usuario = new Usuario(
                l_nomina,
                textBox3.Text,
                textBox4.Text,
                textBox5.Text,
                textBox2.Text,
                textBox1.Text,
                textBox8.Text,
                textBox7.Text,
                dateTimePicker1.Value,
                false
            );

            EnlaceDB l_enlace = new EnlaceDB();
            if (l_enlace.Registrar(l_usuario, false)) {
                MessageBox.Show(this, "Usuario Registrado Correctamente.\nEspere a que un Administrador lo Active.", "Informacion");
                this.Close();
                InicioDeSesion.m_instance.Show();
            } else {
                MessageBox.Show(this, "El Correo que intenta colocar ya se encuentra en uso...", "Error");
            }
        }
    }
}
