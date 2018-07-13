using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Text.DateTime.Utilities;

namespace Microsoft.Recognizers.Text.DateTime.Thai
{
    public class ThaiTimePeriodParserConfiguration : BaseOptionsConfiguration, ITimePeriodParserConfiguration
    {
        public IDateTimeExtractor TimeExtractor { get; }

        public IDateTimeParser TimeParser { get; }

        public IExtractor IntegerExtractor { get; }

        public Regex SpecificTimeFromToRegex { get; }

        public Regex SpecificTimeBetweenAndRegex { get; }

        public Regex PureNumberFromToRegex { get; }

        public Regex PureNumberBetweenAndRegex { get; }

        public Regex TimeOfDayRegex { get; }

        public Regex GeneralEndingRegex { get; }

        public Regex TillRegex { get; }

        public IImmutableDictionary<string, int> Numbers { get; }

        public IDateTimeUtilityConfiguration UtilityConfiguration { get; }

        public ThaiTimePeriodParserConfiguration(ICommonDateTimeParserConfiguration config) : base(config.Options)
        {
            TimeExtractor = config.TimeExtractor;
            IntegerExtractor = config.IntegerExtractor;
            TimeParser = config.TimeParser;
            PureNumberFromToRegex = ThaiTimePeriodExtractorConfiguration.PureNumFromTo;
            PureNumberBetweenAndRegex = ThaiTimePeriodExtractorConfiguration.PureNumBetweenAnd;
            SpecificTimeFromToRegex = ThaiTimePeriodExtractorConfiguration.SpecificTimeFromTo;
            SpecificTimeBetweenAndRegex = ThaiTimePeriodExtractorConfiguration.SpecificTimeBetweenAnd;
            TimeOfDayRegex = ThaiTimePeriodExtractorConfiguration.TimeOfDayRegex;
            GeneralEndingRegex = ThaiTimePeriodExtractorConfiguration.GeneralEndingRegex;
            TillRegex = ThaiTimePeriodExtractorConfiguration.TillRegex;
            Numbers = config.Numbers;
            UtilityConfiguration = config.UtilityConfiguration;
        }

        public bool GetMatchedTimexRange(string text, out string timex, out int beginHour, out int endHour, out int endMin)
        {
            var trimedText = text.Trim().ToLowerInvariant();
            if (trimedText.EndsWith("s"))
            {
                trimedText = trimedText.Substring(0, trimedText.Length - 1);
            }

            beginHour = 0;
            endHour = 0;
            endMin = 0;

            //morning
            if (trimedText.Contains("เช้า"))
            {
                timex = "TMO";
                beginHour = 8;
                endHour = Constants.HalfDayHourCount;
            }
            //afternoon
            else if (trimedText.Contains("บ่าย"))
            {
                timex = "TAF";
                beginHour = Constants.HalfDayHourCount;
                endHour = 16;
            }
            //evening
            else if (trimedText.Contains("เย็น"))
            {
                timex = "TEV";
                beginHour = 16;
                endHour = 20;
            }
            //daytime
            else if (trimedText.Contains("ระหว่างวัน"))
            {
                timex = "TDT";
                beginHour = 8;
                endHour = 18;
            }
            //night
            else if (trimedText.Contains("กลางคืน") || trimedText.Contains("คืน") || trimedText.Contains("ค่ำ"))
            {
                timex = "TNI";
                beginHour = 20;
                endHour = 23;
                endMin = 59;
            }
            else
            {
                timex = null;
                return false;
            }

            return true;
        }
    }
}
