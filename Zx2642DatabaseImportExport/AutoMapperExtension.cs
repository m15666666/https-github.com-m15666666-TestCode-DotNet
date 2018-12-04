using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zx2642DatabaseImportExport
{
    /// <summary>
    /// 
    /// From:http://www.cnblogs.com/czly/p/9202851.html
    /// https://automapper.readthedocs.io/en/latest/Custom-type-converters.html
    /// https://blog.csdn.net/shuizhaoshui/article/details/51425527
    /// C#用反射判断一个类型是否是Nullable同时获取它的根类型: https://blog.csdn.net/apollokk/article/details/76708225
    /// </summary>
    public static class AutoMapperExtension
    {
        /// <summary>
        /// 临时文件目录
        /// </summary>
        public static string TempDir { get; set; } = "temp";

        ///// <summary>
        ///// 实体对象转换
        ///// </summary>
        ///// <typeparam name="TDestination"></typeparam>
        ///// <param name="o"></param>
        ///// <returns></returns>
        //public static TDestination MapTo<TDestination>(this object o)
        //{
        //    if (o == null)
        //        throw new ArgumentNullException();

        //    Mapper.CreateMap(o.GetType(), typeof(TDestination));

        //    return Mapper.Map<TDestination>(o); ;
        //}

        ///// <summary>
        ///// 集合转换
        ///// </summary>
        ///// <typeparam name="TDestination"></typeparam>
        ///// <param name="o"></param>
        ///// <returns></returns>
        //public static List<TDestination> MapTo<TDestination>(this IEnumerable o)
        //{
        //    if (o == null)
        //        throw new ArgumentNullException();


        //    foreach (var item in o)
        //    {
        //        Mapper.CreateMap(item.GetType(), typeof(TDestination));

        //        break;
        //    }
        //    return Mapper.Map<List<TDestination>>(o);
        //}

        ///// <summary>  
        ///// 将 DataTable 转为实体对象  
        ///// </summary>  
        ///// <typeparam name="T"></typeparam>  
        ///// <param name="dt"></param>  
        ///// <returns></returns>  
        //public static List<T> MapTo<T>( this DataTable dt )
        //{
        //    if (dt == null || dt.Rows.Count == 0)
        //        return default(List<T>);

        //    //Mapper.CreateMap<IDataReader, T>();
        //    return Mapper.Map<List<T>>(dt.CreateDataReader());
        //}

        /// <summary>
        /// DataTable转化为List集合
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="dt">datatable表</param>
        /// <returns>返回list集合</returns>
        public static List<T> MapTo<T>(this DataTable dt) where T : new()
        {
            List<T> list = new List<T>();
            Type type = typeof(T);
            //List<string> listColums = new List<string>();
            PropertyInfo[] pArray = type.GetProperties().Where(p => dt.Columns.Contains(p.Name)).ToArray(); //集合属性数组
            foreach (DataRow row in dt.Rows)
            {
                //T entity = Activator.CreateInstance<T>(); //新建对象实例 
                T entity = new T(); //新建对象实例 
                foreach (PropertyInfo p in pArray)
                {
                    var v = row[p.Name];
                    var propertyType = p.PropertyType;
                    if (v == null || v == DBNull.Value || (propertyType != typeof(string) && v is string && string.IsNullOrWhiteSpace(v as string)))
                    {
                        continue;
                    }
                    //if ( p.PropertyType == typeof(DateTime) && Convert.ToDateTime(v) < Convert.ToDateTime("1753-01-01"))
                    //{
                    //    continue;
                    //}
                    try
                    {
                        object pValue = null;
                        if(propertyType == typeof(byte[]))
                        {
                            var filePath = Path.Combine(TempDir, v.ToString());
                            if (File.Exists(filePath))
                            {
                                pValue = File.ReadAllBytes(filePath);
                            }
                            //pValue = Convert.FromBase64String(v.ToString());
                        }
                        else
                        {
                            var targetType = propertyType.IsNullable() ? propertyType.GetNullableUnderlyingType() : propertyType;
                            pValue = Convert.ChangeType(v, targetType);//类型强转，将table字段类型转为集合字段类型
                        }
                        
                        p.SetValue(entity, pValue, null);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine($"DataTable.MapTo<{type}>,{v}=>{p.Name}({p.PropertyType}) error.{ex}");
                        // throw;
                    }
                    //if (v.GetType() == p.PropertyType)
                    //{
                    //    p.SetValue(entity, v, null); //如果不考虑类型异常，foreach下面只要这一句就行
                    //}                    
                    //object obj = null;
                    //if (ConvertType(v, p.PropertyType,isStoreDB, out obj))
                    //{                                        
                    //    p.SetValue(entity, obj, null);
                    //}                
                }
                list.Add(entity);
            } // foreach (DataRow row in dt.Rows)
            return list;
        }

        /// <summary>
        /// 将List转换为Datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToTable<T>(this IEnumerable<T> list)
        {
            return list.MapToTable<T>();
        }

        /// <summary>
        /// 将List转换为Datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable MapToTable<T>(this IEnumerable list)
        {
            if (list == null)
                return default(DataTable);

            //创建属性的集合
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口
            System.Type type = typeof(T);
            DataTable dt = new DataTable();
            dt.TableName = type.Name;
            //把所有的public属性加入到集合 并添加DataTable的列
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => {
                pList.Add(p);

                var propertyType = p.PropertyType;
                Type columnType = propertyType;
                if(propertyType == typeof(byte[]))
                {
                    //将字节数组base64编码，以字符串存储
                   columnType = typeof(string);
                }
                else
                {
                    // We need to check whether the property is NULLABLE
                    if (propertyType.IsNullable())
                    {
                        // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                        columnType = propertyType.GetNullableUnderlyingType();
                    }
                }
                
                dt.Columns.Add(p.Name, columnType);
            });
            foreach (var item in list)
            {
                //创建一个DataRow实例
                DataRow row = dt.NewRow();
                //给row 赋值
                pList.ForEach(p => {
                    var v = p.GetValue(item, null);
                    if (v != null && v != DBNull.Value) {
                        if (v is byte[]) {
                            var fileName = Guid.NewGuid().ToString("N") + ".bin";
                            var filePath = Path.Combine(TempDir, fileName);
                            File.WriteAllBytes(filePath, (byte[])v);
                            v = fileName;
                        }
                        row[p.Name] = v;
                    }
                }
                );
                //加入到DataTable
                dt.Rows.Add(row);
            }
            return dt;
        }

        #region 可空类型:Nullable<int>,int? 处理

        /// <summary>
        /// 属性是否为可空类型:Nullable<int>,int? 等
        ///  check whether the property is NULLABLE: if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
        /// </summary>
        /// <param name="type"></param>
        /// <returns>true:可空类型</returns>
        private static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 获取可空类型中具体的类型，例如：Nullable<int>,int? 中的int。具体方式与取得泛型的具体类型一样
        /// If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Type GetNullableUnderlyingType( this Type type ) { return type.GetGenericArguments()[0]; }

        #endregion
    }
}
