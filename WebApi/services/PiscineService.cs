using Microsoft.EntityFrameworkCore;

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

    /// <inheritdoc cref="IPiscineService.UpdateAsync(PiscineModel)">
    public async Task UpdateAsync(PiscineModel entity)
    {
        var existingEntity = await GetByIdAsync(entity.ID_UEV);

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);

        await _context.SaveChangesAsync();
    }
}