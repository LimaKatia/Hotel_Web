using CapaDAL;
using CapaEN;
using CapaDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CapaBL
{
    public class ImageBL
    {
        public async Task<int> CreateAsync(ImageEN adImage)
        {
            return await ImageDAL.CreateAsync(adImage);
        }

        public async Task<int> UpdateAsync(ImageEN adImage)
        {
            return await ImageDAL.UpdateAsync(adImage);
        }

        public async Task<int> DeleteAsync(ImageEN adImage)
        {
            return await ImageDAL.DeleteAsync(adImage);
        }

        public async Task<ImageEN> GetByIdAsync(ImageEN adImage)
        {
            return await ImageDAL.GetByIdAsync(adImage);
        }

        public async Task<List<ImageEN>> GetAllAsync()
        {
            return await ImageDAL.GetAllAsync();
        }

        public async Task<List<ImageEN>> SearchAsync(ImageEN adImage)
        {
            return await ImageDAL.SearchAsync(adImage);
        }

        public async Task<List<ImageEN>> SearchIncludeAdAsync(ImageEN adImage)
        {
            return await ImageDAL.SearchIncludeAdAsync(adImage);
        }
    }
}
