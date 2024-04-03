import { Pipe, PipeTransform } from '@angular/core';
import { ICandidatePosition, IPosition } from '../models/position';

@Pipe({
  name: 'positionsDisplay'
})
export class PositionsDisplayPipe implements PipeTransform {

  transform(candidatePositions: ICandidatePosition[]): string {
    return candidatePositions?.map(cp => cp.position?.title).join(', ');
  }

}
