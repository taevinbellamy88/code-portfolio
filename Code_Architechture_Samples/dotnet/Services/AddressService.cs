using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models.Domain.Addresses;
using Sabio.Models.Requests.Addresses;
using Sabio.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sabio.Services
{
    public class AddressService : IAddressService
    {
        IDataProvider _data = null;

        public AddressService(IDataProvider data)
        {
            _data = data;

        }



        #region CRUD

        public int Add(AddressAddRequest model, int userId)
        {
            int id = 0;

            string procName = "[dbo].[Sabio_Addresses_Insert]";


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

                    }
                        );

            return id;

        }


        public void Update(AddressUpdateRequest model, int userId)
        {

            string procName = "[dbo].[Sabio_Addresses_Update]";


            _data.ExecuteNonQuery(procName
                , inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    AddCommonParams(model, collection);

                    collection.AddWithValue("@Id", model.Id);
                }
                , returnParameters: null);

        }
        
        public void Delete(int id)
        {
            string procName = "[dbo].[Sabio_Addresses_DeleteById]";


            _data.ExecuteNonQuery(procName,
                            inputParamMapper: delegate (SqlParameterCollection paramCollection)
                            {
                                paramCollection.AddWithValue("@Id", id);
                            });
        }



        public Address Get(int id)
        {
            Address address = null;


            string procName = "[dbo].[Sabio_Addresses_SelectById]";

            _data.ExecuteCmd(procName,
                            inputParamMapper: delegate (SqlParameterCollection paramCollection)
                            {

                                paramCollection.AddWithValue("@Id", id);
                            },
                            singleRecordMapper: delegate (IDataReader reader, short set) //single record mapper
                            {

                                address = MapSingleAddress(reader);
                            });

            return address;
        }


        public List<Address> SelectRandom50()
        {
            List<Address> list = null;
            string procName = "[dbo].[Sabio_Addresses_SelectRandom50]";

            _data.ExecuteCmd(procName
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    Address address = MapSingleAddress(reader);

                    if (list == null)
                    {
                        list = new List<Address>();
                    }

                    list.Add(address);
                });

            return list;
        }

        #endregion



        private static Address MapSingleAddress(IDataReader reader)
        {
            Address aAddress = new Address();

            int startingIdx = 0;

            aAddress.Id = reader.GetSafeInt32(startingIdx++);
            aAddress.LineOne = reader.GetSafeString(startingIdx++);
            aAddress.SuiteNumber = reader.GetSafeInt32(startingIdx++);
            aAddress.City = reader.GetSafeString(startingIdx++);
            aAddress.State = reader.GetSafeString(startingIdx++);
            aAddress.PostalCode = reader.GetSafeString(startingIdx++);
            aAddress.IsActive = reader.GetSafeBool(startingIdx++);
            aAddress.Lat = reader.GetSafeDouble(startingIdx++);
            aAddress.Long = reader.GetSafeDouble(startingIdx++);

            return aAddress;
        }

        private static void AddCommonParams(AddressAddRequest model, SqlParameterCollection collection)
        {
            collection.AddWithValue("@LineOne", model.LineOne);
            collection.AddWithValue("@SuiteNumber", model.SuiteNumber);
            collection.AddWithValue("@City", model.City);
            collection.AddWithValue("@State", model.State);
            collection.AddWithValue("@PostalCode", model.PostalCode);
            collection.AddWithValue("@IsActive", model.IsActive);
            collection.AddWithValue("@Lat", model.Lat);
            collection.AddWithValue("@Long", model.Long);
        }
    }
}
