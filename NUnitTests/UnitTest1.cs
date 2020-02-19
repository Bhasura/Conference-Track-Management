using System;
using System.Collections.Generic;
using System.Linq;
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
        [TestCase("Writing Fast Tests Against Enterprise Rails 60min",
            "09:00 am Writing Fast Tests Against Enterprise Rails 60min")]

        public void Adding60MinSessionAddsToTrackAndReturnsAddedTalkName(string input, string expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("Ruby Errors from Mismatched Gem Versions 45min",
            "09:00 am Ruby Errors from Mismatched Gem Versions 45min")]
        public void Adding45MinSessionAddsToTrackAndReturnsAddedTalkName(string input, string expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("Lua for the Masses 30min", "09:00 am Lua for the Masses 30min")]
        public void Adding30MinSessionAddsToTrackAndReturnsAddedTalkName(string input, string expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("Rails for Python Developers lightning", "09:00 am Rails for Python Developers lightning")]
        public void AddingLightningMinSessionAddsToTrackAndReturnsAddedTalkName(string input, string expected)
        {
            AssertTrue(input, expected);
        }

        [Test]
        public void AddingTwoTalksToTrackSessionAndReturnsAllTheAddedTracks()
        {
            var sut = new Scheduler();
            sut.InitConferenceScheduler();
            sut.AddToTrack("Lua for the Masses 30min");
            sut.AddToTrack("Ruby Errors from Mismatched Gem Versions 45min");

            var result = sut.GetTrackOutput();
            Assert.That(result[0], Is.EqualTo("09:00 am Lua for the Masses 30min"));
            Assert.That(result[1], Is.EqualTo("10:00 am Ruby Errors from Mismatched Gem Versions 45min"));
        }

        [Test]
        public void AddingThreeTalksToTrackSessionAndReturnsAllTheAddedTracks()
        {
            var sut = new Scheduler();
            sut.InitConferenceScheduler();
            sut.AddToTrack("Lua for the Masses 30min");
            sut.AddToTrack("Ruby Errors from Mismatched Gem Versions 45min");
            sut.AddToTrack("Communicating Over Distance 60min");

            var result = sut.GetTrackOutput();
            Assert.That(result[0], Is.EqualTo("09:00 am Lua for the Masses 30min"));
            Assert.That(result[1], Is.EqualTo("10:00 am Ruby Errors from Mismatched Gem Versions 45min"));
            Assert.That(result[2], Is.EqualTo("11:00 am Communicating Over Distance 60min"));
        }

        private static void AssertTrue(string input, string expected)
        {
            var sut = new Scheduler();
            sut.InitConferenceScheduler();
            sut.AddToTrack(input);
            var result = sut.GetTrackOutput();
            Assert.That(result[0], Is.EqualTo(expected));
        }
    }

  

    public class Scheduler
    {
        private static List<Session> Track;
        private static List<string> TrackOutput;
        private static Session TrackSession;

        public void AddToTrack(string newTalk)
        {
            string newOutputLine = MinutesOfNewTalk(newTalk);
            TrackOutput.Add(newOutputLine);
        }

        public List<string> GetTrackOutput()
        {
            return TrackOutput;
        }
        public void InitConferenceScheduler()
        {
            Track = new List<Session>();
            TrackOutput = new List<string>();
            TrackSession = new Session();
        }

        private static string MinutesOfNewTalk(string newTalk)
        {
            string scheduledTalk = string.Empty;
            if (newTalk.Contains("60min"))
            {
                scheduledTalk = OnAddTalkName(newTalk);
            }

            if (newTalk.Contains("45min"))
            {
                scheduledTalk = OnAddTalkName(newTalk);
            }

            if (newTalk.Contains("30min"))
            {
                scheduledTalk = OnAddTalkName(newTalk);

            }

            if (newTalk.Contains("lightning"))
            {
                scheduledTalk = OnAddTalkName(newTalk);
            }

            return scheduledTalk;
        }

        private static string OnAddTalkName(string newTalk)
        {
            string newEntry = string.Empty;

            if (Track.Any())
            {
                newEntry = GetNewlyAddedSessionDetails(newTalk);
            }
            else
            { 
                Track.Add(TrackSession);
                newEntry = GetNewlyAddedSessionDetails(newTalk);
            }

            return newEntry;
        }

        private static string GetNewlyAddedSessionDetails(string newTalk)
        {
            string newEntry;
            TrackSession.SetMorningSessionAvailability(newTalk);
            var indexOfTalkName = TrackSession.GetTalkNameIndex(newTalk);
            newEntry = Track[0].MorningSession[indexOfTalkName].Time.ToString("HH:mm tt") + " " + Track[0].MorningSession[indexOfTalkName].TalkName;
            return newEntry;
        }
    }

    public class Session
    {
        public List<Schedule> MorningSession { get; set; }
        private List<Schedule> AfternoonSession { get; set; }

        public Session()
        {
            MorningSession = new List<Schedule>()
            {
                SetSchedule(new DateTime(2020,2,19,9,0,0,0), true, ""),
                SetSchedule(new DateTime(2020,2,19,10,0,0,0), true, ""),
                SetSchedule(new DateTime(2020,2,19,11,0,0,0), true, ""),
                SetSchedule(new DateTime(2020,2,19,12,0,0,0), false, "Lunch")
            };
        }

        public void SetMorningSessionAvailability(string talkName)
        {
            for (var i = 0; i < MorningSession.Count;)
            {
                if (MorningSession[i].IsAvailable)
                {
                    MorningSession[i].IsAvailable = false;
                    MorningSession[i].TalkName = talkName;
                    break;
                }
                else
                {
                    i++;
                }
            }
        }

        public int GetTalkNameIndex(string talkName)
        {
            var indexOfTalkName = 0;
            for (var i = 0; i < MorningSession.Count;)
            {
                if (MorningSession[i].TalkName == talkName)
                {
                    indexOfTalkName = i;
                    break;
                }
                else
                {
                    i++;
                }
                
            }

            return indexOfTalkName;
        }

        private Schedule SetSchedule(DateTime time, bool availability, string talkName)
        {
            var schedule = new Schedule(time, availability, talkName);
            return schedule;
        }
    }

    public class Schedule
    {
        public DateTime Time { get; set; }
        public bool IsAvailable { get; set; }
        public string TalkName { get; set; }

        public Schedule(DateTime time, bool isAvailable, string talkName)
        {
            Time = time;
            IsAvailable = isAvailable;
            TalkName = talkName;
        }
    }
}