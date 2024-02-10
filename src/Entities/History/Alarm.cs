using Entities.Common;

namespace Entities.History
{
    public class Alarm : BaseEntity
    {
        public long DateTime { get; set; }
        public int CentralId { get; set; }
        public string BedId { get; set; }
        public string AlarmType { get; set; }
        public string Code { get; set; }
        public string Level { get; set; }
    }
}
