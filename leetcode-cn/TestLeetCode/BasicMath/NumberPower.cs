namespace TestLeetCode.BasicMath
{
    /// <summary>
    /// 计算数字的幂
    /// </summary>
    public static class NumberPower
    {
        /// <summary>
        /// 计算 ab 对 mod(1337) 取模，a 是一个正整数，b 是一个非常大的正整数且会以数组形式给出。
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static int SuperPow(int a, int[] b, int mod)
        {
            if (a == 0) return 0;
            if (a == 1 || b == null || b.Length == 0) return 1;

            int len = b.Length;
            int[] dp = new int[len];
            a %= mod;
            dp[0] = a;
            for (int i = 1; i < len; i++) dp[i] = FastPower(dp[i - 1], 10, mod);

            int ret = 1;
            for (int i = 0; i < len; i++)
                if (b[i] != 0) ret = (ret * FastPower(dp[len - 1 - i], b[i], mod)) % mod;

            return ret;
        }

        #region fast power

        public static int FastPower(int baseNum, int power, int mod)
        {
            int ret = 1;
            while (0 < power)
            {
                if ((power & 1) == 1) ret = (ret * baseNum) % mod;

                power >>= 1;
                baseNum = (baseNum * baseNum) % mod;
            }
            return ret;
        }

        public static int FastPower(int baseNum, int power)
        {
            int ret = 1;
            while (0 < power)
            {
                // 此处等价于if(power%2==1)
                if ((power & 1) == 1) ret *= baseNum;

                power >>= 1;//此处等价于power=power/2，幂次变为二分之一
                baseNum *= baseNum; // 基数变为基数的平方
            }
            return ret;
        }

        public static double FastPower(double baseNum, int power)
        {
            double ret = 1;
            while (0 < power)
            {
                if ((power & 1) == 1) ret *= baseNum;

                power >>= 1;
                baseNum *= baseNum;
            }
            return ret;
        }

        #endregion fast power
    }
}