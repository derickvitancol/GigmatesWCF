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
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string derick();

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string Login(Person personLog);


        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped,RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string Register(Person newPerson);

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetGenreList();


        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string AddGig(Gig newGig);

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string AddSong(Song newSong);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,UriTemplate="/GetSongs")]
        string GetSongs(int userID);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/GetSongs")]
        void GetSongsopt();

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetUserNotifs(int userID);

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetPersonTypes();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetAvailableGigs")]
        string GetAvailableGigs(int userID);
        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/GetAvailableGigs")]
        void GetAvailableGigsopt();

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string JoinGig(Gig AvailableGig,Person RegisteredMusician);

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SendGigInvite(Invite NewInvite);

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SendPersonInvite(Invite NewInvite);

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SendBandInvite(Invite NewInvite);

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string EditProfile(Person EditPerson);

        //SEARCH FOR GIG PERSON 
        //ADD GIGMATE(YUNG PARANG FRIENDS)
        //EDIT PROFILE
        //GIGHISTORY
        //GET NOTIFS
        //GET UPCOMING GIGS
        //GET GIGMATES
    }
}
