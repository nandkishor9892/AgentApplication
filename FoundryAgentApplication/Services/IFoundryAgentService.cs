using FoundryAgentApi.Models;
namespace FoundryAgentApi.Services;
public interface IFoundryAgentService

{
    Task<AgentChatResponse> AskAgentAsync(

        string message,

        CancellationToken cancellationToken = default);
}