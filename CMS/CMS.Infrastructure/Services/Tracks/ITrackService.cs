using CMS.Core.Dtos;

namespace CMS.Infrastructure.Services.Tracks
{
    public interface ITrackService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Core.Dtos.Query query);
        Task<int> Create(CreateTrackDto dto);
        Task<UpdateTrackDto> Get(int id);
        Task<int> Update(UpdateTrackDto dto);
        Task<int> Delete(int id);
    }
}
