namespace RememberTheScreenshot
{
    partial class SelectAreaForm
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
            this.panelDrag = new System.Windows.Forms.Panel();
            this.btnCapture = new System.Windows.Forms.Button();
            this.panelDrag.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDrag
            // 
            this.panelDrag.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDrag.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelDrag.Controls.Add(this.btnCapture);
            this.panelDrag.Location = new System.Drawing.Point(9, 9);
            this.panelDrag.Margin = new System.Windows.Forms.Padding(0);
            this.panelDrag.Name = "panelDrag";
            this.panelDrag.Size = new System.Drawing.Size(782, 432);
            this.panelDrag.TabIndex = 0;
            this.panelDrag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelDrag_MouseDown);
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(0, 3);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(106, 30);
            this.btnCapture.TabIndex = 0;
            this.btnCapture.Text = "Capture this";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // SelectAreaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelDrag);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectAreaForm";
            this.Opacity = 0.5D;
            this.Text = "SelectAreaForm";
            this.panelDrag.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDrag;
        private System.Windows.Forms.Button btnCapture;
    }
}