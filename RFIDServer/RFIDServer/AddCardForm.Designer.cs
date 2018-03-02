namespace RFIDServer
{
    partial class AddCardForm
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
            this.label_serial_number = new System.Windows.Forms.Label();
            this.label_owner = new System.Windows.Forms.Label();
            this.textBox_serial_number = new System.Windows.Forms.TextBox();
            this.textBox_owner = new System.Windows.Forms.TextBox();
            this.label_access_status = new System.Windows.Forms.Label();
            this.comboBox_access_status = new System.Windows.Forms.ComboBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_serial_number
            // 
            this.label_serial_number.AutoSize = true;
            this.label_serial_number.Location = new System.Drawing.Point(12, 12);
            this.label_serial_number.Name = "label_serial_number";
            this.label_serial_number.Size = new System.Drawing.Size(127, 13);
            this.label_serial_number.TabIndex = 0;
            this.label_serial_number.Text = "Серийный номер карты";
            // 
            // label_owner
            // 
            this.label_owner.AutoSize = true;
            this.label_owner.Location = new System.Drawing.Point(12, 38);
            this.label_owner.Name = "label_owner";
            this.label_owner.Size = new System.Drawing.Size(56, 13);
            this.label_owner.TabIndex = 1;
            this.label_owner.Text = "Владелец";
            // 
            // textBox_serial_number
            // 
            this.textBox_serial_number.Location = new System.Drawing.Point(145, 9);
            this.textBox_serial_number.Name = "textBox_serial_number";
            this.textBox_serial_number.Size = new System.Drawing.Size(214, 20);
            this.textBox_serial_number.TabIndex = 2;
            // 
            // textBox_owner
            // 
            this.textBox_owner.Location = new System.Drawing.Point(145, 35);
            this.textBox_owner.Multiline = true;
            this.textBox_owner.Name = "textBox_owner";
            this.textBox_owner.Size = new System.Drawing.Size(214, 50);
            this.textBox_owner.TabIndex = 3;
            // 
            // label_access_status
            // 
            this.label_access_status.AutoSize = true;
            this.label_access_status.Location = new System.Drawing.Point(12, 94);
            this.label_access_status.Name = "label_access_status";
            this.label_access_status.Size = new System.Drawing.Size(84, 13);
            this.label_access_status.TabIndex = 4;
            this.label_access_status.Text = "Статус доступа";
            // 
            // comboBox_access_status
            // 
            this.comboBox_access_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_access_status.FormattingEnabled = true;
            this.comboBox_access_status.Items.AddRange(new object[] {
            "Запрещён",
            "Разрешён"});
            this.comboBox_access_status.Location = new System.Drawing.Point(145, 91);
            this.comboBox_access_status.Name = "comboBox_access_status";
            this.comboBox_access_status.Size = new System.Drawing.Size(214, 21);
            this.comboBox_access_status.TabIndex = 5;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(203, 134);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 6;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(284, 134);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 7;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // AddCardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 167);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.comboBox_access_status);
            this.Controls.Add(this.label_access_status);
            this.Controls.Add(this.textBox_owner);
            this.Controls.Add(this.textBox_serial_number);
            this.Controls.Add(this.label_owner);
            this.Controls.Add(this.label_serial_number);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление карты";
            this.Load += new System.EventHandler(this.AddCardForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_serial_number;
        private System.Windows.Forms.Label label_owner;
        private System.Windows.Forms.TextBox textBox_serial_number;
        private System.Windows.Forms.TextBox textBox_owner;
        private System.Windows.Forms.Label label_access_status;
        private System.Windows.Forms.ComboBox comboBox_access_status;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
    }
}