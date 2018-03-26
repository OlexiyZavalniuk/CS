using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSLab1Part1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Введіть посилання на документ:");
            string dir = Console.ReadLine();
            List<string> symbols = new List<string>();
            List<int> numbers = new List<int>();
            CalculateText(dir, symbols, numbers);
            ShowResult(dir, symbols, numbers);
            Console.Read();
        }
        static void CalculateText(string dir, List<string> symbols, List<int> numbers)
        {
            string symbol = "", line = "";
            using (TextReader tr = new StreamReader(dir))
            {
                while (tr.Peek() != -1)
                {
                    line = tr.ReadLine();
                    for (int i = 0; i < line.Length; i++)
                    {
                        symbol = line.Substring(i, 1);
                        //symbol = symbol.ToLower();
                        if (numbers.Count == 0)
                        {
                            symbols.Add(symbol);
                            numbers.Add(1);
                        }
                        else
                        {
                            for (int q = 0; q < symbols.Count; q++)
                            {
                                if (symbols[q] == symbol)
                                {
                                    numbers[q]++;
                                    break;
                                }
                                else if (q == symbols.Count - 1)
                                {
                                    symbols.Add(symbol);
                                    numbers.Add(1);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        static void ShowResult(string dir, List<string> symbols, List<int> numbers)
        {
            Console.WriteLine("Файл: " + dir);
            Console.WriteLine("Кількість всіх символів у файлі: {0}", Number(numbers));
            for (int i = 0; i < symbols.Count; i++)
            {
                Console.WriteLine("Символ '{0}' -> кількість {1} -> ймовірність появи {2}%", symbols[i], numbers[i], Chance(numbers, i));
            }
            double temp_entropy = Entropy(numbers);
            Console.WriteLine("Ентропія: {0}", temp_entropy);
            Console.WriteLine("Кількість інформації: {0} | {1}", AmountOfInformation(temp_entropy, Number(numbers)), new FileInfo(dir).Length);
        }
        static double Chance(List<int> numbers, int num)
        {
            double chance = ((double)numbers[num] / (double)Number(numbers)) * 100;
            return chance;
        }
        static double Entropy(List<int> numbers)
        {
            double entropy = 0;
            double p = 0;
            //result = sum (1 to m) p*log2(1/p)
            for (int i = 0; i < numbers.Count; i++)
            {
                p = (double)numbers[i] / (double)Number(numbers);
                if (p != 0)
                {
                    entropy += p * Math.Log((double)1 / p, 2);
                }
            }
            return entropy;
        }
        static int AmountOfInformation(double entropy, int amount)
        {
            int am_bits = (int)Math.Ceiling(entropy * amount);
            return (int)Math.Ceiling((double)am_bits / (double)8);//returns result in bytes
        }
        static int Number(List<int> numbers)
        {
            int num = 0;
            for(int i = 0; i < numbers.Count; i++)
            {
                num += numbers[i];
            }
            return num;
        }
    }
}
