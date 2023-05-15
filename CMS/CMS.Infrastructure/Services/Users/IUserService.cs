using CMS.Core.Dtos;

namespace CMS.Infrastructure.Services.Users
{
    public interface IUserService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Core.Dtos.Query query);
        Task<UpdateUserDto> Get(string Id);
        Task<string> Create(CreateUserDto dto);
        Task<string> Update(UpdateUserDto dto);
        Task<string> Delete(string Id);
    }
}
