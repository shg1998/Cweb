using Entities.History;

namespace Entities.Test.History
{
    public class ParameterTests
    {
        [Fact]
        public void Create_SetsPropertiesCorrectly()
        {
            // Arrange
            var dateTime = DateTime.UtcNow.Ticks;
            var centralId = 123;
            var bedId = "abc";
            var spo2 = "12";
            var ibp1Sys = "1";
            var hr = "10";

            // Act
            var ecgSignal = new Parameter
            {
                DateTime = dateTime,
                CentralId = centralId,
                BedId = bedId,
                Spo2 = spo2,
                Ibp1Sys = ibp1Sys,
                Hr = hr
            };

            // Assert
            Assert.Equal(dateTime, ecgSignal.DateTime);
            Assert.Equal(centralId, ecgSignal.CentralId);
            Assert.Equal(bedId, ecgSignal.BedId);
            Assert.Equal(spo2, ecgSignal.Spo2);
            Assert.Equal(ibp1Sys, ecgSignal.Ibp1Sys);
            Assert.Equal(hr, ecgSignal.Hr);
        }
    }
}
