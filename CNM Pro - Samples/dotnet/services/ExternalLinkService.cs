using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models.Domain;
using Sabio.Models.Requests.ExternalLinks;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sabio.Services
{
    public class ExternalLinkService : IExternalLinkService
    {
        private IAuthenticationService<int> _authenticationService;
        private IDataProvider _data = null;
        public ExternalLinkService(IAuthenticationService<int> authService, IDataProvider dataProvider)
        {
            _data = dataProvider;
            _authenticationService = authService;
        }


        public void Delete(int id, int userId)
        {
            string procName = "[dbo].[ExternalLinks_DeleteById]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", id);
                col.AddWithValue("@UserId", userId);

            }, returnParameters: null);
        }

        public int Add(ExternalLinkAddRequest model, int userId)
        {
            int id = 0;

            string procName = "[dbo].[ExternalLinks_Insert]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParams(model, col, ref userId);

                SqlParameter IdOut = new SqlParameter("@Id", SqlDbType.Int);
                IdOut.Direction = ParameterDirection.Output;

                col.Add(IdOut);

            }, returnParameters: delegate (SqlParameterCollection returnCollection)
            {
                object oId = returnCollection["@Id"].Value;
                int.TryParse(oId.ToString(), out id);
            });
            return id;
        }

        public void Update(ExternalLinkUpdateRequest model, int userId)
        {

            string procName = "[dbo].[ExternalLinks_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", model.Id);
                AddCommonParams(model, col, ref userId);

            }, returnParameters: null);
        }

        public List<ExternalLink> GetByUserId(int userId)
        {
            List<ExternalLink> list = null;

            _data.ExecuteCmd(
                "[dbo].[ExternalLinks_SelectByCreatedBy]"
                , (param) =>

                {
                    param.AddWithValue("@UserId", userId);
                },
                (reader, recordSetIndex) =>
                {
                    int startingIndex = 0;

                    ExternalLink link = MapSingleLink(reader, ref startingIndex);

                    if (list == null)
                    {
                        list = new List<ExternalLink>();
                    }

                    list.Add(link);
                });

            return list;
        }


        private ExternalLink MapSingleLink(IDataReader reader, ref int startingIndex)
        {
            ExternalLink link = new ExternalLink();
            link.UrlTypes = new LookUp();
            link.EntityTypes = new LookUp();

            link.Id = reader.GetSafeInt32(startingIndex++);
            link.UserId = reader.GetSafeInt32(startingIndex++);
            link.UrlTypes.Id = reader.GetSafeInt32(startingIndex++);
            link.UrlTypes.Name = reader.GetSafeString(startingIndex++);
            link.Url = reader.GetSafeString(startingIndex++);
            link.EntityId = reader.GetSafeInt32(startingIndex++);
            link.EntityTypes.Id = reader.GetSafeInt32(startingIndex++);
            link.EntityTypes.Name = reader.GetSafeString(startingIndex++);
            link.DateCreated = reader.GetSafeDateTime(startingIndex++);
            link.DateModified = reader.GetSafeDateTime(startingIndex++);

            return link;
        }
        private static void AddCommonParams(ExternalLinkAddRequest model, SqlParameterCollection col, ref int userId)
        {
            col.AddWithValue("@UserId", userId);
            col.AddWithValue("@UrlTypeId", model.UrlTypeId);
            col.AddWithValue("@Url", model.Url);
            col.AddWithValue("@EntityId", model.EntityId);
            col.AddWithValue("@EntityTypeId", model.EntityTypeId);
        }
      
    }
}
