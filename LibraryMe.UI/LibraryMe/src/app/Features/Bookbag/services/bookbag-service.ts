import {BookShortcutDTO} from "../../Books/models/book-shortcut-dto";
import {Injectable} from "@angular/core";
@Injectable({
    providedIn: 'root',
})
export class BookbagService{
    key="BookBag"
    constructor() {
        var content=this.getBookbag()
        if(content==null){
            localStorage.setItem(this.key, JSON.stringify([]));
        }
    }

    getBookbag():BookShortcutDTO[]{
        const myList = localStorage.getItem(this.key);

        return myList ? JSON.parse(myList) : [];
    }
    appendItemToBookbag(shortcutDTO: BookShortcutDTO): void {
        const myList = this.getBookbag();
        if(myList.findIndex(item => item.bookId === shortcutDTO.bookId) === -1){
            myList.push(shortcutDTO);
        }

        localStorage.setItem(this.key, JSON.stringify(myList));
    }
    removeItemFromBookBag(shortcut:BookShortcutDTO){
        var myList = this.getBookbag();
        myList= myList.filter(b=>b.bookId!==shortcut.bookId)
        localStorage.setItem(this.key, JSON.stringify(myList));
    }
    clearBookbag(){
        localStorage.setItem(this.key, JSON.stringify([]));
    }
}
