using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnalysisAlgorithm.Tests
{
    /// <summary>
    /// 数据输出辅助工具类
    /// </summary>
    public static class OutputUtils
    {
        /// <summary>
        /// 输出文件目录
        /// </summary>
        public static string OutputDir { get; set; } = @"C:\";

        /// <summary>
        ///     将复数数据数组输出到文本文件
        /// </summary>
        /// <param name="reArray">复数实部数据</param>
        /// <param name="imArray">复数虚部数据</param>
        /// <param name="filename">文本文件名</param>
        /// <param name="dir">输出目录</param>
        public static void ComplexToTxtFile(double[] reArray, double[] imArray, string filename, string dir = null)
        {
            Action<StreamWriter> handler = writer => {
                for (int i = 0; i < reArray.Length; i++)
                    writer.WriteLine(GetComplexString(reArray[i], imArray[i]));
            };
            ToTxtFile(handler, filename, dir);
        }

        /// <summary>
        ///     将数据输出到文本文件
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="filename">文本文件名</param>
        /// <param name="dir">输出目录</param>
        public static void ToTxtFile(IEnumerable<double> data, string filename, string dir = null)
        {
            dir = dir ?? OutputDir;
            using (StreamWriter writer = File.CreateText(Path.Combine(dir, filename)))
            {
                foreach (double v in data)
                {
                    writer.WriteLine(v);
                }
            }
        }

        /// <summary>
        ///     将数据输出到文本文件
        /// </summary>
        /// <param name="handler">写数据的函数</param>
        /// <param name="data">数据</param>
        /// <param name="filename">文本文件名</param>
        /// <param name="dir">输出目录</param>
        public static void ToTxtFile( Action<StreamWriter> handler, string filename, string dir = null)
        {
            dir = dir ?? OutputDir;
            using (StreamWriter writer = File.CreateText(Path.Combine(dir, filename)))
            {
                handler(writer);
            }
        }

        /// <summary>
        /// 获得复数描述字符串
        /// </summary>
        /// <param name="real">复数实部</param>
        /// <param name="imag">复数虚部</param>
        /// <returns>复数描述字符串</returns>
        public static string GetComplexString( double real, double imag)
        {
            var sign = "+";
            if (imag < 0)
            {
                sign = "-";
                imag = -imag;
            }

            if (imag <= double.Epsilon) return $"{real:0.###}";
            return $"{real:0.###} {sign} {imag:0.###}i";
        }

        /// <summary>
        /// 创建数组，并初始化值
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="length">数组长度</param>
        /// <param name="initValue">数组默认值</param>
        /// <returns>数组</returns>
        public static T[] CreateArray<T>( int length, T? initValue) where T : struct
        {
            T[] ret = new T[length];
            if (initValue.HasValue)
            {
                T value = initValue.Value;
                for (int i = 0; i < length; i++)
                    ret[i] = value;
            }
            return ret;
        }
    }
}
