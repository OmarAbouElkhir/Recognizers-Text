using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Text.DateTime.Thai.Utilities;
using Microsoft.Recognizers.Text.DateTime.Utilities;
using Microsoft.Recognizers.Definitions.Thai;

namespace Microsoft.Recognizers.Text.DateTime.Thai
{
    public class ThaiTimePeriodExtractorConfiguration : BaseOptionsConfiguration, ITimePeriodExtractorConfiguration
    {
        public string TokenBeforeDate { get; }

        public static readonly Regex TillRegex = 
            new Regex(DateTimeDefinitions.TillRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex HourRegex =
            new Regex(DateTimeDefinitions.HourRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex PeriodHourNumRegex =
            new Regex(DateTimeDefinitions.PeriodHourNumRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex PeriodDescRegex = 
            new Regex(DateTimeDefinitions.DescRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex PmRegex =
            new Regex(DateTimeDefinitions.PmRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex AmRegex = 
            new Regex(DateTimeDefinitions.AmRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex PureNumFromTo =
            new Regex(DateTimeDefinitions.PureNumFromTo, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex PureNumBetweenAnd =
            new Regex(DateTimeDefinitions.PureNumBetweenAnd, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex SpecificTimeFromTo = new Regex(DateTimeDefinitions.SpecificTimeFromTo, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex SpecificTimeBetweenAnd = new Regex(DateTimeDefinitions.SpecificTimeBetweenAnd, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex PrepositionRegex = 
            new Regex(DateTimeDefinitions.PrepositionRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex TimeOfDayRegex =
            new Regex(DateTimeDefinitions.TimeOfDayRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex SpecificTimeOfDayRegex =
            new Regex(DateTimeDefinitions.SpecificTimeOfDayRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex TimeUnitRegex =
            new Regex(DateTimeDefinitions.TimeUnitRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex TimeFollowedUnit = 
            new Regex(DateTimeDefinitions.TimeFollowedUnit, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex TimeNumberCombinedWithUnit =
            new Regex(DateTimeDefinitions.TimeNumberCombinedWithUnit, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public static readonly Regex GeneralEndingRegex =
            new Regex(DateTimeDefinitions.GeneralEndingRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public ThaiTimePeriodExtractorConfiguration(DateTimeOptions options = DateTimeOptions.None) : base(options)
        {
            TokenBeforeDate = DateTimeDefinitions.TokenBeforeDate;
            SingleTimeExtractor = new BaseTimeExtractor(new ThaiTimeExtractorConfiguration(options));
            UtilityConfiguration = new ThaiDatetimeUtilityConfiguration();
            IntegerExtractor = Number.Thai.IntegerExtractor.GetInstance();
        }

        public IDateTimeUtilityConfiguration UtilityConfiguration { get; }

        public IDateTimeExtractor SingleTimeExtractor { get; }

        public IExtractor IntegerExtractor { get; }

        public IEnumerable<Regex> SimpleCasesRegex => new[] { PureNumFromTo, PureNumBetweenAnd, SpecificTimeFromTo, SpecificTimeBetweenAnd };

        Regex ITimePeriodExtractorConfiguration.TillRegex => TillRegex;

        Regex ITimePeriodExtractorConfiguration.TimeOfDayRegex => TimeOfDayRegex;

        Regex ITimePeriodExtractorConfiguration.GeneralEndingRegex => GeneralEndingRegex;

        public bool GetFromTokenIndex(string text, out int index)
        {
            index = -1;
            if (text.Contains("จาก") || text.Contains("ตั้งแต่"))
            {
                index = text.LastIndexOf("จาก", StringComparison.Ordinal);

                if (index == -1)
                {
                    index = text.LastIndexOf("ตั้งแต่", StringComparison.Ordinal);
                }

                return true;
            }

            return false;
        }

        public bool GetBetweenTokenIndex(string text, out int index)
        {
            index = -1;
            if (text.Contains("ระหว่าง") || text.Contains("ช่วง") || text.Contains("ระหว่างช่วง") || text.Contains("ระหว่าง ช่วง"))
            {
                index = text.LastIndexOf("ระหว่าง", StringComparison.Ordinal);

                if (index == -1)
                {
                    index = text.LastIndexOf("ช่วง", StringComparison.Ordinal);
                }

                if (index == -1)
                {
                    index = text.LastIndexOf("ระหว่างช่วง", StringComparison.Ordinal);
                }

                if (index == -1)
                {
                    index = text.LastIndexOf("ระหว่าง ช่วง", StringComparison.Ordinal);
                }

                return true;
            }

            return false;
        }

        public bool HasConnectorToken(string text)
        {
            // and
            return text.Equals("กับ") || text.Equals("และ");
        }
    }
}