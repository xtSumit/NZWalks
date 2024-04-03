using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;
using NZWalks.Models.DTO;
using NZWalks.Repositories;
using System.Text.Json;

namespace NZWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext,
            IRegionRepository regionRepository,
            IMapper mapper,
            ILogger<RegionsController> logger) 
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                logger.LogInformation("GetAllRegions action method was invoked");

                var regions = await regionRepository.GetAllAsync();

                logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regions)} ");


                //Mapping Regions Domain Model to Region DTO
                var regionsDto = mapper.Map<List<RegionDto>>(regions);

                //var regionsDto = new List<RegionDto>();

                //foreach (var region in regions)
                //{
                //    regionsDto.Add(
                //        new RegionDto()
                //    {
                //            Id = region.Id,
                //            Name = region.Name,
                //            Code = region.Code,
                //            RegionImageUrl = region.RegionImageUrl
                //    });
                //}


                return Ok(regionsDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
            
        }
        
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) 
        {
            var region = await regionRepository.GetByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            //var regionDTO = new RegionDto()
            //{
            //    Id=region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }

        [HttpPost]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            if(ModelState.IsValid)
            {
                //var region = new Region()
                //{
                //    Name = addRegionRequestDto.Name,
                //    Code = addRegionRequestDto.Code,
                //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
                //};
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //var regionDto = new RegionDto()
                //{
                //    Id = region.Id,
                //    Name = region.Name,
                //    Code = region.Code,
                //    RegionImageUrl = region.RegionImageUrl
                //};

                var regionDto = mapper.Map<AddRegionRequestDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
            }
            else
            {
                return BadRequest();
            }
            
        }
        
        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            if (ModelState.IsValid)
            {
                var region = mapper.Map<Region>(updateRegionRequestDto);

                var regionDomainModel = await regionRepository.UpdateAsync(id, region);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                var regionDto = mapper.Map<UpdateRegionRequestDto>(region);
                return Ok(regionDto);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(region);
            return Ok(regionDto);

        }





    }
}
