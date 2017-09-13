using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SportStore.Model.Domain;
using SportStore.Data;

namespace SportStore.Service
{
    public class ImageService : IImageService
    {
        #region Initial

        private ApplicationDBContext _context;
        public ImageService(ApplicationDBContext context)
        {
            _context = context;
        }

        #endregion


        public async Task<Image> GetImage(Image img)
        {
            throw new NotImplementedException();
        }
        
        public async Task<Image> GetImageById(int imageId) =>
            await _context.Images.FindAsync(imageId);

        public async Task<int> save(Image item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }
    }
}
