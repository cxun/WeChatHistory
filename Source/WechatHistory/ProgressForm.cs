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
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置窗口不可被关闭
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                int CS_NOCLOSE = 0x200;
                CreateParams parameters = base.CreateParams;
                parameters.ClassStyle |= CS_NOCLOSE;

                return parameters;
            }
        }
    }
}
