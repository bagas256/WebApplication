using WebApplication.Connections;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication.Library
{
    public class Users
    {
        private ConfigurationUSERMANAGEMENT DbConfiguration = new ConfigurationUSERMANAGEMENT();

        public DataRow GetUser(string email, string username, string password)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                string query = @"SELECT top 1 * FROM NEFSALIEN.dbo.[User] a inner join NEFSALIEN.dbo.UserRole b on a.RoleId = b.RoleId where a.username = @username and a.Password = @password";
                //param.Add(new SqlParameter("@email", email));
                param.Add(new SqlParameter("@username", username));
                param.Add(new SqlParameter("@password", password));
                DataRow data = SqlHelper.ExecuteDataset(DbConfiguration.getConnectionString(), CommandType.Text, query, param.ToArray()).Tables[0].Rows[0];
                return data;
            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataRowCollection UpdateUserLastLogin(string username)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                string query = "UPDATE NEFSALIEN.dbo.[User] SET LastLogin = getdate() WHERE Username = @username";
                param.Add(new SqlParameter("@username", username));
                DataRowCollection data = SqlHelper.ExecuteDataset(DbConfiguration.getConnectionString(), CommandType.Text, query, param.ToArray()).Tables[0].Rows;
                return data;
            }
            catch
            {
                return null;
            }
        }

        public DataRowCollection LogUserLogin(string username)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                string query = "INSERT INTO NEFSALIEN.dbo.UserLoginHistory (Username, Date, Activity) VALUES (@username, getdate(), 'Login')";
                param.Add(new SqlParameter("@username", username));
                DataRowCollection data = SqlHelper.ExecuteDataset(DbConfiguration.getConnectionString(), CommandType.Text, query, param.ToArray()).Tables[0].Rows;
                return data;
            }
            catch
            {
                return null;
            }
        }

        public DataRowCollection LogUserLogout(string username)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                string query = "INSERT INTO NEFSALIEN.dbo.UserLoginHistory (Username, Date, Activity) VALUES (@username, getdate(), 'Logout')";
                param.Add(new SqlParameter("@username", username));
                DataRowCollection data = SqlHelper.ExecuteDataset(DbConfiguration.getConnectionString(), CommandType.Text, query, param.ToArray()).Tables[0].Rows;
                return data;
            }
            catch
            {
                return null;
            }
        }
        public DataRowCollection GetDataUser()
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                string query = "SELECT a.*, b.RoleName FROM NEFSALIEN.dbo.[User] a LEFT JOIN NEFSALIEN.dbo.UserRole b on a.RoleId = b.RoleId;";
                //param.Add(new SqlParameter("@username", username));
                DataRowCollection data = SqlHelper.ExecuteDataset(DbConfiguration.getConnectionString(), CommandType.Text, query, param.ToArray()).Tables[0].Rows;
                return data;
            }
            catch (Exception err)
            {

                throw err;
            }
        }
        public DataRowCollection GetDataUserByID(string UserID)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                string query = "SELECT a.*, b.RoleName FROM NEFSALIEN.dbo.[User] a LEFT JOIN NEFSALIEN.dbo.UserRole b on a.RoleId = b.RoleId where a.UserId ='"+UserID+"'";
                //param.Add(new SqlParameter("@username", username));
                DataRowCollection data = SqlHelper.ExecuteDataset(DbConfiguration.getConnectionString(), CommandType.Text, query, param.ToArray()).Tables[0].Rows;
                return data;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public DataRowCollection GetAllRole()
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                string query = "SELECT * FROM NEFSALIEN.dbo.[UserRole] WHERE RoleName != 'super.admin' AND RoleName !='Kekasih'";
                DataRowCollection data = SqlHelper.ExecuteDataset(DbConfiguration.getConnectionString(), CommandType.Text, query, param.ToArray()).Tables[0].Rows;
                return data;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public DataRowCollection GetRole(string RoleName)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                string query = "SELECT * FROM NEFSALIEN.dbo.[UserRole] WHERE RoleName ='"+RoleName+"'";
                DataRowCollection data = SqlHelper.ExecuteDataset(DbConfiguration.getConnectionString(), CommandType.Text, query, param.ToArray()).Tables[0].Rows;
                return data;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public int InsertUser(string FirstName, string LastName, string Email, string PhoneNumber, string Username, string Password, string RoleId, DateTime CreatedAt, string CreatedBy)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                string query = "INSERT INTO NEFSALIEN.dbo.[User] (FirstName, LastName, Email, PhoneNumber, Username, Password, RoleId, CreatedAt, CreatedBy) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Username, @Password, @RoleId, @CreatedAt, @CreatedBy)";
                param.Add(new SqlParameter("@FirstName", FirstName));
                param.Add(new SqlParameter("@LastName", LastName));
                param.Add(new SqlParameter("@Email", Email));
                param.Add(new SqlParameter("@PhoneNumber", PhoneNumber));
                param.Add(new SqlParameter("@Username", Username));
                param.Add(new SqlParameter("@Password", Password));
                param.Add(new SqlParameter("@RoleId", RoleId));
                param.Add(new SqlParameter("@CreatedAt", CreatedAt));
                param.Add(new SqlParameter("@CreatedBy", CreatedBy));
                int data = SqlHelper.ExecuteNonQuery(DbConfiguration.getConnectionString(), CommandType.Text, query, param.ToArray());
                return data;
            }
            catch (Exception err)
            {
                return 0;
            }
        }
        public int EditUser(string UserId, string FirstName, string LastName, string Email, string PhoneNumber, string RoleId, DateTime ModifiedAt, string ModifiedBy)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                string query = "UPDATE NEFSALIEN.dbo.[User] SET " +
                    "FirstName = @FirstName, " +
                    "LastName = @LastName, " +
                    "Email = @Email, " +
                    "PhoneNumber = @PhoneNumber, " +
                    "RoleId = @RoleId, " +
                    "ModifiedAt = @ModifiedAt, " +
                    "ModifiedBy = @ModifiedBy " +
                    "WHERE UserId = '"+UserId+"'";
                param.Add(new SqlParameter("@FirstName", FirstName));
                param.Add(new SqlParameter("@LastName", LastName));
                param.Add(new SqlParameter("@Email", Email));
                param.Add(new SqlParameter("@PhoneNumber", PhoneNumber));
                param.Add(new SqlParameter("@RoleId", RoleId));
                param.Add(new SqlParameter("@ModifiedAt", ModifiedAt));
                param.Add(new SqlParameter("@ModifiedBy", ModifiedBy));
                int data = SqlHelper.ExecuteNonQuery(DbConfiguration.getConnectionString(), CommandType.Text, query, param.ToArray());
                return data;
            }
            catch (Exception err)
            {
                return 0;
            }
        }
        public DataTable GenerateDataUser()
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                string query = "SELECT a.*, b.RoleName FROM NEFSALIEN.dbo.[User] a LEFT JOIN NEFSALIEN.dbo.UserRole b on a.RoleId = b.RoleId;";
                //param.Add(new SqlParameter("@username", username));
                DataTable data = SqlHelper.ExecuteDataset(DbConfiguration.getConnectionString(), CommandType.Text, query, param.ToArray()).Tables[0];
                return data;
            }
            catch (Exception err)
            {

                throw err;
            }
        }
    }
}