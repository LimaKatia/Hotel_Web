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
    public class ImageSalonDAL
    {
        public static async Task<int> CreateAsync(ImagenSalonEN image)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                dbContext.Add(image);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> UpdateAsync(ImagenSalonEN image)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var imageDB = await dbContext.ImageSalon.FirstOrDefaultAsync(s => s.Id == image.Id);
                if (imageDB != null)
                {
                    imageDB.IdSalon = image.IdSalon;
                    imageDB.UrlImage = image.UrlImage;
                    dbContext.Update(imageDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<int> DeleteAsync(ImagenSalonEN image)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var imageDB = await dbContext.ImageSalon.FirstOrDefaultAsync(s => s.Id == image.Id);
                if (imageDB != null)
                {
                    dbContext.ImageSalon.Remove(imageDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<ImagenSalonEN> GetByIdAsync(ImagenSalonEN image)
        {
            var adImageDB = new ImagenSalonEN();
            using (var dbContext = new ContextDB())
            {
                adImageDB = await dbContext.ImageSalon.FirstOrDefaultAsync(s => s.Id == image.Id);
            }
            return adImageDB!;
        }

        public static async Task<List<ImagenSalonEN>> GetAllAsync()
        {
            var images = new List<ImagenSalonEN>();
            using (var dbContext = new ContextDB())
            {
                images = await dbContext.ImageSalon.ToListAsync();
            }
            return images;
        }
        internal static IQueryable<ImagenSalonEN> QuerySelect(IQueryable<ImagenSalonEN> query, ImagenSalonEN image)
        {
            if (image.Id > 0)
                query = query.Where(s => s.Id == image.Id);

            if (image.IdSalon > 0)
                query = query.Where(s => s.IdSalon == image.IdSalon);

            if (!string.IsNullOrWhiteSpace(image.UrlImage))
                query = query.Where(s => s.UrlImage.Contains(image.UrlImage));

            query = query.OrderByDescending(s => s.Id).AsQueryable();

            if (image.Top_Aux > 0)
                query = query.Take(image.Top_Aux).AsQueryable();
            return query;
        }

        public static async Task<List<ImagenSalonEN>> SearchAsync(ImagenSalonEN image)
        {
            var images = new List<ImagenSalonEN>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.ImageSalon.AsQueryable();
                select = QuerySelect(select, image);
                images = await select.ToListAsync();
            }
            return images;
        }

        public static async Task<List<ImagenSalonEN>> SearchIncludeAdAsync(ImagenSalonEN Image)
        {
            var images = new List<ImagenSalonEN>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.ImageSalon.AsQueryable();
                select = QuerySelect(select, Image).Include(s => s.Salon).AsQueryable();
                images = await select.ToListAsync();
            }
            return images;
        }
    }
}
