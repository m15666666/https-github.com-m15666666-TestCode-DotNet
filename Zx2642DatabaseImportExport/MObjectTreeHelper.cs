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
    /// <summary>
    /// 设备树加载助手类
    /// </summary>
    public class MObjectTreeHelper
    {
        public Dictionary<int, Mob_MObject> MobjectId2MObject { get; set; }
        public Dictionary<int, Mob_MobjectStructure> MobjectId2MStructure { get; set; }
        public Dictionary<int, TreeNode> MobjectId2Node { get; set; }

        #region ef

        public DataBaseHelper DataBaseHelper { get; set; }
        private IRepository Repository => DataBaseHelper.Repository;
        private IRepositoryQuery RepositoryQuery => DataBaseHelper.RepositoryQuery;
        private IUnitOfWork UnitOfWork => DataBaseHelper.UnitOfWork;

        #endregion

        public void LoadMObjectTree(TreeView tv_MObject, int orgId )
        {
            tv_MObject.CheckBoxes = false;
            tv_MObject.Nodes.Clear();

            MobjectId2MObject = new Dictionary<int, Mob_MObject>();
            MobjectId2MStructure = new Dictionary<int, Mob_MobjectStructure>();
            MobjectId2Node = new Dictionary<int, TreeNode>();

            var mobjects = RepositoryQuery.List<Mob_MObject>(m => m.Org_ID == orgId && m.Active_YN == "1").ToList();
            var mstructures = RepositoryQuery.List<Mob_MobjectStructure>(m => m.Org_ID == orgId).ToList();

            foreach (var m in mstructures)
            {
                MobjectId2MStructure.Add(m.Mobject_ID, m);
            }

            foreach (var m in mobjects)
            {
                if (!MobjectId2MStructure.ContainsKey(m.Mobject_ID)) continue;

                var structure = MobjectId2MStructure[m.Mobject_ID];
                m.Parent_ID = structure.Parent_ID;
                m.Lever_NR = structure.Lever_NR;
                MobjectId2MObject.Add(m.Mobject_ID, m);
            }

            mobjects = mobjects.OrderBy(m => m.Lever_NR).ThenBy(m => m.MobjectName_TX).ToList();

            List<TreeNode> topNodes = new List<TreeNode>();
            foreach (var m in mobjects)
                AddTreeNode(topNodes, MobjectId2Node, m);

            tv_MObject.Nodes.AddRange(topNodes.ToArray());
        }

        private void AddTreeNode(List<TreeNode> topNodes, Dictionary<int, TreeNode> mobjectId2Node, Mob_MObject m)
        {
            if (mobjectId2Node.ContainsKey(m.Mobject_ID)) return;

            TreeNode node = new TreeNode(m.MobjectName_TX);
            node.Tag = m.Mobject_ID;
            mobjectId2Node[m.Mobject_ID] = node;

            var parentId = m.Parent_ID;
            if (parentId == 0)
            {
                topNodes.Add(node);
                return;
            }
            if (!mobjectId2Node.ContainsKey(parentId))
            {
                if (!MobjectId2MObject.ContainsKey(parentId)) return;
                AddTreeNode(topNodes, mobjectId2Node, MobjectId2MObject[parentId]);
            }

            mobjectId2Node[parentId].Nodes.Add(node);
        }
    }
}
