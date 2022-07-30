using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.CodeChallenge.Domain;
using Sabio.Models.CodeChallenge.Requests;
using Sabio.Services;
using Sabio.Services.CodeChallenge;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers.CodeChallenge
{
    [Route("api/courses")]
    [ApiController]
    public class CourseApiController : BaseApiController
    {

        private ICourseService _service = null;
        private IAuthenticationService<int> _authService = null;

        public CourseApiController(ICourseService service, ILogger<CourseApiController> logger, IAuthenticationService<int> authService) : base(logger)
        {

            _service = service;
            _authService = authService;

        }


        [HttpPost]
        public ActionResult<ItemResponse<int>> AddCourse(CourseAddRequest model)
        {
            ObjectResult result;

            try
            {
                int userId = _authService.GetCurrentUserId();

                int id = _service.Create(model);


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


        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Course>> GetCourseById(int id)
        {
            int iCode = 200;
            BaseResponse response;

            try
            {
                Course aCourse = _service.GetById(id);

                if (aCourse != null)
                {
                    response = new ItemResponse<Course> { Item = aCourse };
                }
                else
                {
                    iCode = 404;
                    response = new ErrorResponse("Course Not Found");
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(iCode, response);
        }


        [HttpPut("{id:int}")]
        public ActionResult<ItemResponse<SuccessResponse>> UpdateCourse(CourseUpdateRequest model)
        {
            int iCode = 200;
            BaseResponse response;

            try
            {
                _service.Update(model);

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

        [HttpDelete("students/{id:int}")]
        public ActionResult<ItemResponse<SuccessResponse>> DeleteStudent(int id)
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

        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<Course>>> GetCoursesByPage(int pageIndex, int pageSize)
        {
            ActionResult result = null;

            try
            {
                Paged<Course> paged = _service.GetByPage(pageIndex, pageSize);

                if (paged != null)
                {
                    ItemResponse<Paged<Course>> response = new ItemResponse<Paged<Course>>();
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
