import { Pipe, PipeTransform } from '@angular/core';
import { ISkill } from '../models/skill';

@Pipe({
  name: 'skillsDisplay'
})
export class SkillsDisplayPipe implements PipeTransform {

  transform(skills: ISkill[]): string {
    return skills?.map(skill => skill.name).join(', ');
  }

}
