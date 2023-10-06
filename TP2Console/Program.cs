using System;
using TP2Console.Models.EntityFramework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TP2Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* using (var ctx = new FilmsDbContext())
             { 
                 Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
                 Console.WriteLine("Categorie : " + categorieAction.Nom);

                 ctx.Entry(categorieAction).Collection(c => c.Films).Load();
                 Console.WriteLine("Films : ");
                 //Chargement des films de la catégorie Action.
                 foreach (var film in ctx.Films.Where(f => f.CategorieNavigation.Nom ==
                categorieAction.Nom).ToList())
                 {
                     Console.WriteLine(film.Nom);
                 }
             }
             using (var ctx = new FilmsDbContext())
             {
                 //Chargement de la catégorie Action et des films de cette catégorie
                 Categorie categorieAction = ctx.Categories
                 .Include(c => c.Films)
                 .First(c => c.Nom == "Action");
                 Console.WriteLine("Categorie : " + categorieAction.Nom);
                 Console.WriteLine("Films : ");
                 foreach (var film in categorieAction.Films)
                 {
                     Console.WriteLine(film.Nom);
                 }
             }
             using (var ctx = new FilmsDbContext())
             {
                 //Chargement de la catégorie Action, des films de cette catégorie et des avis
                 Categorie categorieAction = ctx.Categories
                 .Include(c => c.Films)
                 .ThenInclude(f => f.Avis)
                 .First(c => c.Nom == "Action");
             }
             using (var ctx = new FilmsDbContext())
             {
                 //Chargement de la catégorie Action
                 Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
                 Console.WriteLine("Categorie : " + categorieAction.Nom);
                 Console.WriteLine("Films : ");
                 //Chargement des films de la catégorie Action.
                 foreach (var film in categorieAction.Films) // lazy loading initiated
                 {
                     Console.WriteLine(film.Nom);
                 }
             }*/
            Exo2Q1();
            Exo2Q2();
            Exo2Q3();
            Exo2Q4();
            Exo2Q5();
            Exo2Q6();
            Exo2Q7();
            Exo2Q8();
            Exo2Q9();
            Exo3Q1();
            Exo3Q2();
            Exo3Q3();
            Exo3Q4();
            Exo3Q5();
            Console.ReadKey();
        }
        public static void Exo2Q1()
        {
            var ctx = new FilmsDbContext();
            foreach (var film in ctx.Films)
            {
                Console.WriteLine(film.ToString());
            }
        }

        public static void Exo2Q2()
        {
            var ctx = new FilmsDbContext();
            foreach (var user in ctx.Utilisateurs)
            {
                Console.WriteLine(user.Email);
            }
        }

        public static void Exo2Q3()
        {
            var ctx = new FilmsDbContext();
            foreach (var user in ctx.Utilisateurs.OrderBy(u => u.Login))
            {
                Console.WriteLine(user);
            }
        }

        public static void Exo2Q4()
        {
            var ctx = new FilmsDbContext();

            Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");

            foreach (var film in categorieAction.Films)
            {
                Console.WriteLine(film.Id + " | " + film.Nom);
            }
        }

        public static void Exo2Q5()
        {
            var ctx = new FilmsDbContext();

            int nombreCategories = ctx.Categories.Count();
            Console.WriteLine("Nombre de categories: " + nombreCategories);
        }

        public static void Exo2Q6()
        {
            var ctx = new FilmsDbContext();

            Console.WriteLine("Note la + basse: " + ctx.Avis.OrderBy(a => a.Note).First().Note);
        }

        public static void Exo2Q7()
        {
            var ctx = new FilmsDbContext();


            foreach (var film in ctx.Films.Where(f => f.Nom.ToLower().StartsWith("le")))
            {
                Console.WriteLine(film.Id + " | " + film.Nom);
            }
        }

        public static void Exo2Q8()
        {
            var ctx = new FilmsDbContext();

            Film pulpFiction = ctx.Films.First(f => f.Nom.ToLower() == "pulp fiction");
            Console.WriteLine("Note moyenne: " + pulpFiction.Avis.Average(a => a.Note));
        }

        public static void Exo2Q9()
        {
            var ctx = new FilmsDbContext();

            Console.WriteLine("Utilisateur à la meilleure note: " + ctx.Utilisateurs.Where(u => u.Avis.Max(a => a.Note) == ctx.Avis.Max(a => a.Note)).First().Login);
        }

        public static void Exo3Q1()
        {
            var ctx = new FilmsDbContext();

            Console.WriteLine("-----------------------Exo3-----------------------");

            Utilisateur newUser = new Utilisateur
            {
                Login = "Zozz",
                Pwd = "zozz2022",
                Email = "zozz@zozz.com"
            };

            ctx.Utilisateurs.Add(newUser);
            ctx.SaveChanges();
        }

        public static void Exo3Q2()
        {
            var ctx = new FilmsDbContext();

            Film singes = ctx.Films.Where(f => f.Nom.ToLower() == "l'armee des douze singes").First();

            Console.WriteLine("Desc film: " + singes.Description);
            singes.Description = "Film de singes";
            Console.WriteLine("Categ film: " + singes.Categorie);
            singes.Categorie = ctx.Categories.Where(c => c.Nom.ToLower() == "drame").First().Id;
            Console.WriteLine("Film : " + singes);

            ctx.Films.Update(singes);
            ctx.SaveChanges();
        }

        public static void Exo3Q3()
        {
            var ctx = new FilmsDbContext();

            Film singes = ctx.Films.Where(f => f.Nom.ToLower() == "l'armee des douze singes").First();

            foreach(Avi avi in singes.Avis)
            {
                ctx.Avis.Remove(avi);
            }
            
            ctx.Films.Remove(singes);
            ctx.SaveChanges();
        }

        public static void Exo3Q4()
        {
            var ctx = new FilmsDbContext();

            Utilisateur zozz = ctx.Utilisateurs.Where(u => u.Login == "Zozz").First();

            Film filmPref = ctx.Films.Where(f => f.Nom.ToLower() == "alien").First();

            Avi avisFilm = new Avi
            {
                Film = filmPref.Id,
                Utilisateur = zozz.Id,
                Avis = "Il les tue tous !",
                Note = (Decimal)0.8
            };

            ctx.Avis.Add(avisFilm);
            ctx.SaveChanges();
        }

        public static void Exo3Q5()
        {
            var ctx = new FilmsDbContext();

            ICollection<Film> filmPrefs = new Film[]
            {
                new Film
                {
                    Nom = "Film1",
                    Description = "Description1",
                    Categorie = ctx.Categories.First(c =>  c.Nom.ToLower() == "drame").Id,
                    CategorieNavigation = ctx.Categories.First(c =>  c.Nom.ToLower() == "drame")
                },
                new Film
                {
                    Nom = "Film2",
                    Description = "Description2",
                    Categorie = ctx.Categories.First(c =>  c.Nom.ToLower() == "drame").Id,
                    CategorieNavigation = ctx.Categories.First(c =>  c.Nom.ToLower() == "drame")
                }
            };

            ctx.Films.AddRange(filmPrefs);

            ctx.SaveChanges();
        }
    }
}