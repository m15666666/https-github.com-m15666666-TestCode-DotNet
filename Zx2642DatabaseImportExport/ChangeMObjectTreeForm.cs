using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testORM.EF;
using Zx2642DatabaseImportExport.EntityFramework;

namespace Zx2642DatabaseImportExport
{
    public partial class ChangeMObjectTreeForm : Form
    {
        public ChangeMObjectTreeForm()
        {
            InitializeComponent();
        }

        private void btn_LoadMObjectTree_Click(object sender, EventArgs e)
        {
            lv_ChildNodes.Items.Clear();
            MObjectTreeHelper.LoadMObjectTree(tv_MObject, OrgId);
        }

        protected override void OnClosed(EventArgs e)
        {
            using (DataBaseHelper) { }
            base.OnClosed(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var dataBaseHelper = DataBaseHelper = new DataBaseHelper();
            dataBaseHelper.InitDataBaseConnection();

            dataBaseHelper.BindOrgs(cmb_Orgs);

            MObjectTreeHelper = new MObjectTreeHelper { DataBaseHelper = dataBaseHelper };
        }

        #region ef

        private DataBaseHelper DataBaseHelper { get; set; }
        private IRepository Repository => DataBaseHelper.Repository;
        private IUnitOfWork UnitOfWork => DataBaseHelper.UnitOfWork;

        #endregion

        #region 设备树

        /// <summary>
        /// 当前选择的OrgId
        /// </summary>
        private int OrgId
        {
            get { return 0 < cmb_Orgs.Items.Count ? (int)cmb_Orgs.SelectedValue : -1; }
        }

        private Dictionary<int, Mob_MObject> MobjectId2MObject => MObjectTreeHelper.MobjectId2MObject;
        private Dictionary<int, Mob_MobjectStructure> MobjectId2MStructure => MObjectTreeHelper.MobjectId2MStructure;
        private Dictionary<int, TreeNode> MobjectId2Node => MObjectTreeHelper.MobjectId2Node;

        #endregion

        #region cut mobject

        private MObjectTreeHelper MObjectTreeHelper { get; set; }
        private int _lastSelectedMObjectId = 0;
        private int _cutFromMObjectId = 0;
        private List<int> _cutChildMObjectIds = new List<int>();
        private List<TreeNode> _cutChildMObjectNodes = new List<TreeNode>();

        #endregion

        private void btn_Cut_Click(object sender, EventArgs e)
        {
            var selectedNode = tv_MObject.SelectedNode;
            if (selectedNode == null) return;

            _cutChildMObjectIds.Clear();
            _cutChildMObjectNodes.Clear();
            var parentId = _cutFromMObjectId = _lastSelectedMObjectId;
            var parentNode = MobjectId2Node[parentId];
            foreach ( ListViewItem i in lv_ChildNodes.CheckedItems )
            {
                var id = Convert.ToInt32(i.Tag);
                var node = MobjectId2Node[id];

                _cutChildMObjectIds.Add(id);
                _cutChildMObjectNodes.Add(node);

                parentNode.Nodes.Remove(node);

                lv_ChildNodes.Items.Remove(i);
            }
        }

        private void tv_MObject_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lv_ChildNodes.Items.Clear();

            var selectedNode = tv_MObject.SelectedNode;
            if (selectedNode == null) return;

            var mobjectId = Convert.ToInt32( selectedNode.Tag );
            _lastSelectedMObjectId = mobjectId;

            LoadChildMobjectsInListView(mobjectId);
        }

        private void LoadChildMobjectsInListView(int mobjectId)
        {
            lv_ChildNodes.Items.Clear();

            foreach (TreeNode n in MobjectId2Node[mobjectId].Nodes)
            {
                var id = Convert.ToInt32(n.Tag);
                ListViewItem i = new ListViewItem(MobjectId2MObject[id].MobjectName_TX);
                i.Tag = id;
                lv_ChildNodes.Items.Add(i);
            }
        }

        private void btn_Paste_Click(object sender, EventArgs e)
        {
            try
            {
                AddLog($"开始粘贴...");

                var selectedNode = tv_MObject.SelectedNode;
                if (selectedNode == null || _cutChildMObjectIds.Count == 0) return;
                if (_cutFromMObjectId == _lastSelectedMObjectId) return;

                var parentId = _lastSelectedMObjectId;
                var parentNode = MobjectId2Node[parentId];

                var parentStructure = MobjectId2MStructure[parentId];
                List<Mob_MobjectStructure> changedMObjects = new List<Mob_MobjectStructure>();
                foreach (TreeNode n in _cutChildMObjectNodes)
                {
                    parentNode.Nodes.Add(n);
                    CollectChangedMObjects(changedMObjects, parentStructure, n);
                }

                _cutChildMObjectIds.Clear();
                _cutChildMObjectNodes.Clear();

                IRepository repository = Repository;

                foreach (var item in changedMObjects)
                {
                    repository.AddOrUpdate(item);
                }

                UnitOfWork.Commit();

                LoadChildMobjectsInListView(parentId);
            }
            catch( Exception ex)
            {
                AddLog($"粘贴错误.{ex}");
            }
            finally
            {
                AddLog($"结束粘贴.");
            }
        }

        /// <summary>
        /// 获得变动的设备
        /// </summary>
        /// <param name="changedMObjects"></param>
        /// <param name="parent"></param>
        /// <param name="n"></param>
        private void CollectChangedMObjects( List<Mob_MobjectStructure> changedMObjects, Mob_MobjectStructure parent, TreeNode n )
        {
            var id = Convert.ToInt32(n.Tag);
            
            MobjectId2MObject[id].Parent_ID = parent.Mobject_ID;

            var structure = MobjectId2MStructure[id];
            structure.Lever_NR = parent.Lever_NR + 1;
            structure.Parent_ID = parent.Mobject_ID;
            structure.ParentList_TX = $"{parent.ParentList_TX}{structure.Mobject_ID}|";
            structure.Org_ID = parent.Org_ID;

            changedMObjects.Add(structure);
            foreach( TreeNode child in MobjectId2Node[structure.Mobject_ID].Nodes)
            {
                CollectChangedMObjects(changedMObjects, structure, child);
            }
        }

        #region 日志

        private void Invoke(Action action)
        {
            if (!rtb_log.InvokeRequired)
            {
                action();
                return;
            }
            rtb_log.Invoke(action);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message"></param>
        private void AddLog(string message)
        {
            Action action = () => {
                var m = string.IsNullOrWhiteSpace(message) ? Environment.NewLine : $"{DateTime.Now} {message} {Environment.NewLine}";
                rtb_log.AppendText(m);
            };

            Invoke(action);
        }

        #endregion
    }
}
