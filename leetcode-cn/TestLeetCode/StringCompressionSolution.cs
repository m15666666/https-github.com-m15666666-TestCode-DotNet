/*
给定一组字符，使用原地算法将其压缩。

压缩后的长度必须始终小于或等于原数组长度。

数组的每个元素应该是长度为1 的字符（不是 int 整数类型）。

在完成原地修改输入数组后，返回数组的新长度。

 

进阶：
你能否仅使用O(1) 空间解决问题？

 

示例 1：

输入：
["a","a","b","b","c","c","c"]
输出：
返回 6 ，输入数组的前 6 个字符应该是：["a","2","b","2","c","3"]

说明：
"aa" 被 "a2" 替代。"bb" 被 "b2" 替代。"ccc" 被 "c3" 替代。
示例 2：

输入：
["a"]
输出：
返回 1 ，输入数组的前 1 个字符应该是：["a"]

解释：
没有任何字符串被替代。
示例 3：

输入：
["a","b","b","b","b","b","b","b","b","b","b","b","b"]
输出：
返回 4 ，输入数组的前4个字符应该是：["a","b","1","2"]。

解释：
由于字符 "a" 不重复，所以不会被压缩。"bbbbbbbbbbbb" 被 “b12” 替代。
注意每个数字在数组中都有它自己的位置。
 

提示：

所有字符都有一个ASCII值在[35, 126]区间内。
1 <= len(chars) <= 1000。
通过次数26,404提交次数63,260

*/

/// <summary>
/// https://leetcode-cn.com/problems/string-compression/
/// 443. 压缩字符串
///
/// </summary>
internal class StringCompressionSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int Compress(char[] chars)
    {
        if (chars == null || chars.Length == 0) return 0;

        int writeIndex = 0;
        int len = chars.Length;
        for (int readIndex = 0, startIndex = 0; readIndex < len; readIndex++)
            if (readIndex + 1 == len || chars[readIndex + 1] != chars[readIndex])
            {
                chars[writeIndex++] = chars[startIndex];
                if (startIndex < readIndex)
                    foreach (char c in (readIndex - startIndex + 1).ToString())
                        chars[writeIndex++] = c;
                startIndex = readIndex + 1;
            }

        return writeIndex;
    }
}

/*
压缩字符串
力扣 (LeetCode)

发布于 2019-07-28
20.8k
方法一：双指针 【通过】
直觉

我们使用两个指针 read 和 write 分别标记读和写的位置。读写操作均从左到右进行：读入连续的一串字符，然后将压缩版写到数组中。最终，write 将指向输出答案的结尾。

算法

保留指针 anchor，指向当前读到连续字符串的起始位置。

从左到右进行读取。当读到最后一个字符，或者下一个下一个字符与当前不同时，则到达连续区块的结尾。

当我们到达连续区块的结尾时，就从 write 写入压缩的结果。chars[anchor] 为字符，read - anchor + 1 （若大于 1）为长度。


class Solution {
    public int compress(char[] chars) {
        int anchor = 0, write = 0;
        for (int read = 0; read < chars.length; read++) {
            if (read + 1 == chars.length || chars[read + 1] != chars[read]) {
                chars[write++] = chars[anchor];
                if (read > anchor) {
                    for (char c: ("" + (read - anchor + 1)).toCharArray()) {
                        chars[write++] = c;
                    }
                }
                anchor = read + 1;
            }
        }
        return write;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是 chars 的长度。

空间复杂度：O(1)O(1)，三个指针的占用空间。

public class Solution {
    public int Compress(char[] chars) {
		var length = chars.Length;
		int slow = 0, fast = 0, cur = 0;
		while (fast < length) {
			if (chars[fast] != chars[slow]) {
				chars[cur++] = chars[slow];
				if ((fast - slow) >= 2) {
					foreach (var ch in (fast - slow).ToString()) {
						chars[cur++] = ch;
					}
				}
				slow = fast;
			}
			fast++;
		}
		chars[cur++] = chars[slow];
		if ((fast - slow) >= 2) {
			foreach (var ch in (fast - slow).ToString()) {
				chars[cur++] = ch;
			}
		}
		return chars.Take(cur).Count();
    }
}
*/