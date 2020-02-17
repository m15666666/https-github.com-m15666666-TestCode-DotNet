using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
我们有一个项的集合，其中第 i 项的值为 values[i]，标签为 labels[i]。

我们从这些项中选出一个子集 S，这样一来：

|S| <= num_wanted
对于任意的标签 L，子集 S 中标签为 L 的项的数目总满足 <= use_limit。
返回子集 S 的最大可能的 和。

 

示例 1：

输入：values = [5,4,3,2,1], labels = [1,1,2,2,3], num_wanted = 3, use_limit = 1
输出：9
解释：选出的子集是第一项，第三项和第五项。
示例 2：

输入：values = [5,4,3,2,1], labels = [1,3,3,3,2], num_wanted = 3, use_limit = 2
输出：12
解释：选出的子集是第一项，第二项和第三项。
示例 3：

输入：values = [9,8,8,7,6], labels = [0,0,0,1,1], num_wanted = 3, use_limit = 1
输出：16
解释：选出的子集是第一项和第四项。
示例 4：

输入：values = [9,8,8,7,6], labels = [0,0,0,1,1], num_wanted = 3, use_limit = 2
输出：24
解释：选出的子集是第一项，第二项和第四项。
 

提示：

1 <= values.length == labels.length <= 20000
0 <= values[i], labels[i] <= 20000
1 <= num_wanted, use_limit <= values.length
*/
/// <summary>
/// https://leetcode-cn.com/problems/largest-values-from-labels/
/// 1090. 受标签影响的最大值
/// 
/// </summary>
class LargestValuesFromLabelsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LargestValsFromLabels(int[] values, int[] labels, int num_wanted, int use_limit)
    {
        Term[] datas = new Term[values.Length];
        int maxLabel = 0;
        for (int i = 0; i < values.Length; i++)
        {
            var label = labels[i]; ;
            datas[i].value = values[i];
            datas[i].label = label;
            if (maxLabel < label) maxLabel = label;
        }

        Array.Sort(datas, (i1, i2) => i2.value.CompareTo( i1.value));

        int[] labelCounts = new int[maxLabel+1];
        Array.Fill(labelCounts, 0);

        int number = 0;
        int sum = 0;
        foreach (var data in datas)
        {
            int label = data.label;
            if (labelCounts[label] < use_limit)
            {
                labelCounts[label]++;

                number++;
                sum += data.value;
                if (num_wanted <= number) return sum;
            }
        }
        return sum;
    }

    public struct Term
    {
        public int value;
        public int label;
    }
}
/*
贪心 C++
ExecutionHY
发布于 4 个月前
342 阅读
image.png

贪心，优先取大的数字，且同一个标签最多取 use_limit 个，取满 num_wanted 个数为止。

class Solution {
public:
    struct term {
        int value, label;
    };
    static bool cmp(term a, term b) {
        return a.value > b.value;
    }
    int largestValsFromLabels(vector<int>& values, vector<int>& labels, int num_wanted, int use_limit) {
        vector<term> data;
        data.resize(values.size());
        int ml = 0;
        for (int i = 0; i < values.size(); i++) {
            data[i].value = values[i];
            data[i].label = labels[i];
            ml = max(ml, labels[i]);
        }
        sort(data.begin(), data.end(), cmp);

        vector<int> bucket;
        bucket.resize(ml+1);
        int num = 0;
        int sum = 0;
        for (int i = 0; i < data.size(); i++) {
            int lab = data[i].label;
            if (bucket[lab] < use_limit) {
                num++;
                bucket[lab]++;
                sum += data[i].value;
                if (num >= num_wanted) return sum;
            }
        }
        return sum;
    }
};
下一篇：贪心算法，调用堆排序中的前k大排序

public class Solution {
    public int LargestValsFromLabels(int[] values, int[] labels, int num_wanted, int use_limit) {
        //节点按照value降序，满足要求时 加入结果，纸质循环结束
        if(num_wanted == 0 || use_limit == 0)
            return 0;
        
        int len = values.Length;
        int[][] pairs = new int[len][];
        for(int i = 0; i < len; i++){
            pairs[i] = new int[2];
            pairs[i][0] = labels[i];
            pairs[i][1] = values[i];
        }
        
        var list = ( from pair in pairs
                     orderby pair[1] descending
                     select pair).ToList();
        
        // 记录当前label选择的数量 max{label} = 20000
        int[] numLabel = new int[20001];  
        int ans = 0;
        // 记录选择的总数 |S|
        int cnt = 0;
        for(int i = 0; i < len; i++){
            int lab = list[i][0];
            if(numLabel[lab] >= use_limit)
                continue;
            ans += list[i][1];
            numLabel[lab]++;
            cnt++;
            if(cnt >= num_wanted)
                return ans;
        }
        return ans;
    }
} 
*/
