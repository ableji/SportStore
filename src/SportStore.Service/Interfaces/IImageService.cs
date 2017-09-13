using SportStore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Service
{
    public interface IImageService
    {
        Task<int> save(Image item);
        Task<Image> GetImage(Image img);
        Task<Image> GetImageById(int imageId);
    }
}
