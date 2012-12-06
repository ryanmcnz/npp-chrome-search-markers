namespace NppPluginNET
{
    partial class frmMarkerPanel
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
            this.tbxSearchString = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbxSearchString
            // 
            this.tbxSearchString.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbxSearchString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSearchString.Location = new System.Drawing.Point(0, 0);
            this.tbxSearchString.Name = "tbxSearchString";
            this.tbxSearchString.Size = new System.Drawing.Size(139, 20);
            this.tbxSearchString.TabIndex = 0;
            this.tbxSearchString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxSearchString_KeyDown);
            this.tbxSearchString.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbxSearchString_KeyUp);
            // 
            // frmMarkerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(139, 140);
            this.Controls.Add(this.tbxSearchString);
            this.DoubleBuffered = true;
            this.Name = "frmMarkerPanel";
            this.Text = "NppDockableForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmGoToLine_Paint);
            this.SizeChanged += new System.EventHandler(this.frmGoToLine_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.FrmGoToLineVisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxSearchString;





    }
}