IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE TABLE [Announcements] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Announcements] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE TABLE [Authors] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Surname] nvarchar(max) NOT NULL,
        [Patronymic] nvarchar(max) NOT NULL,
        [Biography] nvarchar(max) NOT NULL,
        [DateOfBirth] date NOT NULL,
        [DateOfDeath] date NULL,
        [IsDeleted] bit NOT NULL,
        [ImageId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Authors] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE TABLE [BorrowingStatuses] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_BorrowingStatuses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE TABLE [Genres] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE TABLE [Images] (
        [Id] uniqueidentifier NOT NULL,
        [FileName] nvarchar(max) NOT NULL,
        [FileExtension] nvarchar(max) NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Url] nvarchar(max) NOT NULL,
        [DateUploaded] datetime2 NOT NULL,
        CONSTRAINT [PK_Images] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE TABLE [ReservationStatuses] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ReservationStatuses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE TABLE [VisitorMemberships] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_VisitorMemberships] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE TABLE [Reservations] (
        [Id] int NOT NULL IDENTITY,
        [DateCreated] datetime2 NOT NULL,
        [DateAccepted] datetime2 NULL,
        [DateCheckedOut] datetime2 NULL,
        [ReservatorId] uniqueidentifier NOT NULL,
        [ReservationStatusId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Reservations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Reservations_ReservationStatuses_ReservationStatusId] FOREIGN KEY ([ReservationStatusId]) REFERENCES [ReservationStatuses] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE TABLE [VisitorsCards] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Surname] nvarchar(max) NOT NULL,
        [Patronymic] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [VisitorMembershipId] uniqueidentifier NOT NULL,
        [VisitorAccountId] uniqueidentifier NULL,
        CONSTRAINT [PK_VisitorsCards] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_VisitorsCards_VisitorMemberships_VisitorMembershipId] FOREIGN KEY ([VisitorMembershipId]) REFERENCES [VisitorMemberships] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE TABLE [Borrowings] (
        [Id] uniqueidentifier NOT NULL,
        [DateCreated] datetime2 NOT NULL,
        [DueDate] datetime2 NOT NULL,
        [ReadersCardId] uniqueidentifier NOT NULL,
        [BorrowingStatusId] uniqueidentifier NOT NULL,
        [ReadersCardId1] int NOT NULL,
        CONSTRAINT [PK_Borrowings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Borrowings_BorrowingStatuses_BorrowingStatusId] FOREIGN KEY ([BorrowingStatusId]) REFERENCES [BorrowingStatuses] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Borrowings_VisitorsCards_ReadersCardId1] FOREIGN KEY ([ReadersCardId1]) REFERENCES [VisitorsCards] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE TABLE [Books] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [AvailableAMount] int NOT NULL,
        [TotalAmount] int NOT NULL,
        [IsDeleted] bit NOT NULL,
        [ImageId] uniqueidentifier NOT NULL,
        [BorrowingId] uniqueidentifier NULL,
        [ReservationId] int NULL,
        CONSTRAINT [PK_Books] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Books_Borrowings_BorrowingId] FOREIGN KEY ([BorrowingId]) REFERENCES [Borrowings] ([Id]),
        CONSTRAINT [FK_Books_Reservations_ReservationId] FOREIGN KEY ([ReservationId]) REFERENCES [Reservations] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE TABLE [AuthorBook] (
        [AuthorsId] uniqueidentifier NOT NULL,
        [BooksId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AuthorBook] PRIMARY KEY ([AuthorsId], [BooksId]),
        CONSTRAINT [FK_AuthorBook_Authors_AuthorsId] FOREIGN KEY ([AuthorsId]) REFERENCES [Authors] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AuthorBook_Books_BooksId] FOREIGN KEY ([BooksId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE TABLE [BookGenre] (
        [BooksId] uniqueidentifier NOT NULL,
        [GenresId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_BookGenre] PRIMARY KEY ([BooksId], [GenresId]),
        CONSTRAINT [FK_BookGenre_Books_BooksId] FOREIGN KEY ([BooksId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_BookGenre_Genres_GenresId] FOREIGN KEY ([GenresId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE INDEX [IX_AuthorBook_BooksId] ON [AuthorBook] ([BooksId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE INDEX [IX_BookGenre_GenresId] ON [BookGenre] ([GenresId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE INDEX [IX_Books_BorrowingId] ON [Books] ([BorrowingId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE INDEX [IX_Books_ReservationId] ON [Books] ([ReservationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE INDEX [IX_Borrowings_BorrowingStatusId] ON [Borrowings] ([BorrowingStatusId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE INDEX [IX_Borrowings_ReadersCardId1] ON [Borrowings] ([ReadersCardId1]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE INDEX [IX_Reservations_ReservationStatusId] ON [Reservations] ([ReservationStatusId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    CREATE INDEX [IX_VisitorsCards_VisitorMembershipId] ON [VisitorsCards] ([VisitorMembershipId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217212709_Initial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231217212709_Initial', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213121_Add Image Book'
)
BEGIN
    CREATE INDEX [IX_Books_ImageId] ON [Books] ([ImageId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213121_Add Image Book'
)
BEGIN
    ALTER TABLE [Books] ADD CONSTRAINT [FK_Books_Images_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Images] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213121_Add Image Book'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231217213121_Add Image Book', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213740_Author Image'
)
BEGIN
    ALTER TABLE [Books] DROP CONSTRAINT [FK_Books_Images_ImageId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213740_Author Image'
)
BEGIN
    DROP INDEX [IX_Books_ImageId] ON [Books];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213740_Author Image'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Books_ImageId] ON [Books] ([ImageId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213740_Author Image'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Authors_ImageId] ON [Authors] ([ImageId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213740_Author Image'
)
BEGIN
    ALTER TABLE [Authors] ADD CONSTRAINT [FK_Authors_Images_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Images] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213740_Author Image'
)
BEGIN
    ALTER TABLE [Books] ADD CONSTRAINT [FK_Books_Images_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Images] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213740_Author Image'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231217213740_Author Image', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213843_Added the rest'
)
BEGIN
    ALTER TABLE [Books] DROP CONSTRAINT [FK_Books_Borrowings_BorrowingId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213843_Added the rest'
)
BEGIN
    ALTER TABLE [Books] DROP CONSTRAINT [FK_Books_Reservations_ReservationId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213843_Added the rest'
)
BEGIN
    DROP INDEX [IX_Books_BorrowingId] ON [Books];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213843_Added the rest'
)
BEGIN
    DROP INDEX [IX_Books_ReservationId] ON [Books];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213843_Added the rest'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'BorrowingId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Books] DROP COLUMN [BorrowingId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213843_Added the rest'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'ReservationId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Books] DROP COLUMN [ReservationId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213843_Added the rest'
)
BEGIN
    CREATE TABLE [BookBorrowing] (
        [BooksId] uniqueidentifier NOT NULL,
        [BorrowingsId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_BookBorrowing] PRIMARY KEY ([BooksId], [BorrowingsId]),
        CONSTRAINT [FK_BookBorrowing_Books_BooksId] FOREIGN KEY ([BooksId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_BookBorrowing_Borrowings_BorrowingsId] FOREIGN KEY ([BorrowingsId]) REFERENCES [Borrowings] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213843_Added the rest'
)
BEGIN
    CREATE TABLE [BookReservation] (
        [BooksId] uniqueidentifier NOT NULL,
        [ReservationsId] int NOT NULL,
        CONSTRAINT [PK_BookReservation] PRIMARY KEY ([BooksId], [ReservationsId]),
        CONSTRAINT [FK_BookReservation_Books_BooksId] FOREIGN KEY ([BooksId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_BookReservation_Reservations_ReservationsId] FOREIGN KEY ([ReservationsId]) REFERENCES [Reservations] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213843_Added the rest'
)
BEGIN
    CREATE INDEX [IX_BookBorrowing_BorrowingsId] ON [BookBorrowing] ([BorrowingsId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213843_Added the rest'
)
BEGIN
    CREATE INDEX [IX_BookReservation_ReservationsId] ON [BookReservation] ([ReservationsId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231217213843_Added the rest'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231217213843_Added the rest', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219193011_Added genres'
)
BEGIN
    EXEC sp_rename N'[Books].[AvailableAMount]', N'AvailableAmount', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219193011_Added genres'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Genres]'))
        SET IDENTITY_INSERT [Genres] ON;
    EXEC(N'INSERT INTO [Genres] ([Id], [Name])
    VALUES (''0810f87d-0327-41ad-8206-89edac981813'', N''Action''),
    (''2b81926c-b152-4b4f-9ae1-311eb99e4386'', N''Fantasy''),
    (''6a3cd06d-fa8d-4c06-90cb-c87ef2f39f16'', N''Classic'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Genres]'))
        SET IDENTITY_INSERT [Genres] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219193011_Added genres'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231219193011_Added genres', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] ON;
    EXEC(N'INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (N''11959afc-a8d7-4eb2-9501-b50f0ac1217c'', N''11959afc-a8d7-4eb2-9501-b50f0ac1217c'', N''Librarian'', N''LIBRARIAN''),
    (N''c608a4a7-14e7-4d19-bf4d-e17b22bfa097'', N''c608a4a7-14e7-4d19-bf4d-e17b22bfa097'', N''Reder'', N''READER'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] ON;
    EXEC(N'INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
    VALUES (N''3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f'', 0, N''36fe1440-e481-4d9c-87e8-eda342e8e605'', N''frookt4555@gmail.com'', CAST(0 AS bit), CAST(0 AS bit), NULL, N''FROOKT4555@GMAIL.COM'', N''ADMIN'', N''AQAAAAIAAYagAAAAEMwbp7r3tdGATOl9tUzT8vSgSQrvXFwHO+uX1dd+RQrvFfux+ljvHBju4o8OXP41VA=='', NULL, CAST(0 AS bit), N''a186fb21-a668-4fb6-8c3f-d477d0f940a4'', CAST(0 AS bit), N''Admin'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] ON;
    EXEC(N'INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
    VALUES (N''11959afc-a8d7-4eb2-9501-b50f0ac1217c'', N''3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231219212326_Adding Identity'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231219212326_Adding Identity', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143436_More seedings'
)
BEGIN
    ALTER TABLE [Borrowings] DROP CONSTRAINT [FK_Borrowings_VisitorsCards_ReadersCardId1];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143436_More seedings'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Borrowings]') AND [c].[name] = N'ReadersCardId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Borrowings] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Borrowings] DROP COLUMN [ReadersCardId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143436_More seedings'
)
BEGIN
    EXEC sp_rename N'[Borrowings].[ReadersCardId1]', N'VisitorsCardId', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143436_More seedings'
)
BEGIN
    EXEC sp_rename N'[Borrowings].[IX_Borrowings_ReadersCardId1]', N'IX_Borrowings_VisitorsCardId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143436_More seedings'
)
BEGIN
    ALTER TABLE [VisitorsCards] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143436_More seedings'
)
BEGIN
    EXEC(N'UPDATE [AspNetRoles] SET [Name] = N''Reader''
    WHERE [Id] = N''c608a4a7-14e7-4d19-bf4d-e17b22bfa097'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143436_More seedings'
)
BEGIN
    EXEC(N'UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N''605e10f6-600d-4c5e-b983-4a235345053c'', [PasswordHash] = N''AQAAAAIAAYagAAAAEHEj9Uh/XLIMSZnyh8YIIQNX48mXcFhadDw0fCcu2p+m92regDknw6T6MQKPNgBI1w=='', [SecurityStamp] = N''86954186-7934-4c93-9611-6321c296ca52''
    WHERE [Id] = N''3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143436_More seedings'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[BorrowingStatuses]'))
        SET IDENTITY_INSERT [BorrowingStatuses] ON;
    EXEC(N'INSERT INTO [BorrowingStatuses] ([Id], [Name])
    VALUES (''73bb3243-c71c-4f1b-ba1f-f4fc56b5dee2'', N''Active''),
    (''76c30481-34b8-493e-857e-75622551a448'', N''Returned''),
    (''f037329e-b42c-456a-bf8f-b79cbc786433'', N''Expired'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[BorrowingStatuses]'))
        SET IDENTITY_INSERT [BorrowingStatuses] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143436_More seedings'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[ReservationStatuses]'))
        SET IDENTITY_INSERT [ReservationStatuses] ON;
    EXEC(N'INSERT INTO [ReservationStatuses] ([Id], [Name])
    VALUES (''5b0b6de5-7db3-4fb1-9173-8a1f4c2ff9c9'', N''Declined''),
    (''70b5342f-f380-47cf-b9d1-5e3f42a15ff0'', N''Accepted''),
    (''865a254e-5f32-44b1-aa2f-add87443bfb0'', N''Expired''),
    (''929b2083-c7b5-4d8c-b216-f02b0dc65af7'', N''Processing''),
    (''cc7951bd-8930-48c0-b7ce-aa60274c610e'', N''Checked out'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[ReservationStatuses]'))
        SET IDENTITY_INSERT [ReservationStatuses] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143436_More seedings'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[VisitorMemberships]'))
        SET IDENTITY_INSERT [VisitorMemberships] ON;
    EXEC(N'INSERT INTO [VisitorMemberships] ([Id], [Name])
    VALUES (''23ee4dae-2a8a-4901-a048-78e21b42781e'', N''Juvenile library''),
    (''cc81e9d1-3f79-497c-8eaf-5da27afa871b'', N''None''),
    (''dfcdca9c-9858-416f-a49a-4843ed624e6c'', N''Adolescent library'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[VisitorMemberships]'))
        SET IDENTITY_INSERT [VisitorMemberships] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143436_More seedings'
)
BEGIN
    ALTER TABLE [Borrowings] ADD CONSTRAINT [FK_Borrowings_VisitorsCards_VisitorsCardId] FOREIGN KEY ([VisitorsCardId]) REFERENCES [VisitorsCards] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143436_More seedings'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231221143436_More seedings', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143649_Small fixes'
)
BEGIN
    EXEC(N'UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N''762f91b4-8bf8-4577-91cc-5563167b2ed6'', [PasswordHash] = N''AQAAAAIAAYagAAAAEDTD/zDvzMAIkOUGbm54r67ULcMICJJNVIgvfJQgyRv4jOMXHc3rbFUQHvUFNyu10g=='', [SecurityStamp] = N''4d94dfa2-cb56-4c0c-9ae5-4f5499a11b30''
    WHERE [Id] = N''3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231221143649_Small fixes'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231221143649_Small fixes', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231222120559_Add bookmarks'
)
BEGIN
    CREATE TABLE [Bookmarks] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [BookId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Bookmarks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Bookmarks_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231222120559_Add bookmarks'
)
BEGIN
    EXEC(N'UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N''8c595a8f-6aab-4dfd-ac37-2634d60bb186'', [PasswordHash] = N''AQAAAAIAAYagAAAAENB7X7r7AWBQcjbnTAzp+tfsYNaGFiphYBQ7+h6ZEH6vRvxTPwad+HOwvriYeCCVfQ=='', [SecurityStamp] = N''e78832a2-8f41-492c-92f3-8cf500686440''
    WHERE [Id] = N''3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231222120559_Add bookmarks'
)
BEGIN
    CREATE INDEX [IX_Bookmarks_BookId] ON [Bookmarks] ([BookId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231222120559_Add bookmarks'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231222120559_Add bookmarks', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231223111515_Changes in image'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Images]') AND [c].[name] = N'Title');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Images] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Images] DROP COLUMN [Title];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231223111515_Changes in image'
)
BEGIN
    EXEC(N'UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N''1095f014-baf1-4b28-a7ad-318a9cb2b898'', [PasswordHash] = N''AQAAAAIAAYagAAAAEKQ5a8Oz45e64yqZOp9qNiuPjiL/wQYkDvb779nhMfdM63Npvq026K3+rU2bSwqdYQ=='', [SecurityStamp] = N''dcb7fe1e-30b9-426d-a967-3961a0d969b8''
    WHERE [Id] = N''3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231223111515_Changes in image'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231223111515_Changes in image', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231225135211_Removed obsolete stuff from VisitorCard'
)
BEGIN
    ALTER TABLE [VisitorsCards] DROP CONSTRAINT [FK_VisitorsCards_VisitorMemberships_VisitorMembershipId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231225135211_Removed obsolete stuff from VisitorCard'
)
BEGIN
    DROP INDEX [IX_VisitorsCards_VisitorMembershipId] ON [VisitorsCards];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231225135211_Removed obsolete stuff from VisitorCard'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[VisitorsCards]') AND [c].[name] = N'VisitorAccountId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [VisitorsCards] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [VisitorsCards] DROP COLUMN [VisitorAccountId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231225135211_Removed obsolete stuff from VisitorCard'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[VisitorsCards]') AND [c].[name] = N'VisitorMembershipId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [VisitorsCards] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [VisitorsCards] DROP COLUMN [VisitorMembershipId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231225135211_Removed obsolete stuff from VisitorCard'
)
BEGIN
    EXEC(N'UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N''9bb43622-446d-4cb1-bd44-b2a135ed58dc'', [PasswordHash] = N''AQAAAAIAAYagAAAAEDcIgiFN3TiI6TZvwz5wgMTr8+URT2z5CltZsD6xNoNMatjOGYAU5Le5Mo1KlgfTHg=='', [SecurityStamp] = N''83c5f66a-4225-4ecc-955f-cabfe1f8d64f''
    WHERE [Id] = N''3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231225135211_Removed obsolete stuff from VisitorCard'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231225135211_Removed obsolete stuff from VisitorCard', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161907_Remove visitor col'
)
BEGIN
    ALTER TABLE [BookReservation] DROP CONSTRAINT [FK_BookReservation_Reservations_ReservationsId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161907_Remove visitor col'
)
BEGIN
    ALTER TABLE [Reservations] DROP CONSTRAINT [FK_Reservations_ReservationStatuses_ReservationStatusId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161907_Remove visitor col'
)
BEGIN
    ALTER TABLE [Reservations] DROP CONSTRAINT [PK_Reservations];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161907_Remove visitor col'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reservations]') AND [c].[name] = N'ReservatorId');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Reservations] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Reservations] DROP COLUMN [ReservatorId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161907_Remove visitor col'
)
BEGIN
    EXEC sp_rename N'[Reservations]', N'Reservation';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161907_Remove visitor col'
)
BEGIN
    EXEC sp_rename N'[Reservation].[IX_Reservations_ReservationStatusId]', N'IX_Reservation_ReservationStatusId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161907_Remove visitor col'
)
BEGIN
    ALTER TABLE [Reservation] ADD CONSTRAINT [PK_Reservation] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161907_Remove visitor col'
)
BEGIN
    EXEC(N'UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N''b4ed3eeb-3d1e-44de-a8a3-a19838b0407e'', [PasswordHash] = N''AQAAAAIAAYagAAAAEP9PkV/tsUPXYZtCDJn04r+AUpVxeWEZzyqV5ScxGYURibSOtBsWuZOAaZKu1AUmOg=='', [SecurityStamp] = N''cb1a0f16-bcbf-40b3-889e-d80e5a1a763e''
    WHERE [Id] = N''3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161907_Remove visitor col'
)
BEGIN
    ALTER TABLE [BookReservation] ADD CONSTRAINT [FK_BookReservation_Reservation_ReservationsId] FOREIGN KEY ([ReservationsId]) REFERENCES [Reservation] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161907_Remove visitor col'
)
BEGIN
    ALTER TABLE [Reservation] ADD CONSTRAINT [FK_Reservation_ReservationStatuses_ReservationStatusId] FOREIGN KEY ([ReservationStatusId]) REFERENCES [ReservationStatuses] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161907_Remove visitor col'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231226161907_Remove visitor col', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161959_Add reservations again'
)
BEGIN
    ALTER TABLE [BookReservation] DROP CONSTRAINT [FK_BookReservation_Reservation_ReservationsId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161959_Add reservations again'
)
BEGIN
    ALTER TABLE [Reservation] DROP CONSTRAINT [FK_Reservation_ReservationStatuses_ReservationStatusId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161959_Add reservations again'
)
BEGIN
    ALTER TABLE [Reservation] DROP CONSTRAINT [PK_Reservation];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161959_Add reservations again'
)
BEGIN
    EXEC sp_rename N'[Reservation]', N'Reservations';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161959_Add reservations again'
)
BEGIN
    EXEC sp_rename N'[Reservations].[IX_Reservation_ReservationStatusId]', N'IX_Reservations_ReservationStatusId', N'INDEX';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161959_Add reservations again'
)
BEGIN
    ALTER TABLE [Reservations] ADD [ReservatorId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161959_Add reservations again'
)
BEGIN
    ALTER TABLE [Reservations] ADD CONSTRAINT [PK_Reservations] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161959_Add reservations again'
)
BEGIN
    EXEC(N'UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N''3b26039d-5a27-45a3-a8a4-37b75469c2c8'', [PasswordHash] = N''AQAAAAIAAYagAAAAEJjlHPgdPOxwpOl6a1P/WzfAsjYifiZ2QbzvlUe/L7uVAyZtNFVJ6+ec4IWOBZ2luQ=='', [SecurityStamp] = N''b6116c0f-e87e-46eb-9a48-79b6be03b416''
    WHERE [Id] = N''3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161959_Add reservations again'
)
BEGIN
    CREATE INDEX [IX_Reservations_ReservatorId] ON [Reservations] ([ReservatorId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161959_Add reservations again'
)
BEGIN
    ALTER TABLE [BookReservation] ADD CONSTRAINT [FK_BookReservation_Reservations_ReservationsId] FOREIGN KEY ([ReservationsId]) REFERENCES [Reservations] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161959_Add reservations again'
)
BEGIN
    ALTER TABLE [Reservations] ADD CONSTRAINT [FK_Reservations_ReservationStatuses_ReservationStatusId] FOREIGN KEY ([ReservationStatusId]) REFERENCES [ReservationStatuses] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161959_Add reservations again'
)
BEGIN
    ALTER TABLE [Reservations] ADD CONSTRAINT [FK_Reservations_VisitorsCards_ReservatorId] FOREIGN KEY ([ReservatorId]) REFERENCES [VisitorsCards] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231226161959_Add reservations again'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231226161959_Add reservations again', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231228111600_Books changes'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'AvailableAmount');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Books] DROP COLUMN [AvailableAmount];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231228111600_Books changes'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'TotalAmount');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Books] DROP COLUMN [TotalAmount];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231228111600_Books changes'
)
BEGIN
    EXEC(N'UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N''af954a7e-140a-49c8-b60f-e9928fdad54c'', [PasswordHash] = N''AQAAAAIAAYagAAAAEEmncVyCiHzu1kjiPg/RLB2Su+aqWmuM4XUZ61yziUzhfofHzpKPtvnfx+o5bt/ARQ=='', [SecurityStamp] = N''48956776-33b6-4a76-9a8a-3c1576794ca5''
    WHERE [Id] = N''3c8b0d12-13e9-4f42-85a4-5d3ce1e7e34f'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231228111600_Books changes'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231228111600_Books changes', N'8.0.0');
END;
GO

COMMIT;
GO

