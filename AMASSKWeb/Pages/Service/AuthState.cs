namespace AMASSKWeb.Pages.Service
{
    public class AuthState
    {
        public bool IsLoggedIn { get; private set; }

        public string? Token { get; private set; }

        public string? UserEmail { get; private set; }

        public void Login(string email, string token)
        {
            UserEmail = email;
            Token = token;
            IsLoggedIn = true;
        }

        //public void SetToken(string token)
        //{
        //    Token = token;
        //    IsLoggedIn = true;
        //}
        public void Logout()
        {
            Token = null;
            UserEmail = null;
            IsLoggedIn = false;
        }

    }
}