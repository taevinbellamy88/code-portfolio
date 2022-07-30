using Sabio.Models;
using Sabio.Models.CodeChallenge.Domain;
using Sabio.Models.CodeChallenge.Requests;

namespace Sabio.Services.CodeChallenge
{
    public interface ICourseService
    {
        int Create(CourseAddRequest model);
        void Delete(int id);
        Course GetById(int id);
        Paged<Course> GetByPage(int pageIndex, int pageSize);
        void Update(CourseUpdateRequest model);
    }
}