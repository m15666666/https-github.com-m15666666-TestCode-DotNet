using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
返回与给定的前序和后序遍历匹配的任何二叉树。

 pre 和 post 遍历中的值是不同的正整数。

 

示例：

输入：pre = [1,2,4,5,3,6,7], post = [4,5,2,6,7,3,1]
输出：[1,2,3,4,5,6,7]
 

提示：

1 <= pre.length == post.length <= 30
pre[] 和 post[] 都是 1, 2, ..., pre.length 的排列
每个输入保证至少有一个答案。如果有多个答案，可以返回其中一个。
贡献者 
*/
/// <summary>
/// https://leetcode-cn.com/problems/construct-binary-tree-from-preorder-and-postorder-traversal/
/// 889. 根据前序和后序遍历构造二叉树
/// 
/// </summary>
class ConstructBinaryTreeFromPreorderAndPostorderTraversalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public TreeNode ConstructFromPrePost(int[] pre, int[] post)
    {
        int N = pre.Length;
        if (N == 0) return null;
   
        TreeNode root = new TreeNode(pre[0]);
        if (N == 1) return root;

        return ConstructFromPrePost(pre.AsSpan(), post.AsSpan());
    }

    private TreeNode ConstructFromPrePost(Span<int> pre, Span<int> post)
    {
        int N = pre.Length;
        if (N == 0) return null;
        TreeNode root = new TreeNode(pre[0]);
        if (N == 1) return root;

        int L = 0;
        int leftRoot = pre[1];
        for (int i = 0; i < N; ++i)
            if (post[i] == leftRoot)
            {
                L = i + 1;
                break;
            }

        int leftCount = L;
        root.left = ConstructFromPrePost(pre.Slice(1, leftCount),
                                         post.Slice(0, leftCount));

        int rightCount = N - L - 1;
        root.right = ConstructFromPrePost(pre.Slice(L + 1, rightCount),
                                          post.Slice(L, rightCount));
        return root;
    }
}
/*
public class Solution {
    int preIndex=0;
    int postIndex=0;
    
    public TreeNode ConstructFromPrePost(int[] pre, int[] post) {
        TreeNode root = new TreeNode(pre[preIndex++]);
        if(root.val!=post[postIndex]){
            root.left = ConstructFromPrePost(pre,post);
        }
        if(root.val!=post[postIndex]){
            root.right = ConstructFromPrePost(pre,post);
        }
        postIndex++;
        return root;
    }
}

*/
