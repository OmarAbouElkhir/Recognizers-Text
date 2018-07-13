using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Text.Number;

namespace Microsoft.Recognizers.Text.DateTime.Thai
{
    public class ThaiDurationParserConfiguration : BaseOptionsConfiguration, IDurationParserConfiguration
    {
        public IExtractor CardinalExtractor { get; }

        public IExtractor DurationExtractor { get; }

        public IParser NumberParser { get; }

        public Regex NumberCombinedWithUnit { get; }

        public Regex AnUnitRegex { get; }

        public Regex DuringRegex { get; }

        public Regex AllDateUnitRegex { get; }

        public Regex HalfDateUnitRegex { get; }

        public Regex SuffixAndRegex { get; }

        public Regex FollowedUnit { get; }

        public Regex ConjunctionRegex { get; }

        public Regex InexactNumberRegex { get; }

        public Regex InexactNumberUnitRegex { get; }

        public Regex DurationUnitRegex { get; }

        public IImmutableDictionary<string, string> UnitMap { get; }

        public IImmutableDictionary<string, long> UnitValueMap { get; }

        public IImmutableDictionary<string, double> DoubleNumbers { get; }

        public ThaiDurationParserConfiguration(ICommonDateTimeParserConfiguration config) : base(config.Options)
        {
            CardinalExtractor = config.CardinalExtractor;
            NumberParser = config.NumberParser;
            DurationExtractor = new BaseDurationExtractor(new ThaiDurationExtractorConfiguration(), false);
            NumberCombinedWithUnit = ThaiDurationExtractorConfiguration.NumberCombinedWithDurationUnit;
            AnUnitRegex = ThaiDurationExtractorConfiguration.AnUnitRegex;
            DuringRegex = ThaiDurationExtractorConfiguration.DuringRegex;
            AllDateUnitRegex = ThaiDurationExtractorConfiguration.AllRegex;
            HalfDateUnitRegex = ThaiDurationExtractorConfiguration.HalfRegex;
            SuffixAndRegex = ThaiDurationExtractorConfiguration.SuffixAndRegex;
            FollowedUnit = ThaiDurationExtractorConfiguration.DurationFollowedUnit;
            ConjunctionRegex = ThaiDurationExtractorConfiguration.ConjunctionRegex;
            InexactNumberRegex = ThaiDurationExtractorConfiguration.InexactNumberRegex;
            InexactNumberUnitRegex = ThaiDurationExtractorConfiguration.InexactNumberUnitRegex;
            DurationUnitRegex = ThaiDurationExtractorConfiguration.DurationUnitRegex;
            UnitMap = config.UnitMap;
            UnitValueMap = config.UnitValueMap;
            DoubleNumbers = config.DoubleNumbers;
        }
    }
}
