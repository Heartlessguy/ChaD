namespace Chad.Client.WinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ServerAddressText = new System.Windows.Forms.TextBox();
            this.ServerPortText = new System.Windows.Forms.TextBox();
            this.ServerUsername = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.StatusBar = new System.Windows.Forms.TextBox();
            this.UserList = new System.Windows.Forms.ListBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ChatBox = new System.Windows.Forms.RichTextBox();
            this.MessageInput = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ServerAddressText
            // 
            this.ServerAddressText.Dock = System.Windows.Forms.DockStyle.Left;
            this.ServerAddressText.Location = new System.Drawing.Point(0, 0);
            this.ServerAddressText.Name = "ServerAddressText";
            this.ServerAddressText.PlaceholderText = "Address";
            this.ServerAddressText.Size = new System.Drawing.Size(116, 23);
            this.ServerAddressText.TabIndex = 0;
            // 
            // ServerPortText
            // 
            this.ServerPortText.Location = new System.Drawing.Point(122, 0);
            this.ServerPortText.Name = "ServerPortText";
            this.ServerPortText.PlaceholderText = "Port";
            this.ServerPortText.Size = new System.Drawing.Size(116, 23);
            this.ServerPortText.TabIndex = 0;
            // 
            // ServerUsername
            // 
            this.ServerUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerUsername.Location = new System.Drawing.Point(244, 0);
            this.ServerUsername.Name = "ServerUsername";
            this.ServerUsername.PlaceholderText = "Username";
            this.ServerUsername.Size = new System.Drawing.Size(274, 23);
            this.ServerUsername.TabIndex = 1;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ConnectButton.Location = new System.Drawing.Point(521, 1);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(0);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusBar.Enabled = false;
            this.StatusBar.Location = new System.Drawing.Point(0, 427);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(724, 23);
            this.StatusBar.TabIndex = 3;
            // 
            // UserList
            // 
            this.UserList.Dock = System.Windows.Forms.DockStyle.Right;
            this.UserList.FormattingEnabled = true;
            this.UserList.ItemHeight = 15;
            this.UserList.Location = new System.Drawing.Point(604, 0);
            this.UserList.Margin = new System.Windows.Forms.Padding(0);
            this.UserList.Name = "UserList";
            this.UserList.Size = new System.Drawing.Size(120, 427);
            this.UserList.TabIndex = 4;
            this.UserList.SelectedIndexChanged += new System.EventHandler(this.OnUserSelect);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(599, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 427);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ServerAddressText);
            this.panel1.Controls.Add(this.ServerPortText);
            this.panel1.Controls.Add(this.ServerUsername);
            this.panel1.Controls.Add(this.ConnectButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(599, 24);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.MessageInput);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(599, 403);
            this.panel2.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ChatBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(599, 380);
            this.panel3.TabIndex = 2;
            // 
            // ChatBox
            // 
            this.ChatBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ChatBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChatBox.Location = new System.Drawing.Point(0, 0);
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.ReadOnly = true;
            this.ChatBox.Size = new System.Drawing.Size(599, 380);
            this.ChatBox.TabIndex = 0;
            this.ChatBox.Text = "";
            // 
            // MessageInput
            // 
            this.MessageInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MessageInput.Location = new System.Drawing.Point(0, 380);
            this.MessageInput.Name = "MessageInput";
            this.MessageInput.PlaceholderText = "Write message here. Use Enter to send.";
            this.MessageInput.Size = new System.Drawing.Size(599, 23);
            this.MessageInput.TabIndex = 1;
            this.MessageInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendMessage);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.UserList);
            this.Controls.Add(this.StatusBar);
            this.MinimumSize = new System.Drawing.Size(506, 489);
            this.Name = "MainForm";
            this.Text = "ChaD";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ServerAddressText;
        private System.Windows.Forms.TextBox ServerPortText;
        private System.Windows.Forms.TextBox ServerUsername;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox StatusBar;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.RichTextBox ChatBox;
        public System.Windows.Forms.ListBox UserList;
        private System.Windows.Forms.TextBox MessageInput;
        private System.Windows.Forms.Panel panel3;
    }
}

