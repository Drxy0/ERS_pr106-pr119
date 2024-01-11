using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERS_pr106_pr119.Model;
using NUnit.Framework;

namespace ERS_pr106_pr119.Tests
{
    [TestFixture]
	public class DatumTest
	{
		[Test]
		public void Constructor_ValidDate_ShouldNotThrowException()
		{
			// Arrange
			string dan = "01";
			string mjesec = "01";
			string godina = "2022";

			// Act, Assert
			Assert.DoesNotThrow(() => new Datum(dan, mjesec, godina));
		}

		[Test]
		public void Constructor_InvalidDate_ThrowsFormatException()
		{
			// Arrange
			string dan = "35"; // Invalid day
			string mjesec = "12";
			string godina = "2022";

			// Act, Assert
			Assert.Throws<FormatException>(() => new Datum(dan, mjesec, godina));
		}

		[Test]
		public void GetDatum_ReturnsFormattedDate()
		{
			// Arrange
			Datum datum = new Datum("05", "02", "2023");

			// Act
			string result = datum.GetDatum();

			// Assert
			Assert.AreEqual("05.02.2023.", result);
		}

		[Test]
		public void SetDatumFromString_ValidString_ReturnsValidDatumObject()
		{
			// Arrange
			string inputString = "15.07.2021.";

			// Act
			Datum result = new Datum().SetDatumFromString(inputString);

			// Assert
			Assert.AreEqual("15", result.Dan);
			Assert.AreEqual("07", result.Mjesec);
			Assert.AreEqual("2021", result.Godina);
		}

		[TestCase("01.01.2022.")]
		[TestCase("15.06.2023.")]
		[TestCase("31.12.2024.")]
		public void SetDatumFromString_ValidString_ReturnsExpectedDatumObject(string inputString)
		{
			// Arrange, Act
			Datum result = new Datum().SetDatumFromString(inputString);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(inputString, result.GetDatum());
		}

		[TestCase("32.01.2022.")] // Invalid day
		[TestCase("15.13.2023.")] // Invalid month
		[TestCase("01.01.22.")]   // Invalid year
		[TestCase("01-01-2022")] // Invalid separator
		[TestCase("01/01/2022")] // Invalid separator
		[TestCase("2022.01.01.")] // Invalid order
		public void SetDatumFromString_InvalidString_ThrowsFormatException(string invalidInput)
		{
			// Act, Assert
			Assert.Throws<FormatException>(() => new Datum().SetDatumFromString(invalidInput));
		}
	}
}

