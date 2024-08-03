using AutoMapper;
using INWalks.APIs.Models.Domain;
using INWalks.APIs.Models.DTO;
using INWalks.APIs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INWalks.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

   

        //CREATE WALKS
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map Dto to domain model
            var walk = mapper.Map<Walk>(addWalkRequestDto);

            //add to database
            var walkDomainModel = await walkRepository.CreateAsync(walk);

            //Map domain model to Dto
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }

        //GETALL Walks
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //Load from database
            var walkDomainModelList = await walkRepository.GetAllAsync();

            //Map domain model to Dto
            var listWalkDto = mapper.Map<List<WalkDto>>(walkDomainModelList);

            return Ok(listWalkDto);

        }



    }
}
