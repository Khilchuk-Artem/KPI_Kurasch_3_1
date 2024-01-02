import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {AsyncPipe, NgIf} from "@angular/common";
import {NgSelectComponent, NgSelectModule} from "@ng-select/ng-select";
import {VisitorsCardsService} from "../../VisitorCards/services/visitor-cards-service";
import {Observable} from "rxjs";
import {VisitorCardDTO} from "../../VisitorCards/models/visitor-card-dto";
import {VisitorCardShortcutDTO} from "../../VisitorCards/models/visitor-card-shortcut-dto";
import {UpdateUserDTO} from "../models/update-user-dto";
import {UserSummaryService} from "../services/user-summary-service";
import {UserProfileSummaryDTO} from "../models/user-profile-summary-dto";
import {AuthService} from "../../Auth/services/auth-service";
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from '@angular/common'
@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    AsyncPipe,
    NgSelectModule,
    FormsModule,
    NgIf
  ],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent implements OnInit{
  constructor(private visitorsCardsService:VisitorsCardsService,
              private fb: FormBuilder,
              private router:Router,
              private userSummaryService: UserSummaryService,
              private authService: AuthService,
              private route: ActivatedRoute,
              private location: Location

  ) {}
  @ViewChild("cardSelect") genreSelect!: NgSelectComponent;
  selectedVisitorCard!:VisitorCardShortcutDTO;
  bufer!:string;
  selectedCardId:string|undefined;
  cards!:Observable<VisitorCardShortcutDTO[]>
  userProfileForm!: FormGroup;
  oldUserSummary!:UserProfileSummaryDTO;
  ngOnInit() {
    this.cards=this.visitorsCardsService.getVisitorCardShortcuts()
    this.userProfileForm = this.fb.group({
      name: ['', Validators.required],})
    console.log("hello")
    this.selectedCardId=undefined
    this.userSummaryService.getUserSummaryById(this.route.snapshot.params['id'])
      .subscribe((summary)=>{
        console.log(summary)
      this.oldUserSummary=summary;
        this.cards.subscribe((list)=>{
          const card = list.find(vc=>vc.id.toString()===this.selectedCardId)
          if(card){
            this.selectedVisitorCard=card;
          }
        })
      this.selectedCardId=summary.visitorCardId;
      this.userProfileForm.patchValue({
        name:summary.name
      })
    })
  }
  changeCard(event: any){
    this.selectedCardId=event.id
    setTimeout(() => {
      //this.genreSelect.handleClearClick();
    });
    console.log(this.selectedCardId)
  }
  clearCard(event: any){
    this.selectedCardId=undefined
  }
  onSubmit(): void {
    if (this.userProfileForm.valid) {
      const updatedUserSummary: UpdateUserDTO = {
        name: this.userProfileForm.value.name,
        cardId: this.selectedCardId ? this.selectedCardId.toString() : "",
      };

      // Assuming you have a user ID, replace 'userId' with the actual user ID
      const userId = this.route.snapshot.params['id'];
      console.log(updatedUserSummary)
      this.userSummaryService.updateUserSummaryById(updatedUserSummary, userId).subscribe(
        (updatedProfile: UserProfileSummaryDTO) => {
          // Handle success, e.g., show a success message
          console.log(this.authService.getUser())
          if(userId.toLowerCase()===this.authService.getUser()?.userId.toLowerCase()){
            this.authService.updateUser(updatedProfile);
          }
          this.location.back()
          //this.router.navigate(['/'])
          console.log('User profile updated successfully:', updatedProfile);
        },
        (error) => {
          // Handle error, e.g., show an error message
          console.error('Error updating user profile:', error);
        }
      );
    } else {
      // Handle form validation errors
      console.log('Form is invalid.');
    }
  }
}
