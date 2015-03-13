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
    public partial class Jump2PageForm : Form
    {
        public int m_nPage = 0;
        public int m_nMaxPage = 0;

        public Jump2PageForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_nPage = Convert.ToInt32(textBox.Text);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            bool bLegal = true;
            foreach (char c in textBox.Text)
            {
                if (c < '0' || c > '9')
                {
                    bLegal = false;
                    break;
                }
            }
            int nPage = 0;
            try
            {
                nPage = Convert.ToInt32(textBox.Text);
            }
            catch (Exception ex) { }
            if (nPage < 1 || nPage > m_nMaxPage)
                bLegal = false;
            if (bLegal)
                btnOK.Enabled = true;
            else
                btnOK.Enabled = false;
        }

        private void Jump2PageForm_Shown(object sender, EventArgs e)
        {
            btnOK.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnOK.Enabled == true)
                btnOK_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                btnCancel_Click(null, null);
        }
    }
}
