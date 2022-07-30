using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain.Friends;
using Sabio.Models.Requests.Friends;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/friends")]
    [ApiController]
    public class FriendsApiController : BaseApiController
    {
        private IFriendService _service = null;
        private IAuthenticationService<int> _authService = null;

        public FriendsApiController(IFriendService service, ILogger<FriendsApiController> logger, IAuthenticationService<int> authService) : base(logger)
        {

            _service = service;
            _authService = authService;

        }



        [HttpGet("{id:int}")]

        public ActionResult<ItemResponse<Friend>> Get(int id)
        {
            int iCode = 200;
            BaseResponse response;

            try
            {
                Friend friend = _service.Get(id);

                if (friend != null)
                {
                    response = new ItemResponse<Friend> { Item = friend };
                }
                else
                {
                    iCode = 404;
                    response = new ErrorResponse("Friend Not Found");
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(iCode, response);
        }


        [HttpGet("")]

        public ActionResult<ItemsResponse<Friend>> GetAll()
        {
            int iCode = 200;
            BaseResponse response;

            try
            {
                List<Friend> friendList = _service.GetAll();

                if (friendList != null)
                {
                    response = new ItemsResponse<Friend> { Items = friendList };
                }
                else
                {
                    iCode = 404;
                    response = new ErrorResponse("No Friends Found");
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

        public ActionResult<SuccessResponse> Delete(int id)
        {
            int iCode = 200;
            BaseResponse response;

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
        public ActionResult<ItemResponse<int>> Create(FriendAddRequest model)
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
                ErrorResponse response = new ErrorResponse(ex.Message);

                base.Logger.LogError(ex.ToString());

                result = StatusCode(500, response);
            }

            return result;
        }


        [HttpPut("{id:int}")]
        public ActionResult<ItemResponse<int>> Update(FriendUpdateRequest model)
        {
            int iCode = 200;
            BaseResponse response;

            try
            {
                int userId = _authService.GetCurrentUserId();

                _service.Update(model, userId);

                response = new ItemResponse<Friend>();
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
        public ActionResult<ItemResponse<Paged<Friend>>> GetPaginate(int pageIndex, int pageSize)
        {
            ActionResult result = null;

            try
            {
                Paged<Friend> paged = _service.GetPaginate(pageIndex, pageSize);

                if (paged != null)
                {
                    ItemResponse<Paged<Friend>> response = new ItemResponse<Paged<Friend>>();
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
        public ActionResult<ItemResponse<Paged<Friend>>> SearchPaginate(int pageIndex, int pageSize, string query)
        {
            ActionResult result = null;

            try
            {
                Paged<Friend> paged = _service.SearchPaginate(pageIndex, pageSize, query);

                if (paged != null)
                {
                    ItemResponse<Paged<Friend>> response = new ItemResponse<Paged<Friend>>();
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
