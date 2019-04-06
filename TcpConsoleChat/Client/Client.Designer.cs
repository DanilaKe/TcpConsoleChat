namespace Client
{
    partial class ClientForm
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
            this.IpLabel = new System.Windows.Forms.Label();
            this.PortLabel = new System.Windows.Forms.Label();
            this.IpAddress = new System.Windows.Forms.TextBox();
            this.Port = new System.Windows.Forms.TextBox();
            this.UserBox = new System.Windows.Forms.ListBox();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ChatBox = new System.Windows.Forms.RichTextBox();
            this.Message = new System.Windows.Forms.TextBox();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.SendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IpLabel
            // 
            this.IpLabel.AutoSize = true;
            this.IpLabel.Location = new System.Drawing.Point(12, 9);
            this.IpLabel.Name = "IpLabel";
            this.IpLabel.Size = new System.Drawing.Size(75, 17);
            this.IpLabel.TabIndex = 0;
            this.IpLabel.Text = "IP адрес : ";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(12, 50);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(53, 17);
            this.PortLabel.TabIndex = 1;
            this.PortLabel.Text = "Порт : ";
            // 
            // IpAddress
            // 
            this.IpAddress.Location = new System.Drawing.Point(93, 4);
            this.IpAddress.Name = "IpAddress";
            this.IpAddress.Size = new System.Drawing.Size(187, 22);
            this.IpAddress.TabIndex = 2;
            this.IpAddress.Text = "127.0.0.1";
            this.IpAddress.UseWaitCursor = true;
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(93, 45);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(187, 22);
            this.Port.TabIndex = 3;
            this.Port.Text = "8888";
            // 
            // UserBox
            // 
            this.UserBox.FormattingEnabled = true;
            this.UserBox.ItemHeight = 16;
            this.UserBox.Location = new System.Drawing.Point(610, 12);
            this.UserBox.Name = "UserBox";
            this.UserBox.Size = new System.Drawing.Size(165, 340);
            this.UserBox.TabIndex = 4;
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Location = new System.Drawing.Point(298, 9);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(51, 17);
            this.UserNameLabel.TabIndex = 5;
            this.UserNameLabel.Text = "Имя  : ";
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(355, 6);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(187, 22);
            this.UserName.TabIndex = 6;
            this.UserName.UseWaitCursor = true;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(355, 44);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(187, 23);
            this.ConnectButton.TabIndex = 7;
            this.ConnectButton.Text = "Подключится";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectClick);
            // 
            // ChatBox
            // 
            this.ChatBox.Enabled = false;
            this.ChatBox.Location = new System.Drawing.Point(15, 73);
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.ReadOnly = true;
            this.ChatBox.Size = new System.Drawing.Size(573, 282);
            this.ChatBox.TabIndex = 8;
            this.ChatBox.Text = "";
            // 
            // Message
            // 
            this.Message.Location = new System.Drawing.Point(114, 378);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(474, 22);
            this.Message.TabIndex = 9;
            this.Message.UseWaitCursor = true;
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(12, 381);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(96, 17);
            this.MessageLabel.TabIndex = 10;
            this.MessageLabel.Text = "Сообщение : ";
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(601, 378);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(187, 23);
            this.SendButton.TabIndex = 11;
            this.SendButton.Text = "Отправить";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendMessageClick);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 418);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.ChatBox);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.UserNameLabel);
            this.Controls.Add(this.UserBox);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.IpAddress);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.IpLabel);
            this.Name = "ClientForm";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IpLabel;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox IpAddress;
        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.ListBox UserBox;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.RichTextBox ChatBox;
        private System.Windows.Forms.TextBox Message;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Button SendButton;
    }
}