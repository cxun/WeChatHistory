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
    public partial class FriendSearchResultsForm : Form
    {
        public FriendSearchResultsForm()
        {
            InitializeComponent();
        }

        private void FriendSearchResultsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void FriendSearchResultsForm_Shown(object sender, EventArgs e)
        {
            listView.TileSize = new Size(listView.Width - 25, listView.TileSize.Height);
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                TreeNode node = (TreeNode)e.Item.Tag;
                WechatHistory.MainForm.SelectNode(node);
                this.Opacity = 0.3;
                this.TopMost = false;
            }
        }

        private void FriendSearchResultsForm_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void FriendSearchResultsForm_Deactivate(object sender, EventArgs e)
        {
            this.Opacity = 0.3;
        }
    }
}
