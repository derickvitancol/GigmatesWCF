using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
namespace GigmatesWCF
{
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

    [DataContract]
    public class Type
    {
        int TypeID;
        string TypeName;
        [DataMember]
        public int ID { get { return this.TypeID; } set { this.TypeID = value; } }
        [DataMember]
        public string Name { get { return this.TypeName; } set { this.TypeName = value; } }
    }

    [DataContract]
    public class Gig
    {
        string gigName;
        string gigDate;
        string gigVenue;
        string creatorID;
        string gigCreatorName;
        string gigCreatorUsername;
        int gigGenreID;
        string gigGenreName;
        int gigID;
        string gigStatus;
        

        [DataMember]
        public int GigID { get { return this.gigID; } set { this.gigID = value; } }
        [DataMember]
        public string Name { get { return this.gigName; } set { this.gigName = value; } }
        [DataMember]
        public string Date { get { return this.gigDate; } set { this.gigDate = value; } }
        [DataMember]
        public string Venue { get { return this.gigVenue; } set { this.gigVenue = value; } }
        [DataMember]
        public string Creator { get { return this.creatorID; } set { this.creatorID = value; } }
        [DataMember]
        public int GenreID { get { return this.gigGenreID; } set { this.gigGenreID = value; } }
        [DataMember]
        public string CreatorName { get { return this.gigCreatorName; } set { this.gigCreatorName = value; } }
        [DataMember]
        public string Status { get { return this.gigStatus; } set { this.gigStatus = value; } }
        [DataMember]
        public string CreatorUsername { get { return this.gigCreatorUsername; } set { this.gigCreatorUsername=value; } }
        [DataMember]
        public string GenreName { get { return this.gigGenreName; } set { this.gigGenreName = value; } }
    }

    [DataContract]
    public class Song
    {
        int songID;
        string songName;
        int mainArtistID;
        string mainArtist;
        string featureedArtist;
        int genreID;
        string description;

        [DataMember]
        public int ID { get { return this.songID; } set { this.songID = value; } }
        [DataMember]
        public string Name { get { return this.songName; } set { this.songName = value; } }
        [DataMember]
        public int MainArtistID { get { return this.mainArtistID; } set { this.mainArtistID = value; } }
        [DataMember]
        public string MainArtist { get { return this.mainArtist; } set { this.mainArtist = value; } }
        [DataMember]
        public string FeaturedArtist { get { return this.featureedArtist; } set { this.featureedArtist = value; } }
        [DataMember]
        public int Genre { get { return this.genreID; } set { this.genreID = value; } }
        [DataMember]
        public string Description { get { return this.description; } set { this.description = value; } }
    }


    [DataContract]
    public class Invite
    {
        int InviteID;
        string InviteMessage;
        int InviteReceiverID;
        int InviteSenderID;
        int InviteStatusID;
        string InviteStatusName;
        int InviteTypeID;
        string InviteTypeName;
        int InvitePurposeID;

        [DataMember]
        public int ID { set { this.InviteID = value; } get { return this.InviteID; } }

        [DataMember]
        public string Message {set{ this.InviteMessage = value; } get { return this.InviteMessage; } }

        [DataMember]
        public int ReceiverID { set { this.InviteReceiverID = value; } get { return this.InviteReceiverID; } }

        [DataMember]
        public int SenderID { set { this.InviteSenderID = value; } get { return this.InviteSenderID; } }

        [DataMember]
        public int StatusID { set { this.InviteStatusID = value; } get { return this.InviteStatusID; } }

        [DataMember]
        public string StatusName { set { this.InviteStatusName = value; } get { return this.InviteStatusName; } }

        [DataMember]
        public int TypeID { set { this.InviteTypeID = value; } get { return this.InviteTypeID; } }

        [DataMember]
        public string TypeName { set { this.InviteTypeName = value; } get { return this.InviteTypeName; } }

        [DataMember]
        public int PurposeID { set { this.InvitePurposeID = value; } get { return this.InvitePurposeID; } }

    }

}