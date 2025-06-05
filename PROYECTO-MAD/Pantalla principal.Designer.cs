namespace PROYECTO_MAD
{
    partial class Pantalla_principal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pantalla_principal));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.usuarioToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPerfilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verEditarRegistrarUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reservacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verReservacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hotelesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verHotelesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.habitacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.habitacionesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tiposDeHabitacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verServiciosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.verClientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verHistorialDeClientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeOcupaciónPorHotelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeVentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sans Serif Collection", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(560, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(873, 67);
            this.label1.TabIndex = 92;
            this.label1.Text = "---Bienvenido a la cadena GatoHotelero---";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(605, 274);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(557, 345);
            this.pictureBox1.TabIndex = 93;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LavenderBlush;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuarioToolStripMenuItem1,
            this.reservacionesToolStripMenuItem,
            this.hotelesToolStripMenuItem,
            this.clientesToolStripMenuItem1,
            this.reportesToolStripMenuItem,
            this.cerrarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1709, 28);
            this.menuStrip1.TabIndex = 94;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // usuarioToolStripMenuItem1
            // 
            this.usuarioToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPerfilToolStripMenuItem,
            this.verEditarRegistrarUsuarioToolStripMenuItem});
            this.usuarioToolStripMenuItem1.Name = "usuarioToolStripMenuItem1";
            this.usuarioToolStripMenuItem1.Size = new System.Drawing.Size(73, 24);
            this.usuarioToolStripMenuItem1.Text = "Usuario";
            // 
            // miPerfilToolStripMenuItem
            // 
            this.miPerfilToolStripMenuItem.Name = "miPerfilToolStripMenuItem";
            this.miPerfilToolStripMenuItem.Size = new System.Drawing.Size(277, 26);
            this.miPerfilToolStripMenuItem.Text = "Mi Perfil";
            this.miPerfilToolStripMenuItem.Click += new System.EventHandler(this.miPerfilToolStripMenuItem_Click);
            // 
            // verEditarRegistrarUsuarioToolStripMenuItem
            // 
            this.verEditarRegistrarUsuarioToolStripMenuItem.Name = "verEditarRegistrarUsuarioToolStripMenuItem";
            this.verEditarRegistrarUsuarioToolStripMenuItem.Size = new System.Drawing.Size(277, 26);
            this.verEditarRegistrarUsuarioToolStripMenuItem.Text = "Ver/Editar/Registrar Usuario";
            this.verEditarRegistrarUsuarioToolStripMenuItem.Click += new System.EventHandler(this.verEditarRegistrarUsuarioToolStripMenuItem_Click);
            // 
            // reservacionesToolStripMenuItem
            // 
            this.reservacionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verReservacionesToolStripMenuItem});
            this.reservacionesToolStripMenuItem.Name = "reservacionesToolStripMenuItem";
            this.reservacionesToolStripMenuItem.Size = new System.Drawing.Size(116, 24);
            this.reservacionesToolStripMenuItem.Text = "Reservaciones";
            // 
            // verReservacionesToolStripMenuItem
            // 
            this.verReservacionesToolStripMenuItem.Name = "verReservacionesToolStripMenuItem";
            this.verReservacionesToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.verReservacionesToolStripMenuItem.Text = "Ver Reservaciones";
            this.verReservacionesToolStripMenuItem.Click += new System.EventHandler(this.verReservacionesToolStripMenuItem_Click);
            // 
            // hotelesToolStripMenuItem
            // 
            this.hotelesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verHotelesToolStripMenuItem,
            this.habitacionesToolStripMenuItem,
            this.verServiciosToolStripMenuItem});
            this.hotelesToolStripMenuItem.Name = "hotelesToolStripMenuItem";
            this.hotelesToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.hotelesToolStripMenuItem.Text = "Hoteles";
            this.hotelesToolStripMenuItem.Click += new System.EventHandler(this.hotelesToolStripMenuItem_Click);
            // 
            // verHotelesToolStripMenuItem
            // 
            this.verHotelesToolStripMenuItem.Name = "verHotelesToolStripMenuItem";
            this.verHotelesToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.verHotelesToolStripMenuItem.Text = "Ver Hoteles";
            this.verHotelesToolStripMenuItem.Click += new System.EventHandler(this.verHotelesToolStripMenuItem_Click);
            // 
            // habitacionesToolStripMenuItem
            // 
            this.habitacionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.habitacionesToolStripMenuItem1,
            this.tiposDeHabitacionesToolStripMenuItem});
            this.habitacionesToolStripMenuItem.Name = "habitacionesToolStripMenuItem";
            this.habitacionesToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.habitacionesToolStripMenuItem.Text = "Habitaciones";
            // 
            // habitacionesToolStripMenuItem1
            // 
            this.habitacionesToolStripMenuItem1.Name = "habitacionesToolStripMenuItem1";
            this.habitacionesToolStripMenuItem1.Size = new System.Drawing.Size(240, 26);
            this.habitacionesToolStripMenuItem1.Text = "Ver habitaciones";
            this.habitacionesToolStripMenuItem1.Click += new System.EventHandler(this.habitacionesToolStripMenuItem1_Click);
            // 
            // tiposDeHabitacionesToolStripMenuItem
            // 
            this.tiposDeHabitacionesToolStripMenuItem.Name = "tiposDeHabitacionesToolStripMenuItem";
            this.tiposDeHabitacionesToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.tiposDeHabitacionesToolStripMenuItem.Text = "Tipos de Habitaciones";
            this.tiposDeHabitacionesToolStripMenuItem.Click += new System.EventHandler(this.tiposDeHabitacionesToolStripMenuItem_Click);
            // 
            // verServiciosToolStripMenuItem
            // 
            this.verServiciosToolStripMenuItem.Name = "verServiciosToolStripMenuItem";
            this.verServiciosToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.verServiciosToolStripMenuItem.Text = "Ver Servicios";
            this.verServiciosToolStripMenuItem.Click += new System.EventHandler(this.verServiciosToolStripMenuItem_Click);
            // 
            // clientesToolStripMenuItem1
            // 
            this.clientesToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verClientesToolStripMenuItem,
            this.verHistorialDeClientesToolStripMenuItem});
            this.clientesToolStripMenuItem1.Name = "clientesToolStripMenuItem1";
            this.clientesToolStripMenuItem1.Size = new System.Drawing.Size(75, 24);
            this.clientesToolStripMenuItem1.Text = "Clientes";
            // 
            // verClientesToolStripMenuItem
            // 
            this.verClientesToolStripMenuItem.Name = "verClientesToolStripMenuItem";
            this.verClientesToolStripMenuItem.Size = new System.Drawing.Size(250, 26);
            this.verClientesToolStripMenuItem.Text = "Ver Clientes";
            this.verClientesToolStripMenuItem.Click += new System.EventHandler(this.verClientesToolStripMenuItem_Click);
            // 
            // verHistorialDeClientesToolStripMenuItem
            // 
            this.verHistorialDeClientesToolStripMenuItem.Name = "verHistorialDeClientesToolStripMenuItem";
            this.verHistorialDeClientesToolStripMenuItem.Size = new System.Drawing.Size(250, 26);
            this.verHistorialDeClientesToolStripMenuItem.Text = "Ver Historial de Clientes";
            this.verHistorialDeClientesToolStripMenuItem.Click += new System.EventHandler(this.verHistorialDeClientesToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reporteDeOcupaciónPorHotelToolStripMenuItem,
            this.reporteDeVentasToolStripMenuItem});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // reporteDeOcupaciónPorHotelToolStripMenuItem
            // 
            this.reporteDeOcupaciónPorHotelToolStripMenuItem.Name = "reporteDeOcupaciónPorHotelToolStripMenuItem";
            this.reporteDeOcupaciónPorHotelToolStripMenuItem.Size = new System.Drawing.Size(306, 26);
            this.reporteDeOcupaciónPorHotelToolStripMenuItem.Text = "Reporte de Ocupación por hotel";
            this.reporteDeOcupaciónPorHotelToolStripMenuItem.Click += new System.EventHandler(this.reporteDeOcupaciónPorHotelToolStripMenuItem_Click);
            // 
            // reporteDeVentasToolStripMenuItem
            // 
            this.reporteDeVentasToolStripMenuItem.Name = "reporteDeVentasToolStripMenuItem";
            this.reporteDeVentasToolStripMenuItem.Size = new System.Drawing.Size(306, 26);
            this.reporteDeVentasToolStripMenuItem.Text = "Reporte de Ventas";
            this.reporteDeVentasToolStripMenuItem.Click += new System.EventHandler(this.reporteDeVentasToolStripMenuItem_Click);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.cerrarToolStripMenuItem.Text = "Cerrar";
            this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
            // 
            // Pantalla_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(1709, 770);
            this.ControlBox = false;
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Pantalla_principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pantalla_principal";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Pantalla_principal_FormClosed);
            this.Load += new System.EventHandler(this.Pantalla_principal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem usuarioToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem reservacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hotelesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verHotelesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem habitacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem habitacionesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tiposDeHabitacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem verClientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verHistorialDeClientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeOcupaciónPorHotelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeVentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verServiciosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miPerfilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verEditarRegistrarUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verReservacionesToolStripMenuItem;
    }
}