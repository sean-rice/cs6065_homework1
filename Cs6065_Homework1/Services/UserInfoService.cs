using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Cs6065_Homework1.Data;
using Cs6065_Homework1.Models;

namespace Cs6065_Homework1.Services
{
    public class UserInfoService
    {
        private readonly ApplicationDbContext _context;

        public UserInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<UserInfoEntity> GetUserInfoEntityAsync(Guid userId)
        {
            UserInfoEntity item = null;
            try
            {
                item = await _context.UserInfoEntitys
                    .Where(x => x.UserId == userId)
                    .SingleAsync();
                return item;
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserInfo> GetUserInfoAsync(ApplicationUser user)
        {
            var userInfoEntity = await GetUserInfoEntityAsync(user.Id);
            if (userInfoEntity == null)
            {
                return null;
            }
            UserInfo userInfo = new UserInfo
            {
                FirstName = userInfoEntity.FirstName,
                LastName = userInfoEntity.LastName
            };
            return userInfo;
        }

        public async Task<bool> SetUserInfoAsync(ApplicationUser user, UserInfo userInfo)
        {
            #nullable enable
            // TODO: fix this because it's probably a race condition for the db
            UserInfoEntity? userInfoEntity = await GetUserInfoEntityAsync(user.Id);
            if (userInfoEntity != null)
            {
                userInfoEntity.FirstName = userInfo.FirstName;
                userInfoEntity.LastName = userInfo.LastName;
                
                var saveResult =  await _context.SaveChangesAsync();
                return saveResult == 1;
            }
            else
            {
                userInfoEntity = new UserInfoEntity { UserId = user.Id, FirstName = userInfo.FirstName, LastName = userInfo.LastName };
                _context.Add(userInfoEntity);
                var saveResult = await _context.SaveChangesAsync();
                return saveResult == 1;
            }
            #nullable disable
        }
    }
}
