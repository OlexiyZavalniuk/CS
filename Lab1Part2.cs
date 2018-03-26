using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Введіть посилання на файл який закодувати:");
            string inDir = Console.ReadLine();
            Console.WriteLine("Введіть посилання на файл який створити:");
            string outDir = Console.ReadLine();
            FillAlphabet();
            CodeFileToBase64(inDir, outDir);
        }
        static Dictionary<int, char> base64Alphabet = new Dictionary<int, char>();
        static void FillAlphabet()
        {
            int value = 65, key = 0;
            for (; key <= 25; key++)
            {
                base64Alphabet.Add(key, (char)value);
                value++;
            }
            value = 97;
            for (key = 26; key <= 51; key++)
            {
                base64Alphabet.Add(key, (char)value);
                value++;
            }
            value = 48;
            for (key = 52; key <= 61; key++)
            {
                base64Alphabet.Add(key, (char)value);
                value++;
            }
            base64Alphabet.Add(62, '+');
            base64Alphabet.Add(63, '/');
        }

        static bool[] IntToBinary(int value)
        {
            bool[] b = new bool[8];
            for (int i = 7; i >= 0; i--)
            {
                b[i] = (value % 2 == 1) ? true : false;
                value /= 2;
            }
            return b;
        }

        static bool[] ReadBitsFromFile(string path)
        {
            byte[] data = File.ReadAllBytes(path);
            bool[] bits = new bool[data.Length * 8], temp;
            for (int i = data.Length - 1; i >= 0; i--)
            {
                temp = IntToBinary(data[i]);
                for (int l = 0; l < temp.Length; l++)
                    bits[i * 8 + l] = temp[l];
            }
            return bits;
        }

        static int BitInIndex(bool[] bits)
        {
            int decNumber = 0;
            for (int i = 0; i < bits.Length; i++)
                if (bits[i])
                    decNumber += (int)Math.Pow(2, bits.Length - 1 - i);
            return decNumber;
        }
        static void CodeFileToBase64(string path, string newpath)
        {
            using (StreamWriter wr = new StreamWriter(newpath, false))
                wr.Write(ToBase64(ReadBitsFromFile(path)));
        }

        static string ToBase64(bool[] b)
        {
            string data = "", pad = "";
            bool[] temp = new bool[6];
            for (int i = 0; i < b.Length; i += 6)
            {
                for (int k = 0; k < temp.Length; k++)
                {
                    if (i + k < b.Length)
                        temp[k] = b[i + k];
                    else
                    {
                        if (b.Length % 3 == 2)
                        {
                            temp[k + 1] = temp[k + 2] = temp[k + 3] = temp[k] = false;
                            pad = "==";
                            k = 6;
                        }
                        else
                        {
                            temp[k + 1] = temp[k] = false;
                            pad = "=";
                            k = 6;
                        }
                    }
                }
                data += base64Alphabet[BitInIndex(temp)].ToString();
            }
            return data + pad;
        }
    }
}



