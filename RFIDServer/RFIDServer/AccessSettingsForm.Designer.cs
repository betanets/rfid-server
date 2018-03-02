namespace RFIDServer
{
    partial class AccessSettingsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView_cards = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Owner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccessStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_add_card = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_cards)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_cards
            // 
            this.dataGridView_cards.AllowUserToAddRows = false;
            this.dataGridView_cards.AllowUserToDeleteRows = false;
            this.dataGridView_cards.AllowUserToResizeRows = false;
            this.dataGridView_cards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_cards.BackgroundColor = System.Drawing.Color.Snow;
            this.dataGridView_cards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_cards.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.CardSerial,
            this.Owner,
            this.AccessStatus});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.PaleGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_cards.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_cards.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView_cards.Location = new System.Drawing.Point(12, 12);
            this.dataGridView_cards.MultiSelect = false;
            this.dataGridView_cards.Name = "dataGridView_cards";
            this.dataGridView_cards.ReadOnly = true;
            this.dataGridView_cards.RowHeadersVisible = false;
            this.dataGridView_cards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_cards.Size = new System.Drawing.Size(860, 408);
            this.dataGridView_cards.TabIndex = 0;
            this.dataGridView_cards.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_cards_MouseClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 200;
            // 
            // CardSerial
            // 
            this.CardSerial.HeaderText = "Серийный номер карты";
            this.CardSerial.Name = "CardSerial";
            this.CardSerial.ReadOnly = true;
            this.CardSerial.Width = 200;
            // 
            // Owner
            // 
            this.Owner.HeaderText = "Владелец";
            this.Owner.Name = "Owner";
            this.Owner.ReadOnly = true;
            this.Owner.Width = 300;
            // 
            // AccessStatus
            // 
            this.AccessStatus.HeaderText = "Статус доступа";
            this.AccessStatus.Name = "AccessStatus";
            this.AccessStatus.ReadOnly = true;
            this.AccessStatus.Width = 130;
            // 
            // button_add_card
            // 
            this.button_add_card.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_add_card.Location = new System.Drawing.Point(744, 426);
            this.button_add_card.Name = "button_add_card";
            this.button_add_card.Size = new System.Drawing.Size(128, 23);
            this.button_add_card.TabIndex = 1;
            this.button_add_card.Text = "Добавить карту";
            this.button_add_card.UseVisualStyleBackColor = true;
            this.button_add_card.Click += new System.EventHandler(this.button_add_card_Click);
            // 
            // AccessSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.button_add_card);
            this.Controls.Add(this.dataGridView_cards);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "AccessSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройка доступа";
            this.Load += new System.EventHandler(this.AccessSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_cards)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_cards;
        private System.Windows.Forms.Button button_add_card;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardSerial;
        private System.Windows.Forms.DataGridViewTextBoxColumn Owner;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccessStatus;
    }
}