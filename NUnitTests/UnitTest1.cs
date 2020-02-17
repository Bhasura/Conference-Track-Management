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
        [Test]
        [TestCase("60min", "added to Track")]
        public void Adding60MinSessionAddsToTrackAndReturnsAddedToTrack(string input, string expected)
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
            List<Tracks> track1 = new List<Tracks>();
            string output = string.Empty;
            if (newTalk.Contains("60min"))
            {
                Tracks track = new Tracks(newTalk);
                track1.Add(track);
                output = "added to Track";

            }

            return output;
        }
    }

    public class Tracks
    {
        public string TalkName { get; set; }

        public Tracks(string talkName)
        {
            TalkName = talkName;
        }
    }
  
}