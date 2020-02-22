﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
附近的家居城促销，你买回了一直心仪的可调节书架，打算把自己的书都整理到新的书架上。

你把要摆放的书 books 都整理好，叠成一摞：从上往下，第 i 本书的厚度为 books[i][0]，高度为 books[i][1]。

按顺序 将这些书摆放到总宽度为 shelf_width 的书架上。

先选几本书放在书架上（它们的厚度之和小于等于书架的宽度 shelf_width），然后再建一层书架。重复这个过程，直到把所有的书都放在书架上。

需要注意的是，在上述过程的每个步骤中，摆放书的顺序与你整理好的顺序相同。 例如，如果这里有 5 本书，那么可能的一种摆放情况是：第一和第二本书放在第一层书架上，第三本书放在第二层书架上，第四和第五本书放在最后一层书架上。

每一层所摆放的书的最大高度就是这一层书架的层高，书架整体的高度为各层高之和。

以这种方式布置书架，返回书架整体可能的最小高度。

示例：

输入：books = [[1,1],[2,3],[2,3],[1,1],[1,1],[1,1],[1,2]], shelf_width = 4
输出：6
解释：
3 层书架的高度和为 1 + 3 + 2 = 6 。
第 2 本书不必放在第一层书架上。

提示：

1 <= books.length <= 1000
1 <= books[i][0] <= shelf_width <= 1000
1 <= books[i][1] <= 1000
*/
/// <summary>
/// https://leetcode-cn.com/problems/filling-bookcase-shelves/
/// 1105. 填充书架
/// 
/// </summary>
class FillingBookcaseShelvesSolution
{
    public void Test()
    {
        var ret = MinHeightShelves(new int[][] { new int[] { 1, 1 },new int[] { 2, 3 },new int[] { 2, 3 },new int[] { 1, 1 },new int[] { 1, 1 },new int[] { 1, 1 },new int[] { 1, 2 } },4);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinHeightShelves(int[][] books, int shelf_width)
    {
        int n = books.Length;
        int[] dp = new int[n + 1];
        Array.Fill(dp, 1000000);
        dp[0] = 0;
        for (int i = 1; i <= n; i++) {
            int width = 0;
            int j = i;
            int maxHeight = 0;
            while (0 < j) {
                var book = books[j - 1];
                width += book[0];
                if (shelf_width < width) break;

                if (maxHeight < book[1]) maxHeight = book[1];

                int nextHeight = dp[j - 1] + maxHeight;
                if (nextHeight < dp[i]) dp[i] = nextHeight;
                j--;
            }
        }
        return dp[n];
    }
}
/*
动态规划 Python3
刘岳
发布于 8 个月前
3.7k 阅读
思路：
动态规划，用 dp[i] 表示放置前 i 本书所需要的书架最小高度，初始值 dp[0] = 0，其他为最大值 1000*1000。遍历每一本书，把当前这本书作为书架最后一层的最后一本书，将这本书之前的书向后调整，看看是否可以减少之前的书架高度。状态转移方程为 dp[i] = min(dp[i] , dp[j - 1] + h)，其中 j 表示最后一层所能容下书籍的索引，h 表示最后一层最大高度。

图解：
样例输入：books = [[1,1],[2,3],[2,3],[1,1],[1,1],[1,1],[1,2]], shelf_width = 4
最后求 dp[7]，省略了遍历步骤，直接给出结果。



代码：
class Solution:
    def minHeightShelves(self, books: List[List[int]], shelf_width: int) -> int:
        n = len(books)
        dp = [1000000] * (n + 1)
        dp[0] = 0
        for i in range(1, n + 1):
            tmp_width, j, h = 0, i, 0
            while j > 0:
                tmp_width += books[j - 1][0]
                if tmp_width > shelf_width:
                    break
                h = max(h, books[j - 1][1])
                dp[i] = min(dp[i], dp[j - 1] + h)
                j -= 1
        return dp[-1]
下一篇：1105. 填充书架-[c++][动态规划]
 
*/
