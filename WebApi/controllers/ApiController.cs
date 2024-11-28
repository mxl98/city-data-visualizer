using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Quartz.Util;
using WebApi.Controllers.DataController;

[ApiController]
[Route("api")]
public class ApiController : ControllerBase
{
    private DataController _dataController;

    public ApiController(DataController dataController)
    {
        _dataController = dataController;
    }

    [HttpGet("piscines")]
    public async Task<IActionResult> GetWithParameter([FromQuery] string? arrondissement)
    {
        try
        {
            if (arrondissement.IsNullOrWhiteSpace())
            {
                var allPiscines = await _dataController.GetAllPiscinesAsync();
                return Ok(allPiscines);
            }
            var selectPiscine = await _dataController.GetPiscinesByArrondissementAsync(arrondissement);
            return Ok(selectPiscine);
        }
        catch (Exception e)
        {
            return Problem($"Api - Error occured: { e.Message }");
        }
        
    }

    [HttpGet("update_database")]
    public async Task<IActionResult> Update()
    {
        try
        {
            await _dataController.UpdateAllAsync();
            return Ok("Database updated!");
        } 
        catch (Exception e)
        {
            return Problem($"Api - Error occured: { e.Message }");
        }
        
    }
}