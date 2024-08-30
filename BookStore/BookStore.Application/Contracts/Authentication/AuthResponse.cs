namespace BookStore.Application.Contracts.Authentication
{
    public class AuthResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
