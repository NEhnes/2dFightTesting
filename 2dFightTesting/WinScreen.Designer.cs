namespace _2dFightTesting
{
    partial class WinScreen
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
            this.player2Result = new System.Windows.Forms.Label();
            this.player1Result = new System.Windows.Forms.Label();
            this.player2PictureBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.player1PictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.player2PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player1PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // player2Result
            // 
            this.player2Result.AutoSize = true;
            this.player2Result.Font = new System.Drawing.Font("Palatino Linotype", 24F);
            this.player2Result.Location = new System.Drawing.Point(615, -25);
            this.player2Result.Name = "player2Result";
            this.player2Result.Size = new System.Drawing.Size(138, 44);
            this.player2Result.TabIndex = 20;
            this.player2Result.Text = "Player2: ";
            // 
            // player1Result
            // 
            this.player1Result.AutoSize = true;
            this.player1Result.Font = new System.Drawing.Font("Palatino Linotype", 24F);
            this.player1Result.Location = new System.Drawing.Point(-53, -25);
            this.player1Result.Name = "player1Result";
            this.player1Result.Size = new System.Drawing.Size(138, 44);
            this.player1Result.TabIndex = 19;
            this.player1Result.Text = "Player1: ";
            // 
            // player2PictureBox
            // 
            this.player2PictureBox.Location = new System.Drawing.Point(557, 34);
            this.player2PictureBox.Name = "player2PictureBox";
            this.player2PictureBox.Size = new System.Drawing.Size(228, 211);
            this.player2PictureBox.TabIndex = 18;
            this.player2PictureBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Palatino Linotype", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(444, 392);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(194, 83);
            this.button1.TabIndex = 17;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Palatino Linotype", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(80, 392);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(194, 83);
            this.button2.TabIndex = 16;
            this.button2.Text = "Play";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // player1PictureBox
            // 
            this.player1PictureBox.Location = new System.Drawing.Point(-84, 34);
            this.player1PictureBox.Name = "player1PictureBox";
            this.player1PictureBox.Size = new System.Drawing.Size(228, 211);
            this.player1PictureBox.TabIndex = 15;
            this.player1PictureBox.TabStop = false;
            // 
            // WinScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.player2Result);
            this.Controls.Add(this.player1Result);
            this.Controls.Add(this.player2PictureBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.player1PictureBox);
            this.Name = "WinScreen";
            this.Size = new System.Drawing.Size(700, 450);
            ((System.ComponentModel.ISupportInitialize)(this.player2PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player1PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label player2Result;
        private System.Windows.Forms.Label player1Result;
        private System.Windows.Forms.PictureBox player2PictureBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox player1PictureBox;
    }
}
