using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个经过编码的字符串，返回它解码后的字符串。

编码规则为: k[encoded_string]，表示其中方括号内部的 encoded_string 正好重复 k 次。注意 k 保证为正整数。

你可以认为输入字符串总是有效的；输入字符串中没有额外的空格，且输入的方括号总是符合格式要求的。

此外，你可以认为原始数据不包含数字，所有的数字只表示重复的次数 k ，例如不会出现像 3a 或 2[4] 的输入。

示例:

s = "3[a]2[bc]", 返回 "aaabcbc".
s = "3[a2[c]]", 返回 "accaccacc".
s = "2[abc]3[cd]ef", 返回 "abcabccdcdcdef". 
*/
/// <summary>
/// https://leetcode-cn.com/problems/decode-string/
/// 394. 字符串解码
/// </summary>
class DecodeStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    //var groups = s.Split(']', StringSplitOptions.RemoveEmptyEntries);
    //foreach( var group in groups)
    //{
    //    var parts = group.Split('[');
    //    if (parts.Length != 2) continue;
    //    int count = Convert.ToInt32(parts[0]);
    //    for (int i = 0; i < count; i++)
    //        ret.Append(parts[1]);
    //}
    public string DecodeString(string s)
    {
        // "3[z]2[2[y]pq4[2[jk]e1[f]]]ef"
        if (string.IsNullOrWhiteSpace(s)) return s;
        int index = 0;
        return DecodeString(s, ref index);
    }

    private string DecodeString(string s, ref int index)
    {
        // "3[z]2[2[y]pq4[2[jk]e1[f]]]ef"
        StringBuilder ret = new StringBuilder();
        bool isInNumber = false;
        int count = 0;
        for (; index < s.Length;)
        {
            var c = s[index];
            index++;
            if ('0' <= c && c <= '9')
            {
                if (!isInNumber)
                {
                    count = 0;
                    isInNumber = true;
                }
                count = count * 10 + (c - '0');

                continue;
            }
            else
            {
                if (isInNumber)
                {
                    isInNumber = false;
                }

                switch (c)
                {
                    case '[':
                        var subString = DecodeString(s, ref index);
                        while (0 < count--) ret.Append(subString);
                        break;

                    case ']':
                        return ret.ToString();

                    default:
                        ret.Append(c);
                        break;
                }
            }
        }
        return ret.ToString();
    }
}
/*
字符串解码
力扣官方题解
发布于 2020-05-27
46.6k
📺 视频题解

📖 文字题解
方法一：栈操作
思路和算法

本题中可能出现括号嵌套的情况，比如 2[a2[bc]]，这种情况下我们可以先转化成 2[abcbc]，在转化成 abcbcabcbc。我们可以把字母、数字和括号看成是独立的 TOKEN，并用栈来维护这些 TOKEN。具体的做法是，遍历这个栈：

如果当前的字符为数位，解析出一个数字（连续的多个数位）并进栈
如果当前的字符为字母或者左括号，直接进栈
如果当前的字符为右括号，开始出栈，一直到左括号出栈，出栈序列反转后拼接成一个字符串，此时取出栈顶的数字（此时栈顶一定是数字，想想为什么？），就是这个字符串应该出现的次数，我们根据这个次数和字符串构造出新的字符串并进栈
重复如上操作，最终将栈中的元素按照从栈底到栈顶的顺序拼接起来，就得到了答案。注意：这里可以用不定长数组来模拟栈操作，方便从栈底向栈顶遍历。


class Solution {
    int ptr;

    public String decodeString(String s) {
        LinkedList<String> stk = new LinkedList<String>();
        ptr = 0;

        while (ptr < s.length()) {
            char cur = s.charAt(ptr);
            if (Character.isDigit(cur)) {
                // 获取一个数字并进栈
                String digits = getDigits(s);
                stk.addLast(digits);
            } else if (Character.isLetter(cur) || cur == '[') {
                // 获取一个字母并进栈
                stk.addLast(String.valueOf(s.charAt(ptr++))); 
            } else {
                ++ptr;
                LinkedList<String> sub = new LinkedList<String>();
                while (!"[".equals(stk.peekLast())) {
                    sub.addLast(stk.removeLast());
                }
                Collections.reverse(sub);
                // 左括号出栈
                stk.removeLast();
                // 此时栈顶为当前 sub 对应的字符串应该出现的次数
                int repTime = Integer.parseInt(stk.removeLast());
                StringBuffer t = new StringBuffer();
                String o = getString(sub);
                // 构造字符串
                while (repTime-- > 0) {
                    t.append(o);
                }
                // 将构造好的字符串入栈
                stk.addLast(t.toString());
            }
        }

        return getString(stk);
    }

    public String getDigits(String s) {
        StringBuffer ret = new StringBuffer();
        while (Character.isDigit(s.charAt(ptr))) {
            ret.append(s.charAt(ptr++));
        }
        return ret.toString();
    }

    public String getString(LinkedList<String> v) {
        StringBuffer ret = new StringBuffer();
        for (String s : v) {
            ret.append(s);
        }
        return ret.toString();
    }
}
复杂度分析

时间复杂度：记解码后得出的字符串长度为 SS，除了遍历一次原字符串 ss，我们还需要将解码后的字符串中的每个字符都入栈，并最终拼接进答案中，故渐进时间复杂度为 O(S+|s|)O(S+∣s∣)，即 O(S)O(S)。
空间复杂度：记解码后得出的字符串长度为 SS，这里用栈维护 TOKEN，栈的总大小最终与 SS 相同，故渐进空间复杂度为 O(S)O(S)。
方法二：递归
思路和算法

我们也可以用递归来解决这个问题，从左向右解析字符串：

如果当前位置为数字位，那么后面一定包含一个用方括号表示的字符串，即属于这种情况：k[...]：
我们可以先解析出一个数字，然后解析到了左括号，递归向下解析后面的内容，遇到对应的右括号就返回，此时我们可以根据解析出的数字 xx 解析出的括号里的字符串 s's 
′
  构造出一个新的字符串 x \times s'x×s 
′
 ；
我们把 k[...] 解析结束后，再次调用递归函数，解析右括号右边的内容。
如果当前位置是字母位，那么我们直接解析当前这个字母，然后递归向下解析这个字母后面的内容。
如果觉得这里讲的比较抽象，可以结合代码理解一下这个过程。

下面我们可以来讲讲这样做的依据，涉及到《编译原理》相关内容，感兴趣的同学可以参考阅读。 根据题目的定义，我们可以推导出这样的巴科斯范式（BNF）：

\begin{aligned} {\rm String} &\rightarrow { \rm Digits \, [String] \, String \, | \, Alpha \, String \, | \, \epsilon } \\ {\rm Digits} &\rightarrow { \rm Digit \, Digits \, | \, Digit } \\ {\rm Alpha} &\rightarrow { a | \cdots | z | A | \cdots | Z } \\ {\rm Digit} &\rightarrow { 0 | \cdots | 9 } \\ \end{aligned}
String
Digits
Alpha
Digit
​	
  
→Digits[String]String∣AlphaString∣ϵ
→DigitDigits∣Digit
→a∣⋯∣z∣A∣⋯∣Z
→0∣⋯∣9
​	
 

\rm DigitDigit 表示十进制数位，可能的取值是 00 到 99 之间的整数
\rm AlphaAlpha 表示字母，可能的取值是大小写字母的集合，共 5252 个
\rm DigitDigit 表示一个整数，它的组成是 \rm DigitDigit 出现一次或多次
\rm StringString 代表一个代解析的字符串，它可能有三种构成，如 BNF 所示
\rm \epsilonϵ 表示空串，即没有任何子字符
由于 \rm DigitsDigits 和 \rm AlphaAlpha 构成简单，很容易进行词法分析，我们把它他们看作独立的 TOKEN。那么此时的非终结符有 \rm StringString，终结符有 \rm DigitsDigits、\rm AlphaAlpha 和 \rm \epsilonϵ，我们可以根据非终结符和 FOLLOW 集构造出这样的预测分析表：

\rm AlphaAlpha	\rm DigitsDigits	\rm \epsilonϵ
\rm StringString	\rm String \rightarrow Alpha \, StringString→AlphaString	\rm String \rightarrow Digits \, [String] \, StringString→Digits[String]String	\rm String \rightarrow \epsilonString→ϵ
可见不含多重定义的项，为 LL(1) 文法，即：

从左向右分析（Left-to-right-parse）
最左推导（Leftmost-derivation）
超前查看一个符号（1-symbol lookahead）
它决定了我们从左向右遍历这个字符串，每次只判断当前最左边的一个字符的分析方法是正确的。

代码如下。


class Solution {
    String src;
    int ptr;

    public String decodeString(String s) {
        src = s;
        ptr = 0;
        return getString();
    }

    public String getString() {
        if (ptr == src.length() || src.charAt(ptr) == ']') {
            // String -> EPS
            return "";
        }

        char cur = src.charAt(ptr);
        int repTime = 1;
        String ret = "";

        if (Character.isDigit(cur)) {
            // String -> Digits [ String ] String
            // 解析 Digits
            repTime = getDigits(); 
            // 过滤左括号
            ++ptr;
            // 解析 String
            String str = getString(); 
            // 过滤右括号
            ++ptr;
            // 构造字符串
            while (repTime-- > 0) {
                ret += str;
            }
        } else if (Character.isLetter(cur)) {
            // String -> Char String
            // 解析 Char
            ret = String.valueOf(src.charAt(ptr++));
        }
        
        return ret + getString();
    }

    public int getDigits() {
        int ret = 0;
        while (ptr < src.length() && Character.isDigit(src.charAt(ptr))) {
            ret = ret * 10 + src.charAt(ptr++) - '0';
        }
        return ret;
    }
}
复杂度分析

时间复杂度：记解码后得出的字符串长度为 SS，除了遍历一次原字符串 ss，我们还需要将解码后的字符串中的每个字符都拼接进答案中，故渐进时间复杂度为 O(S+|s|)O(S+∣s∣)，即 O(S)O(S)。
空间复杂度：若不考虑答案所占用的空间，那么就只剩递归使用栈空间的大小，这里栈空间的使用和递归树的深度成正比，最坏情况下为 O(|s|)O(∣s∣)，故渐进空间复杂度为 O(|s|)O(∣s∣)。

字符串解码（辅助栈法 / 递归法，清晰图解）
Krahets
发布于 2019-08-09
60.1k
解法一：辅助栈法
本题难点在于括号内嵌套括号，需要从内向外生成与拼接字符串，这与栈的先入后出特性对应。

算法流程：

构建辅助栈 stack， 遍历字符串 s 中每个字符 c；
当 c 为数字时，将数字字符转化为数字 multi，用于后续倍数计算；
当 c 为字母时，在 res 尾部添加 c；
当 c 为 [ 时，将当前 multi 和 res 入栈，并分别置空置 00：
记录此 [ 前的临时结果 res 至栈，用于发现对应 ] 后的拼接操作；
记录此 [ 前的倍数 multi 至栈，用于发现对应 ] 后，获取 multi × [...] 字符串。
进入到新 [ 后，res 和 multi 重新记录。
当 c 为 ] 时，stack 出栈，拼接字符串 res = last_res + cur_multi * res，其中:
last_res是上个 [ 到当前 [ 的字符串，例如 "3[a2[c]]" 中的 a；
cur_multi是当前 [ 到 ] 内字符串的重复倍数，例如 "3[a2[c]]" 中的 2。
返回字符串 res。
复杂度分析：

时间复杂度 O(N)O(N)，一次遍历 s；
空间复杂度 O(N)O(N)，辅助栈在极端情况下需要线性空间，例如 2[2[2[a]]]。



class Solution {
    public String decodeString(String s) {
        StringBuilder res = new StringBuilder();
        int multi = 0;
        LinkedList<Integer> stack_multi = new LinkedList<>();
        LinkedList<String> stack_res = new LinkedList<>();
        for(Character c : s.toCharArray()) {
            if(c == '[') {
                stack_multi.addLast(multi);
                stack_res.addLast(res.toString());
                multi = 0;
                res = new StringBuilder();
            }
            else if(c == ']') {
                StringBuilder tmp = new StringBuilder();
                int cur_multi = stack_multi.removeLast();
                for(int i = 0; i < cur_multi; i++) tmp.append(res);
                res = new StringBuilder(stack_res.removeLast() + tmp);
            }
            else if(c >= '0' && c <= '9') multi = multi * 10 + Integer.parseInt(c + "");
            else res.append(c);
        }
        return res.toString();
    }
}
解法二：递归法
总体思路与辅助栈法一致，不同点在于将 [ 和 ] 分别作为递归的开启与终止条件：

当 s[i] == ']' 时，返回当前括号内记录的 res 字符串与 ] 的索引 i （更新上层递归指针位置）；
当 s[i] == '[' 时，开启新一层递归，记录此 [...] 内字符串 tmp 和递归后的最新索引 i，并执行 res + multi * tmp 拼接字符串。
遍历完毕后返回 res。
复杂度分析：

时间复杂度 O(N)O(N)，递归会更新索引，因此实际上还是一次遍历 s；
空间复杂度 O(N)O(N)，极端情况下递归深度将会达到线性级别。

class Solution {
    public String decodeString(String s) {
        return dfs(s, 0)[0];
    }
    private String[] dfs(String s, int i) {
        StringBuilder res = new StringBuilder();
        int multi = 0;
        while(i < s.length()) {
            if(s.charAt(i) >= '0' && s.charAt(i) <= '9') 
                multi = multi * 10 + Integer.parseInt(String.valueOf(s.charAt(i))); 
            else if(s.charAt(i) == '[') {
                String[] tmp = dfs(s, i + 1);
                i = Integer.parseInt(tmp[0]);
                while(multi > 0) {
                    res.append(tmp[1]);
                    multi--;
                }
            }
            else if(s.charAt(i) == ']') 
                return new String[] { String.valueOf(i), res.toString() };
            else 
                res.append(String.valueOf(s.charAt(i)));
            i++;
        }
        return new String[] { res.toString() };
    } 
}

题解C++，栈
YouLookDeliciousC
发布于 2019-05-28
10.8k
这题看到括号的匹配，首先应该想到的就是用栈来解决问题。

其次，读完题目，要我们类似于制作一个能使用分配律的计算器。想象：如3[a2[c]b] 使用一次分配律-> 3[accb] 再使用一次分配律->accbaccbaccb


class Solution {
public:
    string decodeString(string s) {
        string res = "";
        stack <int> nums;
        stack <string> strs;
        int num = 0;
        int len = s.size();
        for(int i = 0; i < len; ++ i)
        {
            if(s[i] >= '0' && s[i] <= '9')
            {
                num = num * 10 + s[i] - '0';
            }
            else if((s[i] >= 'a' && s[i] <= 'z') ||(s[i] >= 'A' && s[i] <= 'Z'))
            {
                res = res + s[i];
            }
            else if(s[i] == '[') //将‘[’前的数字压入nums栈内， 字母字符串压入strs栈内
            {
                nums.push(num);
                num = 0;
                strs.push(res); 
                res = "";
            }
            else //遇到‘]’时，操作与之相配的‘[’之间的字符，使用分配律
            {
                int times = nums.top();
                nums.pop();
                for(int j = 0; j < times; ++ j)
                    strs.top() += res;
                res = strs.top(); //之后若还是字母，就会直接加到res之后，因为它们是同一级的运算
                                  //若是左括号，res会被压入strs栈，作为上一层的运算
                strs.pop();
            }
        }
        return res;
    }
};

*/
