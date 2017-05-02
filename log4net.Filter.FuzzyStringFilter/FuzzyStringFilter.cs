using System;
using System.Collections.Generic;
using System.Linq;
using DuoVia.FuzzyStrings;
using log4net.Core;

namespace log4net.Filter.FuzzyStringFilter
{
    public class FuzzyStringFilter : FilterSkeleton
    {
        private readonly List<AcceptedMessage> _acceptedMessages;

        public FuzzyStringFilter()
        {
            _acceptedMessages = new List<AcceptedMessage>();
            DecaySeconds = 300;
            MinimumDiceCoefficient = .8;
        }

        public override FilterDecision Decide(LoggingEvent loggingEvent)
        {
            var decision = FilterDecision.Neutral;
            var matchedInList = false;
            var renderedMessage = loggingEvent.RenderedMessage;
            
            if (_acceptedMessages.Any())
            {
                foreach (var acceptedMessage in _acceptedMessages)
                {
                    var matchCoefficient = acceptedMessage.MessageText.DiceCoefficient(renderedMessage);
                    if (matchCoefficient >= MinimumDiceCoefficient)
                    {
                        //If the decay period has expired, do not deny the message
                        if (acceptedMessage.MessageTimestamp.AddSeconds(DecaySeconds).UtcDateTime < loggingEvent.TimeStampUtc)
                        {
                            acceptedMessage.MessageTimestamp = DateTimeOffset.Now;
                            acceptedMessage.MessageText = renderedMessage;
                        }
                        else
                        {
                            decision = FilterDecision.Deny;
                        }
                        matchedInList = true;
                        break;
                    }
                }
            }

            //If it's not getting filtered out or updated, add it to the list
            if (!matchedInList)
            {
                _acceptedMessages.Add(new AcceptedMessage
                {
                    MessageText = loggingEvent.RenderedMessage,
                    MessageTimestamp = DateTimeOffset.Now
                });
            }

            return decision;
        }

        private class AcceptedMessage
        {
            public DateTimeOffset MessageTimestamp { get; set; }
            public string MessageText { get; set; }
        }

        public int DecaySeconds { get; set; }

        public double MinimumDiceCoefficient { get; set; }
    }
}
