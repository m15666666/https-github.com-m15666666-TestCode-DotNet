using System;
using System.Collections.Generic;
using System.Linq;

/*
给定一个机票的字符串二维数组 [from, to]，子数组中的两个成员分别表示飞机出发和降落的机场地点，对该行程进行重新规划排序。所有这些机票都属于一个从JFK（肯尼迪国际机场）出发的先生，所以该行程必须从 JFK 出发。

说明:

如果存在多种有效的行程，你可以按字符自然排序返回最小的行程组合。例如，行程 ["JFK", "LGA"] 与 ["JFK", "LGB"] 相比就更小，排序更靠前
所有的机场都用三个大写字母表示（机场代码）。
假定所有机票至少存在一种合理的行程。
示例 1:

输入: [["MUC", "LHR"], ["JFK", "MUC"], ["SFO", "SJC"], ["LHR", "SFO"]]
输出: ["JFK", "MUC", "LHR", "SFO", "SJC"]
示例 2:

输入: [["JFK","SFO"],["JFK","ATL"],["SFO","ATL"],["ATL","JFK"],["ATL","SFO"]]
输出: ["JFK","ATL","JFK","SFO","ATL","SFO"]
解释: 另一种有效的行程是 ["JFK","SFO","ATL","JFK","ATL","SFO"]。但是它自然排序更大更靠后。
*/

