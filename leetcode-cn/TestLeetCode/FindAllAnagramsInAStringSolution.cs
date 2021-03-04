using System.Collections.Generic;

/*
给定一个字符串 s 和一个非空字符串 p，找到 s 中所有是 p 的字母异位词的子串，返回这些子串的起始索引。

字符串只包含小写英文字母，并且字符串 s 和 p 的长度都不超过 20100。

说明：

字母异位词指字母相同，但排列不同的字符串。
不考虑答案输出的顺序。
示例 1:

输入:
s: "cbaebabacd" p: "abc"

输出:
[0, 6]
解释:
起始索引等于 0 的子串是 "cba", 它是 "abc" 的字母异位词。
起始索引等于 6 的子串是 "bac", 它是 "abc" 的字母异位词。
 示例 2:

输入:
s: "abab" p: "ab"

输出:
[0, 1, 2]
解释:
起始索引等于 0 的子串是 "ab", 它是 "ab" 的字母异位词。
起始索引等于 1 的子串是 "ba", 它是 "ab" 的字母异位词。
起始索引等于 2 的子串是 "ab", 它是 "ab" 的字母异位词。

*/

/// <summary>
/// https://leetcode-cn.com/problems/find-all-anagrams-in-a-string/
/// 438. 找到字符串中所有字母异位词
///
/// </summary>
internal class FindAllAnagramsInAStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> FindAnagrams(string s, string p)
    {
        const char a = 'a';
        int[] counts = new int[26];
        int diffCount = p.Length;

        foreach (char c in p) counts[c - a]++;

        int left = 0, right = 0;
        int length = s.Length;

        var ret = new List<int>();
        while (right < length)
        {
            int rightCharIndex = s[right] - a;
            if (0 < counts[rightCharIndex])
            {
                counts[rightCharIndex]--;
                diffCount--;
                right++;

                if (diffCount == 0) ret.Add(left);
                continue;
            }

            while (counts[rightCharIndex] == 0 && left < right)
            {
                counts[s[left] - a]++;
                left++;
                diffCount++;
            }
            if ((left < right) || (left == right && 0 < counts[rightCharIndex])) continue;

            // 这个字符不存在于p中，跳过
            left++;
            right++;
        }
        return ret;
    }
}
/*
Java 如何让你的滑动窗口击败99%
XSm
发布于 5 天前
349
滑动窗口
常规滑动窗口的运行效率慢的原因
方法采用滑动窗口，大部分滑动窗口运行效率低的原因是因为在判断是否窗口内的子串是否是字母异位词时，采用的方法是俩个fre数组进行比较或者重新计数进行比较这样每次都多了一个O(len(p)级的时间。如何避免这种费时的比较呢
使用一个常量diff表示窗口内的子串和字母异位词的差距
下面展示代码。思路请看注释


 class Solution {
        public List<Integer> findAnagrams(String s, String p) {
            int[] fre = new int[26];


//          表示窗口内相差的字符的数量
            int dif = 0;
            
            // fre 统计频数
            for (char c :
                    p.toCharArray()) {
                fre[c - 'a']++;
                dif++;
            }
            
            int left=0,right=0;
            int length = s.length();
            char[] array = s.toCharArray();
            
            List<Integer> result = new ArrayList<>();
            
            
            while (right < length) {
                char rightChar = array[right];
                
                
                //是p中的字符  
                if (fre[rightChar-'a'] > 0) {
                    fre[rightChar-'a']--;
                    //差距减少
                    dif--;
                    right++;

                    //差距减少为0时 说明窗口内为所求
                    if (dif == 0) {
                        result.add(left);
                    }


                }else{
                    //俩种情况 ： 第一种 rightChar 是p以外的字符串如"c" "abc" "ab" 此时 left 和 right 都应该指向 c后面的位置   
                    //          第二种 rightChar是p内的字符串但是是额外的一个char如第二个"b" 例 "abb" "ab" 此时right不变 left应该指向第一个b后面的位置
                    
                    //对于第一种情况 left 和 right 都应该定位到 c  所以要恢复fre数组 同时恢复差距
                    //对于第二种情况 此时fre[array[right]-'a']=0 让left移动到第一个b后面的位置 这样就融入了新的b（第二个b）
                    
                    while (fre[array[right]-'a']<=0 && left<right) {
                        fre[array[left]-'a']++;
                        left++;
                        dif++;
                    }

                    if (left == right ) {
                        
                        
                        //这个if用来检测right所处字符是否是p外的字符
                        
                        
                        //用来处理第二种情况 
                        if (fre[array[left] - 'a'] > 0) {
                            //说明是p里的字符 跳过
                            continue;
                        }else{
                        //用来处理第一种情况 移动到这个字符后面的位置
                            left++;
                            right++;
                        }

                    }
                }
            }

            return result;


        }
    }
99.png



public class Solution {
    public IList<int> FindAnagrams(string s, string p) {
        int[] a=new int[128];
        int i=-1,n=p.Length;
        while(++i<n){
            ++a[p[i]];
        }
        IList<int> x=new List<int>();
        char[] chs=s.ToCharArray();
        for(int l=0,r=0,m=chs.Length;r<m;++r){
            if(0<a[chs[r]]){
                --i;
            }
            --a[chs[r]];
            if(i==0){
                while(l<r&&a[chs[l]]<0){
                    ++a[chs[l]];
                    ++l;
                }
                if(r-l+1==n){
                    x.Add(l);
                }
                ++a[chs[l]];
                ++l;
                ++i;
            }
        }
        return x;
    }
}



public class Solution {
    public IList<int> FindAnagrams(string s, string p) {
        int[] a=new int[128];
        int i=-1,n=p.Length;
        while(++i<n){
            ++a[p[i]];
        }
        IList<int> x=new List<int>();
        char[] chs=s.ToCharArray();
        for(int l=0,r=0,m=chs.Length;r<m;++r){
            if(0<a[chs[r]]){
                --i;
            }
            --a[chs[r]];
            if(i==0){
                while(l<r&&a[chs[l]]<0){
                    ++a[chs[l]];
                    ++l;
                }
                if(r-l+1==n){
                    x.Add(l);
                }
                ++a[chs[l]];
                ++l;
                ++i;
            }
        }
        return x;
    }
}

*/