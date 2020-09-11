using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace GigmatesWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    [ServiceContract]
    public interface IGigmatesService
    {

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string derick();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Person Login(string username, string password);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string Register(Person newPerson);
    }

    [DataContract]
    public class Person
    {
        string username,
               password,
               firstname,
               lastname,
               location,
               bio;
        int age,
            id,
            type,
            gender;

        float rate;

        [DataMember]
        public string Username { get { return this.username; } set { this.username = value; } }
        [DataMember]
        public string Password { get { return this.password; } set { this.password = value; } }
        [DataMember]
        public string Firstname { get { return this.firstname; } set { this.firstname = value; } }
        [DataMember]
        public string Lastname { get { return this.lastname; } set { this.lastname = value; } }
        [DataMember]
        public string Location { get { return this.location; } set { this.location = value; } }
        [DataMember]
        public string Bio { get { return this.bio; } set { this.bio = value; } }
        [DataMember]
        public int Age { get { return this.age; } set { this.age = value; } }
        [DataMember]
        public int ID { get { return this.id; } set { this.id = value; } }
        [DataMember]
        public int Type { get { return this.type; } set { this.type = value; } }
        [DataMember]
        public float Rate { get { return this.rate; } set { this.rate = value; } }
        [DataMember]
        public int Gender { get { return this.gender; } set { this.gender = value; } }
    }
}
