import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ContactsService } from '../../services/contacts.service';
import { Contact } from '../../models/contact.model';
import { AddressDialogComponent } from '../address-dialog/address-dialog.component';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.css'] 
})
export class AddressComponent implements OnInit {
  contacts: Contact[] = [];
  errorMessage: string | null = null;

  constructor(private contactsService: ContactsService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.getContacts();
  }

  getContacts(): void {
    this.contactsService.getContacts().subscribe(
      data => {
      this.contacts = data;
    },
  error => {
    this.errorMessage = error;
  });
  }

  openAddressDialog(contact: Contact): void {
    this.dialog.open(AddressDialogComponent, {
      data: { name: contact.name, address: contact.address },
      width: '400px',
      panelClass: 'address-dialog'
    });
  }
}
