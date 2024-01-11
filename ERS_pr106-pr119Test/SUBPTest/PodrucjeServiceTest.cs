using ERS_pr106_pr119.SUBP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119Test.SUBPTest
{
    [TestFixture]
    public class PodrucjeServiceTest
    {
        [TestCase("BRAT")]
        public void InsertRowFromPotrosnja_ValidInput_ReturnsNonNegativeValue(string validOblast)
        {
            // Arrange
            var podrucjeService = new PodrucjeService();

            // Act
            int result = podrucjeService.InsertRowFromPotrosnja(validOblast);

            // Assert
            Assert.GreaterOrEqual(result, 0);
        }

        [TestCase("")]
        [TestCase("123")]
        [TestCase("invalid area")]
        public void InsertRowFromPotrosnja_InvalidInput_ThrowsFormatException(string invalidOblast)
        {
            // Arrange
            var podrucjeService = new PodrucjeService();

            // Act
            TestDelegate act = () => podrucjeService.InsertRowFromPotrosnja(invalidOblast);

            // Assert
            Assert.Throws<FormatException>(act);
        }

    }
}
