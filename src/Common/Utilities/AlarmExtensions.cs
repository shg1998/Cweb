namespace Common.Utilities
{
    public static class AlarmExtensions
    {
        public static string ToAlarmMessage(this string value, string alarmType)
        {
            var message = "";
            if (value.Trim() == message) return message;
            var isOldAlarm = alarmType.Trim() == "0";
            message = isOldAlarm
                ? ((AlarmMessages0)int.Parse(value.Trim())).ToString()
                : ((AlarmMessages1)int.Parse(value.Trim())).ToString();
            return NormalizeMessage(message);
        }

        private static string NormalizeMessage(string message)
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
