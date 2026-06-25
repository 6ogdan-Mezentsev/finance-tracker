using Microsoft.AspNetCore.Mvc;
using TransactionService.Api.Dtos;
using TransactionService.Api.Models;
using TransactionService.Api.Services;

namespace TransactionService.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<ActionResult<TransactionResponse>> Create(CreateTransactionRequest request)
    {
        try
        {
            var transaction = await _transactionService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = transaction.Id, userId = transaction.UserId }, transaction);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<TransactionResponse>>> GetByUser(
        [FromQuery] int userId,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] TransactionType? type,
        [FromQuery] int? categoryId)
    {
        try
        {
            return await _transactionService.GetByUserAsync(userId, from, to, type, categoryId);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TransactionResponse>> GetById(int id, [FromQuery] int userId)
    {
        try
        {
            var transaction = await _transactionService.GetByIdAsync(id, userId);
            return transaction == null ? NotFound() : transaction;
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TransactionResponse>> Update(int id, UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await _transactionService.UpdateAsync(id, request);
            return transaction == null ? NotFound() : transaction;
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
    {
        try
        {
            var deleted = await _transactionService.DeleteAsync(id, userId);
            return deleted ? NoContent() : NotFound();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
