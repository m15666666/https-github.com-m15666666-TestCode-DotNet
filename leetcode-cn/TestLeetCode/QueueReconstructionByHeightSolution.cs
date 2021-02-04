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
根据身高重建队列
力扣官方题解
发布于 2020-11-15
45.3k
方法一：从低到高考虑
思路与算法

当每个人的身高都不相同时，如果我们将他们按照身高从小到大进行排序，那么就可以很方便地还原出原始的队列了。

为了叙述方便，我们设人数为 nn，在进行排序后，它们的身高依次为 h_0, h_1, \cdots, h_{n-1}h 
0
​	
 ,h 
1
​	
 ,⋯,h 
n−1
​	
 ，且排在第 ii 个人前面身高大于 h_ih 
i
​	
  的人数为 k_ik 
i
​	
 。如果我们按照排完序后的顺序，依次将每个人放入队列中，那么当我们放入第 ii 个人时：

第 0, \cdots, i-10,⋯,i−1 个人已经在队列中被安排了位置，并且他们无论站在哪里，对第 ii 个人都没有任何影响，因为他们都比第 ii 个人矮；

而第 i+1, \cdots, n-1i+1,⋯,n−1 个人还没有被放入队列中，但他们只要站在第 ii 个人的前面，就会对第 ii 个人产生影响，因为他们都比第 ii 个人高。

如果我们在初始时建立一个包含 nn 个位置的空队列，而我们每次将一个人放入队列中时，会将一个「空」位置变成「满」位置，那么当我们放入第 ii 个人时，我们需要给他安排一个「空」位置，并且这个「空」位置前面恰好还有 k_ik 
i
​	
  个「空」位置，用来安排给后面身高更高的人。也就是说，第 ii 个人的位置，就是队列中从左往右数第 k_i+1k 
i
​	
 +1 个「空」位置。

那么如果有身高相同的人，上述 k_ik 
i
​	
  定义中的大于就与题目描述中要求的大于等于不等价了，此时应该怎么修改上面的方法呢？我们可以这样想，如果第 ii 个人和第 jj 个人的身高相同，即 h_i = h_jh 
i
​	
 =h 
j
​	
 ，那么我们可以把在队列中处于较后位置的那个人的身高减小一点点。换句话说，对于某一个身高值 hh，我们将队列中第一个身高为 hh 的人保持不变，第二个身高为 hh 的人的身高减少 \deltaδ，第三个身高为 hh 的人的身高减少 2\delta2δ，以此类推，其中 \deltaδ 是一个很小的常数，它使得任何身高为 hh 的人不会与其它（身高不为 hh 的）人造成影响。

如何找到第一个、第二个、第三个身高为 hh 的人呢？我们可以借助 kk 值，可以发现：当 h_i=h_jh 
i
​	
 =h 
j
​	
  时，如果 k_i > k_jk 
i
​	
 >k 
j
​	
 ，那么说明 ii 一定相对于 jj 在队列中处于较后的位置（因为在第 jj 个人之前比他高的所有人，一定都比第 ii 个人要高），按照修改之后的结果，h_ih 
i
​	
  略小于 h_jh 
j
​	
 ，第 ii 个人在排序后应该先于第 jj 个人被放入队列。因此，我们不必真的去对身高进行修改，而只需要按照 h_ih 
i
​	
  为第一关键字升序，k_ik 
i
​	
  为第二关键字降序进行排序即可。此时，具有相同身高的人会按照它们在队列中的位置逆序进行排列，也就间接实现了上面将身高减少 \deltaδ 这一操作的效果。

这样一来，我们只需要使用一开始提到的方法，将第 ii 个人放入队列中的第 k_i+1k 
i
​	
 +1 个空位置，即可得到原始的队列。

代码


class Solution {
    public int[][] reconstructQueue(int[][] people) {
        Arrays.sort(people, new Comparator<int[]>() {
            public int compare(int[] person1, int[] person2) {
                if (person1[0] != person2[0]) {
                    return person1[0] - person2[0];
                } else {
                    return person2[1] - person1[1];
                }
            }
        });
        int n = people.length;
        int[][] ans = new int[n][];
        for (int[] person : people) {
            int spaces = person[1] + 1;
            for (int i = 0; i < n; ++i) {
                if (ans[i] == null) {
                    --spaces;
                    if (spaces == 0) {
                        ans[i] = person;
                        break;
                    }
                }
            }
        }
        return ans;
    }
}
复杂度分析

时间复杂度：O(n^2)O(n 
2
 )，其中 nn 是数组 \textit{people}people 的长度。我们需要 O(n \log n)O(nlogn) 的时间进行排序，随后需要 O(n^2)O(n 
2
 ) 的时间遍历每一个人并将他们放入队列中。由于前者在渐近意义下小于后者，因此总时间复杂度为 O(n^2)O(n 
2
 )。

空间复杂度：O(\log n)O(logn)，即为排序需要使用的栈空间。

方法二：从高到低考虑
思路与算法

同样地，我们也可以将每个人按照身高从大到小进行排序，处理身高相同的人使用的方法类似，即：按照 h_ih 
i
​	
  为第一关键字降序，k_ik 
i
​	
  为第二关键字升序进行排序。如果我们按照排完序后的顺序，依次将每个人放入队列中，那么当我们放入第 ii 个人时：

第 0, \cdots, i-10,⋯,i−1 个人已经在队列中被安排了位置，他们只要站在第 ii 个人的前面，就会对第 ii 个人产生影响，因为他们都比第 ii 个人高；

而第 i+1, \cdots, n-1i+1,⋯,n−1 个人还没有被放入队列中，并且他们无论站在哪里，对第 ii 个人都没有任何影响，因为他们都比第 ii 个人矮。

在这种情况下，我们无从得知应该给后面的人安排多少个「空」位置，因此就不能沿用方法一。但我们可以发现，后面的人既然不会对第 ii 个人造成影响，我们可以采用「插空」的方法，依次给每一个人在当前的队列中选择一个插入的位置。也就是说，当我们放入第 ii 个人时，只需要将其插入队列中，使得他的前面恰好有 k_ik 
i
​	
  个人即可。

代码


class Solution {
    public int[][] reconstructQueue(int[][] people) {
        Arrays.sort(people, new Comparator<int[]>() {
            public int compare(int[] person1, int[] person2) {
                if (person1[0] != person2[0]) {
                    return person2[0] - person1[0];
                } else {
                    return person1[1] - person2[1];
                }
            }
        });
        List<int[]> ans = new ArrayList<int[]>();
        for (int[] person : people) {
            ans.add(person[1], person);
        }
        return ans.toArray(new int[ans.size()][]);
    }
}
复杂度分析

时间复杂度：O(n^2)O(n 
2
 )，其中 nn 是数组 \textit{people}people 的长度。我们需要 O(n \log n)O(nlogn) 的时间进行排序，随后需要 O(n^2)O(n 
2
 ) 的时间遍历每一个人并将他们放入队列中。由于前者在渐近意义下小于后者，因此总时间复杂度为 O(n^2)O(n 
2
 )。

空间复杂度：O(\log n)O(logn)。

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
