using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个非空数组，数组中元素为 a0, a1, a2, … , an-1，其中 0 ≤ ai < 231 。

找到 ai 和aj 最大的异或 (XOR) 运算结果，其中0 ≤ i,  j < n 。

你能在O(n)的时间解决这个问题吗？

示例:

输入: [3, 10, 5, 25, 2, 8]

输出: 28

解释: 最大的结果是 5 ^ 25 = 28. 
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-xor-of-two-numbers-in-an-array/
/// 421. 数组中两个数的最大异或值
/// https://www.cnblogs.com/kexinxin/p/10242217.html
/// </summary>
class MaximumXOROfTwoNumbersInAnArraySolution
{
    public void Test()
    {
        var ret = FindMaximumXOR(new int[] { 3, 10, 5, 25, 2, 8 });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindMaximumXOR(int[] nums)
    {
        int maxNum = nums.Max();
        if (maxNum == 0) return 0;
        int width = 0; // 位宽度: 最高位的1在第几位
        while(0 < maxNum)
        {
            maxNum >>= 1;
            width++;
        }

        int ret = 0;
        int mask = 0;
        HashSet<int> set = new HashSet<int>();
        //for (int i = 31; i >= 0; i--)
        for (int nextOneBit = 1 << (width - 1); 0 < nextOneBit; nextOneBit >>= 1)
        {
            set.Clear();

            // 从最高位试着找nums的前缀
            mask |= nextOneBit;
            foreach (int num in nums)
            {
                int prefix = mask & num;
                if (!set.Contains(prefix)) set.Add(prefix);
            }

            //判断最大异或结果的当前位是否为1
            int nextMax = ret | nextOneBit;
            foreach (int prefix in set)
            {
                // 包含亦或互补的两个数
                if (set.Contains(prefix ^ nextMax))
                {
                    ret = nextMax;
                    break;
                }
            }
        }
        return ret;
    }
}
/*
数组中两个数的最大异或值
力扣 (LeetCode)

发布于 2020-03-31
12.4k
概述
题目要求 O(N)O(N) 时间复杂度，下面会讨论两种典型的 O(N)O(N) 复杂度解法。

利用哈希集合存储按位前缀。
利用字典树存储按位前缀。
这两种解法背后的思想是一样的，都是先将整数转化成二进制形式，再从最左侧的比特位开始逐一处理来构建最大异或值。两个方法的不同点在于采用了不同的数据结构来存储按位前缀。第一个方法在给定的测试集下执行速度更快，但第二种方法更加普适，更加简单。

基础知识

0 和任意比特 x 异或结果还是 x 本身。

0 \oplus x = x
0⊕x=x

如果a，b两个值相同，异或结果为0

x \oplus x = 0
x⊕x=0

方法一：利用哈希集合存储按位前缀
假定数组为 [3, 10, 5, 25, 2, 8]，首先将其中的整数转化成二进制形式：

3 = (00011)_23=(00011) 
2
​	
 

10 = (01010)_210=(01010) 
2
​	
 

5 = (00101)_25=(00101) 
2
​	
 

25 = (11001)_225=(11001) 
2
​	
 

2 = (00010)_22=(00010) 
2
​	
 

8 = (01000)_28=(01000) 
2
​	
 

为了简化按位前缀的计算，将所有数转化成二进制形式之后，需要在左边补 0 使得所有数对齐。最终所有数的长度都为 LL，其中 LL 为最大数的二进制长度。

之后我们就可以从最左侧的比特位开始构建最大异或值了，在 L = 5L=5 的情况下，可能的最大异或值为 (11111)_2(11111) 
2
​	
 。

首先检查最左侧的比特位有可能使其为 1 嘛？（即 (1****)_2(1∗∗∗∗) 
2
​	
  这种形式）。
显然是可以的，只要将 25 = (11001)_225=(11001) 
2
​	
  和另一个最左侧比特位为 0 的数（2，3，5，8，10）异或就可以了，这时候就得到了 (1****)_2(1∗∗∗∗) 
2
​	
 。

继续下一步，有可能使得最左侧两个比特位都为 1 嘛？（即 (11***)_2(11∗∗∗) 
2
​	
  这种形式）
这时候考虑所有长度为 2 的按位前缀，检查是否有 p_1p 
1
​	
 ，p_2p 
2
​	
  这样的组合，使得 p_1 \oplus p_2 == 11p 
1
​	
 ⊕p 
2
​	
 ==11。

3 = (00***)_23=(00∗∗∗) 
2
​	
 

10 = (01***)_210=(01∗∗∗) 
2
​	
 

5 = (00***)_25=(00∗∗∗) 
2
​	
 

25 = (11***)_225=(11∗∗∗) 
2
​	
 

2 = (00***)_22=(00∗∗∗) 
2
​	
 

8 = (01***)_28=(01∗∗∗) 
2
​	
 

显然在这个数组里面是有的，如 5 = (00***)_25=(00∗∗∗) 
2
​	
  和 25 = (11***)_225=(11∗∗∗) 
2
​	
  异或，或者 2 = (00***)_22=(00∗∗∗) 
2
​	
  和 25 = (11***)_225=(11∗∗∗) 
2
​	
  异或，又或者 3 = (00***)_23=(00∗∗∗) 
2
​	
  和 25 = (11***)_225=(11∗∗∗) 
2
​	
  异或。

按这种方式一比特一比特处理下去就可以得到最大异或值。在这过程中需要检查各种前缀的异或结果，但由于长度为 L - iL−i 的前缀数量不会超过 2^{L - i}2 
L−i
 ，因此判断第 ii 个比特位是否有可能为 1 最多需要执行 2^{L - i} \times 2^{L - i}2 
L−i
 ×2 
L−i
  次操作。

算法

首先计算数组中最大数的二进制长度 LL。

初始化 max_xor = 0。

从 i = L - 1i=L−1 遍历到 i = 0i=0（代表着从最左侧的比特位 L - 1L−1 遍历到最右侧的比特位 00）：

将 max_xor 左移，释放出下一比特位的位置。

初始化 curr_xor = max_xor | 1（即将 max_xor 最右侧的比特置为 1）。

遍历 nums，计算出长度为 L - iL−i 的所有可能的按位前缀。

将长度为 L - iL−i 的按位前缀加入哈希集合 prefixes，按位前缀的计算公式如下：num >> i。
遍历所有可能的按位前缀，检查是否存在 p1，p2 使得 p1^p2 == curr_xor。比较简单的做法是检查每个 p，看 curr_xor^p 是否存在。如果存在，就将 max_xor 改为 curr_xor（即将 max_xor 最右侧的比特位改为 1）。如果不存在，max_xor 最右侧的比特位继续保持为 0。

返回 max_xor。


class Solution {
  public int findMaximumXOR(int[] nums) {
    int maxNum = nums[0];
    for(int num : nums) maxNum = Math.max(maxNum, num);
    // length of max number in a binary representation
    int L = (Integer.toBinaryString(maxNum)).length();

    int maxXor = 0, currXor;
    Set<Integer> prefixes = new HashSet<>();
    for(int i = L - 1; i > -1; --i) {
      // go to the next bit by the left shift
      maxXor <<= 1;
      // set 1 in the smallest bit
      currXor = maxXor | 1;
      prefixes.clear();
      // compute all possible prefixes 
      // of length (L - i) in binary representation
      for(int num: nums) prefixes.add(num >> i);
      // Update maxXor, if two of these prefixes could result in currXor.
      // Check if p1^p2 == currXor, i.e. p1 == currXor^p2.
      for(int p: prefixes) {
        if (prefixes.contains(currXor^p)) {
          maxXor = currXor;
          break;
        }
      }
    }
    return maxXor;
  }
}
复杂度分析

时间复杂度：O(N)O(N)。计算按位前缀需要遍历 nums 数组，复杂度为 NN，计算所有可能按位前缀的异或结果复杂度为 2^{L - i} \times 2^{L - i}2 
L−i
 ×2 
L−i
 。最终复杂度为 \sum_{i = 0}^{L - 1}{(N + 4^{L - i})} = NL + \frac{4}{3}(4^L - 1)∑ 
i=0
L−1
​	
 (N+4 
L−i
 )=NL+ 
3
4
​	
 (4 
L
 −1)，即 O(N)O(N) 复杂度。

空间复杂度：O(1)O(1)。最长的按位前缀长度为 LL，同时 L = 1 + [\log_2 M]L=1+[log 
2
​	
 M]，其中 M 为 nums 中的最大数值。

方法二：逐位字典树
为什么哈希集合不适合用来存储按位前缀？

对于那些一定不能得到最终解的路径可以通过剪枝来舍弃，但是用哈希集合来存储按位前缀是没法做剪枝优化的。举个例子，两次异或操作之后为了得到 (11***)_2(11∗∗∗) 
2
​	
 ，显然只能让 25 和 最左侧为 0000 前缀的数字（2，3， 5）组合。

3 = (00011)_23=(00011) 
2
​	
 

10 = (01010)_210=(01010) 
2
​	
 

5 = (00101)_25=(00101) 
2
​	
 

25 = (11001)_225=(11001) 
2
​	
 

2 = (00010)_22=(00010) 
2
​	
 

8 = (01000)_28=(01000) 
2
​	
 

因此，在计算第三位比特的时候，我们就没有必要计算所有可能的按位前缀组合了。光看前两位就知道一些组合已经不能得到最大异或值了。

3 = (000**)_23=(000∗∗) 
2
​	
 

10 = (010**)_210=(010∗∗) 
2
​	
 

5 = (001**)_25=(001∗∗) 
2
​	
 

25 = (110**)_225=(110∗∗) 
2
​	
 

2 = (000**)_22=(000∗∗) 
2
​	
 

8 = (010**)_28=(010∗∗) 
2
​	
 

为了方便剪枝，我们要采用一种类树的存储结构。

按位字典树：这是什么？怎么构建？

假设数组为 [3, 10, 5, 25, 2]，据此来构建按位字典树。

3 = (00011)_23=(00011) 
2
​	
 

10 = (01010)_210=(01010) 
2
​	
 

5 = (00101)_25=(00101) 
2
​	
 

25 = (11001)_225=(11001) 
2
​	
 

2 = (00010)_22=(00010) 
2
​	
 

fig

字典树中每条根节点到叶节点的路径都代表了 nums 中的一个整数（二进制形式），举个例子，0 -> 0 -> 0 -> 1 -> 1 表示 3。与之前的方法一样，所有二进制的长度都为 LL，其中 $$L = 1 + [\log_2 M]$$，这里 M 为 nums 中的最大数值。显然字典树的深度也为 LL，同时叶子节点也都在同一层。

字典树非常适合用来存储整数的二进制形式，例如存储 2（00010） 和 3（00011），其中 5 个比特位中有 4 个比特位都是相同的。字典树的构建方式也很简单，就是嵌套哈希表。在每一步，判断要增加的孩子节点（0，1）是否已经存在，如果存在就直接访问该孩子节点。如果不存在，需要先新增孩子节点再访问。


TrieNode trie = new TrieNode();
for (String num : strNums) {
  TrieNode node = trie;
  for (Character bit : num.toCharArray()) { 
    if (node.children.containsKey(bit)) {
      node = node.children.get(bit);
    } else {
      TrieNode newNode = new TrieNode();
      node.children.put(bit, newNode);
      node = newNode;
    }
  }  
}
字典树中给定数的最大异或值

为了最大化异或值，需要在每一步找到当前比特值的互补比特值。下图展示了 25 在每一步要怎么走才能得到最大异或值：

fig

实现方式也很简单：

如果当前比特值存在互补比特值，访问具有互补比特值的孩子节点，并在异或值最右侧附加一个 1。

如果不存在，直接访问具有当前比特值的孩子节点，并在异或值最右侧附加一个 0。


TrieNode trie = new TrieNode();
for (String num : strNums) {
  TrieNode xorNode = trie;
  int currXor = 0;
  for (Character bit : num.toCharArray()) {
    Character toggledBit = bit == '1' ? '0' : '1';
    if (xorNode.children.containsKey(toggledBit)) {
      currXor = (currXor << 1) | 1;
      xorNode = xorNode.children.get(toggledBit);
    } else {
      currXor = currXor << 1;
      xorNode = xorNode.children.get(bit);
    }
  }
}
算法

算法结构如下所示：

在按位字典树中插入数字。

找到插入数字在字典树中所能得到的最大异或值。

算法的具体实现如下所示：

将所有数字转化成二进制形式。

将数字的二进制形式加入字典树，同时计算该数字在字典树中所能得到的最大异或值。再用该数字的最大异或值尝试性更新 max_xor。

返回 max_xor。

实现


class TrieNode {
  HashMap<Character, TrieNode> children = new HashMap<Character, TrieNode>();
  public TrieNode() {}
}

class Solution {
  public int findMaximumXOR(int[] nums) {
    // Compute length L of max number in a binary representation
    int maxNum = nums[0];
    for(int num : nums) maxNum = Math.max(maxNum, num);
    int L = (Integer.toBinaryString(maxNum)).length();

    // zero left-padding to ensure L bits for each number
    int n = nums.length, bitmask = 1 << L;
    String [] strNums = new String[n];
    for(int i = 0; i < n; ++i) {
      strNums[i] = Integer.toBinaryString(bitmask | nums[i]).substring(1);
    }

    TrieNode trie = new TrieNode();
    int maxXor = 0;
    for (String num : strNums) {
      TrieNode node = trie, xorNode = trie;
      int currXor = 0;
      for (Character bit : num.toCharArray()) {
        // insert new number in trie  
        if (node.children.containsKey(bit)) {
          node = node.children.get(bit);
        } else {
          TrieNode newNode = new TrieNode();
          node.children.put(bit, newNode);
          node = newNode;
        }

        // compute max xor of that new number 
        // with all previously inserted
        Character toggledBit = bit == '1' ? '0' : '1';
        if (xorNode.children.containsKey(toggledBit)) {
          currXor = (currXor << 1) | 1;
          xorNode = xorNode.children.get(toggledBit);
        } else {
          currXor = currXor << 1;
          xorNode = xorNode.children.get(bit);
        }
      }
      maxXor = Math.max(maxXor, currXor);
    }

    return maxXor;
  }
}
复杂度分析

时间复杂度：O(N)O(N)。在字典树插入一个数的时间复杂度为 O(L)O(L)，找到一个数的最大异或值时间复杂度也为 O(L)O(L)。其中 L = 1 + [\log_2 M]L=1+[log 
2
​	
 M]，M 为数组中的最大数值，这里可以当做一个常量。因此最终时间复杂度为 O(N)O(N)。

空间复杂度：O(1)O(1)。维护字典树最多需要 O(2^L) = O(M)O(2 
L
 )=O(M) 的空间，但由于输入的限制，这里的 L 和 M 可以当做常数。

public class Solution {
    public int FindMaximumXOR(int[] nums) {
        MyTreeNode root = new MyTreeNode();
        MyTreeNode t;
        foreach (int n in nums) {
            t = root;
            for (int i = 30; i >= 0; i--) {
                if ((n & (1 << i)) > 0) {
                    if (t.one == null) {
                        t.one = new MyTreeNode();
                    }
                    t = t.one;
                }
                else {
                    if (t.zero == null) {
                        t.zero = new MyTreeNode();
                    }
                    t = t.zero;
                }
            }
        }
        
        int res = 0;
        foreach (int n in nums) {
            t = root;
            int tmp = 0;
            for (int i = 30; i >= 0; i--) {
                if ((n & (1 << i)) > 0) {
                    if (t.zero != null) {
                        t = t.zero;
                        tmp += (1 << i);
                    }
                    else {
                        t = t.one;
                    }
                }
                else {
                    if (t.one != null) {
                        t = t.one;
                        tmp += (1 << i);
                    }
                    else {
                        t = t.zero;
                    }
                }
            }
            res = Math.Max(res, tmp);
        }
        return res;
    }
}

public class MyTreeNode {
    public MyTreeNode zero;
    public MyTreeNode one;

    public MyTreeNode() {
        zero = null;
        one = null;
    }
}

public class Solution
{
    private int ret = 0;
    public class TrieNode
    {
        public TrieNode[] son;
        public int val;
        public TrieNode(int val)
        {
            son = new TrieNode[2];
            this.val = val;
        }
    }
    public int FindMaximumXOR(int[] nums)
    {
        TrieNode root = new TrieNode(-1);
        foreach(var n in nums)
        {
            TrieNode tnode = root;
            for(int i=31;i>=0;i--)
            {
                int val = ((n & (1 << i)) != 0) ? 1 : 0;
                if (tnode.son[val]==null)
                {
                    tnode.son[val] = new TrieNode(val);
                }
                else
                {
                }
                tnode = tnode.son[val];
            }
        }
        TrieNode node = root;
        int count = 32;
        while(true)
        {
            if(node.son[0]==null && node.son[1]==null)
            {
                return 0;
            }
            if(node.son[0] != null && node.son[1] != null)
            {
                break;
            }
            if(node.son[0]!=null)
            {
                node = node.son[0];
            }
            else if(node.son[1]!=null)
            {
                node = node.son[1];
            }
            count -= 1;
        }
        TrieNode node0 = node.son[0];
        TrieNode node1 = node.son[1];
        func(node0, node1, 0);
        return ret;
    }
    public void func(TrieNode node0,TrieNode node1,int k)
    {
        k = k * 2 + (node0.val ^ node1.val);
        if(node0.son[0] != null && node0.son[1] != null && node1.son[0] != null && node1.son[1] != null)
        {
            func(node0.son[0], node1.son[1], k);
            func(node0.son[1], node1.son[0], k);
        }
        else if(node0.son[0]!=null && node0.son[1]!=null && node1.son[1]!=null)
        {
            func(node0.son[0], node1.son[1], k);
        }
        else if (node0.son[0] != null && node0.son[1] != null && node1.son[0] != null)
        {
            func(node0.son[1], node1.son[0], k);
        }
        else if(node0.son[1]!=null&&node1.son[0]!=null&&node1.son[0]!=null)
        {
            func(node0.son[1], node1.son[0], k);
        }
        else if (node0.son[0] != null && node1.son[0] != null && node1.son[1] != null)
        {
            func(node0.son[0], node1.son[1], k);
        }
        else if (node0.son[0] == null && node0.son[1] == null)
        {
            ret = Math.Max(k, ret);
            return;
        }
        else
        {
            TrieNode t0 = node0.son[0] == null ? node0.son[1] : node0.son[0];
            TrieNode t1 = node1.son[0] == null ? node1.son[1] : node1.son[0];
            func(t0, t1, k);
        }
    }
} 
*/
