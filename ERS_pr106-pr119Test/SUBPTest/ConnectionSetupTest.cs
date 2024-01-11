using ERS_pr106_pr119.SUBP;
using Moq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119Test.SUBPTest
{
    public class ConnectionSetupTest
    {
        /*
        [Test]
        public void GetConnection_ShouldReturnIDbConnection()
        {
            // Arrange
            var oracleConnectionMock = new Mock<OracleConnection>();

            oracleConnectionMock.Setup(c => c.State).Returns(ConnectionState.Closed);

            ConnectionSetup.OverrideConnection(oracleConnectionMock.Object);

            // Act
            var result = ConnectionSetup.GetConnection();

            // Assert
            Assert.IsInstanceOf<IDbConnection>(result);
        }

        [Test]
        public void Dispose_ShouldCloseAndDisposeConnection()
        {
            // Arrange
            var oracleConnectionMock = new Mock<OracleConnection>();

            oracleConnectionMock.Setup(c => c.State).Returns(ConnectionState.Open);

            ConnectionSetup.OverrideConnection(oracleConnectionMock.Object);

            using (var connectionSetup = new ConnectionSetup())
            {
                // Act
            }

            // Assert
            oracleConnectionMock.Verify(c => c.Close(), Times.Once);
            oracleConnectionMock.Verify(c => c.Dispose(), Times.Once);
        }
        */
    }
}
