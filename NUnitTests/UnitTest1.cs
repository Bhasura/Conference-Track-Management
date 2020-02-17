using System.Collections.Generic;
using NUnit.Framework;

namespace NUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("60min", "added to Track")]
        public void Adding60MinSessionReturnsAddedToTrack(string input, string expected)
        {
            AssertTrue(input, expected);
        }
        private static void AssertTrue(string input, string expected)
        {
            var sut = new TalkEntry();
            var result = sut.AddToTrack(input);
            Assert.That(result, Is.EqualTo(expected));
        }
    }

    public class TalkEntry
    {
        public string AddToTrack(string newTalk)
        {
            string output = string.Empty;
            if (newTalk.Contains("60min"))
            {
                output = "added to Track";

            }

            return output;
        }
    }
    public class Tracks
    {
        private List <Sessions> Track1 { get; set; }
        private List<Sessions> Track2 { get; set; }

    }

    public class Sessions
    {
        
    }

    public class Talks
    {

    }
}