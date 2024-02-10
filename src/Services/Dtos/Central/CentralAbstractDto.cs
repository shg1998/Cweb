using Services.Dtos.General;

namespace Services.Dtos.Central
{
    public class CentralAbstractDto : BaseDtoComplexKey<CentralAbstractDto, Entities.User.User>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int ActiveBeds { get; set; }
    }
}
