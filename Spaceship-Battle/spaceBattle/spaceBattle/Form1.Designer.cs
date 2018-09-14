namespace spaceBattle
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.spaceship = new System.Windows.Forms.PictureBox();
            this.gameloop = new System.Windows.Forms.Timer(this.components);
            this.reload = new System.Windows.Forms.Timer(this.components);
            this.lblScore = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spaceship)).BeginInit();
            this.SuspendLayout();
            // 
            // spaceship
            // 
            this.spaceship.Image = global::spaceBattle.Properties.Resources.shipt0;
            this.spaceship.Location = new System.Drawing.Point(368, 440);
            this.spaceship.Margin = new System.Windows.Forms.Padding(0);
            this.spaceship.Name = "spaceship";
            this.spaceship.Size = new System.Drawing.Size(62, 128);
            this.spaceship.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.spaceship.TabIndex = 0;
            this.spaceship.TabStop = false;
            // 
            // gameloop
            // 
            this.gameloop.Interval = 10;
            this.gameloop.Tick += new System.EventHandler(this.gameloop_Tick);
            // 
            // reload
            // 
            this.reload.Interval = 10;
            this.reload.Tick += new System.EventHandler(this.reload_Tick);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblScore.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblScore.Location = new System.Drawing.Point(5, 5);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(66, 16);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "Score:  0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(23)))), ((int)(((byte)(59)))));
            this.ClientSize = new System.Drawing.Size(794, 573);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.spaceship);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Space Battle";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.spaceship)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox spaceship;
        private System.Windows.Forms.Timer gameloop;
        private System.Windows.Forms.Timer reload;
        private System.Windows.Forms.Label lblScore;
    }
}

