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
    public partial class ChangePointLocationForm : Form
    {
        public ChangePointLocationForm()
        {
            InitializeComponent();
        }

        private Dictionary<int, Pnt_Point> _pointId2Point = new Dictionary<int, Pnt_Point>();
        private List<Pnt_Point> _points = new List<Pnt_Point>();

        private void btn_LoadMObjectTree_Click(object sender, EventArgs e)
        {
            lv_ChildNodes.Items.Clear();
            _pointId2Point.Clear();
            MObjectTreeHelper.LoadMObjectTree(tv_MObject, OrgId);

            _points = RepositoryQuery.List<Pnt_Point>().ToList();
            foreach( var p in _points)
            {
                _pointId2Point[p.Point_ID] = p;
            }
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
        private IRepositoryQuery RepositoryQuery => DataBaseHelper.RepositoryQuery;

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

        #region cut points

        private MObjectTreeHelper MObjectTreeHelper { get; set; }
        private int _lastSelectedMObjectId = 0;
        private int _cutFromMObjectId = 0;
        private List<int> _cutPointIds = new List<int>();

        #endregion

        private void btn_Cut_Click(object sender, EventArgs e)
        {
            var selectedNode = tv_MObject.SelectedNode;
            if (selectedNode == null) return;

            _cutPointIds.Clear();
            _cutFromMObjectId = _lastSelectedMObjectId;
            foreach ( ListViewItem i in lv_ChildNodes.CheckedItems )
            {
                var id = Convert.ToInt32(i.Tag);

                _cutPointIds.Add(id);

                lv_ChildNodes.Items.Remove(i);
            }
        }

        private List<Pnt_Point> GetPointsByMObjectId( int mobjectId )
        {
            return _points.Where(p => p.Mobject_ID == mobjectId).OrderBy(p => p.PointName_TX).ToList();
        }

        private void tv_MObject_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lv_ChildNodes.Items.Clear();

            var selectedNode = tv_MObject.SelectedNode;
            if (selectedNode == null) return;

            var mobjectId = Convert.ToInt32( selectedNode.Tag );
            _lastSelectedMObjectId = mobjectId;

            LoadPointsInListView(mobjectId);
        }

        private void LoadPointsInListView(int mobjectId)
        {
            lv_ChildNodes.Items.Clear();

            var points = GetPointsByMObjectId(mobjectId);

            foreach (var p in points)
            {
                var id = p.Point_ID;
                ListViewItem i = new ListViewItem(p.PointName_TX);
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
                if (selectedNode == null || _cutPointIds.Count == 0) return;
                if (_cutFromMObjectId == _lastSelectedMObjectId) return;

                var parentId = _lastSelectedMObjectId;

                var parentStructure = MobjectId2MStructure[parentId];
                List<Pnt_Point> changedPoints = new List<Pnt_Point>();

                foreach( var pointId in _cutPointIds)
                {
                    var p = _pointId2Point[pointId];
                    p.Mobject_ID = parentId;
                    changedPoints.Add(p);
                }

                _cutPointIds.Clear();

                IRepository repository = Repository;

                foreach (var item in changedPoints)
                {
                    repository.AddOrUpdate(item);
                }

                UnitOfWork.Commit();

                LoadPointsInListView(parentId);
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
