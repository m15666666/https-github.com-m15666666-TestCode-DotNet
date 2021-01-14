using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个字符串，找到它的第一个不重复的字符，并返回它的索引。如果不存在，则返回 -1。

示例：

s = "leetcode"
返回 0

s = "loveleetcode"
返回 2
 

提示：你可以假定该字符串只包含小写字母。

*/
/// <summary>
/// https://leetcode-cn.com/problems/first-unique-character-in-a-string/
/// 387. 字符串中的第一个唯一字符
/// 
/// 
/// 
/// </summary>
class FirstUniqueCharacterInAStringSolution
{
    public int FirstUniqChar(string s) {
        const int NotInit = -2;
        const int Duplicated = -1;
        const char a = 'a';
        int[] charIndexs = new int[26];
        Array.Fill(charIndexs, NotInit);
        int len = s.Length;
        for(int index = 0; index < len; index++)
        {
            var i = s[index] - a;
            var v = charIndexs[i];
            if(v == NotInit) charIndexs[i] = index;
            else if (v == Duplicated) continue;
            else charIndexs[i] = Duplicated;
        }
        int ret = len;
        for( int i = 0; i < charIndexs.Length; i++)
        {
            var v = charIndexs[i];
            if (-1 < v && v < ret) ret = v;
        }
        return ret == len ? -1 : ret;

    }
}
/*
字符串中的第一个唯一字符
力扣官方题解
发布于 2020-12-22
21.0k
方法一：使用哈希表存储频数
思路与算法

我们可以对字符串进行两次遍历。

在第一次遍历时，我们使用哈希映射统计出字符串中每个字符出现的次数。在第二次遍历时，我们只要遍历到了一个只出现一次的字符，那么就返回它的索引，否则在遍历结束后返回 -1−1。

代码


class Solution {
    public int firstUniqChar(String s) {
        Map<Character, Integer> frequency = new HashMap<Character, Integer>();
        for (int i = 0; i < s.length(); ++i) {
            char ch = s.charAt(i);
            frequency.put(ch, frequency.getOrDefault(ch, 0) + 1);
        }
        for (int i = 0; i < s.length(); ++i) {
            if (frequency.get(s.charAt(i)) == 1) {
                return i;
            }
        }
        return -1;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 是字符串 ss 的长度。我们需要进行两次遍历。

空间复杂度：O(|\Sigma|)O(∣Σ∣)，其中 \SigmaΣ 是字符集，在本题中 ss 只包含小写字母，因此 |\Sigma| \leq 26∣Σ∣≤26。我们需要 O(|\Sigma|)O(∣Σ∣) 的空间存储哈希映射。

方法二：使用哈希表存储索引
思路与算法

我们可以对方法一进行修改，使得第二次遍历的对象从字符串变为哈希映射。

具体地，对于哈希映射中的每一个键值对，键表示一个字符，值表示它的首次出现的索引（如果该字符只出现一次）或者 -1−1（如果该字符出现多次）。当我们第一次遍历字符串时，设当前遍历到的字符为 cc，如果 cc 不在哈希映射中，我们就将 cc 与它的索引作为一个键值对加入哈希映射中，否则我们将 cc 在哈希映射中对应的值修改为 -1−1。

在第一次遍历结束后，我们只需要再遍历一次哈希映射中的所有值，找出其中不为 -1−1 的最小值，即为第一个不重复字符的索引。如果哈希映射中的所有值均为 -1−1，我们就返回 -1−1。

代码


class Solution {
    public int firstUniqChar(String s) {
        Map<Character, Integer> position = new HashMap<Character, Integer>();
        int n = s.length();
        for (int i = 0; i < n; ++i) {
            char ch = s.charAt(i);
            if (position.containsKey(ch)) {
                position.put(ch, -1);
            } else {
                position.put(ch, i);
            }
        }
        int first = n;
        for (Map.Entry<Character, Integer> entry : position.entrySet()) {
            int pos = entry.getValue();
            if (pos != -1 && pos < first) {
                first = pos;
            }
        }
        if (first == n) {
            first = -1;
        }
        return first;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 是字符串 ss 的长度。第一次遍历字符串的时间复杂度为 O(n)O(n)，第二次遍历哈希映射的时间复杂度为 O(|\Sigma|)O(∣Σ∣)，由于 ss 包含的字符种类数一定小于 ss 的长度，因此 O(|\Sigma|)O(∣Σ∣) 在渐进意义下小于 O(n)O(n)，可以忽略。

空间复杂度：O(|\Sigma|)O(∣Σ∣)，其中 \SigmaΣ 是字符集，在本题中 ss 只包含小写字母，因此 |\Sigma| \leq 26∣Σ∣≤26。我们需要 O(|\Sigma|)O(∣Σ∣) 的空间存储哈希映射。

方法三：队列
思路与算法

我们也可以借助队列找到第一个不重复的字符。队列具有「先进先出」的性质，因此很适合用来找出第一个满足某个条件的元素。

具体地，我们使用与方法二相同的哈希映射，并且使用一个额外的队列，按照顺序存储每一个字符以及它们第一次出现的位置。当我们对字符串进行遍历时，设当前遍历到的字符为 cc，如果 cc 不在哈希映射中，我们就将 cc 与它的索引作为一个二元组放入队尾，否则我们就需要检查队列中的元素是否都满足「只出现一次」的要求，即我们不断地根据哈希映射中存储的值（是否为 -1−1）选择弹出队首的元素，直到队首元素「真的」只出现了一次或者队列为空。

在遍历完成后，如果队列为空，说明没有不重复的字符，返回 -1−1，否则队首的元素即为第一个不重复的字符以及其索引的二元组。

小贴士

在维护队列时，我们使用了「延迟删除」这一技巧。也就是说，即使队列中有一些字符出现了超过一次，但它只要不位于队首，那么就不会对答案造成影响，我们也就可以不用去删除它。只有当它前面的所有字符被移出队列，它成为队首时，我们才需要将它移除。

代码


class Solution {
    public int firstUniqChar(String s) {
        Map<Character, Integer> position = new HashMap<Character, Integer>();
        Queue<Pair> queue = new LinkedList<Pair>();
        int n = s.length();
        for (int i = 0; i < n; ++i) {
            char ch = s.charAt(i);
            if (!position.containsKey(ch)) {
                position.put(ch, i);
                queue.offer(new Pair(ch, i));
            } else {
                position.put(ch, -1);
                while (!queue.isEmpty() && position.get(queue.peek().ch) == -1) {
                    queue.poll();
                }
            }
        }
        return queue.isEmpty() ? -1 : queue.poll().pos;
    }

    class Pair {
        char ch;
        int pos;

        Pair(char ch, int pos) {
            this.ch = ch;
            this.pos = pos;
        }
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 是字符串 ss 的长度。遍历字符串的时间复杂度为 O(n)O(n)，而在遍历的过程中我们还维护了一个队列，由于每一个字符最多只会被放入和弹出队列最多各一次，因此维护队列的总时间复杂度为 O(|\Sigma|)O(∣Σ∣)，由于 ss 包含的字符种类数一定小于 ss 的长度，因此 O(|\Sigma|)O(∣Σ∣) 在渐进意义下小于 O(n)O(n)，可以忽略。

空间复杂度：O(|\Sigma|)O(∣Σ∣)，其中 \SigmaΣ 是字符集，在本题中 ss 只包含小写字母，因此 |\Sigma| \leq 26∣Σ∣≤26。我们需要 O(|\Sigma|)O(∣Σ∣) 的空间存储哈希映射以及队列。

public class Solution {
    public int FirstUniqChar(string s) {
          int[] arr = new int['z' + 1];
            for (int i = 0; i < s.Length; i++)
            {
                arr[s[i]]++;
            }
            for (int i = 0; i < s.Length; i++)
            {
                if (arr[s[i]] == 1) return i;
            }
            return -1;
    }
}



*/
