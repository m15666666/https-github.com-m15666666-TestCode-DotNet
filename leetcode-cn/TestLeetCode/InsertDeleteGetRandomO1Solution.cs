using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
设计一个支持在平均 时间复杂度 O(1) 下，执行以下操作的数据结构。

insert(val)：当元素 val 不存在时，向集合中插入该项。
remove(val)：元素 val 存在时，从集合中移除该项。
getRandom：随机返回现有集合中的一项。每个元素应该有相同的概率被返回。
示例 :

// 初始化一个空的集合。
RandomizedSet randomSet = new RandomizedSet();

// 向集合中插入 1 。返回 true 表示 1 被成功地插入。
randomSet.insert(1);

// 返回 false ，表示集合中不存在 2 。
randomSet.remove(2);

// 向集合中插入 2 。返回 true 。集合现在包含 [1,2] 。
randomSet.insert(2);

// getRandom 应随机返回 1 或 2 。
randomSet.getRandom();

// 从集合中移除 1 ，返回 true 。集合现在包含 [2] 。
randomSet.remove(1);

// 2 已在集合中，所以返回 false 。
randomSet.insert(2);

// 由于 2 是集合中唯一的数字，getRandom 总是返回 2 。
randomSet.getRandom(); 
*/
/// <summary>
/// https://leetcode-cn.com/problems/insert-delete-getrandom-o1/
/// 380. 常数时间插入、删除和获取随机元素
/// </summary>
class InsertDeleteGetRandomO1Solution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    /** Initialize your data structure here. */
    public InsertDeleteGetRandomO1Solution()
    {

    }

    private HashSet<int> _set = new HashSet<int>();
    private Random _random = new Random();
    /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
    public bool Insert(int val)
    {
        //if (_set.Contains(val)) return false;
        return _set.Add(val);
    }

    /** Removes a value from the set. Returns true if the set contained the specified element. */
    public bool Remove(int val)
    {
        return _set.Remove(val);
    }

    /** Get a random element from the set. */
    public int GetRandom()
    {
        int count = _set.Count;
        if (count == 0) return 0;
        int index = _random.Next() % count;
        int vv = 0;
        int i = 0;
        foreach(int v in _set)
        {
            vv = v;
            if (i++ == index) return v;
        }
        return vv;
    }
}
/*
public class RandomizedSet {
    HashSet<int> set;
    Random r;

    public RandomizedSet()
    {
        this.set = new HashSet<int>();
        r = new Random();
    }

    public bool Insert(int val)
    {
        return this.set.Add(val);
    }

    public bool Remove(int val)
    {
        return this.set.Remove(val);
    }

    public int GetRandom()
    {
        return this.set.ToList()[r.Next(set.Count)];
    }
}

public class RandomizedSet
{
      Dictionary<int, int> _dict;
      List<int> _list;
      Random rd;
      int _count;
    public RandomizedSet()
    {
        _dict = new Dictionary<int, int>();
        _list = new List<int>();
        _count = 0;
        rd = new Random();
    }

    public bool Insert(int val)
    {
        if (_dict.ContainsKey(val))
        {
            return false;
        }
        _dict[val] = _count;
        if (_count >= _list.Count)
        {
            _list.Add(val);
        }
        else
        {
            _list[_count] = val;
        }
        _count++;
        return true;
    }

    public bool Remove(int val)
    {
        if (!_dict.ContainsKey(val))
            return false;

        int index = _dict[val];
        int value = _list[_count - 1];
        _dict[value] = index;
        _list[index] = value;
        _count--;
        _dict.Remove(val);
        return true;
    }

    public int GetRandom()
    {
        int index = rd.Next(_count);
        return _list[index];
    }
}

*/