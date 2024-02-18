using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Application.UseCases.Rules.ListRules.ListRulesResponse>> GetList([FromQuery] Application.UseCases.Rules.ListRules.ListRulesRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application.UseCases.Rules.GetRule.GetRuleResponse>> Get(int id)
        {
            var request = new Application.UseCases.Rules.GetRule.GetRuleRequest(id);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Application.UseCases.Rules.CreateRule.CreateRuleRequest request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtRoute(null, new { Id = result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Application.UseCases.Rules.UpdateRule.UpdateRuleRequest request)
        {
            request.RuleId = id;
            var result = await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var request = new Application.UseCases.Rules.DeleteRule.DeleteRuleRequest(id);
            var result = await _mediator.Send(request);
            return Ok();
        }
    }
}