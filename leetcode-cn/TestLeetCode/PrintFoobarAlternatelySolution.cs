using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


/*
我们提供一个类：

class FooBar {
  public void foo() {
    for (int i = 0; i < n; i++) {
      print("foo");
    }
  }

  public void bar() {
    for (int i = 0; i < n; i++) {
      print("bar");
    }
  }
}
两个不同的线程将会共用一个 FooBar 实例。其中一个线程将会调用 foo() 方法，另一个线程将会调用 bar() 方法。

请设计修改程序，以确保 "foobar" 被输出 n 次。

 

示例 1:

输入: n = 1
输出: "foobar"
解释: 这里有两个线程被异步启动。其中一个调用 foo() 方法, 另一个调用 bar() 方法，"foobar" 将被输出一次。
示例 2:

输入: n = 2
输出: "foobarfoobar"
解释: "foobar" 将被输出两次。
*/
/// <summary>
/// https://leetcode-cn.com/problems/print-foobar-alternately/
/// 1115. 交替打印FooBar
/// 
/// </summary>
class PrintFoobarAlternatelySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    private int n;

    public PrintFoobarAlternatelySolution(int n)
    {
        this.n = n;
    }

    private System.Threading.Semaphore _s1 = new System.Threading.Semaphore(1, 1);
    private System.Threading.Semaphore _s2 = new System.Threading.Semaphore(0, 1);

    //private System.Threading.Mutex _m1 = new System.Threading.Mutex(true);
    //private System.Threading.Mutex _m2 = new System.Threading.Mutex(false);

    public void Foo(Action printFoo)
    {
        //Barrier b = new Barrier(1, null);
        for (int i = 0; i < n; i++)
        {
            //_m1.WaitOne();
            _s1.WaitOne();
            // printFoo() outputs "foo". Do not change or remove this line.
            printFoo();
            //_m2.ReleaseMutex();
            _s2.Release();
        }
    }

    public void Bar(Action printBar)
    {

        for (int i = 0; i < n; i++)
        {
            //_m2.WaitOne();
            _s2.WaitOne();
            // printBar() outputs "bar". Do not change or remove this line.
            printBar();
            //_m1.ReleaseMutex();
            _s1.Release();
        }
    }
}
/*
using System.Threading;
public class FooBar {
    private int n;
    AutoResetEvent _foo=new AutoResetEvent(true);
    AutoResetEvent _bar=new AutoResetEvent(false);
    public FooBar(int n) {
        this.n = n;
    }

    public void Foo(Action printFoo) {
        
        for (int i = 0; i < n; i++) {
            
        	// printFoo() outputs "foo". Do not change or remove this line.
        	_foo.WaitOne();
            printFoo();
            _bar.Set();
        }
    }

    public void Bar(Action printBar) {
        
        for (int i = 0; i < n; i++) {
            
            // printBar() outputs "bar". Do not change or remove this line.
        	_bar.WaitOne();
            printBar();
            _foo.Set();
        }
    }
}

using System.Threading;
public class FooBar {
    private int n;
    private Semaphore _foo = new Semaphore(0, 1);
    private Semaphore _bar = new Semaphore(1, 1);
    public FooBar(int n) {
        this.n = n;
    }

    public void Foo(Action printFoo) {
        
        for (int i = 0; i < n; i++) {
            _bar.WaitOne();
        	// printFoo() outputs "foo". Do not change or remove this line.
        	printFoo();
            _foo.Release();
        }
    }

    public void Bar(Action printBar) {
        
        for (int i = 0; i < n; i++) {
            _foo.WaitOne();
            // printBar() outputs "bar". Do not change or remove this line.
        	printBar();
            _bar.Release();
        }
    }
}

using System.Threading;
public class FooBar {
    private int n;
    private Barrier _barrier;
    private SemaphoreSlim _semaphore;

    public FooBar(int n) {
        this.n = n;
        _barrier = new Barrier(2);
        _semaphore = new SemaphoreSlim(0, 1);
    }

    public void Foo(Action printFoo) {
        
        for (int i = 0; i < n; i++) {
            _barrier.SignalAndWait();
        	// printFoo() outputs "foo". Do not change or remove this line.
        	printFoo();
            _semaphore.Release();
        }
    }

    public void Bar(Action printBar) {
        
        for (int i = 0; i < n; i++) {
            _barrier.SignalAndWait();
            // printBar() outputs "bar". Do not change or remove this line.
            _semaphore.Wait();
        	printBar();
        }
    }
}

using System.Threading;
public class FooBar
{
    private int n;

    private SemaphoreSlim[] sems = new SemaphoreSlim[2];

    public FooBar(int n)
    {
        this.n = n;
        sems[0] = new SemaphoreSlim(0);
        sems[1] = new SemaphoreSlim(1);
    }

    public void Foo(Action printFoo)
    {
        for (int i = 0; i < n; i++)
        {
            sems[1].Wait();
            printFoo();
            sems[0].Release();
        }
    }

    public void Bar(Action printBar)
    {
        for (int i = 0; i < n; i++)
        {
            sems[0].Wait();
            printBar();
            sems[1].Release();
        }
    }
}


*/
