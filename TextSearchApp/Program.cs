using System;
using Quality;

namespace TextSearchApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TextSearch search = new TextSearch();
            search.Search();
            search.OutputFragments();
        }
    }
}
