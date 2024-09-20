public class PiscineModel : IFacilityModel
{
    public required int Id { set; get; }
    public required string Type { set; get; }
    public required string Arrondissement { set; get; }
    public required string Adresse { set; get; }
    public required string Propriete { set; get; }
    public required string Gestion { set; get; }
    public required string PointX { set; get; }
    public required string PointY { set; get; }
    public string? Equipement { set; get; }
    public float Lon { set; get; }
    public float Lat { set; get; }
}