using NUnit.Framework;
using Moq;
using System;
using ERS_pr106_pr119.SUBP.Service;
using ERS_pr106_pr119;
using ERS_pr106_pr119.SUBP.RowManagement;

[TestFixture]
public class OstvarenaEnergijaTest
{
    [TestCase("invalidDate", "someArea", "Nevalidan unos datuma")]
    public void PullOstvPotrosnjaByDateAndArea_InvalidDate_ThrowsException(string date, string area, string expectedErrorMessage)
    {
        // Arrange
        var ostvarenaService = new OstvarenaEnergijaService(); 

        // Act
        TestDelegate act = () => ostvarenaService.PullOstvPotrosnjaByDateAndArea(date, area);

        // Assert
        Assert.Throws<FormatException>(act, expectedErrorMessage);
    }



    [TestCase("01.01.2022.", "invalidArea", "Nema informacija za tu oblast!")]
    public void PullOstvPotrosnjaByDateAndArea_InvalidArea_ThrowsException(string date, string area, string expectedErrorMessage)
    {
        // Arrange
        var ostvarenaService = new OstvarenaEnergijaService(); 

        // Act
        TestDelegate act = () => ostvarenaService.PullOstvPotrosnjaByDateAndArea(date, area);

        // Assert
        Assert.Throws<FormatException>(act, expectedErrorMessage);
    }



    [TestCase("01.01.2023.", "MIRKO")]
    public void PullOstvPotrosnjaByDateAndArea_ValidInputs_ReturnsList(string date, string area)
    {
        // Arrange
        var ostvManagementMock = new Mock<IOstvManagement>();

        // Simulacija podataka u bazi podataka
        var fakeData = new List<Element>
        {
                new Element(1, "100", "MIRKO", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml")

        };

        // Postavljanje ponašanja mock-a
        ostvManagementMock.Setup(m => m.PullOstvPotrosnjaByDateAndArea(date, area))
                         .Returns(fakeData);

        var ostvManagement = ostvManagementMock.Object;

        // Act
        List<Element> result = null;
        try
        {
            result = ostvManagement.PullOstvPotrosnjaByDateAndArea(date, area);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Exception thrown: {ex.Message}");
        }

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<List<Element>>(result);
        // Dodajte druge provere prema potrebama vaše implementacije
    }

}
