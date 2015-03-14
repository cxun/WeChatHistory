namespace WechatHistory
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tvFriends = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tvGroup = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tvOthers = new System.Windows.Forms.TreeView();
            this.btnSearchFriend = new System.Windows.Forms.Button();
            this.tbSearchFriend = new System.Windows.Forms.TextBox();
            this.lbPageNumber = new System.Windows.Forms.Label();
            this.wbHistory = new EO.WebBrowser.WinForm.WebControl();
            this.wbView = new EO.WebBrowser.WebView();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.cbSearchArea = new System.Windows.Forms.ComboBox();
            this.btnSearchHistory = new System.Windows.Forms.Button();
            this.tbSearchHistory = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearchFriend);
            this.splitContainer1.Panel1.Controls.Add(this.tbSearchFriend);
            this.splitContainer1.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbPageNumber);
            this.splitContainer1.Panel2.Controls.Add(this.wbHistory);
            this.splitContainer1.Panel2.Controls.Add(this.btnLast);
            this.splitContainer1.Panel2.Controls.Add(this.btnFirst);
            this.splitContainer1.Panel2.Controls.Add(this.btnForward);
            this.splitContainer1.Panel2.Controls.Add(this.btnBackward);
            this.splitContainer1.Panel2.Controls.Add(this.cbSearchArea);
            this.splitContainer1.Panel2.Controls.Add(this.btnSearchHistory);
            this.splitContainer1.Panel2.Controls.Add(this.tbSearchHistory);
            this.splitContainer1.Panel2.Resize += new System.EventHandler(this.splitContainer1_Panel2_Resize);
            this.splitContainer1.Size = new System.Drawing.Size(969, 595);
            this.splitContainer1.SplitterDistance = 245;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 4;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(3, 32);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(206, 448);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tvFriends);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(198, 422);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "好友";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tvFriends
            // 
            this.tvFriends.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvFriends.FullRowSelect = true;
            this.tvFriends.HideSelection = false;
            this.tvFriends.Location = new System.Drawing.Point(6, 6);
            this.tvFriends.Name = "tvFriends";
            this.tvFriends.Size = new System.Drawing.Size(101, 216);
            this.tvFriends.TabIndex = 2;
            this.tvFriends.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvFriends_BeforeSelect);
            this.tvFriends.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFriends_AfterSelect);
            this.tvFriends.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvFriends_NodeMouseClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tvGroup);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(198, 422);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "群";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tvGroup
            // 
            this.tvGroup.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvGroup.FullRowSelect = true;
            this.tvGroup.HideSelection = false;
            this.tvGroup.Location = new System.Drawing.Point(49, 103);
            this.tvGroup.Name = "tvGroup";
            this.tvGroup.Size = new System.Drawing.Size(101, 216);
            this.tvGroup.TabIndex = 3;
            this.tvGroup.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvFriends_BeforeSelect);
            this.tvGroup.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFriends_AfterSelect);
            this.tvGroup.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvFriends_NodeMouseClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tvOthers);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(198, 422);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "其它";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tvOthers
            // 
            this.tvOthers.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvOthers.FullRowSelect = true;
            this.tvOthers.HideSelection = false;
            this.tvOthers.Location = new System.Drawing.Point(79, 181);
            this.tvOthers.Name = "tvOthers";
            this.tvOthers.Size = new System.Drawing.Size(101, 216);
            this.tvOthers.TabIndex = 3;
            this.tvOthers.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvFriends_BeforeSelect);
            this.tvOthers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFriends_AfterSelect);
            this.tvOthers.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvFriends_NodeMouseClick);
            // 
            // btnSearchFriend
            // 
            this.btnSearchFriend.AutoSize = true;
            this.btnSearchFriend.Location = new System.Drawing.Point(116, 4);
            this.btnSearchFriend.Name = "btnSearchFriend";
            this.btnSearchFriend.Size = new System.Drawing.Size(57, 22);
            this.btnSearchFriend.TabIndex = 7;
            this.btnSearchFriend.Text = "搜索(&S)";
            this.btnSearchFriend.UseVisualStyleBackColor = true;
            this.btnSearchFriend.Click += new System.EventHandler(this.tbSearchFriend_TextChanged);
            // 
            // tbSearchFriend
            // 
            this.tbSearchFriend.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbSearchFriend.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbSearchFriend.Location = new System.Drawing.Point(0, 0);
            this.tbSearchFriend.Name = "tbSearchFriend";
            this.tbSearchFriend.Size = new System.Drawing.Size(113, 21);
            this.tbSearchFriend.TabIndex = 6;
            this.tbSearchFriend.TextChanged += new System.EventHandler(this.tbSearchFriend_TextChanged);
            this.tbSearchFriend.Enter += new System.EventHandler(this.tbSearchFriend_Enter);
            this.tbSearchFriend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearchFriend_KeyDown);
            this.tbSearchFriend.Leave += new System.EventHandler(this.tbSearchFriend_Leave);
            // 
            // lbPageNumber
            // 
            this.lbPageNumber.AutoSize = true;
            this.lbPageNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbPageNumber.Location = new System.Drawing.Point(430, 9);
            this.lbPageNumber.Name = "lbPageNumber";
            this.lbPageNumber.Size = new System.Drawing.Size(59, 12);
            this.lbPageNumber.TabIndex = 18;
            this.lbPageNumber.Text = "Num/Total";
            this.lbPageNumber.Click += new System.EventHandler(this.lbPageNumber_Click);
            // 
            // wbHistory
            // 
            this.wbHistory.BackColor = System.Drawing.Color.White;
            this.wbHistory.Location = new System.Drawing.Point(3, 47);
            this.wbHistory.Name = "wbHistory";
            this.wbHistory.Size = new System.Drawing.Size(166, 206);
            this.wbHistory.TabIndex = 17;
            this.wbHistory.WebView = this.wbView;
            // 
            // wbView
            // 
            this.wbView.AllowDropLoad = false;
            this.wbView.NewWindow += new EO.WebBrowser.NewWindowHandler(this.wbView_NewWindowEvent);
            this.wbView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.wbView_MouseClick);
            // 
            // btnLast
            // 
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
            this.btnLast.Location = new System.Drawing.Point(398, 2);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(26, 26);
            this.btnLast.TabIndex = 16;
            this.btnLast.TabStop = false;
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            this.btnLast.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLast_MouseDown);
            this.btnLast.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLast_MouseUp);
            // 
            // btnFirst
            // 
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.Location = new System.Drawing.Point(302, 2);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(26, 26);
            this.btnFirst.TabIndex = 15;
            this.btnFirst.TabStop = false;
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            this.btnFirst.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnFirst_MouseDown);
            this.btnFirst.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnFirst_MouseUp);
            // 
            // btnForward
            // 
            this.btnForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForward.Image = ((System.Drawing.Image)(resources.GetObject("btnForward.Image")));
            this.btnForward.Location = new System.Drawing.Point(366, 2);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(26, 26);
            this.btnForward.TabIndex = 14;
            this.btnForward.TabStop = false;
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            this.btnForward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnForward_MouseDown);
            this.btnForward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnForward_MouseUp);
            // 
            // btnBackward
            // 
            this.btnBackward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackward.Image = ((System.Drawing.Image)(resources.GetObject("btnBackward.Image")));
            this.btnBackward.Location = new System.Drawing.Point(334, 2);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(26, 26);
            this.btnBackward.TabIndex = 13;
            this.btnBackward.TabStop = false;
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            this.btnBackward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnBackward_MouseDown);
            this.btnBackward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnBackward_MouseUp);
            // 
            // cbSearchArea
            // 
            this.cbSearchArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchArea.FormattingEnabled = true;
            this.cbSearchArea.Items.AddRange(new object[] {
            "当前好友",
            "所有好友"});
            this.cbSearchArea.Location = new System.Drawing.Point(156, 4);
            this.cbSearchArea.Name = "cbSearchArea";
            this.cbSearchArea.Size = new System.Drawing.Size(77, 20);
            this.cbSearchArea.TabIndex = 10;
            // 
            // btnSearchHistory
            // 
            this.btnSearchHistory.AutoSize = true;
            this.btnSearchHistory.Location = new System.Drawing.Point(239, 3);
            this.btnSearchHistory.Name = "btnSearchHistory";
            this.btnSearchHistory.Size = new System.Drawing.Size(57, 22);
            this.btnSearchHistory.TabIndex = 11;
            this.btnSearchHistory.Text = "搜索(&F)";
            this.btnSearchHistory.UseVisualStyleBackColor = true;
            this.btnSearchHistory.Click += new System.EventHandler(this.btnSearchHistory_Click);
            // 
            // tbSearchHistory
            // 
            this.tbSearchHistory.Location = new System.Drawing.Point(3, 4);
            this.tbSearchHistory.Name = "tbSearchHistory";
            this.tbSearchHistory.Size = new System.Drawing.Size(147, 21);
            this.tbSearchHistory.TabIndex = 9;
            this.tbSearchHistory.Enter += new System.EventHandler(this.tbSearchHistory_Enter);
            this.tbSearchHistory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearchHistory_KeyDown);
            this.tbSearchHistory.Leave += new System.EventHandler(this.tbSearchHistory_Leave);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 595);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微信聊天记录查看器";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvFriends;
        private System.Windows.Forms.Button btnSearchFriend;
        private System.Windows.Forms.TextBox tbSearchFriend;
        private System.Windows.Forms.ComboBox cbSearchArea;
        private System.Windows.Forms.Button btnSearchHistory;
        private System.Windows.Forms.TextBox tbSearchHistory;
        private System.Windows.Forms.Button btnBackward;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnForward;
        //private Awesomium.Windows.Forms.WebControl wbHistory;
        private EO.WebBrowser.WinForm.WebControl wbHistory;
        private EO.WebBrowser.WebView wbView;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView tvGroup;
        private System.Windows.Forms.TreeView tvOthers;
        private System.Windows.Forms.Label lbPageNumber;
    }
}

