/*
给定两个数组，编写一个函数来计算它们的交集。

示例 1：

输入：nums1 = [1,2,2,1], nums2 = [2,2]
输出：[2]
示例 2：

输入：nums1 = [4,9,5], nums2 = [9,4,9,8,4]
输出：[9,4]

说明：

输出结果中的每个元素一定是唯一的。
我们可以不考虑输出结果的顺序。

*/

using System.Collections.Generic;

/// <summary>
/// https://leetcode-cn.com/problems/intersection-of-two-arrays/
/// 349. 两个数组的交集
///
///
///
/// </summary>
internal class IntersectionOfTwoArraysSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] Intersection(int[] nums1, int[] nums2)
    {
        //nums1.Intersect(nums2)
        if (nums2.Length < nums1.Length) (nums1, nums2) = (nums2, nums1);
        List<int> ret = new List<int>(nums1.Length);
        HashSet<int> set1 = new HashSet<int>(nums1);
        foreach (int v in nums2)
            if (set1.Contains(v))
            {
                set1.Remove(v);
                ret.Add(v);
            }
        return ret.ToArray();
    }
}

/*
两个数组的交集
力扣官方题解
发布于 2020-11-01
27.8k
方法一：两个集合
计算两个数组的交集，直观的方法是遍历数组 nums1，对于其中的每个元素，遍历数组 nums2 判断该元素是否在数组 nums2 中，如果存在，则将该元素添加到返回值。假设数组 nums1 和 nums2 的长度分别是 mm 和 nn，则遍历数组 nums1 需要 O(m)O(m) 的时间，判断 nums1 中的每个元素是否在数组 nums2 中需要 O(n)O(n) 的时间，因此总时间复杂度是 O(mn)O(mn)。

如果使用哈希集合存储元素，则可以在 O(1)O(1) 的时间内判断一个元素是否在集合中，从而降低时间复杂度。

首先使用两个集合分别存储两个数组中的元素，然后遍历较小的集合，判断其中的每个元素是否在另一个集合中，如果元素也在另一个集合中，则将该元素添加到返回值。该方法的时间复杂度可以降低到 O(m+n)O(m+n)。


class Solution {
    public int[] intersection(int[] nums1, int[] nums2) {
        Set<Integer> set1 = new HashSet<Integer>();
        Set<Integer> set2 = new HashSet<Integer>();
        for (int num : nums1) {
            set1.add(num);
        }
        for (int num : nums2) {
            set2.add(num);
        }
        return getIntersection(set1, set2);
    }

    public int[] getIntersection(Set<Integer> set1, Set<Integer> set2) {
        if (set1.size() > set2.size()) {
            return getIntersection(set2, set1);
        }
        Set<Integer> intersectionSet = new HashSet<Integer>();
        for (int num : set1) {
            if (set2.contains(num)) {
                intersectionSet.add(num);
            }
        }
        int[] intersection = new int[intersectionSet.size()];
        int index = 0;
        for (int num : intersectionSet) {
            intersection[index++] = num;
        }
        return intersection;
    }
}
复杂度分析

时间复杂度：O(m+n)O(m+n)，其中 mm 和 nn 分别是两个数组的长度。使用两个集合分别存储两个数组中的元素需要 O(m+n)O(m+n) 的时间，遍历较小的集合并判断元素是否在另一个集合中需要 O(\min(m,n))O(min(m,n)) 的时间，因此总时间复杂度是 O(m+n)O(m+n)。

空间复杂度：O(m+n)O(m+n)，其中 mm 和 nn 分别是两个数组的长度。空间复杂度主要取决于两个集合。

方法二：排序 + 双指针
如果两个数组是有序的，则可以使用双指针的方法得到两个数组的交集。

首先对两个数组进行排序，然后使用两个指针遍历两个数组。可以预见的是加入答案的数组的元素一定是递增的，为了保证加入元素的唯一性，我们需要额外记录变量 \textit{pre}pre 表示上一次加入答案数组的元素。

初始时，两个指针分别指向两个数组的头部。每次比较两个指针指向的两个数组中的数字，如果两个数字不相等，则将指向较小数字的指针右移一位，如果两个数字相等，且该数字不等于 \textit{pre}pre ，将该数字添加到答案并更新 \textit{pre}pre 变量，同时将两个指针都右移一位。当至少有一个指针超出数组范围时，遍历结束。


class Solution {
    public int[] intersection(int[] nums1, int[] nums2) {
        Arrays.sort(nums1);
        Arrays.sort(nums2);
        int length1 = nums1.length, length2 = nums2.length;
        int[] intersection = new int[length1 + length2];
        int index = 0, index1 = 0, index2 = 0;
        while (index1 < length1 && index2 < length2) {
            int num1 = nums1[index1], num2 = nums2[index2];
            if (num1 == num2) {
                // 保证加入元素的唯一性
                if (index == 0 || num1 != intersection[index - 1]) {
                    intersection[index++] = num1;
                }
                index1++;
                index2++;
            } else if (num1 < num2) {
                index1++;
            } else {
                index2++;
            }
        }
        return Arrays.copyOfRange(intersection, 0, index);
    }
}
复杂度分析

时间复杂度：O(m \log m+n \log n)O(mlogm+nlogn)，其中 mm 和 nn 分别是两个数组的长度。对两个数组排序的时间复杂度分别是 O(m \log m)O(mlogm) 和 O(n \log n)O(nlogn)，双指针寻找交集元素的时间复杂度是 O(m+n)O(m+n)，因此总时间复杂度是 O(m \log m+n \log n)O(mlogm+nlogn)。

空间复杂度：O(\log m+\log n)O(logm+logn)，其中 mm 和 nn 分别是两个数组的长度。空间复杂度主要取决于排序使用的额外空间。

public class Solution {
    public int[] Intersection(int[] nums1, int[] nums2) {
        //1.使用intersect
        //2.用for迴圈跑num1 再跑num2
        
         return nums1.Intersect(nums2).ToArray();
        //List<int> list = new  List<int>();
      //  foreach(var i in nums1){
        //    foreach(var j in nums2){
          //      if(i==j){
            //        list.Add(i);
              //  }
            //}
            
        //}
        
        //return list.Distinct().ToArray();
    }
}

public class Solution {
    public int[] Intersection(int[] nums1, int[] nums2) {
        //1.使用intersect
        //2.用for迴圈跑num1 再跑num2
        List<int> list = new  List<int>();
        foreach(var i in nums1){
            foreach(var j in nums2){
                if(i==j){
                    list.Add(i);
                }
            }
            
        }
        
        return list.Distinct().ToArray();
    }
}

public class Solution {
    public int[] Intersection(int[] nums1, int[] nums2) {
        Dictionary<int,int> Dic =new Dictionary<int,int>();
        Dictionary<int,int> Dic1 =new Dictionary<int,int>();
        List<int> RetList =new List<int>(); 
        for(int i=0;i<nums1.Length;i++){
             if (!Dic.ContainsKey(nums1[i]))
                 Dic[nums1[i]] =nums1[i];
            
        }
        for(int i=0;i<nums2.Length;i++){
            if (Dic.ContainsKey(nums2[i])&&!Dic1.ContainsKey(nums2[i]))
            {
                Dic1[nums2[i]] = nums2[i];
                RetList.Add(nums2[i]);
            }
        }
        return RetList.ToArray();
    }
}

*/