using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
设计一个简化版的推特(Twitter)，可以让用户实现发送推文，关注/取消关注其他用户，能够看见关注人（包括自己）的最近十条推文。你的设计需要支持以下的几个功能：

postTweet(userId, tweetId): 创建一条新的推文
getNewsFeed(userId): 检索最近的十条推文。每个推文都必须是由此用户关注的人或者是用户自己发出的。推文必须按照时间顺序由最近的开始排序。
follow(followerId, followeeId): 关注一个用户
unfollow(followerId, followeeId): 取消关注一个用户
示例:

Twitter twitter = new Twitter();

// 用户1发送了一条新推文 (用户id = 1, 推文id = 5).
twitter.postTweet(1, 5);

// 用户1的获取推文应当返回一个列表，其中包含一个id为5的推文.
twitter.getNewsFeed(1);

// 用户1关注了用户2.
twitter.follow(1, 2);

// 用户2发送了一个新推文 (推文id = 6).
twitter.postTweet(2, 6);

// 用户1的获取推文应当返回一个列表，其中包含两个推文，id分别为 -> [6, 5].
// 推文id6应当在推文id5之前，因为它是在5之后发送的.
twitter.getNewsFeed(1);

// 用户1取消关注了用户2.
twitter.unfollow(1, 2);

