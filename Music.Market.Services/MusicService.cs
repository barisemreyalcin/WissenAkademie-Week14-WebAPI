using Music.Market.Core;
using Music.Market.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Music.Market.Services
{
    public class MusicService : IMusicService
    {
        private readonly IUnitOfWork unitOfWork;

        public MusicService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public async Task<Core.Models.Music> CreateMusic(Core.Models.Music newMusic)
        {
            await unitOfWork.Musics.AddAsync(newMusic);
            await unitOfWork.CommitAsync();
            return newMusic;
        }

        public async Task<Core.Models.Music> DeleteMusic(Core.Models.Music music)
        {
            unitOfWork.Musics.Remove(music);
            await unitOfWork.CommitAsync();
            return music;
        }

        public async Task<IEnumerable<Core.Models.Music>> GetAllWithArtist()
        {
            return await unitOfWork.Musics.GetAllWithArtistAsync();
        }

        public Task<IEnumerable<Core.Models.Music>> GetMusicByArtistId(int artistId)
        {
            return unitOfWork.Musics.GetAllWithArtistByArtistIdAsync(artistId);
        }

        public async Task<Core.Models.Music> GetMusicById(int id)
        {
            return await unitOfWork.Musics.GetWithArtistByIdAsync(id);
        }

        public async Task<Core.Models.Music> UpdateMusic(Core.Models.Music musicToBeUpdated, Core.Models.Music music)
        {
            musicToBeUpdated.Name = music.Name;
            musicToBeUpdated.ArtistId = music.ArtistId;
            await unitOfWork.CommitAsync();
            return await unitOfWork.Musics.GetByIdAsync(music.Id);
        }
    }
}
