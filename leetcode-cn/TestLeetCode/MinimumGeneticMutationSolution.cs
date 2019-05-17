using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
一条基因序列由一个带有8个字符的字符串表示，其中每个字符都属于 "A", "C", "G", "T"中的任意一个。

假设我们要调查一个基因序列的变化。一次基因变化意味着这个基因序列中的一个字符发生了变化。

例如，基因序列由"AACCGGTT" 变化至 "AACCGGTA" 即发生了一次基因变化。

与此同时，每一次基因变化的结果，都需要是一个合法的基因串，即该结果属于一个基因库。

现在给定3个参数 — start, end, bank，分别代表起始基因序列，目标基因序列及基因库，请找出能够使起始基因序列变化为目标基因序列所需的最少变化次数。如果无法实现目标变化，请返回 -1。

注意:

起始基因序列默认是合法的，但是它并不一定会出现在基因库中。
所有的目标基因序列必须是合法的。
假定起始基因序列与目标基因序列是不一样的。
示例 1:

start: "AACCGGTT"
end:   "AACCGGTA"
bank: ["AACCGGTA"]

返回值: 1
示例 2:

start: "AACCGGTT"
end:   "AAACGGTA"
bank: ["AACCGGTA", "AACCGCTA", "AAACGGTA"]

返回值: 2
示例 3:

start: "AAAAACCC"
end:   "AACCCCCC"
bank: ["AAAACCCC", "AAACCCCC", "AACCCCCC"]

返回值: 3 
*/
/// <summary>
/// https://leetcode-cn.com/problems/minimum-genetic-mutation/
/// 433. 最小基因变化
/// https://blog.csdn.net/zrh_CSDN/article/details/84142284
/// </summary>
class MinimumGeneticMutationSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinMutation(string start, string end, string[] bank)
    {
        if (bank == null || bank.Length == 0) return -1;

        HashSet<int> hashcodeBank = new HashSet<int>();
        foreach (var g in bank)
            hashcodeBank.Add(GetGeneHashCode(g));

        HashSet<int> visited = new HashSet<int>();
        
        int level = 0;
        int target = GetGeneHashCode(end);
        visited.Add(GetGeneHashCode(start));

        Queue<char[]> queue = new Queue<char[]>();
        queue.Enqueue(start.ToCharArray());
        while (0 < queue.Count )
        {
            int len = queue.Count;
            for (int i = 0; i < len; ++i)
            {
                var previous = queue.Dequeue();
                if (GetGeneHashCode(previous) == target) return level;
                for (int j = 0; j < previous.Length; ++j)
                {
                    char old = previous[j];
                    foreach (char c in Genes)
                    {
                        if (old == c) continue;
                        char[] next = (char[])previous.Clone();
                        next[j] = c;

                        var code = GetGeneHashCode(next);
                        if (hashcodeBank.Contains(code) && !visited.Contains(code))
                        {
                            visited.Add(code);
                            queue.Enqueue(next);
                        }
                    }
                }
            }
            ++level;
        }
        return -1;
    }

    private static char[] Genes = new char[] { 'A', 'C', 'G', 'T' };

    /// <summary>
    /// 获得基因hashcode，基因长度为8.
    /// </summary>
    /// <param name="gene"></param>
    /// <returns></returns>
    private static int GetGeneHashCode( IEnumerable<char> gene )
    {
        int ret = 0;
        foreach( var c in gene)
        {
            ret <<= 2;
            switch (c)
            {
                case 'A':
                    ret += 0;
                    break;

                case 'C':
                    ret += 1;
                    break;

                case 'G':
                    ret += 2;
                    break;

                case 'T':
                    ret += 3;
                    break;
            }
        }
        return ret;
    }
}