using AMASSKWeb.Pages.Auth;

namespace AMASSKWeb.Repo
{
    public interface IAdminRepo
    {
        Task<ReadRepoResult> Login(string email, string password);
    }
}