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
        [TestCase("Writing Fast Tests Against Enterprise Rails 60min", "09:00AM Writing Fast Tests Against Enterprise Rails 60min")]

        public void Adding60MinSessionAddsToTrackAndReturnsAddedTalkName(string input, string expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("Ruby Errors from Mismatched Gem Versions 45min", "09:00AM Ruby Errors from Mismatched Gem Versions 45min")]
        public void Adding45MinSessionAddsToTrackAndReturnsAddedTalkName(string input, string expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("Lua for the Masses 30min", "09:00AM Lua for the Masses 30min")]
        public void Adding30MinSessionAddsToTrackAndReturnsAddedTalkName(string input, string expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("Rails for Python Developers lightning", "09:00AM Rails for Python Developers lightning")]
        public void AddingLightningMinSessionAddsToTrackAndReturnsAddedTalkName(string input, string expected)
        {
            AssertTrue(input, expected);
        }
        //public void GetTrack1ReturnsListOfTalks(string input, int expected)
        //{
        //    var sut = new TalkEntry();
        //    var result = sut.AddToTrack(input);
        //    Assert.That(result, Is.EqualTo(expected));
        //}
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
            var track1 = GetTrack1List();
            List morningSession = new List();
            return  MinutesOfNewTalk(newTalk, morningSession, track1);
        }

        private static List<Tracks> GetTrack1List()
        {
            List<Tracks> track1 = new List<Tracks>();
            return track1;
        }


        private static string MinutesOfNewTalk(string newTalk, List minutesOfNewTalk, List<Tracks> availableTracks)
        {
            if (newTalk.Contains("60min"))
            {
                OnAddTalkName(availableTracks, newTalk);
            }

            if (newTalk.Contains("45min"))
            {
                OnAddTalkName(availableTracks, newTalk);
            }

            if (newTalk.Contains("30min"))
            {
                OnAddTalkName(availableTracks, newTalk);
            }

            if (newTalk.Contains("lightning"))
            { 
                OnAddTalkName(availableTracks, newTalk);
            }

            return availableTracks[0].ToString();
        }

        private static void OnAddTalkName(List<Tracks> availableTracks, string newTalk)
        {
            var track = new Tracks(newTalk, "09:00AM ");
            availableTracks.Add(track);
        }
    }

    public class Tracks
    {
        private string TalkName { get; set; }
        private string ScheduleTime { get; set; } 

        public Tracks(string talkName, string availableTime)
        {
            TalkName = talkName;
            ScheduleTime = availableTime;
        }

        public override string ToString()
        {
            return ScheduleTime + TalkName;
        }
    }
  
}