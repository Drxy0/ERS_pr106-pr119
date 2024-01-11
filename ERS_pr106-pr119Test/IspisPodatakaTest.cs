using ERS_pr106_pr119;
using ERS_pr106_pr119.DTO;
using ERS_pr106_pr119.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119Test
{
    [TestFixture]
	public class IspisPodatakaTest
	{
		IspisPodataka uiInstance = null;
		InMemoryDataBaseDTO inMemDB = null;

		[SetUp]
		public void Setup()
		{
			uiInstance = new IspisPodataka();

			var inMemDBMock = new Mock<InMemoryDataBaseDTO>();

			var prognoziranaPotrosnja = new List<Element>
			{
				new Element(1, "100", "VOJ", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml"),
				new Element(2, "150", "VOJ", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml"),
				new Element(2, "200", "BGD", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml"),
			};

			var ostvarenaPotrosnja = new List<Element>
			{
				new Element(1, "900", "VOJ", "ostv", "01/01/2023", "01:00:00", "./xml/ostv_2023_01_01.xml", "01.01.2023.", "ostv_2023_01_01.xml"),
				new Element(2, "140", "VOJ", "ostv", "01/01/2023", "01:00:00", "./xml/ostv_2023_01_01.xml", "01.01.2023.", "ostv_2023_01_01.xml"),
				new Element(2, "220", "BGD", "ostv", "01/01/2023", "01:00:00", "./xml/ostv_2023_01_01.xml", "01.01.2023.", "ostv_2023_01_01.xml"),
			};

			inMemDBMock.Setup(x => x.PrognoziranaPotrosnja).Returns(prognoziranaPotrosnja);
			inMemDBMock.Setup(x => x.OstvarenaPotrosnja).Returns(ostvarenaPotrosnja);
			inMemDB = inMemDBMock.Object;
		}

		[Test]
		[TestCase("01.01.2023.", "VOJ")]
		public void IspisOpcijeInMemory_ValidInput_ReturnsExportDTO(string datum, string geoOblast)
		{
			// Act
			var result = uiInstance.IspisOpcijeInMemory(inMemDB, datum, geoOblast);

			// Assert
			Assert.NotNull(result);
		}

		[Test]
		public void IspisOpcijeInMemory_DTOEmpty_ReturnsNull()
		{
			// Arrange
			InMemoryDataBaseDTO inMemDB = new InMemoryDataBaseDTO();

			// Act
			var result = uiInstance.IspisOpcijeInMemory(inMemDB, "01.01.2023.", "VOJ");

			// Assert
			Assert.Null(result);
		}

		[Test]
		[TestCase("01-01-2022")]
		[TestCase("2022/01/01")]
		[TestCase("01/01")]
		[TestCase("01-01-2022")]
		public void IspisOpcijeInMemory_InvalidDatum_ReturnsNull(string datum)
		{
			// Act
			var result = uiInstance.IspisOpcijeInMemory(inMemDB, datum, "VOJ");

			// Assert
			Assert.Null(result);
		}


		[Test]
		public void IspisOpcijeInMemory_InvalidGeoOblast_ReturnsNull()
		{
			// Act
			var result = uiInstance.IspisOpcijeInMemory(inMemDB, "01.01.2023.", "qwerty");

			// Assert
			Assert.Null(result);
		}
	}
}
