using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.CustomActionFilters;
using Web_API.Models.Domain;
using Web_API.Models.DTO;
using Web_API.Repositories;

namespace Web_API.Controllers
{
    //api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // Get all walks
        // GET: http://localhost:port/api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true
        // &pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery
            , [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            //Get Data from database
            var walksDomain = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            //Create an exception
            //throw new Exception("This is a new exception");
            var walkDto = mapper.Map<List<WalkDto>>(walksDomain);
            return Ok(walkDto);

        }


        //GET walk by id
        // GET: http://localhost:port/api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.GetByIdAsync(id);
            if (walkDomain == null)
                return NotFound();

            // Map Region Domain model to Region Dto
            var regionDto = mapper.Map<WalkDto>(walkDomain);
            return Ok(regionDto);
        }


        // Create Walks
        // POST: /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map DTO to domain model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);
            //Map domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }


        // Update Walk
        // PUT: http://localhost:port/api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            //Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
                return NotFound();

            //Convert domain model o dto
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        // Delete Walk
        // DELETE: http://localhost:port/api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteAsync(id);

            if (walkDomainModel == null)
                return NotFound();

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}
