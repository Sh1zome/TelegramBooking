

using System.Text.Json.Nodes;

namespace TelegramBooking_Server
{
    public class Queries
    {
        private DB DB;

        public Queries(DB db) 
        {
            DB = db;
        }

        public async void Init()
        {
            await DB.Query("" +
                "CREATE TABLE IF NOT EXISTS [Users](" +
                "[id] integer PRIMARY KEY AUTOINCREMENT NOT NULL," +
                "[channel_id] char(100) NOT NULL" +
                ");");

            await DB.Query("" +
                "CREATE TABLE IF NOT EXISTS [Admins](" +
                "[id] integer NOT NULL," +
                "[permissions] integer NOT NULL" +
                ");");

            await DB.Query("" +
                "CREATE TABLE IF NOT EXISTS [Providers](" +
                "[id] integer PRIMARY KEY AUTOINCREMENT NOT NULL," +
                "[name] char(50) NOT NULL" +
                ");");

            await DB.Query("" +
                "CREATE TABLE IF NOT EXISTS [Services](" +
                "[id] integer PRIMARY KEY AUTOINCREMENT NOT NULL," +
                "[provider_id] integer NOT NULL," +
                "[name] char(50) NOT NULL" +
                ");");

            await DB.Query("" +
                "CREATE TABLE IF NOT EXISTS [Calendars](" +
                "[id] integer PRIMARY KEY AUTOINCREMENT NOT NULL," +
                "[service_id] integer NOT NULL," +
                "[calendar] char(100) NOT NULL" +
                ");");
        }


        public async Task<JsonNode> GetUsers()
        {
            var res = await DB.Query("" +
                "SELECT id, channel_id FROM Users;");
            return res;
        }

        public async Task<JsonNode> GetUser(int id)
        {
            var res = await DB.Query("" +
                "SELECT id, channel_id FROM Users WHERE id = "+ id +";");
            return res;
        }

        public async Task<JsonNode> AddUser(string channel_id)
        {
            if (await DB.Query("SELECT channel_id FROM Users WHERE channel_id = "+ channel_id +";") != null) { return null; }
            var res = await DB.Query("" +
                "INSERT INTO Users (channel_id) " +
                "VALUES ("+ channel_id +")");
            return res;
        }

        public async Task<JsonNode> SetUser(string channel_id, int id)
        {
            if (await DB.Query("SELECT channel_id FROM Users WHERE channel_id = " + channel_id + ";") != null) { return null; }
            var res = await DB.Query("" +
                "UPDATE Users SET channel_id = " + channel_id + " WHERE id = " + id + ";");
            return res;
        }

        public async Task<JsonNode> DelUser(int id)
        {
            var res = await DB.Query("" +
                "DELETE FROM Users Where id = " + id + ";");
            return res;
        }


        public async Task<JsonNode> GetAdmin(int id)
        {
            var res = await DB.Query("" +
                "SELECT id FROM Admins WHERE id = " + id + ";");
            return res;
        }

        public async Task<JsonNode> AddAdmin(int id, int permissions)
        {
            var res = await DB.Query("" +
                "INSERT INTO Admins (id, permissions) " +
                "VALUES ("+ id +", "+ permissions +")");
            return res;
        }

        public async Task<JsonNode> SetAdmin(int permissions, int id)
        {
            var res = await DB.Query("" +
                "UPDATE Admins SET permissions = " + permissions + " WHERE id = " + id + ";");
            return res;
        }

        public async Task<JsonNode> DelAdmin(int id)
        {
            var res = await DB.Query("" +
                "DELETE FROM Admins Where id = " + id + ";");
            return res;
        }

        public async Task<JsonNode> GetProvider(int id)
        {
            var res = await DB.Query("" +
                "SELECT id FROM Providers WHERE id = " + id + ";");
            return res;
        }

        public async Task<JsonNode> AddProvider(string name)
        {
            var res = await DB.Query("" +
                "INSERT INTO Providers (name) " +
                "VALUES ("+ name +")");
            return res;
        }

        public async Task<JsonNode> SetProvider(string name, int id)
        {
            var res = await DB.Query("" +
                "UPDATE Providers SET name = " + name + " WHERE id = " + id + ";");
            return res;
        }

        public async Task<JsonNode> DelProvider(int id)
        {
            var res = await DB.Query("" +
                "DELETE FROM Providers Where id = " + id + ";");
            return res;
        }

        public async Task<JsonNode> GetService(int id)
        {
            var res = await DB.Query("" +
                "SELECT id FROM Services WHERE id = " + id + ";");
            return res;
        }

        public async Task<JsonNode> AddService(int provider_id, string name)
        {
            var res = await DB.Query("" +
                "INSERT INTO Services (provider_id, name) " +
                "VALUES ("+ provider_id +", "+ name +")");
            return res;
        }

        public async Task<JsonNode> SetService(int id, string name)
        {
            var res = await DB.Query("" +
                "UPDATE Services SET name = " + name + " WHERE id = " + id + ";");
            return res;
        }

        public async Task<JsonNode> DelService(int id)
        {
            var res = await DB.Query("" +
                "DELETE FROM Services Where id = " + id + ";");
            return res;
        }

        public async Task<JsonNode> GetCalendar(int id)
        {
            var res = await DB.Query("" +
                "SELECT id FROM Calendars WHERE id = " + id + ";");
            return res;
        }

        public async Task<JsonNode> AddCalendar(int service_id, string calendar)
        {
            var res = await DB.Query("" +
                "INSERT INTO Calendars(service_id, calendar) " +
                "VALUES ("+ service_id +", "+ calendar +")");
            return res;
        }

        public async Task<JsonNode> SetCalendar(int id, string calendar)
        {
            var res = await DB.Query("" +
                "UPDATE Calendars SET calendar = " + calendar + " WHERE id = " + id + ";");
            return res;
        }

        public async Task<JsonNode> DelCalendar(int id)
        {
            var res = await DB.Query("" +
                "DELETE FROM Calendars Where id = " + id + ";");
            return res;
        }
    }
}
