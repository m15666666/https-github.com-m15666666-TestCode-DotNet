using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class DataBaseHelper : IDisposable
    {
        public DbContext DbContext { get; set; }
        public IRepository Repository { get; set; }

        public IRepositoryQuery RepositoryQuery { get; set; }

        public IUnitOfWork UnitOfWork => Repository as IUnitOfWork;

        /// <summary>
        /// 初始化数据库
        /// </summary>
        public void InitDataBaseConnection()
        {
            var dbContext = new MyDbContext();
            DbContext = dbContext;
            Repository = new RepositoryEFImpl() { DbContext = dbContext };
            RepositoryQuery = new RepositoryQueryEFImpl() { DbContext = dbContext };
        }

        /// <summary>
        /// 绑定公司列表
        /// </summary>
        /// <param name="cmb_Orgs"></param>
        public void BindOrgs( System.Windows.Forms.ComboBox cmb_Orgs )
        {
            var orgs = RepositoryQuery.List<BS_Org>().OrderBy(item => item.OrgName_TX).ToList();

            cmb_Orgs.DisplayMember = "OrgName_TX";
            cmb_Orgs.ValueMember = "Org_ID";
            cmb_Orgs.DataSource = orgs;
            if (0 < cmb_Orgs.Items.Count) cmb_Orgs.SelectedIndex = 0;
        }

        public void Dispose()
        {
            var dbContext = DbContext;
            if (dbContext != null)
            {
                DbContext = null;
                using (dbContext) { }
            }
        }
    }
}
