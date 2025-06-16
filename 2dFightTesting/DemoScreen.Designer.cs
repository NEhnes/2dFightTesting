namespace _2dFightTesting
{
    partial class DemoScreen
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
            this.returnButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // returnButton
            // 
            this.returnButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.returnButton.BackColor = System.Drawing.Color.Transparent;
            this.returnButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.returnButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.returnButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.returnButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.returnButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.returnButton.Font = new System.Drawing.Font("Palatino Linotype", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.returnButton.ForeColor = System.Drawing.Color.Transparent;
            this.returnButton.Location = new System.Drawing.Point(560, 398);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(137, 49);
            this.returnButton.TabIndex = 11;
            this.returnButton.Text = "Return";
            this.returnButton.UseVisualStyleBackColor = false;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // DemoScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.returnButton);
            this.Name = "DemoScreen";
            this.Size = new System.Drawing.Size(700, 450);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button returnButton;
    }
}
