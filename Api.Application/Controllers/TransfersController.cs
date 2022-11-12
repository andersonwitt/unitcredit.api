using Api.Domain.DTOs;
using Api.Domain.Enums;
using Api.Domain.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [Authorize("Bearer")]
    public class TransfersController : ControllerBase
    {
        private readonly IClaimsManager _claimsManager;
        private readonly ITransferManager _transferManager;
        public TransfersController(IClaimsManager claimsManager, ITransferManager transferManager)
        {
            _claimsManager = claimsManager;
            _transferManager = transferManager;
        }

        [HttpPost]
        public async Task<IActionResult> TransferToUser([FromBody]TransferToUserRequestDTO request)
        {
            var userSession = _claimsManager.GetUserSession(this?.HttpContext?.User);

            var payload = new TransferToUserPayloadDTO()
            {
                ToId = request.ToId,
                Total = request.Total,
                FromId = userSession.UserId,
                TransactionType = EnumTransactionType.Transfer,
            };

            var result = await _transferManager.TransferToUser(payload);

            return Ok(result);
        }
    }
}