namespace Camelonta.Backend.Infrastructure
{
    public static class GlobalSettings
    {
        public static string SqliteConnectionString => @"URI=file:Database\Camelonta.Data.sqlite";
        //public static string SqlConnectionString => @"Data Source=Camelonta.Data.sqlite";
        //public static string SqlConnectionString =>
        //    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Camelonta.Data.mdf;Integrated Security=False;User Id=sa;Password=password";

    }
}
