using System;
using System.Collections.Generic;

namespace cine3.Models;

public partial class Ranking
{
    public int Id { get; set; }

    public int IdPel { get; set; }

    public int Puntuacion { get; set; }

    public virtual Pelicula IdPelNavigation { get; set; } = null!;
}
