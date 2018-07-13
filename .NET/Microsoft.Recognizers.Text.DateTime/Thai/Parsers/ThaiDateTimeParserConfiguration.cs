using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Text.DateTime.Utilities;
using Microsoft.Recognizers.Definitions.Thai;

namespace Microsoft.Recognizers.Text.DateTime.Thai
{
    public class ThaiDateTimeParserConfiguration : BaseOptionsConfiguration, IDateTimeParserConfiguration
    {
        public string TokenBeforeDate { get; }

        public string TokenBeforeTime { get; }

        public IDateTimeExtractor DateExtractor { get; }

        public IDateTimeExtractor TimeExtractor { get; }

        public IDateTimeParser DateParser { get; }

        public IDateTimeParser TimeParser { get; }

        public IExtractor CardinalExtractor { get; }

        public IExtractor IntegerExtractor { get; }

        public IParser NumberParser { get; }

        public IDateTimeExtractor DurationExtractor { get; }

        public IDateTimeParser DurationParser { get; }

        public IImmutableDictionary<string, string> UnitMap { get; }

        public Regex NowRegex { get; }

        public Regex AMTimeRegex { get; }

        public Regex PMTimeRegex { get; }

        public Regex SimpleTimeOfTodayAfterRegex { get; }

        public Regex SimpleTimeOfTodayBeforeRegex { get; }

        public Regex SpecificTimeOfDayRegex { get; }

        public Regex TheEndOfRegex { get; }

        public Regex UnitRegex { get; }

        public Regex DateNumberConnectorRegex { get; }

        public Regex PrepositionRegex { get; }

        public Regex ConnectorRegex { get; }

        public IImmutableDictionary<string, int> Numbers { get; }

        public IDateTimeUtilityConfiguration UtilityConfiguration { get; }

        public ThaiDateTimeParserConfiguration(ICommonDateTimeParserConfiguration config) : base(config.Options)
        {

            TokenBeforeDate = DateTimeDefinitions.TokenBeforeDate;
            TokenBeforeTime = DateTimeDefinitions.TokenBeforeTime;

            DateExtractor = config.DateExtractor;
            TimeExtractor = config.TimeExtractor;
            DateParser = config.DateParser;
            TimeParser = config.TimeParser;

            NowRegex = ThaiDateTimeExtractorConfiguration.NowRegex;

            AMTimeRegex = new Regex(DateTimeDefinitions.AMTimeRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            PMTimeRegex = new Regex(DateTimeDefinitions.PMTimeRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            SimpleTimeOfTodayAfterRegex = ThaiDateTimeExtractorConfiguration.SimpleTimeOfTodayAfterRegex;
            SimpleTimeOfTodayBeforeRegex = ThaiDateTimeExtractorConfiguration.SimpleTimeOfTodayBeforeRegex;
            SpecificTimeOfDayRegex = ThaiDateTimeExtractorConfiguration.SpecificTimeOfDayRegex;
            TheEndOfRegex = ThaiDateTimeExtractorConfiguration.TheEndOfRegex;
            UnitRegex = ThaiTimeExtractorConfiguration.TimeUnitRegex;
            DateNumberConnectorRegex = ThaiDateTimeExtractorConfiguration.DateNumberConnectorRegex;

            Numbers = config.Numbers;
            CardinalExtractor = config.CardinalExtractor;
            IntegerExtractor = config.IntegerExtractor;
            NumberParser = config.NumberParser;
            DurationExtractor = config.DurationExtractor;
            DurationParser = config.DurationParser;
            UnitMap = config.UnitMap;
            UtilityConfiguration = config.UtilityConfiguration;
        }

        public int GetHour(string text, int hour)
        {
            var trimedText = text.Trim().ToLowerInvariant();
            int result = hour;
            if (trimedText.Contains("เช้า") && hour >= Constants.HalfDayHourCount)
            {
                result -= Constants.HalfDayHourCount;
            }
            else if (!trimedText.Contains("เช้า") && hour < Constants.HalfDayHourCount)
            {
                result += Constants.HalfDayHourCount;
            }
            return result;
        }

        public bool GetMatchedNowTimex(string text, out string timex)
        {
            var trimedText = text.Trim().ToLowerInvariant();

            // now
            if (trimedText.Contains("ตอนนี้") ||
                trimedText.Contains("เวลานี้") ||
                trimedText.Contains("บัดนี้") ||
                trimedText.Contains("ขณะนี้") ||
                trimedText.Contains("เดี๋ยวนี้") ||
                trimedText.Contains("ปัจจุบัน"))
            {
                timex = "PRESENT_REF";
            }
            // recently & previously
            else if (trimedText.Equals("เมื่อไม่นานมานี้") || 
                trimedText.Equals("ไม่นานมานี้") || 
                trimedText.Equals("เมื่อเร็วๆนี้") || trimedText.Equals("เมื่อเร็วๆ นี้") || 
                trimedText.Equals("เร็วๆนี้") || trimedText.Equals("เร็วๆ นี้") ||

                trimedText.Equals("ก่อนหน้านี้"))
            {
                timex = "PAST_REF";
            }
            else if (trimedText.Equals("ด่วนที่สุดที่เป็นไปได้") || trimedText.Equals("เร็วที่สุดที่เป็นไปได้") ||
                trimedText.Equals("asap"))
            {
                timex = "FUTURE_REF";
            }
            else
            {
                timex = null;
                return false;
            }

            return true;
        }

        public int GetSwiftDay(string text)
        {
            var trimedText = text.Trim().ToLowerInvariant();

            var swift = 0;

            // next
            if (trimedText.EndsWith("หน้า") || trimedText.EndsWith("ถัดไป") || trimedText.EndsWith("ต่อไป"))
            {
                swift = 1;
            }
            // last
            else if (trimedText.Contains("ที่แล้ว") || trimedText.Contains("ที่ผ่านมา") || trimedText.Contains("สุดท้าย"))
            {
                swift = -1;
            }

            return swift;
        }

        public bool ContainsAmbiguousToken(string text, string matchedText) => false;
    }
}
