using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/letter-combinations-of-a-phone-number/
/// 17. 电话号码的字母组合
/// 
/// 关于Span
/// https://blog.csdn.net/sD7O95O/article/details/79565507?depth_1-utm_source=distribute.pc_relevant.none-task&utm_source=distribute.pc_relevant.none-task
/// https://blog.csdn.net/lindexi_gd/article/details/84251753
/// 关于Range
/// https://www.cnblogs.com/cgzl/p/11677002.html
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

    private static readonly Dictionary<char, string> _num2chars = new Dictionary<char, string> {
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
        List<string> ret = new List<string>();
        if (string.IsNullOrEmpty(digits)) return ret;

        int len = digits.Length;
        char[] buffer = new char[digits.Length];

        void BackTrack(int index, ReadOnlySpan<char> nextDigits)
        {
            if (nextDigits.Length == 0)
            {
                ret.Add(new string(buffer));
                return;
            }
            
            var digit = nextDigits[0];
            var letters = _num2chars[digit];
            foreach( var letter in letters )
            {
                buffer[index] = letter;
                //BackTrack(index + 1, nextDigits[1..^0]);
                BackTrack(index + 1, nextDigits.Slice(1, nextDigits.Length - 1));
            }
        }

        BackTrack(0, digits.AsSpan());
        return ret;
    }
    
    //public IList<string> LetterCombinations(string digits)
    //{
    //    var ret = new List<string>();

    //    if (string.IsNullOrWhiteSpace(digits)) return ret;

    //    Accessor[] accessors = new Accessor[digits.Length];
    //    for( int index = 0; index < accessors.Length; index++)
    //    {
    //        char c = digits[index];
    //        if (!_num2chars.ContainsKey(c)) return ret;

    //        accessors[index] = new Accessor(_num2chars[c]);

    //        if( index != accessors.Length - 1)
    //        {
    //            accessors[index].NextValue();
    //        }
    //    }

    //    StringBuilder sb = new StringBuilder();

    //    while (true)
    //    {
    //        int currentIndex = -1;
    //        for (int i = accessors.Length - 1; -1 < i; i--)
    //        {
    //            var a = accessors[i];

    //            a.NextValue();
    //            if (a.ReachEnd)
    //            {
    //                a.Reset();
    //                a.NextValue();
    //                continue;
    //            }

    //            currentIndex = i;
    //            break;
    //        }

    //        // 全部遍历，退出
    //        if (currentIndex == -1)
    //        {
    //            break;
    //        }

    //        sb.Clear();
    //        for (int i = 0; i < accessors.Length; i++)
    //        {
    //            sb.Append( accessors[i].LastValue.Value );
    //        }

    //        var s = sb.ToString();
    //        Console.WriteLine(s);
    //        ret.Add(s);
    //    }

    //    return ret;
    //}

    //public class Accessor
    //{
    //    private int _index = 0;

    //    private string Origin { get; }

    //    public bool ReachEnd { get; private set; } = false;

    //    public Accessor( string text )
    //    {
    //        Origin = text;
    //    }

    //    public void Reset()
    //    {
    //        _index = 0;
    //        ReachEnd = false;
    //    }

    //    public char? LastValue { get; set; }

    //    public char? NextValue()
    //    {
    //        if (Origin == null) return null;

    //        while (_index < Origin.Length)
    //        {
    //            var v = Origin[_index++];

    //            LastValue = v;
    //            return v;
    //        }

    //        ReachEnd = true;
    //        return null;
    //    }
    //}
}
/*

电话号码的字母组合
力扣 (LeetCode)
发布于 10 个月前
64.0k
方法：回溯
回溯是一种通过穷举所有可能情况来找到所有解的算法。如果一个候选解最后被发现并不是可行解，回溯算法会舍弃它，并在前面的一些步骤做出一些修改，并重新尝试找到可行解。

给出如下回溯函数 backtrack(combination, next_digits) ，它将一个目前已经产生的组合 combination 和接下来准备要输入的数字 next_digits 作为参数。

如果没有更多的数字需要被输入，那意味着当前的组合已经产生好了。
如果还有数字需要被输入：
遍历下一个数字所对应的所有映射的字母。
将当前的字母添加到组合最后，也就是 combination = combination + letter 。
重复这个过程，输入剩下的数字： backtrack(combination + letter, next_digits[1:]) 。



class Solution {
  Map<String, String> phone = new HashMap<String, String>() {{
    put("2", "abc");
    put("3", "def");
    put("4", "ghi");
    put("5", "jkl");
    put("6", "mno");
    put("7", "pqrs");
    put("8", "tuv");
    put("9", "wxyz");
  }};

  List<String> output = new ArrayList<String>();

  public void backtrack(String combination, String next_digits) {
    // if there is no more digits to check
    if (next_digits.length() == 0) {
      // the combination is done
      output.add(combination);
    }
    // if there are still digits to check
    else {
      // iterate over all letters which map 
      // the next available digit
      String digit = next_digits.substring(0, 1);
      String letters = phone.get(digit);
      for (int i = 0; i < letters.length(); i++) {
        String letter = phone.get(digit).substring(i, i + 1);
        // append the current letter to the combination
        // and proceed to the next digits
        backtrack(combination + letter, next_digits.substring(1));
      }
    }
  }

  public List<String> letterCombinations(String digits) {
    if (digits.length() != 0)
      backtrack("", digits);
    return output;
  }
}
复杂度分析

时间复杂度： O(3^N \times 4^M)O(3 
N
 ×4 
M
 ) ，其中 N 是输入数字中对应 3 个字母的数目（比方说 2，3，4，5，6，8）， M 是输入数字中对应 4 个字母的数目（比方说 7，9），N+M 是输入数字的总数。

空间复杂度：O(3^N \times 4^M)O(3 
N
 ×4 
M
 ) ，这是因为需要保存 3^N \times 4^M3 
N
 ×4 
M
  个结果。
  
public class Solution {
        public IList<string> LetterCombinations(string digits)
        {
            Dictionary<char, char[]> dictionary = new Dictionary<char, char[]>
            {
                {'2', new[] {'a', 'b', 'c'}},
                {'3', new[] {'d', 'e', 'f'}},
                {'4', new[] {'g', 'h', 'i'}},
                {'5', new[] {'j', 'k', 'l'}},
                {'6', new[] {'m', 'n', 'o'}},
                {'7', new[] {'p', 'q', 'r', 's'}},
                {'8', new[] {'t', 'u', 'v'}},
                {'9', new[] {'w', 'x', 'y', 'z'}}
            };
            return Re(digits, 0, dictionary);
        }

        private List<string> Re(string digits, int index, Dictionary<char, char[]> dictionary)
        {
            List<string> result = new List<string>();
            if (index == digits.Length) return result;
            foreach (var ch in dictionary[digits[index]])
            {
                var returnValue = Re(digits, index + 1, dictionary);
                if (returnValue.Count > 0)
                    foreach (var str in returnValue)
                    {
                        result.Add(ch + str);
                    }
                else
                    result.Add(ch.ToString());
            }

            return result;
        }
}

public class Solution {
    public IList<string> LetterCombinations(string digits) {
        var result = new List<string>();
        if (digits == null || digits == "") {
            return result;
        }

        result.Add("");
        var map = new string[]{"", "", "abc", "def","ghi", "jkl", "mno", "pqrs", "tuv", "wxyz"};
        for(int i = 0; i < digits.Length; i++) {
            var cur = map[digits[i] - '0']; // 注意digit
            var tmp = new List<string>(); // 注意是空List string
            for(var j = 0; j < cur.Length; j++) {
                foreach(var r in result) {
                    tmp.Add(r + cur[j]);
                }
            }

            result = tmp;
        }
        
        return result;
    }
}

public class Solution {
    public IList<string> LetterCombinations(string digits) {
                    var letterNumerDic = new Dictionary<char, List<string>>
            {
                ['2'] = new List<string> { "a", "b", "c" },
                ['3'] = new List<string> { "d", "e", "f" },
                ['4'] = new List<string> { "g", "h", "i" },
                ['5'] = new List<string> { "j", "k", "l" },
                ['6'] = new List<string> { "m", "n", "o" },
                ['7'] = new List<string> { "p", "q", "r", "s" },
                ['8'] = new List<string> { "t", "u", "v" },
                ['9'] = new List<string> { "w", "x", "y", "z" }
            };
            var result = new List<string>();
            foreach (var c in digits)
            {
                if (result.Count() == 0)
                {
                    result.AddRange(letterNumerDic[c]);
                }
                else
                {
                    var resultBase = new List<string>(result);
                    result.Clear();
                    foreach (var appendValue in letterNumerDic[c])
                    {
                        result.AddRange(resultBase.Select(s => s = s + appendValue));
                    }
                }
            }
            return result;
    }
} 
*/
