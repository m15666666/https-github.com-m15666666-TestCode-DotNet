﻿using System;
using System.Collections.Generic;

namespace TestLeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<int> set = new HashSet<int>();
            //set.Contains()

            var containsDuplicateSolution = new ContainsDuplicateSolution();
            containsDuplicateSolution.Test();

            var rotateArraySolution = new RotateArraySolution();

            rotateArraySolution.Test();

            Console.Read();
        }
    }
}
