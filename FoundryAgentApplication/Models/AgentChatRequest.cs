using System.ComponentModel.DataAnnotations;

namespace FoundryAgentApi.Models;
public sealed class AgentChatRequest

{

    [Required]

    [MinLength(1)]

    public string Message { get; set; } = string.Empty;

}