using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models.Domain;
using Sabio.Models.Requests.ExternalLinks;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/externallinks")]
    [ApiController]
    public class ExternalLinksApiController : BaseApiController
    {
        private IExternalLinkService _service = null;
        private IAuthenticationService<int> _authService = null;
        public ExternalLinksApiController(IExternalLinkService service
                                    , ILogger<ExternalLinksApiController> logger
                                    , IAuthenticationService<int> authService)
                                    : base(logger)
        {
            _service = service;
            _authService = authService;
        }


        [HttpPost]
        public ActionResult<ItemResponse<int>> AddExternalLink(ExternalLinkAddRequest model)
        {
            ObjectResult result;

            try
            {
                int userId = _authService.GetCurrentUserId();

                int id = _service.Add(model, userId);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };
                result = Created201(response);

            }

            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }
            return result;
        }

        [HttpGet("user")]
        public ActionResult<ItemsResponse<ExternalLink>> GetByCreatedBy()
        {
            ActionResult result = null;

            try
            {
                int userId = _authService.GetCurrentUserId();

                List<ExternalLink> list = _service.GetByUserId(userId);

                if (list != null)
                {
                    ItemsResponse<ExternalLink> response = new ItemsResponse<ExternalLink>();
                    response.Items = list;

                    result = Ok200(response);
                }
                else
                {
                    result = NotFound404(new ErrorResponse("Error: No External Links Found"));
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
                base.Logger.LogError(ex.ToString());
            }
            return result;
        }

        [HttpPut("user/link/{id:int}")]
        public ActionResult<SuccessResponse> Update(ExternalLinkUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();

                _service.Update(model, userId);
                response = new SuccessResponse();

            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }



        [HttpDelete("user/link/{id:int}")]
        public ActionResult<SuccessResponse> Delete(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();

                _service.Delete(id, userId);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

    }
}

