// dotnet add package Azure.AI.Projects --version 2.0.0-beta.2
using Azure.AI.Projects;
using Azure.AI.Projects.Agents;
using Azure.AI.Extensions.OpenAI;
using Azure.Identity;
using OpenAI.Responses;
using FoundryAgentApi.Models;
using FoundryAgentApi.Services;



namespace FoundryAgentApi.Services;



// Minimal, compile-friendly implementation. Replace with real Foundry integration as needed.

public sealed class FoundryAgentService : IFoundryAgentService

{

    private readonly ILogger<FoundryAgentService> _logger;



    public FoundryAgentService(ILogger<FoundryAgentService> logger)

    {

        _logger = logger;

    }



    public async Task<AgentChatResponse> AskAgentAsync(string message, CancellationToken cancellationToken = default)

    {

        if (string.IsNullOrWhiteSpace(message))

        {

            throw new ArgumentException("Message cannot be empty.", nameof(message));

        }



#pragma warning disable OPENAI001



        const string endpoint = "https://aimsagent01.services.ai.azure.com/api/projects/proj-default";

        const string agentName = "MyAgent";

        const string agentVersion = "1";



        // Connect to your project using the endpoint from your project page



        try

        {

          

#pragma warning disable OPENAI001

          

            // Connect to your project using the endpoint from your project page
            // The AzureCliCredential will use your logged-in Azure CLI identity, make sure to run `az login` first
            AIProjectClient projectClient = new(endpoint: new Uri(endpoint), tokenProvider: new DefaultAzureCredential());

            AgentReference agentReference = new(name: agentName, version: agentVersion);
            ProjectResponsesClient responseClient = projectClient.OpenAI.GetProjectResponsesClientForAgent(agentReference);
            // Use the agent to generate a response
            ResponseResult response = responseClient.CreateResponse(
               message
            );

            var responseResultObj = response.GetOutputText();

            // Last resort: ToString()

            return new AgentChatResponse { Response = responseResultObj.ToString() ?? string.Empty, ConversationId = Guid.NewGuid().ToString() };

        }

        catch (Exception ex)

        {

            _logger.LogError(ex, "Error while calling Foundry agent; returning echo response.");

            return new AgentChatResponse { Response = $"Echo: {message}", ConversationId = Guid.NewGuid().ToString() };

        }





    }

}