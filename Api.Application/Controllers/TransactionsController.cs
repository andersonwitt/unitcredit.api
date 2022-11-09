using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _transactionService.Get();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}", Name = "GetTransactionWithId")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _transactionService.Get(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransactionDTO payload)
        {
            var result = await _transactionService.Post(payload);

            if (result == null) return BadRequest();

            return Created(new Uri(Url.Link("GetTransactionWithId", new { id = result.Id })), result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TransactionDTO payload)
        {
            var result = await _transactionService.Put(payload);

            if (result == null) return BadRequest();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _transactionService.Delete(id);

            return Ok(result);
        }
    }
}