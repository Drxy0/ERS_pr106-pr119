using ERS_pr106_pr119.SUBP.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERS_pr106_pr119.Model;
using ERS_pr106_pr119.SUBP.DAO.RowManagement;

namespace ERS_pr106_pr119Test.SUBPTest
{
    public class PrognoziranaEnergijaTest
    {
        [TestCase("invalidDate", "someArea", "Nevalidan unos datuma")]
        public void PullProgPotrosnjaByDateAndArea_InvalidDate_ThrowsException(string date, string area, string expectedErrorMessage)
        {
            // Arrange
            var prognoziranaService = new PrognozaEnergijeService();

            // Act
            TestDelegate act = () => prognoziranaService.PullProgPotrosnjaByDateAndArea(date, area);

            // Assert
            Assert.Throws<FormatException>(act, expectedErrorMessage);
        }



        [TestCase("01.01.2022.", "invalidArea", "Nema informacija za tu oblast!")]
        public void PullProgPotrosnjaByDateAndArea_InvalidArea_ThrowsException(string date, string area, string expectedErrorMessage)
        {
            // Arrange
            var prognoziranaService = new PrognozaEnergijeService();

            // Act
            TestDelegate act = () => prognoziranaService.PullProgPotrosnjaByDateAndArea(date, area);

            // Assert
            Assert.Throws<FormatException>(act, expectedErrorMessage);
        }



        [TestCase("01.01.2023.", "MIRKO")]
        public void PullProgPotrosnjaByDateAndArea_ValidInputs_ReturnsList(string date, string area)
        {
            // Arrange
            var progManagementMock = new Mock<IProgManagement>();

            // Simulacija podataka u bazi podataka
            var fakeData = new List<Element>
        {
                new Element(1, "100", "MIRKO", "prog", "01/01/2023", "01:00:00", "./xml/prog_2023_01_01.xml", "01.01.2023.", "prog_2023_01_01.xml")

        };

            // Postavljanje ponašanja mock-a
            progManagementMock.Setup(m => m.PullProgPotrosnjaByDateAndArea(date, area))
                             .Returns(fakeData);

            var ostvManagement = progManagementMock.Object;

            // Act
            List<Element> result = null;
            try
            {
                result = ostvManagement.PullProgPotrosnjaByDateAndArea(date, area);
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
}
