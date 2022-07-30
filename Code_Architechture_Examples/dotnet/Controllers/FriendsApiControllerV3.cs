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
    [Route("api/v3/friends")]
    [ApiController]
    public class FriendsApiControllerV3 : BaseApiController
    {
        private IFriendService _service = null;
        private IAuthenticationService<int> _authService = null;

        public FriendsApiControllerV3(IFriendService service, ILogger<FriendsApiControllerV3> logger, IAuthenticationService<int> authService) : base(logger)
        {

            _service = service;
            _authService = authService;

        }

        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<FriendV3>> SelectByIdV3(int id)
        {
            int iCode = 200;
            BaseResponse response;

            try
            {
                FriendV3 friend = _service.SelectByIdV3(id);

                if (friend != null)
                {
                    response = new ItemResponse<FriendV3> { Item = friend };
                }
                else
                {
                    iCode = 404;
                    response = new ErrorResponse("FriendV3 Not Found");
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(iCode, response);
        }

        [HttpGet]
        public ActionResult<ItemsResponse<FriendV3>> GetAllV3()
        {
            int iCode = 200;
            BaseResponse response;

            try
            {
                List<FriendV3> friendList = _service.GetAllV3();

                if (friendList != null)
                {
                    response = new ItemsResponse<FriendV3> { Items = friendList };
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


        [HttpGet("search")]
        public ActionResult<ItemResponse<Paged<FriendV3>>> SearchPaginateV3(int pageIndex, int pageSize, string query)
        {
            ActionResult result = null;

            try
            {
                Paged<FriendV3> paged = _service.SearchPaginateV3(pageIndex, pageSize, query);

                if (paged != null)
                {
                    ItemResponse<Paged<FriendV3>> response = new ItemResponse<Paged<FriendV3>>();
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


        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<FriendV3>>> GetPaginateV3(int pageIndex, int pageSize)
        {
            ActionResult result = null;

            try
            {
                Paged<FriendV3> paged = _service.GetPaginateV3(pageIndex, pageSize);

                if (paged != null)
                {
                    ItemResponse<Paged<FriendV3>> response = new ItemResponse<Paged<FriendV3>>();
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


        [HttpDelete("delete/{id:int}")]
        public ActionResult<ItemResponse<SuccessResponse>> DeleteV3(int id)
        {
            int iCode = 200;
            BaseResponse response;

            try
            {
                _service.DeleteV3(id);

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

        [HttpPut("{id:int}")]
        public ActionResult<ItemResponse<SuccessResponse>> UpdateV3(FriendUpdateRequestV3 model)
        {
            int iCode = 200;
            BaseResponse response;

            try
            {
                int userId = _authService.GetCurrentUserId();

                _service.UpdateV3(model, userId);

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

        [HttpPost]
        public ActionResult<ItemResponse<int>> AddV3(FriendAddRequestV3 model)
        {
            ObjectResult result;

            try
            {
                int userId = _authService.GetCurrentUserId();

                int id = _service.AddV3(model, userId);


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


    }
}

