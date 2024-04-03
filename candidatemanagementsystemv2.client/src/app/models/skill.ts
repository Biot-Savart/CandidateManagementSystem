export interface ISkill {
  skillId: number;
  name: string;
  yearsOfExperience: number;
  candidateId?: number;
}

export interface ISkillCandidatesCount {
  name: string;
  candidateCount: number;
}
