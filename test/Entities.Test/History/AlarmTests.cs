using Entities.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Test.History
{
    public class AlarmTests
    {
        [Fact]
        public void Create_SetsPropertiesCorrectly()
        {
            // Arrange
            var dateTime = DateTime.UtcNow.Ticks;
            var centralId = 123;
            var bedId = "abc";
            var alarmType = "Fire";
            var code = "DEF";
            var level = "High";

            // Act
            var alarm = new Alarm
            {
                DateTime = dateTime,
                CentralId = centralId,
                BedId = bedId,
                AlarmType = alarmType,
                Code = code,
                Level = level
            };

            // Assert
            Assert.Equal(dateTime, alarm.DateTime);
            Assert.Equal(centralId, alarm.CentralId);
            Assert.Equal(bedId, alarm.BedId);
            Assert.Equal(alarmType, alarm.AlarmType);
            Assert.Equal(code, alarm.Code);
            Assert.Equal(level, alarm.Level);
        }
    }
}
