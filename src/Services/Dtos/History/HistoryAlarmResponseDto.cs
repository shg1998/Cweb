using Services.Dtos.General;
using Entities.History;
using Common.Utilities;
using Newtonsoft.Json;

namespace Services.Dtos.History
{
    public class HistoryAlarmResponseDto : BaseDto<HistoryAlarmResponseDto, Alarm>
    {
        public long DateTime { get; set; }
        public int CentralId { get; set; }
        public string BedId { get; set; }
        [JsonIgnore]
        public string AlarmType { get; set; }
        private string _code;

        public string Code
        {
            get => _code;
            set
            {
                this.Message = value.ToAlarmMessage(this.AlarmType);
                this._code = value;
            }
        }

        public string Level { get; set; }
        public string Message { get; set; }
    }
}
