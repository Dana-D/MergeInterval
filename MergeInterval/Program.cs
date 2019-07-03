using System;
using System.Collections.Generic;

namespace MergeIntervals
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] input1 = { { 2, 5 }, { -2, 0 }, { 3, 8 } };
            int[,] input2 = { { 1, 3 }, { 2, 6 }, { 8, 10 }, { 15, 18 } };
            int[,] input3 = { { 1, 4 }, { 4, 5 } };
            int[,] input4 = { { 1, 3 }, { 2, 6 }, { 8, 10 }, { 1, 18 } };

            Interval A = new Interval(input1);
            Interval B = new Interval(input2);
            Interval C = new Interval(input3);
            Interval D = new Interval(input4);

            A.printIntervals();
            A.merge();
            A.printIntervals();
            Console.WriteLine("----------------------");
            B.printIntervals();
            B.merge();
            B.printIntervals();
            Console.WriteLine("----------------------");
            C.printIntervals();
            C.merge();
            C.printIntervals();
            Console.WriteLine("----------------------");
            D.printIntervals();
            D.merge();
            D.printIntervals();

        }
    }

    class Interval
    {
        int[,] intervals;

        public Interval(int[,] a)
        {
            intervals = a;
        }

        public void merge()
        {
            for (int i = 0; i < intervals.Length / 2; i++)
            {
                int[] entry = { 0, 0 };
                bool merged = false;
                bool foundSelf = false;
                if (intervals[i, 0] != Int32.MinValue && intervals[i, 1] != Int32.MinValue)
                {
                    for (int j = 0; j < intervals.Length / 2; j++)
                    {
                        if (intervals[i, 0] > intervals[j, 1]) //Current is larger and no overlap.
                        {
                            entry[0] = intervals[i, 0];
                            entry[1] = intervals[i, 1];

                            merged = false;
                        }
                        else if (intervals[i, 0] > intervals[j, 0] && intervals[i, 0] <= intervals[j, 1]
                            && intervals[i, 1] >= intervals[j, 1]) //Current's first is inside compared and second beyond
                        {
                            entry[0] = intervals[j, 0];
                            entry[1] = intervals[i, 1];

                            merged = true;
                        }
                        else if (intervals[i, 0] >= intervals[j, 0] && intervals[i, 0] <= intervals[j, 1]
                            && intervals[i, 1] <= intervals[j, 1]) //Current completely inside compared
                        {
                            if (foundSelf)
                            {
                                entry[0] = intervals[j, 0];
                                entry[1] = intervals[j, 1];

                                merged = true;
                            }
                            else
                            {
                                merged = false;
                                foundSelf = true;
                            }

                        }
                        else if (intervals[i, 0] < intervals[j, 0] && intervals[i, 1] >= intervals[j, 0]
                            && intervals[i, 1] <= intervals[j, 1]) //Current's first is outside compared and second is inside
                        {
                            entry[0] = intervals[i, 0];
                            entry[1] = intervals[j, 1];

                            merged = true;
                        }
                        else if (intervals[i, 1] < intervals[j, 0]) //Current is smaller and no overlap.
                        {
                            entry[0] = intervals[i, 0];
                            entry[1] = intervals[i, 1];

                            merged = false;
                        }

                        if (merged)
                        {
                            intervals[j, 0] = entry[0];
                            intervals[j, 1] = entry[1];

                            intervals[i, 0] = Int32.MinValue;
                            intervals[i, 1] = Int32.MinValue;
                        }
                    }
                }
                else
                {
                    //do nothing
                }

            }

            //clean up
            int count = 0;
            for (int i = 0; i < intervals.Length / 2; i++)
            {
                if (intervals[i, 0] == Int32.MinValue)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                int[,] newIntervals = new int[(intervals.Length / 2) - count, 2];
                int position = 0;
                for (int i = 0; i < intervals.Length / 2; i++)
                {
                    if (intervals[i, 0] != Int32.MinValue)
                    {
                        newIntervals[position, 0] = intervals[i, 0];
                        newIntervals[position, 1] = intervals[i, 1];
                        position++;
                    }
                }
                intervals = newIntervals;
            }
        }

        public void printIntervals()
        {
            string result = "[";

            for (int i = 0; i < intervals.Length / 2; i++)
            {
                result += ("[" + intervals[i, 0] + ", " + intervals[i, 1] + "]");
            }

            result += "]";

            Console.WriteLine(result);
        }
    }
}
