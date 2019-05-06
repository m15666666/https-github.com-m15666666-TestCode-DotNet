using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给出方程式 A / B = k, 其中 A 和 B 均为代表字符串的变量， k 是一个浮点型数字。根据已知方程式求解问题，并返回计算结果。如果结果不存在，则返回 -1.0。

示例 :
给定 a / b = 2.0, b / c = 3.0
问题: a / c = ?, b / a = ?, a / e = ?, a / a = ?, x / x = ? 
返回 [6.0, 0.5, -1.0, 1.0, -1.0 ]

输入为: vector<pair<string, string>> equations, vector<double>& values, vector<pair<string, string>> queries(方程式，方程式结果，问题方程式)， 其中 equations.size() == values.size()，即方程式的长度与方程式结果长度相等（程式与结果一一对应），并且结果值均为正数。以上为方程式的描述。 返回vector<double>类型。

基于上述例子，输入如下：

equations(方程式) = [ ["a", "b"], ["b", "c"] ],
values(方程式结果) = [2.0, 3.0],
queries(问题方程式) = [ ["a", "c"], ["b", "a"], ["a", "e"], ["a", "a"], ["x", "x"] ]. 
输入总是有效的。你可以假设除法运算中不会出现除数为0的情况，且不存在任何矛盾的结果。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/evaluate-division/
/// 399. 除法求值
/// https://blog.csdn.net/qq_23523409/article/details/84799489
/// </summary>
class EvaluateDivisionSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public double[] CalcEquation(string[][] equations, double[] values, string[][] queries)
    {
        if (equations == null || equations.Length == 0 || queries == null || queries.Length == 0) return new double[0];

        for( int i = 0; i < equations.Length; i++)
        {
            var equation = equations[i];
            var m = equation[0];
            var d = equation[1];

            if( !_map.ContainsKey(m)) _map[m] = new Dictionary<string, double>();
            _map[m][d] = values[i];
        }

        double[] ret = new double[queries.Length];
        for( int i = 0; i < ret.Length; i++)
        {
            var query = queries[i];
            var m = query[0];
            var d = query[1];
            if(m == d)
            {
                ret[i] = 1;
                continue;
            }
            ret[i] = Solve(m, d);
            _visited.Clear();
        }
        return ret;
    }

    private double Solve( string m, string d)
    {
        if (_map.ContainsKey(m) && _map[m].ContainsKey(d)) return _map[m][d];
        if (_map.ContainsKey(d) && _map[d].ContainsKey(m)) return 1 / _map[d][m];

        var paths = Find(m, d);
        if(paths.Count == 0) paths = Find(d, m);
        if (paths.Count == 0) return -1;

        double ret = 1;
        foreach (var path in paths)
            ret *= _map[path.Item1][path.Item2];

        return ret;
    }

    private List<Tuple<string,string>> Find(string m, string d)
    {
        _visited.Add(m);
        if (!_map.ContainsKey(m)) return new List<Tuple<string, string>>();

        var nexts = _map[m];
        if (nexts.ContainsKey(d)) return new List<Tuple<string, string>> { new Tuple<string, string>(m,d) };
        foreach( var key in nexts.Keys)
        {
            if (_visited.Contains(key)) continue;
            var paths = Find(key, d);
            if (paths.Count == 0) continue;

            paths.Insert(0, new Tuple<string, string>(m, key));
            return paths;
        }
        
        return new List<Tuple<string, string>>();
    }

    private Dictionary<string, Dictionary<string, double>> _map = new Dictionary<string, Dictionary<string, double>>();
    private HashSet<string> _visited = new HashSet<string>();
}