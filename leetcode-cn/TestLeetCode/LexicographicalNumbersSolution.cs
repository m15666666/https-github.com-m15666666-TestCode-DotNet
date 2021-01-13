using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数 n, 返回从 1 到 n 的字典顺序。

例如，

给定 n =13，返回 [1,10,11,12,13,2,3,4,5,6,7,8,9] 。

请尽可能的优化算法的时间复杂度和空间复杂度。 输入的数据 n 小于等于 5,000,000。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/lexicographical-numbers/
/// 386. 字典序排数
/// https://blog.csdn.net/qq_36946274/article/details/81394392
/// </summary>
class LexicographicalNumbersSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> LexicalOrder(int n)
    {
        List<int> ret = new List<int>();
        for (int i = 1; i < 10; i++)
        {
            if (i<= n)
            {
                ret.Add(i);
                BackTrack(ret, n, i * 10);
            }
            else break;
        }
        return ret;
    }

    private static void BackTrack( List<int> ret, int n, int baseNum )
    {
        for( int i = 0; i < 10; i++)
        {
            int temp = baseNum + i;
            if (temp <= n)
            {
                ret.Add(temp);
                BackTrack(ret, n, temp * 10);
            }
            else break;
        }
    }
}
/*
java 字典序的遍历
PPPPjcute
发布于 2020-10-28
903
解题思路
字典序可以抽象为一棵树
QQ20201028-170405@2x.png
所以，有小到大输出其实就是输出他的先序遍历
参考二叉搜索树的先序遍历写法
1.递归：
此处不一样的是需要舍去头结点0，以1-9分别为根节点进行遍历输出：
1.递归结束条件，当前结点＞n则递归结束
2.将元素值添加进入res,遍历其10个兄弟结点，进入递归其子节点
2.迭代:
见注释

代码

class Solution {
    // public List<Integer> lexicalOrder(int n) {
    //     List<Integer> list = new ArrayList<>();
    //     int curr = 1;
    //     //10叉树的先序遍历
    //     for(int i=0;i<n;i++){
    //         list.add(curr);
    //         if(curr*10<=n){
    //             curr*=10;//进入下一层
    //         }else{
    //             if(curr>=n)   curr/=10;//如果这一层结束了
    //             curr+=1;
    //             while(curr%10==0) curr/=10;//如果>10就要返回上一层
    //         }
    //     }
    //     return list;
    // }
    public List<Integer> lexicalOrder(int n) {
        List<Integer> list = new ArrayList<>();
        for (int i = 1; i < 10; i++){
             dfs(n, i, list);
        }
        return list;
    }
    private void dfs(int n,int i,List<Integer>list){
        if(i>n){
            return ;
        }
        list.add(i);
        for(int j=0;j<=9;j++){
            dfs(n,i*10+j,list);
        }
    }

}

C++ 中规中矩的12ms解法（dfs 时间O(N)）
冰可乐泡枸杞🍉
发布于 2020-09-28
742

class Solution {
    vector<int> ans;
public:
    void dfs(int num, int& n) {
        if (num > n) return;
        ans.push_back(num);
        for (int i = 0; i <= 9; ++i) dfs(num * 10 + i, n);
    }

    vector<int> lexicalOrder(int n) {
        for (int i = 1; i <= 9; ++i) dfs(i, n);
        return ans;
    }
};

先序遍历10叉树
曾德蛟
发布于 2019-09-06
4.6k
10叉.jpg


class Solution {
    public List<Integer> lexicalOrder(int n) {
        List<Integer> res = new ArrayList<Integer>();
        Stack<Integer> tree = new Stack<Integer>();
        if(n < 10) {
            for(int i = n;i > 0;i--) tree.push(i);
        }else for(int i = 9;i > 0;i--) tree.push(i);
        int t,m;
        while(!tree.empty()){
            t = tree.peek();
            tree.pop();
            res.add(t);
            if(t*10>n) continue;
            else {
                m = n - t * 10;
                if(m >9) m = 9;
            }
            for(int i = m;i >= 0;i--) 
                tree.push(t*10+i);
        }
        return res;
    }
}

public class Solution {
    public IList<int> LexicalOrder(int n) {
        List<int> list = new List<int>();
        
        void dfs(int num)
        {
            list.Add(num);
            for(int t = num * 10, max = t + 9; t <= max && t <= n; t++)
                dfs(t);
        }
        
        for(int i = 1; i <= 9 && i <= n; i++)
            dfs(i);
        
        return list;
    }
}
*/
