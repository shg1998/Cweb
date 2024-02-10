using Entities.History;
using Services.Dtos.General;

namespace Services.Dtos.History
{
    public class CreateEcgSignalDto : BaseDto<CreateEcgSignalDto, EcgSignal>
    {
        public long DateTime { get; set; }
        public int CentralId { get; set; }
        public string BedId { get; set; }
        public string EcgLead { get; set; }
        public string EcgFilter { get; set; }
        public string SignalData { get; set; }
    }
}
