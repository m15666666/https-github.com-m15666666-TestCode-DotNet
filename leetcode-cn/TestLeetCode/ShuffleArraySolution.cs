using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 打乱一个没有重复元素的数组。
/// https://blog.csdn.net/lin453701006/article/details/80397621
/// </summary>
class ShuffleArraySolution
{
    public static void Test()
    {
        int[] nums = new int[] { 1, 2, 3 };
        var t = new ShuffleArraySolution(nums);

        var ret = t.Shuffle();
        ret = t.Shuffle();
        ret = t.Shuffle();
        ret = t.Shuffle();
        ret = t.Reset();
    }


    public ShuffleArraySolution(int[] nums)
    {
        Origin = nums;
        if (Origin == null || Origin.Length < 2) return;

        Init();
    }

    private void Init()
    {
        _map.Clear();
        Accessors = new Accessor[Origin.Length];
        Accessors[0] = new Accessor(_map, Origin);
    }

    private Accessor[] Accessors;
    
    private HashSet<int> _map = new HashSet<int>();
    private int[] Origin { get; set; }

    /** Resets the array to its original configuration and return it. */
    public int[] Reset()
    {
        return Origin;
    }

    /** Returns a random shuffling of the array. */
    public int[] Shuffle()
    {
        if (Origin == null || Origin.Length < 2) return Origin;

        int currentIndex = -1;
        for (int i = Accessors.Length - 1; -1 < i; i--)
        {
            var a = Accessors[i];
            if(a == null) continue;

            if (a.LastValue.HasValue) _map.Remove(a.LastValue.Value);
            a.NextValue();
            if (a.ReachEnd)
            {
                Accessors[i] = null;
                continue;
            }

            currentIndex = i;
            break;
        }

        if (currentIndex == -1)
        {
            Init();
            return Shuffle();
        }

        for (int i = currentIndex + 1; i < Accessors.Length; i++)
        {
            var a = Accessors[i] = new Accessor(_map, Origin);
            a.NextValue();
        }

        int[] ret = new int[Accessors.Length];
        for (int i = 0; i < Accessors.Length; i++)
        {
            ret[i] = Accessors[i].LastValue.Value;
        }

        Console.WriteLine($"currentIndex: {currentIndex}");
        var msg = string.Join(',', ret);
        Console.WriteLine(msg);

        return ret;
    }

    public class Accessor
    {
        private readonly HashSet<int> _map;
        private int _index = 0;
        private int[] Origin { get; }

        public bool NotStart { get; set; } = true;

        public bool ReachEnd { get; set; } = false;


        public Accessor(HashSet<int> map, int[] nums)
        {
            _map = map;
            Origin = nums;
        }

        public int? LastValue { get; set; }

        public int? NextValue()
        {
            NotStart = false;
            if (Origin == null) return null;

            while ( _index < Origin.Length)
            {
                var v = Origin[_index++];

                // 前面的层次取过了，跳过
                if (_map.Contains(v)) continue;

                _map.Add(v);

                LastValue = v;
                return v;
            }

            ReachEnd = true;
            return null;
        }
    }
}