using CapaEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CapaDAL
{
    public class ImageDAL
    {
        public static async Task<int> CreateAsync(ImageEN image)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                dbContext.Add(image);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> UpdateAsync(ImageEN image)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var imageDB = await dbContext.ImageHabitacion.FirstOrDefaultAsync(s => s.Id == image.Id);
                if (imageDB != null)
                {
                    imageDB.IdHabitacion = image.IdHabitacion;
                    imageDB.UrlImage = image.UrlImage;
                    dbContext.Update(imageDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<int> DeleteAsync(ImageEN image)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var imageDB = await dbContext.ImageHabitacion.FirstOrDefaultAsync(s => s.Id == image.Id);
                if (imageDB != null)
                {
                    dbContext.ImageHabitacion.Remove(imageDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<ImageEN> GetByIdAsync(ImageEN image)
        {
            var adImageDB = new ImageEN();
            using (var dbContext = new  ContextDB())
            {
                adImageDB = await dbContext.ImageHabitacion.FirstOrDefaultAsync(s => s.Id == image.Id);
            }
            return adImageDB!;
        }

        public static async Task<List<ImageEN>> GetAllAsync()
        {
            var images = new List<ImageEN>();
            using (var dbContext = new  ContextDB())
            {
                images = await dbContext.ImageHabitacion.ToListAsync();
            }
            return images;
        }
        internal static IQueryable<ImageEN> QuerySelect(IQueryable<ImageEN> query, ImageEN image)
        {
            if (image.Id > 0)
                query = query.Where(s => s.Id == image.Id);

            if (image.IdHabitacion > 0)
                query = query.Where(s => s.IdHabitacion == image.IdHabitacion);

            if (!string.IsNullOrWhiteSpace(image.UrlImage))
                query = query.Where(s => s.UrlImage.Contains(image.UrlImage));

            query = query.OrderByDescending(s => s.Id).AsQueryable();

            if (image.Top_Aux > 0)
                query = query.Take(image.Top_Aux).AsQueryable();
            return query;
        }

        public static async Task<List<ImageEN>> SearchAsync(ImageEN image)
        {
            var images = new List<ImageEN>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.ImageHabitacion.AsQueryable();
                select = QuerySelect(select, image);
                images = await select.ToListAsync();
            }
            return images;
        }

        public static async Task<List<ImageEN>> SearchIncludeAdAsync(ImageEN Image)
        {
            var images = new List<ImageEN>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.ImageHabitacion.AsQueryable();
                select = QuerySelect(select, Image).Include(s => s.Habitacion).AsQueryable();
                images = await select.ToListAsync();
            }
            return images;
        }
    }
}
