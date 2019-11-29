﻿using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using IbanNet.Validation.Methods;
using sinkien.IBAN4Net;

namespace IbanNet.Benchmark
{
	[SimpleJob(RuntimeMoniker.HostProcess)]
	[MarkdownExporterAttribute.GitHub]
	[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical)]
	[MemoryDiagnoser]
	public class ValidatorBenchmark
	{
		private IIbanValidator _strictValidator, _fastValidator;
		private IbanValidation.IbanValidator _nugetIbanValidator;
		private IList<string> _testData;

		[Params(1, 10)]
		public int Count { get; set; }


		[GlobalSetup]
		public void GlobalSetup()
		{
			// IbanNet setup
			_strictValidator = new IbanValidator();
			_fastValidator = new IbanValidator(new IbanValidatorOptions { ValidationMethod = new FastValidation() });

			_nugetIbanValidator = new IbanValidation.IbanValidator();

			_testData = TestSamples.GetIbanSamples(Count);
		}

		[Benchmark(Baseline = true)]
		public void IbanNet_Strict()
		{
			// ReSharper disable once ForCanBeConvertedToForeach
			for (int i = 0; i < _testData.Count; i++)
			{
				_strictValidator.Validate(_testData[i]);
			}
		}

		[Benchmark]
		public void IbanNet_Fast()
		{
			// ReSharper disable once ForCanBeConvertedToForeach
			for (int i = 0; i < _testData.Count; i++)
			{
				_fastValidator.Validate(_testData[i]);
			}
		}

		[Benchmark]
		public void NuGet_IbanValidator()
		{
			// ReSharper disable once ForCanBeConvertedToForeach
			for (int i = 0; i < _testData.Count; i++)
			{
				_nugetIbanValidator.Validate(_testData[i]);
			}
		}

		//[Benchmark]
		//public void NuGet_IBAN4NET()
		//{
		//	// ReSharper disable once ForCanBeConvertedToForeach
		//	for (int i = 0; i < _testData.Count; i++)
		//	{
		//		IbanUtils.IsValid(_testData[i], out IbanFormatViolation _);
		//	}
		//}
	}
}