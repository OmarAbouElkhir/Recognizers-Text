using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Thai;

namespace Microsoft.Recognizers.Text.DateTime.Thai
{
    public class ThaiDatePeriodParserConfiguration : BaseOptionsConfiguration, IDatePeriodParserConfiguration
    {
        public int MinYearNum { get; }

        public int MaxYearNum { get; }

        public string TokenBeforeDate { get; }

        #region internalParsers

        public IDateTimeExtractor DateExtractor { get; }

        public IExtractor CardinalExtractor { get; }

        public IExtractor OrdinalExtractor { get; }

        public IDateTimeExtractor DurationExtractor { get; }

        public IExtractor IntegerExtractor { get; }

        public IParser NumberParser { get; }

        public IDateTimeParser DateParser { get; }

        public IDateTimeParser DurationParser { get; }

        #endregion

        #region Regexes

        public Regex MonthFrontBetweenRegex { get; }
        public Regex BetweenRegex { get; }
        public Regex MonthFrontSimpleCasesRegex { get; }
        public Regex SimpleCasesRegex { get; }
        public Regex OneWordPeriodRegex { get; }
        public Regex MonthWithYear { get; }
        public Regex MonthNumWithYear { get; }
        public Regex YearRegex { get; }
        public Regex PastRegex { get; }
        public Regex FutureRegex { get; }
        public Regex FutureSuffixRegex { get; }
        public Regex NumberCombinedWithUnit { get; }
        public Regex WeekOfMonthRegex { get; }
        public Regex WeekOfYearRegex { get; }
        public Regex QuarterRegex { get; }
        public Regex QuarterRegexYearFront { get; }
        public Regex AllHalfYearRegex { get; }
        public Regex SeasonRegex { get; }
        public Regex WhichWeekRegex { get; }
        public Regex WeekOfRegex { get; }
        public Regex MonthOfRegex { get; }
        public Regex InConnectorRegex { get; }
        public Regex WithinNextPrefixRegex { get; }
        public Regex RestOfDateRegex { get; }
        public Regex LaterEarlyPeriodRegex { get; }
        public Regex WeekWithWeekDayRangeRegex { get; }
        public Regex YearPlusNumberRegex { get; }
        public Regex DecadeWithCenturyRegex { get; }
        public Regex YearPeriodRegex { get; }
        public Regex ComplexDatePeriodRegex { get; }
        public Regex RelativeDecadeRegex { get; }
        public Regex ReferenceDatePeriodRegex { get; }
        public Regex AgoRegex { get; }
        public Regex LaterRegex { get; }
        public Regex LessThanRegex { get; }
        public Regex MoreThanRegex { get; }

        public Regex CenturySuffixRegex { get; }

