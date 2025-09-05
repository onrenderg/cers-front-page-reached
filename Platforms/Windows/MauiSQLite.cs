using SQLite;

namespace CERS.Platforms.Windows
{
    public class MauiSQLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "CERS.db";
            var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            var path = System.IO.Path.Combine(dbPath, dbName);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}
