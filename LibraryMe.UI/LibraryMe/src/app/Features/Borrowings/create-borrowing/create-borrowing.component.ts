import {Component, OnInit, ViewChild} from '@angular/core';
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {NgSelectComponent, NgSelectModule} from "@ng-select/ng-select";
import {FormBuilder, FormGroup, FormsModule, Validators} from "@angular/forms";
import {VisitorsCardsService} from "../../VisitorCards/services/visitor-cards-service";
import {ActivatedRoute, Router} from "@angular/router";
import {UserSummaryService} from "../../Profile/services/user-summary-service";
import {AuthService} from "../../Auth/services/auth-service";
import {VisitorCardShortcutDTO} from "../../VisitorCards/models/visitor-card-shortcut-dto";
import {Observable} from "rxjs";
import {UserProfileSummaryDTO} from "../../Profile/models/user-profile-summary-dto";
import {BookShortcutDTO} from "../../Books/models/book-shortcut-dto";
import {BookService} from "../../Books/services/book-service";
import {BorrowingsService} from "../services/borrowings-service";

@Component({
  selector: 'app-create-borrowing',
  standalone: true,
    imports: [
        AsyncPipe,
        NgSelectModule,
        FormsModule,
        NgForOf,
        NgIf
    ],
  templateUrl: './create-borrowing.component.html',
  styleUrl: './create-borrowing.component.css'
})
export class CreateBorrowingComponent implements OnInit{
    constructor(private visitorsCardsService:VisitorsCardsService,
                private fb: FormBuilder,
                private router:Router,
                private bookService:BookService,
                private borrowingsService:BorrowingsService

    ) {}
    @ViewChild("cardSelect") cardSelect!: NgSelectComponent;
    @ViewChild("bookSelect") bookSelect!: NgSelectComponent;

    selectedVisitorCard!:VisitorCardShortcutDTO;
    selectedVisitorName!:string;
    bufer!:string;
    selectedCardId:string|undefined;
    cards!:Observable<VisitorCardShortcutDTO[]>
    books!:Observable<BookShortcutDTO[]>
    selectedBookId:string|undefined;
    selectedBooks:BookShortcutDTO[]=[]
    userProfileForm!: FormGroup;
    oldUserSummary!:UserProfileSummaryDTO;
    ngOnInit() {
        this.cards=this.visitorsCardsService.getVisitorCardShortcuts()
        this.books=this.bookService.getBookShortcuts(1000)
        console.log(this.books)
        this.userProfileForm = this.fb.group({
            name: ['', Validators.required],})
        console.log("hello")
        this.selectedCardId=undefined
    }
    changeCard(event: any){
        this.selectedCardId=event.id
        this.selectedVisitorName=event.name
        setTimeout(() => {
            //this.genreSelect.handleClearClick();
        });
        console.log(this.selectedCardId)
    }
    changeBook(event: any){
        if (this.selectedBooks.findIndex(item => item.id === event.id) === -1) {
            this.selectedBooks.push(event as BookShortcutDTO);
        }
        setTimeout(() => {
            this.bookSelect.handleClearClick();
        });
        console.log(this.selectedBooks)
    }
    removeBook(book:BookShortcutDTO){
        const index = this.selectedBooks.findIndex(item => item.id === book.id);

        if (index !== -1) {
            this.selectedBooks.splice(index, 1);
        }
    }
    clearCard(event: any){
        this.selectedCardId=undefined
    }
    onSubmit(){
        // Ensure that there is a selected visitor card and at least one selected book
        if (!this.selectedCardId || this.selectedBooks.length === 0) {
            console.error('Please select a visitor card and at least one book.');
            return;
        }

        const createBorrowingDTO = {
            borrowerId: +this.selectedCardId, // Convert to number if necessary
            bookIds: this.selectedBooks.map((book) => book.id),
        };

        this.borrowingsService.createBorrowing(createBorrowingDTO).subscribe(
            (response) => {
                console.log('Borrowing created successfully:', response);
                // Additional logic or navigation after successful creation
            },
            (error) => {
                console.error('Error creating borrowing:', error);
                // Handle error appropriately
            }
        );
    }
}
