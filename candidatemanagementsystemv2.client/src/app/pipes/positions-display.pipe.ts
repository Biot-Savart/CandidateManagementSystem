import { Pipe, PipeTransform } from '@angular/core';
import { ICandidatePosition, IPosition } from '../models/position';
import { ISkill } from '../models/skill';

@Pipe({
  name: 'positionsDisplay'
})
export class PositionsDisplayPipe implements PipeTransform {

  transform(candidatePositions: ICandidatePosition[]): string {
    debugger;
    return candidatePositions?.map(cp => cp.position?.title).join(', ');
  }

}
