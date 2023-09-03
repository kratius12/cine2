namespace Cine_cshart.Models;

public partial class Categorium
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Pelicula> Peliculas { get; set; } = new List<Pelicula>();
}
