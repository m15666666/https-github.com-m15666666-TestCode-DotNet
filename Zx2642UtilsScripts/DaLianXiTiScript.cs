using System;
using System.Collections.Generic;
using System.Text;

namespace Zx2642UtilsScripts
{
    /// <summary>
    /// 大连西太数据库脚本生成器类，MS SQL SERVER数据库
    /// </summary>
    public class DaLianXiTiScript : ScriptBase
    {
        #region 可能需要修改

        /// <summary>
        /// 需要导出数据的备份数据库名
        /// </summary>
        private const string DatabaseNameOfExporting = "jgzx";

        #endregion

        private void GenerateInsertIntoSelectFromSQL()
        {
            const int Year_Begin = 2015;
            const int Year_End = 2016;

            const int Month_Begin = 1;
            const int Month_End = 12;

            string[] tables = new string[] { "ZX_History_Summary", "ZX_History_Waveform", "ZX_History_FeatureValue" };
            for ( int year = Year_End; Year_Begin <= year; year--)
            {
                for( int month = Month_End; Month_Begin <= month; month--)
                {
                    foreach( var table in tables)
                    {
                        base.GenerateInsertIntoSelectFromSQL_PartitionByMonth(table, $"[{DatabaseNameOfExporting}].[dbo]", year, month );
                    }
                    WriteLine();
                }
            }
        }

        /// <summary>
        /// 生成导出分厂的脚本
        /// </summary>
        public void GenerateExportSQLs()
        {
            //ZX_History_Summary201903,ZX_History_Waveform201511,ZX_History_FeatureValue201511
            //var dt = new DateTime(2019, 1, 1);
            //var dt2 = dt.AddMonths(1);
            //var dt4 = dt2.AddSeconds(-1);
            //var s4 = dt4.ToString("yyyyMMddHHmmss");
            GenerateInsertIntoSelectFromSQL();
        }
    }
}
