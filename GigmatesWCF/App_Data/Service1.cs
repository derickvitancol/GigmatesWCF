using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Newtonsoft.Json;


namespace GigmatesWCF
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 : IGigmatesService
    {
        

        public string derick()
        {
            return "derick";
        }


        public string Login(Person personLog)
        {
           
            Person logPerson = new Person();
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            string resultString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SignIn", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("username", SqlDbType.NVarChar, 20).Value = personLog.Username;
                        command.Parameters.Add("password", SqlDbType.NVarChar, 20).Value = personLog.Password;

                        conn.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            logPerson.ID = int.Parse(reader[0].ToString());
                            logPerson.Username = reader[1].ToString();
                            logPerson.Password = reader[2].ToString();
                            logPerson.Age = int.Parse(reader[3].ToString());
                            logPerson.Rate = float.Parse(reader[4].ToString());
                            logPerson.Bio = reader[5].ToString();
                            logPerson.Location = reader[6].ToString();
                            logPerson.Type = int.Parse(reader[7].ToString());
                            logPerson.Firstname = reader[8].ToString();
                            logPerson.Lastname = reader[9].ToString();
                            logPerson.Gender = int.Parse(reader[10].ToString());
                        }

                        conn.Close();
                    }
                }

                resultString = JsonConvert.SerializeObject(logPerson);
            }
            catch (Exception ex)
            {
                resultString = ex.Message;
            }


            return resultString;

        }

        public string Register(Person newPerson)
        {


            string rowAffected = "0";
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("RegisterPerson", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("username", SqlDbType.NVarChar, 20).Value = newPerson.Username;
                        command.Parameters.Add("password", SqlDbType.NVarChar, 20).Value = newPerson.Password;
                        command.Parameters.Add("bio", SqlDbType.NVarChar, 50).Value = ToDbNull(newPerson.Bio);
                        command.Parameters.Add("rate", SqlDbType.Float).Value = ToDbNull(newPerson.Rate);
                        command.Parameters.Add("personType", SqlDbType.TinyInt).Value = newPerson.Type;
                        command.Parameters.Add("age", SqlDbType.SmallInt).Value = newPerson.Age;
                        command.Parameters.Add("location", SqlDbType.VarChar, 30).Value = ToDbNull(newPerson.Location);
                        command.Parameters.Add("fname", SqlDbType.VarChar, 20).Value = newPerson.Firstname;
                        command.Parameters.Add("lname", SqlDbType.VarChar, 20).Value = newPerson.Lastname;
                        command.Parameters.Add("gender", SqlDbType.TinyInt).Value = newPerson.Gender;
                        conn.Open();


                        int val = command.ExecuteNonQuery();
                        conn.Close();
                        rowAffected = val.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                string err = ex.Message;
                rowAffected = ex.Message;
            }
            return rowAffected;
        }

        //FOR NULL ENTRIES TO THE DATABASE 
        object ToDbNull(object obj)
        {
            object dbval = obj;
            if (dbval == null)
            {
                dbval = DBNull.Value;
            }
            return dbval;
        }
    }

    //CODE ADDED FOR CORS

    //public class CustomHeaderMessageInspector : IDispatchMessageInspector
    //{
    //    Dictionary<string, string> requiredHeaders;
    //    public CustomHeaderMessageInspector(Dictionary<string, string> headers)
    //    {
    //        requiredHeaders = headers ?? new Dictionary<string, string>();
    //    }

    //    public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
    //    {
    //        return null;
    //    }

    //    public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
    //    {
    //        var httpHeader = reply.Properties["httpResponse"] as HttpResponseMessageProperty;
    //        foreach (var item in requiredHeaders)
    //        {
    //            httpHeader.Headers.Add(item.Key, item.Value);
    //        }
    //    }
    //}

    //public class EnableCrossOriginResourceSharingBehavior : BehaviorExtensionElement, IEndpointBehavior
    //{
    //    public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
    //    {

    //    }

    //    public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
    //    {

    //    }

    //    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
    //    {
    //        var requiredHeaders = new Dictionary<string, string>();

    //        requiredHeaders.Add("Access-Control-Allow-Origin", "*");
    //        requiredHeaders.Add("Access-Control-Request-Method", "POST,GET,PUT,DELETE,OPTIONS");
    //        requiredHeaders.Add("Access-Control-Allow-Headers", "X-Requested-With,Content-Type");

    //        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new CustomHeaderMessageInspector(requiredHeaders));
    //    }

    //    public void Validate(ServiceEndpoint endpoint)
    //    {

    //    }

    //    public override Type BehaviorType
    //    {
    //        get { return typeof(EnableCrossOriginResourceSharingBehavior); }
    //    }

    //    protected override object CreateBehavior()
    //    {
    //        return new EnableCrossOriginResourceSharingBehavior();
    //    }
    //}


}