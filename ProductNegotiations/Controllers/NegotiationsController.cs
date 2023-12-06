using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductNegotiations.API.Models;
using ProductNegotiations.Library.Helpers;
using ProductNegotiations.Library.Models;
using ProductNegotiations.Library.Services;
using System.Security.Claims;


namespace ProductNegotiations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NegotiationsController : ControllerBase
    {
        private readonly ILogger<NegotiationsController> _logger;
        private readonly INegotiaitionService _service;
        private readonly IValidator<NegotiationClientCreateModel> _validator;

        public NegotiationsController(ILogger<NegotiationsController> logger, INegotiaitionService service, IValidator<NegotiationClientCreateModel> validator)
        {
            _logger = logger;
            _service = service;
            _validator = validator;
        }

        [HttpGet("api/Negotiations/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<NegotiationClientModel>> GetNegotiationById(Guid id)
        {
            try
            {
                var data = await _service.GetNegotiationByIdAsync(id);
                return Ok(data.Adapt<NegotiationClientModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpGet("api/Negotiations/unresolved")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<NegotiationClientModel>>> GetUnresolvedNegotiations([FromQuery] PagingModel paging)
        {
            try
            {
                var data = await _service.GetUnresolvedNegotiationsAsync(paging);
                var metadata = new
                {
                    data.TotalCount,
                    data.PageSize,
                    data.CurrentPage,
                    data.TotalPages,
                    data.HasNext,
                    data.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(data.Adapt<IEnumerable<NegotiationClientModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<NegotiationClientModel>>> GetAllNegotiations([FromQuery] PagingModel paging)
        {
            try
            {
                var data = await _service.GetAllNegotiationsAsync(paging);
                var metadata = new
                {
                    data.TotalCount,
                    data.PageSize,
                    data.CurrentPage,
                    data.TotalPages,
                    data.HasNext,
                    data.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(data.Adapt<IEnumerable<NegotiationClientModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpGet("api/Negotiations/MyNegotiationsAmount")]
        [Authorize]
        public async Task<ActionResult<int?>> GetNegotiationsAmount(Guid productId)
        {
            try
            {
                string? userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userGuid = Guid.Parse(userId);
                var data = await _service.GetNegotiationsAmount(productId, userGuid);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpGet("api/Negotiations/My")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<NegotiationClientModel>>> GetMyAllNegotiations([FromQuery] PagingModel paging)
        {
            try
            {
                string? userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userGuid = Guid.Parse(userId);
                var data = await _service.GetAllNegotiationsByUserIdAsync(paging, userGuid);
                var metadata = new
                {
                    data.TotalCount,
                    data.PageSize,
                    data.CurrentPage,
                    data.TotalPages,
                    data.HasNext,
                    data.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(data.Adapt<IEnumerable<NegotiationClientModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpGet("api/Negotiations/ByUserId/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<NegotiationClientModel>>> GetAllNegotiationsByUserId(Guid userId, [FromQuery] PagingModel paging)
        {
            try
            {
                var data = await _service.GetAllNegotiationsByUserIdAsync(paging, userId);
                var metadata = new
                {
                    data.TotalCount,
                    data.PageSize,
                    data.CurrentPage,
                    data.TotalPages,
                    data.HasNext,
                    data.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(data.Adapt<IEnumerable<NegotiationClientModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpGet("api/Negotiations/MyResolved")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<NegotiationClientModel>>> GetMyResolvedNegotiations([FromQuery] PagingModel paging)
        {
            try
            {
                string? userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userGuid = Guid.Parse(userId);
                var data = await _service.GetResolvedNegotiationsByUserIdAsync(paging, userGuid);
                var metadata = new
                {
                    data.TotalCount,
                    data.PageSize,
                    data.CurrentPage,
                    data.TotalPages,
                    data.HasNext,
                    data.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(data.Adapt<IEnumerable<NegotiationClientModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpGet("api/Negotiations/MyUnresolved")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<NegotiationClientModel>>> GetMyUnresolvedNegotiations([FromQuery] PagingModel paging)
        {
            try
            {
                string? userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userGuid = Guid.Parse(userId);
                var data = await _service.GetUnresolvedNegotiationsByUserIdAsync(paging, userGuid);
                var metadata = new
                {
                    data.TotalCount,
                    data.PageSize,
                    data.CurrentPage,
                    data.TotalPages,
                    data.HasNext,
                    data.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(data.Adapt<IEnumerable<NegotiationClientModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpGet("api/Negotiations/Resolved/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<NegotiationClientModel>>> GetResolvedNegotiationsByUserId(Guid userId, [FromQuery] PagingModel paging)
        {
            try
            {
                var data = await _service.GetResolvedNegotiationsByUserIdAsync(paging, userId);
                var metadata = new
                {
                    data.TotalCount,
                    data.PageSize,
                    data.CurrentPage,
                    data.TotalPages,
                    data.HasNext,
                    data.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(data.Adapt<IEnumerable<NegotiationClientModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpGet("api/Negotiations/Unresolved/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<NegotiationClientModel>>> GetUnresolvedNegotiationsByUserId(Guid userId, [FromQuery] PagingModel paging)
        {
            try
            {
                var data = await _service.GetUnresolvedNegotiationsByUserIdAsync(paging, userId);
                var metadata = new
                {
                    data.TotalCount,
                    data.PageSize,
                    data.CurrentPage,
                    data.TotalPages,
                    data.HasNext,
                    data.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(data.Adapt<IEnumerable<NegotiationClientModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<NegotiationClientModel>> CreateNegotiation(NegotiationClientCreateModel negotiationModel)
        {
            try
            {
                ValidationResult result = await _validator.ValidateAsync(negotiationModel);
                if (!result.IsValid)
                {
                    return BadRequest("Validation error");
                }

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var userGuid = Guid.Parse(userId);
                var data = await _service.CreateNegotiationAsync(userGuid, negotiationModel.Adapt<NegotiationModel>());
                return Created(HttpContext.Request.Host.ToString() + "/" + data?.Id, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpPatch("api/Negotiations/Accept/{negotiationId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AcceptNegotiation(Guid negotiationId, string description)
        {
            try
            {
                await _service.AcceptNegotiation(negotiationId, description);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpPatch("api/Negotiations/Refuse/{negotiationId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RefuseNegotiation(Guid negotiationId, string description)
        {
            try
            {
                await _service.RefuseNegotiation(negotiationId, description);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateNegotiation(NegotiationClientModel negotiationModel)
        {
            try
            {
                await _service.UpdateNegotiationAsync(negotiationModel.Adapt<NegotiationModel>());
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteNegotiationAsync(NegotiationClientModel negotiationModel)
        {
            try
            {
                await _service.DeleteNegotiationAsync(negotiationModel.Adapt<NegotiationModel>());
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }
    }
}
