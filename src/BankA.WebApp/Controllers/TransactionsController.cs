using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankA.WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("accounts/balance")]
        public async Task<ActionResult<Application.UseCases.Accounts.GetAccountBalance.GetAccountBalanceResponse>> GetAccountBalance()
        {
            var request = new Application.UseCases.Accounts.GetAccountBalance.GetAccountBalanceRequest();
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("accounts/{accountId}/categorize")]
        public async Task<ActionResult> CategorizeTransactions(int accountId)
        {
            var request = new Application.UseCases.Transactions.CategorizeTransactions.CategorizeTransactionsRequest(accountId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("accounts/{accountId}/transactions")]
        public async Task<ActionResult<Application.UseCases.Transactions.SearchTransactions.ListTransactionsResponse>> GetTransactions(int accountId, [FromQuery] Application.UseCases.Transactions.SearchTransactions.ListTransactionsRequest request)
        {
            request.AccountId = accountId;
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("accounts/{accountId}/import-file")]
        public async Task<ActionResult> ImportFile(int accountId, [FromForm] IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    formFile.CopyTo(memoryStream);
                    var array = memoryStream.ToArray();

                    var request = new Application.UseCases.Transactions.ImportTransactionsFile.ImportTransactionsFileRequest
                    {
                        AccountId = accountId,
                        FileName = formFile.FileName,
                        ContentType = formFile.ContentType,
                        FileContent = array
                    };

                    await _mediator.Send(request);
                    return Ok();
                }
            }
            return BadRequest("Invalid File");

        }

        [HttpGet("accounts/{accountId}/transactions/{id}")]
        public async Task<ActionResult<Application.UseCases.Transactions.GetTransaction.GetTransactionResponse>> Get(int accountId, int id)
        {
            var request = new Application.UseCases.Transactions.GetTransaction.GetTransactionRequest(accountId, id);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("accounts/{accountId}/transactions")]
        public async Task<ActionResult> Create(int accountId, [FromBody] Application.UseCases.Transactions.CreateTransaction.CreateTransactionRequest request)
        {
            request.AccountId = accountId;
            var result = await _mediator.Send(request);
            return CreatedAtRoute(null, new { Id = result });
        }

        [HttpPut("accounts/{accountId}/transactions/{id}")]
        public async Task<ActionResult> Update(int accountId, int id, [FromBody] Application.UseCases.Transactions.UpdateTransaction.UpdateTransactionRequest request)
        {
            request.AccountId = accountId;
            request.TransactionId = id;
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("accounts/{accountId}/transactions/{id}")]
        public async Task<ActionResult> Delete(int accountId, int id)
        {
            var result = await _mediator.Send(new Application.UseCases.Transactions.DeleteTransaction.DeleteTransactionRequest(accountId, id));
            return Ok(result);
        }

    }
}
