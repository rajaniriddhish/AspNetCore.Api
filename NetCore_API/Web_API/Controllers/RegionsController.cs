using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API.Data;
using Web_API.Models.Domain;
using Web_API.Models.DTO;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        public RegionsController(NZWalksDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        // Get all regions
        // GET: http://localhost:port/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //var regions = new List<Region> { 
            //    new Region { 
            //        Id = Guid.NewGuid(),
            //        Name = "Auckland Region",
            //        Code = "AKL",
            //        RegionImageUrl = "https://images.freeimages.com/variants/i6YmGLtHwW66p5byh1JRqUdX/f4a36f6589a0e50e702740b15352bc00e4bfaf6f58bd4db850e167794d05993d"
            //    },
            //    new Region {
            //        Id = Guid.NewGuid(),
            //        Name = "Wellington Region",
            //        Code = "WLG",
            //        RegionImageUrl = "https://images.freeimages.com/variants/i6YmGLtHwW66p5byh1JRqUdX/f4a36f6589a0e50e702740b15352bc00e4bfaf6f58bd4db850e167794d05993d"
            //    }
            //};

            //Get Data from database
            var regionsDomain= _dbContext.Regions.ToList();

            //Map Domain models to DTOs
            var regionDto = new List<RegionDto>();
            foreach (var region in regionsDomain)
            {
                regionDto.Add(new RegionDto()
                {
                    Id= region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl= region.RegionImageUrl
                });
            }

            return Ok(regionDto);
        }

        // Get region by id
        // GET: http://localhost:port/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var regionDomain = _dbContext.Regions.Find(id);
            if(regionDomain == null)
                return NotFound();

            // Map Region Domain model to Region Dto
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return Ok(regionDto);
        }

        // POST To Create New Region
        // POST: http://localhost:port/api/regions
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequest)
        {
            //Map DTO to Domain Model
            var regionDomainModel = new Region
            {
                Name = addRegionRequest.Name,
                Code = addRegionRequest.Code,
                RegionImageUrl = addRegionRequest.RegionImageUrl
            };

            //Use Domain Model to Create Region
            _dbContext.Regions.Add(regionDomainModel);
            _dbContext.SaveChanges();

            //Map Domain model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            
        }

        // Update Regino
        // PUT: http://localhost:port/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute]Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomainModel == null)
                return NotFound();

            //Map DTO to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            _dbContext.SaveChanges();

            //Convert domain model o dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }


        // Delete Region
        // DELETE: http://localhost:port/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
                return NotFound();

            //Delete Region
            _dbContext.Regions.Remove(regionDomainModel);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
