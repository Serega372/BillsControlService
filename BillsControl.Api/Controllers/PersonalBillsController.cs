using BillsControl.Core;
using Microsoft.AspNetCore.Mvc;

namespace BillsControl.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonalBillsController : ControllerBase
    {
        private readonly IPersonalBillsService _personalBillsService;

        public PersonalBillsController(IPersonalBillsService personalBillsService)
        {
            _personalBillsService = personalBillsService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<PersonalBillsResponse>>> GetAll()
        {
            var bills = await _personalBillsService.GetAll();

            return Ok(bills);
        }

        [HttpGet("withResidents")]
        public async Task<ActionResult<List<PersonalBillsResponse>>> GetOnlyWithResidents()
        {
            var bills = await _personalBillsService.GetOnlyWithResidents();

            return Ok(bills);
        }

        [HttpGet("byId/{id:guid}")]
        public async Task<ActionResult> GetByBillId(Guid id)
        {
            var bills = await _personalBillsService.GetByBillId(id);

            return Ok(bills);
        }

        [HttpGet("byAddress/{address:alpha}")]
        public async Task<ActionResult> GetByAddress(string address)
        {
            var bills = await _personalBillsService.GetByAddress(address);

            return Ok(bills);
        }

        [HttpGet("byNumber/{number:alpha}")]
        public async Task<ActionResult> GetByBillNumber(string number)
        {
            var bills = await _personalBillsService.GetByBillNumber(number);

            return Ok(bills);
        }

        [HttpGet("byOpenDate/{openDate:datetime}")]
        public async Task<ActionResult<List<PersonalBillsResponse>>> GetByOpenDate(DateTime openDate)
        {
            var bills = await _personalBillsService.GetByOpenDate(openDate);

            return Ok(bills);
        }

        [HttpGet("byPage/{page:int}:{pageSize:int}")]
        public async Task<ActionResult<List<PersonalBillsResponse>>> GetByPage(int page, int pageSize)
        {
            var bills = await _personalBillsService.GetByPage(page, pageSize);

            return Ok(bills);
        }

        [HttpPost]
        public async Task<ActionResult> AddBill([FromBody] AddPersonalBillRequest request)
        {
            await _personalBillsService.AddBill(request.Id, request.BillNumber, request.Address,
                request.PlaceArea, request.Residents);

            return Ok();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateBill(Guid id, [FromBody] UpdatePersonalBillRequest request)
        {
            await _personalBillsService.UpdateBill(id, request.BillNumber,
                request.Address, request.PlaceArea);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteBill(Guid id)
        {
            await _personalBillsService.DeleteBill(id);

            return Ok();
        }
    }
}
