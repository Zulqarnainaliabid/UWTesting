using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Upwork.Testing.Data.Models;
using Upwork.Testing.Data.Models.Auth;

namespace Upwork.Testing.Data
{
    public class UpworkTestingDbContext : DbContext
    {
        public UpworkTestingDbContext(DbContextOptions<UpworkTestingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public override int SaveChanges()
        {
            SetupAuditTrail();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetupAuditTrail();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetupAuditTrail();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetupAuditTrail();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureIndexes(modelBuilder);

            ConfigureRelationships(modelBuilder);

            ConfigurePropertyConversion(modelBuilder);

            ConfigurePropertyPrecision(modelBuilder);

            ConfigureSeedData(modelBuilder);

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<decimal>().HavePrecision(18);
        }
        private void SetupAuditTrail()
        {
            // automatically setup date fields on every context save
            var dtNow = DateTime.Now;
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is AuditModel<int>)
                {
                    var entity = entry.Entity as AuditModel<int>;
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedDate = entity.UpdatedDate = dtNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.UpdatedDate = dtNow;
                    }
                }
            }
        }

        private static void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            //if needed please define relationships
            //modelBuilder.Entity<UserRole>().HasNoKey();
            modelBuilder.Entity<UserRole>().HasKey(p => new { p.UserId, p.RoleId });
        }

        private static void ConfigurePropertyConversion(ModelBuilder modelBuilder)
        {
            //if needed property type conversion
        }

        private static void ConfigurePropertyPrecision(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().Property(x => x.Budget).HasPrecision(18,2);
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {

                property.SetPrecision(18);
            }
        }
        private static void ConfigureSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(GetGenres());
            modelBuilder.Entity<Movie>().HasData(GetMovies());
            modelBuilder.Entity<Person>().HasData(GetActors());
            modelBuilder.Entity<MovieCast>().HasData(GetCast());
            modelBuilder.Entity<User>().HasData(GetUsers());
            modelBuilder.Entity<Role>().HasData(GetRoles());
            modelBuilder.Entity<Review>().HasData(GetReviews());

        }

        private static void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasIndex(x => x.Value)
                .IsUnique();
        }

        private static IEnumerable<Genre> GetGenres()
        {
            Genre[] genres = {new Genre()
            {
                Id = 1,
                Value = "Drama",
            },new Genre
            {
                Id = 2,
                Value = "Action",
            },
            new Genre
            {
                Id = 3,
                Value = "Horror",
            },
            new Genre
            {
                Id = 4,
                Value = "Thriller",
            },
            new Genre
            {
                Id = 5,
                Value = "Western",
            },
                new Genre
            {
                Id = 6,
                Value = "Comedy",
            },
            new Genre
            {
                Id = 7,
                Value = "Romance",
            },
            new Genre
            {
                Id = 8,
                Value = "Science Fiction",
            },
            new Genre
            {
                Id = 9,
                Value = "Adventure",
            },};
            return genres;
        }
        private static IEnumerable<Movie> GetMovies()
        {
            Movie[] movies = {
            new Movie(){Id=1,   Title="Dune",PosterUrl="https://upload.wikimedia.org/wikipedia/en/thumb/8/8e/Dune_%282021_film%29.jpg/220px-Dune_%282021_film%29.jpg",Budget=12345.43m,CreatedDate=DateTime.Now,ReleaseDate=new DateTime(2021,1,1),Runtime=new TimeSpan(1,51,30),Plot="Dune is a 2021 American epic science fiction film directed by Denis Villeneuve from a screenplay by Villeneuve, Jon Spaihts, and Eric Roth. It is the first of a two-part adaptation of the 1965 novel by Frank Herbert, primarily covering the first half of the book",StoryLine="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"},
            new Movie(){Id=2,   Title="he Eyes of Tammy Faye",PosterUrl="https://upload.wikimedia.org/wikipedia/en/2/2f/The_Eyes_of_Tammy_Faye_%282021_film%29.jpg",Budget=31345.43m,CreatedDate=DateTime.Now,ReleaseDate=new DateTime(2021,1,1),Runtime=new TimeSpan(1,51,30),Plot="The Eyes of Tammy Faye is a 2021 American biographical drama film directed by Michael Showalter from a screenplay by Abe Sylvia, based on the 2000 documentary of the same name by Fenton Bailey and Randy Barbato of World of Wonder",StoryLine="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"},
            new Movie(){Id=3,   Title="No Time to Die",PosterUrl="https://upload.wikimedia.org/wikipedia/en/thumb/f/fe/No_Time_to_Die_poster.jpg/220px-No_Time_to_Die_poster.jpg",Budget=21345.43m,CreatedDate=DateTime.Now,ReleaseDate=new DateTime(2021,1,1),Runtime=new TimeSpan(1,51,30),Plot="The Windshield Wiper is a 2021 Spanish-American computer-cel adult animated short film directed and co-produced by Alberto Mielgo alongside [1] Leo Sánchez.",StoryLine="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"},
            new Movie(){Id=4,   Title="The Windshield Wiper",PosterUrl="https://upload.wikimedia.org/wikipedia/en/thumb/4/40/TheWindshieldWiperShort.jpg/220px-TheWindshieldWiperShort.jpg",Budget=13245.43m,CreatedDate=DateTime.Now,ReleaseDate=new DateTime(2021,1,1),Runtime=new TimeSpan(1,51,30),Plot="The Long Goodbye is the second studio album by Riz Ahmed. It was released on his own record label Mongrel Records on 6 March 2020",StoryLine="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"},
            new Movie(){Id=5,   Title="The Long Goodbye",PosterUrl="https://upload.wikimedia.org/wikipedia/en/thumb/a/aa/Riz_Ahmed_-_The_Long_Goodbye.png/220px-Riz_Ahmed_-_The_Long_Goodbye.png",Budget=14245.43m,CreatedDate=DateTime.Now,ReleaseDate=new DateTime(2021,1,1),Runtime=new TimeSpan(1,51,30),Plot="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged",StoryLine="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"},
            new Movie(){Id=6,   Title="The Queen of Basketball",PosterUrl="https://upload.wikimedia.org/wikipedia/en/thumb/7/7b/QueenofBasketball.jpg/220px-QueenofBasketball.jpg",Budget=15345.43m,CreatedDate=DateTime.Now,ReleaseDate=new DateTime(2021,1,1),Runtime=new TimeSpan(1,41,30),Plot="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged",StoryLine="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"},
            new Movie(){Id=7,   Title="Summer of Soul",PosterUrl="https://upload.wikimedia.org/wikipedia/en/thumb/a/a2/Summer_of_Soul_2021.jpg/220px-Summer_of_Soul_2021.jpg",Budget=12305.43m,CreatedDate=DateTime.Now,ReleaseDate=new DateTime(2021,1,1),Runtime=new TimeSpan(1,31,30),Plot="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged",StoryLine="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"},
            new Movie(){Id=8,   Title="Drive My Car",PosterUrl="https://upload.wikimedia.org/wikipedia/en/thumb/3/3d/Drive_My_Car_movie_poster.jpeg/220px-Drive_My_Car_movie_poster.jpeg",Budget=123555.43m,CreatedDate=DateTime.Now,ReleaseDate=new DateTime(2021,1,1),Runtime=new TimeSpan(2,21,30),Plot="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged",StoryLine="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"},
            new Movie(){Id=9,   Title="Encanto",PosterUrl="https://upload.wikimedia.org/wikipedia/en/thumb/8/83/Encanto_poster.jpg/220px-Encanto_poster.jpg",Budget=123345.43m,CreatedDate=DateTime.Now,ReleaseDate=new DateTime(2021,1,1),Runtime=new TimeSpan(2,11,30),Plot="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged",StoryLine="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"},
            new Movie(){Id=10,   Title="West Side Story",PosterUrl="https://upload.wikimedia.org/wikipedia/en/thumb/2/2e/West_Side_Story_2021_Official_Poster.jpg/220px-West_Side_Story_2021_Official_Poster.jpg",Budget=152345.43m,CreatedDate=DateTime.Now,ReleaseDate=new DateTime(2021,1,1),Runtime=new TimeSpan(2,09,30),Plot="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged",StoryLine="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"},
            new Movie(){Id=11,   Title="Belfast",PosterUrl="https://upload.wikimedia.org/wikipedia/en/thumb/4/4a/Belfast_poster.jpg/220px-Belfast_poster.jpg",Budget=172345.43m,CreatedDate=DateTime.Now,ReleaseDate=new DateTime(2021,1,1),Runtime=new TimeSpan(1,53,30),Plot="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged",StoryLine="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"},
            new Movie(){Id=12,   Title="The Power of the Dog",PosterUrl="https://upload.wikimedia.org/wikipedia/en/thumb/6/6d/The_Power_of_the_Dog_%28film%29.jpg/220px-The_Power_of_the_Dog_%28film%29.jpg",Budget=162345.43m,CreatedDate=DateTime.Now,ReleaseDate=new DateTime(2021,1,1),Runtime=new TimeSpan(1,58,30),Plot="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged",StoryLine="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"}
            };
            return movies;
        }
        private static IEnumerable<Person> GetActors()
        {
            Person[] persons = {
            new Person(){Id=1, FullName="Tom Hanks",Gender=Gender.Male,ImageURL="https://static1.srcdn.com/wordpress/wp-content/uploads/2016/10/Tom-Hanks-in-Forrest-Gump1.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5",CreatedDate=DateTime.Now},
            new Person(){Id=2, FullName="Anthony Hopkins",Gender=Gender.Male,ImageURL="https://static1.srcdn.com/wordpress/wp-content/uploads/2020/06/The-Silence-of-Lambs-Anthony-Hopkins-as-Smiling-Hannibal-Lecter.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5",CreatedDate=DateTime.Now},
            new Person(){Id=3, FullName="Morgan Freeman",Gender=Gender.Male,ImageURL="https://static1.srcdn.com/wordpress/wp-content/uploads/2020/08/Morgan-Freeman-in-The-Shawshank-Redemption.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5",CreatedDate=DateTime.Now},
            new Person(){Id=4, FullName="Dustin Hoffman",Gender=Gender.Male,ImageURL="https://static1.srcdn.com/wordpress/wp-content/uploads/midnightcowboy.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5",CreatedDate=DateTime.Now},
            new Person(){Id=5, FullName="Daniel Day-Lewis.",Gender=Gender.Male,ImageURL="https://static1.srcdn.com/wordpress/wp-content/uploads/2020/04/last-of-the-mohicans-daniel-day-lewis-1992.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5",CreatedDate=DateTime.Now},
            new Person(){Id=6, FullName="Paul Newman",Gender=Gender.Male,ImageURL="https://static1.srcdn.com/wordpress/wp-content/uploads/2017/05/Paul-Newman-in-The-Hustler.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5",CreatedDate=DateTime.Now},
            new Person(){Id=7, FullName="Marlon Brando",Gender=Gender.Male,ImageURL="https://static1.srcdn.com/wordpress/wp-content/uploads/2020/10/Marlon-Brando-in-The-Godfather.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5",CreatedDate=DateTime.Now},
            new Person(){Id=8, FullName="Gene Hackman",Gender=Gender.Male,ImageURL="https://static1.srcdn.com/wordpress/wp-content/uploads/2021/09/royal-tenenbaums-gene-hackman.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5",CreatedDate=DateTime.Now},
            new Person(){Id=9, FullName="Denzel Washington",Gender=Gender.Male,ImageURL="https://static1.srcdn.com/wordpress/wp-content/uploads/2021/03/The-Hurricane-Denzel-Washington.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5",CreatedDate=DateTime.Now},
            new Person(){Id=10, FullName="Leonardo DiCaprio",Gender=Gender.Male,ImageURL="https://static1.srcdn.com/wordpress/wp-content/uploads/2020/10/Marlon-Brando-in-The-Godfather.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5",CreatedDate=DateTime.Now},
            new Person(){Id=11, FullName="Bruce Lee",Gender=Gender.Male,ImageURL="https://upload.wikimedia.org/wikipedia/commons/thumb/c/ca/Bruce_Lee_1973.jpg/449px-Bruce_Lee_1973.jpg",CreatedDate=DateTime.Now}

            };
            return persons;
        }
        private static IEnumerable<MovieCast> GetCast()
        {
            Random rand = new Random();
            var movies = GetMovies();
            var actors = GetActors();
            int id = 1;
            var cast = new List<MovieCast>();
            foreach (var movie in movies)
            {
                var tempCast= actors.Skip(rand.Next(actors.Count()/2)).Select(x => new MovieCast
                {
                    Id=id++,
                    PersonId=x.Id,
                    MovieId=movie.Id,
                    //Person = x,
                    CreatedDate=DateTime.Now,
                    //Movie = movie,
                    Role = (RoleType)rand.Next(8)
                });
                cast.AddRange(tempCast);
            }
            return cast;
        }

        private static IEnumerable<Review> GetReviews()
        {
            var rand = new Random();
            List<Review> reviews = new List<Review>();
            var users = GetUsers();
            var movies = GetMovies();
            int id = 1;
            foreach (var user in users)
            {
                reviews.AddRange(movies.Select(x => new Review
                {
                    Id = id++,
                    Rating = rand.Next(10) + 1,
                    Title = "Jon Spaihts, and Eric Roth",
                    Text = "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry",
                    MovieId = x.Id,
                    UserId = user.Id
                }));
            }
            return reviews;
        }
        private static IEnumerable<User> GetUsers()
        {
            var rand = new Random();
            User[] users = { 
            new User { Id = 1, FirstName = "admin" ,Email="admin@admin.com",UserName="admin@admin.com",EmailConfirmed=true,PhoneNumber="1234567",PhoneNumberConfirmed=true },
            new User { Id = 2, FirstName = "ipSum" + rand.Next()},
            new User { Id = 3, FirstName = "ipSum" + rand.Next()},
            new User { Id = 4, FirstName = "ipSum" + rand.Next()},
            new User { Id = 5, FirstName = "ipSum" + rand.Next()}};
            return users;
        }
        private static IEnumerable<Role> GetRoles()
        {
            var rand = new Random();
            Role[] roles = { 
                new Role { Id=1,Name="USER",NormalizedName="USER",ConcurrencyStamp=rand.Next().ToString()}, 
                new Role { Id = 2, Name = "ADMIN",NormalizedName="ADMIN",ConcurrencyStamp=rand.Next().ToString() } };

            return roles;
        }
    }
}
