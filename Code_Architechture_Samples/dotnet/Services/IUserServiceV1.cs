using Sabio.Models;
using Sabio.Models.Domain.Users;
using Sabio.Models.Requests.Users;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface IUserServiceV1
    {
        int Add(UserAddRequest model);
        void Delete(int id);
        User Get(int id);
        List<User> GetAll();
        void Update(UserUpdateRequest model);
        Paged<User> Pagination(int pageIndex, int pageSize);
        Paged<User> SearchPaginate(int pageIndex, int pageSize, string query);
    }
}