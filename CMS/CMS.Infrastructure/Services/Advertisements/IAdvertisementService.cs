using CMS.Core.Dtos;
using CMS.Core.ViewModels;

namespace CMS.Infrastructure.Services.Advertisements
{
    public interface IAdvertisementService
    {
        Task<List<UserViewModel>> GetAdvertisementOwners();
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<int> Create(CreateAdvertisementDto dto);
        Task<UpdateAdvertisementDto> Get(int id);
        Task<int> Update(UpdateAdvertisementDto dto);
        Task<int> Delete(int id);
    }
}
