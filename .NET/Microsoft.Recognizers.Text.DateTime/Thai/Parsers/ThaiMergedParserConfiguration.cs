using System.Text.RegularExpressions;

using Microsoft.Recognizers.Text.Matcher;

namespace Microsoft.Recognizers.Text.DateTime.Thai
{
    public sealed class ThaiMergedParserConfiguration : ThaiCommonDateTimeParserConfiguration, IMergedParserConfiguration
    {
        public Regex BeforeRegex { get; }

        public Regex AfterRegex { get; }

        public Regex SinceRegex { get; }

        public Regex YearAfterRegex { get; }

        public Regex YearRegex { get; }

        public IDateTimeParser GetParser { get; }

        public IDateTimeParser HolidayParser { get; }

        public IDateTimeParser TimeZoneParser { get; }

        public StringMatcher SuperfluousWordMatcher { get; }

        public ThaiMergedParserConfiguration(DateTimeOptions options) : base(options)
        {
            BeforeRegex = ThaiMergedExtractorConfiguration.BeforeRegex;
            AfterRegex = ThaiMergedExtractorConfiguration.AfterRegex;
            SinceRegex = ThaiMergedExtractorConfiguration.SinceRegex;
            YearAfterRegex = ThaiMergedExtractorConfiguration.YearAfterRegex;
            YearRegex = ThaiDatePeriodExtractorConfiguration.YearRegex;
            SuperfluousWordMatcher = null;

            DatePeriodParser = new BaseDatePeriodParser(new ThaiDatePeriodParserConfiguration(this));
            TimePeriodParser = new BaseTimePeriodParser(new ThaiTimePeriodParserConfiguration(this));
            DateTimePeriodParser = new BaseDateTimePeriodParser(new ThaiDateTimePeriodParserConfiguration(this));
            GetParser = null;
            HolidayParser = null;
            TimeZoneParser = new BaseTimeZoneParser();
        }
    }
}
