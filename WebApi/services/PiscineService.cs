using Microsoft.EntityFrameworkCore;
using Quartz.Util;

public class PiscineService : IPiscineService
{
    private readonly AppDbContext _context;

    public PiscineService(AppDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc cref="IPiscineService.AddAsync(PiscineModel)">
    public async Task AddAsync(PiscineModel entity)
    {
        var existingEntity = await _context.PiscineModels.FindAsync(entity.ID_UEV);

        if (existingEntity != null)
        {
            await UpdateAsync(entity);
        }
        else
        {
            await _context.PiscineModels.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }

    /// <inheritdoc cref="IPiscineService.AddCollectionAsync(ICollection{PiscineModel})">
    public async Task AddCollectionAsync(ICollection<PiscineModel> collection)
    {
        foreach (PiscineModel piscine in collection)
            await AddAsync(piscine);
    }

    /// <inheritdoc cref="IPiscineService.DeleteByIdAsync(int)">
    public Task DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc cref="IPiscineService.GetAllPiscinesAsync">
    public async Task<List<PiscineModel>> GetAllPiscinesAsync()
    {
        return await _context.PiscineModels.ToListAsync();
    }

    /// <inheritdoc cref="IPiscineService.GetByIdAsync(int)">
    public async Task<PiscineModel> GetByIdAsync(int id)
    {
        return await _context.PiscineModels.FindAsync(id);
    }

    /// <inheritdoc cref="IPiscineService.GetByArrondissement(string)">
    public async Task<List<PiscineModel>> GetByArrondissement(string arrondissement)
    {
        var piscines = await _context
            .PiscineModels
            .Where(p => p.ARRONDISSE.Equals(arrondissement))
            .ToListAsync();
        return piscines;
    }

    /// <inheritdoc cref="IPiscineService.GetAllArrondissementsAsync()">
    public async Task<List<string>> GetAllArrondissementsAsync()
    {
        var arrondissements = await _context
            .PiscineModels
            .Select(p => p.ARRONDISSE)
            .Where(a => a != null || a != "")
            .Distinct()
            .ToListAsync();
        return arrondissements;
    }

    /// <inheritdoc cref="IPiscineService.UpdateAsync(PiscineModel)">
    public async Task UpdateAsync(PiscineModel entity)
    {
        var existingEntity = await GetByIdAsync(entity.ID_UEV);

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IPiscineService.GetByArrondissement(List{string})">
    public Task<List<PiscineModel>> GetByArrondissement(List<string> arrondissements)
    {
        var piscines = _context
            .PiscineModels
            .Where(p => arrondissements.Contains(p.ARRONDISSE))
            .ToListAsync();
        return piscines;
    }
}