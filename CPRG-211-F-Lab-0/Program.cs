using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CPRG_211_F_Lab_0
{
    internal class Program
    {
        private static string _filePath = "E:\\CPRG 211\\Unit 1\\Labs\\Lab 0\\Output\\numbers.txt";

        static void Main(string[] args)
        {
            //prompt the user for the low number
            double low = GetLowNumber();

            //prompt the user for the high number
            double high = GetHighNumber(low);

            //calculate and print out the difference between the low and high variables
            double difference = high - low;
            Console.WriteLine($"The difference between {low} and {high} is {difference}");

            //build a list of numbers between low and high
            List<double> numbersBetween = GetNumbersBetween(low, high);

            //store the numbers in a text file
            StoreNumbersBetween(numbersBetween);

            //get the numbers from the file and display them to the screen
            List<double> numbersFromFile = GetNumbersFromFile();
            
            //change the order, to display the numbers from high to low
            numbersFromFile.Reverse();
            numbersFromFile.ForEach(number => Console.WriteLine(number));

            Console.WriteLine($"\nPrime Numbers Between {low} and {high}");

            numbersFromFile.ForEach(number =>
            {
                bool isPrime = IsPrime(number);

                if (isPrime)
                {
                    Console.WriteLine(number);
                }
            });
        }

        private static double GetLowNumber()
        {
            Console.Write("Enter low number: ");
            bool parseSuccessLow = double.TryParse(Console.ReadLine(), out double low);

            //loop until the low number is positive
            while (low < 0 || !parseSuccessLow)
            {
                Console.Write("The number was negative or invalid. Enter low number: ");
                parseSuccessLow = double.TryParse(Console.ReadLine(), out low);
            }

            return low;
        }

        private static double GetHighNumber(double low)
        {
            Console.Write("Enter high number: ");
            bool parseSuccessHigh = double.TryParse(Console.ReadLine(), out double high);


            //loop until the high number is greater than the low number
            while (high < low || !parseSuccessHigh)
            {
                Console.Write("The number was lower than the low value or invalid. Enter low number: ");
                parseSuccessHigh = double.TryParse(Console.ReadLine(), out high);
            }

            return high;
        }

        private static List<double> GetNumbersBetween(double low, double high)
        {
            List<double> numbersBetween = new();

            for (double i = low + 1; i < high; i++)
            {
                numbersBetween.Add(i);
            }

            return numbersBetween;
        }

        private static void StoreNumbersBetween(List<double> numbersBetween)
        {
            //store each number in the list in the file
            using (StreamWriter sw = new(_filePath)) {
                numbersBetween.ForEach(number => sw.WriteLine(number));
                sw.Close();
            }
        }

        private static List<double> GetNumbersFromFile()
        {
            List<double> numbers = new();

            using (StreamReader sr = new(_filePath))
            {
                while (!sr.EndOfStream)
                {
                    bool parseSuccess = double.TryParse(sr.ReadLine()?.Trim(), out double currentNumber);

                    if (parseSuccess)
                    {
                        numbers.Add(currentNumber);
                    }
                }
            }

            return numbers;
        }

        private static bool IsPrime (double number)
        {
            //one is not a prime, so if the number is one,
            //set isPrime to false
            bool isPrime = number != 1;


            //the number can be divided by 2
            //because a number greater than half will never divide evenly
            for (int i = 2; i <= number / 2; i++)
            {
                if (number % i == 0)
                {
                    isPrime = false;
                }
            }

            return isPrime;
        }
    }
}
