using System.Linq;

namespace Vignere
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDictionary<string, int> DistinctWordCount = new SortedDictionary<string, int>();

            string words = File.ReadAllText("Code.txt");
            words.Trim();

            for (int i = 0; i < words.Length - 2; i++)
            {
                string word = words.Substring(i, 3);
                if (DistinctWordCount.ContainsKey(word))
                {
                    ++DistinctWordCount[word];
                }
                else
                {
                    DistinctWordCount.Add(word, 1);
                }
            }

            int max = 0;
            string mostUsed = "";

            foreach (KeyValuePair<string, int> dist in DistinctWordCount)
            {
                if (dist.Value > 2) 
                {
                    Console.WriteLine("Key: {0}, Value: {1}", dist.Key, dist.Value);

                    if (dist.Value > max)
                    {
                        max = dist.Value;
                        mostUsed = dist.Key;
                    }
                }
            }

            List<int> indexes = new List<int>();

            for (int i = 0; i < words.Length - 2; i++)
            {
                if (mostUsed == words.Substring(i, 3))
                {
                    indexes.Add(i + 1);
                }
            }


            Console.WriteLine("Indexes of the most used triagram");
            foreach (int i in indexes) 
            {
                Console.WriteLine(i);
            }
        }
    }
}
