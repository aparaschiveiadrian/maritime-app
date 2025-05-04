import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { PortService } from '../../services/port.service';
import { Port } from '../../models/port';


@Component({
  selector: 'app-port',
  standalone: true,
  imports: [NgIf, NgFor, FormsModule, HttpClientModule],
  templateUrl: './port.component.html',
  styleUrls: ['./port.component.css']
})
export class PortComponent implements OnInit {
  ports: Port[] = [];
  newPortName = '';
  newCountryId: number | null = null;

  editingId: number | null = null;
  editedName = '';
  editedCountryId: number | null = null;

  error = '';
  success = '';

  constructor(private portService: PortService) {}

  ngOnInit(): void {
    this.loadPorts();
  }

  loadPorts() {
    this.resetMessages();
    this.portService.getAll().subscribe({
      next: data => this.ports = data,
      error: () => this.error = 'Failed to load ports.'
    });
  }

  addPort() {
    this.resetMessages();
    if (!this.newPortName.trim() || !this.newCountryId || this.newCountryId <= 0) {
      this.error = 'Provide valid name and country ID.';
      return;
    }

    const dto = { name: this.newPortName.trim(), idCountry: this.newCountryId };
    this.portService.create(dto).subscribe({
      next: () => {
        this.success = 'Port added.';
        this.newPortName = '';
        this.newCountryId = null;
        this.loadPorts();
      },
      error: () => this.error = 'Error creating port.'
    });
  }

  startEdit(port: Port) {
    this.editingId = port.idPort;
    this.editedName = port.name;
  }

  saveEdit(id: number) {
    if (!this.editedName.trim() || !this.editedCountryId || this.editedCountryId <= 0) {
      this.error = 'Valid name and country ID required.';
      return;
    }

    const dto = { name: this.editedName.trim(), idCountry: this.editedCountryId };
    this.portService.update(id, dto).subscribe({
      next: () => {
        this.success = 'Port updated.';
        this.cancelEdit();
        this.loadPorts();
      },
      error: () => this.error = 'Failed to update port.'
    });
  }

  deletePort(id: number) {
    this.resetMessages();
    this.portService.delete(id).subscribe({
      next: () => {
        this.success = 'Port deleted.';
        this.loadPorts();
      },
      error: () => this.error = 'Failed to delete port.'
    });
  }

  cancelEdit() {
    this.editingId = null;
    this.editedName = '';
    this.editedCountryId = null;
  }

  private resetMessages() {
    this.error = '';
    this.success = '';
  }
}