// 用户1的获取推文应当返回一个列表，其中包含一个id为5的推文.
// 因为用户1已经不再关注用户2.
twitter.getNewsFeed(1); 
*/
/// <summary>
/// https://leetcode-cn.com/problems/design-twitter/
/// 355. 设计推特
/// </summary>
class DesignTwitterSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    /** Initialize your data structure here. */
    public DesignTwitterSolution()
    {
        TwitterManager.Instance = new TwitterManager();
    }

    /** Compose a new tweet. */
    public void PostTweet(int userId, int tweetId)
    {
        TwitterManager.Instance.PostTweet(userId, tweetId);
    }

    /** Retrieve the 10 most recent tweet ids in the user's news feed. Each item in the news feed must be posted by users who the user followed or by the user herself. Tweets must be ordered from most recent to least recent. */
    public IList<int> GetNewsFeed(int userId)
    {
        return TwitterManager.Instance.GetNewsFeed(userId);
    }

    /** Follower follows a followee. If the operation is invalid, it should be a no-op. */
    public void Follow(int followerId, int followeeId)
    {
        TwitterManager.Instance.Follow(followerId, followeeId);
    }

    /** Follower unfollows a followee. If the operation is invalid, it should be a no-op. */
    public void Unfollow(int followerId, int followeeId)
    {
        TwitterManager.Instance.Unfollow(followerId, followeeId);
    }

    public class TwitterManager
    {
        public static TwitterManager Instance { get; set; }

        static TwitterManager()
        {
            Instance = new TwitterManager();
        }

        public Dictionary<int, TwitterUser> UserId2Users { get; } = new Dictionary<int, TwitterUser>();

        public TwitterUser GetUser(int userId)
        {
            var map = UserId2Users;
            if (!map.ContainsKey(userId)) map.Add(userId, new TwitterUser { Id = userId });
            return map[userId]; 
        }

        public void PostTweet(int userId, int tweetId)
        {
            var user = GetUser(userId);
            user.PostTweet(tweetId);
        }

        public IList<int> GetNewsFeed(int userId)
        {
            var user = GetUser(userId);
            return user.GetNewsFeed();
        }

        public void Follow(int followerId, int followeeId)
        {
            if (followerId == followeeId) return;
            GetUser(followerId).Follow(GetUser(followeeId));
        }

        /** Follower unfollows a followee. If the operation is invalid, it should be a no-op. */
        public void Unfollow(int followerId, int followeeId)
        {
            if (followerId == followeeId) return;
            GetUser(followerId).Unfollow(GetUser(followeeId));
        }

    }

    public class TwitterUser
    {
        /// <summary>
        /// twitter id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 关注的其他用户
        /// </summary>
        public HashSet<TwitterUser> Followees { get; } = new HashSet<TwitterUser>();

        /// <summary>
        /// 关注本用户的用户集合
        /// </summary>
        public HashSet<TwitterUser> Followers { get; } = new HashSet<TwitterUser>();

        public List<TwiterMessage> Messages { get; } = new List<TwiterMessage>();

        public void PostTweet(int tweetId)
        {
            var message = new TwiterMessage { UserId = Id, MessageId = tweetId };
            Messages.Insert(0, message);

            //foreach (var f in Followees) f.Messages.Insert(0, message);
        }

        public IList<int> GetNewsFeed()
        {
            SortedList<int,int> a = new SortedList<int,int>();

            int minTimeId = 0;
            foreach (var m in Messages)
            {
                a.Add(m.TimeId, m.MessageId);
                if (a.Count == 10)
                {
                    minTimeId = a.Keys[0];
                    break;
                }
            }

            foreach ( var f in Followees )
            {
                foreach (var m in f.Messages)
                {
                    if (m.TimeId < minTimeId) break;
                    a.Add(m.TimeId, m.MessageId);

                    if (9 < a.Count) { minTimeId = a.Keys[a.Count - 10]; }
                }
            }

            while (10 < a.Count) a.RemoveAt(0);

            return a.Values.Reverse().ToList();
        }

        public void Follow(TwitterUser user)
        {
            if (!Followees.Contains(user)) Followees.Add(user);
            if (!user.Followers.Contains(this)) user.Followers.Add(this);
        }

        public void Unfollow(TwitterUser user)
        {
            if (Followees.Contains(user)) Followees.Remove(user);
            if (user.Followers.Contains(this)) user.Followers.Remove(this);
        }
    }

    public class TwiterMessage
    {
        public TwiterMessage()
        {
            TimeId = System.Threading.Interlocked.Increment(ref TimeIdGenerator);
        }

        /// <summary>
        /// time id
        /// </summary>
        private static int TimeIdGenerator = 0;

        /// <summary>
        /// Message id
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// time id
        /// </summary>
        public int TimeId { get; set; }
    }
}
/*
public class Twitter {
    public class User
    {
        public int ID;
        public HashSet<int> Follow;
        public User(int userId){
            ID=userId;
            Follow=new HashSet<int>(){userId};
        }
    }

    List<(int,int)> Tweets;
    Dictionary<int,User> _dict;
    int Count;

    public Twitter()
    {
        Tweets = new List<(int, int)>();
        _dict = new Dictionary<int, User>();
        Count = 0;
    }

    public void PostTweet(int userId, int tweetId)
    {
        if (!_dict.ContainsKey(userId))
            _dict[userId] = new User(userId);
        Tweets.Add((userId, tweetId));
        Count++;
    }

    public IList<int> GetNewsFeed(int userId)
    {
        int i = 1;
        int size = 0;
        if (!_dict.ContainsKey(userId))
        {
            return new List<int>();
        }
        User user = _dict[userId];

        List<int> output = new List<int>();
        while (Count - i >= 0 && size < 10)
        {
            if (user.Follow.Contains(Tweets[Count - i].Item1))
            {
                output.Add(Tweets[Count - i].Item2);
                size++;
            }
            i++;
        }
        return output;
    }

    public void Follow(int followerId, int followeeId)
    {
        if (!_dict.ContainsKey(followerId))
        {
            _dict[followerId] = new User(followerId);
        }
        _dict[followerId].Follow.Add(followeeId);
    }

    public void Unfollow(int followerId, int followeeId)
    {
        if (followerId == followeeId)
            return;
        if (_dict.ContainsKey(followerId) && _dict[followerId].Follow.Contains(followeeId))
        {
            _dict[followerId].Follow.Remove(followeeId);
        }
    }
}


*/