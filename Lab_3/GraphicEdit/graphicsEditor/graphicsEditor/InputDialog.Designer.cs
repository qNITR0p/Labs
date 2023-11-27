
namespace graphicsEditor
{
    partial class InputDialog
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.txtbName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtbWidtht = new System.Windows.Forms.NumericUpDown();
            this.txtbHeight = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtbWidtht)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // txtbName
            // 
            this.txtbName.Location = new System.Drawing.Point(101, 12);
            this.txtbName.Name = "txtbName";
            this.txtbName.Size = new System.Drawing.Size(160, 20);
            this.txtbName.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(22, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(64, 13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Имя файла";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(41, 50);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(45, 13);
            this.lblWidth.TabIndex = 4;
            this.lblWidth.Text = "Высота";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(40, 86);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(46, 13);
            this.lblHeight.TabIndex = 5;
            this.lblHeight.Text = "Ширина";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(25, 124);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 25);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ок";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(161, 124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 25);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtbWidtht
            // 
            this.txtbWidtht.Location = new System.Drawing.Point(101, 48);
            this.txtbWidtht.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.txtbWidtht.Name = "txtbWidtht";
            this.txtbWidtht.Size = new System.Drawing.Size(90, 20);
            this.txtbWidtht.TabIndex = 8;
            // 
            // txtbHeight
            // 
            this.txtbHeight.Location = new System.Drawing.Point(101, 84);
            this.txtbHeight.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.txtbHeight.Name = "txtbHeight";
            this.txtbHeight.Size = new System.Drawing.Size(90, 20);
            this.txtbHeight.TabIndex = 9;
            // 
            // InputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 161);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtbHeight);
            this.Controls.Add(this.txtbWidtht);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtbName);
            this.Name = "InputDialog";
            this.Text = "Создание изображения";
            ((System.ComponentModel.ISupportInitialize)(this.txtbWidtht)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txtbName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown txtbWidtht;
        private System.Windows.Forms.NumericUpDown txtbHeight;
    }
}