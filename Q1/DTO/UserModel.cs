using System.Text.Json.Serialization;

namespace Q1.DTO
{
    public class UserModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        [JsonIgnore]
        public int RoleId { get; set; }
    }
}
