using SQLite;

namespace CERS.Platforms.iOS
{
    public class MauiSQLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "CERS.db";
            var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(dbPath, "..", "Library", dbName);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}
