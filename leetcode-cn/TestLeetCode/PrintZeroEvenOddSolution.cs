using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


/*
假设有这么一个类：

class ZeroEvenOdd {
  public ZeroEvenOdd(int n) { ... }      // 构造函数
  public void zero(printNumber) { ... }  // 仅打印出 0
  public void even(printNumber) { ... }  // 仅打印出 偶数
  public void odd(printNumber) { ... }   // 仅打印出 奇数
}
相同的一个 ZeroEvenOdd 类实例将会传递给三个不同的线程：

线程 A 将调用 zero()，它只输出 0 。
线程 B 将调用 even()，它只输出偶数。
线程 C 将调用 odd()，它只输出奇数。
每个线程都有一个 printNumber 方法来输出一个整数。请修改给出的代码以输出整数序列 010203040506... ，其中序列的长度必须为 2n。

 

示例 1：

输入：n = 2
输出："0102"
说明：三条线程异步执行，其中一个调用 zero()，另一个线程调用 even()，最后一个线程调用odd()。正确的输出为 "0102"。
示例 2：

输入：n = 5
输出："0102030405"
*/
/// <summary>
/// https://leetcode-cn.com/problems/print-zero-even-odd/
/// 1116. 打印零与奇偶数
/// 
/// </summary>
class PrintZeroEvenOddSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    private int n;

    public PrintZeroEvenOddSolution(int n)
    {
        this.n = n;
    }

    private readonly System.Threading.SemaphoreSlim _zero = new System.Threading.SemaphoreSlim(1);
    private readonly System.Threading.SemaphoreSlim _even = new System.Threading.SemaphoreSlim(0);
    private readonly System.Threading.SemaphoreSlim _odd = new System.Threading.SemaphoreSlim(0);

    // printNumber(x) outputs "x", where x is an integer.
    public void Zero(Action<int> printNumber)
    {
        //SpinWait
        for (int i = 0; i < n; i++)
        {
            _zero.Wait();
            printNumber(0);
            if ((i & 1) == 0) _odd.Release();
            else _even.Release();
        }
    }

    public void Even(Action<int> printNumber)
    {
        for (int i = 2; i <= n; i += 2)
        {
            _even.Wait();
            printNumber(i);
            _zero.Release();
        }
    }

    public void Odd(Action<int> printNumber)
    {
        for (int i = 1; i <= n; i += 2)
        {
            _odd.Wait();
            printNumber(i);
            _zero.Release();
        }
    }
}
/*

JAVA并发工具类大练兵
KevinBauer
发布于 5 个月前
3.9k 阅读
原文地址： https://zhuanlan.zhihu.com/p/81626432

方案一：Semaphore
借助信号量来建立屏障，还是很方便的：

class ZeroEvenOdd {
    private int n;
    
    public ZeroEvenOdd(int n) {
        this.n = n;
    }

    Semaphore z = new Semaphore(1);
    Semaphore e = new Semaphore(0);
    Semaphore o = new Semaphore(0);
	
    public void zero(IntConsumer printNumber) throws InterruptedException {
        for(int i=0; i<n;i++) {
        	z.acquire();
        	printNumber.accept(0);
        	if((i&1)==0) {
        		o.release();
        	}else {
        		e.release();
        	}
        }
    }

    public void even(IntConsumer printNumber) throws InterruptedException {
        for(int i=2; i<=n; i+=2) {
        	e.acquire();
        	printNumber.accept(i);
        	z.release();
        }
    }

    public void odd(IntConsumer printNumber) throws InterruptedException {
        for(int i=1; i<=n; i+=2) {
        	o.acquire();
        	printNumber.accept(i);
        	z.release();
        }
    }
}
方案二：Lock
之前听课时听过老师讲：“凡是可以用信号量解决的问题，都可以用管程模型来解决”，那么我们试试吧（实践发现，也确实可以，但逻辑有点绕不够直观）：

class ZeroEvenOdd {
    private int n;
    
    public ZeroEvenOdd(int n) {
        this.n = n;
    }

    Lock lock = new ReentrantLock();
    Condition z = lock.newCondition();
    Condition num = lock.newCondition();
    volatile boolean zTurn = true;
    volatile int zIndex = 0;
	
    public void zero(IntConsumer printNumber) throws InterruptedException {
        for(;zIndex<n;) {
            lock.lock();
            try {
        	while(!zTurn) {
        		z.await();
        	}
        	printNumber.accept(0);
        	zTurn = false;
        	num.signalAll();
                zIndex++;
            }finally {
        	lock.unlock();
            }
        }
    }

    public void even(IntConsumer printNumber) throws InterruptedException {
        for(int i=2; i<=n; i+=2) {
            lock.lock();
            try {
        	while(zTurn || (zIndex&1)==1) {
        		num.await();
        	}
        	printNumber.accept(i);
        	zTurn = true;
        	z.signal();
            }finally {
        	lock.unlock();
            }
        }
    }

    public void odd(IntConsumer printNumber) throws InterruptedException {
        for(int i=1; i<=n; i+=2) {
            lock.lock();
            try {
        	while(zTurn || (zIndex&1)==0) {
        		num.await();
        	}
        	printNumber.accept(i);
        	zTurn = true;
        	z.signal();
            }finally {
        	lock.unlock();
            }
        }
    }
}
方案三：无锁
老规矩，但凡用了锁的，都来试试可否变成无锁的（本机测试是可行的，但测评平台报超时）：

class ZeroEvenOdd {
    private int n;
    
    public ZeroEvenOdd(int n) {
        this.n = n;
    }

    volatile int stage = 0;
	
    public void zero(IntConsumer printNumber) throws InterruptedException {
        for(int i=0;i<n;i++) {
        	while(stage>0);
    		printNumber.accept(0);
    		if((i&1)==0) {
    			stage = 1;
    		}else {
    			stage = 2;
    		}
        }
    }

    public void even(IntConsumer printNumber) throws InterruptedException {
        for(int i=2; i<=n; i+=2) {
        	while(stage!=2);
    		printNumber.accept(i);
    		stage = 0;
        }
    }

    public void odd(IntConsumer printNumber) throws InterruptedException {
        for(int i=1; i<=n; i+=2) {
        	while(stage!=1);
    		printNumber.accept(i);
    		stage = 0;
        }
    }
}
下一篇：用Lock和Condition解决问题

using System.Threading;
public class ZeroEvenOdd {
    private int n;
    private SpinWait _spinWait=new SpinWait();
    private bool _state=true;
    private bool _end=false;
    private int _state2=0;
    public ZeroEvenOdd(int n) {
        this.n = n;
    }

    // printNumber(x) outputs "x", where x is an integer.
    public void Zero(Action<int> printNumber) {
        while(!_end){
        SpinWait.SpinUntil(()=>_state);
        if(_state2==n) {
        _end=true;
        _state=false;
            return;}
        printNumber(0);
         ++_state2;
        _state=false;}
    }

    public void Even(Action<int> printNumber) {
        while(!_end){
        SpinWait.SpinUntil(()=>!_state&&_state2%2==0);
         if(_end){
         ++_state2;
            return;
         }
        printNumber(_state2);
        _state=true;
        }
    }

    public void Odd(Action<int> printNumber) {
        while(!_end){
        SpinWait.SpinUntil(()=>!_state&&_state2%2==1);
         if(_end){
         ++_state2;
            return;
         }
        printNumber(_state2);
        _state=true;
        }
    }
}

using System.Threading;
public class ZeroEvenOdd
{
	private int n;
	private Semaphore zero = new Semaphore(1, 1);
	private Semaphore even = new Semaphore(0, 1);
	private Semaphore odd = new Semaphore(0, 1);
	public ZeroEvenOdd(int n)
	{
		this.n = n;
	}

	//输出0
	// printNumber(x) outputs "x", where x is an integer.
	public void Zero(Action<int> printNumber)
	{
		for (var i = 0; i < n; i++)
		{
			zero.WaitOne();
			printNumber(0);
			lock (this)
			{
				if (i % 2 == 1)
				{ even.Release(1); }
				if (i % 2 == 0)
				{ odd.Release(1); }
			}
		}
	}
	//输出偶数
	public void Even(Action<int> printNumber)
	{
		for (var i = 2; i <= n; i = i + 2)
		{
			even.WaitOne();
			printNumber(i);
			lock (this)
			{
				zero.Release(1);
			}
		}
	}

	//输出奇数
	public void Odd(Action<int> printNumber)
	{
		for (var i = 1; i <= n; i = i + 2)
		{
			odd.WaitOne();
			printNumber(i);
			lock (this)
			{
				zero.Release(1);
			}
		}
	}
}
	
using System.Threading;
public class ZeroEvenOdd {
    private int n;
    private static   AutoResetEvent oddResetEvent = new AutoResetEvent(false);
    private static  AutoResetEvent evenResetEvent = new AutoResetEvent(false);
    private static   AutoResetEvent zeroResetEvent = new AutoResetEvent(true);
    public ZeroEvenOdd(int n) {
        this.n = n;
    }

    // printNumber(x) outputs "x", where x is an integer.
    public void Zero(Action<int> printNumber) {
        int i=0;
        while(this.n>i){
            zeroResetEvent.WaitOne();
            printNumber(0);
            if(i%2==0){
                evenResetEvent.Set();
            }else
                oddResetEvent.Set();
            i++;
        }
    }

    public void Even(Action<int> printNumber) {
        for(int i=2;i<=this.n;i+=2){
            oddResetEvent.WaitOne();
            printNumber(i);
            zeroResetEvent.Set();
        }
    }

    public void Odd(Action<int> printNumber) {
        for(int i=1;i<=this.n;i+=2)
        {
            evenResetEvent.WaitOne();
            printNumber(i);
            zeroResetEvent.Set();
        }
    }
}

 
*/
