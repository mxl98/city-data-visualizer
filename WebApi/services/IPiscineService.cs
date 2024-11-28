/// <summary>
/// Represents the interface for all Piscine operations with the database.
/// </summary>
public interface IPiscineService
{
    /// <summary>
    /// Gets all PiscineModel from the database.
    /// </summary>
    /// <returns>A list of PiscineModel.</returns>
    Task<List<PiscineModel>> GetAllPiscinesAsync();

    /// <summary>
    /// Gets all known arrondissements from the database.
    /// </summary>
    /// <returns>The list of arrondissement names.</returns>
    Task<List<string>> GetAllArrondissementsAsync();

    /// <summary>
    /// Gets a specific PiscineModel by its id from the database.
    /// </summary>
    /// <param name="id">The id of the PiscineModel.</param>
    /// <returns>The PiscineModel.</returns>
    Task<PiscineModel> GetByIdAsync(int id);

    /// <summary>
    /// Gets a list of PiscineModel by their arrondissement from the database.
    /// </summary>
    /// <param name="arrondissement">The arrondissement of the PiscineModels</param>
    /// <returns>The list of PiscineModels of the specified arrondissement.</returns>
    Task<List<PiscineModel>> GetByArrondissement(string arrondissement);

    /// <summary>
    /// Adds the specified PiscineModel to the database.
    /// If the entity exists, update it.
    /// </summary>
    /// <param name="entity">The PiscineModel to add.</param>
    /// <returns>The added PiscineModel.</returns>
    Task AddAsync(PiscineModel entity);

    /// <summary>
    /// Adds a collection of PiscineModel to the database.
    /// </summary>
    /// <param name="collection">The collection of PiscineModel to add.</param>
    /// <returns>The added collection of PiscineModel.</returns>
    Task AddCollectionAsync(ICollection<PiscineModel> collection);

    /// <summary>
    /// Updates the PiscineModel with the same id as the provided PiscineModel inside the database.
    /// </summary>
    /// <param name="entity">The updated PiscineModel.</param>
    /// <returns>The updated entity.</returns>
    Task UpdateAsync(PiscineModel entity);

    /// <summary>
    /// Deletes the PiscineModel with the specified id from the database.
    /// </summary>
    /// <param name="id">the id of the PiscineModel to delete.</param>
    /// <returns>The deleted PiscineModel.</returns>
    Task DeleteByIdAsync(int id);
}