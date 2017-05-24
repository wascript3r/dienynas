namespace Baigiamasis_d
{
    partial class Prisijungimas
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
            this.monoFlat_ThemeContainer1 = new MonoFlat.MonoFlat_ThemeContainer();
            this.monoFlat_LinkLabel1 = new MonoFlat.MonoFlat_LinkLabel();
            this.registracija = new MonoFlat.MonoFlat_Button();
            this.slaptazodis = new MonoFlat.MonoFlat_TextBox();
            this.username = new MonoFlat.MonoFlat_TextBox();
            this.prisijungti = new MonoFlat.MonoFlat_Button();
            this.monoFlat_ControlBox1 = new MonoFlat.MonoFlat_ControlBox();
            this.monoFlat_ThemeContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // monoFlat_ThemeContainer1
            // 
            this.monoFlat_ThemeContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(41)))), ((int)(((byte)(50)))));
            this.monoFlat_ThemeContainer1.Controls.Add(this.monoFlat_LinkLabel1);
            this.monoFlat_ThemeContainer1.Controls.Add(this.registracija);
            this.monoFlat_ThemeContainer1.Controls.Add(this.slaptazodis);
            this.monoFlat_ThemeContainer1.Controls.Add(this.username);
            this.monoFlat_ThemeContainer1.Controls.Add(this.prisijungti);
            this.monoFlat_ThemeContainer1.Controls.Add(this.monoFlat_ControlBox1);
            this.monoFlat_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monoFlat_ThemeContainer1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.monoFlat_ThemeContainer1.Location = new System.Drawing.Point(0, 0);
            this.monoFlat_ThemeContainer1.Name = "monoFlat_ThemeContainer1";
            this.monoFlat_ThemeContainer1.Padding = new System.Windows.Forms.Padding(10, 70, 10, 9);
            this.monoFlat_ThemeContainer1.RoundCorners = true;
            this.monoFlat_ThemeContainer1.Sizable = false;
            this.monoFlat_ThemeContainer1.Size = new System.Drawing.Size(391, 310);
            this.monoFlat_ThemeContainer1.SmartBounds = true;
            this.monoFlat_ThemeContainer1.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
            this.monoFlat_ThemeContainer1.TabIndex = 0;
            this.monoFlat_ThemeContainer1.Text = "Prisijungimas";
            // 
            // monoFlat_LinkLabel1
            // 
            this.monoFlat_LinkLabel1.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.monoFlat_LinkLabel1.AutoSize = true;
            this.monoFlat_LinkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.monoFlat_LinkLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.monoFlat_LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.monoFlat_LinkLabel1.LinkColor = System.Drawing.Color.White;
            this.monoFlat_LinkLabel1.Location = new System.Drawing.Point(30, 74);
            this.monoFlat_LinkLabel1.Name = "monoFlat_LinkLabel1";
            this.monoFlat_LinkLabel1.Size = new System.Drawing.Size(93, 20);
            this.monoFlat_LinkLabel1.TabIndex = 5;
            this.monoFlat_LinkLabel1.TabStop = true;
            this.monoFlat_LinkLabel1.Text = "<-- Į pradžią";
            this.monoFlat_LinkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(41)))), ((int)(((byte)(42)))));
            this.monoFlat_LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.monoFlat_LinkLabel1_LinkClicked);
            // 
            // registracija
            // 
            this.registracija.BackColor = System.Drawing.Color.Transparent;
            this.registracija.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.registracija.Image = null;
            this.registracija.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.registracija.Location = new System.Drawing.Point(34, 241);
            this.registracija.Name = "registracija";
            this.registracija.Size = new System.Drawing.Size(136, 41);
            this.registracija.TabIndex = 4;
            this.registracija.Text = "Registracija";
            this.registracija.TextAlignment = System.Drawing.StringAlignment.Center;
            this.registracija.Click += new System.EventHandler(this.registracija_Click);
            // 
            // slaptazodis
            // 
            this.slaptazodis.BackColor = System.Drawing.Color.Transparent;
            this.slaptazodis.Font = new System.Drawing.Font("Tahoma", 11F);
            this.slaptazodis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(183)))), ((int)(((byte)(191)))));
            this.slaptazodis.Image = null;
            this.slaptazodis.Location = new System.Drawing.Point(34, 175);
            this.slaptazodis.MaxLength = 50;
            this.slaptazodis.Multiline = false;
            this.slaptazodis.Name = "slaptazodis";
            this.slaptazodis.ReadOnly = false;
            this.slaptazodis.Size = new System.Drawing.Size(322, 45);
            this.slaptazodis.TabIndex = 2;
            this.slaptazodis.Text = "Slaptažodis";
            this.slaptazodis.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.slaptazodis.UseSystemPasswordChar = false;
            this.slaptazodis.Enter += new System.EventHandler(this.slaptazodis_Enter);
            this.slaptazodis.Leave += new System.EventHandler(this.slaptazodis_Leave);
            // 
            // username
            // 
            this.username.BackColor = System.Drawing.Color.Transparent;
            this.username.Font = new System.Drawing.Font("Tahoma", 11F);
            this.username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(183)))), ((int)(((byte)(191)))));
            this.username.Image = null;
            this.username.Location = new System.Drawing.Point(34, 109);
            this.username.MaxLength = 50;
            this.username.Multiline = false;
            this.username.Name = "username";
            this.username.ReadOnly = false;
            this.username.Size = new System.Drawing.Size(322, 45);
            this.username.TabIndex = 1;
            this.username.Text = "Vartotojo vardas";
            this.username.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.username.UseSystemPasswordChar = false;
            this.username.Enter += new System.EventHandler(this.username_Enter);
            this.username.Leave += new System.EventHandler(this.username_Leave);
            // 
            // prisijungti
            // 
            this.prisijungti.BackColor = System.Drawing.Color.Transparent;
            this.prisijungti.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.prisijungti.Image = null;
            this.prisijungti.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.prisijungti.Location = new System.Drawing.Point(220, 241);
            this.prisijungti.Name = "prisijungti";
            this.prisijungti.Size = new System.Drawing.Size(136, 41);
            this.prisijungti.TabIndex = 3;
            this.prisijungti.Text = "Prisijungti";
            this.prisijungti.TextAlignment = System.Drawing.StringAlignment.Center;
            this.prisijungti.Click += new System.EventHandler(this.prisijungti_Click);
            // 
            // monoFlat_ControlBox1
            // 
            this.monoFlat_ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.monoFlat_ControlBox1.EnableHoverHighlight = false;
            this.monoFlat_ControlBox1.EnableMaximizeButton = true;
            this.monoFlat_ControlBox1.EnableMinimizeButton = true;
            this.monoFlat_ControlBox1.Location = new System.Drawing.Point(279, 15);
            this.monoFlat_ControlBox1.Name = "monoFlat_ControlBox1";
            this.monoFlat_ControlBox1.Size = new System.Drawing.Size(100, 25);
            this.monoFlat_ControlBox1.TabIndex = 0;
            this.monoFlat_ControlBox1.Text = "monoFlat_ControlBox1";
            // 
            // Prisijungimas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(391, 310);
            this.Controls.Add(this.monoFlat_ThemeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Prisijungimas";
            this.Text = "Prisijungimas";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Prisijungimas_Load);
            this.monoFlat_ThemeContainer1.ResumeLayout(false);
            this.monoFlat_ThemeContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MonoFlat.MonoFlat_ThemeContainer monoFlat_ThemeContainer1;
        private MonoFlat.MonoFlat_ControlBox monoFlat_ControlBox1;
        private MonoFlat.MonoFlat_TextBox slaptazodis;
        private MonoFlat.MonoFlat_TextBox username;
        private MonoFlat.MonoFlat_Button prisijungti;
        private MonoFlat.MonoFlat_Button registracija;
        private MonoFlat.MonoFlat_LinkLabel monoFlat_LinkLabel1;
    }
}