﻿using System.Collections.Immutable;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions.Thai;
using Microsoft.Recognizers.Text.Number.Thai;

namespace Microsoft.Recognizers.Text.NumberWithUnit.Thai
{
    public abstract class ThaiNumberWithUnitExtractorConfiguration : INumberWithUnitExtractorConfiguration
    {
        protected ThaiNumberWithUnitExtractorConfiguration(CultureInfo ci)
        {
            this.CultureInfo = ci;
            this.UnitNumExtractor = NumberExtractor.GetInstance();
            this.BuildPrefix = NumbersWithUnitDefinitions.BuildPrefix;
            this.BuildSuffix = NumbersWithUnitDefinitions.BuildSuffix;
            this.ConnectorToken = string.Empty;
            this.CompoundUnitConnectorRegex = new Regex(NumbersWithUnitDefinitions.CompoundUnitConnectorRegex, RegexOptions.IgnoreCase);
        }

        public abstract string ExtractType { get; }

        public CultureInfo CultureInfo { get; }

        public IExtractor UnitNumExtractor { get; }

        public string BuildPrefix { get; }

        public string BuildSuffix { get; }

        public string ConnectorToken { get; }

        public Regex CompoundUnitConnectorRegex { get; set; }

        public abstract ImmutableDictionary<string, string> SuffixList { get; }

        public abstract ImmutableDictionary<string, string> PrefixList { get; }

        public abstract ImmutableList<string> AmbiguousUnitList { get; }
    }
}