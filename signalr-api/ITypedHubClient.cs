using System.Threading.Tasks;

public interface ITypedHubClient
{
    Task BroadcastMessage(string type, string payload);
}