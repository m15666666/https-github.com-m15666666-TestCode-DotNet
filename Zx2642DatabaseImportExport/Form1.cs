using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using testORM.EF;
using Zx2642DatabaseImportExport.EntityFramework;
using Zx2642DatabaseImportExport.ObjectMaps;

namespace Zx2642DatabaseImportExport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// excel文件路径
        /// </summary>
        private string ExcelFilePath { get; set; } = "ExcelUtils.xls";

        private void mnu_exportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void mnu_importExcelToDatabase_Click(object sender, EventArgs e)
        {
            ImportExcelToDatabase();
        }

        private void ExportToExcel()
        {
            //ClearLog();
            SetBusy(true);
            AddLog("开始导出...");

            WaitCallback action = state => {
                try
                {
                    DataSet ds = new DataSet();
                    ExportToExcel<Pnt_Point>(ds);
                    ExportMOjbectToExcel(ds);

                    ExportToExcel<Sample_Server>(ds);
                    ExportToExcel<Sample_PntChannel>(ds);
                    ExportToExcel<Sample_ServerDAU>(ds);
                    ExportToExcel<Sample_DAUStation>(ds);
                    ExportToExcel<Sample_Station>(ds);
                    ExportToExcel<Sample_StationChannel>(ds);

                    ExportToExcel<Analysis_PntPosition>(ds);
                    ExportToExcel<Analysis_MObjPosition>(ds);
                    ExportToExcel<Analysis_MObjPicture>(ds);

                    ExcelUtils.DataSetToExcel(ExcelFilePath, ds);

                    AddLog("导出完成!");
                    //MessageBox.Show("导出完成!");
                }
                catch (Exception ex)
                {
                    AddLog($"导出失败！{ex}");
                }
                finally
                {
                    SetBusy(false);
                }
            };
            ThreadPool.QueueUserWorkItem(action);
        }

        private void ImportExcelToDatabase()
        {
            //ClearLog();
            SetBusy(true);
            AddLog("开始导入...");
            //Debug.WriteLine("Debug.WriteLine test");
            //Trace.WriteLine("Trace.WriteLine test");

            WaitCallback action = state => {
                try
                {
                    ImportExcelToDatabase<Pnt_Point>();
                    ImportExcelToDatabase_MObject();

                    ImportExcelToDatabase<Sample_Server>();
                    ImportExcelToDatabase<Sample_PntChannel>();
                    ImportExcelToDatabase<Sample_ServerDAU>();
                    ImportExcelToDatabase<Sample_DAUStation>();
                    ImportExcelToDatabase<Sample_Station>();
                    ImportExcelToDatabase<Sample_StationChannel>();

                    ImportExcelToDatabase<Analysis_PntPosition>();
                    ImportExcelToDatabase<Analysis_MObjPosition>();
                    ImportExcelToDatabase<Analysis_MObjPicture>();

                    AddLog("导入完成!");
                }
                catch (Exception ex)
                {
                    AddLog($"导入失败！{ex}");
                }
                finally
                {
                    SetBusy(false);
                }
            };
            ThreadPool.QueueUserWorkItem(action);
        }

        private void ExportMOjbectToExcel(DataSet ds)
        {
            var tableName = nameof(Mob_MObject);
            AddLog($"开始:{tableName}...");
            var entities = RepositoryQuery.List<Mob_MObject>().ToList();
            var mobjectId2Structure = RepositoryQuery.List<Mob_MobjectStructure>().ToDictionary(item => item.Mobject_ID);

            entities.ForEach(mobject => {
                var structure = mobjectId2Structure[mobject.Mobject_ID];
                mobject.Parent_ID = structure.Parent_ID;
            });

            var dt = entities.ToTable();
            ds.Tables.Add(dt);
            AddLog($"完成:{tableName}.");
        }

        private void ExportToExcel<T>(DataSet ds ) where T : class
        {
            var tableName = typeof(T).Name;
            AddLog($"开始:{tableName}...");
            var entities = RepositoryQuery.List<T>().ToList();

            var dt = entities.MapToTable<T>();
            ds.Tables.Add(dt);
            AddLog($"完成:{tableName}.");
        }

        /// <summary>
        /// 设置界面的忙碌状态
        /// </summary>
        /// <param name="isBusy"></param>
        private void SetBusy( bool isBusy)
        {
            Action action = () =>
            {
                rtb_log.ForeColor = isBusy ? Color.Red : Color.Green;
                mnu_importExcelToDatabase.Enabled = mnu_exportToExcel.Enabled = !isBusy;
                rtb_log.Cursor = Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
            };
            Invoke(action);
        }

        private void Invoke( Action action )
        {
            if( !rtb_log.InvokeRequired )
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
        private void AddLog( string message)
        {
            Action action = () => {
                var m = string.IsNullOrWhiteSpace( message ) ? Environment.NewLine : $"{DateTime.Now} {message} {Environment.NewLine}";
                rtb_log.AppendText( m );
            };

            Invoke( action );
        }

        /// <summary>
        /// 清空日志
        /// </summary>
        private void ClearLog()
        {
            rtb_log.Text = string.Empty;
        }

        private void ImportExcelToDatabase<T>() where T : class, new()
        {
            var type = typeof(T);
            var tableName = type.Name;
            AddLog($"开始:{tableName}...");
            var table = ExcelUtils.ExcelToDataTable(ExcelFilePath, tableName, true);
            if (table == null || table.Rows.Count == 0) return;
            var entities = table.MapTo<T>();
            IRepository repository = Repository;
            foreach(var e in entities)
            {
                repository.AddOrUpdate(e);
            }
            UnitOfWork.Commit();
            AddLog($"完成:{tableName}.");
        }

        private void ImportExcelToDatabase_MObject()
        {
            var tableName = nameof(Mob_MObject);
            var table = ExcelUtils.ExcelToDataTable(ExcelFilePath, tableName, true);
            if (table.Rows.Count == 0) return;

            AddLog($"开始:{tableName}...");

            var mobjects = table.MapTo<Mob_MObject>();
            var lookup = mobjects.ToLookup(item => item.Parent_ID);

            Dictionary<int, Mob_MobjectStructure> id2Structure = new Dictionary<int, Mob_MobjectStructure>() {
                { 0, new Mob_MobjectStructure { Lever_NR = 0, ParentList_TX = "|0|" } }
            };
            List<Mob_MobjectStructure> structures = new List<Mob_MobjectStructure>();
            List<Mob_MObject> addMobjects = new List<Mob_MObject>();
            
            Queue<int> parentIds = new Queue<int>();
            parentIds.Enqueue(0);
            while( 0 < parentIds.Count)
            {
                var parentId = parentIds.Dequeue();
                if (!lookup.Contains(parentId)) continue;

                foreach( var m in lookup[parentId] )
                {
                    Mob_MobjectStructure parentStructure = id2Structure[parentId];
                    var mid = m.Mobject_ID;
                    Mob_MobjectStructure s = new Mob_MobjectStructure {
                        MobjectStructure_ID = mid,
                        Mobject_ID = mid,
                        Org_ID = m.Org_ID,
                        Parent_ID = parentId,
                        Lever_NR = parentStructure.Lever_NR + 1,
                        ParentList_TX = parentStructure.ParentList_TX + $"{mid}|",
                    };

                    if (lookup.Contains(mid)) parentIds.Enqueue(mid);
                    addMobjects.Add(m);
                    id2Structure[mid] = s;
                    structures.Add(s);
                }
            }

            IRepository repository = Repository;

            foreach (var e in addMobjects)
            {
                repository.AddOrUpdate(e);
            }

            foreach (var e in structures)
            {
                repository.AddOrUpdate(e);
            }

            UnitOfWork.Commit();

            AddLog($"完成:{tableName}.");
        }

        protected override void OnClosed(EventArgs e)
        {
            using (DbContext) { }
            base.OnClosed(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var tempDir = AutoMapperExtension.TempDir;
            if (!Directory.Exists(tempDir)) Directory.CreateDirectory(tempDir);

            Mapper.Initialize(CustomDtoMapper.CreateMappings);

            Debug.Listeners.Clear();
            Debug.Listeners.Add(new InnerTraceListener(AddLog));

            Trace.Listeners.Clear();
            Trace.Listeners.Add(new InnerTraceListener(AddLog));

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

        /// <summary>
        /// 将System.Diagnostics.Debug的日志转到界面
        /// .Net下几种日志管理方法（C# 将Diagnostics.Trace 调试输出 保存到txt）: https://blog.csdn.net/xwdpepsi/article/details/6605261
        /// </summary>
        private class InnerTraceListener : TraceListener
        {
            private Action<string> _writer;

            public InnerTraceListener(Action<string> writer)
            {
                _writer = writer;
            }

            public override void Write(string message)
            {
                _writer(message);
            }
            public override void WriteLine(string message)
            {
                Write(message);
                Write(Environment.NewLine);
            }
        }
    }
}
