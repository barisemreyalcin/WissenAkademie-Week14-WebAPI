using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Music.Market.Api.DTO;
using Music.Market.Api.Validations;
using Music.Market.Core.Models;
using Music.Market.Core.Services;

namespace Music.Market.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService artistService;
        private readonly IMapper mapper;

        public ArtistController(IArtistService _artistService, IMapper _mapper)
        {
            this.artistService = _artistService;
            this.mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetAllArtists()
        {
            var artist = await artistService.GetAllArtists();
            var artistResources = mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistDTO>>(artist);
            return Ok(artistResources);
        }

        [HttpGet("id")]
        public async Task<ActionResult<ArtistDTO>> GetArtistById(int id)
        {
            var artist = await artistService.GetArtistById(id);
            var artistResource = mapper.Map<Artist, ArtistDTO>(artist);
            return Ok(artistResource);
        }

        [HttpPost]
        public async Task<ActionResult<ArtistDTO>> CreateArtist([FromBody] SaveArtistDTO saveArtistResource)
        {
            var validator = new SaveArtistResourceValidator();
            var validationResult = await validator.ValidateAsync(saveArtistResource);

            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var artistToCreate =  mapper.Map<SaveArtistDTO, Artist>(saveArtistResource);

            var newArtist = await artistService.CreateArtist(artistToCreate);

            var artist = await artistService.GetArtistById(newArtist.Id);

            var artistResourece = mapper.Map<Artist, ArtistDTO>(artist);
            return Ok(artistResourece);
        }

        //[HttpDelete]
        //public async Task<IActionResult> DeleteArtist(int id)
        //{
        //    var artist = await artistService.GetArtistById(id);

        //    await artistService.DeleteArtist(artist);

        //    return NoContent();
        //}

        [HttpDelete]
        public async Task<ActionResult<ArtistDTO>> DeleteArtist(int id)
        {
            var artist = await artistService.GetArtistById(id);

            if (artist == null)
                return NotFound();
            await artistService.DeleteArtist(artist);

            var artistResource = mapper.Map<Artist, ArtistDTO>(artist);

            return Ok(artistResource);
        }

        [HttpPut("id")]
        public async Task<ActionResult<ArtistDTO>> UpdateArtist(int id, [FromBody]SaveArtistDTO saveArtistResource)
        {
            var validator = new SaveArtistResourceValidator();
            var validationResult = await validator.ValidateAsync(saveArtistResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var artistToBeUpdated = await artistService.GetArtistById(id);

            if (artistToBeUpdated == null)
                return NotFound();

            var artist = mapper.Map<SaveArtistDTO, Artist>(saveArtistResource);

            await artistService.UpdateArtist(artistToBeUpdated, artist);

            var updatedArtist = await artistService.GetArtistById(id);

            var updatedArtistResource = mapper.Map<Artist, ArtistDTO>(updatedArtist);
            return Ok(updatedArtistResource);

        }
    }
}
