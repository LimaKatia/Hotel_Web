using CapaDAL;
using CapaEN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBL
{
    public class RoleBL
    {
        public async Task<int> CreateAsync(RoleEN role)
        {
            return await RoleDAL.CreateAsync(role);
        }

        public async Task<int> UpdateAsync(RoleEN role)
        {
            return await RoleDAL.UpdateAsync(role);
        }

        public async Task<int> DeleteAsync(RoleEN role)
        {
            return await RoleDAL.DeleteAsync(role);
        }

        public async Task<RoleEN> GetByIdAsync(RoleEN role)
        {
            return await RoleDAL.GetByIdAsync(role);
        }

        public async Task<List<RoleEN>> GetAllAsync()
        {
            return await RoleDAL.GetAllAsync();
        }

        public async Task<List<RoleEN>> SearchAsync(RoleEN role)
        {
            return await RoleDAL.SearchAsync(role);
        }
    }
}
