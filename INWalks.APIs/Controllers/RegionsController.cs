using INWalks.API.Data;
using INWalks.API.Models.DTO;
using INWalks.APIs.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INWalks.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly INWalksDbContext _dbContext;

        public RegionsController(INWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        //GET ALL REGIONS
        //https://localhost:8080/api/Regions/GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            //1.Get data from Database
            var regions = _dbContext.Regions.ToList();

            //2. Map Domain model to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }


            //3. Return the DTOs.
            return Ok(regionsDto);
        }

        //GET SINGLE REGION BY ID
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            var regionDto = new RegionDto();
            if (region == null)
            {
                return NotFound();
            }

            regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(region);

        }

        //CREATE REGION
        //https://localhost:8080/api/regions/Create
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            if (addRegionRequestDto == null)
                return BadRequest();

            //Create Domain model from Dto
            var regionDomainModel = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //Use Domain model to create region.
            _dbContext.Regions.Add(regionDomainModel);
            _dbContext.SaveChanges();

            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);

        }

        //UPDATE REGION
        [HttpPut("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Load domain model
            var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
                return NotFound();

            region.Code = updateRegionRequestDto.Code;
            region.Name = updateRegionRequestDto.Name;
            region.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            _dbContext.SaveChanges();

            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);

        }

        //DELETE REGION
        [HttpDelete("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
                return NotFound();

            //Delete from db
            _dbContext.Regions.Remove(region);
            _dbContext.SaveChanges();

            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
