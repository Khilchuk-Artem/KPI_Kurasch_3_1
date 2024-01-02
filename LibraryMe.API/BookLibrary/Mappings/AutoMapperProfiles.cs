using AutoMapper;
using BookLibrary.Models.Domain;
using BookLibrary.Models.DTO;

namespace BookLibrary.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() 
        {
            /*CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image.Url))
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(author => new AuthorLinkDTO { AuthorId = author.Id.ToString(), Name = author.Name })));
            CreateMap<Book, BookSummaryDTO>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image.Url));*/
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image.Url))
                .ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>src.Id))
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(author => new AuthorLinkDTO { AuthorId = author.Id, Name = $"{author.Surname} {author.Name[0]}. {author.Patronymic[0]}" })));
            CreateMap<Book, BookShortcutDTO>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image.Url));
            CreateMap<Author, AuthorLinkDTO>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"));

            CreateMap<Genre, GenreDTO>();
            CreateMap<CreateBookDTO, Book>();
            /*CreateMap<Author, AuthorDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image.Url))
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(book => new BookSummaryDTO { BookId = book.Id.ToString(), Title = book.Title, ImageUrl = book.Image.Url })));*/
            CreateMap<Author, AuthorLinkDTO>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"));
            CreateMap<Author, AuthorDTO>()
                        .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image.Url))
                        .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books ?? Enumerable.Empty<Book>()));

            CreateMap<AuthorDTO, Author>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Books, opt => opt.Ignore());
            CreateMap<CreateAuthorDTO, Author>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore()) 
                .ForMember(dest => dest.Books, opt => opt.Ignore()) 
                .ForMember(dest => dest.Image, opt => opt.Ignore()) 
                .ReverseMap();
            CreateMap<Author, AuthorSummaryDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Biography.Substring(0, Math.Min(src.Biography.Length, 150))));

            CreateMap<Announcement, AnnouncementDTO>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.CreatedDate)).ReverseMap();
            CreateMap<Announcement, AnnouncementSummaryDTO>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.AnnouncementId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<AnnouncementDTO, Announcement>();
            CreateMap<UpdateAnnouncementDTO, Announcement>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) 
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore()); 


            CreateMap<Reservation, ReservationDTO>()
                .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ReservationStatus.Name))
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(book => new BookShortcutDTO { BookId = book.Id.ToString(), Title = book.Title, ImageUrl = book.Image.Url })));

            /*CreateMap<Reservation, ReservationSummaryDTO>()
                .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ReservationStatus.Name))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => $"{src..Name} {src.Reservator.Surname}"));*/
            CreateMap<VisitorsCard, VisitorCardDTO>();
                //.ForMember(dest => dest.VisitorMembershipName, opt => opt.MapFrom(src => src.VisitorMembership.Name));

            //borrowings
            CreateMap<Borrowing, BorrowingDTO>()
                .ForMember(dest => dest.Borrower, opt => opt.MapFrom(src => $"{src.VisitorsCard.Name} {src.VisitorsCard.Surname}"))
                .ForMember(dest => dest.BorrowerVisitorCardId, opt => opt.MapFrom(src => src.VisitorsCard.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.BorrowingStatus.Name))
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(book => new BookShortcutDTO
                {
                    BookId = book.Id.ToString(),
                    Title = book.Title,
                    ImageUrl = book.Image.Url,
                    Authors=book.Authors.Select(a => new AuthorLinkDTO { Name=a.Name, AuthorId=a.Id })
                })));

            CreateMap<Borrowing, BorrowingSummaryDTO>()
                .ForMember(dest => dest.BorrowingId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.DateCreated.Date))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.BorrowingStatus.Name))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => $"{src.VisitorsCard.Name} {src.VisitorsCard.Surname}"));

            CreateMap<BorrowingStatus, BorrowingDTO>()
                .ForMember(dest => dest.Borrower, opt => opt.Ignore())
                .ForMember(dest => dest.BorrowerVisitorCardId, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Books, opt => opt.Ignore());

            CreateMap<BorrowingStatus, BorrowingSummaryDTO>()
                .ForMember(dest => dest.BorrowingId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Creator, opt => opt.Ignore());

            //visitor cards
            CreateMap<VisitorsCard, VisitorCardDTO>();
            //.ForMember(dest => dest.VisitorMembershipName, opt => opt.MapFrom(src => src.VisitorMembership.Name));

            CreateMap<CreateVisitorCardDTO, VisitorsCard>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
                //.ForMember(dest => dest.VisitorAccountId, opt => opt.Ignore()) 
                //.ForMember(dest => dest.VisitorMembership, opt => opt.Ignore());

            // reservations
            CreateMap<Reservation, ReservationDTO>()
                .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.AcceptedTime, opt => opt.MapFrom(src => src.DateAccepted))
                .ForMember(dest => dest.CheckOutTime, opt => opt.MapFrom(src => src.DateCheckedOut))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ReservationStatus.Name))
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(book => new BookShortcutDTO
                {
                    BookId = book.Id.ToString(),
                    Title = book.Title,
                    ImageUrl = book.Image.Url
                })));

            CreateMap<Reservation, ReservationSummaryDTO>()
                .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ReservationStatus.Name))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => $"{src.Reservator.Surname} {src.Reservator.Name} {src.Reservator.Patronymic}")); //gotta make name and surname

            /*CreateMap<Book, BookShortcutDTO>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image.Url));*/
            //Bookmarks
            CreateMap<Bookmark, BookmarkDTO>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Book.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<CreateBookmarkDTO, Bookmark>();
            CreateMap<VisitorsCard, VisitorCardShortcutDTO>()
                .ForMember(cs => cs.Id, opt => opt.MapFrom(vc => vc.Id))
                .ForMember(cs=>cs.Name, opt => opt.MapFrom(vc =>$"{vc.Surname} {vc.Name} {vc.Patronymic}"));
        }
    }
}
