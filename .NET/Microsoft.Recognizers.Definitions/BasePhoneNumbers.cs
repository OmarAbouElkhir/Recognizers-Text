﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//     
//     Generation parameters:
//     - DataFilename: Patterns\Base-PhoneNumbers.yaml
//     - Language: NULL
//     - ClassName: BasePhoneNumbers
// </auto-generated>
//------------------------------------------------------------------------------
namespace Microsoft.Recognizers.Definitions
{
	using System;
	using System.Collections.Generic;

	public static class BasePhoneNumbers
	{
		public const string NumberReplaceToken = "@builtin.phone";
		public static readonly Func<string, string, string> IntegerRegexDefinition = (placeholder, thousandsmark) => $@"(((?<!\d+\s*)-\s*)|((?<=\b)(?<!(\d+\.|\d+,))))\d{{1,3}}({thousandsmark}\d{{3}})+(?={placeholder})";
		public const string PlaceHolderDefault = "\\D|\\b";
	}
}