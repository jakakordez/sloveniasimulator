﻿namespace Map_editor
{
    partial class ReferencePoint
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
            this.SuspendLayout();
            // 
            // ReferencePoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.Name = "ReferencePoint";
            this.Size = new System.Drawing.Size(10, 10);
            this.Load += new System.EventHandler(this.ReferencePoint_Load);
            this.Enter += new System.EventHandler(this.ReferencePoint_Enter);
            this.Leave += new System.EventHandler(this.ReferencePoint_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ReferencePoint_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ReferencePoint_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
