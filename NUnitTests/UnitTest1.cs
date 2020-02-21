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
            Assert.That(result[1], Is.EqualTo("09:30 am Ruby Errors from Mismatched Gem Versions 45min"));
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
            Assert.That(result[1], Is.EqualTo("09:30 am Ruby Errors from Mismatched Gem Versions 45min"));
            Assert.That(result[2], Is.EqualTo("10:15 am Communicating Over Distance 60min"));
        }

        [Test]
        public void AddingManyTalksToTrackSessionAndReturnsAllTheAddedTracksIncludingLunchTime()
        {
            var sut = new Scheduler();
            sut.InitConferenceScheduler();
            sut.AddToTrack("Lua for the Masses 30min");
            sut.AddToTrack("Ruby Errors from Mismatched Gem Versions 45min");
            sut.AddToTrack("Communicating Over Distance 60min");
            sut.AddToTrack("Common Ruby Errors 45min");
            sut.AddToTrack("Sit Down and Write 30min");

            var result = sut.GetTrackOutput();
            Assert.That(result[0], Is.EqualTo("09:00 am Lua for the Masses 30min"));
            Assert.That(result[1], Is.EqualTo("09:30 am Ruby Errors from Mismatched Gem Versions 45min"));
            Assert.That(result[2], Is.EqualTo("10:15 am Communicating Over Distance 60min"));
            Assert.That(result[3], Is.EqualTo("11:15 am Common Ruby Errors 45min"));
            Assert.That(result[4], Is.EqualTo("12:00 pm Lunch"));
            Assert.That(result[5], Is.EqualTo("13:00 pm Sit Down and Write 30min"));
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
            var newOutputLine = MinutesOfNewTalk(newTalk);
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
            var scheduledTalk = string.Empty;
            if (newTalk.Contains("60min"))
            {
                scheduledTalk = OnAddTalkName(newTalk, 60);
            }

            if (newTalk.Contains("45min"))
            {
                scheduledTalk = OnAddTalkName(newTalk, 45);
            }

            if (newTalk.Contains("30min"))
            {
                scheduledTalk = OnAddTalkName(newTalk, 30);

            }

            if (newTalk.Contains("lightning"))
            {
                scheduledTalk = OnAddTalkName(newTalk, 5);
            }

            return scheduledTalk;
        }

        private static string OnAddTalkName(string newTalk, int lengthOfTalk)
        {
            var newEntry = string.Empty;

            if (Track.Any())
            {
                newEntry = GetNewlyAddedSessionDetails(newTalk, lengthOfTalk);
            }
            else
            { 
                Track.Add(TrackSession);
                newEntry = GetNewlyAddedSessionDetails(newTalk, lengthOfTalk);
            }

            return newEntry;
        }

        private static string GetNewlyAddedSessionDetails(string newTalk, int lengthOfTalk)
        {
            TrackSession.SetNewScheduleInSession(newTalk, lengthOfTalk);
            var indexOfTalkName = TrackSession.GetTalkNameIndex(newTalk);
            if (TrackSession.Sessions[indexOfTalkName].StartTime.Hour == 13)
            {
                TrackOutput.Add("12:00 pm Lunch");
            }
            var newEntry = TrackSession.Sessions[indexOfTalkName].StartTime.ToString("HH:mm tt") + " " + TrackSession.Sessions[indexOfTalkName].TalkName;
            return newEntry;
        }
    }

    public class Session
    {
        public List<Schedule> Sessions { get; set; }

        public Session()
        {
            Sessions = new List<Schedule>();
        }

        public void SetNewScheduleInSession(string talkName, int talkLength)
        {
            if (Sessions.Any())
            {
                AddNewSchedule(talkName, talkLength);
            }
            else
            {
                AddFirstSchedule(talkName, talkLength);
            }
        }

        private void AddFirstSchedule(string talkName, int talkLength)
        {
            var startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0, 0);
            var endTime = startTime.AddMinutes(talkLength);

            var newSchedule = SetSchedule(startTime, endTime, talkName);
            Sessions.Add(newSchedule);
        }

        private void AddNewSchedule(string talkName, int talkLength)
        {
            var indexOfPreviousSchedule = Sessions.Count - 1;
            var newScheduleStartTime = Sessions[indexOfPreviousSchedule].EndTime;
            var newScheduledEndTime = newScheduleStartTime.AddMinutes(talkLength);
            newScheduledEndTime = CheckClashWithLunchTime(talkLength, newScheduledEndTime, ref newScheduleStartTime);

            var newSchedule = SetSchedule(newScheduleStartTime, newScheduledEndTime, talkName);
                Sessions.Add(newSchedule);
        }

        private DateTime CheckClashWithLunchTime(int talkLength, DateTime newScheduledEndTime, ref DateTime newScheduleStartTime)
        {
            if (newScheduledEndTime.Hour == 12 && newScheduledEndTime.Minute > 0)
            {
                var lunchTimeStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0, 0);
                var lunchTimeEnd = lunchTimeStart.AddMinutes(60);
                var lunchSchedule = SetSchedule(lunchTimeStart, lunchTimeEnd, "Lunch");
                Sessions.Add(lunchSchedule);
                newScheduleStartTime = lunchTimeEnd;
                newScheduledEndTime = lunchTimeEnd.AddMinutes(talkLength);
            }

            return newScheduledEndTime;
        }

        public int GetTalkNameIndex(string talkName)
        {
            var indexOfTalkName = 0;
            for (var i = 0; i < Sessions.Count;)
            {
                if (Sessions[i].TalkName == talkName)
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

        private Schedule SetSchedule(DateTime startTime,DateTime endTime, string talkName)
        {
            var schedule = new Schedule(startTime,endTime, talkName);
            return schedule;
        }
    }

    public class Schedule
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TalkName { get; set; }
        public Schedule(DateTime startTime,DateTime endTime, string talkName)
        {
            StartTime = startTime;
            EndTime = endTime;
            TalkName = talkName;
        }
    }
}