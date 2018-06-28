using System.Collections.Immutable;
using System.Globalization;
using Microsoft.Recognizers.Definitions.Thai;

namespace Microsoft.Recognizers.Text.NumberWithUnit.Thai
{
    public class CurrencyParserConfiguration : ThaiNumberWithUnitParserConfiguration
    {
        public CurrencyParserConfiguration() : this(new CultureInfo(Culture.Thai)) { }

        public CurrencyParserConfiguration(CultureInfo ci) : base(ci)
        {
            this.BindDictionary(CurrencyExtractorConfiguration.CurrencySuffixList);
            this.BindDictionary(CurrencyExtractorConfiguration.CurrencyPrefixList);
            this.CurrencyNameToIsoCodeMap = NumbersWithUnitDefinitions.CurrencyNameToIsoCodeMap.ToImmutableDictionary();
            this.CurrencyFractionCodeList = NumbersWithUnitDefinitions.FractionalUnitNameToCodeMap.ToImmutableDictionary();
        }

    }
}
