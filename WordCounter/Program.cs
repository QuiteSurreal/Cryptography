using System.Linq;

namespace WordCounter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDictionary<char, double> WordProbabilities = new SortedDictionary<char, double>()
            {
                { 'a', 8.167 },
                { 'b', 1.492 },
                { 'c', 2.782 },
                { 'd', 4.253 },
                { 'e', 12.702 },
                { 'f', 2.228 },
                { 'g', 2.015 },
                { 'h', 6.094 },
                { 'i', 6.966 },
                { 'j', 0.153 },
                { 'k', 0.772 },
                { 'l', 4.025 },
                { 'm', 2.406 },
                { 'n', 6.749 },
                { 'o', 7.507 },
                { 'p', 1.929 },
                { 'q', 0.095 },
                { 'r', 5.987 },
                { 's', 6.327 },
                { 't', 9.056 },
                { 'u', 2.758 },
                { 'v', 0.978 },
                { 'w', 2.360 },
                { 'x', 0.150 },
                { 'y', 1.974 },
                { 'z', 0.074 }
            };



            IDictionary<char, double> DistinctWordCount = new SortedDictionary<char, double>();

            string words = File.ReadAllText("Code.txt");
            words.Trim();

            int sum = 0;

            //for (int i = 0;  i < words.Length; i += 6) 
            foreach (char word in words)
            {
                string low = word.ToString();
                low = low.ToLower();
                low.ToCharArray();
                char lowCh = low[0];

                if (lowCh == 'á')
                {
                    lowCh = 'a';
                }
                else if (lowCh == 'ó' ||  lowCh == 'ö' || lowCh == 'ő')
                {
                    lowCh = 'o';
                }
                else if (lowCh == 'ú' || lowCh == 'ü' || lowCh == 'ű')
                {
                    lowCh = 'u';
                }

                if (WordProbabilities.ContainsKey(lowCh))
                {
                    if (DistinctWordCount.ContainsKey(lowCh))
                    {
                        ++DistinctWordCount[lowCh];
                    }
                    else
                    {
                        DistinctWordCount.Add(lowCh, 1);
                    }
                    sum++;
                }
            }
            

            foreach (KeyValuePair<char, double> dist in DistinctWordCount) 
            {
                Console.WriteLine("Key: {0}, Value: {1}", dist.Key, dist.Value);
            }


            Console.WriteLine("\nCounting probabilities..\n");
            foreach (KeyValuePair<char, double> word in WordProbabilities)
            {
                double probability = 0;
                double value;
                int k;
                for (int i = 0; i < 26; i++)
                {
                    if (DistinctWordCount.TryGetValue(word.Key, out value))
                    {
                        probability += ((word.Value / 100) * (value / sum));
                    }
                }
            }


            foreach (KeyValuePair<char, double> dist in DistinctWordCount)
            {
                Console.WriteLine("Key: {0}, Probability: {1}", dist.Key, dist.Value / sum * 100);
            }
        }
    }
}
