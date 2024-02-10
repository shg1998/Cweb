namespace Services.Dtos.Central
{
    public class BedDetailDto
    {
        public string BedNumber { get; set; }
        public string Seq { get; set; }
        public string PatientName { get; set; }
        public string AlarmType { get; set; }
        public string PatientId { get; set; }
        public string Hr { get; set; }
        public bool HRBlink { get; set; }
        public string Rr { get; set; }
        public bool RrBlink { get; set; }
        public string Spo2 { get; set; }
        public bool Spo2Blink { get; set; }
        public string T1 { get; set; }
        public bool T1Blink { get; set; }
        public string T2 { get; set; }
        public bool T2Blink { get; set; }
        public string Dt { get; set; }
        public bool DtBlink { get; set; }
        public string Ibp1Sys { get; set; }
        public bool Ibp1SysBlink { get; set; }
        public string Ibp1Dia { get; set; }
        public bool Ibp1DiaBlink { get; set; }
        public string Ibp1Map { get; set; }
        public bool Ibp1MapBlink { get; set; }
        public string Ibp2Sys { get; set; }
        public bool Ibp2SysBlink { get; set; }
        public string Ibp2Dia { get; set; }
        public bool Ibp2DiaBlink { get; set; }
        public string Ibp2Map { get; set; }
        public bool Ibp2MapBlink { get; set; }
        public string Awrr { get; set; }
        public bool AwrrBlink { get; set; }
        public string EtcO2 { get; set; }
        public bool EtcO2Blink { get; set; }
        public string Fico2 { get; set; }
        public bool Fico2Blink { get; set; }
        public string NibpSys { get; set; }
        public bool NibpSysBlink { get; set; }
        public string NibpDia { get; set; }
        public bool NibpDiaBlink { get; set; }
        public string NibpMap { get; set; }
        public bool NibpMapBlink { get; set; }
        public string EcgLead { get; set; }
        public string EcgFilter { get; set; }
        public string PrSent { get; set; }
        public string HrSource { get; set; }
        public string AsysFlag { get; set; }
        public string EcgSignal { get; set; }
        public List<AlarmDto> AlarmList { get; set; }
    }
}