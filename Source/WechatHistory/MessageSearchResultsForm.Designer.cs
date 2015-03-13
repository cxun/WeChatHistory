namespace WechatHistory
{
    partial class MessageSearchResultsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageSearchResultsForm));
            this.listView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(698, 335);
            this.listView.TabIndex = 0;
            this.listView.TileSize = new System.Drawing.Size(1280, 18);
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Tile;
            this.listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
            // 
            // MessageSearchResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 335);
            this.Controls.Add(this.listView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MessageSearchResultsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "搜索结果";
            this.Activated += new System.EventHandler(this.MessageSearchResultsForm_Activated);
            this.Deactivate += new System.EventHandler(this.MessageSearchResultsForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessageSearchResultsForm_FormClosing);
            this.Shown += new System.EventHandler(this.MessageSearchResultsForm_Shown);
            this.Resize += new System.EventHandler(this.MessageSearchResultsForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView listView;
    }
}