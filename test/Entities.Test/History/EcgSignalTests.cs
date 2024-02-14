using Entities.History;

namespace Entities.Test.History
{
    public class EcgSignalTests
    {
        [Fact]
        public void Create_SetsPropertiesCorrectly()
        {
            // Arrange
            var dateTime = DateTime.UtcNow.Ticks;
            var centralId = 123;
            var bedId = "abc";
            var signalData = "dlhdkghdkjgdkjgfdjfdgfdhdf87356375735735hdfhdf";
            var ecgFilter = "No";
            var ecgLead = "I";

            // Act
            var ecgSignal = new EcgSignal
            {
                DateTime = dateTime,
                CentralId = centralId,
                BedId = bedId,
                SignalData = signalData,
                EcgFilter = ecgFilter,
                EcgLead = ecgLead
            };

            // Assert
            Assert.Equal(dateTime, ecgSignal.DateTime);
            Assert.Equal(centralId, ecgSignal.CentralId);
            Assert.Equal(bedId, ecgSignal.BedId);
            Assert.Equal(signalData, ecgSignal.SignalData);
            Assert.Equal(ecgLead, ecgSignal.EcgLead);
            Assert.Equal(ecgFilter, ecgSignal.EcgFilter);
        }
    }
}
