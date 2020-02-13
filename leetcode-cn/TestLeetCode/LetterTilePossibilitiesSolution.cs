using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
你有一套活字字模 tiles，其中每个字模上都刻有一个字母 tiles[i]。返回你可以印出的非空字母序列的数目。

 

示例 1：

输入："AAB"
输出：8
解释：可能的序列为 "A", "B", "AA", "AB", "BA", "AAB", "ABA", "BAA"。
示例 2：

输入："AAABBC"
输出：188
 

提示：

1 <= tiles.length <= 7
tiles 由大写英文字母组成
*/
/// <summary>
/// https://leetcode-cn.com/problems/letter-tile-possibilities/
/// 1079. 活字印刷
/// 
/// </summary>
class LetterTilePossibilitiesSolution
{
    public void Test()
    {
        var ret = NumTilePossibilities("AAB");
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NumTilePossibilities(string tiles)
    {
        const char a = 'A';
        int[] counter = new int[26];
        Array.Fill(counter, 0);

        foreach( var c in tiles ) counter[c - a]++;

        int Dfs()
        {
            int combinationCount = 0;
            for( int i = 0; i < 26; i++)
            {
                if (counter[i] == 0) continue;
                combinationCount++;

                counter[i]--;

                combinationCount += Dfs();

                counter[i]++;
            }
            return combinationCount;
        }

        return Dfs();
    }
}
/*
回溯算法（Python 代码）
liweiwei1419
发布于 8 个月前
3.3k 阅读
思路分析：

这道题与 LeetCode 第 90 题：子集 II很像，把 LeetCode 第 90 题的每一个解变成排列，就是这道题了。

回溯算法：

由于是排列，我们不难想到，解决这个问题的思路应该是一个树形结构。不妨先从规模小的问题入手，以题目示例 1 的输入：“AAB” 为例，可以画出树形图如下。

（温馨提示：下面的幻灯片中，有几页上有较多的文字，可能需要您停留一下，可以点击右下角的后退 “|◀” 或者前进 “▶|” 按钮控制幻灯片的播放。）



我们只要一开始做一个字母频次统计，如果当前这个字母的频次为 00，就不再往下执行，马上要回溯了，在回溯的过程中一定要记得状态重置。

参考代码：

class Solution:

    def numTilePossibilities(self, tiles: str) -> int:
        counter = [0] * 26
        for alpha in tiles:
            counter[ord(alpha) - ord('A')] += 1
        return self.__dfs(counter)

    def __dfs(self, counter):
        res = 0
        for i in range(26):
            if counter[i] == 0:
                continue
            res += 1
            counter[i] -= 1

            res += self.__dfs(counter)
            counter[i] += 1
        return res

public class Solution {
    
	int[] nums = new int[98];
	HashSet<char> hs = new HashSet<char>();
	public int NumTilePossibilities(string tiles)
	{
		int length = tiles.Length;
		for (int i = 0; i < tiles.Length; i++)
		{
			hs.Add(tiles[i]);
			nums[tiles[i]]++;
		}

		 Dfs(0);
		return count;
	}

	int count = 0;
	private void Dfs(int v)
	{
		if(v!=0){
			count++;
		}
		
		foreach (var item in hs)
		{
			 if (nums[item] > 0)
			{
				nums[item]--;
				Dfs(v + 1);
				nums[item]++;
			}
		}
	}
}
 
*/
