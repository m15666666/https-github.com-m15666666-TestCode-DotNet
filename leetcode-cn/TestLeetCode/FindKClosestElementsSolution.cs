using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个排序好的数组，两个整数 k 和 x，从数组中找到最靠近 x（两数之差最小）的 k 个数。返回的结果必须要是按升序排好的。如果有两个数与 x 的差值一样，优先选择数值较小的那个数。

示例 1:

输入: [1,2,3,4,5], k=4, x=3
输出: [1,2,3,4]
 
示例 2:

输入: [1,2,3,4,5], k=4, x=-1
输出: [1,2,3,4]

说明:

k 的值为正数，且总是小于给定排序数组的长度。
数组不为空，且长度不超过 104
数组里的每个元素与 x 的绝对值不超过 104
 

更新(2017/9/19):
这个参数 arr 已经被改变为一个整数数组（而不是整数列表）。 请重新加载代码定义以获取最新更改。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/find-k-closest-elements/
/// 658. 找到 K 个最接近的元素
/// https://www.cnblogs.com/kexinxin/p/10394906.html
/// </summary>
class FindKClosestElementsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> FindClosestElements(int[] arr, int k, int x)
    {
        Comparison<int> comparison = (a, b) => {
            if (a == b) return a.CompareTo(b);
            int aDiff = Math.Abs(a - x);
            int bDiff = Math.Abs(b - x);

            return aDiff == bDiff ? a.CompareTo(b) : aDiff.CompareTo(bDiff);
        };
        Array.Sort(arr, comparison);

        int[] ret = new int[k];
        Array.Copy(arr, ret, ret.Length);
        Array.Sort(ret);
        return ret;
    }
}
/*
public class Solution {
    public IList<int> FindClosestElements(int[] arr, int k, int x) {
       List<int> res = new List<int>();
	    int start = 0, end = arr.Length-k;
	    while(start<end){
	        int mid = start + (end-start)/2;
	        if(Math.Abs(arr[mid]-x)>Math.Abs(arr[mid+k]-x)){ //中位数距离x比较远  所以看右半部分 
	            start = mid+1;
	        } else {
	            end = mid;
	        }
	    }
	    for(int i=start; i<start+k; i++){
	        res.Add(arr[i]);
	    }
	    return res;
    }
}
*/
