using System;

namespace AnalysisAlgorithm.Tests
{
    internal class Program
    {
        private static void Main( string[] args )
        {
            OutputUtils.OutputDir = @"D:\temp2";

            //new FftTests().InitAngleTest();
            //return;

            new FilterTests().BandPassTest();

            new FftTests().InitAngleTest();
            
            new CurveFitTests().LMS_CurveFit();
            new FftTests().Fft();
            new FftTests().IFft();
            new FftTests().FftAndIFft();
            new FftTests().HilbertT();

           // Console.ReadLine();
        }
    }
}