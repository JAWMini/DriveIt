using DriveIt.Data;
using Microsoft.EntityFrameworkCore;

namespace DriveIt.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetUserByEmailAsync(String email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if(user is null || user is not Customer)
            {
                return null;
            }

            return user as Customer;

        }
    }
}
