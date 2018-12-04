using System;
using System.Collections.Generic;
using System.Text;

namespace Zx2642UtilsScripts
{
    /// <summary>
    /// 酒钢数据库脚本生成器类，MS SQL SERVER数据库
    /// </summary>
    public class WineSteelScript : ScriptBase
    {
        #region 可能需要修改

        /// <summary>
        /// 需要导出的分厂数据库名
        /// </summary>
        private const string DatabaseNameOfExporting = "jgzx";

        #endregion

        private static readonly string[] _tables = new string[] 
        {
            "Analysis_MObjPicture",
            "Analysis_MObjPosition",
            "Analysis_PntPosition",
            "BS_AppRole",
            "BS_AppUser",
            "BS_AppuserPost",
            "BS_Dept",
            "BS_Org",
            "BS_Post",
            "Mob_MObject",
            "Mob_MobjectStructure",
            "Pnt_Point",
            "BS_AppUserSpec",
            "BS_AppRoleAction",
            "BS_PostAppAction",

        };

        //private const string Table_BS_AppRoleAction = "BS_AppRoleAction";

        private void GenerateDeleteSQLs()
        {
            List<string> deleteTables = new List<string>(_tables);
            //deleteTables.Add(Table_BS_AppRoleAction);

            base.GenerateDeleteSQL( deleteTables );
        }

        private void GenerateInsertIntoSelectFromSQL()
        {
            base.GenerateInsertIntoSelectFromSQL(_tables, $"[{DatabaseNameOfExporting}].[dbo]");

            //var tableOfExporting = $"[{DatabaseNameOfExporting}].[dbo].{Table_BS_AppRoleAction}";
            //GenerateInsertIntoSelectFromSQL(Table_BS_AppRoleAction, $"SELECT * FROM {tableOfExporting} WHERE {tableOfExporting}.AppRole_ID > 1000000");
        }

        private void GenerateObjectKeyStartValueSQL( )
        {
            int startValue = 3000000;

            List<string[]> tableAndKeyNames = new List<string[]> {
                new string[]{"Pnt_Point","Point_ID" },
                new string[]{ "Analysis_PntPosition", "PntPosition_ID" },
                new string[]{ "Analysis_MObjPosition", "MObjPosition_ID" },
                new string[]{ "Mob_MObject", "Mobject_ID" },
                new string[]{ "Mob_MobjectStructure", "MobjectStructure_ID" },
                new string[]{ "BS_Org", "Org_ID" },
                new string[]{ "BS_Dept", "Dept_ID" },
                new string[]{ "BS_Post", "Post_ID" },
                new string[]{ "BS_AppUser", "AppUser_ID" },
                new string[]{ "BS_AppuserPost", "AppuserPost_ID" },
                new string[]{ "BS_AppRole", "AppRole_ID" },
                //new string[]{"","" },
                //new string[]{"","" },
                //new string[]{"","" },
                //new string[]{"","" },
                //new string[]{"","" },
                //new string[]{"","" },
                //new string[]{"","" },
                //new string[]{"","" },
                //new string[]{"","" },
                //new string[]{"","" },
                //new string[]{"","" },
            };
            base.GenerateObjectKeyStartValueSQL(tableAndKeyNames, startValue);
        }

        /// <summary>
        /// 生成导出分厂的脚本
        /// </summary>
        public void GenerateExportSQLs()
        {
            GenerateDeleteSQLs();
            GenerateInsertIntoSelectFromSQL();
            GenerateObjectKeyStartValueSQL();
        }
    }
}
