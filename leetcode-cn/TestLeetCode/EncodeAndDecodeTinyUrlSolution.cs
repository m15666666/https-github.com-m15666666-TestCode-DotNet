using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
TinyURL是一种URL简化服务， 比如：当你输入一个URL https://leetcode.com/problems/design-tinyurl 时，它将返回一个简化的URL http://tinyurl.com/4e9iAk.

要求：设计一个 TinyURL 的加密 encode 和解密 decode 的方法。你的加密和解密算法如何设计和运作是没有限制的，你只需要保证一个URL可以被加密成一个TinyURL，并且这个TinyURL可以用解密方法恢复成原本的URL。

*/
/// <summary>
/// https://leetcode-cn.com/problems/encode-and-decode-tinyurl/
/// 535. TinyURL 的加密与解密
/// https://blog.csdn.net/sixkery/article/details/84777966
/// </summary>
class EncodeAndDecodeTinyUrlSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    private Dictionary<string, string> _map = new Dictionary<string, string>();
    // Encodes a URL to a shortened URL
    public string encode(string longUrl)
    {
        if (string.IsNullOrEmpty(longUrl)) return longUrl;
        var code = longUrl.GetHashCode().ToString();
        if (!_map.ContainsKey(code))
        {
            _map[code] = longUrl;
        }
        return $"http://tinyurl.com/{code}";
    }

    // Decodes a shortened URL to its original URL.
    public string decode(string shortUrl)
    {
        if (string.IsNullOrEmpty(shortUrl)) return shortUrl;
        var index = shortUrl.LastIndexOf('/');
        if (index == -1 || index == shortUrl.Length - 1) return shortUrl;
        var code = shortUrl.Substring(index + 1);
        return _map.ContainsKey(code) ? _map[code] : shortUrl;
    }
}
/*
public class Codec {

    private static readonly string TinyUrlPrefix = "http://tinyurl.com/";
    private static readonly string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    
    private readonly Dictionary<string, string> _shortToLongMap = new Dictionary<string, string>();
    
    // Encodes a URL to a shortened URL
    public string encode(string longUrl) 
    {
        var shortUrl = GenerateShortUrl();
        while(_shortToLongMap.ContainsKey(shortUrl))
        {
            shortUrl = GenerateShortUrl();
        }
        
        _shortToLongMap.Add(shortUrl, longUrl);
        return shortUrl;
    }

    // Decodes a shortened URL to its original URL.
    public string decode(string shortUrl) 
    {
        return _shortToLongMap[shortUrl];
    }
    
    private string GenerateShortUrl()
    {
        var random = new Random();
        var shortUrl = TinyUrlPrefix;
        for(var i = 0; i < 6; ++i)
        {
            shortUrl += Characters[random.Next(0, 62)].ToString();
        }
        
        return shortUrl;
    }
}
public class Codec {

    private Dictionary<string, string> dict = new Dictionary<string, string>();
    
    // Encodes a URL to a shortened URL
    public string encode(string longUrl) {
        var hash = longUrl.GetHashCode().ToString();
        if (!dict.ContainsKey(hash))
        {
            return dict[hash] = longUrl;
        }
        return "http://tinyurl.com/" + hash;
    }

    // Decodes a shortened URL to its original URL.
    public string decode(string shortUrl) {
        var hash = shortUrl.Substring(19);
        if (dict.ContainsKey(hash))
        {
            return dict[hash];
        }
        else
        {
            return shortUrl;
        }
    }
}

*/
