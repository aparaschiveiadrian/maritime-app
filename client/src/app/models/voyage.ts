export interface VoyageRequestDto {
  idShip: number;
  departurePortId: number;
  arrivalPortId: number;
  voyageDate: string;
  startTime: string;
  endTime: string;
}

export interface VoyageResponseDto {
  idVoyage: number;
  idShip: number;
  shipName: string;
  departurePortId: number;
  departurePortName: string;
  arrivalPortId: number;
  arrivalPortName: string;
  voyageDate: string;
  startTime: string;
  endTime: string;
}
