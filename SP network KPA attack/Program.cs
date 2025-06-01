using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;

namespace SP_network_KPA_attack
{
    internal class Program
    {

        static string hexMessage = "0x621cc14c";
        static string hexKey = "0x00000000";
        static string sBox = "c56b90ad3ef84712";
        static string hexOutput = "0x25ab95e1";

        static int[] permutation = { 20, 25, 19, 7, 31, 23, 8, 5, 11, 14, 17, 22, 18, 1, 0, 28, 29, 21, 12, 27, 30, 26, 9, 16, 6, 15, 10, 13, 24, 3, 4, 2 };

        static void Main(string[] args)
        {
            menu();
        }

        static void menu()
        {
            Console.WriteLine("Select mode:");
            Console.WriteLine("1. Output");
            Console.WriteLine("2. Key");
            Console.WriteLine("3. 2x outp");
            Console.WriteLine("4. 2x key");


            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    getOutp();
                    break;
                case "2":
                    getKey();
                    break;
                case "3":
                    twoLoopOutp();
                    break;
                case "4":
                    //brute();
                    break;
                default:
                    menu();
                    break;
            }
        }

        static void getOutp()
        {
            string message = Convert.ToString(Convert.ToInt32(hexMessage, 16), 2).PadLeft(32, '0');
            string key = Convert.ToString(Convert.ToInt32(hexKey, 16), 2).PadLeft(32, '0');


            string combine = "";
            for (int i = 0; i < 32; i++)
            {
                if (message[i] == '1' ^ key[i] == '1')
                {
                    combine += "1";
                }
                else
                {
                    combine += "0";
                }
            }


            string sub = "";


            for (int i = 0; i < 8; i++)
            {
                string toKill = combine.Substring(i * 4, 4);
                int id = Convert.ToInt32(toKill, 2);
                string wow = Convert.ToString(sBox[id]);
                string gotNew = Convert.ToString(Convert.ToInt32(wow, 16), 2).PadLeft(4, '0');
                sub += gotNew;
                Console.WriteLine(gotNew);
            }

            Console.WriteLine(sub);
            string output = "00000000000000000000000000000000";


            for (int i = 0; i < 32; i++)
            {
                int id = permutation[i];
                StringBuilder sb = new StringBuilder(output);
                sb[id] = sub[i];
                output = sb.ToString();
            }

            Console.WriteLine(output);
        }

        static void getKey()
        {
            string message = Convert.ToString(Convert.ToInt32(hexMessage, 16), 2).PadLeft(32, '0');
            string output = Convert.ToString(Convert.ToInt32(hexOutput, 16), 2).PadLeft(32, '0');

            Console.WriteLine(output);


            string befPerm = "00000000000000000000000000000000";

            for (int i = 0; i < 32; i++)
            {
                int id = permutation[i];
                StringBuilder sb = new StringBuilder(befPerm);
                sb[i] = output[id];
                befPerm = sb.ToString();
            }

            Console.WriteLine(befPerm);

            string befSub = "";

            for (int i = 0; i < 8; i++)
            {
                string toKill = befPerm.Substring(i * 4, 4);
                int id = Convert.ToInt32(toKill, 2);

                int j = 0;
                bool notFound = true;



                while (j < 16 && notFound)
                {
                    string wow = Convert.ToString(sBox[j]);
                    string s = Convert.ToString(Convert.ToInt32(wow, 16), 2).PadLeft(4, '0');

                    if (s == toKill)
                    {
                        befSub += Convert.ToString(j, 2).PadLeft(4, '0');
                        notFound = false;
                    }
                    j++;
                }
            }

            string key = "";

            for (int i = 0; i < 32; i++)
            {
                if (befSub[i] != message[i])
                {
                    key += '1';
                }
                else
                {
                    key += "0";
                }
            }

            Console.WriteLine("\n\n\n" + befSub);
            Console.WriteLine(message);
            Console.WriteLine(key);
        }

        static void twoLoopOutp()
        {
            string message = Convert.ToString(Convert.ToInt32(hexMessage, 16), 2).PadLeft(32, '0');
            string key = Convert.ToString(Convert.ToInt32(hexKey, 16), 2).PadLeft(32, '0');


            string combine = "";
            for (int i = 0; i < 32; i++)
            {
                if (message[i] == '1' ^ key[i] == '1')
                {
                    combine += "1";
                }
                else
                {
                    combine += "0";
                }
            }


            string sub = "";


            for (int i = 0; i < 8; i++)
            {
                string toKill = combine.Substring(i * 4, 4);
                int id = Convert.ToInt32(toKill, 2);
                string wow = Convert.ToString(sBox[id]);
                string gotNew = Convert.ToString(Convert.ToInt32(wow, 16), 2).PadLeft(4, '0');
                sub += gotNew;
                Console.WriteLine(gotNew);
            }

            Console.WriteLine(sub);
            string output = "00000000000000000000000000000000";


            for (int i = 0; i < 32; i++)
            {
                int id = permutation[i];
                StringBuilder sb = new StringBuilder(output);
                sb[id] = sub[i];
                output = sb.ToString();
            }

            message = output;
            hexKey = "0xCC835454";
            key = Convert.ToString(Convert.ToInt32(hexKey, 16), 2).PadLeft(32, '0');


            combine = "";
            for (int i = 0; i < 32; i++)
            {
                if (message[i] == '1' ^ key[i] == '1')
                {
                    combine += "1";
                }
                else
                {
                    combine += "0";
                }
            }


            sub = "";


            for (int i = 0; i < 8; i++)
            {
                string toKill = combine.Substring(i * 4, 4);
                int id = Convert.ToInt32(toKill, 2);
                string wow = Convert.ToString(sBox[id]);
                string gotNew = Convert.ToString(Convert.ToInt32(wow, 16), 2).PadLeft(4, '0');
                sub += gotNew;
                Console.WriteLine(gotNew);
            }

            Console.WriteLine(sub);
            output = "00000000000000000000000000000000";


            for (int i = 0; i < 32; i++)
            {
                int id = permutation[i];
                StringBuilder sb = new StringBuilder(output);
                sb[id] = sub[i];
                output = sb.ToString();
            }

            Console.WriteLine(output);

            Console.WriteLine("\n\n\n" + (Convert.ToInt32(output, 2)).ToString("X"));
        }
    }
}
