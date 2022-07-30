using Sabio.Data.Providers;
using Sabio.Models.Requests.Friends;
using Sabio.Models.Domain.Friends;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabio.Data;
using Sabio.Models;
using Sabio.Services.Interfaces;
using Newtonsoft;
using static Sabio.Models.Domain.Friends.Friend;
using Sabio.Models.Domain;

namespace Sabio.Services
{
    public class FriendService : IFriendService
    {
        IDataProvider _data = null;


        public FriendService(IDataProvider data)
        {
            _data = data;

        }
        
       
        public int Add(FriendAddRequest model, int userId)
        {
            int id = 0;

            string procName = "[dbo].[Friends_Insert]";


            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {

                AddCommonParams(model, collection);

                collection.AddWithValue("@UserId", userId);

                //and 1 OUTPUT

                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;

                collection.Add(idOut);

            }
            , returnParameters: delegate (SqlParameterCollection returnCollection)
            {

                object receivedId = returnCollection["@Id"].Value;

                int.TryParse(receivedId.ToString(), out id);

                Console.WriteLine($"New Friends Id: {id}");

            });

            return id;

        }
        public void Update(FriendUpdateRequest model, int userId)
        {

            string procName = "[dbo].[Friends_Update]";


            _data.ExecuteNonQuery(procName
                , inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@Id", model.Id);
                    collection.AddWithValue("@UserId", userId);

                    AddCommonParams(model, collection);

                }
                , returnParameters: null);

        }
        public void Delete(int id)
        {
            string procName = "[dbo].[Friends_Delete]";


            _data.ExecuteNonQuery(procName,
                            inputParamMapper: delegate (SqlParameterCollection paramCollection)
                            {
                                paramCollection.AddWithValue("@Id", id);
                            });
        }
        public Friend Get(int id)
        {
            Friend friend = null;

            string procName = "[dbo].[Friends_SelectById]";


            _data.ExecuteCmd(procName,
                            inputParamMapper: delegate (SqlParameterCollection paramCollection)
                            {
                                paramCollection.AddWithValue("@Id", id);
                            },
                            singleRecordMapper: delegate (IDataReader reader, short set) //single record mapper
                            {
                               

                                friend = MapperV1(reader);

                            });
            return friend;
        }
        public List<Friend> GetAll()
        {
            List<Friend> friendList = null;

            string procName = "[dbo].[Friends_SelectAll]";


            _data.ExecuteCmd(procName,
                            inputParamMapper: null,
                            singleRecordMapper: delegate (IDataReader reader, short set) //single record mapper
                            {
                               
                                Friend afriend = MapperV1(reader);

                                if (friendList == null)
                                {
                                    friendList = new List<Friend>();
                                }

                                friendList.Add(afriend);
                            });
            return friendList;
        }
        public Paged<Friend> GetPaginate(int pageIndex, int pageSize)
        {

            string procName = "[dbo].[Friends_Pagination]";

            Paged<Friend> pagedList = null;
            List<Friend> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName
                            , inputParamMapper: delegate (SqlParameterCollection param)
                            {
                                param.AddWithValue("@PageIndex", pageIndex);
                                param.AddWithValue("@PageSize", pageSize);
                            }
                            , singleRecordMapper: delegate (IDataReader reader, short set)
                            {
                                Friend friend = MapperV1(reader);

                                totalCount = reader.GetSafeInt32(11);

                                if (list == null)
                                {
                                    list = new List<Friend>();
                                }

                                list.Add(friend);
                            });
            if (list != null)
            {
                pagedList = new Paged<Friend>(list, pageIndex, pageSize, totalCount);
            }


            return pagedList;

        }
        public Paged<Friend> SearchPaginate(int pageIndex, int pageSize, string query)
        {

            string procName = "[Friends_Search_Pagination]";

            Paged<Friend> pagedList = null;
            List<Friend> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName
                            , inputParamMapper: delegate (SqlParameterCollection param)
                            {
                                param.AddWithValue("@PageIndex", pageIndex);
                                param.AddWithValue("@PageSize", pageSize);
                                param.AddWithValue("@Query", query);
                            }
                            , singleRecordMapper: delegate (IDataReader reader, short set)
                            {
                                Friend friend = MapperV1(reader);

                                totalCount = reader.GetSafeInt32(11);

                                if (list == null)
                                {
                                    list = new List<Friend>();
                                }

                                list.Add(friend);
                            });
            if (list != null)
            {
                pagedList = new Paged<Friend>(list, pageIndex, pageSize, totalCount);
            }


            return pagedList;

        }



        public FriendV2 SelectByIdV2(int id)
        {
            FriendV2 friend = null;

            string procName = "[dbo].[Friends_SelectByIdV2]";

            _data.ExecuteCmd(procName,
                            inputParamMapper: delegate (SqlParameterCollection paramCollection)
                            {
                                paramCollection.AddWithValue("@Id", id);
                            },
                            singleRecordMapper: delegate (IDataReader reader, short set) //single record mapper
                            {
                                friend = MapperV2(reader);
                            });
            return friend;
        }
        public List<FriendV2> GetAllV2()
        {
            List<FriendV2> friendList = null;

            string procName = "[dbo].[Friends_SelectAllV2]";


            _data.ExecuteCmd(procName,
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set) //single record mapper
                {

                    FriendV2 friend = MapperV2(reader);

                    if (friendList == null)
                    {
                        friendList = new List<FriendV2>();
                    }

                    friendList.Add(friend);
                });
            return friendList;
        }
        public Paged<FriendV2> GetPaginateV2(int pageIndex, int pageSize)
        {

            string procName = "[dbo].[Friends_PaginationV2]";

            Paged<FriendV2> pagedList = null;
            List<FriendV2> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName
                            , inputParamMapper: delegate (SqlParameterCollection param)
                            {
                                param.AddWithValue("@PageIndex", pageIndex);
                                param.AddWithValue("@PageSize", pageSize);
                            }
                            , singleRecordMapper: delegate (IDataReader reader, short set)
                            {
                                FriendV2 friend = MapperV2(reader);

                                totalCount = reader.GetSafeInt32(13);

                                if (list == null)
                                {
                                    list = new List<FriendV2>();
                                }

                                list.Add(friend);
                            });
            if (list != null)
            {
                pagedList = new Paged<FriendV2>(list, pageIndex, pageSize, totalCount);
            }


            return pagedList;

        }
        public Paged<FriendV2> SearchPaginateV2(int pageIndex, int pageSize, string query)
        {

            string procName = "[dbo].[Friends_Search_PaginationV2]";

            Paged<FriendV2> pagedList = null;
            List<FriendV2> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName
                            , inputParamMapper: delegate (SqlParameterCollection param)
                            {
                                param.AddWithValue("@PageIndex", pageIndex);
                                param.AddWithValue("@PageSize", pageSize);
                                param.AddWithValue("@Query", query);
                            }
                            , singleRecordMapper: delegate (IDataReader reader, short set)
                            {
                                FriendV2 friend = MapperV2(reader);

                                totalCount = reader.GetSafeInt32(13);

                                if (list == null)
                                {
                                    list = new List<FriendV2>();
                                }

                                list.Add(friend);
                            });
            if (list != null)
            {
                pagedList = new Paged<FriendV2>(list, pageIndex, pageSize, totalCount);
            }


            return pagedList;

        }
        public void DeleteV2(int id)
        {
            string procName = "[dbo].[Friends_DeleteV2]";


            _data.ExecuteNonQuery(procName,
                            inputParamMapper: delegate (SqlParameterCollection paramCollection)
                            {
                                paramCollection.AddWithValue("@Id", id);
                            });
        }
        public void UpdateV2(FriendUpdateRequestV2 model, int userId)
        {

            string procName = "[dbo].[Friends_UpdateV2]";


            _data.ExecuteNonQuery(procName
                , inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@Id", model.Id);
                    collection.AddWithValue("@UserId", userId);

                    AddCommonParamsV2(model, collection);

                }
                , returnParameters: null);

        }
        public int AddV2(FriendAddRequestV2 model, int userId)
        {
            int id = 0;

            string procName = "[dbo].[Friends_InsertV2]";


            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {

                AddCommonParamsV2(model, collection);

                collection.AddWithValue("@UserId", userId);

                //and 1 OUTPUT

                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;

                collection.Add(idOut);

            }
            , returnParameters: delegate (SqlParameterCollection returnCollection)
            {

                object receivedId = returnCollection["@Id"].Value;

                int.TryParse(receivedId.ToString(), out id);

                Console.WriteLine($"New Friends Id: {id}");

            });

            return id;

        }


        public FriendV3 SelectByIdV3(int id)
        {
            FriendV3 friend = null;

            string procName = "[dbo].[Friends_SelectByIdV3]";

            _data.ExecuteCmd(procName,
                            inputParamMapper: delegate (SqlParameterCollection paramCollection)
                            {
                                paramCollection.AddWithValue("@Id", id);
                            },
                            singleRecordMapper: delegate (IDataReader reader, short set) //single record mapper
                            {
                                friend = MapperV3(reader);
                            });
            return friend;
        }
        public List<FriendV3> GetAllV3()
        {
            List<FriendV3> friendList = null;

            string procName = "[dbo].[Friends_SelectAllV3]";


            _data.ExecuteCmd(procName,
                            inputParamMapper: null,
                            singleRecordMapper: delegate (IDataReader reader, short set) //single record mapper
                            {

                                FriendV3 friend = MapperV3(reader);

                                if (friendList == null)
                                {
                                    friendList = new List<FriendV3>();
                                }

                                friendList.Add(friend);
                            });
            return friendList;
        }
        public Paged<FriendV3> GetPaginateV3(int pageIndex, int pageSize)
        {

            string procName = "[dbo].[Friends_PaginationV3]";

            Paged<FriendV3> pagedList = null;
            List<FriendV3> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName
                            , inputParamMapper: delegate (SqlParameterCollection param)
                            {
                                param.AddWithValue("@PageIndex", pageIndex);
                                param.AddWithValue("@PageSize", pageSize);
                            }
                            , singleRecordMapper: delegate (IDataReader reader, short set)
                            {
                                FriendV3 friend = MapperV3(reader);

                                totalCount = reader.GetSafeInt32(14);

                                if (list == null)
                                {
                                    list = new List<FriendV3>();
                                }

                                list.Add(friend);
                            });
            if (list != null)
            {
                pagedList = new Paged<FriendV3>(list, pageIndex, pageSize, totalCount);
            }


            return pagedList;

        }
        public Paged<FriendV3> SearchPaginateV3(int pageIndex, int pageSize, string query)
        {

            string procName = "[dbo].[Friends_Search_PaginationV3]";

            Paged<FriendV3> pagedList = null;
            List<FriendV3> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName
                            , inputParamMapper: delegate (SqlParameterCollection param)
                            {
                                param.AddWithValue("@PageIndex", pageIndex);
                                param.AddWithValue("@PageSize", pageSize);
                                param.AddWithValue("@Query", query);
                            }
                            , singleRecordMapper: delegate (IDataReader reader, short set)
                            {
                                FriendV3 friend = MapperV3(reader);

                                totalCount = reader.GetSafeInt32(14);

                                if (list == null)
                                {
                                    list = new List<FriendV3>();
                                }

                                list.Add(friend);
                            });
            if (list != null)
            {
                pagedList = new Paged<FriendV3>(list, pageIndex, pageSize, totalCount);
            }


            return pagedList;

        }
        public void DeleteV3(int id)
        {
            string procName = "[dbo].[Friends_DeleteV3]";


            _data.ExecuteNonQuery(procName,
                            inputParamMapper: delegate (SqlParameterCollection paramCollection)
                            {
                                paramCollection.AddWithValue("@Id", id);
                            });
        }
        public void UpdateV3(FriendUpdateRequestV3 model, int userId)
        {

            string procName = "[dbo].[Friends_UpdateV3]";


            _data.ExecuteNonQuery(procName
                , inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@Id", model.Id);
                    collection.AddWithValue("@UserId", userId);

                    AddCommonParamsV3(model, collection);

                }
                , returnParameters: null);

        }
        public int AddV3(FriendAddRequestV3 model, int userId)
        {
            int id = 0;

            string procName = "[dbo].[Friends_InsertV3]";

            DataTable myParamValue = null;   
            
            if (model.Skills != null)
            {
                myParamValue = MapSkillsToTableV3(model.Skills);
            };


            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {

                AddCommonParamsV3(model, collection);

                collection.AddWithValue("@batchSkills", myParamValue);

                collection.AddWithValue("@UserId", userId);

                //and 1 Id OUTPUT

                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;

                collection.Add(idOut);

            }
            , returnParameters: delegate (SqlParameterCollection returnCollection)
            {

                object receivedId = returnCollection["@Id"].Value;

                int.TryParse(receivedId.ToString(), out id);

                Console.WriteLine($"New Friends Id: {id}");

            });

            return id;

        }


        private DataTable MapSkillsToTableV3(List<String> skillsToMap)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Name", typeof(String));

            foreach (string singleSkill in skillsToMap)
            {
                DataRow dr = dt.NewRow();
                int startingIdx = 0;

                dr.SetField(startingIdx++, singleSkill);

                dt.Rows.Add(dr);

            } 

          return dt;

        }

        public void AddSkills(List<String> model)
        {
            string procName = "[dbo].[Skills_InsertBatchV2]";

            DataTable myParamValue = null;

            if (model != null)
            {
                myParamValue = MapSkillsToTableV3(model);
            };

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection sqlParams)
            {
                sqlParams.AddWithValue("@batchSkills", myParamValue);

            });
        }

        public List<SkillIdPair> AddExternalSkills(List<String> model)
        {

            string procName = "[dbo].[Skills_InsertBatchV2_Insert]";

            List<SkillIdPair> ids = null;

            DataTable myParamValue = MapSkillsToTableV3(model);

            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection sqlParams)
            {
                sqlParams.AddWithValue("@batchSkills", myParamValue);
            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                SkillIdPair pair = new SkillIdPair();
                int startingIdx = 0;
                pair.NewId = reader.GetInt32(startingIdx++);
                pair.ExternalId = reader.GetInt32(startingIdx++);

                if (ids == null)
                {
                    ids = new List<SkillIdPair>();
                }
                ids.Add(pair);
            });

            return ids;
        }





        private static Friend MapperV1(IDataReader reader)
        {
            Friend afriend = new Friend();

            int startingIdx = 0;

            afriend.Id = reader.GetSafeInt32(startingIdx++);
            afriend.Title = reader.GetSafeString(startingIdx++);
            afriend.Bio = reader.GetSafeString(startingIdx++);
            afriend.Summary = reader.GetSafeString(startingIdx++);
            afriend.Headline = reader.GetSafeString(startingIdx++);
            afriend.Slug = reader.GetSafeString(startingIdx++);
            afriend.StatusId = reader.GetSafeInt32(startingIdx++);
            afriend.PrimaryImageUrl = reader.GetSafeString(startingIdx++);
            afriend.DateCreated = reader.GetSafeDateTime(startingIdx++);
            afriend.DateModified = reader.GetSafeDateTime(startingIdx++);
            afriend.UserId = reader.GetSafeInt32(startingIdx++);



            return afriend;

        }
        private static FriendV2 MapperV2(IDataReader reader)
        {
            FriendV2 afriend = new FriendV2();
            afriend.PrimaryImage = new Image();

            int startingIdx = 0;

            afriend.Id = reader.GetSafeInt32(startingIdx++);

            afriend.PrimaryImage.Url = reader.GetSafeString(startingIdx++); 
            afriend.PrimaryImage.ImageId = reader.GetSafeInt32(startingIdx++);    
            afriend.PrimaryImage.TypeId = reader.GetSafeInt32(startingIdx++);

            afriend.Title = reader.GetSafeString(startingIdx++);
            afriend.Bio = reader.GetSafeString(startingIdx++);
            afriend.Summary = reader.GetSafeString(startingIdx++);
            afriend.Headline = reader.GetSafeString(startingIdx++);
            afriend.Slug = reader.GetSafeString(startingIdx++);
            afriend.StatusId = reader.GetSafeInt32(startingIdx++);
            afriend.DateCreated = reader.GetSafeDateTime(startingIdx++);
            afriend.DateModified = reader.GetSafeDateTime(startingIdx++);
            afriend.UserId = reader.GetSafeInt32(startingIdx++);
           
            return afriend;
        }
        private static FriendV3 MapperV3(IDataReader reader)
        {
            FriendV3 afriend = new FriendV3();
            afriend.PrimaryImage = new Image();


            int startingIdx = 0;

            afriend.Id = reader.GetSafeInt32(startingIdx++);

            afriend.PrimaryImage.Url = reader.GetSafeString(startingIdx++);
            afriend.PrimaryImage.ImageId = reader.GetSafeInt32(startingIdx++);
            afriend.PrimaryImage.TypeId = reader.GetSafeInt32(startingIdx++);


            afriend.Title = reader.GetSafeString(startingIdx++);
            afriend.Bio = reader.GetSafeString(startingIdx++);
            afriend.Summary = reader.GetSafeString(startingIdx++);
            afriend.Headline = reader.GetSafeString(startingIdx++);
            afriend.Slug = reader.GetSafeString(startingIdx++);
            afriend.StatusId = reader.GetSafeInt32(startingIdx++);
            afriend.DateCreated = reader.GetSafeDateTime(startingIdx++);
            afriend.DateModified = reader.GetSafeDateTime(startingIdx++);
            afriend.UserId = reader.GetSafeInt32(startingIdx++);
            afriend.Skills = reader.DeserializeObject<List<Skills>>(startingIdx++);

            return afriend;
        }                          
        
        
        
        private static void AddCommonParams(FriendAddRequest model, SqlParameterCollection collection)
        {
            collection.AddWithValue("@Title", model.Title);
            collection.AddWithValue("@Bio", model.Bio);
            collection.AddWithValue("@Summary", model.Summary);
            collection.AddWithValue("@Headline", model.Headline);
            collection.AddWithValue("@Slug", model.Slug);
            collection.AddWithValue("@StatusId", model.StatusId);
            collection.AddWithValue("@PrimaryImageUrl", model.PrimaryImageUrl);
        }

        private static void AddCommonParamsV2(FriendAddRequestV2 model, SqlParameterCollection collection)
        {
            collection.AddWithValue("@Title", model.Title);
            collection.AddWithValue("@Bio", model.Bio);
            collection.AddWithValue("@Summary", model.Summary);
            collection.AddWithValue("@Headline", model.Headline);
            collection.AddWithValue("@Slug", model.Slug);
            collection.AddWithValue("@StatusId", model.StatusId);
            collection.AddWithValue("@TypeId", model.PrimaryImage.TypeId);
            collection.AddWithValue("@PrimaryImageUrl", model.PrimaryImage.Url);
        }

        private static void AddCommonParamsV3(FriendAddRequestV3 model, SqlParameterCollection collection)
        {
            collection.AddWithValue("@Title", model.Title);
            collection.AddWithValue("@Bio", model.Bio);
            collection.AddWithValue("@Summary", model.Summary);
            collection.AddWithValue("@Headline", model.Headline);
            collection.AddWithValue("@Slug", model.Slug);
            collection.AddWithValue("@StatusId", model.StatusId);
            collection.AddWithValue("@TypeId", model.PrimaryImage.TypeId);
            collection.AddWithValue("@PrimaryImageUrl", model.PrimaryImage.Url);
           
        }

      
    }

}

