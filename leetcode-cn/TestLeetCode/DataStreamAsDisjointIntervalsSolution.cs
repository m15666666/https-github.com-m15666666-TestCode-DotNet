using System.Collections.Generic;

/*
给定一个非负整数的数据流输入 a1，a2，…，an，…，将到目前为止看到的数字总结为不相交的区间列表。

例如，假设数据流中的整数为 1，3，7，2，6，…，每次的总结为：

[1, 1]
[1, 1], [3, 3]
[1, 1], [3, 3], [7, 7]
[1, 3], [7, 7]
[1, 3], [6, 7]
 

进阶：
如果有很多合并，并且与数据流的大小相比，不相交区间的数量很小，该怎么办?

*/

/// <summary>
/// https://leetcode-cn.com/problems/data-stream-as-disjoint-intervals/
/// 352. 将数据流变为多个不相交区间
///
///
/// </summary>
internal class DataStreamAsDisjointIntervalsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public DataStreamAsDisjointIntervalsSolution()
    {
    }

    private readonly List<int[]> _intervals = new List<int[]>();
    public void AddNum(int val)
    {
        if (_intervals.Count == 0)
        {
            _intervals.Add(new int[] { val, val });
            return;
        }

        var first = _intervals[0];
        var leftV = first[0];
        if (leftV <= val && val <= first[1]) return;
        if (val < leftV)
        {
            if (val == leftV - 1)
            {
                first[0] = val;
                return;
            }
            _intervals.Insert(0, new int[] { val, val });
            return;
        }
        var last = _intervals[^1];
        var rightV = last[1];
        if (last[0] <= val && val <= rightV) return;
        if (rightV < val)
        {
            if (val == rightV + 1)
            {
                last[1] = val;
                return;
            }
            _intervals.Add(new int[] { val, val });
            return;
        }

        int leftIndex = 0, rightIndex = _intervals.Count - 1;
        int afterIndex = -1;
        while (leftIndex <= rightIndex)
        {
            int midIndex = (leftIndex + rightIndex) / 2;
            var mid = _intervals[midIndex];
            var midV = mid[0];
            if (midV <= val && val <= mid[1]) return;
            if (val < midV)
            {
                rightIndex = midIndex - 1;
                afterIndex = midIndex;
                continue;
            }

            leftIndex = midIndex + 1;
        }

        int prevIndex = afterIndex - 1;
        var prev = _intervals[prevIndex];
        var after = _intervals[afterIndex];
        if (after[0] - prev[1] == 2)
        {
            prev[1] = after[1];
            _intervals.RemoveAt(afterIndex);
            return;
        }

        if (val - prev[1] == 1)
        {
            prev[1] = val;
            return;
        }
        if (after[0] - val == 1)
        {
            after[0] = val;
            return;
        }

        _intervals.Insert(afterIndex, new[] { val, val });
    }

    public int[][] GetIntervals()
    {
        return _intervals.ToArray();
    }
}

