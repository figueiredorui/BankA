using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MerchantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Application.UseCases.Merchants.ListMerchants.ListMerchantsResponse>> GetList([FromQuery] Application.UseCases.Merchants.ListMerchants.ListMerchantsRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application.UseCases.Merchants.GetMerchant.GetMerchantResponse>> Get(int id)
        {
            var request = new Application.UseCases.Merchants.GetMerchant.GetMerchantRequest(id);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Application.UseCases.Merchants.CreateMerchant.CreateMerchantRequest request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtRoute(null, new { Id = result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Application.UseCases.Merchants.UpdateMerchant.UpdateMerchantRequest request)
        {
            request.MerchantId = id;
            var result = await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var request = new Application.UseCases.Merchants.DeleteMerchant.DeleteMerchantRequest(id);
            var result = await _mediator.Send(request);
            return Ok();
        }
    }
}