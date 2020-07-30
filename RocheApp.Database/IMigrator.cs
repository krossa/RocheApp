namespace RocheApp.Database
{
    public interface IMigrator  
    {
        void Execute(string connectionString);
    }
}
