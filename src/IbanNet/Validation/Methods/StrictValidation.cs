﻿using System.Collections.Generic;
using IbanNet.Validation.Rules;

namespace IbanNet.Validation.Methods
{
	/// <summary>
	/// Strict validation consists of all built-in IBAN validation rules.
	/// </summary>
	public class StrictValidation : FastValidation
	{
		internal override IEnumerable<IIbanValidationRule> GetRules()
		{
			foreach (IIbanValidationRule rule in base.GetRules())
			{
				if (rule is Mod97Rule)
				{
					var structureValidationFactory = new CachedStructureValidationFactory(new SwiftStructureValidationFactory());
					yield return new IsMatchingStructureRule(structureValidationFactory);
				}

				yield return rule;
			}
		}
	}
}