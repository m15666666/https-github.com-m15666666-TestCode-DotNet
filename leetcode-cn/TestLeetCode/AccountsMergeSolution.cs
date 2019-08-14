using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个列表 accounts，每个元素 accounts[i] 是一个字符串列表，其中第一个元素 accounts[i][0] 是 名称 (name)，其余元素是 emails 表示该帐户的邮箱地址。

现在，我们想合并这些帐户。如果两个帐户都有一些共同的邮件地址，则两个帐户必定属于同一个人。请注意，即使两个帐户具有相同的名称，它们也可能属于不同的人，因为人们可能具有相同的名称。一个人最初可以拥有任意数量的帐户，但其所有帐户都具有相同的名称。

合并帐户后，按以下格式返回帐户：每个帐户的第一个元素是名称，其余元素是按顺序排列的邮箱地址。accounts 本身可以以任意顺序返回。

例子 1:

Input: 
accounts = [["John", "johnsmith@mail.com", "john00@mail.com"], ["John", "johnnybravo@mail.com"], ["John", "johnsmith@mail.com", "john_newyork@mail.com"], ["Mary", "mary@mail.com"]]
Output: [["John", 'john00@mail.com', 'john_newyork@mail.com', 'johnsmith@mail.com'],  ["John", "johnnybravo@mail.com"], ["Mary", "mary@mail.com"]]
Explanation: 
  第一个和第三个 John 是同一个人，因为他们有共同的电子邮件 "johnsmith@mail.com"。 
  第二个 John 和 Mary 是不同的人，因为他们的电子邮件地址没有被其他帐户使用。
  我们可以以任何顺序返回这些列表，例如答案[['Mary'，'mary@mail.com']，['John'，'johnnybravo@mail.com']，
  ['John'，'john00@mail.com'，'john_newyork@mail.com'，'johnsmith@mail.com']]仍然会被接受。

注意：

accounts的长度将在[1，1000]的范围内。
accounts[i]的长度将在[1，10]的范围内。
accounts[i][j]的长度将在[1，30]的范围内。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/accounts-merge/
/// 721. 账户合并
/// https://blog.csdn.net/SundyGuo/article/details/80910059
/// </summary>
class AccountsMergeSolution
{
    public void Test()
    {
        IList<IList<string>> accounts = new List<IList<string>>() { new [] { "John", "johnsmith@mail.com", "john_newyork@mail.com" }, new[] { "John", "johnsmith@mail.com", "john00@mail.com" }, new[] { "Mary", "mary@mail.com" }, new[] { "John", "johnnybravo@mail.com" } };
        var ret = AccountsMerge(accounts);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
    {
        int[] personArray = new int[accounts.Count];
        for (int n = 0; n < personArray.Length; n++)
        {
            personArray[n] = n;
        }

        Dictionary<string, int> email2Positioin = new Dictionary<string, int>();
        Dictionary<int, List<string>> position2EmailList = new Dictionary<int, List<string>>();

        for (int i = 0; i < accounts.Count; i++)
        {
            var account = accounts[i];
            for (int j = 1; j < account.Count; j++) // 0 index是用户名
            {
                var email = account[j];
                if (email2Positioin.ContainsKey(email))
                    UnionRoot(personArray, i, email2Positioin[email]);
                else
                    email2Positioin.Add(email, i);
            }
        }

        foreach (var pair in email2Positioin)
        {
            int personPosition = FindRoot(personArray, pair.Value);
            List<string> emailList = null;
            if (position2EmailList.ContainsKey(personPosition))
            {
                emailList = position2EmailList[personPosition];
            }
            else
            {
                emailList = new List<string>();
                position2EmailList.Add(personPosition, emailList);
            }
            emailList.Add(pair.Key);
        }

        IList<IList<string>> ret = new List<IList<string>>();
        foreach (int position in position2EmailList.Keys )
        {
            List<string> personList = position2EmailList[position];// new List<string>();
            //personList.AddRange(positionHashMap[position]);
            
            personList.Sort();
            personList.Insert(0, accounts[position][0]);
            ret.Add(personList);
        }
        return ret;
    }

    /// <summary>
    /// 找到根节点
    /// </summary>
    /// <param name="array"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    private static int FindRoot(int[] array, int x)
    {
        while(array[x] != x)
        {
            x = array[x];
        }
        return x;
        //if (array[x] == x) return x;
        //return FindRoot(array, array[x]);
    }

    /// <summary>
    /// 合并两个分支
    /// </summary>
    /// <param name="array"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    private static void UnionRoot(int[] array, int from, int to)
    {
        int realFrom = FindRoot(array, from);
        int realTo = FindRoot(array, to);
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == realFrom)
            {
                array[i] = realTo;
            }

        }
    }
}