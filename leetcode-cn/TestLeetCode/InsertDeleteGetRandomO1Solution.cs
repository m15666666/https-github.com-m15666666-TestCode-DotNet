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
    private readonly Dictionary<int, int> _value2Index = new Dictionary<int, int>();
    private readonly List<int> _values = new List<int>();
    private readonly Random _random = new Random();
    public bool Insert(int val)
    {
        if (_value2Index.ContainsKey(val)) return false;
        int index = _values.Count;
        _values.Add(val);
        _value2Index[val] = index;
        return true;
    }

    public bool Remove(int val)
    {
        if (!_value2Index.ContainsKey(val)) return false;
        int lastIndex = _values.Count - 1;
        int delIndex = _value2Index[val];
        if (lastIndex == delIndex)
        {
            _value2Index.Remove(val);
            _values.RemoveAt(lastIndex);
        }
        else
        {
            int lastV = _values[lastIndex];
            _value2Index.Remove(val);
            _values.RemoveAt(lastIndex);

            _values[delIndex] = lastV;
            _value2Index[lastV] = delIndex;
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

    //private HashSet<int> _set = new HashSet<int>();
    //private Random _random = new Random();
    //public bool Insert(int val)
    //{
    //    return _set.Add(val);
    //}

    //public bool Remove(int val)
    //{
    //    return _set.Remove(val);
    //}

    //public int GetRandom()
    //{
    //    int count = _set.Count;
    //    if (count == 0) return 0;
    //    int index = _random.Next() % count;
    //    int vv = 0;
    //    int i = 0;
    //    foreach(int v in _set)
    //    {
    //        vv = v;
    //        if (i++ == index) return v;
    //    }
    //    return vv;
    //}
}
/*
常数时间插入、删除和获取随机元素
力扣 (LeetCode)

发布于 2020-01-02
13.3k
概述
我们需要在平均复杂度为 \mathcal{O}(1)O(1) 实现以下操作：

insert
remove
getRadom
让我们想想如何实现它。从 insert 开始，我们具有两个平均插入时间为 \mathcal{O}(1)O(1) 的选择：

哈希表：Java 中为 HashMap，Python 中为 dictionary。
动态数组：Java 中为 ArrayList，Python 中为 list。
让我们一个个进行思考，虽然哈希表提供常数时间的插入和删除，但是实现 getRandom 时会出现问题。

getRandom 的思想是选择一个随机索引，然后使用该索引返回一个元素。而哈希表中没有索引，因此要获得真正的随机值，则要将哈希表中的键转换为列表，这需要线性时间。解决的方法是用一个列表存储值，并在该列表中实现常数时间的 getRandom。

列表有索引可以实现常数时间的 insert 和 getRandom，则接下来的问题是如何实现常数时间的 remove。

删除任意索引元素需要线性时间，这里的解决方案是总是删除最后一个元素。

将要删除元素和最后一个元素交换。
将最后一个元素删除。
为此，必须在常数时间获取到要删除元素的索引，因此需要一个哈希表来存储值到索引的映射。

综上所述，我们使用以下数据结构：

动态数组存储元素值
哈希表存储存储值到索引的映射。
方法：哈希表 + 动态数组
Insert:

添加元素到动态数组。
在哈希表中添加值到索引的映射
在这里插入图片描述


public boolean insert(int val) {
  if (dict.containsKey(val)) return false;
    
  dict.put(val, list.size());
  list.add(list.size(), val);
  return true;
}
remove:

在哈希表中查找要删除元素的索引。
将要删除元素与最后一个元素交换。
删除最后一个元素。
更新哈希表中的对应关系。
在这里插入图片描述


public boolean remove(int val) {
  if (! dict.containsKey(val)) return false;

  // move the last element to the place idx of the element to delete
  int lastElement = list.get(list.size() - 1);
  int idx = dict.get(val);
  list.set(idx, lastElement);
  dict.put(lastElement, idx);
  // delete the last element
  list.remove(list.size() - 1);
  dict.remove(val);
  return true;
}
getRandom：
借助 Python 中的 random.choice 和 Java 中 的 Random 实现。


public int getRandom() {
  return list.get(rand.nextInt(list.size()));
}
完整代码：


class RandomizedSet {
  Map<Integer, Integer> dict;
  List<Integer> list;
  Random rand = new Random();

  public RandomizedSet() {
    dict = new HashMap();
    list = new ArrayList();
  }

  public boolean insert(int val) {
    if (dict.containsKey(val)) return false;

    dict.put(val, list.size());
    list.add(list.size(), val);
    return true;
  }

  public boolean remove(int val) {
    if (! dict.containsKey(val)) return false;

    // move the last element to the place idx of the element to delete
    int lastElement = list.get(list.size() - 1);
    int idx = dict.get(val);
    list.set(idx, lastElement);
    dict.put(lastElement, idx);
    // delete the last element
    list.remove(list.size() - 1);
    dict.remove(val);
    return true;
  }

  public int getRandom() {
    return list.get(rand.nextInt(list.size()));
  }
}
复杂度分析

时间复杂度：getRandom 时间复杂度为 \mathcal{O}(1)O(1)，insert 和 remove 平均时间复杂度为 \mathcal{O}(1)O(1)，在最坏情况下为 \mathcal{O}(N)O(N) 当元素数量超过当前分配的动态数组和哈希表的容量导致空间重新分配时。
空间复杂度：O(N)O(N)，在动态数组和哈希表分别存储了 NN 个元素的信息。

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