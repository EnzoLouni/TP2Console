using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TP2Console.Models.EntityFramework;

[Table("film")]
public partial class Film
{
    private ILazyLoader _lazyLoader;
    public Film(ILazyLoader lazyLoader)
    {
        _lazyLoader = lazyLoader;
    }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nom")]
    [StringLength(50)]
    public string Nom { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("categorie")]
    public int Categorie { get; set; }

    private ICollection<Avi> avis;

    [InverseProperty("FilmNavigation")]
    public virtual ICollection<Avi> Avis {
        get
        {
            return _lazyLoader.Load(this, ref avis);
        }
        set { avis = value; }
    }

    [ForeignKey("Categorie")]
    [InverseProperty("Films")]
    public virtual Categorie CategorieNavigation { get; set; } = null!;
}
