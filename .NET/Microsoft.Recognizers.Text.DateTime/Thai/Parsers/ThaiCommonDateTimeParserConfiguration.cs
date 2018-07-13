using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Text.DateTime.Thai.Utilities;
using Microsoft.Recognizers.Definitions.Thai;
using Microsoft.Recognizers.Definitions;
using Microsoft.Recognizers.Text.Number.Thai;
using Microsoft.Recognizers.Text.Number;

namespace Microsoft.Recognizers.Text.DateTime.Thai
{
    public class ThaiCommonDateTimeParserConfiguration : BaseDateParserConfiguration, ICommonDateTimeParserConfiguration
    {

        public new static readonly Regex AmbiguousMonthP0Regex =
            new Regex(DateTimeDefinitions.AmbiguousMonthP0Regex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public ThaiCommonDateTimeParserConfiguration(DateTimeOptions options) : base(options)
        {
            UtilityConfiguration = new ThaiDatetimeUtilityConfiguration();

            UnitMap = DateTimeDefinitions.UnitMap.ToImmutableDictionary();
            UnitValueMap = DateTimeDefinitions.UnitValueMap.ToImmutableDictionary();
            SeasonMap = DateTimeDefinitions.SeasonMap.ToImmutableDictionary();
            CardinalMap = DateTimeDefinitions.CardinalMap.ToImmutableDictionary();
            DayOfWeek = DateTimeDefinitions.DayOfWeek.ToImmutableDictionary();
            MonthOfYear = DateTimeDefinitions.MonthOfYear.ToImmutableDictionary();
            Numbers = DateTimeDefinitions.Numbers.ToImmutableDictionary();
            DoubleNumbers = DateTimeDefinitions.DoubleNumbers.ToImmutableDictionary();
            WrittenDecades = DateTimeDefinitions.WrittenDecades.ToImmutableDictionary();
            SpecialDecadeCases = DateTimeDefinitions.SpecialDecadeCases.ToImmutableDictionary();

            CardinalExtractor = Number.Thai.CardinalExtractor.GetInstance();
            IntegerExtractor = Number.Thai.IntegerExtractor.GetInstance();
            OrdinalExtractor = Number.Thai.OrdinalExtractor.GetInstance();

            NumberParser = new BaseNumberParser(new ThaiNumberParserConfiguration());
            DateExtractor = new BaseDateExtractor(new ThaiDateExtractorConfiguration());
            TimeExtractor = new BaseTimeExtractor(new ThaiTimeExtractorConfiguration(options));
            DateTimeExtractor = new BaseDateTimeExtractor(new ThaiDateTimeExtractorConfiguration(options));
            DurationExtractor = new BaseDurationExtractor(new ThaiDurationExtractorConfiguration(options));
            DatePeriodExtractor = new BaseDatePeriodExtractor(new ThaiDatePeriodExtractorConfiguration());
            TimePeriodExtractor = new BaseTimePeriodExtractor(new ThaiTimePeriodExtractorConfiguration(options));
            DateTimePeriodExtractor = new BaseDateTimePeriodExtractor(new ThaiDateTimePeriodExtractorConfiguration(options));
            DurationParser = new BaseDurationParser(new ThaiDurationParserConfiguration(this));
            DateParser = new BaseDateParser(new ThaiDateParserConfiguration(this));
            TimeParser = new TimeParser(new ThaiTimeParserConfiguration(this));
            DateTimeParser = new BaseDateTimeParser(new ThaiDateTimeParserConfiguration(this));
            DatePeriodParser = new BaseDatePeriodParser(new ThaiDatePeriodParserConfiguration(this));
            TimePeriodParser = new BaseTimePeriodParser(new ThaiTimePeriodParserConfiguration(this));
            DateTimePeriodParser = new BaseDateTimePeriodParser(new ThaiDateTimePeriodParserConfiguration(this));
            DateTimeAltParser = null;
        }

        Regex ICommonDateTimeParserConfiguration.AmbiguousMonthP0Regex => AmbiguousMonthP0Regex;

        public override IImmutableDictionary<string, int> DayOfMonth => BaseDateTime.DayOfMonthDictionary.ToImmutableDictionary().AddRange(DateTimeDefinitions.DayOfMonth);
    }
}