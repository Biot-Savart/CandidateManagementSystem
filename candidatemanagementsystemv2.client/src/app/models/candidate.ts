import { ICandidatePosition, IPosition } from "./position";
import { ISkill } from "./skill";

export interface ICandidate {
  candidateId?: number;
  name: string;
  email?: string;
  phone?: string;
  skills?: ISkill[];
  experience?: number;
  candidatePositions?: ICandidatePosition[];
}
