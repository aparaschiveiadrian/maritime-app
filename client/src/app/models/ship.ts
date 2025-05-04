export interface Ship {
  idShip: number;
  name: string;
  maxSpeed: number;
}

export interface ShipRequestDto {
  name: string;
  maxSpeed: number;
}
