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
            var track = new Tracks();
            var session = new Session();
           // var track1 = GetTrack1List();
            return MinutesOfNewTalk(newTalk, track, session);
        }

        //private static List<Talk> GetTrack1List()
        //{
        //    List<Talk> track1 = new List<Talk>();
        //    return track1;
        //}

        private static string MinutesOfNewTalk(string newTalk, Tracks track, Session session)
        {
            string output = string.Empty;
            if (newTalk.Contains("60min"))
            {
                output = OnAddTalkName(newTalk, track, session);
            }

            if (newTalk.Contains("45min"))
            {
                output = OnAddTalkName(newTalk, track, session);
            }

            if (newTalk.Contains("30min"))
            {
                output = OnAddTalkName(newTalk, track, session);

            }

            if (newTalk.Contains("lightning"))
            {
                output = OnAddTalkName(newTalk, track, session);
            }

            return output;
        }

        private static string OnAddTalkName(string newTalk, Tracks track, Session session)
        {
            session.SetMorningSessionAvailability(newTalk);
            track.Track.Add(session);
            return track.Track[0].MorningSession[0].Time + " " + track.Track[0].MorningSession[0].TalkName;
        }
    }

    public class Tracks
    {
        public List<Session> Track { get; set; }

        public Tracks()
        {
            Track = new List<Session>();
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
                }
                else
                {
                    i++;
                }
            }
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