using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
class ReconstructItinerarySolution
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

    public IList<string> FindItinerary(string[,] tickets)
    {
        if (tickets == null) return new string[0];
        int m = tickets.GetLength(0);
        List<Ticket> starts = new List<Ticket>();
        Dictionary<string, Ticket> flight2Count = new Dictionary<string, Ticket>();
        Dictionary<string, SortedSet<string>> from2Flights = new Dictionary<string, SortedSet<string>>();
        
        string[] ret = new string[m+1];
        for ( int i = 0; i < m; i++)
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
        if( route.Count == ret.Length - 1 )
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

        if(ret[1] != null)
        {
            var compare = ret[route.Count].CompareTo(t.To);
            if (compare < 0) return;
        }

        var key = t.To;
        if (!from2Flights.ContainsKey(key)) return;
        foreach( var flightKey in from2Flights[key] )
        {
            var nextTicket = flight2Count[flightKey];
            if (nextTicket.Count == 0) continue;

            nextTicket.Count--;
            route.Push(nextTicket.To);

            Find(ref ret, nextTicket, flight2Count, from2Flights, route );

            nextTicket.Count++;
            route.Pop();
        }
    }

    private const string Start = "JFK";
    class Ticket
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

*/
