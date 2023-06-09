﻿using AutoMapper;
using CMS.Data.Models;
using CMS.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Core.Constants;
using CMS.Core.Dtos;
using CMS.Core.Enums;
using CMS.Core.Exceptions;
using CMS.Core.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Services.Tracks
{
    public class TrackService : ITrackService
    {
        private readonly CMSDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public TrackService(CMSDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<ResponseDto> GetAll(Pagination pagination, Core.Dtos.Query query)
        {
            var queryString = _db.Tracks.Include(x => x.PublishedBy).Include(x => x.Category).Where(x => !x.IsDelete).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var tracks = _mapper.Map<List<TrackViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = tracks,
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
        public async Task<int> Create(CreateTrackDto dto)
        {
            var track = _mapper.Map<Track>(dto);
            if (dto.Track != null)
            {
                track.TrackUrl = await _fileService.SaveFile(dto.Track, FolderNames.TracksFolder);
            }

            await _db.Tracks.AddAsync(track);
            await _db.SaveChangesAsync();
            return track.Id;
        }
        public async Task<UpdateTrackDto> Get(int id)
        {
            var track = await _db.Tracks.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (track == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateTrackDto>(track);
        }
        public async Task<int> Update(UpdateTrackDto dto)
        {
            var track = await _db.Tracks.SingleOrDefaultAsync(x => x.Id == dto.Id && !x.IsDelete);
            if (track == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedTrack = _mapper.Map(dto, track);

            if (dto.Track != null)
            {
                updatedTrack.TrackUrl = await _fileService.SaveFile(dto.Track, FolderNames.TracksFolder);
            }

            track.UpdatedAt = DateTime.Now;

            _db.Tracks.Update(updatedTrack);
            await _db.SaveChangesAsync();

            return track.Id;
        }
        public async Task<int> Delete(int id)
        {
            var track = await _db.Tracks.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (track == null)
            {
                throw new EntityNotFoundException();
            }

            track.IsDelete = true;
            _db.Tracks.Update(track);
            await _db.SaveChangesAsync();

            return track.Id;
        }
    }
}
