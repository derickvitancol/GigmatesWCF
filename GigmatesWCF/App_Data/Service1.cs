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

       public string GetGenreList()
        {
            string resultString;
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                List<Type> genreList = new List<Type>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetGenre", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        conn.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Type musicGenre = new Type();
                            musicGenre.ID = int.Parse(reader["GenreID"].ToString());
                            musicGenre.Name = reader["GenreName"].ToString();
                            genreList.Add(musicGenre);
                        }

                        conn.Close();
                    }
                }

                resultString = JsonConvert.SerializeObject(genreList);
            }
            catch (Exception ex)
            {
                resultString = ex.Message;
            }


            return resultString;
        }
    
    
       public string AddGig(Gig newGig)
        {
            string resultString = "0";
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("AddGig", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("userID", SqlDbType.SmallInt).Value = newGig.Creator ;
                        command.Parameters.Add("gigDate", SqlDbType.DateTime).Value = DateTime.Parse(newGig.Date);
                        command.Parameters.Add("gigVenue", SqlDbType.NVarChar, 50).Value = newGig.Venue;
                        command.Parameters.Add("gigName", SqlDbType.NVarChar, 30).Value = newGig.Name;
                        command.Parameters.Add("gigGenreID", SqlDbType.TinyInt).Value = newGig.GenreID;
                        conn.Open();
                        int val = command.ExecuteNonQuery();
                        conn.Close();
                        resultString = val.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                resultString = ex.Message;

            }


            return resultString;
        }
      //FUNCTION TO ADD FUNCTION TO THE DATABASE
        public string AddSong(Song newSong)
        {
            string resultString = "0";
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("AddSong", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("songName", SqlDbType.VarChar, 30).Value = newSong.Name;
                        command.Parameters.Add("genreID", SqlDbType.TinyInt).Value = newSong.Genre;
                        command.Parameters.Add("mainartist", SqlDbType.SmallInt).Value = newSong.MainArtist;
                        command.Parameters.Add("collabArtist", SqlDbType.VarChar, 30).Value =ToDbNull(newSong.FeaturedArtist);
                        command.Parameters.Add("description", SqlDbType.NVarChar,50).Value = ToDbNull(newSong.Description);
                        conn.Open();
                        int val = command.ExecuteNonQuery();
                        conn.Close();
                        resultString = val.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                resultString = ex.Message;
            }
            return resultString;
        }


        public string GetSongs(int userID)
        {
            string resultString = "0";
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                List<Song> registeredSongList = new List<Song>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetSongs", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("userID", SqlDbType.SmallInt).Value = userID;
                        conn.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Song registeredSong = new Song();
                            registeredSong.Name = reader["Title"].ToString();
                            registeredSong.ID = int.Parse(reader["ID"].ToString());
                            registeredSong.Genre = int.Parse(reader["Genre"].ToString());
                            registeredSong.MainArtistID = int.Parse(reader["Main Artist"].ToString());
                            registeredSong.MainArtist = GetFullName(registeredSong.MainArtistID);
                            registeredSong.FeaturedArtist = reader["Collab Artist"].ToString();
                            registeredSong.Description = reader["Description"].ToString();
                            registeredSongList.Add(registeredSong);
                        }
                        conn.Close();
                    }
                }

                resultString = JsonConvert.SerializeObject(registeredSongList);
            }
            catch (Exception ex)
            {
                resultString = ex.Message;
            }

            return resultString;
        }

        public void GetSongsopt()
        { }

        public string GetAvailableGigs(int userID)
        {
            string resultString = "0";
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                List<Gig> AvailableGigList = new List<Gig>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetAvailableGigs", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("userID", SqlDbType.SmallInt).Value = userID;
                        conn.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Gig AvailableGig = new Gig();
                            AvailableGig.Name = reader["gigName"].ToString();
                            AvailableGig.GigID = int.Parse(reader["gigID"].ToString());
                            AvailableGig.Venue = reader["gigVenue"].ToString();
                            AvailableGig.Date = reader["gigDate"].ToString();
                            AvailableGig.CreatorUsername = reader["Creator Username"].ToString();
                            AvailableGig.CreatorName = reader["Creator Name"].ToString();
                            AvailableGig.Creator = int.Parse(reader["Creator ID"].ToString());
                            AvailableGig.GenreName = reader["Genre"].ToString();
                            AvailableGig.Status = reader["Gig Status"].ToString();
                            AvailableGigList.Add(AvailableGig);
                        }
                        conn.Close();
                    }
                }
                resultString = JsonConvert.SerializeObject(AvailableGigList);
            }
            catch (Exception ex)
            {
                resultString = ex.Message;
            }
            return resultString;
        }

        public void GetAvailableGigsopt() { }

        public string GetUserNotifs(int userID)
        {
            string resultString = "";
            return resultString;
        }
        //FUNCTON TO GET THE PERSON TYPES FOR THE APPLICATION
        public string GetPersonTypes()
        {
            string resultString = "0";
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                List<Type> personTypeList = new List<Type>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetUserTypes", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        conn.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Type personType = new Type();
                            personType.ID = int.Parse(reader["ID"].ToString());
                            personType.Name = reader["Name"].ToString();
                            personTypeList.Add(personType);
                        }

                        conn.Close();
                    }
                }

                resultString = JsonConvert.SerializeObject(personTypeList);
            }
            catch (Exception ex)
            {
                resultString = ex.Message;
            }
            return resultString;

        }

        //FUNCTION TO GET FULL NAME BASED ON THE ID GIVEN
        string GetFullName(int userID)
        {
            string fullname = "";
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetFullname", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("userID", SqlDbType.SmallInt).Value = userID;

                        conn.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            
                            fullname = reader["Full Name"].ToString();
                        }

                        conn.Close();

                    }
                }

            }
            catch (Exception ex)
            {
                fullname = ex.Message;
            }
            return fullname;
        }

        // FUNCTION TO SEND NOTIFICATION (GIG INVITE) TO USER
        public string SendGigInvite(Invite NewInvite)
        {
            string resultString = "0";
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SendGigInvite", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("senderID", SqlDbType.SmallInt).Value = NewInvite.SenderID;
                        command.Parameters.Add("receiverID", SqlDbType.SmallInt).Value = NewInvite.ReceiverID;
                        command.Parameters.Add("message", SqlDbType.NVarChar,100).Value = ToDbNull(NewInvite.Message);
                        command.Parameters.Add("inviteType", SqlDbType.TinyInt).Value = NewInvite.TypeID;
                        command.Parameters.Add("gigID", SqlDbType.Int).Value = NewInvite.PurposeID;
                        conn.Open();
                        int val = command.ExecuteNonQuery();
                        conn.Close();
                        resultString = val.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                resultString = ex.Message;
            }
            return resultString;
        }

        //FUNCTION TO SEND NOTIFICATION(PERSON INVITE) TO USER
        public string SendPersonInvite(Invite NewInvite)
        {
            string resultString = "0";
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SendPersonInvite", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("senderID", SqlDbType.SmallInt).Value = NewInvite.SenderID;
                        command.Parameters.Add("receiverID", SqlDbType.SmallInt).Value = NewInvite.ReceiverID;
                        command.Parameters.Add("message", SqlDbType.NVarChar, 100).Value = ToDbNull(NewInvite.Message);
                        command.Parameters.Add("inviteType", SqlDbType.TinyInt).Value = NewInvite.TypeID;
                        command.Parameters.Add("personID", SqlDbType.SmallInt).Value = NewInvite.PurposeID;
                        conn.Open();
                        int val = command.ExecuteNonQuery();
                        conn.Close();
                        resultString = val.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                resultString = ex.Message;
            }
            return resultString;
        }

        //FUNCTION TO SEND NOTIFICATION(BAND INVITE) TO USER
        public string SendBandInvite(Invite NewInvite)
        {
            string resultString = "0";
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SendBandInvite", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("senderID", SqlDbType.SmallInt).Value = NewInvite.SenderID;
                        command.Parameters.Add("receiverID", SqlDbType.SmallInt).Value = NewInvite.ReceiverID;
                        command.Parameters.Add("message", SqlDbType.NVarChar, 100).Value = ToDbNull(NewInvite.Message);
                        command.Parameters.Add("inviteType", SqlDbType.TinyInt).Value = NewInvite.TypeID;
                        command.Parameters.Add("bandID", SqlDbType.SmallInt).Value = NewInvite.PurposeID;
                        conn.Open();
                        int val = command.ExecuteNonQuery();
                        conn.Close();
                        resultString = val.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                resultString = ex.Message;
            }
            return resultString;
        }

        //FUNCTION TO JOIN A PERSON TO A GIG NEEDS TO BE FIXED 
        public string JoinGig(Gig AvailableGig, Person RegisteredMusician)
        {
            string resultString = "0";


            return resultString;

        }
        
        //FUNCTION TO EDIT THE PROFILE 
        public string EditProfile(Person EditPerson)
        {
            string resultString = "0";
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("EditProfile", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("userID", SqlDbType.SmallInt).Value = EditPerson.ID;
                        command.Parameters.Add("age", SqlDbType.SmallInt).Value = EditPerson.Age;
                        command.Parameters.Add("location", SqlDbType.VarChar,30).Value = EditPerson.Location;
                        command.Parameters.Add("password", SqlDbType.VarChar, 30).Value = EditPerson.Password;
                        command.Parameters.Add("bio", SqlDbType.NVarChar, 50).Value = ToDbNull(EditPerson.Bio);
                        conn.Open();
                        int val = command.ExecuteNonQuery();
                        conn.Close();
                        resultString = val.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                resultString = ex.Message;
            }

            return resultString;
        }

        public void GetUserDataByIDopt() { }

        //FUNCTION TO GET THE INVITES TO BE EVALUATED BY THE USER
        public string GetInvites(int userID)
        {
            string resultString= "0";

            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                List<Invite> InviteList = new List<Invite>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetInvites", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("userID", SqlDbType.SmallInt).Value = userID;
                        conn.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Invite collectedInvite = new Invite();
                            collectedInvite.ID = int.Parse(reader["inviteID"].ToString());
                            collectedInvite.SenderID = int.Parse(reader["senderID"].ToString());
                            collectedInvite.SenderName = reader["Sender Name"].ToString();
                            collectedInvite.TypeID = int.Parse(reader["Type ID"].ToString());
                            collectedInvite.TypeName = reader["Type Name"].ToString();
                            //GIG
                            if(collectedInvite.TypeID == 1)
                            {
                                collectedInvite.GigInvite = new Gig();
                                collectedInvite.GigInvite.Name = reader["gigName"].ToString();
                                collectedInvite.GigInvite.Date = reader["gigDate"].ToString();
                                collectedInvite.GigInvite.GigID = int.Parse(reader["gigID"].ToString());
                            }
                            //BAND
                            else if(collectedInvite.TypeID == 2)
                            {
                                //INSERT BAND HERE
                            }
                            //GIGMATE
                            else if(collectedInvite.TypeID == 3)
                            {
                               //INSERT FOR PERSON HERE 
                            }


                            InviteList.Add(collectedInvite);
                        }

                        conn.Close();
                    }
                }

                resultString = JsonConvert.SerializeObject(InviteList);
            }
            catch (Exception ex)
            {
                resultString = ex.Message;
            }

            return resultString;
        }
        public void GetInvitessopt() { }

        //FUNCTION TO SEND THE RESPONSE OF THE USER TO A (1)GIG INVITE (2)BAND INVITE OR (3)GIGMATE REQUEST
        public string InviteResponse(Invite inviteReceived)
        {
            string resultString = "0";
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InviteResponse", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("inviteID", SqlDbType.SmallInt).Value = inviteReceived.ID;
                        command.Parameters.Add("responseValue", SqlDbType.TinyInt).Value = inviteReceived.StatusID;
                        //IF INVITE TYPE IS 1 PARTICIPANT ID IS THE ID OF THE USER JOINING THE GIG 
                        // 2 FOR BAND
                        // IF INVITE TYPE IS 3 PARTICIPANT ID IS THE LOGGED IN USER
                        if(inviteReceived.TypeID == 1)
                        {
                            //GIG INVITE
                            command.Parameters.Add("participantID", SqlDbType.TinyInt).Value = inviteReceived.SenderID;
                        }
                        else if(inviteReceived.TypeID == 3)
                        {
                            //GIGMATE INVITE
                            command.Parameters.Add("participantID", SqlDbType.TinyInt).Value = inviteReceived.GigmateInvite.ID;
                        }
                        conn.Open();
                        int val = command.ExecuteNonQuery();
                        conn.Close();
                        resultString = val.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                resultString = ex.Message;

            }
            return resultString;
        }

        //FUNCTION CALLED TO GET THE GIGS THE USER CREATED OR A PARTICIPANT  
        public string GetGigsForProf(int userID)
        {
            string resultString = "0";
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetGigsForProf", conn))
                    {
                        List<Gig> collectedGigList = new List<Gig>();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("userID", SqlDbType.SmallInt).Value = userID;

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Gig collectedGig = new Gig();
                            collectedGig.GigID = int.Parse(reader["gigID"].ToString());
                            collectedGig.Name = reader["gigName"].ToString();
                            collectedGig.Venue = reader["gigVenue"].ToString();
                            collectedGig.Date = reader["gigDate"].ToString();
                            collectedGig.GenreID = int.Parse(reader["gigGenre"].ToString());
                            collectedGig.GenreName = reader["genreName"].ToString();
                            collectedGig.CreatorName = reader["Creator Name"].ToString();
                            collectedGig.Creator = int.Parse(reader["creatorID"].ToString());
                            collectedGig.Status = reader["GigStatusName"].ToString();
                            collectedGigList.Add(collectedGig);
                        }
                        conn.Close();
                        resultString = JsonConvert.SerializeObject(collectedGigList);
                    }
                }

            }
            catch (Exception ex)
            {
                resultString = ex.Message;
            }

            return resultString;
        }

        public void GetGigsForProfopt() { }

        public string GetUserDataByID(int userID)
        {
            string resultString = "0";
            Person PersonDetails = new Person();
            string connectionString = ConfigurationManager.ConnectionStrings["GigmatesDB"]?.ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetUserDataByID", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("userID", SqlDbType.SmallInt).Value = userID;
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while(reader.Read())
                        {
                            
                            PersonDetails.Age =int.Parse(reader["Age"].ToString());
                            PersonDetails.Firstname = reader["Firstname"].ToString();
                            PersonDetails.Lastname = reader["Lastname"].ToString();
                            PersonDetails.ID = int.Parse(reader["ID"].ToString());
                            PersonDetails.Gender = int.Parse(reader["genderID"].ToString());
                            PersonDetails.Password = reader["Password"].ToString();
                            PersonDetails.Location = reader["Location"].ToString();
                            PersonDetails.Username = reader["Username"].ToString();
                            PersonDetails.Bio = reader["Bio"].ToString();
                            PersonDetails.Rate = int.Parse(reader["Rate"].ToString());
                            PersonDetails.Type = int.Parse(reader["Type ID"].ToString());
                        }
                        conn.Close();
                        resultString = JsonConvert.SerializeObject(PersonDetails);
                    }
                }

            }
            catch (Exception ex)
            {
                resultString = ex.Message;

            }
            return resultString;
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

}