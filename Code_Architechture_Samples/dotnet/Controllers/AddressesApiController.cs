using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain.Addresses;
using Sabio.Models.Requests.Addresses;
using Sabio.Services;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressesApiController : BaseApiController
    {
        private IAddressService _service = null;
        private IAuthenticationService<int> _authService = null;

        public AddressesApiController(IAddressService service
            , ILogger<AddressesApiController> logger
            , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }


        //api/addresses/

        [HttpGet("get/random")]
        public ActionResult<ItemsResponse<Address>> GetRandomAddresses()
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                List<Address> list = _service.SelectRandom50();


                if (list != null)
                {
                    response = new ItemsResponse<Address> { Items = list };
                }
                else
                {
                    iCode = 404;
                    response = new ErrorResponse("App Resource not found");
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);

                base.Logger.LogError(ex.ToString());
            }

            return StatusCode(iCode, response);
        }


        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Address>> Get(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {

                Address address = _service.Get(id);

                if (address != null)
                {
                    response = new ItemResponse<Address> { Item = address };
                }
                else
                {
                    iCode = 404;
                    response = new ErrorResponse("Address Not Found");
                }

            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);

                base.Logger.LogError(ex.ToString());
            }

            return StatusCode(iCode, response);
        }


        [HttpDelete("delete/{id:int}")]
        public ActionResult<ItemResponse<Address>> Delete(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                _service.Delete(id);

                response = new ItemResponse<Address>();
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }

            return StatusCode(iCode, response);
        }


        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(AddressAddRequest model)
        {
            ObjectResult result = null;

            try
            {
                int userId = _authService.GetCurrentUserId();

                int id = _service.Add(model, userId);

                
               ItemResponse<int> response = new ItemResponse<int>() { Item = id };

                result = Created201(response);


            }
            catch (Exception ex)
            {
                ErrorResponse response = new ErrorResponse(ex.Message);

                base.Logger.LogError(ex.ToString());

                result = StatusCode(500, response);
            }

            return result;
        }


        [HttpPut("{id:int}")]
        public ActionResult<ItemResponse<int>> Update(AddressUpdateRequest model, int userId)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                _service.Update(model, userId);

                response = new ItemResponse<Address>();
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }

            return StatusCode(iCode, response);


        }

    }
}
