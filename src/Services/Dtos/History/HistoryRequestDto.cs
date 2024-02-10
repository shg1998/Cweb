namespace Services.Dtos.History
{
    public class HistoryRequestDto
    {
        public byte CentralId { get; set; }
        public byte BedId { get; set; }
        public byte Lead { get; set; }
        public long StartDateTime { get; set; }
        public int SamplesCount { get; set; }
    }
}
