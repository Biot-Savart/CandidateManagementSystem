export interface ICandidatePosition {
  position?: IPosition;
  candidateId: number;
  positionId: number;
}

export interface IPosition {
  positionId?: number;
  title: string;
}
