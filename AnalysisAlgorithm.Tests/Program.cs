using System;

namespace AnalysisAlgorithm.Tests
{
    internal class Program
    {
        private static void Main( string[] args )
        {
            new FftTests().InitAngleTest();
            new FilterTests().BandPassTest();
            new CurveFitTests().LMS_CurveFit();
            new FftTests().Fft();
            new FftTests().IFft();
            new FftTests().FftAndIFft();
            new FftTests().HilbertT();

           // Console.ReadLine();
        }
    }
}