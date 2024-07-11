namespace IdentityServer.DTO.Model
{
    public class AuthResponseDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
