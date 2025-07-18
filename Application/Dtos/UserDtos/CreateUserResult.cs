namespace Application.Dtos.UserDtos
{
    public class CreateUserResult
    {
        public UserDto User { get; set; }
        public string? Warning { get; set; }
    }
}
