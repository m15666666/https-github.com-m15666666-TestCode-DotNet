using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testORM.EF;
using Zx2642DatabaseImportExport.EntityFramework;

namespace Zx2642DatabaseImportExport
{
    /// <summary>
    /// 数据库访问助手
    /// </summary>
    public class DataBaseHelper
    {
        public IRepository Repository { get; set; }

        public IRepositoryQuery RepositoryQuery { get; set; }

        public void BindOrgs( System.Windows.Forms.ComboBox cmb_Orgs )
        {
            var orgs = RepositoryQuery.List<BS_Org>().OrderBy(item => item.OrgName_TX).ToList();

            cmb_Orgs.DisplayMember = "OrgName_TX";
            cmb_Orgs.ValueMember = "Org_ID";
            cmb_Orgs.DataSource = orgs;
            if (0 < cmb_Orgs.Items.Count) cmb_Orgs.SelectedIndex = 0;
        }
    }
}
