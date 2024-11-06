using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Market.Core.Services
{
    public interface IMusicService
    {
        Task<IEnumerable<Models.Music>> GetAllWithArtist();
        Task<Models.Music> GetMusicById(int id);
        Task<IEnumerable<Models.Music>> GetMusicByArtistId(int artistId);
        Task<Models.Music> CreateMusic(Models.Music newMusic);
        Task<Models.Music> UpdateMusic(Models.Music musicToBeUpdated, Models.Music music);
        //Task<Models.Music> UpdateMusic(int musicId, Models.Music music);
        Task<Models.Music> DeleteMusic(Models.Music music);
        //Task<Models.Music> DeleteMusic(int musicId);

    }
}
