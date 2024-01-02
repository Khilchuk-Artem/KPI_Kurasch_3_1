import { Routes } from '@angular/router';
import {HomepageComponent} from "./Features/homepage/homepage.component";
import {ViewBooksComponent} from "./Features/Books/view-books/view-books.component";
import {ViewAuthorsComponent} from "./Features/Authors/view-authors/view-authors.component";
import {CreateAuthorComponent} from "./Features/Authors/create-author/create-author.component";
import {ViewAuthorComponent} from "./Features/Authors/view-author/view-author.component";
import {EditAuthorComponent} from "./Features/Authors/edit-author/edit-author.component";
import {CreateBookComponent} from "./Features/Books/create-book/create-book.component";
import {ViewBookComponent} from "./Features/Books/view-book/view-book.component";
import {LoginComponent} from "./Features/Auth/login/login.component";
import {RegisterComponent} from "./Features/Auth/register/register.component";
import {ViewMyProfileComponent} from "./Features/Profile/view-my-profile/view-my-profile.component";
import {EditBookComponent} from "./Features/Books/edit-book/edit-book.component";
import {ViewBookmarksComponent} from "./Features/Profile/view-bookmarks/view-bookmarks.component";
import {EditProfileComponent} from "./Features/Profile/edit-profile/edit-profile.component";
import {CreateVisitorCardComponent} from "./Features/VisitorCards/create-visitor-card/create-visitor-card.component";
import {EditVisitorCardComponent} from "./Features/VisitorCards/edit-visitor-card/edit-visitor-card.component";
import {ViewAnnouncementsComponent} from "./Features/Announcements/view-announcements/view-announcements.component";
import {CreateAnnouncementComponent} from "./Features/Announcements/create-announcement/create-announcement.component";
import {ViewAnnouncementComponent} from "./Features/Announcements/view-announcement/view-announcement.component";
import {EditAnnouncementComponent} from "./Features/Announcements/edit-announcement/edit-announcement.component";
import {CreateBorrowingComponent} from "./Features/Borrowings/create-borrowing/create-borrowing.component";
import {ViewMyBorrowingsComponent} from "./Features/Borrowings/view-my-borrowings/view-my-borrowings.component";
import {ViewBorrowingComponent} from "./Features/Borrowings/view-borrowing/view-borrowing.component";
import {ViewAllBorrowingsComponent} from "./Features/Borrowings/view-all-borrowings/view-all-borrowings.component";
import {ViewBookbagComponent} from "./Features/Bookbag/view-bookbag/view-bookbag.component";
import {ViewAllReservationsComponent} from "./Features/Reservations/view-all-reservations/view-all-reservations.component";
import {ViewMyReservationsComponent} from "./Features/Reservations/view-my-reservations/view-my-reservations.component";
import {ViewReservationComponent} from "./Features/Reservations/view-reservation/view-reservation.component";
import {authGuard} from "./Features/Auth/Guards/auth.guard";
import {ViewVisitorCardComponent} from "./Features/VisitorCards/view-visitor-card/view-visitor-card.component";
import {ViewVisitorCardsComponent} from "./Features/VisitorCards/view-visitor-cards/view-visitor-cards.component";
import {ViewProfilesComponent} from "./Features/Profile/view-profiles/view-profiles.component";
import {SearchBookComponent} from "./Features/Books/search-book/search-book.component";
import {ViewProfileComponent} from "./Features/Profile/view-profile/view-profile.component";

export const routes: Routes = [{
  path:'',
  component:HomepageComponent
},
  {
    path:'books',
    component:ViewBooksComponent
  },
  {
    path:'authors',
    component:ViewAuthorsComponent
  },
  {
    path:'authors/create',
    component:CreateAuthorComponent,
    canActivate:[authGuard],
    data: {
      role: 'Librarian'
    }
  },
  {
    path:'books/create',
    component:CreateBookComponent,
    canActivate:[authGuard],
    data: {
      role: 'Librarian'
    }
  },
  {
    path:'books/search',
    component:SearchBookComponent
  },
  {
    path:'books/:id',
    component:ViewBookComponent
  },
  {
    path:'books/:id/edit',
    component:EditBookComponent,
    canActivate:[authGuard],
    data: {
      role: 'Librarian'
    }
  },
  {
    path: 'authors/:id',
    component: ViewAuthorComponent,
  },
  {
    path: 'authors/:id/edit',
    component: EditAuthorComponent,
    canActivate:[authGuard],
    data: {
      role: 'Librarian'
    }
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'profile',
    component: ViewMyProfileComponent,
    canActivate:[authGuard]
  },
  {
    path: 'profiles/all',
    component: ViewProfilesComponent,
    canActivate:[authGuard],
    data: {
      role: 'Librarian'
    }
  },
  {
    path: 'profile/:id',
    component: ViewProfileComponent,
  },
  {
    path: 'profile/:id/edit',
    component: EditProfileComponent,
    canActivate:[authGuard]
  },
  {
    path: 'bookmarks',
    component: ViewBookmarksComponent,
    canActivate:[authGuard]
  },
  {
    path:'visitor-cards/create',
    component: CreateVisitorCardComponent,
    canActivate:[authGuard],
    data: {
      role: 'Librarian'
    }
  },
  {
    path:'visitor-cards/all',
    component:ViewVisitorCardsComponent,
    canActivate:[authGuard],
    data: {
      role: 'Librarian'
    }
  },
  {
    path:'visitor-cards/:id',
    component: ViewVisitorCardComponent,
    canActivate:[authGuard]
  },
  {
    path:'visitor-cards/:id/edit',
    component: EditVisitorCardComponent,
    canActivate:[authGuard],
    data: {
      role: 'Librarian'
    }
  },
  {
    path:'announcements',
    component: ViewAnnouncementsComponent
  },
  {
    path:'announcements/create',
    component: CreateAnnouncementComponent,
    canActivate:[authGuard],
    data: {
      role: 'Librarian'
    }
  },
  {
    path:'announcements/:id',
    component: ViewAnnouncementComponent
  },
  {
    path:'announcements/:id/edit',
    component: EditAnnouncementComponent,
    canActivate:[authGuard],
    data: {
      role: 'Librarian'
    }
  },
  {
    path:'borrowings/create',
    component: CreateBorrowingComponent,
    canActivate:[authGuard],
    data: {
      role: 'Librarian'
    }
  },
  {
    path:'borrowings',
    component: ViewMyBorrowingsComponent,
    canActivate:[authGuard]
  },
  {
    path:'borrowings/all',
    component: ViewAllBorrowingsComponent,
    canActivate:[authGuard],
    data: {
      role: 'Librarian'
    }
  },
  {
    path:'borrowings/:id',
    component: ViewBorrowingComponent,
    canActivate:[authGuard]
  },
  {
    path:'bookbag',
    component: ViewBookbagComponent,
    canActivate:[authGuard]
  },
  {
    path:'reservations/all',
    component: ViewAllReservationsComponent,
    canActivate:[authGuard],
    data: {
      role: 'Librarian'
    }
  },
  {
    path:'reservations',
    component: ViewMyReservationsComponent,
    canActivate:[authGuard]
  },
  {
    path:'reservations/:id',
    component: ViewReservationComponent,
    canActivate:[authGuard]
  },
];
