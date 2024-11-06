using Microsoft.EntityFrameworkCore;
using Music.Market.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Market.Data.Repositories
{
    public class MusicRepository : Repository<Core.Models.Music>,IMusicRepository
    {
        public MusicRepository(MusicMarketDbContext context):base(context)
        {

        }

        private MusicMarketDbContext MusicMarketDbContext
        {
            get { return Context as MusicMarketDbContext; }
        }

        public async Task<IEnumerable<Core.Models.Music>> GetAllWithArtistAsync()
        {
            return await MusicMarketDbContext.Musics
                .Include(x => x.Artist)
                .ToListAsync();
        }

        public async Task<IEnumerable<Core.Models.Music>> GetAllWithArtistByArtistIdAsync(int artistID)
        {
            return await MusicMarketDbContext.Musics
                .Include(x => x.Artist)
                .Where(x => x.ArtistId == artistID)
                .ToListAsync();
        }

        public async Task<Core.Models.Music> GetWithArtistByIdAsync(int id)
        {
            return await MusicMarketDbContext.Musics
                .Include(x => x.Artist)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
