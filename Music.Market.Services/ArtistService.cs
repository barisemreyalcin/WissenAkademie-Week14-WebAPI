using Music.Market.Core;
using Music.Market.Core.Models;
using Music.Market.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Market.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork unitofwork;

        public ArtistService(IUnitOfWork _unitofwork)
        {
            this.unitofwork = _unitofwork;
        }

        public async Task<Artist> CreateArtist(Artist newArtist)
        {
            await unitofwork.Artists.AddAsync(newArtist);
            unitofwork.CommitAsync();
            return newArtist;
        }

        public async Task DeleteArtist(Artist artist)
        {
            unitofwork.Artists.Remove(artist);
            await unitofwork.CommitAsync();
        }

        public async Task<IEnumerable<Artist>> GetAllArtists()
        {
            return await unitofwork.Artists.GetAllAsync();
        }

        public async Task<Artist> GetArtistById(int id)
        {
            return await unitofwork.Artists.GetByIdAsync(id);
        }

        public async Task UpdateArtist(Artist artistToBeUpdated, Artist artist)
        {
            artistToBeUpdated.Name = artist.Name;
            await unitofwork.CommitAsync();
        }
    }
}
