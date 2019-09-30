using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
如果同时有多米诺骨牌落在一张垂直竖立的多米诺骨牌的两边，由于受力平衡， 该骨牌仍然保持不变。

就这个问题而言，我们会认为正在下降的多米诺骨牌不会对其它正在下降或已经下降的多米诺骨牌施加额外的力。

给定表示初始状态的字符串 "S" 。如果第 i 张多米诺骨牌被推向左边，则 S[i] = 'L'；如果第 i 张多米诺骨牌被推向右边，则 S[i] = 'R'；如果第 i 张多米诺骨牌没有被推动，则 S[i] = '.'。

返回表示最终状态的字符串。

示例 1：

输入：".L.R...LR..L.."
输出："LL.RR.LLRRLL.."
示例 2：

输入："RR.L"
输出："RR.L"
说明：第一张多米诺骨牌没有给第二张施加额外的力。
提示：

0 <= N <= 10^5
表示多米诺骨牌状态的字符串只含有 'L'，'R'; 以及 '.';
*/
/// <summary>
/// https://leetcode-cn.com/problems/push-dominoes/
/// 838. 推多米诺
/// 
/// </summary>
class PushDominoesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string PushDominoes(string dominoes)
    {
        const char Left = 'L';
        const char Right = 'R';
        const char Empty = '.';
        if (string.IsNullOrEmpty(dominoes)) return dominoes;

        var chars = dominoes.ToCharArray();
        int len = chars.Length;
        Queue<(int, char)> queue = new Queue<(int, char)>();
        bool changed = false;
        while (true)
        {
            for (int i = 0; i < len; i++)
            {
                var c = chars[i];
                if (c == Empty) continue;

                if (c == Right)
                {
                    var next = i + 1;
                    if (next < len && chars[next] == Empty)
                    {
                        var next2 = next + 1;
                        if (next2 < len && chars[next2] == Left) continue;

                        changed = true;
                        queue.Enqueue((next, Right));
                    }
                    continue;
                }

                // if( c == Left)
                var prev = i - 1;
                if (-1 < prev && chars[prev] == Empty)
                {
                    var prev2 = prev - 1;
                    if (-1 < prev2 && chars[prev2] == Right) continue;

                    changed = true;
                    queue.Enqueue((prev, Left));
                }
            }

            if (queue.Count == 0) break;
            while(0 < queue.Count)
            {
                var item = queue.Dequeue();
                chars[item.Item1] = item.Item2;
            }
        }
        return changed ? new string(chars) : dominoes;
    }
}
/*
public class Solution {
    public string PushDominoes(string dominoes) {
        int countL = 0, countR = 0;
        char[] str = dominoes.ToCharArray();
        for(int i=0;i<str.Length;i++)
        {
            countR = 0;
            countL = 0;
            if (str[i] == '.')
            {
                for (int j = i; j >= 0; j--)
                {
                    if (j == i)
                    {

                    }
                    else if (dominoes[j] == 'R')
                        break;
                    else if (dominoes[j] == '.')
                        countR++;
                    else
                    {
                        countR = -1;
                        break;
                    }
                    if (j == 0 && dominoes[j] == '.')
                        countR = -1;
                }
                for (int j = i; j < str.Length; j++)
                {
                    if(j==i)
                    {

                    }
                    else if (dominoes[j] == 'L')
                        break;
                    else if (dominoes[j] == '.')
                        countL++;
                    else
                    {
                        countL = -1;
                        break;
                    }
                    if (j == str.Length - 1 && dominoes[j] == '.')
                        countL = -1;
                        
                }
                if (countL == countR)
                {
                    str[i] = '.';
                }
                else if (countL == -1)
                {
                    str[i] = 'R';
                }
                else if (countR == -1)
                    str[i] = 'L';
                else if (countL > countR)
                    str[i] = 'R';
                else if (countL < countR)
                    str[i] = 'L';
            }
        }
        return new string(str);
    }
} 
class Solution {
    public String pushDominoes(String dominoes) {
        char[] ss = dominoes.toCharArray();
        return dp(ss);
    }

    public String dp(char[] ss){
        int m;
        for(m = 0; m < ss.length; m++){
            if(ss[m] == 'L'){
                for(int i = 0; i < m; i++){
                    ss[i] = 'L';
                }
                break;
            }
            if(ss[m] == 'R'){
                break;
            }
        }
        int n;
        for(n = ss.length - 1; n >= 0; n--){
            if(ss[n] == 'R'){
                for(int i = ss.length - 1; i > n; i--){
                    ss[i] = 'R';
                }
                break;
            }
            if(ss[n] == 'L'){
                break;
            }
        }
        for(int i = m; i < ss.length - 1; i++){
            if(ss[i] == 'L' || ss[i] == 'R'){
                for(int j = i+1; j < ss.length; j++){
                    if(ss[j] == 'L' || ss[j] == 'R'){
                        if(ss[i] == 'L' && ss[j] == 'L'){
                            for(int k = i + 1; k < j; k++){
                                ss[k] = 'L';
                            }
                        } else if(ss[i] == 'R' && ss[j] == 'R'){
                            for(int k = i + 1; k < j; k++){
                                ss[k] = 'R';
                            }
                        } else if(ss[i] == 'R' && ss[j] == 'L'){
                            for(int k = i + 1; k < j; k++){
                                if(k < j - (j-i)/2){
                                    ss[k] = 'R';
                                } else if(k > i + (j-i)/2){
                                    ss[k] = 'L';
                                }
                            }
                        }
                        i = j;
                    }
                }
            }
        }
        return new String(ss);
    }
}
class Solution {
    public String pushDominoes(String dominoes) {
        int N = dominoes.length();
        int[] indexes = new int[N+2];
        char[] symbols = new char[N+2];
        int len = 1;
        indexes[0] = -1;
        symbols[0] = 'L';

        for (int i = 0; i < N; ++i)
            if (dominoes.charAt(i) != '.') {
                indexes[len] = i;
                symbols[len++] = dominoes.charAt(i);
            }

        indexes[len] = N;
        symbols[len++] = 'R';

        char[] ans = dominoes.toCharArray();
        for (int index = 0; index < len - 1; ++index) {
            int i = indexes[index], j = indexes[index+1];
            char x = symbols[index], y = symbols[index+1];
            char write;
            if (x == y) {
                for (int k = i+1; k < j; ++k)
                    ans[k] = x;
            } else if (x > y) { // RL
                for (int k = i+1; k < j; ++k)
                    ans[k] = k-i == j-k ? '.' : k-i < j-k ? 'R' : 'L';
            }
        }

        return String.valueOf(ans);
    }
}
class Solution {
    public String pushDominoes(String S) {
        char[] A = S.toCharArray();
        int N = A.length;
        int[] forces = new int[N];

        // Populate forces going from left to right
        int force = 0;
        for (int i = 0; i < N; ++i) {
            if (A[i] == 'R') force = N;
            else if (A[i] == 'L') force = 0;
            else force = Math.max(force - 1, 0);
            forces[i] += force;
        }

        // Populate forces going from right to left
        force = 0;
        for (int i = N-1; i >= 0; --i) {
            if (A[i] == 'L') force = N;
            else if (A[i] == 'R') force = 0;
            else force = Math.max(force - 1, 0);
            forces[i] -= force;
        }

        StringBuilder ans = new StringBuilder();
        for (int f: forces)
            ans.append(f > 0 ? 'R' : f < 0 ? 'L' : '.');
        return ans.toString();
    }
}
*/
