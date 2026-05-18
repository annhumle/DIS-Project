using DIS.ApiTwo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DIS.ApiTwo.Controllers;

[Route("api/cycle-tracker")]
[ApiController]
public class CycleTrackerController : ControllerBase
{
    private readonly ICycleTrackerService _cycleTrackerService;
    private readonly ILogger<CycleTrackerController> _logger;

    public CycleTrackerController(
        ICycleTrackerService cycleTrackerService,
        ILogger<CycleTrackerController> logger)
    {
        _cycleTrackerService = cycleTrackerService;
        _logger = logger;
    }

    [HttpGet("cycles")]
    public async Task<IActionResult> GetCycles()
    {
        var cycles = await _cycleTrackerService.GetAllCycles();

        return Ok(cycles);
    }
}