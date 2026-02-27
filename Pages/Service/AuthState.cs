namespace AMASSKWeb.Pages.Service
{
    public class AuthState
    {
        public string? Email { get; private set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(Email);

        public void Login(string email)
        {
            Email = email;
        }

        public void Logout()
        {
            Email = null;
        }

    }
}
