using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.SiteReferences;
using Sabio.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sabio.Services
{
    public class SiteReferenceService : ISiteReferenceService
    {
        private IAuthenticationService<int> _authenticationService;
        private IDataProvider _dataProvider;

        public SiteReferenceService(IAuthenticationService<int> authService, IDataProvider dataProvider)
        {
            _authenticationService = authService;
            _dataProvider = dataProvider;
        }

        public int Add(SiteReferenceAddRequest model)
        {
            int userId = model.UserId;

            string procName = "[dbo].[SiteReferences_Insert]";

            _dataProvider.ExecuteNonQuery(procName
                , inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    AddSiteRefParams(model, collection);
                }
                , returnParameters: null);

            return userId;
        }
        public Paged<LookUp> SelectAll(int pageIndex, int pageSize)
        {
            string procName = "[dbo].[SiteReferences_SelectAll]";
            Paged<LookUp> pagedList = null;
            List<LookUp> list = null;
            int totalCount = 0;

            _dataProvider.ExecuteCmd(procName
                             , inputParamMapper: delegate (SqlParameterCollection param)
                            {
                                AddPaginateParams(pageIndex, pageSize, param);
                            }
                            , singleRecordMapper: delegate (IDataReader reader, short set)
                            {
                                int startingIndex = 0;
                                LookUp refType = ReferenceMapper(reader, ref startingIndex);

                                if (totalCount == 0)
                                {
                                    totalCount = reader.GetSafeInt32(startingIndex++);
                                };

                                if (list == null)
                                {
                                    list = new List<LookUp>();
                                }
                                list.Add(refType);
                            });
            if (list != null)
            {
                pagedList = new Paged<LookUp>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        public List<LookUp> SelectAllTypes()
        {
            string procName = "[dbo].[ReferenceTypes_SelectAll]";
            List<LookUp> list = null;

            _dataProvider.ExecuteCmd(procName
                             , inputParamMapper: null

                            , singleRecordMapper: delegate (IDataReader reader, short set)
                            {
                                int startingIndex = 0;
                                LookUp type = ReferenceMapper(reader, ref startingIndex);

                                if (list == null)
                                {
                                    list = new List<LookUp>();
                                }
                                list.Add(type);
                            });
            return list;
        }

        public SiteRefChart SelectAllChart()
        {
            string procName = "[dbo].[SiteReferences_SelectAllChart]";
            SiteRefChart chartData = null;

            _dataProvider.ExecuteCmd(procName
                             , inputParamMapper: null

                            , singleRecordMapper: delegate (IDataReader reader, short set)
                            {
                                int startingIndex = 0;
                                chartData = ChartMapper(reader, ref startingIndex);

                            });
            return chartData;
        }

        private static SiteRefChart ChartMapper(IDataReader reader, ref int startingIndex)
        {
            SiteRefChart chartData = new SiteRefChart();
            chartData.Search = reader.GetSafeInt32(startingIndex++);
            chartData.Google = reader.GetSafeInt32(startingIndex++);
            chartData.Facebook = reader.GetSafeInt32(startingIndex++);
            chartData.OtherSocial = reader.GetSafeInt32(startingIndex++);
            chartData.Email = reader.GetSafeInt32(startingIndex++);
            chartData.WordOfMouth = reader.GetSafeInt32(startingIndex++);
            chartData.Recruiter = reader.GetSafeInt32(startingIndex++);
            chartData.JobFair = reader.GetSafeInt32(startingIndex++);
            chartData.Other = reader.GetSafeInt32(startingIndex++);
            return chartData;
        }
        private static void AddPaginateParams(int pageIndex, int pageSize, SqlParameterCollection param)
        {
            param.AddWithValue("@PageIndex", pageIndex);
            param.AddWithValue("@PageSize", pageSize);
        }
        private static LookUp ReferenceMapper(IDataReader reader, ref int startingIndex)
        {
            LookUp aReference = new LookUp();

            aReference.Id = reader.GetSafeInt32(startingIndex++);
            aReference.Name = reader.GetString(startingIndex++);

            return aReference;

        }
        private static void AddSiteRefParams(SiteReferenceAddRequest model, SqlParameterCollection collection)
        {
            collection.AddWithValue("@ReferenceTypeId", model.Id);
            collection.AddWithValue("@UserId", model.UserId);
        }
    }
}
