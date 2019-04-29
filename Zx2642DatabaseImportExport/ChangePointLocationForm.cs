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

        private Dictionary<int, Mob_MObject> _mobjectId2MObject = new Dictionary<int, Mob_MObject>();
        private Dictionary<int, Mob_MobjectStructure> _mobjectId2MStructure = new Dictionary<int, Mob_MobjectStructure>();
        private Dictionary<int, TreeNode> _mobjectId2Node = new Dictionary<int, TreeNode>();

        private Dictionary<int, Pnt_Point> _pointId2Point = new Dictionary<int, Pnt_Point>();
        private List<Pnt_Point> _points = new List<Pnt_Point>();

        private void btn_LoadMObjectTree_Click(object sender, EventArgs e)
        {
            tv_MObject.CheckBoxes = false;
            tv_MObject.Nodes.Clear();
            lv_ChildNodes.Items.Clear();
            _mobjectId2MObject.Clear();
            _mobjectId2MStructure.Clear();
            _mobjectId2Node.Clear();
            _pointId2Point.Clear();

            var mobjects = RepositoryQuery.List<Mob_MObject>().ToList();
            var mstructures = RepositoryQuery.List<Mob_MobjectStructure>().ToList();
            _points = RepositoryQuery.List<Pnt_Point>().ToList();

            foreach( var p in _points)
            {
                _pointId2Point[p.Point_ID] = p;
            }

            foreach (var m in mstructures)
            {
                _mobjectId2MStructure.Add(m.Mobject_ID, m);
            }

            foreach ( var m in mobjects )
            {
                if (!_mobjectId2MStructure.ContainsKey(m.Mobject_ID)) continue;

                var structure = _mobjectId2MStructure[m.Mobject_ID];
                m.Parent_ID = structure.Parent_ID;
                m.Lever_NR = structure.Lever_NR;
                _mobjectId2MObject.Add(m.Mobject_ID, m);
            }

            mobjects = mobjects.OrderBy(m => m.Lever_NR).ThenBy(m => m.MobjectName_TX).ToList();

            List<TreeNode> topNodes = new List<TreeNode>();
            foreach (var m in mobjects)
                AddTreeNode(topNodes, _mobjectId2Node, m);

            tv_MObject.Nodes.AddRange(topNodes.ToArray());
        }

        private void AddTreeNode(List<TreeNode> topNodes, Dictionary<int, TreeNode> mobjectId2Node, Mob_MObject m)
        {
            if (mobjectId2Node.ContainsKey(m.Mobject_ID)) return;

            TreeNode node = new TreeNode(m.MobjectName_TX);
            node.Tag = m.Mobject_ID;
            mobjectId2Node[m.Mobject_ID] = node;

            var parentId = m.Parent_ID;
            if( parentId == 0)
            {
                topNodes.Add(node);
                return;
            }
            if (!mobjectId2Node.ContainsKey(parentId))
            {
                if (!_mobjectId2MObject.ContainsKey(parentId)) return;
                AddTreeNode(topNodes, mobjectId2Node, _mobjectId2MObject[parentId]);
            }

            mobjectId2Node[parentId].Nodes.Add(node);
        }

        protected override void OnClosed(EventArgs e)
        {
            using (DbContext) { }
            base.OnClosed(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var dbContext = new MyDbContext();
            DbContext = dbContext;
            Repository = new RepositoryEFImpl() { DbContext = dbContext };
            RepositoryQuery = new RepositoryQueryEFImpl() { DbContext = dbContext };

            //IUnitOfWork unitOfWork = (repository as IUnitOfWork);

            //int personID1 = 1;
            //repository.DeleteByKeys<BS_Person>(personID1);
            //unitOfWork.Commit();

            //BS_Person p1 = new BS_Person { Id_NR = personID1, Name_TX = "P1-张" };
            //repository.Add(p1);

            //unitOfWork.Commit();

            //p1 = repository.GetByKeys<BS_Person>(personID1);
            //p1.Description_TX = "张三";
            //repository.Update(p1);

            //unitOfWork.Commit();
        }

        #region ef

        private DbContext DbContext { get; set; }
        private IRepository Repository { get; set; }

        private IRepositoryQuery RepositoryQuery { get; set; }

        private IUnitOfWork UnitOfWork => Repository as IUnitOfWork;


        #endregion

        #region cut points

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

                var parentStructure = _mobjectId2MStructure[parentId];
                List<Pnt_Point> changedMObjects = new List<Pnt_Point>();

                foreach( var pointId in _cutPointIds)
                {
                    var p = _pointId2Point[pointId];
                    p.Mobject_ID = parentId;
                    changedMObjects.Add(p);
                }

                _cutPointIds.Clear();

                IRepository repository = Repository;

                foreach (var item in changedMObjects)
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