/// <summary>
/// https://leetcode-cn.com/problems/reconstruct-itinerary/
/// 332. 重新安排行程
/// </summary>
internal class ReconstructItinerarySolution
{
    public void Test()
    {
        string[,] tickets = { { "AXA", "EZE" }, { "EZE", "AUA" }, { "ADL", "JFK" }, { "ADL", "TIA" }, { "AUA", "AXA" }, { "EZE", "TIA" }, { "EZE", "TIA" }, { "AXA", "EZE" }, { "EZE", "ADL" }, { "ANU", "EZE" }, { "TIA", "EZE" }, { "JFK", "ADL" }, { "AUA", "JFK" }, { "JFK", "EZE" }, { "EZE", "ANU" }, { "ADL", "AUA" }, { "ANU", "AXA" }, { "AXA", "ADL" }, { "AUA", "JFK" }, { "EZE", "ADL" }, { "ANU", "TIA" }, { "AUA", "JFK" }, { "TIA", "JFK" }, { "EZE", "AUA" }, { "AXA", "EZE" }, { "AUA", "ANU" }, { "ADL", "AXA" }, { "EZE", "ADL" }, { "AUA", "ANU" }, { "AXA", "EZE" }, { "TIA", "AUA" }, { "AXA", "EZE" }, { "AUA", "SYD" }, { "ADL", "JFK" }, { "EZE", "AUA" }, { "ADL", "ANU" }, { "AUA", "TIA" }, { "ADL", "EZE" }, { "TIA", "JFK" }, { "AXA", "ANU" }, { "JFK", "AXA" }, { "JFK", "ADL" }, { "ADL", "EZE" }, { "AXA", "TIA" }, { "JFK", "AUA" }, { "ADL", "EZE" }, { "JFK", "ADL" }, { "ADL", "AXA" }, { "TIA", "AUA" }, { "AXA", "JFK" }, { "ADL", "AUA" }, { "TIA", "JFK" }, { "JFK", "ADL" }, { "JFK", "ADL" }, { "ANU", "AXA" }, { "TIA", "AXA" }, { "EZE", "JFK" }, { "EZE", "AXA" }, { "ADL", "TIA" }, { "JFK", "AUA" }, { "TIA", "EZE" }, { "EZE", "ADL" }, { "JFK", "ANU" }, { "TIA", "AUA" }, { "EZE", "ADL" }, { "ADL", "JFK" }, { "ANU", "AXA" }, { "AUA", "AXA" }, { "ANU", "EZE" }, { "ADL", "AXA" }, { "ANU", "AXA" }, { "TIA", "ADL" }, { "JFK", "ADL" }, { "JFK", "TIA" }, { "AUA", "ADL" }, { "AUA", "TIA" }, { "TIA", "JFK" }, { "EZE", "JFK" }, { "AUA", "ADL" }, { "ADL", "AUA" }, { "EZE", "ANU" }, { "ADL", "ANU" }, { "AUA", "AXA" }, { "AXA", "TIA" }, { "AXA", "TIA" }, { "ADL", "AXA" }, { "EZE", "AXA" }, { "AXA", "JFK" }, { "JFK", "AUA" }, { "ANU", "ADL" }, { "AXA", "TIA" }, { "ANU", "AUA" }, { "JFK", "EZE" }, { "AXA", "ADL" }, { "TIA", "EZE" }, { "JFK", "AXA" }, { "AXA", "ADL" }, { "EZE", "AUA" }, { "AXA", "ANU" }, { "ADL", "EZE" }, { "AUA", "EZE" } };
        var ret = FindItinerary(tickets);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public IList<string> FindItinerary(IList<IList<string>> tickets)
    {
        int n = tickets.Count;
        var map = new Dictionary<string, MaxPQ<string>>();
        List<string> itinerary = new List<string>();

        foreach (var ticket in tickets)
        {
            string src = ticket[0], dst = ticket[1];
            if (!map.ContainsKey(src)) map[src] = new MaxPQ<string>(n + 1, false);
            map[src].Insert(dst);
        }
        Dfs("JFK");
        itinerary.Reverse();
        return itinerary;

        void Dfs(string from)
        {
            MaxPQ<string> list;
            while (map.ContainsKey(from) && 0 < (list = map[from]).Count)
            {
                string to = list.DelMax();
                Dfs(to);
            }
            itinerary.Add(from);
        }
    }

    public class MaxPQ<Key> where Key : System.IComparable<Key>
    {
        private Key[] pq;

        private int N = 0;
        private readonly bool _maxOrMin = true;

        /// <summary>
        /// 最大堆或者最小堆
        /// </summary>
        /// <param name="cap">容量</param>
        /// <param name="maxOrMin">true:最大堆，false:最小堆</param>
        public MaxPQ(int cap, bool maxOrMin = true)
        {
            pq = new Key[cap + 1];

            _maxOrMin = maxOrMin;
        }

        private void Swim(int k)
        {
            while (k > 1 && Less(Parent(k), k))
            {
                Exchange(Parent(k), k);
                k = Parent(k);
            }
        }

        private void Sink(int k)
        {
            while (Left(k) <= N)
            {
                int older = Left(k);
                if (Right(k) <= N && Less(older, Right(k)))
                    older = Right(k);
                if (Less(older, k)) break;
                Exchange(k, older);
                k = older;
            }
        }

        private void Exchange(int i, int j)
        {
            Key temp = pq[i];
            pq[i] = pq[j];
            pq[j] = temp;
        }

        private bool Less(int i, int j) => (_maxOrMin ? pq[i].CompareTo(pq[j]) : pq[j].CompareTo(pq[i])) < 0;

        private int Parent(int i) => i / 2;

        private int Left(int i) => i * 2;

        private int Right(int i) => i * 2 + 1;

        public void Insert(Key e)
        {
            N++;
            pq[N] = e;
            Swim(N);
        }

        public Key DelMax()
        {
            Key max = pq[1];
            Exchange(1, N);
            pq[N] = default(Key);
            N--;
            Sink(1);
            return max;
        }

        public Key Max => pq[1];
        public int Count => N;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///
    /// </summary>
    /// <param name="tickets"></param>
    /// <returns></returns>
    public IList<string> FindItinerary(string[,] tickets)
    {
        if (tickets == null) return new string[0];
        int m = tickets.GetLength(0);
        List<Ticket> starts = new List<Ticket>();
        Dictionary<string, Ticket> flight2Count = new Dictionary<string, Ticket>();
        Dictionary<string, SortedSet<string>> from2Flights = new Dictionary<string, SortedSet<string>>();

        string[] ret = new string[m + 1];
        for (int i = 0; i < m; i++)
        {
            var from = tickets[i, 0];
            var to = tickets[i, 1];
            var flightKey = $"{from}-{to}";

            Ticket t;
            if (!flight2Count.ContainsKey(flightKey))
            {
                flight2Count[flightKey] = t = new Ticket(i, from, to);
            }
            else
            {
                t = flight2Count[flightKey];
                t.Count++;
            }

            if (!from2Flights.ContainsKey(from)) from2Flights[from] = new SortedSet<string>();
            if (!from2Flights[from].Contains(flightKey)) from2Flights[from].Add(flightKey);

            if (from == Start && t.Count == 1) starts.Add(t);
        }

        Stack<string> route = new Stack<string>();
        starts = starts.OrderBy(item => item.To).ToList();
        foreach (var nextTicket in starts)
        {
            nextTicket.Count--;
            route.Push(nextTicket.To);

            //Find(ref ret, start, from2tos, indexs, route);
            Find(ref ret, nextTicket, flight2Count, from2Flights, route);

            nextTicket.Count++;
            route.Pop();
        }

        ret[0] = Start;
        return ret;
    }

    //private void Find( ref string[] ret, Ticket t, Dictionary<string, List<Ticket>> from2tos, HashSet<int> indexs, Stack<Ticket> route )
    private void Find(ref string[] ret, Ticket t, Dictionary<string, Ticket> flight2Count, Dictionary<string, SortedSet<string>> from2Flights, Stack<string> route)
    {
        if (ret[^1] != null)
        {
            var compare = ret[route.Count].CompareTo(t.To);
            if (compare < 0) return;
        }

        if (route.Count == ret.Length - 1)
        {
            int startIndex = 1;
            var rr = route.ToList();
            rr.Reverse();
            //if ( ret[1] == null)
            //{
            //    foreach (var to in rr) ret[startIndex++] = to;
            //    return;
            //}
            //foreach (var to in rr) {
            //    var compare = ret[startIndex++].CompareTo(to);
            //    if (compare == 0) continue;
            //    if (compare < 0 ) return;
            //    break;
            //}
            startIndex = 1;
            foreach (var to in rr) ret[startIndex++] = to;
            return;
        }

        var key = t.To;
        if (!from2Flights.ContainsKey(key)) return;
        foreach (var flightKey in from2Flights[key])
        {
            var nextTicket = flight2Count[flightKey];
            if (nextTicket.Count == 0) continue;

            nextTicket.Count--;
            route.Push(nextTicket.To);

            Find(ref ret, nextTicket, flight2Count, from2Flights, route);

            nextTicket.Count++;
            route.Pop();
        }
    }

    private const string Start = "JFK";

    private class Ticket
    {
        public int Index { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Count { get; set; }

        public Ticket(int index, string from, string to)
        {
            Index = index;
            From = from;
            To = to;
            Count = 1;
        }
    }
}

/*
public class Solution {
    Dictionary<string, List<string>> _dict;
    List<string> _result;
    public IList<string> FindItinerary (string[, ] tickets) {
        _dict = new Dictionary<string, List<string>> ();
        _result=new List<string>();
        int h = tickets.GetLength (0);
        // int w=tickets.GetLength(1);
        for (int i = 0; i < h; i++) {
            if (!_dict.ContainsKey (tickets[i, 0])) {
                _dict[tickets[i, 0]] = new List<string> ();
            }
            _dict[tickets[i, 0]].Add(tickets[i, 1]);
        }
        foreach (var key in _dict.Keys) {
            _dict[key].Sort();
        }
        Down("JFK");
        _result.Reverse();
        return _result;
    }
    private void Down(string start){
        while(_dict.ContainsKey(start)&&_dict[start].Count>0){
            string temp=_dict[start][0];
            _dict[start].RemoveAt(0);
            Down(temp);
        }
        _result.Add(start);
    }
}

重新安排行程
力扣官方题解
发布于 2020-08-26
27.8k
前言
本题和 753. 破解保险箱 类似，是力扣平台上为数不多的求解欧拉回路 / 欧拉通路的题目。读者可以配合着进行练习。

我们化简本题题意：给定一个 nn 个点 mm 条边的图，要求从指定的顶点出发，经过所有的边恰好一次（可以理解为给定起点的「一笔画」问题），使得路径的字典序最小。

这种「一笔画」问题与欧拉图或者半欧拉图有着紧密的联系，下面给出定义：

通过图中所有边恰好一次且行遍所有顶点的通路称为欧拉通路。

通过图中所有边恰好一次且行遍所有顶点的回路称为欧拉回路。

具有欧拉回路的无向图称为欧拉图。

具有欧拉通路但不具有欧拉回路的无向图称为半欧拉图。

因为本题保证至少存在一种合理的路径，也就告诉了我们，这张图是一个欧拉图或者半欧拉图。我们只需要输出这条欧拉通路的路径即可。

如果没有保证至少存在一种合理的路径，我们需要判别这张图是否是欧拉图或者半欧拉图，具体地：

对于无向图 GG，GG 是欧拉图当且仅当 GG 是连通的且没有奇度顶点。

对于无向图 GG，GG 是半欧拉图当且仅当 GG 是连通的且 GG 中恰有 22 个奇度顶点。

对于有向图 GG，GG 是欧拉图当且仅当 GG 的所有顶点属于同一个强连通分量且每个顶点的入度和出度相同。

对于有向图 GG，GG 是半欧拉图当且仅当 GG 的所有顶点属于同一个强连通分量且

恰有一个顶点的出度与入度差为 11；

恰有一个顶点的入度与出度差为 11；

所有其他顶点的入度和出度相同。

让我们考虑下面的这张图：

Graph1

我们从起点 \text{JFK}JFK 出发，合法路径有两条：

\text{JFK} \to \text{AAA} \to \text{JFK} \to \text{BBB} \to \text{JFK}JFK→AAA→JFK→BBB→JFK

\text{JFK} \to \text{BBB} \to \text{JFK} \to \text{AAA} \to \text{JFK}JFK→BBB→JFK→AAA→JFK

既然要求字典序最小，那么我们每次应该贪心地选择当前节点所连的节点中字典序最小的那一个，并将其入栈。最后栈中就保存了我们遍历的顺序。

为了保证我们能够快速找到当前节点所连的节点中字典序最小的那一个，我们可以使用优先队列存储当前节点所连到的点，每次我们 O(1)O(1) 地找到最小字典序的节点，并 O(\log m)O(logm) 地删除它。

然后我们考虑一种特殊情况：

Graph2

当我们先访问 \text{AAA}AAA 时，我们无法回到 \text{JFK}JFK，这样我们就无法访问剩余的边了。

也就是说，当我们贪心地选择字典序最小的节点前进时，我们可能先走入「死胡同」，从而导致无法遍历到其他还未访问的边。于是我们希望能够遍历完当前节点所连接的其他节点后再进入「死胡同」。

注意对于每一个节点，它只有最多一个「死胡同」分支。依据前言中对于半欧拉图的描述，只有那个入度与出度差为 11 的节点会导致死胡同。

方法一：\text{Hierholzer}Hierholzer 算法
思路及算法

\text{Hierholzer}Hierholzer 算法用于在连通图中寻找欧拉路径，其流程如下：

从起点出发，进行深度优先搜索。

每次沿着某条边从某个顶点移动到另外一个顶点的时候，都需要删除这条边。

如果没有可移动的路径，则将所在节点加入到栈中，并返回。

当我们顺序地考虑该问题时，我们也许很难解决该问题，因为我们无法判断当前节点的哪一个分支是「死胡同」分支。

不妨倒过来思考。我们注意到只有那个入度与出度差为 11 的节点会导致死胡同。而该节点必然是最后一个遍历到的节点。我们可以改变入栈的规则，当我们遍历完一个节点所连的所有节点后，我们才将该节点入栈（即逆序入栈）。

对于当前节点而言，从它的每一个非「死胡同」分支出发进行深度优先搜索，都将会搜回到当前节点。而从它的「死胡同」分支出发进行深度优先搜索将不会搜回到当前节点。也就是说当前节点的死胡同分支将会优先于其他非「死胡同」分支入栈。

这样就能保证我们可以「一笔画」地走完所有边，最终的栈中逆序地保存了「一笔画」的结果。我们只要将栈中的内容反转，即可得到答案。

代码


class Solution {
    Map<String, PriorityQueue<String>> map = new HashMap<String, PriorityQueue<String>>();
    List<String> itinerary = new LinkedList<String>();

    public List<String> findItinerary(List<List<String>> tickets) {
        for (List<String> ticket : tickets) {
            String src = ticket.get(0), dst = ticket.get(1);
            if (!map.containsKey(src)) {
                map.put(src, new PriorityQueue<String>());
            }
            map.get(src).offer(dst);
        }
        dfs("JFK");
        Collections.reverse(itinerary);
        return itinerary;
    }

    public void dfs(String curr) {
        while (map.containsKey(curr) && map.get(curr).size() > 0) {
            String tmp = map.get(curr).poll();
            dfs(tmp);
        }
        itinerary.add(curr);
    }
}
复杂度分析

时间复杂度：O(m \log m)O(mlogm)，其中 mm 是边的数量。对于每一条边我们需要 O(\log m)O(logm) 地删除它，最终的答案序列长度为 m+1m+1，而与 nn 无关。

空间复杂度：O(m)O(m)，其中 mm 是边的数量。我们需要存储每一条边。

public class Solution {
    private readonly List<int> intRes = new List<int>();
    private readonly Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();

    public IList<string> FindItinerary(IList<IList<string>> tickets)
    {
        foreach (IList<string> item in tickets)
        {
            int key = this.ConvertToInt(item[0]), value = this.ConvertToInt(item[1]);
            if (this.map.ContainsKey(key))
            {
                this.map[key].Add(value);
            }
            else
            {
                this.map[key] = new List<int>() { value };
            }
        }

        foreach (List<int> item in map.Values)
        {
            item.Sort((x, y) => -x.CompareTo(y));
        }

        this.FindItinerary(9386);      // 9376=JFK

        List<string> res = new List<string>();
        for (int i = this.intRes.Count - 1; i >= 0; --i)
        {
            res.Add(this.ConvertToString(this.intRes[i]));
        }

        return res;
    }

    private void FindItinerary(int cur)
    {
        if (!this.map.ContainsKey(cur))
        {
            this.intRes.Add(cur);
            return;
        }
        List<int> list = this.map[cur];
        while (list.Count > 0)
        {
            int next = list.Last();
            list.RemoveAt(list.Count - 1);
            this.FindItinerary(next);
        }
        this.intRes.Add(cur);
    }

    private string ConvertToString(int val)
    {
        return "" + (char)((val >> 10) + 'A') + (char)(((val & 0x3FF) >> 5) + 'A') + (char)((val & 0x1F) + 'A');
    }

    private int ConvertToInt(string str)
    {
        return ((str[0] - 'A') << 10) | ((str[1] - 'A') << 5) | str[2] - 'A';
    }
}

*/