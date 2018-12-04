using System;
using System.Collections.Generic;
using System.Text;

namespace Zx2642UtilsScripts
{
    /// <summary>
    /// Entity Framework 脚本生成器类
    /// </summary>
    public class EFScript : ScriptBase
    {
        /// <summary>
        /// 生成 public virtual DbSet<Analysis_PntPosition> Analysis_PntPosition { get; set; } 类似 语句
        /// </summary>
        private void GenerateEfDbContextDbSet()
        {
            List<string> classNames = new List<string>() {
                "Analysis_PntPosition","Analysis_MObjPosition","Analysis_MObjPicture"
            };

            GenerateEfDbContextDbSet(classNames);
        }


        /// <summary>
        /// 生成导出的脚本
        /// </summary>
        public void GenerateExportScripts()
        {
            GenerateEfDbContextDbSet();
        }
    }
}
