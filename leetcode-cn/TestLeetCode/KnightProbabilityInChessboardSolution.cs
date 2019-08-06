using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
已知一个 NxN 的国际象棋棋盘，棋盘的行号和列号都是从 0 开始。即最左上角的格子记为 (0, 0)，最右下角的记为 (N-1, N-1)。 

现有一个 “马”（也译作 “骑士”）位于 (r, c) ，并打算进行 K 次移动。 

如下图所示，国际象棋的 “马” 每一步先沿水平或垂直方向移动 2 个格子，然后向与之相垂直的方向再移动 1 个格子，共有 8 个可选的位置。

 



 

现在 “马” 每一步都从可选的位置（包括棋盘外部的）中独立随机地选择一个进行移动，直到移动了 K 次或跳到了棋盘外面。

求移动结束后，“马” 仍留在棋盘上的概率。

 

示例：

输入: 3, 2, 0, 0
输出: 0.0625
解释: 
输入的数据依次为 N, K, r, c
第 1 步时，有且只有 2 种走法令 “马” 可以留在棋盘上（跳到（1,2）或（2,1））。对于以上的两种情况，各自在第2步均有且只有2种走法令 “马” 仍然留在棋盘上。
所以 “马” 在结束后仍在棋盘上的概率为 0.0625。
 

注意：

N 的取值范围为 [1, 25]
K 的取值范围为 [0, 100]
开始时，“马” 总是位于棋盘上 
*/
/// <summary>
/// https://leetcode-cn.com/problems/knight-probability-in-chessboard/
/// 688. “马”在棋盘上的概率
/// https://blog.csdn.net/xx_123_1_rj/article/details/81149267
/// </summary>
class KnightProbabilityInChessboardSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public double KnightProbability(int N, int K, int r, int c)
    {
        Dictionary<int, double> coordination2Probability = new Dictionary<int, double>();
        Dictionary<int, double> coordination2Probability2 = new Dictionary<int, double>();

        coordination2Probability.Add(ToKey(r, c), 1);
        for ( int i = 0; i < K; i++)
        {
            if (coordination2Probability.Count == 0) break;
            coordination2Probability2.Clear();

            foreach ( var pair in coordination2Probability )
            {
                double probability = pair.Value / 8f;// 保留棋盘内的概率（除以8，是因为有八个方向，每个方向是八分之一）
                KeyToRC(pair.Key, ref r, ref c);
                for (int j = 0; j < Drs.Length; j++)
                {
                    var newR = r + Drs[j];
                    var newC = c + Dcs[j];
                    if (0 <= newR && newR < N && 0 <= newC && newC < N)// 判断是否出界
                    {
                        int newKey = ToKey(newR, newC);
                        if (!coordination2Probability2.ContainsKey(newKey)) coordination2Probability2.Add(newKey, probability);
                        else coordination2Probability2[newKey] += probability;  
                    }
                }
            }
            var c2p = coordination2Probability;
            coordination2Probability = coordination2Probability2;
            coordination2Probability2 = c2p;
        }
        return coordination2Probability.Values.Sum();
    }

    private static int ToKey(int r, int c)
    {
        return r * 100 + c;
    }
    private static void KeyToRC(int key, ref int r, ref int c)
    {
        c = key % 100;
        r = key / 100;
    }

    private static readonly int[] Drs = new int[] { 2, 2, -2, -2, 1, 1, -1, -1 };
    private static readonly int[] Dcs = new int[] { 1, -1, 1, -1, 2, -2, 2, -2 };
}