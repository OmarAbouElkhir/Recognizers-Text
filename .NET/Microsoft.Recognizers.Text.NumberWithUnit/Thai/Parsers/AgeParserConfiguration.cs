using System.Globalization;

namespace Microsoft.Recognizers.Text.NumberWithUnit.Thai
{
    public class AgeParserConfiguration : ThaiNumberWithUnitParserConfiguration
    {
        public AgeParserConfiguration() : this(new CultureInfo(Culture.Thai)) { }

        public AgeParserConfiguration(CultureInfo ci) : base(ci)
        {
            this.BindDictionary(AgeExtractorConfiguration.AgeSuffixList);
        }
    }
}
