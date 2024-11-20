using System.ComponentModel.DataAnnotations;

public class PiscineModel
{
    [Key]
    public required int ID_UEV { set; get; }
    public required string TYPE { set; get; }
    public required string NOM { set; get; }
    public required string ARRONDISSE { set; get; }
    public required string ADRESSE { set; get; }
    public required string PROPRIETE { set; get; }
    public required string GESTION { set; get; }
    public required string POINT_X { set; get; }
    public required string POINT_Y { set; get; }
    public string? EQUIPEME { set; get; }
    public float LONG { set; get; }
    public float LAT { set; get; }
}