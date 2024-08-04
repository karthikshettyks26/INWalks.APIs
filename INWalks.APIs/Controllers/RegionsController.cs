using AutoMapper;
using INWalks.API.Data;
using INWalks.API.Models.DTO;
using INWalks.APIs.CustomActionFilters;
using INWalks.APIs.Models.Domain;
using INWalks.APIs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INWalks.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly INWalksDbContext _dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(INWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //GET ALL REGIONS
        //https://localhost:8080/api/Regions/GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //1.Get data from Database
            //var regions = await _dbContext.Regions.ToListAsync();
            var regions = await regionRepository.GetAllAsync();

            //2. Map Domain model to DTOs
            #region old mapping
            //var regionsDto = new List<RegionDto>();
            //foreach (var region in regions)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}
            #endregion

            //Map domain model to DTO using automapper
            var regionsDto = mapper.Map<List<RegionDto>>(regions);

            //3. Return the DTOs.
            return Ok(regionsDto);
        }

        //GET SINGLE REGION BY ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null)
                return NotFound();

            //Map domain model to DTO
            var regionDto = mapper.Map<Region>(region);
            return Ok(regionDto);
        }

        //CREATE REGION
        //https://localhost:8080/api/regions/Create
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            if (addRegionRequestDto == null)
                return BadRequest();

            //Create Domain model from Dto
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            //Use Domain model to create region.
            await regionRepository.CreateAsync(regionDomainModel);

            //Map domain model to Dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }

        //UPDATE REGION
        [HttpPut("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map DTO to domain model.
            var region = mapper.Map<Region>(updateRegionRequestDto);

            //Check if region exists.
            var regionDomainModel = await regionRepository.UpdateAsync(id, region);

            if (regionDomainModel == null)
                return NotFound();

            //Map domain model to DTO.
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }

        //DELETE REGION
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
                return NotFound();

            //Map domain model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }
    }
}
