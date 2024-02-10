using Entities.History;
using Services.Dtos.General;

namespace Services.Dtos.History
{
    public class HistoryResponseParameterDto : BaseDtoComplexKey<HistoryResponseParameterDto, Parameter>
    {
        public long DateTime { get; set; }
        public int CentralId { get; set; }
        public string BedId { get; set; }
        public string Hr { get; set; }
        public string Rr { get; set; }
        public string Spo2 { get; set; }
        public string T1 { get; set; }
        public string T2 { get; set; }
        public string Dt { get; set; }
        public string Ibp1Sys { get; set; }
        public string Ibp1Dia { get; set; }
        public string Ibp1Map { get; set; }
        public string Ibp2Sys { get; set; }
        public string Ibp2Dia { get; set; }
        public string Ibp2Map { get; set; }
        public string Awrr { get; set; }
        public string EtcO2 { get; set; }
        public string FiCo2 { get; set; }
        public string NibpSys { get; set; }
        public string NibpDia { get; set; }
        public string NibpMap { get; set; }
    }
}
