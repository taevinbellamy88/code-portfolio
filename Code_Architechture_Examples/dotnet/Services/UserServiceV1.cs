using Sabio.Data.Providers;
using Sabio.Models.Domain.Users;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using Sabio.Data;
using Sabio.Models.Requests.Users;
using Sabio.Services.Interfaces;
using Sabio.Models;

namespace Sabio.Services
{
    public class UserServiceV1 : IUserServiceV1
    {
        IDataProvider _data = null;

        public UserServiceV1(IDataProvider data)
        {
            _data = data;

        }

        public int Add(UserAddRequest model)
        {
            int id = 0;

            string procName = "[dbo].[Users_Insert]";


            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                AddCommonParams(model, collection);


                //and 1 OUTPUT

                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;

                collection.Add(idOut);

            }
            , returnParameters: delegate (SqlParameterCollection returnCollection)
            {


                object receivedId = returnCollection["@Id"].Value;

                int.TryParse(receivedId.ToString(), out id);

                Console.WriteLine("");

            });

            return id;

        }

        public void Update(UserUpdateRequest model)
        {

            string procName = "[dbo].[Users_Update]";


            _data.ExecuteNonQuery(procName
                , inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@Id", model.Id);
                    collection.AddWithValue("@FirstName", model.FirstName);
                    collection.AddWithValue("@LastName", model.LastName);
                    collection.AddWithValue("@Email", model.Email);
                    collection.AddWithValue("@AvatarUrl", model.AvatarUrl);
                    collection.AddWithValue("@TenantId", model.TenantId);
                    collection.AddWithValue("@Password", model.Password);
                    collection.AddWithValue("@PasswordConfirm", model.PasswordConfirm);


                }
                , returnParameters: null);

        }

        public void Delete(int id)
        {
            string procName = "[dbo].[Users_Delete]";


            _data.ExecuteNonQuery(procName,
                            inputParamMapper: delegate (SqlParameterCollection paramCollection)
                            {
                                paramCollection.AddWithValue("@Id", id);
                            });
        }

        public User Get(int id)
        {
            User user = null;

            // string storedProc,
            // Action < SqlParameterCollection > inputParamMapper,
            // Action <IDataReader, short> singleRecordMapper,


            string procName = "[dbo].[Users_SelectById]";


            _data.ExecuteCmd(procName,
                            inputParamMapper: delegate (SqlParameterCollection paramCollection)
                            {
                                paramCollection.AddWithValue("@Id", id);
                            },
                            singleRecordMapper: delegate (IDataReader reader, short set) //single record mapper
                            {
                                user = MapSingleUser(reader);
                            });
            return user;
        }

        public List<User> GetAll()
        {
            List<User> userList = null;

            string procName = "[dbo].[Users_SelectAll]";


            _data.ExecuteCmd(procName,
                            inputParamMapper: null,
                            singleRecordMapper: delegate (IDataReader reader, short set) //single record mapper
                            {
                                User aUser = MapSingleUser(reader);


                                if (userList == null)
                                {
                                    userList = new List<User>();
                                }

                                userList.Add(aUser);
                            });
            return userList;
        }


        public Paged<User> Pagination(int pageIndex, int pageSize)
        {

            string procName = "[dbo].[Users_Pagination]";

            Paged<User> pagedList = null;
            List<User> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName
                            , inputParamMapper: delegate (SqlParameterCollection param)
                            {
                                param.AddWithValue("@PageIndex", pageIndex);
                                param.AddWithValue("@PageSize", pageSize);
                            }
                            , singleRecordMapper: delegate (IDataReader reader, short set)
                            {
                                User user = MapSingleUser(reader);

                                totalCount = reader.GetSafeInt32(8);

                                if (list == null)
                                {
                                    list = new List<User>();
                                }

                                list.Add(user);
                            });
            if (list != null)
            {
                pagedList = new Paged<User>(list, pageIndex, pageSize, totalCount);
            }


            return pagedList;

        }


        public Paged<User> SearchPaginate(int pageIndex, int pageSize, string query)
        {

            string procName = "[dbo].[Users_Search_Pagination]";

            Paged<User> pagedList = null;
            List<User> list = null;
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
                                User user = MapSingleUser(reader);

                                totalCount = reader.GetSafeInt32(8);

                                if (list == null)
                                {
                                    list = new List<User>();
                                }

                                list.Add(user);
                            });
            if (list != null)
            {
                pagedList = new Paged<User>(list, pageIndex, pageSize, totalCount);
            }


            return pagedList;

        }

        private static void AddCommonParams(UserAddRequest model, SqlParameterCollection collection)
        {
            collection.AddWithValue("@FirstName", model.FirstName);
            collection.AddWithValue("@LastName", model.LastName);
            collection.AddWithValue("@Email", model.Email);
            collection.AddWithValue("@AvatarUrl", model.AvatarUrl);
            collection.AddWithValue("@TenantId", model.TenantId);
            collection.AddWithValue("@Password", model.Password);
            collection.AddWithValue("@PasswordConfirm", model.PasswordConfirm);
        }

        private static User MapSingleUser(IDataReader reader)
        {
            User aUser = new User();

            int startingIdx = 0;

            aUser.Id = reader.GetSafeInt32(startingIdx++);
            aUser.FirstName = reader.GetSafeString(startingIdx++);
            aUser.LastName = reader.GetSafeString(startingIdx++);
            aUser.Email = reader.GetSafeString(startingIdx++);
            aUser.AvatarUrl = reader.GetSafeString(startingIdx++);
            aUser.TenantId = reader.GetSafeString(startingIdx++);
            aUser.DateCreated = reader.GetSafeDateTime(startingIdx++);
            aUser.DateModified = reader.GetSafeDateTime(startingIdx++);

            return aUser;
        }
    }



}
