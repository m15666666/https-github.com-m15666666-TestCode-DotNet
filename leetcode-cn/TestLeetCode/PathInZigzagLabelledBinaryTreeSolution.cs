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
        List<int> ret = new List<int>();
        label = GetBefore(label);
        while (0 < label)
        {
            ret.Add(label);
            label /= 2;
        }

        ret.Reverse();
        for (int i = 0; i < ret.Count; i++)
            ret[i] = GetBefore(ret[i]);
        return ret;

    }
    private static int GetBefore(int label)
    {
        int powCount = 1;
        long p = 1;
        while (p <= label)
        {
            p *= 2;
            powCount++;
        }
        powCount--;
        if (powCount % 2 == 1) return label;

        int start = (int)(p / 2);
        return (int)(p - 1 - label + start);
    }
}
/*
label转换函数与寻根 双百算法
满天都是小欣欣
发布于 2 个月前
263 阅读
首做先，假设我们数是正常得满二叉树编码方式，根节点index是1，那假设父节点index是n，那左子树节点是2 * n， 右子树 2 * n + 1
在这个数组上找根节点的路径非常简单。

写一个转换函数，可以把正常上述的idnex 转换成题目中的index， 反之也可以将题目中的index转换成正常得index。

算法流程
1 首先将label转换成正常索引下的label
2 寻找在正常索引下的label到根的路径
3 反转路径
4 对路径中的每个索引转换成题目中的索引方法。

class Solution {
public:
    vector<int> pathInZigZagTree(int label) {
        vector<int> res;
        cout << label;
        label = getBefore(label);
        // 寻找根节点路径
        while(label >= 1){
            res.push_back(label);
            label /= 2;
        }
        reverse(res.begin(), res.end());
        for(int i = 0; i < res.size(); i ++){
            res[i] = getBefore(res[i]);
        }
        return res;
        
    }
    int getBefore(int label){
        int cnt = 1;
        while(pow(2, cnt - 1) <= label)
            cnt++;
        cnt --;
        if(cnt % 2 == 1)
            return label;
        else{
            int start = pow(2, cnt - 1);
            return pow(2, cnt) - 1 - label + start;
        }
    }
};
下一篇：简单递归，从最后一步开始向后退

public class Solution {
    public IList<int> PathInZigZagTree(int label) {
        int rank = (int)Math.Log(label, 2) + 1;
            int normalLabel = TransformLabel(rank, label);
            List<int> path = new List<int>();
            while(normalLabel != 0)
            {
                path.Add(TransformLabel(rank, normalLabel));
                normalLabel /= 2;
                rank--;
            }
            path.Reverse();
            return path;
    }

    public int TransformLabel(int rank,int n)
        {
            if (rank % 2 == 0)
                return n;
            else
            {
                int temp =(int)Math.Pow(2, rank - 1);
                return temp*3-n-1;
            }
                
        }
} 
*/
