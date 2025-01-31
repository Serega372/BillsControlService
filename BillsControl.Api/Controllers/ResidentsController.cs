using BillsControl.Core;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace BillsControl.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResidentsController : ControllerBase
    {
        private readonly IResidentsService _residentsService;

        public ResidentsController(IResidentsService residentsService)
        {
            _residentsService = residentsService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<ResidentDto>>> GetAll()
        {
            var residents = await _residentsService.GetAll();

            return Ok(residents);
        }

        [HttpGet("byBillId/{id:guid}")]
        public async Task<ActionResult> GetByBillId(Guid id)
        {
            var residents = await _residentsService.GetByBillId(id);

            return Ok(residents);
        }

        [HttpGet("byPage/{page:int}:{pageSize:int}")]
        public async Task<ActionResult<List<ResidentDto>>> GetByPage(int page, int pageSize)
        {
            var residents = await _residentsService.GetByPage(page, pageSize);

            return Ok(residents);
        }

        [HttpGet("byId/{id:guid}")]
        public async Task<ActionResult> GetByResidentId(Guid id)
        {
            var residents = await _residentsService.GetByResidentId(id);

            return Ok(residents);
        }

        [HttpPost]
        public async Task<ActionResult> AddResident([FromBody] ResidentDto request)
        {
            await _residentsService.AddResident(request.Id, request.Name,
                request.Surname, request.PersonalBillId, request.IsOwner);

            return Ok();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateResident(Guid id, [FromBody] ResidentDto request)
        {
            await _residentsService.UpdateResident(request.Id, request.Name,
                request.Surname, request.PersonalBillId, request.IsOwner);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteResident(Guid id)
        {
            await _residentsService.DeleteResident(id);

            return Ok();
        }
    }
}
