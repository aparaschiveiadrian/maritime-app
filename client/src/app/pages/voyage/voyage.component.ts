import { Component, OnInit } from '@angular/core';
import { VoyageRequestDto, VoyageResponseDto } from '../../models/voyage';
import { VoyageService } from '../../services/voyage.service';
import { NgIf, NgFor, CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-voyage',
  standalone: true,
  imports: [NgIf, NgFor, FormsModule, CommonModule],
  templateUrl: './voyage.component.html',
  styleUrls: ['./voyage.component.css']
})
export class VoyageComponent implements OnInit {
  voyages: VoyageResponseDto[] = [];
  newVoyage: VoyageRequestDto = {
    idShip: 0,
    departurePortId: 0,
    arrivalPortId: 0,
    voyageDate: '',
    startTime: '',
    endTime: ''
  };

  constructor(private voyageService: VoyageService) {}

  ngOnInit(): void {
    this.fetchVoyages();
  }

  fetchVoyages(): void {
    this.voyageService.getAll().subscribe({
      next: (data) => (this.voyages = data),
      error: (err) => console.error('Error fetching voyages:', err)
    });
  }

  createVoyage(): void {
    const { idShip, departurePortId, arrivalPortId, voyageDate, startTime, endTime } = this.newVoyage;

    if (!voyageDate || !startTime || !endTime) {
      alert('Please fill in all fields with valid date/time.');
      return;
    }

    const parsedDate = new Date(voyageDate + 'T00:00:00');
    const parsedStart = new Date(startTime);
    const parsedEnd = new Date(endTime);

    if ([parsedDate, parsedStart, parsedEnd].some(d => isNaN(d.getTime()))) {
      alert('Invalid date/time input.');
      return;
    }

    const dto: VoyageRequestDto = {
      idShip,
      departurePortId,
      arrivalPortId,
      voyageDate: parsedDate.toISOString(),
      startTime: parsedStart.toISOString(),
      endTime: parsedEnd.toISOString(),
    };

    this.voyageService.create(dto).subscribe({
      next: () => {
        this.resetForm();
        this.fetchVoyages();
      },
      error: (err) => {
        console.error('Error creating voyage:', err);
        alert(`Could not create voyage: ${err.status} ${err.statusText}`);
      }
    });
  }


  deleteVoyage(id: number): void {
    this.voyageService.delete(id).subscribe({
      next: () => this.fetchVoyages(),
      error: (err) => alert('Could not delete voyage: ' + err.message)
    });
  }

  private resetForm(): void {
    this.newVoyage = {
      idShip: 0,
      departurePortId: 0,
      arrivalPortId: 0,
      voyageDate: '',
      startTime: '',
      endTime: ''
    };
  }
}
