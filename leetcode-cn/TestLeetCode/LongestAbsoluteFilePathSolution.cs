using System;
using System.Collections.Generic;
using System.IO;

/*
假设我们以下述方式将我们的文件系统抽象成一个字符串:

字符串 "dir\n\tsubdir1\n\tsubdir2\n\t\tfile.ext" 表示:

dir
    subdir1
    subdir2
        file.ext
目录 dir 包含一个空的子目录 subdir1 和一个包含一个文件 file.ext 的子目录 subdir2 。

字符串 "dir\n\tsubdir1\n\t\tfile1.ext\n\t\tsubsubdir1\n\tsubdir2\n\t\tsubsubdir2\n\t\t\tfile2.ext" 表示:

dir
    subdir1
        file1.ext
        subsubdir1
    subdir2
        subsubdir2
            file2.ext
目录 dir 包含两个子目录 subdir1 和 subdir2。 subdir1 包含一个文件 file1.ext 和一个空的二级子目录 subsubdir1。subdir2 包含一个二级子目录 subsubdir2 ，其中包含一个文件 file2.ext。

我们致力于寻找我们文件系统中文件的最长 (按字符的数量统计) 绝对路径。例如，在上述的第二个例子中，最长路径为 "dir/subdir2/subsubdir2/file2.ext"，其长度为 32 (不包含双引号)。

给定一个以上述格式表示文件系统的字符串，返回文件系统中文件的最长绝对路径的长度。 如果系统中没有文件，返回 0。

说明:

文件名至少存在一个 . 和一个扩展名。
目录或者子目录的名字不能包含 .。
要求时间复杂度为 O(n) ，其中 n 是输入字符串的大小。

请注意，如果存在路径 aaaaaaaaaaaaaaaaaaaaa/sth.png 的话，那么  a/aa/aaa/file1.txt 就不是一个最长的路径。
*/

/// <summary>
/// https://leetcode-cn.com/problems/longest-absolute-file-path/
/// 388. 文件的最长绝对路径
/// http://www.cnblogs.com/lightwindy/p/8491377.html
/// </summary>
internal class LongestAbsoluteFilePathSolution
{
    public void Test()
    {
        var ret = LengthLongestPath("dir\n\tsubdir1\n\tsubdir2\n\t\tfile.ext");
    }

