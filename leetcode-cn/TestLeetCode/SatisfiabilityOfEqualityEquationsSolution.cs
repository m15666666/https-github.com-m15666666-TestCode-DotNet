using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个由表示变量之间关系的字符串方程组成的数组，每个字符串方程 equations[i] 的长度为 4，并采用两种不同的形式之一："a==b" 或 "a!=b"。在这里，a 和 b 是小写字母（不一定不同），表示单字母变量名。

只有当可以将整数分配给变量名，以便满足所有给定的方程时才返回 true，否则返回 false。 

 

示例 1：

输入：["a==b","b!=a"]
输出：false
解释：如果我们指定，a = 1 且 b = 1，那么可以满足第一个方程，但无法满足第二个方程。没有办法分配变量同时满足这两个方程。
示例 2：

输出：["b==a","a==b"]
输入：true
解释：我们可以指定 a = 1 且 b = 1 以满足满足这两个方程。
示例 3：

输入：["a==b","b==c","a==c"]
输出：true
示例 4：

输入：["a==b","b!=c","c==a"]
输出：false
示例 5：

输入：["c==c","b==d","x!=z"]
输出：true
 

提示：

1 <= equations.length <= 500
equations[i].length == 4
equations[i][0] 和 equations[i][3] 是小写字母
equations[i][1] 要么是 '='，要么是 '!'
equations[i][2] 是 '='
*/
/// <summary>
/// https://leetcode-cn.com/problems/satisfiability-of-equality-equations/
/// 990. 等式方程的可满足性
/// 
/// </summary>
class SatisfiabilityOfEqualityEquationsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool EquationsPossible(string[] equations)
    {
        const char a = 'a';
        const int AlphaCount = 26;
        var neighbors = new List<int>[AlphaCount];
        for (int i = 0; i < AlphaCount; ++i)
            neighbors[i] = new List<int>();

        List<(int, int)> notEqualVariables = new List<(int, int)>();
        foreach (var eqn in equations)
        {
            int x = eqn[0] - a;
            int y = eqn[3] - a;
            if (eqn[1] == '=')
            {
                neighbors[x].Add(y);
                neighbors[y].Add(x);
            }
            else
            {
                if (x == y) return false;
                notEqualVariables.Add((x, y));
            }
        }

        int[] colors = new int[AlphaCount];
        Array.Fill(colors, 0);
        int colorNumber = 0;
        Stack<int> stack = new Stack<int>();
        for (int start = 0; start < AlphaCount; ++start)
        {
            if (colors[start] == 0)
            {
                colorNumber++;
                colors[start] = colorNumber;
                
                stack.Push(start);
                while (0 < stack.Count)
                {
                    int node = stack.Pop();
                    foreach (int neighbor in neighbors[node])
                    {
                        if (colors[neighbor] == 0)
                        {
                            colors[neighbor] = colorNumber;
                            stack.Push(neighbor);
                        }
                    }
                }
            }
        }

        return !notEqualVariables.Any(item => colors[item.Item1] == colors[item.Item2]);
        //foreach (var (x, y) in notEqualVariables)
        //{
        //    if (colors[x] == colors[y]) return false;
        //}

        //return true;
    }
}
/*
等式方程的可满足性
力扣 (LeetCode)
发布于 1 年前
2.6k 阅读
方法：联通分量
思路

所有相互等于的变量能组成一个联通分量。举一个例子，如果 a=b, b=c, c=d，那么 a, b, c, d 就在同一个联通分量中，因为它们必须相等。

算法

第一步，我们基于给定的等式，用深度优先遍历将每一个变量按照联通分量染色。

将联通分量染色之后，我们分析形如 a != b 的不等式。如果两个分量有相同的颜色，那么它们一定相等，因此如果说它们不相等的话，就一定无法满足给定的方程组。

否则，我们的染色就可以看作是一组能满足方程组约束的方案，所以结果是 true。

class Solution {
    public boolean equationsPossible(String[] equations) {
        ArrayList<Integer>[] graph = new ArrayList[26];
        for (int i = 0; i < 26; ++i)
            graph[i] = new ArrayList();

        for (String eqn: equations) {
            if (eqn.charAt(1) == '=') {
                int x = eqn.charAt(0) - 'a';
                int y = eqn.charAt(3) - 'a';
                graph[x].add(y);
                graph[y].add(x);
            }
        }

        int[] color = new int[26];
        int t = 0;
        for (int start = 0; start < 26; ++start) {
            if (color[start] == 0) {
                t++;
                Stack<Integer> stack = new Stack();
                stack.Push(start);
                while (!stack.isEmpty()) {
                    int node = stack.pop();
                    for (int nei: graph[node]) {
                        if (color[nei] == 0) {
                            color[nei] = t;
                            stack.Push(nei);
                        }
                    }
                }
            }
        }

        for (String eqn: equations) {
            if (eqn.charAt(1) == '!') {
                int x = eqn.charAt(0) - 'a';
                int y = eqn.charAt(3) - 'a';
                if (x == y || color[x] != 0 && color[x] == color[y])
                    return false;
            }
        }

        return true;
    }
}
复杂度分析

时间复杂度： O(N)O(N)，其中 NN 是方程组 equations 的数量。

空间复杂度： O(1)O(1)，认为字母表的大小是 O(1)O(1) 的。 

public class Solution {
    int[] uf;
    public bool EquationsPossible(string[] equations) {
        uf = new int[26];
        for(int i = 0;i<uf.Length;i++)
            uf[i] = i;
        
        foreach(string eq in equations){
            if(eq[1] == '='){
                uf[find(eq[0]-'a')] = find(eq[3]-'a');
            }
        }
        foreach(string eq in equations){
            if(eq[1] == '!' && find(eq[0]-'a') == find(eq[3]-'a'))
                return false;
                
            
        }
        
        return true;
    }
    
    public int find(int x){
        if(x != uf[x])
            uf[x] = find(uf[x]);
        
        return uf[x];
    }
}

public class Solution {
       public bool EquationsPossible(string[] equations)
        {
            Dictionary<char, Value> temple = new Dictionary<char, Value>();

            for (int i = 0; i < equations.Length; i++)
            {
                if (equations[i][1] == '=')
                {
                    bool ContainA = temple.ContainsKey(equations[i][0]);
                    bool ContainB = temple.ContainsKey(equations[i][3]);

                    ///检查是否有这两个键
                    if (ContainA || ContainB)
                    {
                        ///如果两个都有，统一值
                        if (ContainA && ContainB)
                        {
                            temple[equations[i][3]].number = temple[equations[i][0]].number;
                        }
                        else if (ContainA)///如果只有一个，新建另一个赋值已存在的值
                        {
                            temple.Add(equations[i][3], temple[equations[i][0]]);
                        }
                        else
                        {
                            temple.Add(equations[i][0], temple[equations[i][3]]);
                        }
                    }
                    else
                    {
                        Value templeValue = new Value();
                        if(equations[i][0]!= equations[i][3])
                        {
                            temple.Add(equations[i][0], templeValue);
                            temple.Add(equations[i][3], templeValue);
                        }
                        else
                        {
                               temple.Add(equations[i][0], templeValue);
                        }
                  
                    }
                }


            }

            for (int i = 0; i < equations.Length; i++)
            {
                if (equations[i][1] == '!')
                {
                    bool ContainA = temple.ContainsKey(equations[i][0]);
                    bool ContainB = temple.ContainsKey(equations[i][3]);
                    ///检查是否有这两个键
                    if (ContainA || ContainB)
                    {
                        ///如果两个都有，检查值
                        if (ContainA && ContainB)
                        {
                            if ((temple[equations[i][3]].number == temple[equations[i][0]].number))
                            {
                                return false;
                            }
                        }
                        else if (ContainA)///如果只有一个，新建另一个赋值已存在的值
                        {
                            temple.Add(equations[i][3], new Value());
                        }
                        else
                        {
                            temple.Add(equations[i][0], new Value());
                        }
                    }
                    else
                    {
                        if (equations[i][0] == equations[i][3])
                        {
                            return false;
                        }
                        Value templeValueA = new Value();
                        Value templeValueB = new Value();
                        temple.Add(equations[i][0], templeValueA);
                        temple.Add(equations[i][3], templeValueB);
                    }
                }
            }

            for (int i = 0; i < equations.Length; i++)
            {
                if (equations[i][1] == '!')
                {
                    if ((temple[equations[i][3]].number == temple[equations[i][0]].number))
                    {
                        return false;
                    }
                }
            }
            return true;


        }
        class Value
        {
            static int i = 0;
            public Value()
            {
                i++;
                number = i;
            }
            public int number;
        }
}
*/
