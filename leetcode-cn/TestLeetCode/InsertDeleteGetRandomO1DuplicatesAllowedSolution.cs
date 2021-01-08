using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
设计一个支持在平均 时间复杂度 O(1) 下， 执行以下操作的数据结构。

注意: 允许出现重复元素。

insert(val)：向集合中插入元素 val。
remove(val)：当 val 存在时，从集合中移除一个 val。
getRandom：从现有集合中随机获取一个元素。每个元素被返回的概率应该与其在集合中的数量呈线性相关。
示例:

// 初始化一个空的集合。
RandomizedCollection collection = new RandomizedCollection();

// 向集合中插入 1 。返回 true 表示集合不包含 1 。
collection.insert(1);

// 向集合中插入另一个 1 。返回 false 表示集合包含 1 。集合现在包含 [1,1] 。
collection.insert(1);

// 向集合中插入 2 ，返回 true 。集合现在包含 [1,1,2] 。
collection.insert(2);

// getRandom 应当有 2/3 的概率返回 1 ，1/3 的概率返回 2 。
collection.getRandom();

// 从集合中删除 1 ，返回 true 。集合现在包含 [1,2] 。
collection.remove(1);

// getRandom 应有相同概率返回 1 和 2 。
collection.getRandom();

*/
/// <summary>
/// https://leetcode-cn.com/problems/insert-delete-getrandom-o1-duplicates-allowed/
/// 381. O(1) 时间插入、删除和获取随机元素 - 允许重复
/// 
/// </summary>
class InsertDeleteGetRandomO1DuplicatesAllowedSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    /** Initialize your data structure here. */
    public InsertDeleteGetRandomO1DuplicatesAllowedSolution()
    {

    }
    private readonly Dictionary<int, HashSet<int>> _value2Index = new Dictionary<int, HashSet<int>>();
    private readonly List<int> _values = new List<int>();
    private readonly Random _random = new Random();
    public bool Insert(int val)
    {
        int index = _values.Count;
        _values.Add(val);
        if (!_value2Index.ContainsKey(val))
        {
            _value2Index[val] = new HashSet<int>() { index };
            return true;
        }
        _value2Index[val].Add(index);
        return false;
    }

    public bool Remove(int val)
    {
        if (_values.Count == 0) return false;
        if (!_value2Index.ContainsKey(val)) return false;

        var set = _value2Index[val];
        int lastIndex = _values.Count - 1;
        int lastV = _values[lastIndex];
        int delIndex = val == lastV ? lastIndex : set.First();

        if(set.Count == 1) _value2Index.Remove(val);
        else set.Remove(delIndex);
        _values.RemoveAt(lastIndex);

        if (lastIndex != delIndex)
        {
            _values[delIndex] = lastV;
            var lastVSet = _value2Index[lastV];
            lastVSet.Remove(lastIndex);
            lastVSet.Add(delIndex);
        }
        
        return true;
    }

    public int GetRandom()
    {
        int count = _values.Count;
        if (count == 0) return 0;
        int index = _random.Next(count);
        return _values[index];
    }

}
/*
O(1) 时间插入、删除和获取随机元素 - 允许重复
力扣官方题解
发布于 2020-10-30
23.4k
方法一：哈希表
思路与算法

为了使得 O(1)O(1) 时间内能够随机获取一个元素，我们将每个数值（可以重复）存储在一个列表 \textit{nums}nums 中。这样，获取随机元素时，只需要随机生成一个列表中的索引，就能够得到一个随机元素。

这样做的问题在于：列表中的随机删除并不是 O(1)O(1) 的。然而我们可以发现，列表中元素的顺序是无关紧要的，只要它们正确地存在于列表中即可。因此，在删除元素时，我们可以将被删的元素与列表中最后一个元素交换位置，随后便可以在 O(1)O(1) 时间内，从列表中去除该元素。

这需要我们额外维护数值在列表中每一次出现的下标集合。对于数值 \textit{val}val 而言，记其下标集合为 S_{idx}S 
idx
​	
 。

在删除时，我们找出 valval 出现的其中一个下标 ii，并将 \textit{nums}[i]nums[i] 与 \textit{nums}[\textit{nums}.\textit{length}-1]nums[nums.length−1] 交换。随后，将 ii 从 S_{val}S 
val
​	
  中去除，并将 S_{\textit{nums}[\textit{nums}.\textit{length}-1]}S 
nums[nums.length−1]
​	
  中原有的 \textit{nums}[\textit{nums}.\textit{length}-1]nums[nums.length−1] 替换成 ii。由于集合的每个操作都是 O(1)O(1) 的，因此总的平均时间复杂度也是 O(1)O(1) 的。

代码


class RandomizedCollection {
    Map<Integer, Set<Integer>> idx;
    List<Integer> nums;

    public RandomizedCollection() {
        idx = new HashMap<Integer, Set<Integer>>();
        nums = new ArrayList<Integer>();
    }
    
    public boolean insert(int val) {
        nums.add(val);
        Set<Integer> set = idx.getOrDefault(val, new HashSet<Integer>());
        set.add(nums.size() - 1);
        idx.put(val, set);
        return set.size() == 1;
    }
    
    public boolean remove(int val) {
        if (!idx.containsKey(val)) {
            return false;
        }
        Iterator<Integer> it = idx.get(val).iterator();  
        int i = it.next();
        int lastNum = nums.get(nums.size() - 1);
        nums.set(i, lastNum);
        idx.get(val).remove(i);
        idx.get(lastNum).remove(nums.size() - 1);
        if (i < nums.size() - 1) {
            idx.get(lastNum).add(i);
        }
        if (idx.get(val).size() == 0) {
            idx.remove(val);
        }
        nums.remove(nums.size() - 1);
        return true;
    }
    
    public int getRandom() {
        return nums.get((int) (Math.random() * nums.size()));
    }
}
复杂度分析

时间复杂度：O(1)O(1)。
空间复杂度：O(N)O(N)，其中 NN 为集合中的元素数目。

public class RandomizedCollection {
       private Dictionary<int, HashSet<int>> d ;
        private List<int> l;
        private Random r ;

    public RandomizedCollection() {
        d = new Dictionary<int, HashSet<int>>();
        l = new List<int>();
        r = new Random();
    }
    
    public bool Insert(int val) {
            l.Add(val);
            if (d.ContainsKey(val))
            {
                d[val].Add(l.Count - 1);
                return false;
            }
            else
            {
                d[val] = new HashSet<int>() { l.Count - 1 };
                return true;
            }
    }
	
    public bool Remove(int val) {
       if (!d.ContainsKey(val))
            {
                return false;
            }
            int first = d[val].First();
            if (d[val].Count > 1)
            {
                d[val].Remove(first);
            }
            else
            {
                d.Remove(val);
            }

            if (first != l.Count -1)
            {
                l[first] = l[^1];
                d[l[first]].Add(first);
                d[l[first]].Remove(l.Count - 1);
            }
            l.RemoveAt(l.Count - 1);
            return true;
    }
    
    public int GetRandom() {
             return l[r.Next(l.Count)];
    }
}

*/