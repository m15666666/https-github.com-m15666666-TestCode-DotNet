using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
假设有打乱顺序的一群人站成一个队列。 每个人由一个整数对(h, k)表示，其中h是这个人的身高，k是排在这个人前面且身高大于或等于h的人数。 编写一个算法来重建这个队列。

注意：
总人数少于1100人。

示例

输入:
[[7,0], [4,4], [7,1], [5,0], [6,1], [5,2]]

输出:
[[5,0], [7,0], [5,2], [6,1], [4,4], [7,1]] 
*/
/// <summary>
/// https://leetcode-cn.com/problems/queue-reconstruction-by-height/
/// 406. 根据身高重建队列
/// https://blog.csdn.net/w8253497062015/article/details/79946558
/// </summary>
class QueueReconstructionByHeightSolution
{
    public void Test()
    {
        var ret = ReconstructQueue(new int[][] { new []{7,0 }, new[] { 4,4 }, new[] { 7,1}, new[] { 5,0}, new[] { 6,1}, new[] { 5,2} });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[][] ReconstructQueue(int[][] people)
    {
        Comparison<int[]> action = (a,b) => {
            var a0 = a[0];
            var a1 = a[1];
            var b0 = b[0];
            var b1 = b[1];

            if (a0 != b0) return b0.CompareTo(a0);
            return a1.CompareTo(b1);
        };
        Array.Sort(people, action);

        List<int[]> ret = new List<int[]>();

        foreach(var p in people)
        {
            ret.Insert(p[1], p);
        }

        return ret.ToArray();
    }
}
/*
public class Solution {
    public int[][] ReconstructQueue(int[][] people) {
        
        if (people.Length <= 1)
            return people;
        
        QuickSort(people, 0, people.Length -1);
        
        // foreach(var item in people) {
        //     Console.Write("   " + item[0] + " " + item[1]);
        // }
        
        List<int[]> list = new List<int[]>();
        for (int i = 0; i < people.Length; i ++) {
            if (people[i][1] >= list.Count) {
                list.Add(people[i]);
            } else {
                list.Insert(people[i][1], people[i]);
            }
        }
        
        int[][] result = list.ToArray();
        
        return result;
    }
    
    void QuickSort(int[][] people, int low, int high) {
        int index = Sub(people, low, high);
        
        if (low < index - 1)
            QuickSort(people, low, index -1);
        
        if (high > index + 1)
            QuickSort(people, index + 1, high);
    }
    
    int Sub(int[][] people, int low, int high) {
        int[] key = people[low];
        
        while(low < high) {
            while(low < high && (people[high][0] < key[0] || (people[high][0] == key[0] && people[high][1] > key[1]))) {
                high --;
            }
            people[low] = people[high];
            
            while(low < high && (people[low][0] > key[0] || (people[low][0] == key[0] && people[low][1] < key[1]))) {
                low ++;
            }
            people[high] = people[low];
        }
        
        people[low] = key;
        
        return low;
    }
}
public class Solution {
    public int[][] ReconstructQueue(int[][] people) {
        
        if (people.Length <= 1)
            return people;
        
        QuickSort(people, 0, people.Length -1);
        
        // foreach(var item in people) {
        //     Console.Write("   " + item[0] + " " + item[1]);
        // }
        
        List<int[]> list = new List<int[]>();
        for (int i = 0; i < people.Length; i ++) {
            if (people[i][1] >= list.Count) {
                list.Add(people[i]);
            } else {
                list.Insert(people[i][1], people[i]);
            }
        }
        
        int[][] result = list.ToArray();
        
        return result;
    }
    
    void QuickSort(int[][] people, int low, int high) {
        if (low >= high)
            return;
        
        int index = Sub(people, low, high);
        
        if (low < index - 1)
            QuickSort(people, low, index -1);
        
        if (high > index + 1)
            QuickSort(people, index + 1, high);
    }
    
    int Sub(int[][] people, int low, int high) {
        int[] key = people[low];
        
        while(low < high) {
            while(low < high && (people[high][0] < key[0] || (people[high][0] == key[0] && people[high][1] > key[1]))) {
                high --;
            }
            people[low] = people[high];
            
            while(low < high && (people[low][0] > key[0] || (people[low][0] == key[0] && people[low][1] < key[1]))) {
                low ++;
            }
            people[high] = people[low];
        }
        
        people[low] = key;
        
        return low;
    }
}
public class Solution {
    public int[][] ReconstructQueue(int[][] people) 
    {
        Array.Sort(people,ComparerTuple);
        List<int[]> result = new List<int[]>();
        for (int i = 0; i < people.Length; i++)
        {
            result.Insert(people[i][1], people[i]);
        }
        return result.ToArray();
    }
    
    public int ComparerTuple(int[] a, int[] b)
    {
        if(a[0] != b[0])
        {
            return b[0].CompareTo(a[0]);
        }
        else
        {
            return a[1].CompareTo(b[1]);
        }
    }
}
public class Solution {
    public int[][] ReconstructQueue(int[][] people) {
        
            Array.Sort(people, (a, b) => (b[0] == a[0] ? a[1] - b[1] : b[0] - a[0]));
            List<int[]> list = new List<int[]>();
            foreach (int[] a in people)
            {
                list.Insert(a[1], new int[] { a[0], a[1] });
            }
            return list.ToArray();
    }
} 
*/
