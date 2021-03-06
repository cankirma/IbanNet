﻿using System;
using FluentAssertions;
using Xunit;

namespace IbanNet.Registry
{
	public class IbanCountryTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("N")]
		[InlineData("NLD")]
		public void When_country_code_is_of_invalid_length_should_throw(string countryCode)
		{
			// Act
			Action act = () => new IbanCountry(countryCode);

			// Assert
			act.Should().Throw<ArgumentException>()
				.Which.ParamName.Should().Be("twoLetterISORegionName");
		}

		[Fact]
		public void When_country_code_is_of_valid_length_should_not_throw()
		{
			// Act
			Action act = () => new IbanCountry("ZA");

			// Assert
			act.Should().NotThrow();
		}

		[Fact]
		public void When_country_code_is_provided_in_lowercase_should_make_it_uppercase()
		{
			// Act
			var actual = new IbanCountry("nl");

			// Assert
			actual.TwoLetterISORegionName.Should().Be("NL");
		}
	}
}
