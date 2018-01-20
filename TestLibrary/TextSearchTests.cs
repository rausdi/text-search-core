using System;
using System.IO;
using System.Text.RegularExpressions;
using Xunit;
using Quality;

namespace TestLibrary
{
    public class TextSearch_Files
    {
        TextSearch _search;

        public TextSearch_Files()
        {
            _search = new TextSearch();
        }

        [Fact]
        public void IsTextLoaded()
        {
            string pattern = @"\w*\s*\W*";
            Regex reg = new Regex(pattern);
            Assert.Matches(reg, _search.LoadText(_search.path));
        }

        [Fact]
        public void IsWrongArgumentThrowException()
        {
            Assert.Throws<ArgumentException>(() => _search.LoadText(""));
        }

        [Fact]
        public void IsFileNotFound()
        {
            string startsWith = "Could not find file";
            Assert.StartsWith(startsWith, _search.LoadText("path/text.txt"));
        }
    }

    public class TextSearch_Search
    {
        TextSearch _search;

        public TextSearch_Search()
        {
            _search = new TextSearch();
        }

        [Fact]
        public void ReturnedStringIsNotEmpty()
        {
            Assert.NotEqual(String.Empty, _search.GetPattern(0, 1));
            Assert.NotEqual(String.Empty, _search.GetPattern(0, 0));
            Assert.NotEqual(String.Empty, _search.GetPattern(1, 1));
        }

        [Fact]
        public void ReturnedStringIsEmpty()
        {
            Assert.Equal(String.Empty, _search.GetPattern(1, 0));
            Assert.Equal(String.Empty, _search.GetPattern(5, 2));
            Assert.Equal(String.Empty, _search.GetPattern(10, 4));

            TextSearch s = new TextSearch("");
            Assert.Equal(String.Empty, s.GetPattern(0, 0));
        }

        [Fact]
        public void RightResultIsExpected()
        {
            TextSearch s = new TextSearch("я иду гулять с я иду гулять в я иду гулять на я иду гулять с");
            Assert.Equal("я", s.GetPattern(0, 0));
            Assert.Equal("я иду", s.GetPattern(0, 1));
            Assert.Equal("я иду гулять", s.GetPattern(0, 2));
            Assert.Equal("в я", s.GetPattern(7, 8));
        }

        [Fact]
        public void WrongResultIsExpected()
        {
            TextSearch s = new TextSearch("я иду гулять с я иду гулять в я иду гулять на я иду гулять с");
            Assert.NotEqual("", s.GetPattern(0, 0));
            Assert.NotEqual("иду", s.GetPattern(5, 0));
        }

        [Fact]
        public void ReturnedTypeIsString()
        {
            Assert.IsType(typeof(String), _search.GetPattern(1, 0));
        }

        [Fact]
        public void ExpectedOutOfRangeException()
        {
            TextSearch s = new TextSearch("я иду гулять с я иду гулять в я иду гулять на я иду гулять с");
            Assert.Throws<ArgumentOutOfRangeException>(() => s.GetPattern(-1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => s.GetPattern(5, 16));

            s = new TextSearch("");
            Assert.Throws<ArgumentOutOfRangeException>(() => s.GetPattern(0, 1));
        }
    }
}
