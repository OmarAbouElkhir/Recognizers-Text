using System.Globalization;
using Microsoft.Recognizers.Text.Number;
using Microsoft.Recognizers.Text.Number.Thai;

namespace Microsoft.Recognizers.Text.NumberWithUnit.Thai
{
    public class ThaiNumberWithUnitParserConfiguration : BaseNumberWithUnitParserConfiguration
    {
        public ThaiNumberWithUnitParserConfiguration(CultureInfo ci) : base(ci)
        {
            this.InternalNumberExtractor = NumberExtractor.GetInstance();
            this.InternalNumberParser = AgnosticNumberParserFactory.GetParser(AgnosticNumberParserType.Number, new ThaiNumberParserConfiguration());
            this.ConnectorToken = string.Empty;
        }

        public override IParser InternalNumberParser { get; }

        public override IExtractor InternalNumberExtractor { get; }

        public override string ConnectorToken { get; }
    }
}
