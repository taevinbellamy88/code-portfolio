using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.SiteReferences;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/sitereferences")]
    [ApiController]
    public class SiteReferencesApiController : BaseApiController
    {
        private ISiteReferenceService _service = null;
        private IAuthenticationService<int> _authService = null;

        public SiteReferencesApiController(ISiteReferenceService service
                                  , ILogger<SiteReferencesApiController> logger
                                  , IAuthenticationService<int> authService)
                                  : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<ItemResponse<int>> Add(SiteReferenceAddRequest model)
        {
            ObjectResult result;

            try
            {
                int id = _service.Add(model);

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

        [HttpGet("paginate")]
        [Authorize(Roles ="Admin")]
        public ActionResult<ItemResponse<Paged<LookUp>>> GetAll(int pageIndex, int pageSize)
        {
            ActionResult result;

            try
            {
                Paged<LookUp> paged = _service.SelectAll(pageIndex, pageSize);

                if (paged != null)
                {
                    ItemResponse<Paged<LookUp>> response = new ItemResponse<Paged<LookUp>>();
                    response.Item = paged;

                    result = Ok200(response);
                }
                else
                {
                    result = NotFound404(new ErrorResponse("Error: No Reference Types Found"));
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
                base.Logger.LogError(ex.ToString());
            }
            return result;
        }

        [HttpGet("types")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ItemResponse<List<LookUp>>> GetAllTypes()
        {
            ActionResult result;

            try
            {
                List<LookUp> list = _service.SelectAllTypes();

                if (list != null)
                {
                    ItemResponse<List<LookUp>> response = new ItemResponse<List<LookUp>>();
                    response.Item = list;

                    result = Ok200(response);
                }
                else
                {
                    result = NotFound404(new ErrorResponse("Error: No Reference Types Found"));
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
                base.Logger.LogError(ex.ToString());
            }
            return result;
        }

        [HttpGet("chart/data")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ItemResponse<SiteRefChart>> GetAllChartData()
        {
            ActionResult result;

            try
            {
                SiteRefChart refCount = _service.SelectAllChart();

                if (refCount != null)
                {
                    ItemResponse<SiteRefChart> response = new ItemResponse<SiteRefChart>();
                    response.Item = refCount;

                    result = Ok200(response);
                }
                else
                {
                    result = NotFound404(new ErrorResponse("Error: No Reference Types Found"));
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
                base.Logger.LogError(ex.ToString());
            }
            return result;
        }
    }
}
