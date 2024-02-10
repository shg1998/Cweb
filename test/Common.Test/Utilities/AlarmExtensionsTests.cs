using Common.Utilities;

namespace Common.Test.Utilities
{
    public class AlarmExtensionsTests
    {
        [Theory]
        [InlineData("30", "0", "ECG_ASYSTOLE")]
        [InlineData("60", "0", "CO2_SYSTEM_FAULT_1")]
        [InlineData("51", "1", "ECG_ASYSTOLE")]
        [InlineData("101", "1", "ASYSTOLE_ARRHYTHMIA")]
        [InlineData("118", "1", "ST_HIGH")]
        [InlineData("201", "1", "T1_TOO_LOW")]
        public void ToAlarmMessage_Returns_Correct_Message(string value, string alarmType, string expectedMessage)
        {
            // Act
            var message = value.ToAlarmMessage(alarmType);

            // Assert
            Xunit.Assert.Equal(this.NormalizeMessage(expectedMessage), message);
        }


        private string NormalizeMessage(string message)
        {
            return message.Replace("SHARPSIGN", "#").
                Replace("DOTSIGN", ".").
                Replace("PERCENTAGESIGN", "%").
                Replace("QUESTIONSIGN", "?").
                Replace("_", " ").
                Trim();
        }
    }
}
