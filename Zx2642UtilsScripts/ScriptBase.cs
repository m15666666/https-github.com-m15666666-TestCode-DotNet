using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Zx2642UtilsScripts
{
    /// <summary>
    /// 脚本类的基类
    /// </summary>
    public abstract class ScriptBase
    {
        /// <summary>
        /// 向输出写信息，然后换行
        /// </summary>
        /// <param name="message"></param>
        public void WriteLine( string message = "" )
        {
            // 使用Visual Studio IDE 的输出窗口输出脚本
            Debug.WriteLine( message );
        }

        #region 数据库相关

        /// <summary>
        /// 生成delete 语句
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="commit"></param>
        protected void GenerateDeleteSQL( IEnumerable<string> tables, string commit = null )
        {
            foreach (var table in tables)
            {
                WriteLine($"DELETE FROM {table};");
                if( !string.IsNullOrWhiteSpace(commit)) WriteLine($"{commit};");
            }
        }

        /// <summary>
        /// 生成insert into select from 语句
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="sourceTablePrefix"></param>
        protected void GenerateInsertIntoSelectFromSQL( IEnumerable<string> tables, string sourceTablePrefix )
        {
            foreach (var table in tables)
            {
                WriteLine($"INSERT INTO {table} SELECT * FROM {sourceTablePrefix}.{table};");
            }
        }

        protected void GenerateInsertIntoSelectFromSQL(string table, string sql )
        {
            WriteLine($"INSERT INTO {table} {sql};");
        }

        protected void GenerateObjectKeyStartValueSQL( List<string[]> tableAndKeyNames, int startValue, string objectKeyTable = "BS_ObjectKey" )
        {
            WriteLine($"{Environment.NewLine}{Environment.NewLine}");
            WriteLine("--ObjectKeyStartValue");
            WriteLine($"UPDATE {objectKeyTable} set KeyValue = {startValue};");
            foreach (var tableAndKey in tableAndKeyNames)
            {
                var table = tableAndKey[0];
                var key = tableAndKey[1];
                WriteLine($"DELETE FROM {objectKeyTable} where Source_CD = '{table}' and KeyName = '{key}';");
                WriteLine($"INSERT INTO {objectKeyTable} values('{table}','{key}',{startValue},null,null);");
                WriteLine("");
            }
        }

        #endregion

        #region Entity framework 相关
        
        /// <summary>
        /// 生成 public virtual DbSet<Analysis_PntPosition> Analysis_PntPosition { get; set; } 类似 语句
        /// </summary>
        /// <param name="classNames"></param>
        protected void GenerateEfDbContextDbSet(IEnumerable<string> classNames )
        {
            foreach (var className in classNames )
            {
                WriteLine($"public virtual DbSet<{className}> {className}" + " { get; set; }");
            }
        }

        #endregion
    }
}
