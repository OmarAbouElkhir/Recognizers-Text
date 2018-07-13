using System.Text.RegularExpressions;

using Microsoft.Recognizers.Text.DateTime.Thai.Utilities;
using Microsoft.Recognizers.Text.DateTime.Utilities;
using Microsoft.Recognizers.Definitions.Thai;

namespace Microsoft.Recognizers.Text.DateTime.Thai
{
    public class ThaiDateTimeExtractorConfiguration : BaseOptionsConfiguration, IDateTimeExtractorConfiguration
    {
        public static readonly Regex PrepositionRegex = new Regex(DateTimeDefinitions.PrepositionRegex,
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex NowRegex =
            new Regex(DateTimeDefinitions.NowRegex,
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex SuffixRegex = new Regex(DateTimeDefinitions.SuffixRegex,
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex TimeOfDayRegex = new Regex(DateTimeDefinitions.TimeOfDayRegex,
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex SpecificTimeOfDayRegex =
            new Regex(DateTimeDefinitions.SpecificTimeOfDayRegex,
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex TimeOfTodayAfterRegex =
             new Regex(DateTimeDefinitions.TimeOfTodayAfterRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex TimeOfTodayBeforeRegex =
            new Regex(DateTimeDefinitions.TimeOfTodayBeforeRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex SimpleTimeOfTodayAfterRegex =
            new Regex(DateTimeDefinitions.SimpleTimeOfTodayAfterRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex SimpleTimeOfTodayBeforeRegex =
            new Regex(DateTimeDefinitions.SimpleTimeOfTodayBeforeRegex,
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex TheEndOfRegex = new Regex(DateTimeDefinitions.TheEndOfRegex,
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex UnitRegex = new Regex(DateTimeDefinitions.TimeUnitRegex,
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex ConnectorRegex = new Regex(DateTimeDefinitions.ConnectorRegex,
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex NumberAsTimeRegex = new Regex(DateTimeDefinitions.NumberAsTimeRegex,
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex DateNumberConnectorRegex = new Regex(DateTimeDefinitions.DateNumberConnectorRegex,
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public ThaiDateTimeExtractorConfiguration(DateTimeOptions options = DateTimeOptions.None) : base(options)
        {
            IntegerExtractor = Number.Thai.IntegerExtractor.GetInstance();
            DatePointExtractor = new BaseDateExtractor(new ThaiDateExtractorConfiguration());
            TimePointExtractor = new BaseTimeExtractor(new ThaiTimeExtractorConfiguration(options));
            DurationExtractor = new BaseDurationExtractor(new ThaiDurationExtractorConfiguration(options));
            UtilityConfiguration = new ThaiDatetimeUtilityConfiguration();
        }

        public IExtractor IntegerExtractor { get; }

        public IDateTimeExtractor DatePointExtractor { get; }

        public IDateTimeExtractor TimePointExtractor { get; }

        public IDateTimeUtilityConfiguration UtilityConfiguration { get; }

        Regex IDateTimeExtractorConfiguration.NowRegex => NowRegex;

        Regex IDateTimeExtractorConfiguration.SuffixRegex => SuffixRegex;

        Regex IDateTimeExtractorConfiguration.TimeOfTodayAfterRegex => TimeOfTodayAfterRegex;

        Regex IDateTimeExtractorConfiguration.SimpleTimeOfTodayAfterRegex => SimpleTimeOfTodayAfterRegex;

        Regex IDateTimeExtractorConfiguration.TimeOfTodayBeforeRegex => TimeOfTodayBeforeRegex;

        Regex IDateTimeExtractorConfiguration.SimpleTimeOfTodayBeforeRegex => SimpleTimeOfTodayBeforeRegex;

        Regex IDateTimeExtractorConfiguration.TimeOfDayRegex => TimeOfDayRegex;

        Regex IDateTimeExtractorConfiguration.TheEndOfRegex => TheEndOfRegex;

        Regex IDateTimeExtractorConfiguration.UnitRegex => UnitRegex;

        Regex IDateTimeExtractorConfiguration.NumberAsTimeRegex => NumberAsTimeRegex;

        Regex IDateTimeExtractorConfiguration.DateNumberConnectorRegex => DateNumberConnectorRegex;

        public IDateTimeExtractor DurationExtractor { get; }

        public bool IsConnector(string text)
        {
            text = text.Trim();
            return (string.IsNullOrEmpty(text) 
                    || PrepositionRegex.IsMatch(text)
                    || ConnectorRegex.IsMatch(text));
        }

    }
}