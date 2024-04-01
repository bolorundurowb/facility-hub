import { IssueStatus } from '../components';

export const mapIssueStatusToColour = (status: IssueStatus): string => {
  if (status === IssueStatus.REPAIRED || status === IssueStatus.CLOSED) {
    return 'success';
  } else if (status === IssueStatus.DUPLICATE) {
    return 'secondary';
  } else if (status === IssueStatus.VALIDATED || status === IssueStatus.REPAIR_SCHEDULED) {
    return 'primary';
  } else {
    return 'info';
  }
};

export const mapDocumentTypeToText = (type: string): string => {
  if ([ 'Affidavit', 'Other' ].includes(type)) {
    return type;
  } else if (type === 'TenancyAgreement') {
    return 'Tenancy Agreement';
  } else if (type === 'TenantInformationSheet') {
    return 'Tenant Information Sheet';
  } else if (type === 'IssueEvidence') {
    return 'Issue Evidence';
  } else if (type === 'RefundProof') {
    return 'Refund Proof';
  }

  throw new Error('Unknown document type');
};

export const mapIssueStatusToText = (status: string): string => {
  if (status === 'RepairScheduled') {
    return 'Repair Scheduled';
  } else {
    return status;
  }
};
