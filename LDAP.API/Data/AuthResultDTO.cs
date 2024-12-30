namespace LDAP.API.Data
{
    public class AuthResultDTO
    {
        public string Token { get; set; }
        public bool Result { get; set; }
        public List<string> Errors { get; set; }
    }
}
