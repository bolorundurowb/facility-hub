import { DocumentType } from './enums';

export interface DocumentRes {
  id: string;
  type: DocumentType;
  fileName: string;
  mimeType: string;
  url: string;
  fileSize: number;
  createdBy: string;
  createdAt: string;
}

export interface TenantRes {
  name?: string;
  emailAddress?: string;
  phoneNumber?: string;
  startsAt?: string;
  endsAt?: string;
}

export interface LocationRes {
  longitude: number,
  latitude: number
}

export interface IssueRes {
  id: string;
  code: string;
  facilityId?: string;
  facilityName?: string;
  occurredAt: string;
  description: string;
  location: string;
  remedialAction?: string;
  status: string;
  filedById?: string;
  filedByName?: string;
  filedAt: string;
  repairer?: {
    name?: string;
    phoneNumber: string;
  }
}

export interface DocumentUploadPayload {
  type?: DocumentType,
  file?: any;
}

export enum IssueTransitions {
  VALIDATE = "validate",
  SCHEDULE_REPAIR = "schedule-repair",
  MARK_REPAIRED = "mark-as-repaired",
}
