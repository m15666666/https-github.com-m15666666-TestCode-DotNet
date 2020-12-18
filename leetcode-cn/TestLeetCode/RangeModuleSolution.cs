using System;
using System.Collections.Generic;

/*
Range 模块是跟踪数字范围的模块。你的任务是以一种有效的方式设计和实现以下接口。

addRange(int left, int right) 添加半开区间 [left, right)，跟踪该区间中的每个实数。添加与当前跟踪的数字部分重叠的区间时，应当添加在区间 [left, right) 中尚未跟踪的任何数字到该区间中。
queryRange(int left, int right) 只有在当前正在跟踪区间 [left, right) 中的每一个实数时，才返回 true。
removeRange(int left, int right) 停止跟踪区间 [left, right) 中当前正在跟踪的每个实数。

示例：

addRange(10, 20): null
removeRange(14, 16): null
queryRange(10, 14): true （区间 [10, 14) 中的每个数都正在被跟踪）
queryRange(13, 15): false （未跟踪区间 [13, 15) 中像 14, 14.03, 14.17 这样的数字）
queryRange(16, 17): true （尽管执行了删除操作，区间 [16, 17) 中的数字 16 仍然会被跟踪）

提示：

半开区间 [left, right) 表示所有满足 left <= x < right 的实数。
对 addRange, queryRange, removeRange 的所有调用中 0 < left < right < 10^9。
在单个测试用例中，对 addRange 的调用总数不超过 1000 次。
在单个测试用例中，对  queryRange 的调用总数不超过 5000 次。
在单个测试用例中，对 removeRange 的调用总数不超过 1000 次。

*/

/// <summary>
/// https://leetcode-cn.com/problems/range-module/
/// 715. Range 模块
///
///
/// </summary>
internal class RangeModuleSolution
{
    public static void Test()
    {
        var test = new RangeModuleSolution();
        var r1 = test.QueryRange(1,4);
        r1 = test.QueryRange(6,10);
        test.AddRange(2,6);
        test.AddRange(2,8);
        test.AddRange(4,7);
        r1 = test.QueryRange(2,5);
        test.RemoveRange(1, 10);
        test.RemoveRange(3, 5);
        test.RemoveRange(1, 2);
    }

    public RangeModuleSolution()
    {
    }

    private readonly List<int[]> _ranges = new List<int[]>();
    public void AddRange(int left, int right)
    {
        if (_ranges.Count == 0)
        {
            _ranges.Add(new[] { left, right });
            return;
        }

        var first = _ranges[0];
        if (first[0] <= left && right <= first[1]) return;
        if (first[0] <= right && right <= first[1])
        {
            first[0] = left;
            return;
        }
        if (right < first[0])
        {
            _ranges.Insert(0, new[] { left, right });
            return;
        }

        var last = _ranges[_ranges.Count - 1];
        if (last[0] <= left && right <= last[1]) return;
        if (last[0] <= left && left <= last[1])
        {
            last[1] = right;
            return;
        }
        if (last[1] < left)
        {
            _ranges.Add(new[] { left, right });
            return;
        }
        if (left <= first[0] && last[1] <= right)
        {
            _ranges.Clear();
            _ranges.Add(new[] { left, right });
            return;
        }
        var (leftBound, rightBound) = Bound(left, right);
        if (leftBound <= rightBound)
        {
            left = Math.Min(left, _ranges[leftBound][0]);
            right = Math.Max(right, _ranges[rightBound][1]);
            _ranges.RemoveRange(leftBound, rightBound - leftBound + 1);
        }
        _ranges.Insert(leftBound, new[] { left, right });
    }

    public bool QueryRange(int left, int right)
    {
        if (_ranges.Count == 0) return false;

        var first = _ranges[0];
        if (left < first[0]) return false;
        var last = _ranges[_ranges.Count - 1];
        if (last[1] < right) return false;

        int L = 0, R = _ranges.Count - 1;
        while (L <= R)
        {
            var mid = (L + R) / 2;
            var range = _ranges[mid];
            if (range[0] <= left && left <= range[1])
                return range[0] <= right && right <= range[1];

            if (range[0] < left) L = mid + 1;
            else R = mid - 1;
        }
        return false;
    }

