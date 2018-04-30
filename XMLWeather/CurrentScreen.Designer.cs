namespace XMLWeather
{
    partial class CurrentScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.locationBox = new System.Windows.Forms.PictureBox();
            this.windSpeedBox = new System.Windows.Forms.PictureBox();
            this.humidityBox = new System.Windows.Forms.PictureBox();
            this.sunsetBox = new System.Windows.Forms.PictureBox();
            this.sunriseBox = new System.Windows.Forms.PictureBox();
            this.weatherImageBox = new System.Windows.Forms.PictureBox();
            this.cityBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.locationBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.windSpeedBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.humidityBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sunsetBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sunriseBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weatherImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // locationBox
            // 
            this.locationBox.BackColor = System.Drawing.Color.Transparent;
            this.locationBox.Image = global::XMLWeather.Properties.Resources.locationIcon;
            this.locationBox.Location = new System.Drawing.Point(409, 150);
            this.locationBox.Name = "locationBox";
            this.locationBox.Size = new System.Drawing.Size(50, 50);
            this.locationBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.locationBox.TabIndex = 50;
            this.locationBox.TabStop = false;
            this.locationBox.Click += new System.EventHandler(this.locationBox_Click);
            // 
            // windSpeedBox
            // 
            this.windSpeedBox.Image = global::XMLWeather.Properties.Resources.windSpeedIcon;
            this.windSpeedBox.Location = new System.Drawing.Point(466, 109);
            this.windSpeedBox.Name = "windSpeedBox";
            this.windSpeedBox.Size = new System.Drawing.Size(171, 171);
            this.windSpeedBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.windSpeedBox.TabIndex = 49;
            this.windSpeedBox.TabStop = false;
            // 
            // humidityBox
            // 
            this.humidityBox.Image = global::XMLWeather.Properties.Resources.humidityIcon;
            this.humidityBox.Location = new System.Drawing.Point(465, 286);
            this.humidityBox.Name = "humidityBox";
            this.humidityBox.Size = new System.Drawing.Size(171, 171);
            this.humidityBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.humidityBox.TabIndex = 48;
            this.humidityBox.TabStop = false;
            // 
            // sunsetBox
            // 
            this.sunsetBox.Image = global::XMLWeather.Properties.Resources.sunsetIcon;
            this.sunsetBox.Location = new System.Drawing.Point(466, 639);
            this.sunsetBox.Name = "sunsetBox";
            this.sunsetBox.Size = new System.Drawing.Size(171, 171);
            this.sunsetBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sunsetBox.TabIndex = 47;
            this.sunsetBox.TabStop = false;
            // 
            // sunriseBox
            // 
            this.sunriseBox.Image = global::XMLWeather.Properties.Resources.sunriseIcon;
            this.sunriseBox.Location = new System.Drawing.Point(466, 463);
            this.sunriseBox.Name = "sunriseBox";
            this.sunriseBox.Size = new System.Drawing.Size(170, 170);
            this.sunriseBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sunriseBox.TabIndex = 46;
            this.sunriseBox.TabStop = false;
            // 
            // weatherImageBox
            // 
            this.weatherImageBox.BackColor = System.Drawing.Color.Transparent;
            this.weatherImageBox.Image = global::XMLWeather.Properties.Resources.RainDrops;
            this.weatherImageBox.Location = new System.Drawing.Point(29, 206);
            this.weatherImageBox.Name = "weatherImageBox";
            this.weatherImageBox.Size = new System.Drawing.Size(431, 382);
            this.weatherImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.weatherImageBox.TabIndex = 45;
            this.weatherImageBox.TabStop = false;
            this.weatherImageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.weatherImageBox_Paint);
            // 
            // cityBox
            // 
            this.cityBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cityBox.Location = new System.Drawing.Point(281, 109);
            this.cityBox.Name = "cityBox";
            this.cityBox.Size = new System.Drawing.Size(179, 35);
            this.cityBox.TabIndex = 51;
            this.cityBox.Text = "Stratford,CA";
            this.cityBox.Visible = false;
            this.cityBox.Click += new System.EventHandler(this.cityBox_Click);
            this.cityBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.cityBox_PreviewKeyDown);
            // 
            // CurrentScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(232)))), ((int)(((byte)(233)))));
            this.Controls.Add(this.cityBox);
            this.Controls.Add(this.locationBox);
            this.Controls.Add(this.windSpeedBox);
            this.Controls.Add(this.humidityBox);
            this.Controls.Add(this.sunsetBox);
            this.Controls.Add(this.sunriseBox);
            this.Controls.Add(this.weatherImageBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CurrentScreen";
            this.Size = new System.Drawing.Size(650, 865);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CurrentScreen_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CurrentScreen_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.locationBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.windSpeedBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.humidityBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sunsetBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sunriseBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weatherImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox weatherImageBox;
        private System.Windows.Forms.PictureBox sunriseBox;
        private System.Windows.Forms.PictureBox sunsetBox;
        private System.Windows.Forms.PictureBox humidityBox;
        private System.Windows.Forms.PictureBox windSpeedBox;
        private System.Windows.Forms.PictureBox locationBox;
        private System.Windows.Forms.TextBox cityBox;
    }
}
