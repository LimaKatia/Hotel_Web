using CapaDAL;
using CapaEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBL
{
    public class UserBL
    {
        public async Task<int> CreateAsync(UserEN user)
        {
            return await UserDAL.CreateAsync(user);
        }

        public async Task<int> UpdateAsync(UserEN user)
        {
            return await UserDAL.UpdateAsync(user);
        }

        public async Task<int> DeleteAsync(UserEN user)
        {
            return await UserDAL.DeleteAsync(user);
        }

        public async Task<UserEN> GetByIdAsync(UserEN user)
        {
            return await UserDAL.GetByIdAsync(user);
        }

        public async Task<List<UserEN>> GetAllAsync()
        {
            return await UserDAL.GetAllAsync();
        }

        public async Task<List<UserEN>> SearchAsync(UserEN user)
        {
            return await UserDAL.SearchAsync(user);
        }

        public async Task<List<UserEN>> SearchIncludeRoleAsync(UserEN user)
        {
            return await UserDAL.SearchIncludeRoleAsync(user);
        }

        public async Task<UserEN> LoginAsync(UserEN user)
        {
            return await UserDAL.LoginAsync(user);
        }

        public async Task<int> ChangePasswordAsync(UserEN user, string oldPassword)
        {
            return await UserDAL.ChangePasswordAsync(user, oldPassword);
        }
    }
}
