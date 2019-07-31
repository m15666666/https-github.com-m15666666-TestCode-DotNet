using System;

namespace AnalysisAlgorithm.Tests
{
    internal class Program
    {
        private static void Main( string[] args )
        {
            new FftTests().Fft();
            new FftTests().IFft();
            new FftTests().FftAndIFft();
            new FftTests().HilbertT();

           // Console.ReadLine();
        }
    }
}