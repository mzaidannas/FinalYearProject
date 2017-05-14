using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FinalYearProject.Models;

namespace FinalYearProject.Photo
{
    public interface IPhotoManager
    {
        Task<IEnumerable<PhotoViewModel>> Get();
        Task<PhotoActionResult> Delete(string fileName);
        Task<IEnumerable<PhotoViewModel>> Add(HttpRequestMessage request);
        bool FileExists(string fileName);
    }
}