    public void RemoveRange(int left, int right)
    {
        if (_ranges.Count == 0) return;

        var first = _ranges[0];
        if (right <= first[0]) return;

        var last = _ranges[_ranges.Count - 1];
        if (last[1] <= left) return;

        if (left <= first[0] && last[1] <= right)
        {
            _ranges.Clear();
            return;
        }

        var (leftBound, rightBound) = Bound(left, right);
        if (leftBound <= rightBound)
        {
            List<int[]> inserted = new List<int[]>();
            for( int i = leftBound; i <= rightBound; i++)
            {
                if (_ranges[i][0] < left) inserted.Add(new[] { _ranges[i][0], left });
                if (right < _ranges[i][1]) inserted.Add(new[] { right, _ranges[i][1] });
            }
            //if (_ranges[leftBound][0] < left) inserted.Add(new[] { _ranges[leftBound][0], left });
            //if (right < _ranges[rightBound][1]) inserted.Add(new[] { right, _ranges[rightBound][1] });
            _ranges.RemoveRange(leftBound, rightBound - leftBound + 1);
            foreach (var item in inserted) _ranges.Insert(leftBound++, item);
            //if(0 < inserted.Count) _ranges.InsertRange(leftBound, inserted);
        }
    }

    private (int, int) Bound(int left, int right)
    {
        int L = 0, R = _ranges.Count - 1;
        int leftBound = 0, rightBound = _ranges.Count - 1;

        while (L <= R)
        {
            var mid = (L + R) / 2;
            if (_ranges[mid][1] < left)
            {
                leftBound = mid + 1;
                L = leftBound;
                continue;
            }
            R = mid - 1;
        }
        while (L <= R)
        {
            var mid = (L + R) / 2;
            if (right < _ranges[mid][0])
            {
                leftBound = mid - 1;
                R = leftBound;
                continue;
            }
            L = mid + 1;
        }

        return (leftBound, rightBound);
    }
}

