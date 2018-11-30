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
        public void WriteLine( string message = "" )
        {
            Debug.WriteLine( message );
        }

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
    }
}
