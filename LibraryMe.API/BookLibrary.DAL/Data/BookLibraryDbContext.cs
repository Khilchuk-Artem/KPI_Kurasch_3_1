using BookLibrary.DAL.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL.Data
{
    public class BookLibraryDbContext:IdentityDbContext
    {
        public BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options):base(options) 
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<BorrowingStatus> BorrowingStatuses { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<VisitorMembership> VisitorMemberships { get; set; } 
        public DbSet<VisitorsCard> VisitorsCards { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasOne(a => a.Image).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Book>().HasOne(a => a.Image).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Genre>().HasData(new List<Genre>()
            {
                new Genre()
                {
                    Name="Action",
                    Id=Guid.Parse("0810f87d-0327-41ad-8206-89edac981813")
                },
                new Genre()
                {
                    Name="Fantasy",
                    Id=Guid.Parse("2b81926c-b152-4b4f-9ae1-311eb99e4386")
                },
                new Genre()
                {
                    Name="Classic",
                    Id=Guid.Parse("6a3cd06d-fa8d-4c06-90cb-c87ef2f39f16")
                }
            });
            modelBuilder.Entity<IdentityRole>().HasData(new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                    Id="c608a4a7-14e7-4d19-bf4d-e17b22bfa097",
                    ConcurrencyStamp="c608a4a7-14e7-4d19-bf4d-e17b22bfa097"
                },
                new IdentityRole()
                {
                    Name="Librarian",
                    NormalizedName="Librarian".ToUpper(),
                    Id="11959afc-a8d7-4eb2-9501-b50f0ac1217c",
                    ConcurrencyStamp="11959afc-a8d7-4eb2-9501-b50f0ac1217c"
                }
            });

            var metaAdmin = new IdentityUser()
            {
                Id = "3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                UserName = "Admin",
                Email = "frookt4555@gmail.com",
                NormalizedEmail = "frookt4555@gmail.com".ToUpper(),
                NormalizedUserName = "Admin".ToUpper()
            };
            metaAdmin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(metaAdmin, "SuperSecurePaswwordqwerty@");
            modelBuilder.Entity<IdentityUser>().HasData(metaAdmin);

            var adminRole = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId="3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f",
                    RoleId="11959afc-a8d7-4eb2-9501-b50f0ac1217c"
                }
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(adminRole);


            var borrowingStatuses = new List<BorrowingStatus>()
            {
                new BorrowingStatus()
                {
                    Id=Guid.Parse("73bb3243-c71c-4f1b-ba1f-f4fc56b5dee2"),
                    Name="Active"
                },
                new BorrowingStatus()
                {
                    Id=Guid.Parse("76c30481-34b8-493e-857e-75622551a448"),
                    Name="Returned"
                },new BorrowingStatus()
                {
                    Id=Guid.Parse("f037329e-b42c-456a-bf8f-b79cbc786433"),
                    Name="Expired"
                },
            };
            modelBuilder.Entity<BorrowingStatus>().HasData(borrowingStatuses);

            var reservationStatuses = new List<ReservationStatus>()
            {
                new ReservationStatus()
                {
                    Id=Guid.Parse("929b2083-c7b5-4d8c-b216-f02b0dc65af7"),
                    Name="Processing"
                },
                new ReservationStatus()
                {
                    Id=Guid.Parse("70b5342f-f380-47cf-b9d1-5e3f42a15ff0"),
                    Name="Accepted"
                },
                new ReservationStatus()
                {
                    Id=Guid.Parse("cc7951bd-8930-48c0-b7ce-aa60274c610e"),
                    Name="Checked out"
                },
                new ReservationStatus()
                {
                    Id=Guid.Parse("865a254e-5f32-44b1-aa2f-add87443bfb0"),
                    Name="Expired"
                },
                new ReservationStatus()
                {
                    Id=Guid.Parse("5b0b6de5-7db3-4fb1-9173-8a1f4c2ff9c9"),
                    Name="Declined"
                },
            };
            modelBuilder.Entity<ReservationStatus>().HasData(reservationStatuses);

            var visitorMemberships = new List<VisitorMembership>()
            {
                new VisitorMembership()
                {
                    Id=Guid.Parse("cc81e9d1-3f79-497c-8eaf-5da27afa871b"),
                    Name="None"
                },
                new VisitorMembership()
                {
                    Id=Guid.Parse("23ee4dae-2a8a-4901-a048-78e21b42781e"),
                    Name="Juvenile library"
                },
                new VisitorMembership()
                {
                    Id=Guid.Parse("dfcdca9c-9858-416f-a49a-4843ed624e6c"),
                    Name="Adolescent library"
                }
            };
            modelBuilder.Entity<VisitorMembership>().HasData(visitorMemberships);
        }
    }
}