/*
Range 模块
力扣 (LeetCode)
发布于 2019-06-30
2.4k
方法一：维护有序的不相交区间
分析

我们尝试维护一个数据结构，存储有序的不相交区间。这里存储的区间都是闭区间（与题目中的半开区间不同），并且不会相交。例如，我们不会存储 [[1, 2], [2, 3]][[1,2],[2,3]]，而是存储 [[1,3]][[1,3]]。

由于 Python 和 Java 支持的数据结构不相同，因此我们会对这两种语言分别给出一种算法。

算法（Python）

我们使用列表（list）ranges 来维护这个数据结构。

addRange(): 当我们要添加一个区间 [\mathrm{left}, \mathrm{right}][left,right] 时，我们首先使用二分查找，找到 ii 和 jj，满足 ranges[i: j + 1] 中的所有区间和 [\mathrm{left}, \mathrm{right}][left,right] 都相交。这里也可以直接使用线性查找，因为接下来的操作的时间复杂度是线性的。随后，我们将 ranges[i: j + 1] 中的所有区间替换成一个新的区间 [min(left, sranges[i][0]), max(right, ranges[j][1])]。

removeRange()：当我们要删除一个区间 [\mathrm{left}, \mathrm{right}][left,right] 时，我们同样先找到 ii 和 jj，满足 ranges[i: j + 1] 中的所有区间和 [\mathrm{left}, \mathrm{right}][left,right] 都相交。随后根据不同的情况，ranges[i: j + 1] 中的所有区间会被替换成 0，1，2 个新的区间。

queryRange()：当我们要查找一个区间 [\mathrm{left}, \mathrm{right}][left,right] 时，我们只需要进行二分查找，判断是否有一个区间包含了 [\mathrm{left}, \mathrm{right}][left,right] 即可。


class RangeModule(object):
    def __init__(self):
        self.ranges = []

    def _bounds(self, left, right):
        i, j = 0, len(self.ranges) - 1
        for d in (100, 10, 1):
            while i + d - 1 < len(self.ranges) and self.ranges[i+d-1][1] < left:
                i += d
            while j >= d - 1 and self.ranges[j-d+1][0] > right:
                j -= d
        return i, j

    def addRange(self, left, right):
        i, j = self._bounds(left, right)
        if i <= j:
            left = min(left, self.ranges[i][0])
            right = max(right, self.ranges[j][1])
        self.ranges[i:j+1] = [(left, right)]

    def queryRange(self, left, right):
        i = bisect.bisect_left(self.ranges, (left, float('inf')))
        if i: i -= 1
        return (bool(self.ranges) and
                self.ranges[i][0] <= left and
                right <= self.ranges[i][1])

    def removeRange(self, left, right):
        i, j = self._bounds(left, right)
        merge = []
        for k in xrange(i, j+1):
            if self.ranges[k][0] < left:
                merge.append((self.ranges[k][0], left))
            if right < self.ranges[k][1]:
                merge.append((right, self.ranges[k][1]))
        self.ranges[i:j+1] = merge
算法（Java）

我们使用基于平衡树的集合（TreeSet）ranges 来维护这个数据结构。ranges 内部的区间按照右端点从小到大排序。

addRange()，removeRange(): 和 Python 的实现方法相同，我们遍历 ranges 里的所有区间，找到其中所有与 [\mathrm{left}, \mathrm{right}][left,right] 重合的区间。如果需要添加区间 [\mathrm{left}, \mathrm{right}][left,right]，就删除这些重合的区间，并将 [\mathrm{left}, \mathrm{right}][left,right] 添加到 ranges 中。如果需要删除区间，就在删除这些重合的区间的同时记录下出现的新区间，并在删除操作结束后把这 0，1，2 个新区间添加到 ranges 中。

queryRange()：由于 ranges 是一颗平衡树，我们可以在对数时间复杂度内找出是否有一个区间包含 [\mathrm{left}, \mathrm{right}][left,right]。


class RangeModule {
    TreeSet<Interval> ranges;
    public RangeModule() {
        ranges = new TreeSet();
    }

    public void addRange(int left, int right) {
        Iterator<Interval> itr = ranges.tailSet(new Interval(0, left - 1)).iterator();
        while (itr.hasNext()) {
            Interval iv = itr.next();
            if (right < iv.left) break;
            left = Math.min(left, iv.left);
            right = Math.max(right, iv.right);
            itr.remove();
        }
        ranges.add(new Interval(left, right));
    }

    public boolean queryRange(int left, int right) {
        Interval iv = ranges.higher(new Interval(0, left));
        return (iv != null && iv.left <= left && right <= iv.right);
    }

    public void removeRange(int left, int right) {
        Iterator<Interval> itr = ranges.tailSet(new Interval(0, left)).iterator();
        ArrayList<Interval> todo = new ArrayList();
        while (itr.hasNext()) {
            Interval iv = itr.next();
            if (right < iv.left) break;
            if (iv.left < left) todo.add(new Interval(iv.left, left));
            if (right < iv.right) todo.add(new Interval(right, iv.right));
            itr.remove();
        }
        for (Interval iv: todo) ranges.add(iv);
    }
}

class Interval implements Comparable<Interval>{
    int left;
    int right;

    public Interval(int left, int right){
        this.left = left;
        this.right = right;
    }

    public int compareTo(Interval that){
        if (this.right == that.right) return this.left - that.left;
        return this.right - that.right;
    }
}
复杂度分析

时间复杂度：设 KK 为 ranges 中的元素个数，那么 addRange() 和 removeRange() 的时间复杂度为 O(K)O(K)，queryRange() 的时间复杂度为 O(\log K)O(logK)。更具体地，如果有 AA 次 addRange() 操作，RR 次 removeRange() 操作和 QQ 次 queryRange() 操作，那么总的时间复杂度为 O((A+R)^2+Q\log(A+R))O((A+R) 
2
 +Qlog(A+R))。
空间复杂度：O(A+R)O(A+R)。

线段树，动态开点，延迟标记


const int M = 1e9;
const int MX = 1e6+5;
class RangeModule {
public:
    int ls[MX],rs[MX],sum[MX];
    bool lazy[MX];
    int cnt,root;
    RangeModule() {
        cnt = 0;
        root = addNode();
    }
    void init_node(int p){
        ls[p] = rs[p] = sum[p] = 0;
        lazy[p] = false;
    }
    void spread(int p){
        if(lazy[p]){
            if(!ls[p]) ls[p] = addNode();
            if(!rs[p]) rs[p] = addNode();
            int l = ls[p],r = rs[p];
            if(sum[p]){
                sum[l] = sum[p]/2+(sum[p]&1);
                sum[r] = sum[p]-sum[l];
            }else{
                sum[l] = sum[r] = 0;
            }
            lazy[p] = false;
            lazy[l] = lazy[r] = true;
        }
    }
    int addNode(){
        ++cnt;
        init_node(cnt);
        return cnt;
    }
    void change(int l,int r,int L,int R,int& p,bool add){
        if(p==0){
            p = addNode();
        }
        if(L<=l&&r<=R){
            if(add){
                sum[p] = r-l+1;
            }else{
                sum[p] = 0;
            }
            lazy[p] = true;
            return ;
        }
        spread(p);
        int mid = (l+r)/2;
        if(L<=mid)change(l,mid,L,R,ls[p],add);
        if(R>mid) change(mid+1,r,L,R,rs[p],add);
        updateUp(p);
    }
    void updateUp(int p){
        int l = ls[p],r = rs[p];
        sum[p] = sum[l]+sum[r];
    }

    int query(int l,int r,int L,int R,int p){
        if(r<L||R<l||p==0){
            return 0;
        }
        if(L<=l&&r<=R){
            return sum[p];
        }
        spread(p);
        int mid = (l+r)/2;
        return query(l,mid,L,R,ls[p])+query(mid+1,r,L,R,rs[p]);
    }
    
    void addRange(int left, int right) {
        change(0,M,left,right-1,root,true);
    }
    
    bool queryRange(int left, int right) {
        int num = query(0, M, left, right-1, root);
        return num==(right-left);
    }
    
    void removeRange(int left, int right) {
        change(0,M,left,right-1,root,false);
    }
};

*/