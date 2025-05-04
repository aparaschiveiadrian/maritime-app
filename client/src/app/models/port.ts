export interface Port {
  idPort: number;
  name: string;
  countryName: string;
}

export interface PortRequestDto {
  name: string;
  idCountry: number;
}
