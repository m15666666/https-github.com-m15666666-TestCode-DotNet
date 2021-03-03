using System.Collections.Generic;

/*
给定一个二叉树，它的每个结点都存放着一个整数值。

找出路径和等于给定数值的路径总数。

路径不需要从根节点开始，也不需要在叶子节点结束，但是路径方向必须是向下的（只能从父节点到子节点）。

二叉树不超过1000个节点，且节点数值范围是 [-1000000,1000000] 的整数。

示例：

root = [10,5,-3,3,2,null,11,3,-2,null,1], sum = 8

      10
     /  \
    5   -3
   / \    \
  3   2   11
 / \   \
3  -2   1

返回 3。和等于 8 的路径有:

1.  5 -> 3
2.  5 -> 2 -> 1
3.  -3 -> 11

*/

/// <summary>
/// https://leetcode-cn.com/problems/path-sum-iii/
/// 437. 路径总和 III
///
///
/// </summary>
internal class PathSumIIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int PathSum(TreeNode root, int sum)
    {
        return Dfs(new Dictionary<int, int>() { { 0, 1 } }, 0, sum, root);

        static int Dfs(Dictionary<int, int> dic, int curSum, int target, TreeNode node)
        {
            if (node == null) return 0;

            curSum += node.val;
            int ret = 0;
            if (dic.ContainsKey(curSum - target)) ret += dic[curSum - target];

            if (dic.ContainsKey(curSum)) dic[curSum]++;
            else dic[curSum] = 1;

            ret += Dfs(dic, curSum, target, node.left);
            ret += Dfs(dic, curSum, target, node.right);

            //因为是前缀和 所以在再次回到这一层的时候需要把到这一层的和去掉
            dic[curSum]--;
            return ret;
        }
    }

    //public int PathSum(TreeNode root, int sum)
    //{
    //    if (root == null) return 0;
    //    int ret = 0;
    //    PSum(root, new Dictionary<int, int>(), sum, ref ret);
    //    return ret;

    //    static void PSum(TreeNode node, Dictionary<int, int> val2counts, int sum, ref int ret)
    //    {
    //        var v = node.val;
    //        if (v == sum) ret++;
    //        Dictionary<int, int> nval2counts = new Dictionary<int, int>();
    //        foreach (var pair in val2counts)
    //        {
    //            int key = pair.Key + v;
    //            if (key == sum) ret += pair.Value;
    //            nval2counts[key] = pair.Value;
    //        }
    //        if (nval2counts.ContainsKey(v)) nval2counts[v]++;
    //        else nval2counts[v] = 1;

    //        var n = node.left;
    //        if (n != null) PSum(n, nval2counts, sum, ref ret);
    //        n = node.right;
    //        if (n != null) PSum(n, nval2counts, sum, ref ret);
    //    }
    //}
}

/*

*/