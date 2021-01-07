using System;

namespace TestLeetCode.BasicDataStructure
{
    /// <summary>
    /// 二叉堆
    /// 二叉堆详解实现优先级队列
    /// https://github.com/labuladong/fucking-algorithm/blob/master/%E6%95%B0%E6%8D%AE%E7%BB%93%E6%9E%84%E7%B3%BB%E5%88%97/%E4%BA%8C%E5%8F%89%E5%A0%86%E8%AF%A6%E8%A7%A3%E5%AE%9E%E7%8E%B0%E4%BC%98%E5%85%88%E7%BA%A7%E9%98%9F%E5%88%97.md
    /// </summary>
    public class BinaryHeap
    {
    }

    /// <summary>
    /// 二叉堆详解实现优先级队列
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    public class MaxPQ<Key> where Key : System.IComparable<Key>
    {
        // 存储元素的数组
        private Key[] pq;

        // 当前 Priority Queue 中的元素个数
        private int N = 0;
        /// <summary>
        /// 是最大堆还是最小堆，默认最大堆
        /// </summary>
        private readonly bool _maxOrMin = true;

        public MaxPQ(int cap, bool maxOrMin = true)
        {
            // 索引 0 不用，所以多分配一个空间
            pq = new Key[cap + 1];

            _maxOrMin = maxOrMin;
        }

        // 上浮第 k 个元素，以维护最大堆性质
        private void Swim(int k)
        {
            // 如果浮到堆顶，就不能再上浮了
            while (k > 1 && Less(Parent(k), k))
            {
                // 如果第 k 个元素比上层大
                // 将 k 换上去
                Exchange(Parent(k), k);
                k = Parent(k);
            }
        }

        ///下沉第 k 个元素，以维护最大堆性质
        private void Sink(int k)
        {
            // 如果沉到堆底，就沉不下去了
            while (Left(k) <= N)
            {
                // 先假设左边节点较大
                int older = Left(k);

                // 如果右边节点存在，比一下大小
                if (Right(k) <= N && Less(older, Right(k))) older = Right(k);

                // 结点 k 比俩孩子都大，就不必下沉了
                if (Less(older, k)) break;
                
                // 否则，不符合最大堆的结构，下沉 k 结点
                Exchange(k, older);
                k = older;
            }
        }

        // 交换数组的两个元素
        private void Exchange(int i, int j)
        {
            Key temp = pq[i];
            pq[i] = pq[j];
            pq[j] = temp;
        }

        // pq[i] 是否比 pq[j] 小？
        private bool Less(int i, int j) => (_maxOrMin ? pq[i].CompareTo(pq[j]) : pq[j].CompareTo(pq[i])) < 0;

        private int Parent(int i) => i / 2;

        private int Left(int i) => i * 2;

        private int Right(int i) => i * 2 + 1;

        public void Insert(Key e)
        {
            N++;
            // 先把新元素加到最后
            pq[N] = e;
            // 然后让它上浮到正确的位置
            Swim(N);
        }

        public Key DelMax()
        {
            // 最大堆的堆顶就是最大元素
            Key max = pq[1];

            // 把这个最大元素换到最后，删除之
            Exchange(1, N);
            pq[N] = default(Key);
            N--;
            
            // 让 pq[1] 下沉到正确位置
            Sink(1);

            return max;
        }

        // 返回当前队列中最大元素
        public Key Max => pq[1];
        public int Count => N;
    }
}