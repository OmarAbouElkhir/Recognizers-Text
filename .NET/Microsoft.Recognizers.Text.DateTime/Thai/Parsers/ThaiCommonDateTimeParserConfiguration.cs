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
            TimeExtractor = null;
            DateTimeExtractor = null;
            DurationExtractor = null;
            DatePeriodExtractor = null;
            TimePeriodExtractor = null;
            DateTimePeriodExtractor = null;
            DurationParser = null;
            DateParser = new BaseDateParser(new ThaiDateParserConfiguration(this));
            TimeParser = null;
            DateTimeParser = null;
            DatePeriodParser = null;
            TimePeriodParser = null;
            DateTimePeriodParser = null;
            DateTimeAltParser = null;
        }

        Regex ICommonDateTimeParserConfiguration.AmbiguousMonthP0Regex => AmbiguousMonthP0Regex;

        public override IImmutableDictionary<string, int> DayOfMonth => BaseDateTime.DayOfMonthDictionary.ToImmutableDictionary().AddRange(DateTimeDefinitions.DayOfMonth);
    }
}