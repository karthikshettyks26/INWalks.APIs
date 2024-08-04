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
            if (ModelState.IsValid)
            {
                //Map Dto to domain model
                var walk = mapper.Map<Walk>(addWalkRequestDto);

                //add to database
                var walkDomainModel = await walkRepository.CreateAsync(walk);

                //Map domain model to Dto
                var walkDto = mapper.Map<WalkDto>(walkDomainModel);

                //return CreatedAtAction(nameof(GetByIdAsync), new { id = walkDomainModel.Id }, walkDto);
                return Ok(walkDto);
            }
            else
                return BadRequest(ModelState);
            
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

        //GET WALK BY ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            //Load from Database
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null) 
                return NotFound();

            //Map domainmodel to Dto
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        //UPDATE WALK BY ID
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                //Map Dto to domain model
                var walk = mapper.Map<Walk>(updateWalkRequestDto);

                //Update the data
                var walkDomainModel = await walkRepository.UpdateAsync(id, walk);

                //Map domain model to Dto
                var walkDto = mapper.Map<WalkDto>(walk);

                return Ok(walkDto);
            }
            else
                return BadRequest(ModelState);
           
        }

        //DELETE WALK BY ID
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            //Delete the record
            var walkDomainModel = await walkRepository.DeleteAsync(id);

            if(walkDomainModel == null)
                return NotFound();

            //Map domain model to Dto
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

    }
}
