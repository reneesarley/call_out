using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CallOut.Models
{
    public class Politician
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }


        public Politician( string firstNameInput, string lastNameInput, int id = 0)
        {
            firstName = firstNameInput;
            lastName = lastNameInput;
        }



        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO politicians (first_name, last_name) VALUES (@FirstName, @Lastname);";

            MySqlParameter firstNameParameter = new MySqlParameter();
            firstNameParameter.ParameterName = "@FirstName";
            firstNameParameter.Value = firstName;
            cmd.Parameters.Add(firstNameParameter);

            MySqlParameter lastNameParameter = new MySqlParameter();
            lastNameParameter.ParameterName = "@LastName";
            lastNameParameter.Value = lastName;
            cmd.Parameters.Add(lastNameParameter);


            cmd.ExecuteNonQuery();
            id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }

        public static List<Politician> GetAll()
        {
            List<Politician> allPoliticians = new List<Politician> { };

            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM politicians;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int idRdr = rdr.GetInt32(0);
                string firstNameRdr = rdr.GetString(1);
                string lastNameRdr = rdr.GetString(2);
                Politician newPolitician = new Politician(firstNameRdr, lastNameRdr, idRdr);
                allPoliticians.Add(newPolitician);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allPoliticians;

        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM politicians;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherPolitician)
        {
            if (!(otherPolitician is Politician))
            {
                return false;
            }
            else
            {
                Politician newPolitician = (Politician)otherPolitician;
                bool firstNameEquality = this.firstName.Equals(newPolitician.firstName);
                bool lastNameEquality = this.lastName.Equals(newPolitician.lastName);     
                return (firstNameEquality && lastNameEquality);
            }
        }

    }
}
