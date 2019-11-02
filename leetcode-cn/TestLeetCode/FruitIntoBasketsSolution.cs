using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在一排树中，第 i 棵树产生 tree[i] 型的水果。
你可以从你选择的任何树开始，然后重复执行以下步骤：

把这棵树上的水果放进你的篮子里。如果你做不到，就停下来。
移动到当前树右侧的下一棵树。如果右边没有树，就停下来。
请注意，在选择一颗树后，你没有任何选择：你必须执行步骤 1，然后执行步骤 2，然后返回步骤 1，然后执行步骤 2，依此类推，直至停止。

你有两个篮子，每个篮子可以携带任何数量的水果，但你希望每个篮子只携带一种类型的水果。
用这个程序你能收集的水果总量是多少？

 

示例 1：

输入：[1,2,1]
输出：3
解释：我们可以收集 [1,2,1]。
示例 2：

输入：[0,1,2,2]
输出：3
解释：我们可以收集 [1,2,2].
如果我们从第一棵树开始，我们将只能收集到 [0, 1]。
示例 3：

输入：[1,2,3,2,2]
输出：4
解释：我们可以收集 [2,3,2,2].
如果我们从第一棵树开始，我们将只能收集到 [1, 2]。
示例 4：

输入：[3,3,3,1,2,1,1,2,3,3,4]
输出：5
解释：我们可以收集 [1,2,1,1,2].
如果我们从第一棵树或第八棵树开始，我们将只能收集到 4 个水果。
 

提示：

1 <= tree.length <= 40000
0 <= tree[i] < tree.length
*/
/// <summary>
/// https://leetcode-cn.com/problems/fruit-into-baskets/
/// 904. 水果成篮
/// 
/// </summary>
class FruitIntoBasketsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int TotalFruit(int[] tree)
    {
        int ret = 0;
        int startIndex = 0;
        Dictionary<int,int> counts = new Dictionary<int, int>();
        for (int stopIndex = 0; stopIndex < tree.Length; ++stopIndex)
        {
            var key = tree[stopIndex];
            if (counts.ContainsKey(key)) counts[key]++;
            else
            {
                counts.Add(key, 1);
                while (counts.Count == 3)
                {
                    var k = tree[startIndex];
                    if (counts[k] == 1) counts.Remove(k);
                    else --counts[k];
                    startIndex++;
                }
            }

            int span = stopIndex - startIndex + 1;
            if (ret < span) ret = span;
        }

        return ret;
    }
}
/*
方法 1：按块扫描
想法

问题等价于，找到最长的子序列，最多含有两种“类型”（tree[i] 的值）。

不单独考虑每个元素，转而考虑相同类型的相连块。

比如说，tree = [1, 1, 1, 1, 2, 2, 3, 3, 3] 可以看成是 blocks = [(1, weight = 4), (2, weight = 2), (3, weight = 3)]。

现在可以使用暴力，从左往右扫描。我们会有类似于 blocks = [1, _2_, 1, 2, 1, 2, _1_, 3, ...] 以及对应权重。

处理的核心思想是，当我们考虑 3 的时候，我们不需要从第二个元素 2 （也就是标记成 _2_ 的数字）开始考虑，我们可以从 3 之前的第一个元素开始考虑（_1_）。这是因为如果我们从前两个或更多元素开始，这个序列一定包含类型 1 和 2，所以序列一定会在 3 处停止，这就比已经考虑的序列更短了。

从每个开始点（块的最左端点）开始考虑，这个结果一定是对的。

算法

Python 和 Java 的实现方法，符号和策略有所不同，可以查看代码内的注释。

JavaPython
class Solution {
    public int totalFruit(int[] tree) {
        // We'll make a list of indexes for which a block starts.
        List<Integer> blockLefts = new ArrayList();

        // Add the left boundary of each block
        for (int i = 0; i < tree.length; ++i)
            if (i == 0 || tree[i-1] != tree[i])
                blockLefts.add(i);

        // Add tree.length as a sentinel for convenience
        blockLefts.add(tree.length);

        int ans = 0, i = 0;
        search: while (true) {
            // We'll start our scan at block[i].
            // types : the different values of tree[i] seen
            // weight : the total number of trees represented
            //          by blocks under consideration
            Set<Integer> types = new HashSet();
            int weight = 0;

            // For each block from the i-th and going forward,
            for (int j = i; j < blockLefts.size() - 1; ++j) {
                // Add each block to consideration
                types.add(tree[blockLefts.get(j)]);
                weight += blockLefts.get(j+1) - blockLefts.get(j);

                // If we have 3+ types, this is an illegal subarray
                if (types.size() >= 3) {
                    i = j - 1;
                    continue search;
                }

                // If it is a legal subarray, record the answer
                ans = Math.max(ans, weight);
            }

            break;
        }

        return ans;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是 tree 的长度。
空间复杂度：O(N)O(N)。
方法 2：滑动窗口
想法

在方法 1中，我们希望找到最长的包含两种不同“类型”的子序列，我们称这样的子序列为合法的。

假设我们考虑所有以下标 j 为结尾的合法子序列，那么一定有一个最小的开始下标 i：称之为 opt(j) = i。

我们会发现这个 opt(j) 是一个单调递增的函数，这是因为所有合法子序列的子序列一定也是合法的。

算法

模拟一个滑动窗口，维护变量 i 是最小的下标满足 [i, j] 是合法的子序列。

维护 count 是序列中各种类型的个数，这使得我们可以很快知道子序列中是否含有 3 中类型。

JavaPython
class Solution {
    public int totalFruit(int[] tree) {
        int ans = 0, i = 0;
        Counter count = new Counter();
        for (int j = 0; j < tree.length; ++j) {
            count.add(tree[j], 1);
            while (count.size() >= 3) {
                count.add(tree[i], -1);
                if (count.get(tree[i]) == 0)
                    count.remove(tree[i]);
                i++;
            }

            ans = Math.max(ans, j - i + 1);
        }

        return ans;
    }
}

class Counter extends HashMap<Integer, Integer> {
    public int get(int k) {
        return containsKey(k) ? super.get(k) : 0;
    }

    public void add(int k, int v) {
        put(k, get(k) + v);
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是 tree 的长度。
空间复杂度：O(N)O(N)。
 
public class Solution {
    public int TotalFruit(int[] tree) {
        int position=0;
        int a=tree[0];
        int b=-1;
        int i=0;
        int j=0;
        int now=0;//现在果篮里的果子数量
        int answer=0;//之前果篮里果子的最大数量记录！
        while(true){
            for(i=position;i<tree.Length;i++){
                if(tree[i]!=a){//遇到第二种果子了！
                    b=tree[i];
                    break;
                }
            }
            if(b==-1){//如果b依旧是-1，就说明我已经遍历完了所有可能了！所以就可以直接输出答案啦！
                if(position==0){return tree.Length;}//如果position依旧是0，就说明果树种类少于2，所以直接return果树数量就可以了。
                return answer;
                }
            //开始从position位置往后检索！（也就是我们开始采集果子啦！）
            for(j=position;j<tree.Length;j++){
                if(tree[j]!=a&&tree[j]!=b){//如果这棵树的果子不是a或者b，就没法继续采集了哦！
                    
                    break;
                }
            now++;//增加现在果篮里的果子数量！
            }
            if(now>answer){answer=now;}//如果最大纪录比我们现在果篮里的果子少的话，就得更新记录啦！
            if(position==0&&j==tree.Length){return tree.Length;}
            a=b;
            b=-1;
            position=i;
            now=0;
        }
        return 24601;
    }
}
*/
