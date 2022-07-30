using Sabio.Models;
using Sabio.Models.Domain.Friends;
using Sabio.Models.Requests.Friends;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface IFriendService
    {
        int Add(FriendAddRequest model, int userId);
        void Delete(int id);
        Friend Get(int id);
        List<Friend> GetAll();
        void Update(FriendUpdateRequest model, int userId);
        Paged<Friend> GetPaginate(int pageIndex, int pageSize);
        Paged<Friend> SearchPaginate(int pageIndex, int pageSize, string query);

        void DeleteV2(int id);
        void UpdateV2(FriendUpdateRequestV2 model, int userId);
        int AddV2(FriendAddRequestV2 model, int userId);
        FriendV2 SelectByIdV2(int id);
        List<FriendV2> GetAllV2();
        Paged<FriendV2> GetPaginateV2(int pageIndex, int pageSize);
        Paged<FriendV2> SearchPaginateV2(int pageIndex, int pageSize, string query);


        FriendV3 SelectByIdV3(int id);
        List<FriendV3> GetAllV3();
        Paged<FriendV3> GetPaginateV3(int pageIndex, int pageSize);
        Paged<FriendV3> SearchPaginateV3(int pageIndex, int pageSize, string query);
        void DeleteV3(int id);
        void UpdateV3(FriendUpdateRequestV3 model, int userId);
        int AddV3(FriendAddRequestV3 model, int userId);
     
    }
}