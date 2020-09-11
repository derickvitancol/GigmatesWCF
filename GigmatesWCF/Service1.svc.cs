using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GigmatesWCF
{
    // //NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // //NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    //public class Service1 : IGigmatesService
    //{
    //    //public string GetData(int value)
    //    //{
    //    //    return string.Format("You entered: {0}", value);
    //    //}

    //    //public CompositeType GetDataUsingDataContract(CompositeType composite)
    //    //{
    //    //    if (composite == null)
    //    //    {
    //    //        throw new ArgumentNullException("composite");
    //    //    }
    //    //    if (composite.BoolValue)
    //    //    {
    //    //        composite.StringValue += "Suffix";
    //    //    }
    //    //    return composite;
    //    //}

    //    //public string greetings()
    //    //{
    //    //    return "Hello werld";
    //    //}

    //    public string derick()
    //    {
    //        return "yow derick";
    //    }



    //    public Person Login(string username, string password)
    //    {
    //        Person logPerson = new Person();
    //        string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
    //        try
    //        {
    //            using (SqlConnection conn = new SqlConnection(connectionString))
    //            {
    //                using (SqlCommand command = new SqlCommand("SignIn", conn))
    //                {
    //                    command.CommandType = CommandType.StoredProcedure;
    //                    command.Parameters.Add("username", SqlDbType.NVarChar, 20).Value = username;
    //                    command.Parameters.Add("password", SqlDbType.NVarChar, 20).Value = password;

    //                    conn.Open();

    //                    SqlDataReader reader = command.ExecuteReader();
    //                    while (reader.Read())
    //                    {
    //                        logPerson.ID = int.Parse(reader[0].ToString());
    //                        logPerson.Username = reader[1].ToString();
    //                        logPerson.Password = reader[2].ToString();
    //                        logPerson.Age = int.Parse(reader[3].ToString());
    //                        logPerson.Rate = float.Parse(reader[4].ToString());
    //                        logPerson.Bio = reader[5].ToString();
    //                        logPerson.Location = reader[6].ToString();
    //                        logPerson.Type = int.Parse(reader[7].ToString());
    //                        logPerson.Firstname = reader[8].ToString();
    //                        logPerson.Lastname = reader[9].ToString();
    //                        logPerson.Gender = int.Parse(reader[10].ToString());
    //                    }

    //                    conn.Close();
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            string err = ex.Message;
    //        }
    //        return logPerson;

    //    }

    //    public string Register(Person newPerson)
    //    {
         
    //        string rowAffected = "0";
    //        string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
    //        try
    //        {
    //            using (SqlConnection conn = new SqlConnection(connectionString))
    //            {
    //                using (SqlCommand command = new SqlCommand("RegisterPerson", conn))
    //                {
    //                    command.CommandType = CommandType.StoredProcedure;
    //                    command.Parameters.Add("username", SqlDbType.NVarChar, 20).Value = newPerson.Username;
    //                    command.Parameters.Add("password", SqlDbType.NVarChar, 20).Value = newPerson.Password;
    //                    command.Parameters.Add("bio", SqlDbType.NVarChar, 50).Value = ToDbNull(newPerson.Bio);
    //                    command.Parameters.Add("rate", SqlDbType.Float).Value = ToDbNull(newPerson.Rate);
    //                    command.Parameters.Add("personType", SqlDbType.TinyInt).Value = newPerson.Type;
    //                    command.Parameters.Add("age", SqlDbType.SmallInt).Value = newPerson.Age;
    //                    command.Parameters.Add("location", SqlDbType.VarChar, 30).Value = ToDbNull(newPerson.Location);
    //                    command.Parameters.Add("fname", SqlDbType.VarChar, 20).Value = newPerson.Firstname;
    //                    command.Parameters.Add("lname", SqlDbType.VarChar, 20).Value = newPerson.Lastname;
    //                    command.Parameters.Add("gender", SqlDbType.TinyInt).Value = newPerson.Gender;
    //                    conn.Open();


    //                    int val = command.ExecuteNonQuery();
    //                    conn.Close();
    //                    rowAffected = val.ToString();
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            string err = ex.Message;
    //            rowAffected = err;
    //        }
    //        return rowAffected;
    //    }

    //    //FOR NULL ENTRIES TO THE DATABASE 
    //    object ToDbNull(object obj)
    //    {
    //        object dbval = obj;
    //        if (dbval == null)
    //        {
    //            dbval = DBNull.Value;
    //        }
    //        return dbval;
    //    }
    //}

}
