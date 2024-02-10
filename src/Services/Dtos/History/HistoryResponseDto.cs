namespace Services.Dtos.History
{
    public class HistoryResponseDto
    {
        public long DateTime { get; set; }
        public string SignalData { get; set; }
        public List<HistoryResponseParameterDto> Parameters { get; set; }
    }
}
