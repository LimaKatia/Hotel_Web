using CapaEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDAL
{
    public class RoleDAL
    {
        public static async Task<int> CreateAsync(RoleEN role)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                dbContext.Role.Add(role);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> UpdateAsync(RoleEN role)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var roleDB = await dbContext.Role.FirstOrDefaultAsync(c => c.Id == role.Id);
                if (roleDB != null)
                {
                    roleDB.Name = role.Name;
                    dbContext.Update(roleDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<int> DeleteAsync(RoleEN role)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var roleDB = await dbContext.Role.FirstOrDefaultAsync(c => c.Id == role.Id);

                if (roleDB != null)
                {
                    dbContext.Role.Remove(roleDB);
                    result = await dbContext.SaveChangesAsync();
                }

            }
            return result;
        }


        public static async Task<RoleEN> GetByIdAsync(RoleEN role)
        {
            var roleDB = new RoleEN();
            using (var dbContext = new ContextDB())
            {
                roleDB = await dbContext.Role.FirstOrDefaultAsync(c => c.Id == role.Id);

            }
            return roleDB;
        }

        //DEVUELVE TODO LO QUE CONTIENE LA TABLA
        public static async Task<List<RoleEN>> GetAllAsync()
        {
            var roles = new List<RoleEN>();
            using (var dbContext = new ContextDB())
            {
                roles = await dbContext.Role.ToListAsync();
            }
            return roles;
        }
        internal static IQueryable<RoleEN> QuerySelect(IQueryable<RoleEN> query, RoleEN role)
        {
            if (role.Id > 0)
                query = query.Where(r => r.Id == role.Id);

            if (!string.IsNullOrWhiteSpace(role.Name))
                query = query.Where(r => r.Name.Contains(role.Name));

            query = query.OrderByDescending(r => r.Id).AsQueryable();

            if (role.Top_Aux > 0)
                query = query.Take(role.Top_Aux).AsQueryable();

            return query;
        }

        public static async Task<List<RoleEN>> SearchAsync(RoleEN role)
        {
            var roles = new List<RoleEN>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Role.AsQueryable();
                select = QuerySelect(select, role);
                roles = await select.ToListAsync();
            }
            return roles;
        }
    }
}
