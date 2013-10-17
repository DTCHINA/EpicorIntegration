using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using EdmLib;
using System.IO;

namespace VaultExplorerControl
{
    public enum TreeViewChanges
    {
        Clear =0,
        AddTopLevel,
        AddNode,
        Expand
    }
    public partial class VaultExplorer : UserControl
    {
        #region Class Variables
        List<TreeNode> nodeList = new List<TreeNode>();
        string m_rootPath, m_vaultName;
        FileSystemWatcher fsw;
        bool m_AutoRefresh;
        bool IsRefreshing = false;
        string m_currentPath;
        List<string> pathTracker = new List<string>();
        int index = 0;

        #endregion

        #region Class Functions and Events
        public VaultExplorer(string rootPath, string vaultName)
        {
            InitializeComponent();

            m_rootPath = rootPath;
            m_vaultName = vaultName;

            LoadTree();

            fsw = new FileSystemWatcher(rootPath);

            fsw.Changed += new FileSystemEventHandler(fsw_Changed);
            fsw.Created += new FileSystemEventHandler(fsw_Created);
            fsw.Deleted += new FileSystemEventHandler(fsw_Deleted);
            fsw.Renamed += new RenamedEventHandler(fsw_Renamed);

            m_AutoRefresh = true;
            fsw.EnableRaisingEvents = m_AutoRefresh;

            

        }

        public void RefreshViews()
        {
            LoadTree();
            IsRefreshing = true;
        }

        void SetPath(string path)
        {
            SetPath(path, true);

        }

        void SetPath(string path, bool storePath)
        {
            if (path != "blank")
            {
                m_currentPath = path;
                path = path.Replace('/', '\\');

                foreach (TreeNode tn in nodeList)
                {
                    if (tn.Tag.ToString() == m_currentPath)
                    {
                        this.treeView1.SelectedNode = tn;
                    }
                }

                RaisePathChangedEvent();

                if (storePath)
                {
                    this.pathTracker.Add(path);
                }
            }

        }

        public delegate void PathChangedEventHanlder(object sender, PathChangedEventArgs e);
        public event PathChangedEventHanlder PathChangedEvent;

        protected virtual void RaisePathChangedEvent()
        {
            PathChangedEvent(this, new PathChangedEventArgs(m_currentPath));
        }

        public String CurrentPath
        {
            get
            {
                return m_currentPath;
            }
            set
            {
                m_currentPath = value;
                SetPath(m_currentPath);
            }
        }

        public void MoveUp()
        {
            System.IO.DirectoryInfo d0 = new System.IO.DirectoryInfo(m_currentPath);
            if (d0.Parent != null & m_currentPath != this.m_rootPath)
            {
           
                DirectoryInfo d1 = d0.Parent;
                //d1 = d1.Parent;
                this.webBrowser1.Navigate(d1.FullName);
            }
        }

        public void MoveForward()
        {

        }

        public void MoveBack()
        {
            //if (this.pathTracker.Count > 0)
            //{
            //    string path = this.pathTracker[this.pathTracker.Count - 2];
            //    SetPath(path, false);

            //}
        }


        #endregion

        #region FileSystemWatch events
        void fsw_Renamed(object sender, RenamedEventArgs e)
        {
            RefreshViews();
        }

        void fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            RefreshViews();
        }

        void fsw_Created(object sender, FileSystemEventArgs e)
        {
            RefreshViews();
        }

        void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            RefreshViews();
        }
        #endregion

        #region TreeView Functions and Events
        private void LoadTree()
        {
            nodeList.Clear();

            ChangeTreeView(this.treeView1, TreeViewChanges.Clear, null, null);


            TreeNode tn = (TreeNode)ChangeTreeView(this.treeView1, TreeViewChanges.AddTopLevel, m_vaultName, null);


            DirectoryInfo dirInf = new DirectoryInfo(m_rootPath);

            GetDirectories(dirInf.GetDirectories(), tn);

            nodeList.Add(tn);

            ChangeTreeView(this.treeView1, TreeViewChanges.Expand, null, tn);
        
        }

        public delegate object ChangeTreeViewHandler(Control sender, TreeViewChanges action, object nodes, object node);
        public object ChangeTreeView(Control sender, TreeViewChanges action, object nodes, object node)
        {
            object obj = null;

            if (sender.InvokeRequired)
            {
                ChangeTreeViewHandler d = new ChangeTreeViewHandler(ChangeTreeView);
                return sender.Invoke(d, new object[]{ sender, action, nodes, node });
                
            }
            else
            {
                
                switch (action)
                {
                    case TreeViewChanges.Clear:
                        {
                            TreeView tv = (TreeView)sender;
                            tv.Nodes.Clear();
                        }
                        break;
                    case TreeViewChanges.AddTopLevel:
                        {
                            TreeView tv = (TreeView)sender;

                            TreeNode tn = tv.Nodes.Add((string)nodes);
                            tn.Tag = m_rootPath;

                            tn.ImageIndex = 2;
                            tn.SelectedImageIndex = 2;
                            tn.Expand();

                            obj = tn;
                        }
                        break;
                    case TreeViewChanges.AddNode:
                        {
                            TreeNode tn = (TreeNode)node;
                            tn.Nodes.Add((TreeNode)nodes);

                            obj = tn;
                        }
                        break;
                    case TreeViewChanges.Expand:
                        {
                            TreeNode tn = (TreeNode)node;
                            tn.Expand();
                        }
                        break;

                    default:
                        break;
                }

                return obj;
                
            }

           

        }

        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            try
            {
                TreeNode aNode;
                DirectoryInfo[] subSubDirs;
                foreach (DirectoryInfo subDir in subDirs)
                {
                    aNode = new TreeNode(subDir.Name, 0, 0);
                    aNode.Tag = subDir.FullName;
                    aNode.ImageIndex = 0;
                    aNode.SelectedImageIndex = 1;
                    subSubDirs = subDir.GetDirectories();
                    if (subSubDirs.Length != 0)
                    {
                        GetDirectories(subSubDirs, aNode);
                    }
                    ChangeTreeView(this.treeView1, TreeViewChanges.AddNode, aNode,nodeToAddTo);
                    
                    nodeList.Add(aNode);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                //throw;
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.webBrowser1.Navigate(e.Node.Tag.ToString());
        }
        #endregion

        #region Web Browser Functions and Events

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (IsRefreshing)
            {
                SetPath(m_currentPath);
                IsRefreshing = false;
            }
            else
            {
                SetPath(this.webBrowser1.Url.LocalPath);
            }
        }

        #endregion

    }

    public class PathChangedEventArgs
    {
        private string m_FilePath;

        public PathChangedEventArgs(string s) { FilePath = s; }
        public String FilePath
        {
            get
            {
                return m_FilePath;
            }
            private set
            {
                m_FilePath = value;
            }
        }
    }
}