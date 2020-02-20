using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在一棵无限的二叉树上，每个节点都有两个子节点，树中的节点 逐行 依次按 “之” 字形进行标记。

如下图所示，在奇数行（即，第一行、第三行、第五行……）中，按从左到右的顺序进行标记；

而偶数行（即，第二行、第四行、第六行……）中，按从右到左的顺序进行标记。



给你树上某一个节点的标号 label，请你返回从根节点到该标号为 label 节点的路径，该路径是由途经的节点标号所组成的。

 

示例 1：

输入：label = 14
输出：[1,3,4,14]
示例 2：

输入：label = 26
输出：[1,2,6,10,26]
 

提示：

1 <= label <= 10^6
*/
/// <summary>
/// https://leetcode-cn.com/problems/path-in-zigzag-labelled-binary-tree/
/// 1104. 二叉树寻路
/// 
/// </summary>
class PathInZigzagLabelledBinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> PathInZigZagTree(int label)
    {
        return default;
    }
}