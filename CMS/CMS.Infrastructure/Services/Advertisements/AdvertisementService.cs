using AutoMapper;
using CMS.Core.Constants;
using CMS.Core.Dtos;
using CMS.Core.Enums;
using CMS.Core.Exceptions;
using CMS.Core.ViewModels;
using CMS.Data;
using CMS.Data.Models;
using CMS.Infrastructure.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Services.Advertisements
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly CMSDbContext _db;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public AdvertisementService(CMSDbContext db, IMapper mapper, IUserService userService, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<List<UserViewModel>> GetAdvertisementOwners()
        {
            var users = await _db.Users.Where(x => !x.IsDelete && x.UserType == UserType.AdvertisementOwner).ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }
        public async Task<ResponseDto> GetAll(Pagination pagination, Core.Dtos.Query query)
        {
            var queryString = _db.Advertisements.Include(x => x.Owner).Where(x => !x.IsDelete).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var advertisements = _mapper.Map<List<AdvertisementViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = advertisements,
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
        public async Task<int> Create(CreateAdvertisementDto dto)
        {
            if (dto.StartDate >= dto.EndDate)
            {
                throw new InvalidDateException();
            }

            var advertisements = _mapper.Map<Advertisement>(dto);
            if (dto.Image != null)
            {
                advertisements.ImageUrl = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }

            // refactor the code from chatgpt
            if (string.IsNullOrWhiteSpace(dto.OwnerId))
            {
                var ownerIsExist = _db.Users.Any(x => !x.IsDelete && x.UserType == UserType.AdvertisementOwner && (x.Email == dto.Owner.Email || x.PhoneNumber == dto.Owner.PhoneNumber));
                if (ownerIsExist)
                {
                    throw new DuplicateOwnerException();
                }

                var userId = await _userService.Create(dto.Owner);
                advertisements.OwnerId = userId;
            }
            else
            {
                var ownerExists = await _db.Users.AnyAsync(x => x.Id == dto.OwnerId && !x.IsDelete);
                if (!ownerExists)
                {
                    throw new InvalidDataException();
                }

                advertisements.OwnerId = dto.OwnerId;
            }

            await _db.Advertisements.AddAsync(advertisements);
            await _db.SaveChangesAsync();

            return advertisements.Id;

            // the primary code

            //if (!string.IsNullOrWhiteSpace(dto.OwnerId))
            //{
            //    advertisements.OwnerId = dto.OwnerId;
            //}

            //When I want to add an advertisement first and then add the owner of the advertisement
            //await _db.Advertisements.AddAsync(advertisements);
            //await _db.SaveChangesAsync();

            //if (advertisements.OwnerId == null)
            //{
            //    // I can remove it because this exception is on creating a new user, but the message is different
            //    var ownerIsExist = _db.Users.Any(x => !x.IsDelete && x.UserType == UserType.AdvertisementOwner && (x.Email == dto.Owner.Email || x.PhoneNumber == dto.Owner.PhoneNumber));
            //    if (ownerIsExist)
            //    {
            //        throw new DuplicateOwnerException();
            //    }

            //    var userId = await _userService.Create(dto.Owner);
            //    advertisements.OwnerId = userId;

            //    // When I want to add an advertisement first and then add the owner of the advertisement

            //    //_db.Advertisements.Update(advertisements);
            //    //await _db.SaveChangesAsync();
            //}
            //if (!dto.Owner.PhoneNumber.StartsWith("059"))
            //{
            //    throw new Exception("رقم الجوال يجب أن يبدأ ب 059");
            //}

            //await _db.Advertisements.AddAsync(advertisements);
            //await _db.SaveChangesAsync();

            //return advertisements.Id;


        }
        public async Task<UpdateAdvertisementDto> Get(int id)
        {
            var advertisements = await _db.Advertisements.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (advertisements == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateAdvertisementDto>(advertisements);
        }
        public async Task<int> Update(UpdateAdvertisementDto dto)
        {
            if (dto.StartDate >= dto.EndDate)
            {
                throw new InvalidDateException();
            }

            var advertisement = await _db.Advertisements.SingleOrDefaultAsync(x => x.Id == dto.Id && !x.IsDelete);
            if (advertisement == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedAdvertisement = _mapper.Map(dto, advertisement);

            if (dto.Image != null)
            {
                updatedAdvertisement.ImageUrl = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }

            advertisement.UpdatedAt = DateTime.Now;

             _db.Advertisements.Update(updatedAdvertisement);
            await _db.SaveChangesAsync();

            return advertisement.Id;
        }
        public async Task<int> Delete(int id)
        {
            var advertisements = await _db.Advertisements.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (advertisements == null)
            {
                throw new EntityNotFoundException();
            }

            advertisements.IsDelete = true;
            _db.Advertisements.Update(advertisements);
            await _db.SaveChangesAsync();

            return advertisements.Id;
        }
    }
}
