namespace Client
{
    partial class ClientForm
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
            this.ConnectButton = new System.Windows.Forms.Button();
            this.Directory = new System.Windows.Forms.TextBox();
            this.DirectoryLabel = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.TextBox();
            this.IpAddress = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.IpLabel = new System.Windows.Forms.Label();
            this.FileText = new System.Windows.Forms.RichTextBox();
            this.Command = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(403, 52);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(187, 23);
            this.ConnectButton.TabIndex = 14;
            this.ConnectButton.Text = "Подключится";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectClick);
            // 
            // Directory
            // 
            this.Directory.Location = new System.Drawing.Point(403, 14);
            this.Directory.Name = "Directory";
            this.Directory.Size = new System.Drawing.Size(187, 22);
            this.Directory.TabIndex = 13;
            this.Directory.UseWaitCursor = true;
            // 
            // DirectoryLabel
            // 
            this.DirectoryLabel.AutoSize = true;
            this.DirectoryLabel.Location = new System.Drawing.Point(296, 17);
            this.DirectoryLabel.Name = "DirectoryLabel";
            this.DirectoryLabel.Size = new System.Drawing.Size(101, 17);
            this.DirectoryLabel.TabIndex = 12;
            this.DirectoryLabel.Text = "Директория : ";
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(91, 53);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(187, 22);
            this.Port.TabIndex = 11;
            this.Port.Text = "8888";
            // 
            // IpAddress
            // 
            this.IpAddress.Location = new System.Drawing.Point(91, 12);
            this.IpAddress.Name = "IpAddress";
            this.IpAddress.Size = new System.Drawing.Size(187, 22);
            this.IpAddress.TabIndex = 10;
            this.IpAddress.Text = "127.0.0.1";
            this.IpAddress.UseWaitCursor = true;
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(10, 58);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(53, 17);
            this.PortLabel.TabIndex = 9;
            this.PortLabel.Text = "Порт : ";
            // 
            // IpLabel
            // 
            this.IpLabel.AutoSize = true;
            this.IpLabel.Location = new System.Drawing.Point(10, 17);
            this.IpLabel.Name = "IpLabel";
            this.IpLabel.Size = new System.Drawing.Size(75, 17);
            this.IpLabel.TabIndex = 8;
            this.IpLabel.Text = "IP адрес : ";
            // 
            // FileText
            // 
            this.FileText.Location = new System.Drawing.Point(12, 81);
            this.FileText.Name = "FileText";
            this.FileText.ReadOnly = true;
            this.FileText.Size = new System.Drawing.Size(578, 291);
            this.FileText.TabIndex = 15;
            this.FileText.Text = "";
            // 
            // Command
            // 
            this.Command.Location = new System.Drawing.Point(13, 394);
            this.Command.Name = "Command";
            this.Command.Size = new System.Drawing.Size(422, 22);
            this.Command.TabIndex = 16;
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(441, 394);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(149, 23);
            this.SendButton.TabIndex = 17;
            this.SendButton.Text = "Отправить";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendMessageClick);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 450);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.Command);
            this.Controls.Add(this.FileText);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.Directory);
            this.Controls.Add(this.DirectoryLabel);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.IpAddress);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.IpLabel);
            this.MaximumSize = new System.Drawing.Size(633, 492);
            this.MinimumSize = new System.Drawing.Size(633, 492);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClientForm_Close);
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox Directory;
        private System.Windows.Forms.Label DirectoryLabel;
        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.TextBox IpAddress;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Label IpLabel;
        private System.Windows.Forms.RichTextBox FileText;
        private System.Windows.Forms.TextBox Command;
        private System.Windows.Forms.Button SendButton;
    }
}

