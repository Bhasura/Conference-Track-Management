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
        [TestCase("60min", 60)]
        [TestCase("Writing Fast Tests Against Enterprise Rails 60min", 60)]

        public void Adding60MinSessionAddsToTrackAndReturnsAddedTalkName(string input, int expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("45min", 45)]
        [TestCase("Ruby Errors from Mismatched Gem Versions 45min", 45)]

        public void Adding45MinSessionAddsToTrackAndReturnsAddedTalkName(string input, int expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("30min", 30)]
        [TestCase("Lua for the Masses 30min ", 30)]
        public void Adding30MinSessionAddsToTrackAndReturnsAddedTalkName(string input, int expected)
        {
            AssertTrue(input, expected);
        }
        private static void AssertTrue(string input, int expected)
        {
            var sut = new TalkEntry();
            var result = sut.AddToTrack(input);
            Assert.That(result, Is.EqualTo(expected));
        }
    }

    public class TalkEntry
    {
        public int AddToTrack(string newTalk)
        {
            List<Tracks> track1 = new List<Tracks>();
            int minutesOfNewTalk = 0;
            if (newTalk.Contains("60min"))
            {
                minutesOfNewTalk = OnAddTalkName(track1, newTalk, 60);
            }
            if (newTalk.Contains("45min"))
            {
                minutesOfNewTalk = OnAddTalkName(track1, newTalk, 45);
            }
            if (newTalk.Contains("30min"))
            {
                minutesOfNewTalk = OnAddTalkName(track1, newTalk, 30);
            }

            return minutesOfNewTalk;
        }

        private static int OnAddTalkName(List<Tracks> availableTracks, string newTalk, int minutes)
        {
            Tracks track = new Tracks(newTalk);
            availableTracks.Add(track);
            return minutes;
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