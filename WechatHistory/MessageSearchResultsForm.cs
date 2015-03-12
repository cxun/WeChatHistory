using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WechatHistory
{
    public partial class MessageSearchResultsForm : Form
    {
        public MainForm m_fmMain;

        public MessageSearchResultsForm(MainForm fmMain)
        {
            InitializeComponent();
            m_fmMain = fmMain;
        }

        private void MessageSearchResultsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void MessageSearchResultsForm_Shown(object sender, EventArgs e)
        {
            listView.TileSize = new Size(listView.Width - 25, listView.TileSize.Height);
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                //this.Opacity = 0.3;
                KeyValuePair<FriendInfo, KeyValuePair<int, string>> kvp =
                    (KeyValuePair<FriendInfo, KeyValuePair<int, string>>)e.Item.Tag;
                FriendInfo node = kvp.Key;
                m_fmMain.SetHistory(node, kvp.Value.Key);
            }
        }

        private void MessageSearchResultsForm_Resize(object sender, EventArgs e)
        {
            Size size = listView.TileSize;
            size.Width = this.Width - 10;
            listView.TileSize = size;
        }

        private void MessageSearchResultsForm_Activated(object sender, EventArgs e)
        {
            //this.Opacity = 1;
        }

        private void MessageSearchResultsForm_Deactivate(object sender, EventArgs e)
        {
            //this.Opacity = 0.3;
        }
    }
}
