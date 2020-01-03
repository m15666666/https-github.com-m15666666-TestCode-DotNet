using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定二叉树，按垂序遍历返回其结点值。

对位于 (X, Y) 的每个结点而言，其左右子结点分别位于 (X-1, Y-1) 和 (X+1, Y-1)。

把一条垂线从 X = -infinity 移动到 X = +infinity ，每当该垂线与结点接触时，我们按从上到下的顺序报告结点的值（ Y 坐标递减）。

如果两个结点位置相同，则首先报告的结点值较小。

按 X 坐标顺序返回非空报告的列表。每个报告都有一个结点值列表。

 

示例 1：



输入：[3,9,20,null,null,15,7]
输出：[[9],[3,15],[20],[7]]
解释： 
在不丧失其普遍性的情况下，我们可以假设根结点位于 (0, 0)：
然后，值为 9 的结点出现在 (-1, -1)；
值为 3 和 15 的两个结点分别出现在 (0, 0) 和 (0, -2)；
值为 20 的结点出现在 (1, -1)；
值为 7 的结点出现在 (2, -2)。
示例 2：



输入：[1,2,3,4,5,6,7]
输出：[[4],[2],[1,5,6],[3],[7]]
解释：
根据给定的方案，值为 5 和 6 的两个结点出现在同一位置。
然而，在报告 "[1,5,6]" 中，结点值 5 排在前面，因为 5 小于 6。
 

提示：

树的结点数介于 1 和 1000 之间。
每个结点值介于 0 和 1000 之间。
*/
/// <summary>
/// https://leetcode-cn.com/problems/vertical-order-traversal-of-a-binary-tree/
/// 987. 二叉树的垂序遍历
/// 
/// </summary>
class VerticalOrderTraversalOfABinaryTreeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<int>> VerticalTraversal(TreeNode root)
    {
        Dfs(root, 0, 0);
        var groups = _list.OrderBy(item => item[0]).ThenBy(item => item[1]).ThenBy(item => item[2]).ToArray();
        var upper = groups.Length - 1;
        var x = groups[0][0];
        IList<IList<int>> ret = new List<IList<int>>();
        IList<int> res = new List<int>();
        foreach (int[] group in groups) 
        {
            if (group[0] == x) res.Add(group[2]);
            else 
            {
                x = group[0];
                ret.Add(res);
                res = new List<int> { group[2] };
            }
        }
        if(0 < res.Count) ret.Add(res);
        return ret;
    }

    private void Dfs(TreeNode root, int x, int y)
    {
        if (root == null) return;

        _list.Add(new int[] { x, y, root.val });
        Dfs(root.left, x - 1, y + 1);
        Dfs(root.right, x + 1, y + 1);
    }
    private readonly List<int[]> _list = new List<int[]>();
}
/*
Python 遍历 字典存坐标。再遍历 存答案
Sanjay
发布于 18 天前
31 阅读
class Solution:
    def verticalTraversal(self, root: TreeNode) -> List[List[int]]:
        res = []
        dic = collections.defaultdict()
        def find(root,x,y):
            if not root:
                return
            # if not root.left and not root.right:
            dic[root.val] = [x,y,root.val]
            find(root.left,x-1,y+1)
            find(root.right,x+1,y+1)
        find(root,0,0)
        ###到这里为止，把所有的坐标值存进去字典，key值为root.val，value值为[x,y,root.val]

        vals = sorted(dic.values())
        print(vals)
        if not vals:
            return []
        tmp = vals[0][0]
        print(tmp)
        ans = []
        res = []
        for i in vals:
            # print(i)
            if i[0]==tmp:
                res.append(i[2])
                print(res)
                if vals.index(i) == len(vals)-1:###这里需要判断最后一步一步
                    ans.append(res)
            else:
                ans.append(res)
                res = [] 
                res.append(i[2])
                tmp = i[0]
                if vals.index(i) == len(vals)-1:###这里需要判断最后一步一步
                    ans.append([i[2]])
        return ans

public class Solution
{
    Dictionary<int, IList<(int, int)>> dict = new Dictionary<int, IList<(int, int)>>();

    public IList<IList<int>> VerticalTraversal(TreeNode root)
    {
        dfs(root, 0, 0);
        var values = dict.OrderBy(x => x.Key)
            .Select(kv => kv.Value);
        var ans = new List<IList<int>>();
        foreach(var list in values)
        {
            var sorted = list.OrderBy(
                x => x, 
                Comparer<(int, int)>.Create((a, b)
                    =>
                    {
                        if (a.Item1 == b.Item1) return a.Item2 - b.Item2;
                        return b.Item1 - a.Item1;
                    }))
                .Select(x => x.Item2).ToList();

            ans.Add(sorted);
        }

        return ans;
    }

    void dfs(TreeNode node, int x, int y)
    {
        if (node == null) return;

        if (!dict.TryGetValue(x, out var list))
        {
            dict[x] = list = new List<(int, int)>();
        }

        list.Add((y, node.val));
        dfs(node.left, x - 1, y - 1);
        dfs(node.right, x + 1, y - 1);
    }
} 
*/
