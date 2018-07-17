﻿using System.Collections.Immutable;
using System.Globalization;

using Microsoft.Recognizers.Definitions.Thai;

namespace Microsoft.Recognizers.Text.NumberWithUnit.Thai
{
    public class CurrencyExtractorConfiguration : ThaiNumberWithUnitExtractorConfiguration
    {
        public CurrencyExtractorConfiguration() : this(new CultureInfo(Culture.Thai)) { }

        public CurrencyExtractorConfiguration(CultureInfo ci) : base(ci) { }

        public override ImmutableDictionary<string, string> SuffixList => CurrencySuffixList;

        public override ImmutableDictionary<string, string> PrefixList => CurrencyPrefixList;

        public override ImmutableList<string> AmbiguousUnitList => AmbiguousValues;

        public override string ExtractType => Constants.SYS_UNIT_CURRENCY;

        public static readonly ImmutableDictionary<string, string> CurrencySuffixList = NumbersWithUnitDefinitions.CurrencySuffixList.ToImmutableDictionary();

        public static readonly ImmutableDictionary<string, string> CurrencyPrefixList = NumbersWithUnitDefinitions.CurrencyPrefixList.ToImmutableDictionary();

        public static readonly ImmutableDictionary<string, string> FractionalUnitNameToCodeMap = NumbersWithUnitDefinitions.FractionalUnitNameToCodeMap.ToImmutableDictionary();

        private static readonly ImmutableList<string> AmbiguousValues = NumbersWithUnitDefinitions.AmbiguousCurrencyUnitList.ToImmutableList();
    }
}