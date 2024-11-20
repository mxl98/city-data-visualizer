public class PatinoireModel
{
    public required int Id { set; get; }
    public required string Arrondissement { set; get; }
    public required string Patinoire { set; get; }
    public required string DateTRS { set; get; }
    public bool? Ouvert { set; get; }
    public bool? Deblaye { set; get; }
    public bool? Arrose { set; get; }
    public bool? Resurface { set; get; }
}