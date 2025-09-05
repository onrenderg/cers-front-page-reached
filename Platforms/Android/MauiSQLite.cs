using SQLite;

namespace CERS.Platforms.Android
{
    public class MauiSQLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "CERS.db";
            var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(dbPath, dbName);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}
