using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
第 i 个人的体重为 people[i]，每艘船可以承载的最大重量为 limit。

每艘船最多可同时载两人，但条件是这些人的重量之和最多为 limit。

返回载到每一个人所需的最小船数。(保证每个人都能被船载)。

 

示例 1：

输入：people = [1,2], limit = 3
输出：1
解释：1 艘船载 (1, 2)
示例 2：

输入：people = [3,2,2,1], limit = 3
输出：3
解释：3 艘船分别载 (1, 2), (2) 和 (3)
示例 3：

输入：people = [3,5,3,4], limit = 5
输出：4
解释：4 艘船分别载 (3), (3), (4), (5)
提示：

1 <= people.length <= 50000
1 <= people[i] <= limit <= 30000
*/
/// <summary>
/// https://leetcode-cn.com/problems/boats-to-save-people/
/// 881. 救生艇
/// 
/// </summary>
class BoatsToSavePeopleSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NumRescueBoats(int[] people, int limit)
    {
        Array.Sort(people);

        int lightIndex = 0;
        int heavyIndex = people.Length - 1;
        int ret = 0;

        while (lightIndex <= heavyIndex)
        {
            ret++;
            if (people[lightIndex] + people[heavyIndex] <= limit) lightIndex++;
            heavyIndex--;
        }

        return ret;
    }
}
/*
方法：贪心（双指针）
思路

如果最重的人可以与最轻的人共用一艘船，那么就这样安排。否则，最重的人无法与任何人配对，那么他们将自己独自乘一艘船。

这么做的原因是，如果最轻的人可以与任何人配对，那么他们也可以与最重的人配对。

算法

令 people[i] 指向当前最轻的人，而 people[j] 指向最重的那位。

然后，如上所述，如果最重的人可以与最轻的人共用一条船（即 people[j] + people[i] <= limit），那么就这样做；否则，最重的人自己独自坐在船上。

C++JavaPython
class Solution {
public:
    int numRescueBoats(vector<int>& people, int limit) {
        sort(people.begin(), people.end());
        int i = 0, j = people.size() - 1;
        int ans = 0;

        while (i <= j) {
            ans++;
            if (people[i] + people[j] <= limit)
                i++;
            j--;
        }

        return ans;
    }
};
复杂度分析

时间复杂度：O(NlogN)，其中 NN 是 people 的长度。

空间复杂度：O(N)。

public class Solution {
    public int NumRescueBoats(int[] people, int limit) {
        int a=0;
	    	int []n=new int[30001];
	    	for (int i = 0; i < people.Length; i++) {
				n[people[i]]++;
			}
	    	for (int i = 30000; i >0; i--) {
	    		if(n[i]>0)
	    		{
                    bool p=true;
				    for (int j = 0; j <= limit-i; j++) {
					int z=limit-i-j;
					if(z==i&&i*2<=limit)
					{
						a+=n[i]/2;
						n[i]=n[i]%2==0?0:1;
					}
					else if(n[z]>=n[i])
					{
						p=false;
						a+=n[i];
						n[z]-=n[i];
						n[i]=0;
						break;
					}
					else{
						n[i]-=n[z];
						a+=n[z];
						n[z]=0;
					}
				    }
				    if(p)
				    {
					    a+=n[i];
					    n[i]=0;
				    }
				}
			}
	    	return a;
    }
}

public class Solution {
    public int NumRescueBoats(int[] people, int limit) {
        Array.Sort(people);
        
        int ans = 0;
        
        int l = 0;
        int r = people.Length-1;
        
        while(l <=r){
            if(people[r] >= limit){
                ans++;
                r--;
                continue;
            }
            if(people[r]+people[l] > limit){
                ans++;
                r--;
            }
            else{
                ans++;
                r--;
                l++;
            }
        }
        
        return ans;
    }
}
*/
