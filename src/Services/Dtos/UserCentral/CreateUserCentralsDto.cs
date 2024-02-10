namespace Services.Dtos.UserCentral
{
    public class CreateUserCentralsDto
    {
        public int UserId { get; set; }
        public List<int> CentralIds { get; set; }
    }
}
