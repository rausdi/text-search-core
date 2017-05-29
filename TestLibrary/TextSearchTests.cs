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

        public TextSearch_Files() {
            _search = new TextSearch();
        }

        [Fact]
        public void IsTextLoaded() {
            string pattern = @"\w*\s*\W*";
            Regex reg = new Regex(pattern);
            Assert.Matches(reg, _search.LoadText(_search.path));
        }

        [Fact]
        public void IsWrongArgumentThrowException() {
            Assert.Throws<ArgumentException>(() => _search.LoadText(""));
        }

        [Fact]
        public void IsFileNotFound() {
            string startsWith = "Could not find file";
            Assert.StartsWith(startsWith, _search.LoadText("path/text.txt"));
        }
    }
}
