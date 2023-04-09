namespace Webinex.Migrations
{
    public interface IMigrator
    {
        void Run(string[] args);
    }
}