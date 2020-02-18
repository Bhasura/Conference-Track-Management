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
            "09:00AM Writing Fast Tests Against Enterprise Rails 60min")]

        public void Adding60MinSessionAddsToTrackAndReturnsAddedTalkName(string input, string expected)
        {
            AssertTrue(input, expected);
        }

        [TestCase("Ruby Errors from Mismatched Gem Versions 45min",
            "09:00AM Ruby Errors from Mismatched Gem Versions 45min")]
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

        [Test]
        public void AddingMultipleTalksToMorningSessionTrackAndReturnsAllTheAddedTracks()
        {
            var sut = new TalkEntry();
            sut.InitConferenceScheduler();
            var result1 = sut.AddToTrack("Lua for the Masses 30min");
            var result2 = sut.AddToTrack("Ruby Errors from Mismatched Gem Versions 45min");
            Assert.That(result1, Is.EqualTo("09:00AM Lua for the Masses 30min"));
            Assert.That(result2, Is.EqualTo("10:00AM Ruby Errors from Mismatched Gem Versions 45min"));
        }

        private static void AssertTrue(string input, string expected)
        {
            var sut = new TalkEntry();
            sut.InitConferenceScheduler();
            var result = sut.AddToTrack(input);
            Assert.That(result, Is.EqualTo(expected));
        }
    }

  

    public class TalkEntry
    {
        private static List<Session> Track;
        private static List<string> TrackOutput;
        private static Session session;

        public string AddToTrack(string newTalk)
        {
            string newOutputLine = MinutesOfNewTalk(newTalk);
            TrackOutput.Add(newOutputLine);
            return newOutputLine;
        }

        public void InitConferenceScheduler()
        {
            Track = new List<Session>();
            TrackOutput = new List<string>();
            session = new Session();
        }

        private static string MinutesOfNewTalk(string newTalk)
        {
            string output = string.Empty;
            if (newTalk.Contains("60min"))
            {
                output = OnAddTalkName(newTalk);
            }

            if (newTalk.Contains("45min"))
            {
                output = OnAddTalkName(newTalk);
            }

            if (newTalk.Contains("30min"))
            {
                output = OnAddTalkName(newTalk);

            }

            if (newTalk.Contains("lightning"))
            {
                output = OnAddTalkName(newTalk);
            }

            return output;
        }

        private static string OnAddTalkName(string newTalk)
        {
            string newEntry = string.Empty;

            if (Track.Any())
            {
                session.SetMorningSessionAvailability(newTalk);
                var indexOfTalkName = session.GetTalkNameIndex(newTalk);
                newEntry = Track[0].MorningSession[indexOfTalkName].Time + " " + Track[0].MorningSession[indexOfTalkName].TalkName;
            }
            else
            { 
                Track.Add(session);
                session.SetMorningSessionAvailability(newTalk);
                var indexOfTalkName = session.GetTalkNameIndex(newTalk);
                newEntry = Track[0].MorningSession[indexOfTalkName].Time + " " + Track[0].MorningSession[indexOfTalkName].TalkName;
            }

            return newEntry;
        }
    }

    public class Talk
    {
        private string TalkName { get; set; }
        private string ScheduleTime { get; set; }

        public Talk(string talkName, string availableTime)
        {
            TalkName = talkName;
            ScheduleTime = availableTime;
        }

        public override string ToString()
        {
            return ScheduleTime + " " + TalkName;
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
                SetSchedule("09:00AM", true, ""),
                SetSchedule("10:00AM", true, "")
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
            for (var i = 0; i < MorningSession.Count; i++)
            {
                if (MorningSession[i].TalkName == talkName)
                {
                    indexOfTalkName = i;
                }
                else
                {
                    indexOfTalkName = 0;
                }
                
            }

            return indexOfTalkName;
        }

        private Schedule SetSchedule(string time, bool availability, string talkName)
        {
            var schedule = new Schedule(time, availability, talkName);
            return schedule;
        }
    }

    public class Schedule
    {
        public string Time { get; set; }
        public bool IsAvailable { get; set; }
        public string TalkName { get; set; }

        public Schedule(string time, bool isAvailable, string talkName)
        {
            Time = time;
            IsAvailable = isAvailable;
            TalkName = talkName;
        }


    }
}