        public static readonly Regex NextPrefixRegex =
            new Regex(
                DateTimeDefinitions.NextPrefixRegex,
                RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static readonly Regex PastPrefixRegex =
            new Regex(
                DateTimeDefinitions.PastPrefixRegex,
                RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static readonly Regex ThisPrefixRegex =
            new Regex(
                DateTimeDefinitions.ThisPrefixRegex,
                RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public static readonly Regex AfterNextSuffixRegex =
            new Regex(
                DateTimeDefinitions.AfterNextSuffixRegex,
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

        Regex IDatePeriodParserConfiguration.NextPrefixRegex => NextPrefixRegex;
        Regex IDatePeriodParserConfiguration.PastPrefixRegex => PastPrefixRegex;
        Regex IDatePeriodParserConfiguration.ThisPrefixRegex => ThisPrefixRegex;
        
        #endregion

        #region Dictionaries
        public IImmutableDictionary<string, string> UnitMap { get; }

        public IImmutableDictionary<string, int> CardinalMap { get; }

        public IImmutableDictionary<string, int> DayOfMonth { get; }

        public IImmutableDictionary<string, int> MonthOfYear { get; }

        public IImmutableDictionary<string, string> SeasonMap { get; }

        public IImmutableDictionary<string, int> WrittenDecades { get; }

        public IImmutableDictionary<string, int> Numbers { get; }

        public IImmutableDictionary<string, int> SpecialDecadeCases { get; }

        #endregion

        public IImmutableList<string> InStringList { get; }

        public ThaiDatePeriodParserConfiguration(ICommonDateTimeParserConfiguration config) : base(config.Options)
        {
            TokenBeforeDate = DateTimeDefinitions.TokenBeforeDate;
            CardinalExtractor = config.CardinalExtractor;
            OrdinalExtractor = config.OrdinalExtractor;
            IntegerExtractor = config.IntegerExtractor;
            NumberParser = config.NumberParser;
            DateExtractor = config.DateExtractor;
            DurationExtractor = config.DurationExtractor;
            DurationParser = config.DurationParser;
            DateParser = config.DateParser;
            MonthFrontBetweenRegex = ThaiDatePeriodExtractorConfiguration.MonthFrontBetweenRegex;
            BetweenRegex = ThaiDatePeriodExtractorConfiguration.BetweenRegex;
            MonthFrontSimpleCasesRegex = ThaiDatePeriodExtractorConfiguration.MonthFrontSimpleCasesRegex;
            SimpleCasesRegex = ThaiDatePeriodExtractorConfiguration.SimpleCasesRegex;
            OneWordPeriodRegex = ThaiDatePeriodExtractorConfiguration.OneWordPeriodRegex;
            MonthWithYear = ThaiDatePeriodExtractorConfiguration.MonthWithYear;
            MonthNumWithYear = ThaiDatePeriodExtractorConfiguration.MonthNumWithYear;
            YearRegex = ThaiDatePeriodExtractorConfiguration.YearRegex;
            PastRegex = ThaiDatePeriodExtractorConfiguration.PastPrefixRegex;
            FutureRegex = ThaiDatePeriodExtractorConfiguration.NextPrefixRegex;
            FutureSuffixRegex = ThaiDatePeriodExtractorConfiguration.FutureSuffixRegex;
            NumberCombinedWithUnit = ThaiDurationExtractorConfiguration.NumberCombinedWithDurationUnit;
            WeekOfMonthRegex = ThaiDatePeriodExtractorConfiguration.WeekOfMonthRegex;
            WeekOfYearRegex = ThaiDatePeriodExtractorConfiguration.WeekOfYearRegex;
            QuarterRegex = ThaiDatePeriodExtractorConfiguration.QuarterRegex;
            QuarterRegexYearFront = ThaiDatePeriodExtractorConfiguration.QuarterRegexYearFront;
            AllHalfYearRegex = ThaiDatePeriodExtractorConfiguration.AllHalfYearRegex;
            SeasonRegex = ThaiDatePeriodExtractorConfiguration.SeasonRegex;
            WhichWeekRegex = ThaiDatePeriodExtractorConfiguration.WhichWeekRegex;
            WeekOfRegex= ThaiDatePeriodExtractorConfiguration.WeekOfRegex;
            MonthOfRegex = ThaiDatePeriodExtractorConfiguration.MonthOfRegex;
            RestOfDateRegex = ThaiDatePeriodExtractorConfiguration.RestOfDateRegex;
            LaterEarlyPeriodRegex = ThaiDatePeriodExtractorConfiguration.LaterEarlyPeriodRegex;
            WeekWithWeekDayRangeRegex = ThaiDatePeriodExtractorConfiguration.WeekWithWeekDayRangeRegex;
            YearPlusNumberRegex = ThaiDatePeriodExtractorConfiguration.YearPlusNumberRegex;
            DecadeWithCenturyRegex = ThaiDatePeriodExtractorConfiguration.DecadeWithCenturyRegex;
            YearPeriodRegex = ThaiDatePeriodExtractorConfiguration.YearPeriodRegex;
            ComplexDatePeriodRegex = ThaiDatePeriodExtractorConfiguration.ComplexDatePeriodRegex;
            RelativeDecadeRegex = ThaiDatePeriodExtractorConfiguration.RelativeDecadeRegex;
            InConnectorRegex = config.UtilityConfiguration.InConnectorRegex;
            WithinNextPrefixRegex = ThaiDatePeriodExtractorConfiguration.WithinNextPrefixRegex;
            ReferenceDatePeriodRegex = ThaiDatePeriodExtractorConfiguration.ReferenceDatePeriodRegex;
            AgoRegex = ThaiDatePeriodExtractorConfiguration.AgoRegex;
            LaterRegex = ThaiDatePeriodExtractorConfiguration.LaterRegex;
            LessThanRegex = ThaiDatePeriodExtractorConfiguration.LessThanRegex;
            MoreThanRegex = ThaiDatePeriodExtractorConfiguration.MoreThanRegex;
            CenturySuffixRegex = ThaiDatePeriodExtractorConfiguration.CenturySuffixRegex;
            UnitMap = config.UnitMap;
            CardinalMap = config.CardinalMap;
            DayOfMonth = config.DayOfMonth;
            MonthOfYear = config.MonthOfYear;
            SeasonMap = config.SeasonMap;
            WrittenDecades = config.WrittenDecades;
            Numbers = config.Numbers;
            SpecialDecadeCases = config.SpecialDecadeCases;
        }

        public int GetSwiftDayOrMonth(string text)
        {
            var trimedText = text.Trim().ToLowerInvariant();
            var swift = 0;
            if (AfterNextSuffixRegex.IsMatch(trimedText))
            {
                swift = 2;
            }
            else if (NextPrefixRegex.IsMatch(trimedText))
            {
                swift = 1;
            }
            else if (PastPrefixRegex.IsMatch(trimedText))
            {
                swift = -1;
            }
            return swift;
        }

        public int GetSwiftYear(string text)
        {
            var trimedText = text.Trim().ToLowerInvariant();
            var swift = -10;
            if (AfterNextSuffixRegex.IsMatch(trimedText))
            {
                swift = 2;
            }
            else if (NextPrefixRegex.IsMatch(trimedText))
            {
                swift = 1;
            }
            else if (PastPrefixRegex.IsMatch(trimedText))
            {
                swift = -1;
            }
            else if (ThisPrefixRegex.IsMatch(trimedText))
            {
                swift = 0;
            }
            return swift;
        }

        public bool IsFuture(string text)
        {
            var trimedText = text.Trim().ToLowerInvariant();

            // this - next
            return (trimedText.EndsWith("นี้") || trimedText.EndsWith("หน้า") || trimedText.EndsWith("ถัดไป") || trimedText.EndsWith("ต่อไป"));
        }

        public bool IsLastCardinal(string text)
        {
            var trimedText = text.Trim().ToLowerInvariant();

            // (last) week, and (last) week of the month
            return trimedText.Equals("ที่แล้ว") || trimedText.Equals("ที่ผ่านมา") || trimedText.Equals("สุดท้าย");
        }

        public bool IsMonthOnly(string text)
        {
            var trimedText = text.Trim().ToLowerInvariant();
            return trimedText.Contains("เดือน") || trimedText.Contains(" เดือน ") && AfterNextSuffixRegex.IsMatch(trimedText);
        }

        public bool IsMonthToDate(string text)
        {
            var trimedText = text.Trim();
            return trimedText.Equals("MTD");
        }

        public bool IsWeekend(string text)
        {
            var trimedText = text.Trim().ToLowerInvariant();
            return trimedText.Contains("สุดสัปดาห์") || trimedText.Contains(" สุดสัปดาห์ ") && AfterNextSuffixRegex.IsMatch(trimedText);
        }

        public bool IsWeekOnly(string text)
        {
            var trimedText = text.Trim().ToLowerInvariant();
            return (trimedText.Contains("สัปดาห์") || trimedText.Contains(" สัปดาห์ ") && AfterNextSuffixRegex.IsMatch(trimedText)) || 
                (trimedText.Contains("อาทิตย์") || trimedText.Contains(" อาทิตย์ ") && AfterNextSuffixRegex.IsMatch(trimedText));
        }

        public bool IsYearOnly(string text)
        {
            var trimedText = text.Trim().ToLowerInvariant();
            return trimedText.Contains("ปี") || trimedText.Contains(" ปี ") && AfterNextSuffixRegex.IsMatch(trimedText);
        }

        public bool IsYearToDate(string text)
        {
            var trimedText = text.Trim();
            return trimedText.Equals("YTD");
        }

    }
}
