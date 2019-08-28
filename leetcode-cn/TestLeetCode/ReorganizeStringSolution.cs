using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个字符串S，检查是否能重新排布其中的字母，使得两相邻的字符不同。

若可行，输出任意可行的结果。若不可行，返回空字符串。

示例 1:

输入: S = "aab"
输出: "aba"
示例 2:

输入: S = "aaab"
输出: ""
注意:

S 只包含小写字母并且长度在[1, 500]区间内。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/reorganize-string/
/// 767. 重构字符串
/// https://blog.csdn.net/cswhit/article/details/81904059
/// </summary>
class ReorganizeStringSolution
{
    public void Test()
    {
        var ret = ReorganizeString("tndsewnllhrtwsvxenkscbivijfqnysamckzoyfnapuotmdexzkkrpmppttficzerdndssuveompqkemtbwbodrhwsfpbmkafpwyedpcowruntvymxtyyejqtajkcjakghtdwmuygecjncxzcxezgecrxonnszmqmecgvqqkdagvaaucewelchsmebikscciegzoiamovdojrmmwgbxeygibxxltemfgpogjkhobmhwquizuwvhfaiavsxhiknysdghcawcrphaykyashchyomklvghkyabxatmrkmrfsppfhgrwywtlxebgzmevefcqquvhvgounldxkdzndwybxhtycmlybhaaqvodntsvfhwcuhvuccwcsxelafyzushjhfyklvghpfvknprfouevsxmcuhiiiewcluehpmzrjzffnrptwbuhnyahrbzqvirvmffbxvrmynfcnupnukayjghpusewdwrbkhvjnveuiionefmnfxao");//"ogccckcwmbmxtsbmozli"
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string ReorganizeString(string S)
    {
            const string NotFound = "";
            if (string.IsNullOrEmpty(S)) return NotFound;
            int len = S.Length;
            if (len == 1) return S;

            int[] charCounts = new int[26];
            Array.Fill(charCounts, 0);

            //int[] charIndexs = new int[charCounts.Length];
            //for (int i = 0; i < charIndexs.Length; i++)
            //    charIndexs[i] = i;

            foreach (var c in S)
                charCounts[c - 'a']++;
            //Array.Sort(charCounts, charIndexs);

            int maxCount = charCounts.Max();
            if( 1 < maxCount - (S.Length - maxCount)) return NotFound;

            var half = len / 2;
            var rest = len - half;
            Stack<int> stack = new Stack<int>();
            if (!DFS(charCounts, 0, half, stack)) return NotFound;

            char[] r = new char[len];
            int oddIndex = 0;
            int evenIndex = 1;

            // 先填偶数，偶数个数 <= 基数个数
            foreach( var currentIndex in stack)
            {
                int count = charCounts[currentIndex];
                charCounts[currentIndex] = -1;
                char c = (char)(currentIndex + 'a');

                for( int i = 0; i < count; i++ )
                {
                    r[evenIndex] = c;
                    evenIndex += 2;
                }
            }

            // 奇数
            for(var currentIndex = 0; currentIndex < charCounts.Length; currentIndex++)
            {
                int count = charCounts[currentIndex];
                if (count < 1) continue;

                charCounts[currentIndex] = -2;
                char c = (char)(currentIndex + 'a');

                for (int i = 0; i < count; i++)
                {
                    r[oddIndex] = c;
                    oddIndex += 2;
                }
            }

            return new string(r);

        //int totalCount = len;
        //bool isOdd = true;
        //int currentIndex = charCounts.Length - 1;
        //while (0 < totalCount)
        //{
        //    int count = charCounts[currentIndex];
        //    charCounts[currentIndex] = -1;
        //    totalCount -= count;
        //    char c = (char)(charIndexs[currentIndex] + 'a');
        //    currentIndex--;
        //    //int index;
        //    if(oddIndex < evenIndex)
        //    {
        //        //index = oddIndex;
        //        //oddIndex += 2 * count;
        //        int times = 0;
        //        while (oddIndex < len && times++ < count)
        //        {
        //            r[oddIndex] = c;
        //            oddIndex += 2;
        //        }
        //        if (times < count) return "";
        //    }
        //    else
        //    {
        //        //index = evenIndex;
        //        //evenIndex += 2 * count;

        //        int times = 0;
        //        while (evenIndex < len && times++ < count)
        //        {
        //            r[evenIndex] = c;
        //            evenIndex += 2;
        //        }
        //        if (times < count) return "";
        //    }
        //    //isOdd = !isOdd;
        //    //for (int i = index, times = 0; times < count; i += 2, times++) r[i] = c;
        //}
        //return new string(r);

        //int largeIndex = currentIndex--;
        //int smallIndex = currentIndex--;
        //StringBuilder ret = new StringBuilder(len);
        //while( 0 < charCounts[largeIndex] )
        //{
        //    ret.Append((char)(charIndexs[largeIndex] + 'a'));
        //    --charCounts[largeIndex];

        //    ret.Append((char)(charIndexs[smallIndex] + 'a'));
        //    --charCounts[smallIndex];

        //    if (charCounts[smallIndex] == 0)
        //    {
        //        if (charCounts[largeIndex] == 0)
        //        {
        //            if (-1 < currentIndex && 0 < charCounts[currentIndex]) largeIndex = currentIndex--;
        //            else break;
        //        }

        //        if (-1 < currentIndex && 0 < charCounts[currentIndex]) smallIndex = currentIndex--;
        //        else
        //        {
        //            ret.Append((char)(charIndexs[largeIndex] + 'a'));
        //            break;
        //        }

        //        if (charCounts[largeIndex] < charCounts[smallIndex])
        //        {
        //            (smallIndex, largeIndex) = (largeIndex, smallIndex);
        //        }
        //    }
        //}

        //return ret.ToString();
    }

    private static bool DFS(int[] charCounts, int index, int target, Stack<int> match )
    {
        for (int i = index; i < charCounts.Length; i++)
        {
            var v = charCounts[i];
            if (v == target)
            {
                match.Push(i);
                return true;
            }
            if (target < v) continue;

            match.Push(i);

            if (DFS(charCounts, i + 1, target - v, match)) return true;

            match.Pop();
        }
        return false;
    }
}