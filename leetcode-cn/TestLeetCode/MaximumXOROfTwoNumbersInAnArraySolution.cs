using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个非空数组，数组中元素为 a0, a1, a2, … , an-1，其中 0 ≤ ai < 231 。

找到 ai 和aj 最大的异或 (XOR) 运算结果，其中0 ≤ i,  j < n 。

你能在O(n)的时间解决这个问题吗？

示例:

输入: [3, 10, 5, 25, 2, 8]

输出: 28

解释: 最大的结果是 5 ^ 25 = 28. 
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-xor-of-two-numbers-in-an-array/
/// 421. 数组中两个数的最大异或值
/// https://www.cnblogs.com/kexinxin/p/10242217.html
/// </summary>
class MaximumXOROfTwoNumbersInAnArraySolution
{
    public void Test()
    {
        var ret = FindMaximumXOR(new int[] { 3, 10, 5, 25, 2, 8 });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindMaximumXOR(int[] nums)
    {
        int max = 0;
        int mask = 0;
        for (int i = 31; i >= 0; i--)
        {
            // 从最高位试着找nums的前缀
            mask = mask | (1 << i);
            HashSet<int> set = new HashSet<int>();
            foreach (int num in nums)
                if (!set.Contains(num)) set.Add(mask & num);

            //判断最大异或结果的当前位是否为1
            int nextMax = max | (1 << i);
            foreach (int prefix in set)
            {
                // 包含亦或互补的两个数
                if (set.Contains(prefix ^ nextMax))
                {
                    max = nextMax;
                    break;
                }
            }
        }
        return max;
    }
}
/*
public class Solution
{
    private int ret = 0;
    public class TrieNode
    {
        public TrieNode[] son;
        public int val;
        public TrieNode(int val)
        {
            son = new TrieNode[2];
            this.val = val;
        }
    }
    public int FindMaximumXOR(int[] nums)
    {
        TrieNode root = new TrieNode(-1);
        foreach(var n in nums)
        {
            TrieNode tnode = root;
            for(int i=31;i>=0;i--)
            {
                int val = ((n & (1 << i)) != 0) ? 1 : 0;
                if (tnode.son[val]==null)
                {
                    tnode.son[val] = new TrieNode(val);
                }
                else
                {
                }
                tnode = tnode.son[val];
            }
        }
        TrieNode node = root;
        int count = 32;
        while(true)
        {
            if(node.son[0]==null && node.son[1]==null)
            {
                return 0;
            }
            if(node.son[0] != null && node.son[1] != null)
            {
                break;
            }
            if(node.son[0]!=null)
            {
                node = node.son[0];
            }
            else if(node.son[1]!=null)
            {
                node = node.son[1];
            }
            count -= 1;
        }
        TrieNode node0 = node.son[0];
        TrieNode node1 = node.son[1];
        func(node0, node1, 0);
        return ret;
    }
    public void func(TrieNode node0,TrieNode node1,int k)
    {
        k = k * 2 + (node0.val ^ node1.val);
        if(node0.son[0] != null && node0.son[1] != null && node1.son[0] != null && node1.son[1] != null)
        {
            func(node0.son[0], node1.son[1], k);
            func(node0.son[1], node1.son[0], k);
        }
        else if(node0.son[0]!=null && node0.son[1]!=null && node1.son[1]!=null)
        {
            func(node0.son[0], node1.son[1], k);
        }
        else if (node0.son[0] != null && node0.son[1] != null && node1.son[0] != null)
        {
            func(node0.son[1], node1.son[0], k);
        }
        else if(node0.son[1]!=null&&node1.son[0]!=null&&node1.son[0]!=null)
        {
            func(node0.son[1], node1.son[0], k);
        }
        else if (node0.son[0] != null && node1.son[0] != null && node1.son[1] != null)
        {
            func(node0.son[0], node1.son[1], k);
        }
        else if (node0.son[0] == null && node0.son[1] == null)
        {
            ret = Math.Max(k, ret);
            return;
        }
        else
        {
            TrieNode t0 = node0.son[0] == null ? node0.son[1] : node0.son[0];
            TrieNode t1 = node1.son[0] == null ? node1.son[1] : node1.son[0];
            func(t0, t1, k);
        }
    }
} 
*/
