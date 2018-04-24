namespace XMLWeather
{
    partial class ForecastScreen
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
            this.day2WeatherBox = new System.Windows.Forms.PictureBox();
            this.day3WeatherBox = new System.Windows.Forms.PictureBox();
            this.day4WeatherBox = new System.Windows.Forms.PictureBox();
            this.day1WeatherBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.day2WeatherBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.day3WeatherBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.day4WeatherBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.day1WeatherBox)).BeginInit();
            this.SuspendLayout();
            // 
            // day2WeatherBox
            // 
            this.day2WeatherBox.BackColor = System.Drawing.Color.Transparent;
            this.day2WeatherBox.Image = global::XMLWeather.Properties.Resources.windSpeedIcon;
            this.day2WeatherBox.Location = new System.Drawing.Point(497, 280);
            this.day2WeatherBox.Name = "day2WeatherBox";
            this.day2WeatherBox.Size = new System.Drawing.Size(110, 110);
            this.day2WeatherBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.day2WeatherBox.TabIndex = 53;
            this.day2WeatherBox.TabStop = false;
            // 
            // day3WeatherBox
            // 
            this.day3WeatherBox.BackColor = System.Drawing.Color.Transparent;
            this.day3WeatherBox.Image = global::XMLWeather.Properties.Resources.partlyCloudyIcon;
            this.day3WeatherBox.Location = new System.Drawing.Point(497, 144);
            this.day3WeatherBox.Name = "day3WeatherBox";
            this.day3WeatherBox.Size = new System.Drawing.Size(110, 110);
            this.day3WeatherBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.day3WeatherBox.TabIndex = 52;
            this.day3WeatherBox.TabStop = false;
            // 
            // day4WeatherBox
            // 
            this.day4WeatherBox.BackColor = System.Drawing.Color.Transparent;
            this.day4WeatherBox.Image = global::XMLWeather.Properties.Resources.sunsetIcon;
            this.day4WeatherBox.Location = new System.Drawing.Point(497, 416);
            this.day4WeatherBox.Name = "day4WeatherBox";
            this.day4WeatherBox.Size = new System.Drawing.Size(110, 110);
            this.day4WeatherBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.day4WeatherBox.TabIndex = 51;
            this.day4WeatherBox.TabStop = false;
            // 
            // day1WeatherBox
            // 
            this.day1WeatherBox.BackColor = System.Drawing.Color.Transparent;
            this.day1WeatherBox.Image = global::XMLWeather.Properties.Resources.sunriseIcon;
            this.day1WeatherBox.Location = new System.Drawing.Point(497, 552);
            this.day1WeatherBox.Name = "day1WeatherBox";
            this.day1WeatherBox.Size = new System.Drawing.Size(110, 110);
            this.day1WeatherBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.day1WeatherBox.TabIndex = 50;
            this.day1WeatherBox.TabStop = false;
            // 
            // ForecastScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(232)))), ((int)(((byte)(233)))));
            this.Controls.Add(this.day2WeatherBox);
            this.Controls.Add(this.day3WeatherBox);
            this.Controls.Add(this.day4WeatherBox);
            this.Controls.Add(this.day1WeatherBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ForecastScreen";
            this.Size = new System.Drawing.Size(650, 865);
            this.Load += new System.EventHandler(this.ForecastScreen_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ForecastScreen_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ForecastScreen_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.day2WeatherBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.day3WeatherBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.day4WeatherBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.day1WeatherBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox day2WeatherBox;
        private System.Windows.Forms.PictureBox day3WeatherBox;
        private System.Windows.Forms.PictureBox day4WeatherBox;
        private System.Windows.Forms.PictureBox day1WeatherBox;
    }
}
