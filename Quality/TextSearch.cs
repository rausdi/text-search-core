using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Quality
{
    public struct FragmentsAmount {
        public String fragment;
        public int count;

        public FragmentsAmount(String fragment, int count) {
            this.fragment = fragment;
            this.count = count;
        }
    }

    public struct FragmentsPosition {
        public int startPos;
        public int endPos;

        public FragmentsPosition(int m, int k) {
            startPos = m;
            endPos = k;
        }
    }

    public class TextSearch
    {
        public String path = Path.GetFullPath("resource/text.txt");
        String text;
        List<String> wordsList = new List<String>();
        public List<FragmentsAmount> fragmentsAmountList = new List<FragmentsAmount>();
        public TextSearch() {
            text = LoadText(path);
            text = Regex.Replace(text, @"[^\w\s]", "");
            text = text.Replace(Environment.NewLine, " ");
            text = "я иду гулять с я иду гулять в я иду гулять на";
            wordsList = text.Split(' ').ToList();
        }

        public String LoadText(String path) {
            try {
                return File.ReadAllText(path);
            }
            catch (FileNotFoundException e) {
                return e.Message;
            }
        }

        public void Search() {
            List<FragmentsAmount> tempFragments = new List<FragmentsAmount>();
            List<FragmentsPosition> fragmentsPositionList = new List<FragmentsPosition>();
            int i = 0;
            int j = 2;
            while (wordsList.Count - j >= (j - i) + 1) {
                int counter = 1;
                bool hasMatch = false;
                String pattern = getPattern(i, j);
                int m = j + 1;
                int k = m + (j - i);
                while (k < wordsList.Count) {
                    String match = getPattern(m, k);
                    if (pattern == match) {
                        counter++;
                        hasMatch = true;
                        FragmentsPosition fp = new FragmentsPosition(m, k);
                        fragmentsPositionList.Add(fp);
                        m += (j - i) + 1;
                        k += (j - i) + 1;
                        //removeMatchedFragment(m, k);
                    } else {
                        m++;
                        k++;
                    }
                }
                if (hasMatch) {
                    FragmentsAmount fragment = new FragmentsAmount(pattern, counter);
                    //fragmentsAmountList.Add(fragment);
                    tempFragments.Add(fragment);
                    j++;
                } else
                {
                    if (tempFragments.Count() > 0) {
                        fragmentsAmountList.Add(tempFragments[tempFragments.Count() - 1]);
                        tempFragments.Clear();
                        if (fragmentsPositionList.Count() > 0) {
                            fragmentsPositionList.ForEach(delegate(FragmentsPosition fp) {
                                removeMatchedFragment(fp.startPos, fp.endPos);
                            });
                            fragmentsPositionList.Clear();  
                        }
                    }
                    i++;
                    j = i + 2;
                }
            }
        }

        public String getPattern(int startPos, int endPos) {
            List<String> patternList = new List<string>();
            for (int i = startPos; i <= endPos; i++) {
                patternList.Add(wordsList[i]);
            }
            return String.Join(" ", patternList.ToArray());
        }

        public void removeMatchedFragment(int startPos, int endPos) {
            for (int i = startPos; i <= endPos; i ++) {
                wordsList.RemoveAt(startPos);
            }
        }

        public void OutputFragmentsAmountList() {
            fragmentsAmountList.ForEach(delegate(FragmentsAmount fragment)
            {
                Console.WriteLine("string '{0}' found {1} times", fragment.fragment, fragment.count);
            });
        }
    }
}
