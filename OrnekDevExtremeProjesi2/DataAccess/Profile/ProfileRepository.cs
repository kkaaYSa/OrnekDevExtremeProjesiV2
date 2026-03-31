using System.Linq;
using OrnekDevExtremeProjesi2.Models;
using OrnekDevExtremeProjesi2.Models.DTOs;

namespace OrnekDevExtremeProjesi2.DataAccess.Profile
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _db;

        public ProfileRepository()
        {
            _db = new AppDbContext();
        }

        public ProfileDataDto GetProfileData(int userId)
        {
            return _db.Users
                .Where(u => u.Id == userId)
                .Select(u => new ProfileDataDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    UsersRole = u.UsersRole.RoleName
                })
                .FirstOrDefault();
        }
        public Users GetById(int id)
        {
            return _db.Users.Find(id);
        }

        public bool UserNameExists(string userName, int id)
        {
            return _db.Users.Any(x => x.UserName == userName && x.Id != id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}