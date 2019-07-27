using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Moons.Common20
{
    /// <summary>
    /// 关于xml操作的实用工具类
    /// </summary>
    public static class XmlUtils
    {
        #region 读xml

        #region 查询节点

        //public static IList<XmlNode> GetNodes( XmlDocument doc, string path )
        //{
        //    return CollectionUtils.ToList<XmlNode>( doc.SelectNodes( path ) );
        //}

        public static IList<XmlNode> GetNodes( XmlNode node, string path )
        {
            return CollectionUtils.ToList<XmlNode>( node.SelectNodes( path ) );
        }

        //public static XmlNode GetSingleNode( XmlDocument doc, string path )
        //{
        //    return doc.SelectSingleNode( path );
        //}

        public static XmlNode GetSingleNode( XmlNode node, string path )
        {
            return node.SelectSingleNode( path );
        }

        #endregion

        #region 创建xml文档对象

        /// <summary>
        /// 判断字节数组是否是xml文档
        /// </summary>
        /// <param name="content">字节数组</param>
        /// <returns>字节数组是否是xml文档</returns>
        public static bool IsXml( byte[] content )
        {
            try
            {
                LoadXmlDocument( content );
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 从xml文件内容加载XmlDocument
        /// </summary>
        /// <param name="xmlContent">xml文件内容，字符串</param>
        /// <returns>XmlDocument</returns>
        public static XmlDocument LoadXmlDocumentByXmlContent( string xmlContent )
        {
            if( !string.IsNullOrEmpty( xmlContent ) )
            {
                var ret = new XmlDocument();
                ret.LoadXml( xmlContent );

                return ret;
            }
            return null;
        }

        /// <summary>
        /// 加载xml文件，文件不存在则返回null
        /// </summary>
        /// <param name="path">xml文件路径</param>
        /// <returns>XmlDocument</returns>
        public static XmlDocument LoadXmlDocument( string path )
        {
            if( File.Exists( path ) )
            {
                var ret = new XmlDocument();
                ret.Load( path );

                return ret;
            }
            return null;
        }

        /// <summary>
        /// 从字节数组加载XmlDocument
        /// </summary>
        /// <param name="content">字节数组</param>
        /// <returns>XmlDocument</returns>
        public static XmlDocument LoadXmlDocument( byte[] content )
        {
            if( content == null || content.Length == 0 )
            {
                return null;
            }

            var ret = new XmlDocument();
            using( var stream = new MemoryStream( content ) )
            {
                ret.Load( stream );
            }
            return ret;
        }

        #endregion

        #region 读内部的数值

        #region InnerText

        /// <summary>
        /// 读出内部的字符串，失败则返回null
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <returns>内部的字符串</returns>
        public static string InnerText( XmlNode xmlNode )
        {
            return xmlNode != null ? StringUtils.Trim( xmlNode.InnerText ) : null;
        }

        /// <summary>
        /// 读出内部的字符串，失败则返回null
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="xpath">xpath</param>
        /// <returns>内部的字符串</returns>
        public static string InnerText( XmlNode xmlNode, string xpath )
        {
            return InnerText( xmlNode.SelectSingleNode( xpath ) );
        }

        /// <summary>
        /// 读出内部的字符串数组，失败则返回null
        /// </summary>
        /// <param name="xmlNodes">XmlNode[]</param>
        /// <returns>内部的字符串数组</returns>
        public static string[] InnerTexts( IList<XmlNode> xmlNodes )
        {
            return CollectionUtils.IsNotEmptyG( xmlNodes )
                       ? CollectionUtils.ConvertAll( xmlNodes, xmlNode => InnerText( xmlNode ) ).ToArray()
                       : null;
        }

        /// <summary>
        /// 读出内部的字符串数组，失败则返回null
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="xpath">xpath</param>
        /// <returns>内部的字符串数组</returns>
        public static string[] InnerTexts( XmlNode xmlNode, string xpath )
        {
            return InnerTexts( GetNodes( xmlNode, xpath ) );
        }

        #endregion

        #region InnerBoolean

        /// <summary>
        /// 读出内部的Boolean，失败则返回false
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <returns>内部的Boolean</returns>
        public static bool InnerBoolean( XmlNode xmlNode )
        {
            return ConvertUtils.ToBoolean( InnerText( xmlNode ) );
        }

        /// <summary>
        /// 读出内部的Boolean，失败则返回false
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="xpath">xpath</param>
        /// <returns>内部的Boolean</returns>
        public static bool InnerBoolean( XmlNode xmlNode, string xpath )
        {
            return InnerBoolean( xmlNode.SelectSingleNode( xpath ) );
        }

        /// <summary>
        /// 读出内部的Boolean
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <returns>内部的Boolean</returns>
        public static bool? InnerNullBoolean( XmlNode xmlNode )
        {
            return ConvertUtils.ToNullBoolean( InnerText( xmlNode ) );
        }

        /// <summary>
        /// 读出内部的Boolean
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="xpath">xpath</param>
        /// <returns>内部的Boolean</returns>
        public static bool? InnerNullBoolean( XmlNode xmlNode, string xpath )
        {
            return InnerNullBoolean( xmlNode.SelectSingleNode( xpath ) );
        }

        #endregion

        #region InnerInt32

        /// <summary>
        /// 读出内部的int，失败则返回0
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <returns>内部的int</returns>
        public static int InnerInt32( XmlNode xmlNode )
        {
            return ConvertUtils.ToInt32( InnerText( xmlNode ) );
        }

        /// <summary>
        /// 读出内部的int，失败则返回0
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="xpath">xpath</param>
        /// <returns>内部的int</returns>
        public static int InnerInt32( XmlNode xmlNode, string xpath )
        {
            return InnerInt32( xmlNode.SelectSingleNode( xpath ) );
        }

        /// <summary>
        /// 读出内部的int
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <returns>内部的int</returns>
        public static int? InnerNullInt32( XmlNode xmlNode )
        {
            return ConvertUtils.ToNullInt32( InnerText( xmlNode ) );
        }

        /// <summary>
        /// 读出内部的int
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="xpath">xpath</param>
        /// <returns>内部的int</returns>
        public static int? InnerNullInt32( XmlNode xmlNode, string xpath )
        {
            return InnerNullInt32( xmlNode.SelectSingleNode( xpath ) );
        }

        #endregion

        #region InnerInt64

        /// <summary>
        /// 读出内部的long，失败则返回0
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <returns>内部的long</returns>
        public static long InnerInt64( XmlNode xmlNode )
        {
            return ConvertUtils.ToInt64( InnerText( xmlNode ) );
        }

        /// <summary>
        /// 读出内部的long，失败则返回0
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="xpath">xpath</param>
        /// <returns>内部的long</returns>
        public static long InnerInt64( XmlNode xmlNode, string xpath )
        {
            return InnerInt64( xmlNode.SelectSingleNode( xpath ) );
        }

        /// <summary>
        /// 读出内部的long
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <returns>内部的long</returns>
        public static long? InnerNullInt64( XmlNode xmlNode )
        {
            return ConvertUtils.ToNullInt64( InnerText( xmlNode ) );
        }

        /// <summary>
        /// 读出内部的long
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="xpath">xpath</param>
        /// <returns>内部的long</returns>
        public static long? InnerNullInt64( XmlNode xmlNode, string xpath )
        {
            return InnerNullInt64( xmlNode.SelectSingleNode( xpath ) );
        }

        #endregion

        #region InnerDouble

        /// <summary>
        /// 读出内部的double，失败则返回0
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <returns>内部的double</returns>
        public static double InnerDouble( XmlNode xmlNode )
        {
            return ConvertUtils.ToDouble( InnerText( xmlNode ) );
        }

        /// <summary>
        /// 读出内部的double，失败则返回0
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="xpath">xpath</param>
        /// <returns>内部的double</returns>
        public static double InnerDouble( XmlNode xmlNode, string xpath )
        {
            return InnerDouble( xmlNode.SelectSingleNode( xpath ) );
        }

        /// <summary>
        /// 读出内部的double?，失败则返回null
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <returns>内部的double?</returns>
        public static double? InnerNullDouble( XmlNode xmlNode )
        {
            return ConvertUtils.ToNullDouble( InnerText( xmlNode ) );
        }

        /// <summary>
        /// 读出内部的double?，失败则返回null
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="xpath">xpath</param>
        /// <returns>内部的double?</returns>
        public static double? InnerNullDouble( XmlNode xmlNode, string xpath )
        {
            return InnerNullDouble( xmlNode.SelectSingleNode( xpath ) );
        }

        #endregion

        #region InnerDateTime

        /// <summary>
        /// 读出内部的DateTime，失败则返回DateTime.MinValue
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <returns>内部的DateTime</returns>
        public static DateTime InnerDateTime( XmlNode xmlNode )
        {
            return ConvertUtils.ToDateTime( InnerText( xmlNode ) );
        }

        /// <summary>
        /// 读出内部的DateTime，失败则返回DateTime.MinValue
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="xpath">xpath</param>
        /// <returns>内部的DateTime</returns>
        public static DateTime InnerDateTime( XmlNode xmlNode, string xpath )
        {
            return InnerDateTime( xmlNode.SelectSingleNode( xpath ) );
        }

        /// <summary>
        /// 读出内部的DateTime
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <returns>内部的DateTime</returns>
        public static DateTime? InnerNullDateTime( XmlNode xmlNode )
        {
            return ConvertUtils.ToNullDateTime( InnerText( xmlNode ) );
        }

        /// <summary>
        /// 读出内部的DateTime
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="xpath">xpath</param>
        /// <returns>内部的DateTime</returns>
        public static DateTime? InnerNullDateTime( XmlNode xmlNode, string xpath )
        {
            return InnerNullDateTime( xmlNode.SelectSingleNode( xpath ) );
        }

        #endregion

        #endregion

        #endregion

        #region 写xml

        /// <summary>
        /// 以UTF8格式保存xml文件
        /// </summary>
        /// <param name="fileContent">文件内容，是一个xml文档</param>
        /// <param name="path">xml文件路径</param>
        public static void SaveXmlDocument_UTF8( string fileContent, string path )
        {
            var doc = new XmlDocument();
            doc.LoadXml( fileContent );

            SaveXmlDocument_UTF8( doc, path );
        }

        /// <summary>
        /// 以UTF8格式保存xml文件
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <param name="path">xml文件路径</param>
        public static void SaveXmlDocument_UTF8( XmlDocument doc, string path )
        {
            XmlDeclaration xmldecl = doc.CreateXmlDeclaration( "1.0", "utf-8", null );
            if( doc.FirstChild.NodeType == XmlNodeType.XmlDeclaration )
            {
                doc.ReplaceChild( xmldecl, doc.FirstChild );
            }
            else
            {
                doc.InsertBefore( xmldecl, doc.DocumentElement );
            }

            // 如果该文件存在，截断该文件并用新内容对其进行覆盖
            // 以 UTF-8 的形式写出该文件
            using( var writer = new XmlTextWriter( path, null ) )
            {
                // 自动缩进
                writer.Formatting = Formatting.Indented;

                doc.Save( writer );
            }
        }

        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <param name="parentNode">父节点</param>
        /// <param name="tag">节点Tag</param>
        /// <returns>子节点</returns>
        public static XmlElement CreateChild( XmlDocument doc, XmlNode parentNode, string tag )
        {
            XmlElement childElement = doc.CreateElement( tag );
            if( parentNode != null )
            {
                parentNode.AppendChild( childElement );
            }
            return childElement;
        }

        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <param name="parentNode">父节点</param>
        /// <param name="tag">节点Tag</param>
        /// <param name="value">值</param>
        /// <param name="attrNames">属性名数组</param>
        /// <param name="attrValues">属性值数组</param>
        /// <param name="setInnterText">true：设置内部文本，false：不设置，目的是保证有或没有标签</param>
        /// <returns>子节点</returns>
        public static XmlElement CreateChild( XmlDocument doc, XmlNode parentNode, string tag, object value,
                                              string[] attrNames, object[] attrValues, bool setInnterText )
        {
            string defaultValue = string.Empty;

            XmlElement node = CreateChild( doc, parentNode, tag );
            if( attrNames != null && attrValues != null && attrNames.Length == attrValues.Length )
            {
                for( int index = 0; index < attrNames.Length; index++ )
                {
                    XmlAttribute attr = doc.CreateAttribute( attrNames[index] );
                    attr.InnerText = ConvertUtils.ToString( attrValues[index], defaultValue );

                    // 如果节点为 XmlNodeType.Element 类型，则返回该节点的属性。否则，该属性将返回 null
// ReSharper disable PossibleNullReferenceException
                    node.Attributes.Append( attr );
// ReSharper restore PossibleNullReferenceException
                }
            }

            if( setInnterText )
            {
                node.InnerText = ConvertUtils.ToString( value, defaultValue );
            }

            return node;
        }

        /// <summary>
        /// 在节点下加入子节点
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <param name="parentNode">父节点</param>
        /// <param name="tag">节点Tag</param>
        /// <param name="text">节点内容</param>
        public static void AppendChild( XmlDocument doc, XmlElement parentNode, string tag, object text )
        {
            if( text != null )
            {
                CreateChild( doc, parentNode, tag ).InnerText = text.ToString();
            }
        }

        /// <summary>
        /// 在节点下加入子节点
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <param name="parentNode">父节点</param>
        /// <param name="tag">节点Tag</param>
        /// <param name="time">时间</param>
        public static void AppendChild( XmlDocument doc, XmlElement parentNode, string tag, DateTime time )
        {
            CreateChild( doc, parentNode, tag ).InnerText = time.ToString( "yyyy-MM-dd HH:mm:ss" );
        }

        /// <summary>
        /// 在节点下加入子节点
        /// </summary>
        /// <typeparam name="T">内容的类型</typeparam>
        /// <param name="doc">XmlDocument</param>
        /// <param name="parentNode">父节点</param>
        /// <param name="tag">节点Tag</param>
        /// <param name="value">节点内容</param>
        public static void AppendChild<T>( XmlDocument doc, XmlElement parentNode, string tag, T? value )
            where T : struct
        {
            if( value.HasValue )
            {
                AppendChild( doc, parentNode, tag, value.Value.ToString() );
            }
        }

        #endregion

        #region Xml Serializer 相关

        /// <summary>
        /// 创建XmlSerializer
        /// </summary>
        /// <typeparam name="T">需要xml序列化的类型</typeparam>
        /// <returns>T类型的XmlSerializer</returns>
        public static XmlSerializer CreateXmlSerializer<T>()
        {
            return new XmlSerializer( typeof(T) );
        }

        /// <summary>
        /// 将数据xml序列化到文件
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="data">数据</param>
        public static void XmlSerialize2File<T>( string path, T data )
        {
            using( var fs = new FileStream( path, FileMode.Create ) )
            {
                CreateXmlSerializer<T>().Serialize( fs, data );
            }
        }

        /// <summary>
        /// 将文件反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <returns>对象</returns>
        public static T XmlDeserializeFromFile<T>( string path ) where T : class
        {
            if( !File.Exists( path ) )
            {
                return null;
            }

            using( var fs = new FileStream( path, FileMode.Open ) )
            {
                return CreateXmlSerializer<T>().Deserialize( fs ) as T;
            }
        }

        /// <summary>
        /// 将数据xml序列化到xml字符串，UTF-16编码
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">数据</param>
        public static string XmlSerialize2Xml<T>( T data )
        {
            using( var writer = new StringWriter() )
            {
                CreateXmlSerializer<T>().Serialize( writer, data );

                return writer.ToString();
            }
        }

        /// <summary>
        /// 将xml字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xml">xml字符串</param>
        /// <returns>对象</returns>
        public static T XmlDeserializeFromXml<T>( string xml ) where T : class
        {
            if( string.IsNullOrEmpty( xml ) )
            {
                return null;
            }

            using( var reader = new StringReader( xml ) )
            {
                return CreateXmlSerializer<T>().Deserialize( reader ) as T;
            }
        }

        #endregion
    }
}