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
