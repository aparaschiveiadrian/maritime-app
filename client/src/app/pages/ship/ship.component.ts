import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ShipService } from '../../services/ship.service';
import { Ship } from '../../models/ship';

@Component({
  selector: 'app-ship',
  standalone: true,
  imports: [NgIf, NgFor, FormsModule, HttpClientModule],
  templateUrl: './ship.component.html',
  styleUrls: ['./ship.component.css']
})
export class ShipComponent implements OnInit {
  ships: Ship[] = [];
  newName = '';
  newSpeed: number | null = null;
  editingId: number | null = null;
  editedName = '';
  editedSpeed: number | null = null;

  error = '';
  success = '';

  constructor(private shipService: ShipService) {}

  ngOnInit(): void {
    this.fetchShips();
  }

  fetchShips() {
    this.resetMessages();
    this.shipService.getAll().subscribe({
      next: res => this.ships = res,
      error: () => this.error = 'Failed to fetch ships. Check API connection.'
    });
  }

  addShip() {
    this.resetMessages();

    if (!this.newName.trim()) {
      this.error = 'Name cannot be empty.';
      return;
    }
    if (this.newName.length > 100) {
      this.error = 'Name too long (max 100 chars).';
      return;
    }
    if (!this.newSpeed || this.newSpeed <= 0) {
      this.error = 'Speed must be a positive number.';
      return;
    }

    this.shipService.create({ name: this.newName.trim(), maxSpeed: this.newSpeed }).subscribe({
      next: () => {
        this.success = 'Ship added.';
        this.newName = '';
        this.newSpeed = null;
        this.fetchShips();
      },
      error: () => this.error = 'Failed to add ship.'
    });
  }

  startEdit(ship: Ship) {
    this.editingId = ship.idShip;
    this.editedName = ship.name;
    this.editedSpeed = ship.maxSpeed;
  }

  saveEdit(id: number) {
    this.resetMessages();

    if (!this.editedName.trim() || !this.editedSpeed || this.editedSpeed <= 0) {
      this.error = 'All fields must be valid.';
      return;
    }

    this.shipService.update(id, {
      name: this.editedName.trim(),
      maxSpeed: this.editedSpeed
    }).subscribe({
      next: () => {
        this.success = 'Ship updated.';
        this.cancelEdit();
        this.fetchShips();
      },
      error: () => this.error = 'Failed to update ship.'
    });
  }

  cancelEdit() {
    this.editingId = null;
    this.editedName = '';
    this.editedSpeed = null;
  }

  deleteShip(id: number) {
    this.resetMessages();
    this.shipService.delete(id).subscribe({
      next: () => {
        this.success = 'Ship deleted.';
        this.fetchShips();
      },
      error: () => this.error = 'Failed to delete ship.'
    });
  }

  private resetMessages() {
    this.error = '';
    this.success = '';
  }
}
