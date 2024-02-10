using Entities.Common;

namespace Entities.History
{
    public class EcgSignal : BaseEntity
    {
        public long DateTime { get; set; }
        public int CentralId { get; set; }
        public string BedId { get; set; }
        public string EcgLead { get; set; }
        public string EcgFilter { get; set; }
        public string SignalData { get; set; }
    }
}
