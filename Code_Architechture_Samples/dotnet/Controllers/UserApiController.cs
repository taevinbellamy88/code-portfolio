using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain.Users;
using Sabio.Models.Requests.Users;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;


namespace Sabio.Web.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserApiController : BaseApiController
    {
        // connect to BaseApiController by providing ILogger< ***ApiController> through constructor
        private IUserServiceV1 _service = null;

        private IAuthenticationService<int> _authService = null;

        public UserApiController(IUserServiceV1 service, ILogger<UserApiController> logger, IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }


        //server matches route pattern

        //api/users/{int} 
        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<UserApiController>> GetById(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                User user = _service.Get(id);

                if (user != null)
                {
                    response = new ItemResponse<User> { Item = user };
                }
                else
                {
                    iCode = 404;
                    response = new ErrorResponse("Error: User Not Found");
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


        [HttpGet("")]
        public ActionResult<ItemsResponse<UserApiController>> GetAll()
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                List<User> user = _service.GetAll();

                if (user != null)
                {
                    response = new ItemsResponse<User> { Items = user };
                }
                else
                {
                    iCode = 404;
                    response = new ErrorResponse("Error: No Users Found");

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


        [HttpDelete("{id:int}")]

        public ActionResult<ItemResponse<UserApiController>> Delete(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                _service.Delete(id);

                response = new SuccessResponse();

            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }

            return StatusCode(iCode, response);

        }




        [HttpPost("")]

        public ActionResult<ItemResponse<User>> Add(UserAddRequest model) 
        {
            ObjectResult result = null;

            try
            {
                int id = _service.Add(model);

                ItemResponse<int> response = new ItemResponse<int> { Item = id };

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
        public ActionResult<ItemResponse<int>> Update(UserUpdateRequest model)
        {
            int iCode = 200;
            BaseResponse response = null;
            
            try
            {
                int userId = _authService.GetCurrentUserId();

                _service.Update(model);

                response = new ItemResponse<User>();
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }

            return StatusCode(iCode, response);


        }

        
        
        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<User>>> Pagination(int pageIndex, int pageSize)
        {
            ActionResult result = null;

            try
            {
                Paged<User> paged = _service.Pagination(pageIndex, pageSize);

                if (paged != null)
                {
                    ItemResponse<Paged<User>> response = new ItemResponse<Paged<User>>();
                    response.Item = paged;  

                    result = Ok200(response);
                }
                else
                {

                    result = NotFound404(new ErrorResponse("Error: No Users Found"));

                }

            }
            catch (Exception ex)
            {
               
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
                base.Logger.LogError(ex.ToString());
            }

            return result;

        }

        [HttpGet("search")]
        public ActionResult<ItemResponse<Paged<User>>> SearchPaginate(int pageIndex, int pageSize, string query)
        {
            ActionResult result = null;

            try
            {
                Paged<User> paged = _service.SearchPaginate(pageIndex, pageSize, query);

                if (paged != null)
                {
                    ItemResponse<Paged<User>> response = new ItemResponse<Paged<User>>();
                    response.Item = paged;

                    result = Ok200(response);
                }
                else
                {

                    result = NotFound404(new ErrorResponse("Error: No Users Found"));

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
