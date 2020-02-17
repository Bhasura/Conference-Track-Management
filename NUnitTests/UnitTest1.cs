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
        [TestCase("60min", "60min")]
        public void Adding60MinSessionAddsToTrackAndReturnsAddedTalkName(string input, string expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("45min", "45min")]
        public void Adding45MinSessionAddsToTrackAndReturnsAddedTalkName(string input, string expected)
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
                output = OnAddTalkName(track1, newTalk);
            }
            if (newTalk.Contains("45min"))
            {
                output = OnAddTalkName(track1, newTalk);
            }

            return output;
        }

        private static string OnAddTalkName(List<Tracks> availableTracks, string newTalk)
        {
            string addedTalkName = string.Empty;
            Tracks track = new Tracks(newTalk);
            availableTracks.Add(track);
            int indexOfTalk = availableTracks.IndexOf(track);
            addedTalkName = availableTracks[indexOfTalk].TalkName;
            return addedTalkName;
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