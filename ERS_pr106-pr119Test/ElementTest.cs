using ERS_pr106_pr119.Model;
using NUnit.Framework;

namespace ERS_pr106_pr119.Tests
{
    [TestFixture]
	public class ElementTests
	{
		[Test]
		public void Constructor_ValidArguments_InitializesProperties()
		{
			// Arrange
			int sat = 1;
			string load = "10";
			string oblast = "Test";
			string tip = "ostv";
			string datumUvoza = "01/01/2022";
			string satnicaUvoza = "12:00:00";
			string fileLocation = "C:\\Test\\file.txt";
			string datumImenaFajla = "01.01.2023.";
			string fileName = "prog_2023_01_01.xml";

			// Act
			Element element = new Element(sat, load, oblast, tip, datumUvoza, satnicaUvoza, fileLocation, datumImenaFajla, fileName);

			// Assert
			Assert.AreEqual(sat, element.Sat);
			Assert.AreEqual(load, element.Load);
			Assert.AreEqual(oblast, element.Oblast);
			Assert.AreEqual(tip, element.Tip);
			Assert.AreEqual(datumUvoza, element.DatumUvoza);
			Assert.AreEqual(satnicaUvoza, element.SatnicaUvoza);
			Assert.AreEqual(fileLocation, element.File_location);
			Assert.AreEqual(datumImenaFajla, element.DatumImenaFajla);
			Assert.AreEqual(fileName, element.FileName);
		}

		[Test]
		[TestCase("test123")]
		[TestCase("_)!ssd")]
		public void Constructor_InvalidTip_ThrowsArgumentException(string tip)
		{
			// Arrange
			int sat = 1;
			string load = "10";
			string oblast = "Test";
			string datumUvoza = "01/01/2022";
			string satnicaUvoza = "12:00:00";
			string fileLocation = "C:\\Test\\file.txt";
			string datumImenaFajla = "01012022";
			string fileName = "prog_2023_01_01.xml";

			// Act, Assert
			Assert.Throws<ArgumentException>(() => new Element(sat, load, oblast, tip, datumUvoza, satnicaUvoza, fileLocation, datumImenaFajla, fileName));
		}

		[Test]
		[TestCase("-10")]
		[TestCase("abc")]
		public void Constructor_InvalidLoad_ThrowsArgumentException(string load)
		{
			// Arrange
			int sat = 1;
			string oblast = "Test";
			string tip = "ostv";
			string datumUvoza = "01/01/2022";
			string satnicaUvoza = "12:00:00";
			string fileLocation = "C:\\Test\\file.txt";
			string datumImenaFajla = "01012022";
			string fileName = "prog_2023_01_01.xml";

			// Act, Assert
			Assert.Throws<ArgumentException>(() => new Element(sat, load, oblast, tip, datumUvoza, satnicaUvoza, fileLocation, datumImenaFajla, fileName));
		}

		[Test]
		[TestCase("01-01-2022")]
		[TestCase("35/01/2022")]
		[TestCase("01/15/2022")]
		[TestCase("2022/01/01")]
		[TestCase("01/01")]
		[TestCase("01-01-2022")]
		public void Constructor_InvalidDatumUvoza_ThrowsFormatException(string datumUvoza)
		{
			// Arrange
			int sat = 1;
			string load = "10";
			string oblast = "Test";
			string tip = "ostv";
			string satnicaUvoza = "12:00:00";
			string fileLocation = "C:\\Test\\file.txt";
			string datumImenaFajla = "01012022";
			string fileName = "prog_2023_01_01.xml";

			// Act, Assert
			Assert.Throws<FormatException>(() => new Element(sat, load, oblast, tip, datumUvoza, satnicaUvoza, fileLocation, datumImenaFajla, fileName));
		}

		[Test]
		[TestCase("12:00")]
		[TestCase("25:00:00")]
		[TestCase("00:-10")]
		public void Constructor_InvalidSatnicaUvoza_ThrowsFormatException(string satnicaUvozaa)
		{
			// Arrange
			int sat = 1;
			string load = "10";
			string oblast = "Test";
			string tip = "ostv";
			string datumUvoza = "01/01/2022";
			string satnicaUvoza = satnicaUvozaa;
			string fileLocation = "C:\\Test\\file.txt";
			string datumImenaFajla = "01012022";
			string fileName = "prog_2023_01_01.xml";

			// Act, Assert
			Assert.Throws<FormatException>(() => new Element(sat, load, oblast, tip, datumUvoza, satnicaUvoza, fileLocation, datumImenaFajla, fileName));
		}
	}
}
