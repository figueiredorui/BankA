using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankA.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("file-formats")]
        public ActionResult<List<string>> GetFileFormats()
        {

            var result = Domain.Account.GetFileFormats();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Application.UseCases.Accounts.ListAccounts.ListAccountsResponse>> Get([FromQuery] Application.UseCases.Accounts.ListAccounts.ListAccountsRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application.UseCases.Accounts.GetAccount.GetAccountResponse>> Get(int id)
        {
            var request = new Application.UseCases.Accounts.GetAccount.GetAccountRequest(id);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Application.UseCases.Accounts.CreateAccount.CreateAccountRequest account)
        {
            var result = await _mediator.Send(account);
            return CreatedAtRoute(null, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Application.UseCases.Accounts.UpdateAccount.UpdateAccountRequest account)
        {
            account.AccountId = id;
            await _mediator.Send(account);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var request = new Application.UseCases.Accounts.DeleteAccount.DeleteAccountRequest(id);
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut("{id}/file-format-config")]
        public async Task<ActionResult> UpdateFileFormatConfig(int id, Application.UseCases.Accounts.FileFormatConfig.UpdateFileFormatConfigurationRequest updateFileFormatConfig)
        {
            updateFileFormatConfig.AccountId = id;
            await _mediator.Send(updateFileFormatConfig);
            return Ok();
        }
    }
}
