using System;
using log4net.Core;
using NUnit.Framework;

namespace log4net.Filter.FuzzyStringFilter.Tests
{
    [TestFixture]
    public class FuzzyStringFilterTest
    {
        [Test]
        public void TestFilterDeny()
        {
            var fuzzyStringFilter = new FuzzyStringFilter();

            var logEventOne = GetLoggingEvent("This is a test message", DateTimeOffset.Now);
            var logEventTwo = GetLoggingEvent("This is a test message2", DateTimeOffset.Now);
            var logEventThree = GetLoggingEvent("This is a totally different message", DateTimeOffset.Now);
            var logEventFour = GetLoggingEvent("This is a totally different message too", DateTimeOffset.Now);
            var logEventFive = GetLoggingEvent("This is a test message", DateTimeOffset.Now.AddSeconds(301));

            var decisionOne = fuzzyStringFilter.Decide(logEventOne);
            var decisionTwo = fuzzyStringFilter.Decide(logEventTwo);
            var decisionThree = fuzzyStringFilter.Decide(logEventThree);
            var decisionFour = fuzzyStringFilter.Decide(logEventFour);
            var decisionFive = fuzzyStringFilter.Decide(logEventFive);

            Assert.AreEqual(FilterDecision.Neutral, decisionOne);
            Assert.AreEqual(FilterDecision.Deny, decisionTwo);
            Assert.AreEqual(FilterDecision.Neutral, decisionThree);
            Assert.AreEqual(FilterDecision.Deny, decisionFour);
            Assert.AreEqual(FilterDecision.Neutral, decisionFive);
        }

        private LoggingEvent GetLoggingEvent(string text, DateTimeOffset timestamp)
        {
            return new LoggingEvent(new LoggingEventData
            {
                TimeStampUtc = timestamp.UtcDateTime,
                Message = text
            });
        }
    }
}
