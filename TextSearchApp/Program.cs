using System;
using Quality;

namespace TextSearchApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TextSearch search = new TextSearch("я иду гулять с я иду гулять в я иду гулять на я иду гулять с");
            search.Search();
            search.OutputFragments();
        }
    }
}
