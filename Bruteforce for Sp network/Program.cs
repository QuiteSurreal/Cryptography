using System.Text;

namespace Bruteforce_for_Sp_network
{
    internal class Program
    {
        static string sBox = "c56b90ad3ef84712";

        static int[] permutation = { 20, 25, 19, 7, 31, 23, 8, 5, 11, 14, 17, 22, 18, 1, 0, 28, 29, 21, 12, 27, 30, 26, 9, 16, 6, 15, 10, 13, 24, 3, 4, 2 };

        static string[] hexMessages = { "0x621cc14c", "0xbdfa29ed", "0x9aeb890a", "0xf36ae096", "0x80058b52" };

        static string[] hexOutputs = { "0x25ab95e1", "0x8abf147a", "0x59ce7c45", "0x1e3a76f2", "0xed2185c4" };


        static void Main(string[] args)
        {
            Brute();
        }

        static void Brute()
        {
            string finalKey1 = "";
            string finalKey2 = "00000000000000000000000000000000";

            for (int all = 0; all < 8; all++)
            {
                List<string> poss1 = new List<string>();
                List<string> poss2 = new List<string>();

                for (int msgSize = 0; msgSize < 5; msgSize++)
                {


                    string message = Convert.ToString(Convert.ToInt32(hexMessages[msgSize], 16), 2).PadLeft(32, '0');
                    message = message.Substring(all * 4, 4);
                    string output = Convert.ToString(Convert.ToInt32(hexOutputs[msgSize], 16), 2).PadLeft(32, '0');


                    string befPerm = "00000000000000000000000000000000";

                    for (int i = 0; i < 32; i++)
                    {
                        int id = permutation[i];
                        StringBuilder sb = new StringBuilder(befPerm);
                        sb[i] = output[id];
                        befPerm = sb.ToString();
                    }

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

                    Console.WriteLine(befSub);

                    for (int i = 0; i < 16; i++)
                    {
                        string key1 = Convert.ToString(i, 2).PadLeft(4, '0');

                        string combine = "";
                        for (int j = 0; j < 4; j++)
                        {
                            if (message[j] == '1' ^ key1[j] == '1')
                            {
                                combine += "1";
                            }
                            else
                            {
                                combine += "0";
                            }
                        }

                        int id = Convert.ToInt32(combine, 2);
                        string wow = Convert.ToString(sBox[id]);
                        string gotNew = Convert.ToString(Convert.ToInt32(wow, 16), 2).PadLeft(4, '0');
                        string sub = gotNew;

                        output = "00000000000000000000000000000000";


                        for (int j = 0; j < 4; j++)
                        {
                            id = permutation[j + all * 4];
                            StringBuilder sb = new StringBuilder(output);
                            sb[id] = sub[j];
                            output = sb.ToString();
                        }

                        bool allowed = true;

                        for (int j = 0; j < 16; j++)
                        {
                            string key2 = Convert.ToString(j, 2).PadLeft(4, '0');
                            message = output;

                            combine = "";
                            for (int k = 0; k < 4; k++)
                            {
                                if (message[permutation[k + all * 4]] == '1' ^ key2[k] == '1')
                                {
                                    combine += "1";
                                }
                                else
                                {
                                    combine += "0";
                                }
                            }

                            if (combine[0] == befSub[permutation[0 + all * 4]] && combine[1] == befSub[permutation[1 + all * 4]] && 
                                combine[2] == befSub[permutation[2 + all * 4]] && combine[3] == befSub[permutation[3 + all * 4]] && allowed)
                            {
                                poss1.Add(key1);
                                poss2.Add(key2);
                                allowed = false;
                            }
                            /*else if (combine[0] == befSub[permutation[0 + all * 4]] && combine[1] == befSub[permutation[1 + all * 4]] &&
                                    combine[2] == befSub[permutation[2 + all * 4]] && combine[3] == befSub[permutation[3 + all * 4]])
                            {
                                poss2.Add(key2);
                            }*/
                        }
                    }
                }

                var g1 = poss1.GroupBy( i => i );
                bool done = false;

                foreach (var grp1 in g1) 
                {
                    if (grp1.Count() == 5 && !done)
                    {
                        var g2 = poss2.GroupBy( i => i );

                        foreach (var grp2 in g2)
                        {
                            if (grp2.Count() == 5 && !done)
                            {
                                finalKey1 += grp1.Key;
                                
                                for (int i = 0; i < 4; i++)
                                {
                                    int id = permutation[i + all * 4];
                                    StringBuilder sb = new StringBuilder(finalKey2);
                                    sb[id] = grp2.Key[i];
                                    finalKey2 = sb.ToString();
                                }
                                done = true;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(finalKey1);
            Console.WriteLine(finalKey2);

            Console.WriteLine((Convert.ToInt32(finalKey1, 2)).ToString("X"));
            Console.WriteLine((Convert.ToInt32(finalKey2, 2)).ToString("X"));
        }
    }
}
