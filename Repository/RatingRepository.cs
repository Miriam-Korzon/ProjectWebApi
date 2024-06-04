using Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace Repositories 
{ 
    public class RatingRepository : IRatingRepository
    {
        private readonly IConfiguration _configuration;

        public RatingRepository(IConfiguration configuration)
        {
           _configuration = configuration;            
        }

        public async Task AddRating(Rating rating)
        {
            
            string query = "INSERT INTO Rating(HOST, METHOD, PATH, REFERER, USER_AGENT, Record_Date) " +
                "VALUES(@Host, @Method, @Path, @Referer, @UserAgent, @RecordDate)";
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Store")))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add("@Host", SqlDbType.NVarChar, 50).Value = rating.Host;
                cmd.Parameters.Add("@Method", SqlDbType.NChar, 10).Value = rating.Method;
                cmd.Parameters.Add("@Path", SqlDbType.NVarChar, 50).Value = rating.Path;
                cmd.Parameters.Add("@Referer", SqlDbType.NVarChar, 100).Value = rating.Referer;
                cmd.Parameters.Add("@UserAgent", SqlDbType.NVarChar,300).Value = rating.UserAgent;
                cmd.Parameters.Add("@RecordDate", SqlDbType.DateTime).Value = rating.RecordDate;

                con.Open();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            return;
        }

    }


}
