using ERS_pr106_pr119.DTO;
using ERS_pr106_pr119.InMemoryDB.Service;
using ERS_pr106_pr119.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119Test.InMemoryDBTest
{
	[TestFixture]
	public class InMemoryDB_ServiceTest
	{
		private InMemoryDB_Service inMemoryDBService;
		private Mock<InMemoryDataBaseDTO> mockInMemoryDataBase;

		[SetUp]
		public void SetUp()
		{
			mockInMemoryDataBase = new Mock<InMemoryDataBaseDTO>();
			inMemoryDBService = new InMemoryDB_Service(mockInMemoryDataBase.Object);
		}

		[Test]
		public void TestDataBaseCount_WhenCountsAreEqual_ReturnsTrue()
		{
			// Arrange
			mockInMemoryDataBase.SetupGet(db => db.PrognoziranaPotrosnja).Returns(new List<Element> 
			{
				new Element(1, "100", "VOJ", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml"),
				new Element(2, "150", "VOJ", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml"),
				new Element(2, "200", "BGD", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml"),
			});
			mockInMemoryDataBase.SetupGet(db => db.OstvarenaPotrosnja).Returns(new List<Element> 
			{
				new Element(1, "900", "VOJ", "ostv", "01/01/2023", "01:00:00", "./xml/ostv_2023_01_01.xml", "01.01.2023.", "ostv_2023_01_01.xml"),
				new Element(2, "140", "VOJ", "ostv", "01/01/2023", "01:00:00", "./xml/ostv_2023_01_01.xml", "01.01.2023.", "ostv_2023_01_01.xml"),
				new Element(2, "220", "BGD", "ostv", "01/01/2023", "01:00:00", "./xml/ostv_2023_01_01.xml", "01.01.2023.", "ostv_2023_01_01.xml")
			});

			// Act
			bool result = inMemoryDBService.DataBaseCount();

			// Assert
			Assert.IsTrue(result);
		}

		[Test]
		public void TestDataBaseCount_WhenCountsAreNotEqualOrBothEmpty_ReturnsFalse()
		{
			// Arrange
			mockInMemoryDataBase.SetupGet(db => db.PrognoziranaPotrosnja).Returns(new List<Element> 
			{
				new Element(1, "100", "VOJ", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml"),
				new Element(2, "150", "VOJ", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml"),
				new Element(2, "200", "BGD", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml"),
			});
			mockInMemoryDataBase.SetupGet(db => db.OstvarenaPotrosnja).Returns(new List<Element>()); // Empty list

			// Act
			bool result = inMemoryDBService.DataBaseCount();

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		[TestCase("01.01.2023.", "VOJ")]
		[TestCase("01.01.2023.", "BGD")]
		public void TestPullProgPotrosnjaByDateAndArea_WhenMatchingElementsExist_ReturnsMatchingList(string datum, string geoOblast)
		{
			mockInMemoryDataBase.SetupGet(db => db.PrognoziranaPotrosnja).Returns(new List<Element>
			{
				new Element(1, "100", "VOJ", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml"),
				new Element(2, "150", "BGD", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml"),
            });

			// Act
			List<Element> result = inMemoryDBService.PullProgPotrosnjaByDateAndArea(datum, geoOblast);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count);
		}

		[Test]
		[TestCase("01.10.2023.", "VOJ")]
		[TestCase("01.01.2020.", "BGD")]
		[TestCase("01.01.2023.", "zxcvzvcx")]
		[TestCase("qwerty", "VOJ")]
		public void TestPullProgPotrosnjaByDateAndArea_WrongParameters(string datum, string geoOblast)
		{
			mockInMemoryDataBase.SetupGet(db => db.PrognoziranaPotrosnja).Returns(new List<Element>
			{
				new Element(1, "100", "VOJ", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml"),
				new Element(2, "150", "VOJ", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml"),
			});

			// Act
			List<Element> result = inMemoryDBService.PullProgPotrosnjaByDateAndArea(datum, geoOblast);

			// Assert
			Assert.IsNull(result);
		}

	}
}
