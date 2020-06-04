using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chad.Client.WinForms.Models;
using static ChaD.client.Client;

namespace Chad.Client.WinForms
{
    public partial class MainForm : Form
    {
        private ChaD.client.Client Client { get; set; }

        public delegate void DrawEvent(string text);

        public delegate void DrawUserListEvent();

        private bool isConnected = false;

        // статчиное событие, которое дёргается для отрисовки сообщений из Client.cs
        public static DrawEvent _appendText;
        public static DrawUserListEvent _DrawUserList;

        public MainForm()
        {
            InitializeComponent();
            _appendText = DrawText;
            _DrawUserList = DrawUserList;
        }


        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isConnected)
                {
                    Client = new ChaD.client.Client(ServerAddressText.Text, int.Parse(ServerPortText.Text), ServerUsername.Text);
                    ConnectionInputLock();
                    isConnected = true;
                }
                else
                {
                    Client.ServerConnection.Disconnect();
                    ConnectionInputUnlock();
                    isConnected = false;
                }
            }
            catch (Exception ex)
            {
                ConnectionInputUnlock();
                StatusBar.Text = ex.Message;
            }
        }


        private void ConnectionInputLock()
        {
            ServerAddressText.Enabled = false;
            ServerPortText.Enabled = false;
            ConnectButton.Text = @"Disconnect";
        }

        private void ConnectionInputUnlock()
        {
            ServerAddressText.Enabled = true;
            ServerPortText.Enabled = true;
            ConnectButton.Text = @"Connect";
        }
        private void DrawText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(_appendText, text);
                return;
            }
            ChatBox.Text += $@"{text}{Environment.NewLine}";
        }
        private void DrawUserList()
        {
            if (InvokeRequired)
            {
                Invoke(_DrawUserList);
            }

            UserList.Items.Clear();
            if (Interlocutors.Count != 0) UserList.Items
                .AddRange(Interlocutors.Select(x => x.Name).ToArray());
        }



        private void SendMessage(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (MessageInput.Text == "") return;
            Client.ServerConnection.SendTo(_recipientId, MessageInput.Text);
            Interlocutors
                .Find(x => x.Id == _recipientId)
                ?.MessageHistory
                .Add(new MessageRecord()
                {
                    IsIncoming = false,
                    Message = MessageInput.Text
                });
            _appendText(MessageInput.Text);
            MessageInput.Text = "";
            OnUserSelect(sender, e);
        }

        private int _recipientId;
        private void OnUserSelect(object sender, EventArgs e)
        {
            ChatBox.Clear();
            var recipient = Interlocutors
                .Find(x => x.Name == (string)UserList.SelectedItem);

            _recipientId = recipient?.Id ?? 0;
            if (recipient == null || recipient.MessageHistory == null) return;
            foreach (var record in recipient.MessageHistory)
            {
                DrawText($"{(record.IsIncoming ? recipient.Name : "Me")}: {record.Message}");
            }
        }

    }
}
