namespace Services.Dtos.Central
{
    public class CentralBedsDto
    {
        public int CentralId { get; set; }
        public int ActiveBedCount { get; set; }
        public long LastAdditionTime { get; set; }
    }
}
