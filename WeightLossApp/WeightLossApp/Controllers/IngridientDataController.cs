using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WeightLossApp.Models;
using System.Collections.Generic;

namespace WeightLossApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngridientDataController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public IngridientDataController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ID, Name, Calories, Proteins, Carbohydrates, Fats
                            FROM dbo.IngridientData
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DbConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(IngridientData ingridientData)
        {
            string query = @"
                            insert into dbo.IngridientData 
                            values (@Name, @Calories, @Proteins, @Carbohydrates, @Fats)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DbConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Name", ingridientData.Name);
                    myCommand.Parameters.AddWithValue("@Calories", ingridientData.Calories);
                    myCommand.Parameters.AddWithValue("@Proteins", ingridientData.Proteins);
                    myCommand.Parameters.AddWithValue("@Carbohydrates", ingridientData.Carbohydrates);
                    myCommand.Parameters.AddWithValue("@Fats", ingridientData.Fats);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added successfully");
        }

        [HttpPut]
        public JsonResult Put(IngridientData ingridientData)
        {
            string query = @"
                            update dbo.IngridientData 
                            set Name = @Name, Calories = @Calories, Proteins = @Proteins, Carbohydrates = @Carbohydrates, Fats = @Fats
                            where ID=@ID
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DbConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", ingridientData.ID);
                    myCommand.Parameters.AddWithValue("@Name", ingridientData.Name);
                    myCommand.Parameters.AddWithValue("@Calories", ingridientData.Calories);
                    myCommand.Parameters.AddWithValue("@Proteins", ingridientData.Proteins);
                    myCommand.Parameters.AddWithValue("@Carbohydrates", ingridientData.Carbohydrates);
                    myCommand.Parameters.AddWithValue("@Fats", ingridientData.Fats);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.IngridientData 
                            where ID=@ID
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DbConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
