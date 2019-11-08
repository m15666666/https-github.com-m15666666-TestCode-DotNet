using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在选举中，第 i 张票是在时间为 times[i] 时投给 persons[i] 的。

现在，我们想要实现下面的查询函数： TopVotedCandidate.q(int t) 将返回在 t 时刻主导选举的候选人的编号。

在 t 时刻投出的选票也将被计入我们的查询之中。在平局的情况下，最近获得投票的候选人将会获胜。

示例：

输入：["TopVotedCandidate","q","q","q","q","q","q"], [[[0,1,1,0,0,1,0],[0,5,10,15,20,25,30]],[3],[12],[25],[15],[24],[8]]
输出：[null,0,1,1,0,0,1]
解释：
时间为 3，票数分布情况是 [0]，编号为 0 的候选人领先。
时间为 12，票数分布情况是 [0,1,1]，编号为 1 的候选人领先。
时间为 25，票数分布情况是 [0,1,1,0,0,1]，编号为 1 的候选人领先（因为最近的投票结果是平局）。
在时间 15、24 和 8 处继续执行 3 个查询。
 

提示：

1 <= persons.length = times.length <= 5000
0 <= persons[i] <= persons.length
times 是严格递增的数组，所有元素都在 [0, 10^9] 范围中。
每个测试用例最多调用 10000 次 TopVotedCandidate.q。
TopVotedCandidate.q(int t) 被调用时总是满足 t >= times[0]。
*/
/// <summary>
/// https://leetcode-cn.com/problems/online-election/
/// 911. 在线选举
/// 
/// </summary>
class OnlineElectionSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public OnlineElectionSolution(int[] persons, int[] times)
    {
        var count = new Dictionary<int,int>();
        for (int i = 0; i < persons.Length; ++i)
        {
            int p = persons[i], t = times[i];
            if (!count.ContainsKey(p)) count.Add(p, 1);
            else count[p]++;
            int c = count[p];

            while (A.Count <= c)
                A.Add(new List<Vote>());
            A[c].Add(new Vote(p, t));
        }
    }

    public int Q(int t)
    {
        // Binary search on A[i][0].time for smallest i
        // such that A[i][0].time > t
        int lo = 1, hi = A.Count;
        while (lo < hi)
        {
            int mi = lo + (hi - lo) / 2;
            if (A[mi][0].time <= t) lo = mi + 1;
            else hi = mi;
        }
        int i = lo - 1;

        // Binary search on A[i][j].time for smallest j
        // such that A[i][j].time > t
        lo = 0; hi = A[i].Count;
        while (lo < hi)
        {
            int mi = lo + (hi - lo) / 2;
            if (A[i][mi].time <= t)
                lo = mi + 1;
            else
                hi = mi;
        }
        int j = Math.Max(lo - 1, 0);
        return A[i][j].person;
    }

    private List<List<Vote>> A = new List<List<Vote>>();

    public class Vote
    {
        public int person;
        public int time;
        public Vote(int p, int t)
        {
            person = p;
            time = t;
        }
    }
}
/*
方法 1：列表 + 二分搜索
想法和算法

我么可以把选票存储在选票列表的列表 A 中。每个投票都有一个人和一个时间戳，A[count] 是一个列表，记录当前人获得的第 count 张选票。

然后，A[i][0] 和 A[i] 单调增加，所以我们可以利用二分搜索根据时间找到最近的选票。

JavaPython
class TopVotedCandidate {
    List<List<Vote>> A;
    public TopVotedCandidate(int[] persons, int[] times) {
        A = new ArrayList();
        Map<Integer, Integer> count = new HashMap();
        for (int i = 0; i < persons.length; ++i) {
            int p = persons[i], t = times[i];
            int c = count.getOrDefault(p, 0) + 1;

            count.put(p, c);
            while (A.size() <= c)
                A.add(new ArrayList<Vote>());
            A.get(c).add(new Vote(p, t));
        }
    }

    public int q(int t) {
        // Binary search on A[i][0].time for smallest i
        // such that A[i][0].time > t
        int lo = 1, hi = A.size();
        while (lo < hi) {
            int mi = lo + (hi - lo) / 2;
            if (A.get(mi).get(0).time <= t)
                lo = mi + 1;
            else
                hi = mi;
        }
        int i = lo - 1;

        // Binary search on A[i][j].time for smallest j
        // such that A[i][j].time > t
        lo = 0; hi = A.get(i).size();
        while (lo < hi) {
            int mi = lo + (hi - lo) / 2;
            if (A.get(i).get(mi).time <= t)
                lo = mi + 1;
            else
                hi = mi;
        }
        int j = Math.max(lo-1, 0);
        return A.get(i).get(j).person;
    }
}

class Vote {
    int person, time;
    Vote(int p, int t) {
        person = p;
        time = t;
    }
}
复杂度分析

时间复杂度：O(N + Q \log^2 N)O(N+Qlog 
2
 N)，其中 NN 是选票的个数，QQ 是询问的个数。
空间复杂度：O(N)O(N)。
方法 2：预计算结果 + 二分搜索
想法和算法

每当选票记录，我们可以记录每个胜者改变的 (winner, time) 时刻信息。之后，我们拥有一个有序的时刻信息，并用二分搜索找到答案。

JavaPython
class TopVotedCandidate {
    List<Vote> A;
    public TopVotedCandidate(int[] persons, int[] times) {
        A = new ArrayList();
        Map<Integer, Integer> count = new HashMap();
        int leader = -1;  // current leader
        int m = 0;  // current number of votes for leader

        for (int i = 0; i < persons.length; ++i) {
            int p = persons[i], t = times[i];
            int c = count.getOrDefault(p, 0) + 1;
            count.put(p, c);

            if (c >= m) {
                if (p != leader) {  // lead change
                    leader = p;
                    A.add(new Vote(leader, t));
                }

                if (c > m) m = c;
            }
        }
    }

    public int q(int t) {
        int lo = 1, hi = A.size();
        while (lo < hi) {
            int mi = lo + (hi - lo) / 2;
            if (A.get(mi).time <= t)
                lo = mi + 1;
            else
                hi = mi;
        }

        return A.get(lo - 1).person;
    }
}

class Vote {
    int person, time;
    Vote(int p, int t) {
        person = p;
        time = t;
    }
}
复杂度分析

时间复杂度：O(N + Q\log{N})O(N+QlogN)，其中 NN 是选票个数，QQ 是询问个数。
空间复杂度：O(N)O(N)。
 
*/
