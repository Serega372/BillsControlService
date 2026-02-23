using BillsControl.ApplicationCore.Abstract;
using BillsControl.ApplicationCore.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillsControl.Api.Controllers
{
    [ApiController]
    [Route("api/residents")]
    public class ResidentsController(IResidentsService residentsService) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Resident")]
        public async Task<ActionResult<List<ResidentsResponse>>> GetFilteredResidents(
            [FromQuery] ResidentsQueryFilterParams residentsQueryFilterParams)
        {
            var filteredResult = await residentsService.GetFiltered(residentsQueryFilterParams);
            return filteredResult;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ResidentsResponse>> GetByResidentId(Guid id)
        {
            var resident = await residentsService.GetByResidentId(id);
            return Ok(resident);
        }

        [HttpPost]
        public async Task<IActionResult> AddResident([FromBody] AddResidentRequest request)
        {
            var residentId = await residentsService.AddResident(request);
            return Ok(residentId);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateResident(
            Guid id, 
            [FromBody] UpdateResidentRequest request)
        {
            var residentId = await residentsService.UpdateResident(id, request);
            return Ok(residentId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteResident(Guid id)
        {
            var residentId = await residentsService.DeleteResident(id);
            return Ok(residentId);
        }
    }
}
