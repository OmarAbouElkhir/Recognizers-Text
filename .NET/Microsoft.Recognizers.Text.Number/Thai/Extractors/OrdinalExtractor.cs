using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Thai;

namespace Microsoft.Recognizers.Text.Number.Thai
{
    public class OrdinalExtractor : BaseNumberExtractor
    {
        internal sealed override ImmutableDictionary<Regex, string> Regexes { get; }

        protected sealed override string ExtractType { get; } = Constants.SYS_NUM_ORDINAL; // "Ordinal";

        private static readonly ConcurrentDictionary<string, OrdinalExtractor> Instances = new ConcurrentDictionary<string, OrdinalExtractor>();

        public static OrdinalExtractor GetInstance(string placeholder = "")
        {

            if (!Instances.ContainsKey(placeholder))
            {
                var instance = new OrdinalExtractor();
                Instances.TryAdd(placeholder, instance);
            }

            return Instances[placeholder];
        }

        private OrdinalExtractor()
        {
            var regexes = new Dictionary<Regex, string>
            {
                {
                    new Regex(NumbersDefinitions.OrdinalSuffixRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline),
                    "OrdinalNum"
                },
                {
                    new Regex(NumbersDefinitions.OrdinalNumericRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline),
                    "OrdinalNum"
                },
                {
                    new Regex(NumbersDefinitions.AllOrdinalRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline),
                    "OrdThai"
                },
                {
                    new Regex(NumbersDefinitions.RoundNumberOrdinalRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline),
                    "OrdThai"
                }
            };

            Regexes = regexes.ToImmutableDictionary();
        }
    }
}