using Entities.History;
using Services.Dtos.General;

namespace Services.Dtos.History
{
    public class CreateAlarmDto : BaseDto<CreateAlarmDto, Alarm>
    {
        public long DateTime { get; set; }
        public int CentralId { get; set; }
        public string BedId { get; set; }
        public string AlarmType { get; set; }
        public string Code { get; set; }
        public string Level { get; set; }
    }
}