/*
利用两个列表
两颗赛艇
发布于 2020-10-02
225
解题思路
此处撰写解题思路

代码

class SummaryRanges:

    def __init__(self):
        """
        Initialize your data structure here.
        """
        self.l = []  # 左边界
        self.r = []  # 右边界

    # 二分查找是找大于等于目标值的第一个索引
    def addNum(self, val: int) -> None:
        i = bisect.bisect(self.l, val)
        if i < len(self.l):  # 正常情况，找到大于目标值的左边界
            if i - 1 >= 0:  # 目标值大于等于第一个左边界的情况
                if val - self.r[i - 1] > 1: #目标值比上一个右边界大超过1
                    if self.l[i] - val > 1: #左边界比目标值大超过1
                        self.l.insert(i, val)
                        self.r.insert(i, val)
                    elif self.l[i] - val == 1: #左边界比目标值刚好大1，更新左边界
                        self.l[i] = val
                elif val - self.r[i - 1] == 1: #目标值比上一个右边界刚好大一
                    self.r[i - 1] = val
                if self.l[i] - self.r[i - 1] == 1:  # 更新完了还需要再考察，左边界和上一个右边界是否合并
                    self.r.pop(i - 1)
                    self.l.pop(i)
            if i == 0: #如果刚好插入到0，则说明第一个左边界就大于目标值了
                if self.l[i] - val > 1:
                    self.l.insert(0, val)
                    self.r.insert(0, val)
                elif self.l[i] - val == 1:
                    self.l[i] = val
        else: #如果空序列，或目标值大于最后一个右边界的情况
            if not self.l or val - self.r[-1] > 1:
                self.l.append(val)
                self.r.append(val)
            elif val - self.r[-1] == 1:
                self.r[-1] = val

    def getIntervals(self) -> List[List[int]]:
        return zip(self.l, self.r)

352. 将数据流变为多个不相交区间：一维数组的插入与维护
typingMonkey
发布于 2019-08-04
1.7k
一次就AC还行，只维护一个2n2n的数组，二分查找坐标插入，如果查找出来的坐标为奇数，则说明插入了已有的区间，为偶数才需要修改区间。

image.png


class SummaryRanges:
    def __init__(self):
        self.d = []

    def addNum(self, val: int) -> None:
        m = bisect.bisect(self.d, val)                  #二分查找插入坐标
        if m % 2 == 0:
            if m < len(self.d) and self.d[m] - val == 1:#如果跟右侧区间差值为1时就直接更新右侧区间，为0时不会插入在这里，所以只判断1
                self.d[m] = val
            else:
                self.d.insert(m, val)                   #如果跟区间不相邻就插入区间
                self.d.insert(m, val)
            if m > 0 and self.d[m] - self.d[m - 1] <= 1:#根据现有情况选择是否区间合并
                self.d.pop(m)
                self.d.pop(m - 1)

    def getIntervals(self) -> List[List[int]]:
        return zip(self.d[:: 2], self.d[1:: 2])         #按奇偶顺序打包输出

TreeMap + 链表 (Java)
fanhua
发布于 2020-02-08
1.3k
思路：此题是715题的简化版，715题加入的是区间，本题加入的是一个个点。在查找加入位置时可以通过二分查找或者TreeMap来实现，在进行区间合并时如果使用数组列表的话时间复杂度为O(n), 而用链表的话时间复杂度为O(1)。因此使用TreeMap达到O(lgn)的搜索速度，使用链表达到O(1)的区间合并速度，TreeMap的key采用区间的右端点。
(PS: 715题也可以使用本思路，即用TreeMap + 链表实现)


class SummaryRanges {

  private class Node {

    int left, right;
    Node next;

    public Node(int left, int right, Node next) {
      this.left = left;
      this.right = right;
      this.next = next;
    }
  }

  private TreeMap<Integer, Node> treeMap;

  private Node dummy;

  private int count;

  public SummaryRanges() {
    treeMap = new TreeMap<>();
    dummy = new Node(-2, -2, null);
    treeMap.put(-2, dummy);
    count = 0;
  }

  public void addNum(int val) {
    Node pre = treeMap.lowerEntry(val).getValue();
    Node cur = pre.next;
    if (cur != null && cur.left <= val) {
      return;
    }
    boolean isNull = cur == null;
    if (isNull) {
      cur = new Node(val + 2, val + 2, null);
    }
    if (pre.right + 1 == val) {
      if (val + 1 == cur.left) {
        treeMap.remove(pre.right);
        treeMap.remove(cur.right);
        pre.right = cur.right;
        pre.next = cur.next;
        treeMap.put(cur.right, pre);
        count--;
      } else {
        treeMap.remove(pre.right);
        pre.right++;
        treeMap.put(pre.right, pre);
      }
    } else if (val + 1 == cur.left) {
      cur.left--;
    } else {
      Node node = new Node(val, val, isNull ? null: cur);
      treeMap.put(val, node);
      count++;
      pre.next = node;
    }
  }

  public int[][] getIntervals() {
    int[][] res = new int[count][2];
    Node node = dummy.next;
    int id = 0;
    while (node != null) {
      res[id++] = new int[]{node.left, node.right};
      node = node.next;
    }
    return res;
  }
}

public class SummaryRanges {

    List<int> list;
    public SummaryRanges() {
         
         list = new List<int>();
    }
    
    public void AddNum(int val) {

        int idx = BinarySearch(val);

        if(idx%2 != 0) return;

        if(idx < list.Count && list[idx] - 1 == val){
            list[idx] = val;
        }else{
            list.Insert(idx,val);
            list.Insert(idx,val);
        }

        if(idx > 0 && list[idx] - list[idx-1] <= 1){
            list.RemoveAt(idx);
            list.RemoveAt(idx-1);
        }

    }
    
    public int[][] GetIntervals() {

        List<int[]> ans = new List<int[]>();


        for(int i = 0; i<list.Count; i+=2){
           ans.Add(new int[]{list[i],list[i+1] });
        }

        return ans.ToArray();

    }

    public int BinarySearch(int val){

          int left = 0, right = list.Count;

          while(left < right){
              
              int mid = left + (right - left )/2;


              if(list[mid] <= val ){
                  left = mid + 1;
              }else{
                 right = mid;
              }
          }

          return left;

    }
}
*/