
namespace FormLocation
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenMyKingdom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenMyKingdom
            // 
            this.btnOpenMyKingdom.Location = new System.Drawing.Point(174, 25);
            this.btnOpenMyKingdom.Name = "btnOpenMyKingdom";
            this.btnOpenMyKingdom.Size = new System.Drawing.Size(113, 23);
            this.btnOpenMyKingdom.TabIndex = 0;
            this.btnOpenMyKingdom.Text = "Open My Kingdom";
            this.btnOpenMyKingdom.UseVisualStyleBackColor = true;
            this.btnOpenMyKingdom.Click += new System.EventHandler(this.btnOpenMyKingdom_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 484);
            this.Controls.Add(this.btnOpenMyKingdom);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenMyKingdom;
    }
}

