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

    [HttpGet("arrondissements")]
    public async Task<IActionResult> GetAllArrondissements()
    {
        try 
        {
            var arrondissements = await _dataController.GetAllArrondissementsAsync();
            return Ok(arrondissements);
        }
        catch (Exception e)
        {
            return Problem($"Api - Error occured: { e.Message }");
        }
    }

    [HttpGet("piscines")]
    public async Task<IActionResult> GetWithParameter([FromQuery] List<string>? arrondissements)
    {
        try
        {
            if (arrondissements.Count() == 0)
            {
                var allPiscines = await _dataController.GetAllPiscinesAsync();
                return Ok(allPiscines);
            }
            var selectPiscine = await _dataController.GetPiscinesByArrondissementAsync(arrondissements);
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