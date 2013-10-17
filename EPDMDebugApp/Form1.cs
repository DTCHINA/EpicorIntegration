using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EdmLib;

namespace EPDMDebugApp
{
    public partial class Form1 : Form
    {
        #region Variables

        //private edmVaultSingleton m_edmVault;
        VaultExplorerControl.VaultExplorer currentExplorer;
        
        #endregion

        public Form1()
        {
            InitializeComponent();

            //m_edmVault = new edmVaultSingleton();

            this.toolStripSplitButton2.DefaultItem = this.upOnLevelToolStripMenuItem;
            this.tabControl1.TabIndexChanged += new EventHandler(tabControl1_TabIndexChanged);
            LoadExplorers();


            this.tabControl1.SelectedIndex = Properties.Settings.Default.LastIndex;

            this.currentExplorer = (VaultExplorerControl.VaultExplorer)this.tabControl1.SelectedTab.Controls[0];

          
            if (Properties.Settings.Default.LastPath != "" & this.currentExplorer != null)
            {
                this.currentExplorer.CurrentPath = Properties.Settings.Default.LastPath;

            }

            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);

            


            
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.currentExplorer != null)
            {
                Properties.Settings.Default.LastPath = this.currentExplorer.CurrentPath;
            }
            
            Properties.Settings.Default.LastIndex = this.tabControl1.SelectedIndex;
            Properties.Settings.Default.Save();
        }



        private void LoadExplorers()
        {
            IEdmVault9 edmVault = (IEdmVault9)edmVaultSingleton.Instance;

            Array views;
            EdmViewInfo[] viewInfos;
            edmVault.GetVaultViews(out views, false);
            viewInfos = (EdmViewInfo[])views;

            foreach (EdmViewInfo viewInfo in viewInfos)
            {
                this.tabControl1.TabPages.Add(viewInfo.mbsVaultName, viewInfo.mbsVaultName);
                TabPage page = this.tabControl1.TabPages[viewInfo.mbsVaultName];


                VaultExplorerControl.VaultExplorer exp = new VaultExplorerControl.VaultExplorer(viewInfo.mbsPath, viewInfo.mbsVaultName);

                exp.Dock = DockStyle.Fill;
                exp.PathChangedEvent += new VaultExplorerControl.VaultExplorer.PathChangedEventHanlder(exp_PathChangedEvent);
                page.Controls.Add(exp);
               
            }
        }

        void exp_PathChangedEvent(object sender, VaultExplorerControl.PathChangedEventArgs e)
        {
            this.toolStripTextBox1.Text = e.FilePath;
            Graphics g = this.CreateGraphics();
            SizeF s = g.MeasureString(e.FilePath, this.toolStripTextBox1.Font);
            int i = (int)s.Width;
            this.toolStripTextBox1.Width = (int)s.Width + 2;
        }



        private int Hwnd
        {
            get
            {
                return (int)this.Handle;
            }
        }

        #region Toolbar items
        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            if (this.currentExplorer != null)
            {

            }
        }

        private void upOnLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentExplorer != null)
            {
                this.currentExplorer.MoveUp();
            }
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.currentExplorer != null)
            {
                this.currentExplorer.RefreshViews();
            }
        }


        #endregion

        #region Tab Functions and Events
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            TabPage page = this.tabControl1.SelectedTab;
            currentExplorer = (VaultExplorerControl.VaultExplorer)page.Controls[0];
        }

        void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            TabPage page = this.tabControl1.SelectedTab;
            currentExplorer = (VaultExplorerControl.VaultExplorer)page.Controls[0];
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        #endregion






    }

    public sealed class edmVaultSingleton
    {
        private static volatile EdmVault5 instance;
        private static object syncRoot = new Object();

        private edmVaultSingleton() { }

        public static EdmVault5 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new EdmVault5();
                    }
                }

                return instance;
            }
        }
    }
 
}