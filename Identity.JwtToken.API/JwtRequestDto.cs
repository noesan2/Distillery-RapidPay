namespace Identity.JwtToken.API
{
    public class JwtRequestDto
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public IDictionary<string, bool> Claims { get; set; }
    }


}
