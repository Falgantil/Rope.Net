namespace Rope.Net.WinForms.Sample
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
            this.sldValue = new System.Windows.Forms.TrackBar();
            this.sldMultiplier = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.sldRed = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.sldGreen = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.sldBlue = new System.Windows.Forms.TrackBar();
            this.btnSubmit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sldValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldMultiplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldBlue)).BeginInit();
            this.SuspendLayout();
            // 
            // sldValue
            // 
            this.sldValue.Location = new System.Drawing.Point(12, 32);
            this.sldValue.Maximum = 1000;
            this.sldValue.Name = "sldValue";
            this.sldValue.Size = new System.Drawing.Size(180, 45);
            this.sldValue.TabIndex = 0;
            this.sldValue.Value = 50;
            // 
            // sldMultiplier
            // 
            this.sldMultiplier.Location = new System.Drawing.Point(192, 32);
            this.sldMultiplier.Name = "sldMultiplier";
            this.sldMultiplier.Size = new System.Drawing.Size(180, 45);
            this.sldMultiplier.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(81, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(249, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Multiplier";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(145, 80);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(85, 20);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "Result: 0.0";
            // 
            // sldRed
            // 
            this.sldRed.Location = new System.Drawing.Point(12, 187);
            this.sldRed.Maximum = 255;
            this.sldRed.Name = "sldRed";
            this.sldRed.Size = new System.Drawing.Size(360, 45);
            this.sldRed.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(173, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Red";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(165, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Green";
            // 
            // sldGreen
            // 
            this.sldGreen.Location = new System.Drawing.Point(12, 250);
            this.sldGreen.Maximum = 255;
            this.sldGreen.Name = "sldGreen";
            this.sldGreen.Size = new System.Drawing.Size(360, 45);
            this.sldGreen.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(172, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Blue";
            // 
            // sldBlue
            // 
            this.sldBlue.Location = new System.Drawing.Point(12, 318);
            this.sldBlue.Maximum = 255;
            this.sldBlue.Name = "sldBlue";
            this.sldBlue.Size = new System.Drawing.Size(360, 45);
            this.sldBlue.TabIndex = 9;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(12, 109);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(360, 38);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 379);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sldBlue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sldGreen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sldRed);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sldMultiplier);
            this.Controls.Add(this.sldValue);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.sldValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldMultiplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldBlue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar sldValue;
        private System.Windows.Forms.TrackBar sldMultiplier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TrackBar sldRed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar sldGreen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar sldBlue;
        private System.Windows.Forms.Button btnSubmit;
    }
}

