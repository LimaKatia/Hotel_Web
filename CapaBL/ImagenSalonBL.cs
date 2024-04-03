using CapaDAL;
using CapaEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBL
{
    public class ImagenSalonBL
    {
        public async Task<int> CreateAsync(ImagenSalonEN adImage)
        {
            return await ImageSalonDAL.CreateAsync(adImage);
        }

        public async Task<int> UpdateAsync(ImagenSalonEN adImage)
        {
            return await ImageSalonDAL.UpdateAsync(adImage);
        }

        public async Task<int> DeleteAsync(ImagenSalonEN adImage)
        {
            return await ImageSalonDAL.DeleteAsync(adImage);
        }

        public async Task<ImagenSalonEN> GetByIdAsync(ImagenSalonEN adImage)
        {
            return await ImageSalonDAL.GetByIdAsync(adImage);
        }

        public async Task<List<ImagenSalonEN>> GetAllAsync()
        {
            return await ImageSalonDAL.GetAllAsync();
        }

        public async Task<List<ImagenSalonEN>> SearchAsync(ImagenSalonEN adImage)
        {
            return await ImageSalonDAL.SearchAsync(adImage);
        }

        public async Task<List<ImagenSalonEN>> SearchIncludeAdAsync(ImagenSalonEN adImage)
        {
            return await ImageSalonDAL.SearchIncludeAdAsync(adImage);
        }
    }
}
