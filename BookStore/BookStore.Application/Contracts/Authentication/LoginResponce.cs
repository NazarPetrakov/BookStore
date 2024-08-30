namespace BookStore.Application.Contracts.Authentication
{
    public class LoginResponse : AuthResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
