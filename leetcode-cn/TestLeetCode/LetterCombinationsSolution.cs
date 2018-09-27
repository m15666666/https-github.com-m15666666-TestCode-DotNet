using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/letter-combinations-of-a-phone-number/
/// 电话号码的字母组合
/// </summary>
class LetterCombinationsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    private Dictionary<char, string> _num2chars = new Dictionary<char, string> {
        { '2',"abc"},
        { '3',"def"},
        { '4',"ghi"},
        { '5',"jkl"},
        { '6',"mno"},
        { '7',"pqrs"},
        { '8',"tuv"},
        { '9',"wxyz"},
    };
    public IList<string> LetterCombinations(string digits)
    {
        var ret = new List<string>();

        if (string.IsNullOrWhiteSpace(digits)) return ret;

        Accessor[] accessors = new Accessor[digits.Length];
        for( int index = 0; index < accessors.Length; index++)
        {
            char c = digits[index];
            if (!_num2chars.ContainsKey(c)) return ret;

            accessors[index] = new Accessor(_num2chars[c]);

            if( index != accessors.Length - 1)
            {
                accessors[index].NextValue();
            }
        }

        StringBuilder sb = new StringBuilder();
        
        while (true)
        {
            int currentIndex = -1;
            for (int i = accessors.Length - 1; -1 < i; i--)
            {
                var a = accessors[i];

                a.NextValue();
                if (a.ReachEnd)
                {
                    a.Reset();
                    a.NextValue();
                    continue;
                }

                currentIndex = i;
                break;
            }

            // 全部遍历，退出
            if (currentIndex == -1)
            {
                break;
            }

            sb.Clear();
            for (int i = 0; i < accessors.Length; i++)
            {
                sb.Append( accessors[i].LastValue.Value );
            }

            var s = sb.ToString();
            Console.WriteLine(s);
            ret.Add(s);
        }

        return ret;
    }

    public class Accessor
    {
        private int _index = 0;

        private string Origin { get; }

        public bool ReachEnd { get; private set; } = false;

        public Accessor( string text )
        {
            Origin = text;
        }

        public void Reset()
        {
            _index = 0;
            ReachEnd = false;
        }

        public char? LastValue { get; set; }

        public char? NextValue()
        {
            if (Origin == null) return null;

            while (_index < Origin.Length)
            {
                var v = Origin[_index++];

                LastValue = v;
                return v;
            }

            ReachEnd = true;
            return null;
        }
    }
}