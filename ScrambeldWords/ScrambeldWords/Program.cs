using System;
using System.Diagnostics;

class Programm
{
    static int Main()
    {
        
        long input = long.Parse(Console.ReadLine());
        input = int.MaxValue;
        char[] displayDigits;
        long answer;
        var random = new Random(1);

        //Stopwatch sw = new Stopwatch();
        //sw.Start();
        //Console.WriteLine("START");
        for (long i = 1; i <= input; i++)
        {
            
            //var inputString = Console.ReadLine();
            //var inputString = random.Next(int.MaxValue).ToString();
            var inputString = (i).ToString();

            //Console.WriteLine("input: " + inputString);
            displayDigits = inputString.ToCharArray();

            //The whole number as one longeger
            long displayNumber = long.Parse(inputString);
            //Create long Array with each Digit
            long[] longDigits = new long[displayDigits.Length];
            ParseTolongDigits(displayDigits, longDigits);

            bool allEven = IsEven(longDigits);

            if (allEven)
            {
                answer = 0;

                Console.WriteLine("Case #" + i + ": " + answer);
            } else
            {
                long[] copylong = new long[longDigits.Length];
                Array.Copy(longDigits, copylong, longDigits.Length);
                //long bruteAnswer = BruteForce(i, displayNumber, ref copylong);
                //Console.WriteLine("Case #" + i + ": " + answer);

                long firstOddDigitIndex=0;
                for (long j = 0; j < longDigits.Length; j++){
                    if (longDigits[j] % 2 != 0){
                        firstOddDigitIndex = j;
                        j = longDigits.Length;

                    }                                                           
                }

                //Console.WriteLine("First ODD Digit: " + firstOddDigitIndex);
                //long[] newDigits = new long[longDigits.Length - firstOddDigitIndex];
                //Array.ConstrainedCopy(longDigits, firstOddDigitIndex, newDigits, 0, longDigits.Length-firstOddDigitIndex);
                //Console.WriteLine("New Array: " + longDigitsTolong(newDigits));

                long[] upperlongDigits = new long[longDigits.Length];
                Array.Copy(longDigits, upperlongDigits, longDigits.Length);
                long[] lowerlongDigits = new long[longDigits.Length];
                Array.Copy(longDigits, lowerlongDigits, longDigits.Length);

                fill(ref upperlongDigits, 0, firstOddDigitIndex+1, upperlongDigits.Length-firstOddDigitIndex-1);
                fill(ref lowerlongDigits, 8, firstOddDigitIndex + 1, lowerlongDigits.Length - firstOddDigitIndex - 1);


                long upperNumber = longDigitsTolong(upperlongDigits);
                long lowerNumber = longDigitsTolong(lowerlongDigits);
               
                upperNumber += (long)Math.Pow(10, upperlongDigits.Length - firstOddDigitIndex -1);
                lowerNumber -= (long)Math.Pow(10, upperlongDigits.Length - firstOddDigitIndex - 1);


                if (!IsEven(
                    ParseTolongDigits(
                        upperNumber.ToString().ToCharArray()
                    )
                )){
                    upperNumber = long.MaxValue;
                }


                if (!IsEven(
                    ParseTolongDigits(
                        lowerNumber.ToString().ToCharArray()
                    )
                ))
                {
                    lowerNumber = long.MaxValue;
                }

                long upperDiff, lowerDiff;
                upperDiff = upperNumber - displayNumber;
                lowerDiff = displayNumber - lowerNumber;
                answer = Math.Min(lowerDiff, upperDiff);
                //Console.Write(answer - bruteAnswer);
                //if (answer - bruteAnswer != 0)
                //{
                    //Console.WriteLine("Input: " + inputString + " Answer: " + answer + " Bruter Answer: " + bruteAnswer);
                    Console.WriteLine("Upper Number: " + upperNumber + " LowerNumber: " + lowerNumber);
                    //Console.WriteLine("Upper Diff: " + upperDiff + " Lower Diff: " + lowerDiff);
                    //Console.WriteLine();
                //}
                Console.WriteLine("Case #" + i + ": " + answer + " \n");

            }
        }

        //Console.WriteLine("Done in " + sw.Elapsed.Seconds + "," + sw.Elapsed.Milliseconds);
        return 0;
    }

    public static void fill(ref long[] array, long value, long startIndex, long count)
    {
        for (long i = startIndex; i < array.Length; i++){
            array[i] = value;
        }
    }
        


    private static long BruteForce(long i, long displayNumber, ref long[] longDigits)
    {
        long answer;
        bool isOdd = true;
        long copyDisplayNumber = displayNumber;
        long counterPlus = 0;
        long counterMinus = 0;


        while (isOdd)
        {
            //Increase Number
            copyDisplayNumber++;
            counterPlus++;
            //Check if even
            var numberStr = copyDisplayNumber.ToString().ToCharArray();
            longDigits = new long[copyDisplayNumber.ToString().ToCharArray().Length];
            ParseTolongDigits(numberStr, longDigits);
            isOdd = !IsEven(longDigits);
        }

        isOdd = true;
        copyDisplayNumber = displayNumber;

        while (isOdd)
        {
            //Increase Number
            copyDisplayNumber--;
            counterMinus++;
            //Check if even
            var numberStr = copyDisplayNumber.ToString().ToCharArray();
            longDigits = new long[copyDisplayNumber.ToString().ToCharArray().Length];
            ParseTolongDigits(numberStr, longDigits);
            isOdd = !IsEven(longDigits);
        }

        answer = Math.Min(counterPlus, counterMinus);
        //Console.WriteLine("Brute Force = Case #" + i + ": " + answer);

        return answer;
    }

    private static long longDigitsTolong(long[] longDigits){
        long longeger = 0;

        for (long i = longDigits.Length - 1; i >= 0; i--)
        {
            long exponent = longDigits.Length - 1 - i;
            longeger += longDigits[i] * (long)Math.Pow(10, exponent);
        }

        return longeger;
    }


    private static bool IsEven(long[] longDigits)
    {
        bool allEven = true;
        for (long j = 0; j < longDigits.Length; j++)
        {
            if (longDigits[j] % 2 != 0)
                allEven = false;
        }

        return allEven;
    }

    private static long[] ParseTolongDigits(char[] displayDigits, long[] longDigits)
    {
        for (long j = 0; j < displayDigits.Length; j++)
        {
            longDigits[j] = long.Parse(displayDigits[j].ToString());
        }

        return longDigits;
    }

    private static long[] ParseTolongDigits(char[] displayDigits)
    {
        long[] longDigits = new long[displayDigits.Length];
        for (long j = 0; j < displayDigits.Length; j++)
        {
            longDigits[j] = long.Parse(displayDigits[j].ToString());
        }

        return longDigits;
    }
}

