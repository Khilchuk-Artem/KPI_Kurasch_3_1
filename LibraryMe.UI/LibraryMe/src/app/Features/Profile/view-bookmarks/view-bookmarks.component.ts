import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../Auth/services/auth-service";
import {RouterLink} from "@angular/router";
import {BookmarkDTO} from "../models/bookmark-dto";
import {BookmarkService} from "../services/bookmark-service";
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-view-bookmarks',
  standalone: true,
  imports: [
    RouterLink,
    NgForOf
  ],
  templateUrl: './view-bookmarks.component.html',
  styleUrl: './view-bookmarks.component.css'
})
export class ViewBookmarksComponent implements OnInit {
  bookmarks: BookmarkDTO[] = [];
  userId!: string;

  constructor(private authService: AuthService, private bookmarksService: BookmarkService) {}

  ngOnInit() {
    this.userId = this.authService.getUser()?.userId ?? 'nan';
    this.fetchBookmarks();
  }

  private fetchBookmarks() {
    this.bookmarksService.getBookmarksByUserId(this.userId).subscribe(
        (bookmarks) => {
          this.bookmarks = bookmarks;
          console.log(bookmarks)
        },
        (error) => {
          console.error('Error fetching bookmarks:', error);
        }
    );
  }
  removeBookmark(bookmarkId: string) {
    this.bookmarksService.deleteBookmark(bookmarkId).subscribe(
        (deletedBookmark) => {
          // Remove the deleted bookmark from the local array
          this.bookmarks = this.bookmarks.filter((b) => b.id !== deletedBookmark.id);
        },
        (error) => {
          console.error('Error deleting bookmark:', error);
        }
    );
  }
}
