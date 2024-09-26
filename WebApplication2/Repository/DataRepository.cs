using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class DataRepository
    {
        public int AddUser(User user)
        { 
            Db_Connectivity dc = new Db_Connectivity();
            try
            {
                using (IDbConnection db = new SqlConnection(dc.ConnectionString))
                {
                    db.Open();
                    DynamicParameters dyParam = new DynamicParameters();
                    dyParam.Add("@name", user.Name, direction: ParameterDirection.Input);
                    dyParam.Add("@email", user.Email, direction: ParameterDirection.Input);
                    dyParam.Add("@createdat", user.CreatedAt, direction: ParameterDirection.Input);
                    const string sp = "AddUser";
                    var result = db.Execute(sp, dyParam, commandType: CommandType.StoredProcedure);
                    db.Close();
                    return result;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public List<User> GetUsers()
        {
            Db_Connectivity dc= new Db_Connectivity();
            try
            {
                using (IDbConnection db = new SqlConnection(dc.ConnectionString))
                {
                    db.Open();
                    //DynamicParameters dyParam = new DynamicParameters();
                    List<User> users = SqlMapper.Query<User>(

                        db, "GetUser").ToList();
                    db.Close();
                    return users;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
    }
}
