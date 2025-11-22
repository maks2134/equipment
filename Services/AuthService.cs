using System.Linq;
using equipment_accounting_system.Models;
namespace equipment_accounting_system.Services
{
    public class AuthService
    {
        private readonly FileService _fileService;
        private User? _currentUser;
        public AuthService(FileService fileService)
        {
            _fileService = fileService;
        }
        public User? CurrentUser => _currentUser;
        public bool IsAuthenticated => _currentUser != null;
        public bool IsAdministrator => _currentUser?.Role == UserRole.Administrator;
        public bool Login(string username, string password)
        {
            try
            {
                var users = _fileService.LoadUsers();
                var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
                if (user != null)
                {
                    _currentUser = user;
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public void Logout()
        {
            _currentUser = null;
        }
    }
}
