using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Quality
{
    public class TextSearch
    {
        public string path = Path.GetFullPath("resource/text.txt");
        public string text;
        List<string> wordsList = new List<string>();
        public Dictionary<string, int> fragments = new Dictionary<string, int>();
        public TextSearch() {
            text = LoadText(path);
            text = Regex.Replace(text, @"[^\w\s]", "");
            wordsList = text.Split(' ').ToList();
        }

        public TextSearch(string text) {
            this.text = text;
            wordsList = text.Split(' ').ToList();
        }

        public string LoadText(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (FileNotFoundException e)
            {
                return e.Message;
            }
        }

        public void Search() {
            SortedDictionary<string, int> fragmentsTemp = new SortedDictionary<string, int>();
            int i = 0;
            int j = 2;
            while (wordsList.Count - j >= (j - i) + 1) {
                int counter = 1;
                bool hasMatch = false;
                string pattern = GetPattern(i, j);
                int m = j + 1;
                int k = m + (j - i);
                if (fragments.ContainsKey(pattern)) {
                    i++;
                    j = i + 2;
                    continue;
                }
                while (k < wordsList.Count) {
                    string match = GetPattern(m, k);
                    if (pattern == match) {
                        counter++;
                        hasMatch = true;
                        m += (j - i) + 1;
                        k += (j - i) + 1;
                    } else {
                        m++;
                        k++;
                    }
                }
                if (hasMatch) {
                    fragmentsTemp.Add(pattern, counter);
                    j++;
                } else
                {
                    if (fragmentsTemp.Count() > 0) {
                        fragments.Add(fragmentsTemp.Keys.Last(), fragmentsTemp.Values.Last());
                        fragmentsTemp.Clear();
                        int t = j - i;
                        i += t;
                        j += i + 2;
                    } else {
                        i++;
                        j = i + 2;
                    }
                }
            }
        }

        public string GetPattern(int startPos, int endPos) {
            List<string> patternList = new List<string>();
            for (int i = startPos; i <= endPos; i++) {
                patternList.Add(wordsList[i]);
            }
            return string.Join(" ", patternList.ToArray());
        }

        public void OutputFragments() {
            foreach (KeyValuePair<string, int> fragment in fragments) {
                Console.WriteLine("string '{0}' found {1} times", fragment.Key, fragment.Value);
            }
        }
    }
}
