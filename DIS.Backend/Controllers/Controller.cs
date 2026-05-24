using DIS.ApiTwo.DTO;
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

    [HttpGet("flow-levels")]
    public async Task<IActionResult> GetFlowLevel()
    {
        var flowLevels = await _cycleTrackerService.GetAllFlowLevels();

        return Ok(flowLevels);
    }

    [HttpGet("physical-symptoms")]
    public async Task<IActionResult> GetPhysicalSymptom()
    {
        var physicalSymptom = await _cycleTrackerService.GetAllPhysicalSymptom();

        return Ok(physicalSymptom);
    }

    [HttpGet("cycles/{id}/logs")]
    public async Task<IActionResult> GetLogsByCycleId(int id)
    {
        var logs = await _cycleTrackerService.GetLogsByCycleId(id);

        return Ok(logs);
    }

    [HttpPost("dailylogs")]
    public async Task<IActionResult> CreateDailyLog([FromBody] CreateDailyLogDTO dto)
    {
        var created = await _cycleTrackerService.CreateDailyLog(dto);

        return Ok(created);
    }

    [HttpPut("dailylogs/{id}")]
    public async Task<IActionResult> UpdateDailyLog(int id, [FromBody] UpdateDailyLogDTO dto)
    {
        var updated = await _cycleTrackerService.UpdateDailyLog(id, dto);

        if (updated is null) return NotFound();

        return Ok(updated);
    }
}