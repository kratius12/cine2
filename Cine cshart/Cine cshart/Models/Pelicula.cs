namespace Cine_cshart.Models;

public partial class Pelicula
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int IdCategoria { get; set; }

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<Ranking> Rankings { get; set; } = new List<Ranking>();
}
