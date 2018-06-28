using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions;

namespace Microsoft.Recognizers.Text.Number
{
    public abstract class BaseNumberExtractor : IExtractor
    {
        internal abstract ImmutableDictionary<Regex, string> Regexes { get; }

        protected virtual string ExtractType { get; } = "";

        protected virtual NumberOptions Options { get; } = NumberOptions.None;

        protected virtual Regex NegativeNumberTermsRegex { get; } = null;

        public virtual List<ExtractResult> Extract(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return new List<ExtractResult>();
            }

            var result = new List<ExtractResult>();
            var matchSource = new Dictionary<Match, string>();
            var matched = new bool[source.Length];

            var collections = Regexes.ToDictionary(o => o.Key.Matches(source), p => p.Value);
            foreach (var collection in collections)
            {
                foreach (Match m in collection.Key)
                {
                    matchSource.Add(m, collection.Value);
                }
            }

            matchSource = RemoveRedundantMatches(matchSource);
            matchSource = RankMatches(matchSource);

            foreach (var match in matchSource)
            {
                for (var i = 0; i < match.Key.Length; i++)
                {
                    matched[match.Key.Index + i] = true;
                }
            }

            var last = -1;
            for (var i = 0; i < source.Length; i++)
            {
                if (matched[i])
                {
                    if (i + 1 == source.Length || !matched[i + 1])
                    {
                        var start = last + 1;
                        var length = i - last;
                        var substr = source.Substring(start, length);

                        if (matchSource.Keys.Any(o => o.Index == start && o.Length == length))
                        {
                            var srcMatch = matchSource.Keys.First(o => o.Index == start && o.Length == length);

                            // Extract negative numbers
                            if (NegativeNumberTermsRegex != null) {
                                var match = NegativeNumberTermsRegex.Match(source.Substring(0, start));
                                if (match.Success)
                                {
                                    start = match.Index;
                                    length = length + match.Length;
                                    substr = match.Value + substr;
                                }
                            }

                            var er = new ExtractResult
                            {
                                Start = start,
                                Length = length,
                                Text = substr,
                                Type = ExtractType,
                                Data = matchSource.ContainsKey(srcMatch) ? matchSource[srcMatch] : null
                            };
                            result.Add(er);
                        }
                    }
                }
                else
                {
                    last = i;
                }
            }

            return result;
        }

        protected Regex GenerateLongFormatNumberRegexes(LongFormatType type, string placeholder = BaseNumbers.PlaceHolderDefault)
        {
            var thousandsMark = Regex.Escape(type.ThousandsMark.ToString());
            var decimalsMark = Regex.Escape(type.DecimalsMark.ToString());

            var regexDefinition = type.DecimalsMark.Equals('\0') ?
                BaseNumbers.IntegerRegexDefinition(placeholder, thousandsMark) :
                BaseNumbers.DoubleRegexDefinition(placeholder, thousandsMark, decimalsMark);

            return new Regex(regexDefinition, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        private Dictionary<Match, string> RemoveRedundantMatches(Dictionary<Match, string> matches)
        {
            var result = new Dictionary<Match, string>();
            var toBeRemoved = new Dictionary<Match, string>();

            var orderedMatches = matches.OrderBy(x => x.Key.Index).ToDictionary(x => x.Key, y => y.Value);

            for (var i = 0; i < orderedMatches.Count; i++)
            {
                for (var j = i + 1; j < orderedMatches.Count; j++)
                {
                    var start1 = orderedMatches.ElementAt(i).Key.Index;
                    var end1 = start1 + orderedMatches.ElementAt(i).Key.Length;

                    var start2 = orderedMatches.ElementAt(j).Key.Index;
                    var end2 = start2 + orderedMatches.ElementAt(j).Key.Length;

                    if ((start1 < start2 && end1 < end2 && end1 > start2) 
                        || (start1 == start2 && end1 == end2) 
                        || (start1 < start2 && end1 > end2)
                        || (start1 == start2 && end2 < end1)
                        || (start1 < start2 && end1 == end2))
                    {
                        if (!toBeRemoved.ContainsKey(orderedMatches.ElementAt(j).Key))
                        {
                            toBeRemoved.Add(orderedMatches.ElementAt(j).Key, orderedMatches.ElementAt(j).Value);
                        }
                    }
                }
            }

            foreach (var match in orderedMatches)
            {
                if (!toBeRemoved.ContainsKey(match.Key))
                {
                    result.Add(match.Key, match.Value);
                }
            }

            return result;
        }

        private Dictionary<Match, string> RankMatches(Dictionary<Match, string> matches)
        {
            var result = new Dictionary<Match, string>();
            var staticRanks = new List<string> { "Integer" };

            foreach (var rank in staticRanks)
            {
                foreach (var match in matches)
                {
                    if (match.Value.Contains(rank))
                    {
                        result.Add(match.Key, match.Value);
                    }
                }
            }

            foreach (var match in matches)
            {
                if (!result.ContainsKey(match.Key))
                {
                    result.Add(match.Key, match.Value);
                }
            }

            return result;
        }
    }

    public enum NumberMode
    {
        //Default is for unit and datetime
        Default,
        //Add 67.5 billion & million support.
        Currency,
        //Don't extract number from cases like 16ml
        PureNumber
    }

    public class LongFormatType
    {
        // Reference Value : 1234567.89

        // 1,234,567
        public static LongFormatType IntegerNumComma = new LongFormatType(',', '\0');

        // 1.234.567
        public static LongFormatType IntegerNumDot = new LongFormatType('.', '\0');

        // 1 234 567
        public static LongFormatType IntegerNumBlank = new LongFormatType(' ', '\0');

        // 1 234 567
        public static LongFormatType IntegerNumNoBreakSpace = new LongFormatType(Constants.NO_BREAK_SPACE, '\0');

        // 1'234'567
        public static LongFormatType IntegerNumQuote = new LongFormatType('\'', '\0');

        // 1,234,567.89
        public static LongFormatType DoubleNumCommaDot = new LongFormatType(',', '.');

        // 1,234,567·89
        public static LongFormatType DoubleNumCommaCdot = new LongFormatType(',', '·');

        // 1 234 567,89
        public static LongFormatType DoubleNumBlankComma = new LongFormatType(' ', ',');

        // 1 234 567,89
        public static LongFormatType DoubleNumNoBreakSpaceComma = new LongFormatType(Constants.NO_BREAK_SPACE, ',');

        // 1 234 567.89
        public static LongFormatType DoubleNumBlankDot = new LongFormatType(' ', '.');

        // 1 234 567.89
        public static LongFormatType DoubleNumNoBreakSpaceDot = new LongFormatType(Constants.NO_BREAK_SPACE, '.');

        // 1.234.567,89
        public static LongFormatType DoubleNumDotComma = new LongFormatType('.', ',');

        // 1'234'567,89
        public static LongFormatType DoubleNumQuoteComma = new LongFormatType('\'', ',');
        
        private LongFormatType(char thousandsMark, char decimalsMark)
        {
            ThousandsMark = thousandsMark;
            DecimalsMark = decimalsMark;
        }

        public char DecimalsMark { get; }

        public char ThousandsMark { get; }
    }
}