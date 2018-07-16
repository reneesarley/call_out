using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CallOut.Models
{
    public class Offense
    {
        public int id { get; set; }
        public int politicianId { get; set; }
        public string description { get; set; }

        public Offense(int polId, string descr, int id = 0)
        {
            politicianId = polId;
            description = descr;
        }

        public void Save(List<int> typeList)
        {

            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO offenses (politician_Id, description) VALUES (@PoliticianId, @Description);";

            MySqlParameter politicianIdParameter = new MySqlParameter();
            politicianIdParameter.ParameterName = "@PoliticianId";
            politicianIdParameter.Value = politicianId;
            cmd.Parameters.Add(politicianIdParameter);

            MySqlParameter descriptionParameter = new MySqlParameter();
            descriptionParameter.ParameterName = "@Description";
            descriptionParameter.Value = description;
            cmd.Parameters.Add(descriptionParameter);

            cmd.ExecuteNonQuery();
            id = (int)cmd.LastInsertedId;

            for (int i = 0; i < typeList.Count; i++ )
            {
                cmd.CommandText = @"INSERT INTO offenses_types (offense_id, type_id) VALUES (@Offense_Id, @Type_Id);";

                MySqlParameter offenseIdParameter = new MySqlParameter();
                offenseIdParameter.ParameterName = "@Offense_id";
                offenseIdParameter.Value = id;
                cmd.Parameters.Add(offenseIdParameter);

                MySqlParameter typeIdParameter = new MySqlParameter();
                typeIdParameter.ParameterName = "@Type_Id";
                typeIdParameter.Value = typeList[i];
                cmd.Parameters.Add(typeIdParameter);

                cmd.ExecuteNonQuery();
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }

        public static List<Offense> GetAll()
        {
            List<Offense> allOffenses = new List<Offense> { };

            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM offenses;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int idRdr = rdr.GetInt32(0);
                int politicianIdRdr = rdr.GetInt32(1);
                string descriptionRdr = rdr.GetString(2);
                Offense newOffense = new Offense (politicianIdRdr, descriptionRdr, idRdr);
                allOffenses.Add(newOffense);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allOffenses;

        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM offenses;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherOffense)
        {
            if (!(otherOffense is Offense))
            {
                return false;
            }
            else
            {
                Offense newOffense = (Offense)otherOffense;
                bool descriptionEquality = this.description.Equals(newOffense.description);
                return (descriptionEquality);
            }
        }
    }
}
