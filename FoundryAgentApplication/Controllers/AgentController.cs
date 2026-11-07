using FoundryAgentApi.Models;

using FoundryAgentApi.Services;

using Microsoft.AspNetCore.Mvc;

namespace FoundryAgentApi.Controllers;

[ApiController]

[Route("api/[controller]")]

public sealed class AgentController : ControllerBase

{
    private readonly IFoundryAgentService _agentService;

    private readonly ILogger<AgentController> _logger;
    public AgentController(

    IFoundryAgentService agentService,

    ILogger<AgentController> logger)

    {

        _agentService = agentService;

        _logger = logger;

    }

    [HttpPost("chat")]

    [ProducesResponseType(

    typeof(AgentChatResponse),

    StatusCodes.Status200OK)]

    [ProducesResponseType(

    typeof(ProblemDetails),

    StatusCodes.Status400BadRequest)]

    [ProducesResponseType(

    typeof(ProblemDetails),

    StatusCodes.Status500InternalServerError)]

    public async Task<ActionResult<AgentChatResponse>> Chat(

    [FromBody] AgentChatRequest request,

    CancellationToken cancellationToken)

    {

        if (string.IsNullOrWhiteSpace(request.Message))

        {

            return BadRequest(new ProblemDetails

            {

                Title = "Invalid request",

                Detail = "Message is required.",

                Status = StatusCodes.Status400BadRequest

            });

        }

        try

        {
            var response = await _agentService.AskAgentAsync(

            request.Message,

            cancellationToken);



            return Ok(response);

        }

        catch (Exception ex)

        {

            _logger.LogError(ex, "Agent request failed.");



            return Problem(

            title: "Agent request failed",

            detail:

            "The API could not obtain a response from the Foundry agent.",

            statusCode: StatusCodes.Status500InternalServerError);

        }

    }

}