using System.Data.Common;
using System.Data;
using System.Data.SQLite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.Sqlite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Text.Json.Nodes;

namespace TelegramBooking_Server
{
    public class DB
    {
        private SqliteConnection? connection;

        public DB()
        {
            using (connection = new SqliteConnection("Data Source=data.db"))
            {
                connection.Open();
            }
        }

        public async Task<JsonNode> Query(string query)
        {
            var res = "";
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = query;
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    res += "[";
                    while (reader.Read())   // построчно считываем данные
                    {
                        res += "{";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            res += "\"";
                            res += reader.GetName(i).ToString();
                            res += "\":\"";
                            res += reader.GetString(i);
                            res += "\",";
                        }
                        res = res.Remove(res.Length-1);
                        res += "},";
                    }
                    res = res.Remove(res.Length-1);
                    res += "]";
                }
            }
            connection.Close();
            return string.IsNullOrEmpty(res) ? null : JsonObject.Parse(res);
        }
    }
}
