using AutoMapper;
using INWalks.APIs.CustomActionFilters;
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
        [ValidateModel]
        //Checks model validation using data annotations.
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //if (ModelState.IsValid)
            //{
            //Map Dto to domain model
            var walk = mapper.Map<Walk>(addWalkRequestDto);

            //add to database
            var walkDomainModel = await walkRepository.CreateAsync(walk);

            //Map domain model to Dto
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            //return CreatedAtAction(nameof(GetByIdAsync), new { id = walkDomainModel.Id }, walkDto);
            return Ok(walkDto);
            //}
            //else
            //    return BadRequest(ModelState);

        }

        //GETALL Walks
        //api/Walks/GetAll?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
        {
            //Load from database
            var walkDomainModelList = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending?? true, pageNumber, pageSize);

            //Map domain model to Dto
            var listWalkDto = mapper.Map<List<WalkDto>>(walkDomainModelList);

            return Ok(listWalkDto);

        }

        //GET WALK BY ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
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
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {

            //Map Dto to domain model
            var walk = mapper.Map<Walk>(updateWalkRequestDto);

            //Update the data
            var walkDomainModel = await walkRepository.UpdateAsync(id, walk);

            //Map domain model to Dto
            var walkDto = mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        //DELETE WALK BY ID
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Delete the record
            var walkDomainModel = await walkRepository.DeleteAsync(id);

            if (walkDomainModel == null)
                return NotFound();

            //Map domain model to Dto
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

    }
}
