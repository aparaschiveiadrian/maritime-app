import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { CountryService } from '../../services/country.service';
import { Country } from '../../models/country';

@Component({
  selector: 'app-country',
  standalone: true,
  imports: [NgIf, NgFor, FormsModule, HttpClientModule],
  templateUrl: './country.component.html',
  styleUrls: ['./country.component.css']
})
export class CountryComponent implements OnInit {
  countries: Country[] = [];
  newCountryName: string = '';
  editingId: number | null = null;
  editedCountryName: string = '';
  error: string = '';
  success: string = '';

  constructor(private countryService: CountryService) {}

  ngOnInit() {
    this.loadCountries();
  }

  loadCountries() {
    this.error = '';
    this.countryService.getAll().subscribe({
      next: data => this.countries = data,
      error: () => this.error = 'Failed to load countries. Please check the API connection.'
    });
  }

  addCountry() {
    this.resetMessages();

    if (!this.newCountryName.trim()) {
      this.error = 'Country name cannot be empty.';
      return;
    }

    if (this.newCountryName.length > 100) {
      this.error = 'Country name must be less than 100 characters.';
      return;
    }

    this.countryService.create({ name: this.newCountryName.trim() }).subscribe({
      next: () => {
        this.success = 'Country added successfully.';
        this.newCountryName = '';
        this.loadCountries();
      },
      error: () => this.error = 'Failed to add country. Server might be offline.'
    });
  }

  deleteCountry(id: number) {
    this.resetMessages();

    this.countryService.delete(id).subscribe({
      next: () => {
        this.success = 'Country deleted.';
        this.loadCountries();
      },
      error: () => this.error = 'Failed to delete. Please try again later.'
    });
  }

  startEdit(country: Country) {
    this.editingId = country.idCountry;
    this.editedCountryName = country.name;
  }

  cancelEdit() {
    this.editingId = null;
    this.editedCountryName = '';
    this.resetMessages();
  }

  saveEdit(id: number) {
    this.resetMessages();

    if (!this.editedCountryName.trim()) {
      this.error = 'Country name cannot be empty.';
      return;
    }

    if (this.editedCountryName.length > 100) {
      this.error = 'Country name must be less than 100 characters.';
      return;
    }

    this.countryService.update(id, { name: this.editedCountryName.trim() }).subscribe({
      next: () => {
        this.success = 'Country updated successfully.';
        this.cancelEdit();
        this.loadCountries();
      },
      error: () => this.error = 'Update failed. Server might be down.'
    });
  }

  private resetMessages() {
    this.error = '';
    this.success = '';
  }
}
