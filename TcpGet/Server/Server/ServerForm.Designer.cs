namespace TcpGet
{
    partial class ServerForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.PortLabel = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.TextBox();
            this.Connect = new System.Windows.Forms.Button();
            this.LogText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(12, 9);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(53, 17);
            this.PortLabel.TabIndex = 0;
            this.PortLabel.Text = "Порт : ";
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(72, 9);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(100, 22);
            this.Port.TabIndex = 1;
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(334, 8);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(138, 23);
            this.Connect.TabIndex = 2;
            this.Connect.Text = "Подключить";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // LogText
            // 
            this.LogText.Location = new System.Drawing.Point(15, 46);
            this.LogText.Name = "LogText";
            this.LogText.ReadOnly = true;
            this.LogText.Size = new System.Drawing.Size(468, 367);
            this.LogText.TabIndex = 3;
            this.LogText.Text = "";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 450);
            this.Controls.Add(this.LogText);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.PortLabel);
            this.Name = "Server";
            this.Text = "Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Server_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.RichTextBox LogText;
    }
}

