using AutoMapper;
using CMS.Core.Constants;
using CMS.Core.Dtos;
using CMS.Core.Exceptions;
using CMS.Core.ViewModels;
using CMS.Data;
using CMS.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly CMSDbContext _db;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        public UserService(CMSDbContext db, IMapper mapper, UserManager<User> userManager, IFileService fileService, IEmailService emailService)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _fileService = fileService;
            _emailService = emailService;
        }

        public async Task<ResponseDto> GetAll(Pagination pagination, Core.Dtos.Query query)
        {
            // Without Using Data Tabel
            //var users = await _db.Users.Where(x => !x.IsDelete).ToListAsync();
            //return _mapper.Map<List<UserViewModel>>(users);

            // With Using Data Tabel Concept
            var queryString = _db.Users.Where(x => !x.IsDelete && (x.FullName.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch) || x.Email.Contains(query.GeneralSearch) || x.PhoneNumber.Contains(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var users = _mapper.Map<List<UserViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = users,
                meta = new Meta
                {
                    page = pagination.Page,
                    perpage = pagination.PerPage,
                    pages = pages,
                    total = dataCount,
                }
            };
            return result;
        }
        public async Task<UpdateUserDto> Get(string Id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateUserDto>(user);
        }
        public async Task<string> Create(CreateUserDto dto)
        {
            var emailOrPhoneIsExist = _db.Users.Any(x => !x.IsDelete && (x.Email == dto.Email || x.PhoneNumber == dto.PhoneNumber));
            if (emailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }

            var emailHaveBeenUsed = _db.Users.Any(x => x.IsDelete);
            if (emailHaveBeenUsed)
            {
                throw new EmailHaveBeenUsedException();
            }

            var user = _mapper.Map<User>(dto);
            user.UserName = dto.Email;
            user.CreatedAt = DateTime.Now;

            if (dto.Image != null)
            {
                user.ImageUrl = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }

            var password = GeneratePassword();

            try
            {
                var result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    throw new OperationFailedException();
                }

            }
            catch (Exception e)
            {

            }

            await _emailService.Send(user.Email, "New Account !", $"Username is: {user.Email} and Password is {password}");

            return user.Id;
        }

        public async Task<string> Update(UpdateUserDto dto)
        {
            var emailOrPhoneIsExist = _db.Users.Any(x => !x.IsDelete && (x.Email == dto.Email || x.PhoneNumber == dto.PhoneNumber) && x.Id != dto.Id);
            if (emailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }

            var user = await _db.Users.FindAsync(dto.Id);
            var updatedUser = _mapper.Map<UpdateUserDto, User>(dto, user);

            if (dto.Image != null)
            {
                updatedUser.ImageUrl = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }

            _db.Users.Update(updatedUser);
            await _db.SaveChangesAsync();

            return user.Id;
        }
        public async Task<string> Delete(string Id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            user.IsDelete = true;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return user.Id;
        }

        private string GeneratePassword()
        {
            return Guid.NewGuid().ToString().Substring(1, 8);
        }
    }
}
