using OrnekDevExtremeProjesi2.DataAccess.Profile;
using OrnekDevExtremeProjesi2.Models.DTOs;
using OrnekDevExtremeProjesi2.Models;
using OrnekDevExtremeProjesi2.Business.Logging;

namespace OrnekDevExtremeProjesi2.Business.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IActivityLogService _activityLogService;

        public ProfileService()
        {
            _profileRepository = new ProfileRepository();
            _activityLogService = new ActivityLogService();
        }

        public ProfileDataDto GetProfileData(int userId)
        {
            return _profileRepository.GetProfileData(userId);
        }

        public UpdateProfileResultDto UpdateProfile(int id, string userName, string oldPassword, string newPassword, int currentUserId)
        {
            var user = _profileRepository.GetById(id);

            if (user == null)
            {
                return new UpdateProfileResultDto
                {
                    Success = false,
                    Message = "Kullanıcı bulunamadı!"
                };
            }

            // Kullanıcı adı değişmiş mi?
            if (user.UserName != userName)
            {
                if (_profileRepository.UserNameExists(userName, id))
                {
                    return new UpdateProfileResultDto
                    {
                        Success = false,
                        Message = "Bu kullanıcı adı zaten alınmış!"
                    };
                }

                string eskiAd = user.UserName;
                user.UserName = userName;

                _activityLogService.AddLog(
                    0,
                    "PROFİL",
                    $"{eskiAd} olan kullanıcı adı {userName} olarak değiştirildi.",
                    currentUserId
                );
            }

            // Şifre değiştirilecek mi?
            if (!string.IsNullOrEmpty(newPassword))
            {
                if (user.Password != oldPassword)
                {
                    return new UpdateProfileResultDto
                    {
                        Success = false,
                        Message = "Mevcut şifreniz hatalı!"
                    };
                }

                user.Password = newPassword;

                _activityLogService.AddLog(
                    0,
                    "GÜVENLİK",
                    "Kullanıcı şifresini başarıyla güncelledi.",
                    currentUserId
                );
            }

            _profileRepository.Save();

            return new UpdateProfileResultDto
            {
                Success = true,
                Message = "Profil bilgileriniz başarıyla güncellendi."
            };
        }
    }
}