using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Upwork.Testing.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Plot = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    PosterUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    StoryLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", maxLength: 256, precision: 18, scale: 2, nullable: false),
                    Runtime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gender = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    MovieId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovieCasts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieCasts_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCasts_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(28)", maxLength: 28, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedDate", "MovieId", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drama" },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action" },
                    { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horror" },
                    { 4L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thriller" },
                    { 5L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Western" },
                    { 6L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comedy" },
                    { 7L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romance" },
                    { 8L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Science Fiction" },
                    { 9L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adventure" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Budget", "CreatedDate", "Plot", "PosterUrl", "ReleaseDate", "Runtime", "StoryLine", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, 12345.43m, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7823), "Dune is a 2021 American epic science fiction film directed by Denis Villeneuve from a screenplay by Villeneuve, Jon Spaihts, and Eric Roth. It is the first of a two-part adaptation of the 1965 novel by Frank Herbert, primarily covering the first half of the book", "https://upload.wikimedia.org/wikipedia/en/thumb/8/8e/Dune_%282021_film%29.jpg/220px-Dune_%282021_film%29.jpg", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 51, 30, 0), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", "Dune", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, 31345.43m, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7845), "The Eyes of Tammy Faye is a 2021 American biographical drama film directed by Michael Showalter from a screenplay by Abe Sylvia, based on the 2000 documentary of the same name by Fenton Bailey and Randy Barbato of World of Wonder", "https://upload.wikimedia.org/wikipedia/en/2/2f/The_Eyes_of_Tammy_Faye_%282021_film%29.jpg", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 51, 30, 0), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", "he Eyes of Tammy Faye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, 21345.43m, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7849), "The Windshield Wiper is a 2021 Spanish-American computer-cel adult animated short film directed and co-produced by Alberto Mielgo alongside [1] Leo Sánchez.", "https://upload.wikimedia.org/wikipedia/en/thumb/f/fe/No_Time_to_Die_poster.jpg/220px-No_Time_to_Die_poster.jpg", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 51, 30, 0), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", "No Time to Die", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4L, 13245.43m, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7852), "The Long Goodbye is the second studio album by Riz Ahmed. It was released on his own record label Mongrel Records on 6 March 2020", "https://upload.wikimedia.org/wikipedia/en/thumb/4/40/TheWindshieldWiperShort.jpg/220px-TheWindshieldWiperShort.jpg", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 51, 30, 0), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", "The Windshield Wiper", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5L, 14245.43m, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7855), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged", "https://upload.wikimedia.org/wikipedia/en/thumb/a/aa/Riz_Ahmed_-_The_Long_Goodbye.png/220px-Riz_Ahmed_-_The_Long_Goodbye.png", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 51, 30, 0), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", "The Long Goodbye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6L, 15345.43m, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7857), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged", "https://upload.wikimedia.org/wikipedia/en/thumb/7/7b/QueenofBasketball.jpg/220px-QueenofBasketball.jpg", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 41, 30, 0), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", "The Queen of Basketball", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7L, 12305.43m, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7860), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged", "https://upload.wikimedia.org/wikipedia/en/thumb/a/a2/Summer_of_Soul_2021.jpg/220px-Summer_of_Soul_2021.jpg", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 31, 30, 0), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", "Summer of Soul", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8L, 123555.43m, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7864), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged", "https://upload.wikimedia.org/wikipedia/en/thumb/3/3d/Drive_My_Car_movie_poster.jpeg/220px-Drive_My_Car_movie_poster.jpeg", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 21, 30, 0), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", "Drive My Car", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9L, 123345.43m, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7867), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged", "https://upload.wikimedia.org/wikipedia/en/thumb/8/83/Encanto_poster.jpg/220px-Encanto_poster.jpg", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 11, 30, 0), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", "Encanto", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10L, 152345.43m, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7869), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged", "https://upload.wikimedia.org/wikipedia/en/thumb/2/2e/West_Side_Story_2021_Official_Poster.jpg/220px-West_Side_Story_2021_Official_Poster.jpg", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 9, 30, 0), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", "West Side Story", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11L, 172345.43m, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7872), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged", "https://upload.wikimedia.org/wikipedia/en/thumb/4/4a/Belfast_poster.jpg/220px-Belfast_poster.jpg", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 53, 30, 0), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", "Belfast", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12L, 162345.43m, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7875), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged", "https://upload.wikimedia.org/wikipedia/en/thumb/6/6d/The_Power_of_the_Dog_%28film%29.jpg/220px-The_Power_of_the_Dog_%28film%29.jpg", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 58, 30, 0), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", "The Power of the Dog", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "CreatedDate", "FullName", "Gender", "ImageURL", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7963), "Tom Hanks", 0, "https://static1.srcdn.com/wordpress/wp-content/uploads/2016/10/Tom-Hanks-in-Forrest-Gump1.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7966), "Anthony Hopkins", 0, "https://static1.srcdn.com/wordpress/wp-content/uploads/2020/06/The-Silence-of-Lambs-Anthony-Hopkins-as-Smiling-Hannibal-Lecter.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7968), "Morgan Freeman", 0, "https://static1.srcdn.com/wordpress/wp-content/uploads/2020/08/Morgan-Freeman-in-The-Shawshank-Redemption.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7970), "Dustin Hoffman", 0, "https://static1.srcdn.com/wordpress/wp-content/uploads/midnightcowboy.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7971), "Daniel Day-Lewis.", 0, "https://static1.srcdn.com/wordpress/wp-content/uploads/2020/04/last-of-the-mohicans-daniel-day-lewis-1992.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7974), "Paul Newman", 0, "https://static1.srcdn.com/wordpress/wp-content/uploads/2017/05/Paul-Newman-in-The-Hustler.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7976), "Marlon Brando", 0, "https://static1.srcdn.com/wordpress/wp-content/uploads/2020/10/Marlon-Brando-in-The-Godfather.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7979), "Gene Hackman", 0, "https://static1.srcdn.com/wordpress/wp-content/uploads/2021/09/royal-tenenbaums-gene-hackman.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7981), "Denzel Washington", 0, "https://static1.srcdn.com/wordpress/wp-content/uploads/2021/03/The-Hurricane-Denzel-Washington.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7982), "Leonardo DiCaprio", 0, "https://static1.srcdn.com/wordpress/wp-content/uploads/2020/10/Marlon-Brando-in-The-Godfather.jpg?q=50&fit=crop&w=740&h=370&dpr=1.5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(7984), "Bruce Lee", 0, "https://upload.wikimedia.org/wikipedia/commons/thumb/c/ca/Bruce_Lee_1973.jpg/449px-Bruce_Lee_1973.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1L, "1885771235", "USER", "USER" },
                    { 2L, "1348392307", "ADMIN", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1L, 0, "af216d22-a4fd-4e10-bcaf-a2e59705ec89", "admin@admin.com", true, "admin", null, false, null, null, null, null, "1234567", true, null, false, "admin@admin.com" },
                    { 2L, 0, "e184047f-1473-4f3e-9963-30bc7f6549d5", null, false, "ipSum1922256827", null, false, null, null, null, null, null, false, null, false, null },
                    { 3L, 0, "6328ff6c-3866-461a-b417-5b288e316a2d", null, false, "ipSum1631374751", null, false, null, null, null, null, null, false, null, false, null },
                    { 4L, 0, "ef33cfc3-e3f1-42e0-a5ac-a7d2ab43e0a2", null, false, "ipSum1111337001", null, false, null, null, null, null, null, false, null, false, null },
                    { 5L, 0, "8cfae37f-a1c4-4660-bcad-25cee2f56805", null, false, "ipSum161194795", null, false, null, null, null, null, null, false, null, false, null }
                });

            migrationBuilder.InsertData(
                table: "MovieCasts",
                columns: new[] { "Id", "CreatedDate", "MovieId", "PersonId", "Role", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8245), 1L, 4L, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8254), 1L, 5L, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8257), 1L, 6L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8259), 1L, 7L, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8261), 1L, 8L, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8265), 1L, 9L, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8267), 1L, 10L, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8269), 1L, 11L, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8278), 2L, 4L, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8282), 2L, 5L, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8284), 2L, 6L, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8286), 2L, 7L, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8288), 2L, 8L, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8290), 2L, 9L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8292), 2L, 10L, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8294), 2L, 11L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8300), 3L, 3L, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8304), 3L, 4L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8306), 3L, 5L, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8308), 3L, 6L, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8310), 3L, 7L, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8312), 3L, 8L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8314), 3L, 9L, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8462), 3L, 10L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8466), 3L, 11L, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8476), 4L, 3L, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8478), 4L, 4L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8482), 4L, 5L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8484), 4L, 6L, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8486), 4L, 7L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8488), 4L, 8L, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8489), 4L, 9L, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8491), 4L, 10L, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8495), 4L, 11L, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8502), 5L, 2L, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8504), 5L, 3L, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8506), 5L, 4L, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8508), 5L, 5L, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8510), 5L, 6L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8512), 5L, 7L, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8514), 5L, 8L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8516), 5L, 9L, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "MovieCasts",
                columns: new[] { "Id", "CreatedDate", "MovieId", "PersonId", "Role", "UpdatedDate" },
                values: new object[,]
                {
                    { 43L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8518), 5L, 10L, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8519), 5L, 11L, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8527), 6L, 4L, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8529), 6L, 5L, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8531), 6L, 6L, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8533), 6L, 7L, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8535), 6L, 8L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8537), 6L, 9L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 51L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8539), 6L, 10L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 52L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8541), 6L, 11L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 53L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8548), 7L, 5L, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 54L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8550), 7L, 6L, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 55L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8552), 7L, 7L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 56L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8554), 7L, 8L, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 57L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8556), 7L, 9L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 58L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8558), 7L, 10L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 59L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8560), 7L, 11L, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 60L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8567), 8L, 3L, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 61L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8569), 8L, 4L, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 62L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8571), 8L, 5L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 63L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8573), 8L, 6L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 64L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8575), 8L, 7L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 65L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8577), 8L, 8L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 66L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8581), 8L, 9L, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 67L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8583), 8L, 10L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 68L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8585), 8L, 11L, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 69L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8592), 9L, 4L, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 70L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8594), 9L, 5L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 71L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8596), 9L, 6L, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 72L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8598), 9L, 7L, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 73L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8600), 9L, 8L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 74L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8602), 9L, 9L, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 75L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8604), 9L, 10L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 76L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8605), 9L, 11L, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 77L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8613), 10L, 4L, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 78L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8615), 10L, 5L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 79L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8617), 10L, 6L, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 80L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8619), 10L, 7L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 81L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8621), 10L, 8L, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 82L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8623), 10L, 9L, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 83L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8625), 10L, 10L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 84L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8627), 10L, 11L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "MovieCasts",
                columns: new[] { "Id", "CreatedDate", "MovieId", "PersonId", "Role", "UpdatedDate" },
                values: new object[,]
                {
                    { 85L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8634), 11L, 2L, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 86L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8681), 11L, 3L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 87L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8684), 11L, 4L, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 88L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8686), 11L, 5L, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 89L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8688), 11L, 6L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 90L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8690), 11L, 7L, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 91L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8692), 11L, 8L, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 92L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8694), 11L, 9L, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 93L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8695), 11L, 10L, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 94L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8697), 11L, 11L, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 95L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8705), 12L, 5L, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 96L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8707), 12L, 6L, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 97L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8709), 12L, 7L, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 98L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8711), 12L, 8L, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 99L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8713), 12L, 9L, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 100L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8715), 12L, 10L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 101L, new DateTime(2022, 7, 23, 18, 25, 32, 17, DateTimeKind.Local).AddTicks(8717), 12L, 11L, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "CreatedDate", "MovieId", "Rating", "Text", "Title", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 8, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, 10, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, 3, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 4L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L, 7, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 5L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L, 9, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 6L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6L, 4, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 7L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7L, 2, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 8L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8L, 10, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 9L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9L, 6, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 10L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10L, 9, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 11L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11L, 4, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 12L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12L, 10, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 13L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 1, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L },
                    { 14L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, 6, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L },
                    { 15L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, 2, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L },
                    { 16L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L, 10, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L },
                    { 17L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L, 4, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L },
                    { 18L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6L, 4, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L },
                    { 19L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7L, 6, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L },
                    { 20L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8L, 2, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L },
                    { 21L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9L, 7, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L },
                    { 22L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10L, 8, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L },
                    { 23L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11L, 2, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L },
                    { 24L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12L, 2, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L },
                    { 25L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 9, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "CreatedDate", "MovieId", "Rating", "Text", "Title", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 26L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, 7, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 27L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, 4, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 28L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L, 8, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 29L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L, 9, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 30L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6L, 2, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 31L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7L, 4, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 32L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8L, 10, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 33L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9L, 6, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 34L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10L, 8, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 35L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11L, 8, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 36L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12L, 9, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 37L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 10, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 38L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, 8, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 39L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, 1, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 40L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L, 9, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 41L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L, 4, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 42L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6L, 7, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 43L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7L, 9, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 44L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8L, 2, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 45L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9L, 3, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 46L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10L, 2, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 47L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11L, 8, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 48L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12L, 9, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 49L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 2, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L },
                    { 50L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, 3, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L },
                    { 51L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, 1, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L },
                    { 52L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L, 9, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L },
                    { 53L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L, 5, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L },
                    { 54L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6L, 8, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L },
                    { 55L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7L, 6, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L },
                    { 56L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8L, 4, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L },
                    { 57L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9L, 6, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L },
                    { 58L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10L, 2, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L },
                    { 59L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11L, 3, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L },
                    { 60L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12L, 6, "simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry", "Jon Spaihts, and Eric Roth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_MovieId",
                table: "Genres",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Value",
                table: "Genres",
                column: "Value",
                unique: true,
                filter: "[Value] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCasts_MovieId",
                table: "MovieCasts",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCasts_PersonId",
                table: "MovieCasts",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "MovieCasts");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
