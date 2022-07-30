using Sabio.Data.Providers;
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

using Sabio.Models.Domain;
using Sabio.Models.CodeChallenge.Requests;
using Sabio.Models.CodeChallenge.Domain;

namespace Sabio.Services.CodeChallenge
{
    public class CourseService : ICourseService
    {
        IDataProvider _data = null;
      
        public CourseService(IDataProvider data)
        {
            _data = data;

        }




        public int Create(CourseAddRequest model)
        {
            int id = 0;

            string procName = "[dbo].[Courses_Insert]";


            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                Mapper(model, collection);

                //and 1 OUTPUT

                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;

                collection.Add(idOut);

            }
            , returnParameters: delegate (SqlParameterCollection returnCollection)
            {

                object receivedId = returnCollection["@Id"].Value;

                int.TryParse(receivedId.ToString(), out id);

                Console.WriteLine($"New Courses Id: {id}");

            });

            return id;

        }

        public Course GetById(int id)
        {
            Course course = null;

            string procName = "[dbo].[Courses_SelectById]";

            _data.ExecuteCmd(procName,
                            inputParamMapper: delegate (SqlParameterCollection paramCollection)
                            {
                                paramCollection.AddWithValue("@Id", id);
                            },
                            singleRecordMapper: delegate (IDataReader reader, short set) //single record mapper
                            {
                                course = CourseMapper(reader);

                            });
            return course;
        }

        public void Update(CourseUpdateRequest model)
        {

            string procName = "[dbo].[Courses_Update]";


            _data.ExecuteNonQuery(procName
                , inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    UpdateMapper(model, collection);

                }
                , returnParameters: null);

        }

        public void Delete(int id)
        {
            string procName = "[dbo].[Students_Delete]";


            _data.ExecuteNonQuery(procName,
                            inputParamMapper: delegate (SqlParameterCollection paramCollection)
                            {
                                paramCollection.AddWithValue("@Id", id);
                            });
        }


        public Paged<Course> GetByPage(int pageIndex, int pageSize)
        {

            string procName = "[dbo].[Courses_Pagination]";

            Paged<Course> pagedList = null;
            List<Course> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName
                            , inputParamMapper: delegate (SqlParameterCollection param)
                            {
                                param.AddWithValue("@PageIndex", pageIndex);
                                param.AddWithValue("@PageSize", pageSize);
                            }
                            , singleRecordMapper: delegate (IDataReader reader, short set)
                            {
                                Course aCourse = CourseMapper(reader);

                                totalCount = reader.GetSafeInt32(6);

                                if (list == null)
                                {
                                    list = new List<Course>();
                                }

                                list.Add(aCourse);
                            });
            if (list != null)
            {
                pagedList = new Paged<Course>(list, pageIndex, pageSize, totalCount);
            }


            return pagedList;

        }






        private static void UpdateMapper(CourseUpdateRequest model, SqlParameterCollection collection)
        {
            collection.AddWithValue("@Id", model.Id);
            collection.AddWithValue("@Name", model.Name);
            collection.AddWithValue("@Description", model.Description);
            collection.AddWithValue("@SeasonTermId", model.SeasonTermId);
            collection.AddWithValue("@TeacherId", model.TeacherId);
        }

        private static Course CourseMapper(IDataReader reader)
        {
            Course aCourse = new Course();

            int startingIdx = 0;

            aCourse.Id = reader.GetSafeInt32(startingIdx++);

            aCourse.Name = reader.GetSafeString(startingIdx++);
            aCourse.Description = reader.GetSafeString(startingIdx++);
            aCourse.SeasonTerm = reader.GetSafeString(startingIdx++);
            aCourse.Teacher = reader.GetSafeString(startingIdx++);
            aCourse.Students = reader.DeserializeObject<List<Students>>(startingIdx++);

            return aCourse;
        }

        private static void Mapper(CourseAddRequest model, SqlParameterCollection collection)
        {
            collection.AddWithValue("@Name", model.Name);
            collection.AddWithValue("@Description", model.Description);
            collection.AddWithValue("@SeasonTermId", model.SeasonTermId);
            collection.AddWithValue("@TeacherId", model.TeacherId);
        }
    }
}
