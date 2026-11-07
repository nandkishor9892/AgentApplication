
using System.Text.Json.Serialization;
namespace FoundryAgentApi.Models;
public sealed class AgentChatResponse
{
    public string Response { get; set; } = string.Empty;
    public string? ConversationId { get; set; }
}
