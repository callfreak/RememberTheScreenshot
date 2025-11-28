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
            this.LblAreaSize = new System.Windows.Forms.Label();
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
            this.panelDrag.BackColor = System.Drawing.Color.Transparent;
            this.panelDrag.Controls.Add(this.LblAreaSize);
            this.panelDrag.Controls.Add(this.btnCapture);
            this.panelDrag.Location = new System.Drawing.Point(7, 7);
            this.panelDrag.Margin = new System.Windows.Forms.Padding(0);
            this.panelDrag.Name = "panelDrag";
            this.panelDrag.Size = new System.Drawing.Size(586, 351);
            this.panelDrag.TabIndex = 0;
            this.panelDrag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelDrag_MouseDown);
            this.panelDrag.Resize += new System.EventHandler(this.panelDrag_Resize);
            // 
            // LblAreaSize
            // 
            this.LblAreaSize.AutoSize = true;
            this.LblAreaSize.BackColor = System.Drawing.Color.Gainsboro;
            this.LblAreaSize.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LblAreaSize.Location = new System.Drawing.Point(0, 338);
            this.LblAreaSize.Name = "LblAreaSize";
            this.LblAreaSize.Size = new System.Drawing.Size(35, 13);
            this.LblAreaSize.TabIndex = 1;
            this.LblAreaSize.Text = "label1";
            this.LblAreaSize.UseWaitCursor = true;
            // 
            // btnCapture
            // 
            this.btnCapture.Image = global::RememberTheScreenshot.Properties.Resources.screenshot;
            this.btnCapture.Location = new System.Drawing.Point(0, 2);
            this.btnCapture.Margin = new System.Windows.Forms.Padding(2);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(80, 65);
            this.btnCapture.TabIndex = 0;
            this.btnCapture.Text = "Capture this";
            this.btnCapture.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // SelectAreaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.panelDrag);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SelectAreaForm";
            this.Opacity = 0.35D;
            this.Text = "SelectAreaForm";
            this.panelDrag.ResumeLayout(false);
            this.panelDrag.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDrag;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Label LblAreaSize;
    }
}