    public int LengthLongestPath(string input)
    {
        // StreamReader a = new StreamReader();
        using (StringReader reader = new StringReader(input))
        {
            int max = 0;
            Dictionary<int, int> lengths = new Dictionary<int, int> { { 0, 0 } };
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                int index = line.LastIndexOf('\t');
                int len = line.Length - 1 - index;
                int preLevel = index == -1 ? 0 : index + 1;
                if (-1 < line.IndexOf('.')) max = Math.Max(max, lengths[preLevel] + len);
                else lengths[preLevel + 1] = lengths[preLevel] + 1 + len;
            }
            return max;
        }
    }

    //public int LengthLongestPath(string input)
    //{
    //    var lines = input.Split('\n');
    //    int[] lengths = new int[lines.Length + 1];
    //    lengths[0] = 0;
    //    int max = 0;
    //    foreach (var line in lines)
    //    {
    //        int index = line.LastIndexOf('\t');
    //        int len = line.Length - 1 - index;
    //        int preLevel = index == -1 ? 0 : index + 1;
    //        if ( -1 < line.IndexOf('.') ) max = Math.Max(max, lengths[preLevel] + len);
    //        else lengths[preLevel + 1] = lengths[preLevel] + 1 + len;
    //    }
    //    return max;
    //}
}
/*
【中规中矩】求最长路径（哈希表加stringstream）
卖码翁
发布于 2020-12-14
208
解题思路
用stringstream+getline以'\n'为分割符分割字符串为若干行。建立层次到其文件夹前缀的长度的映射。对特定的path根据里面是否含有'.'来判断是否是文件。是文件的话用mp[level] + len有条件的更新最大长度，否则更新新文件夹及其长度到map表：mp[level+1] = mp[level] + len + 1，为可能的其下文件长度做准备。

易错的地方：
初始条件 mp[0] = 0
如果没有\t是被认为目录level 1，有一个\t被认为目录level 2
查找tab \t要用find_last_of而不能用find，find找到的是第一个。
别忘了dir后面的/，也占一个长度
同时个人认为该题目有隐含假设，说有点同级子目录的长度相同，也就是说可以运行sub1, sub2的情况（长度都为4），但不会大出现longdir1, shortdir2 (长度不同)。

代码

class Solution {
public:
    int lengthLongestPath(string input) {
        assert(!input.empty() && "Input cannot be null!");
        stringstream ss(input);
        vector<string> paths;
        string path;
        while (getline(ss, path, '\n')) {
            paths.push_back(path);
        }

        int res = 0;
        unordered_map<int, int> mp{{0, 0}};
        for (const auto& path : paths) {
            int level = path.find_last_of('\t') + 1;
            // above code equivalent to the follow n lines of code
            // int pos = path.find_last_of('\t');
            // next pos of \t (if no \t, level = 0)
            //     if (pos == string::npos) {
            //     pos = 0;
            //     }  else {
            //       pos += 1;
            //     }

            int len = path.substr(level).size();
            if (path.find('.') != string::npos) {
                // file
                res = max(res, mp[level] + len);
            } else {
                mp[level + 1] = mp[level] + len + 1 ;// \n
            }
        }

        return res;
    }
};

【golang】栈方法
珊璞桑
发布于 2020-08-26
126
解题思路
参考powercai的解法。

1、用\n来切分路径input，得到分层的路径s
2、遍历s，根据其包含的\t的个数，得到其缩进层次，及该层的路径paths，比如

\t\tfile.ext
可以得到如下数据

Path{Content: "file.ext", Level: 2}
3、遍历paths，利用栈stack来处理路径，当碰到带.的Path.Content，说明碰到了文件名，可以计算一次长度
4、如果不是文件名，就看看当前栈顶缩进是不是小于当前遍历的路径，如果不是，就弹出
5、最后看看最大长度是多少
代码

func lengthLongestPath(input string) int {
	s := strings.Split(input, "\n")
	var paths []Path // 所有路径，包括缩进层次和路径
	for _, v := range s {
		level := strings.Count(v, "\t")
		paths = append(paths, Path{v[level:], level})
	}
	var stack []Path
	var result int
	for _, v := range paths {
		// 总是保证栈顶是小于该文件的缩进的
		for len(stack) != 0 && stack[len(stack)-1].Level >= v.Level {
			stack = stack[:len(stack)-1]
		}
		stack = append(stack, v)
		if strings.Contains(v.Content, ".") {
			result = max(result, calLen(stack))
		}
	}
	return result
}

type Path struct {
	Content string
	Level   int
}

func calLen(stack []Path) int {
	result := 0
	for _, v := range stack {
		result += len(v.Content)
	}
	// 多加 len(stack) - 1 个 "/"
	return result + len(stack) - 1
}

func max(num1, num2 int) int {
	if num1 > num2 {
		return num1
	}
	return num2
}

public class Solution {
    public int LengthLongestPath(string input) {
        int ans = 0;
        List<int> dirLen = new List<int>(){0};
        int curDepth = 0;
        int totalLen = 0;
        for (int i=0;i<input.Length;i++) {
            if (input[i] == '\n') {
                curDepth = 0;
                i++;
                while (input[i] == '\t') {
                    i += 1;
                    curDepth++;
                    if (curDepth >= dirLen.Count) {
                        dirLen.Add(0);
                    }
                }
            }
            int left = i;
            bool isFile = false;
            while (i+1 < input.Length && input[i+1] != '\n' && input[i+1] != '\t') {
                if (input[i+1] == '.') {
                    isFile = true;
                }
                i++;
            }
            dirLen[curDepth] = i - left + 1;
            //Console.WriteLine("cur dir = {0},depth = {1}",input.Substring(left,i-left+1),curDepth);
            if (isFile) {
                totalLen = 0;
                for (int j=0;j<=curDepth;j++) {
                    totalLen += dirLen[j];
                }
                totalLen += curDepth;
                ans = Math.Max(totalLen,ans);
            }
        }
        return ans;
    }
}

*/