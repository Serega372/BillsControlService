using BillsControl.Core.Abstract;
using BillsControl.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BillsControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalBillsController(IPersonalBillsService personalBillsService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<PersonalBillsResponse>>> GetFilteredBills(
            [FromQuery] PersonalBillsQueryFilterParams personalBillsQueryFilterParams)
        {
            var filteredResult = await personalBillsService.GetFiltered(personalBillsQueryFilterParams);
            return Ok(filteredResult);
        }

        [HttpPatch("{id:guid}/close")]
        public async Task<IActionResult> CloseBill(Guid id)
        {
            var closedBillId = await personalBillsService.CloseBill(id);
            return Ok(closedBillId);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PersonalBillsResponse>> GetByBillId(Guid id)
        {
            var bill = await personalBillsService.GetByBillId(id);
            return Ok(bill);
        }

        [HttpPost]
        public async Task<IActionResult> AddBill([FromBody] AddPersonalBillRequest request)
        {
            var billId = await personalBillsService.AddBill(request);
            return Ok(billId);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBill(
            Guid id, 
            [FromBody] UpdatePersonalBillRequest request)
        {
            var billId = await personalBillsService.UpdateBill(id, request);
            return Ok(billId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBill(Guid id)
        {
            var billId = await personalBillsService.DeleteBill(id);
            return Ok(billId);
        }
    }
}
