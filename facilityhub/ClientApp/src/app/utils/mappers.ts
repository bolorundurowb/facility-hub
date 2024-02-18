export const mapIssueStatusToColour = (status: string): string => {
  if (status === 'Repaired' || status === 'Closed') {
    return 'success';
  } else if (status === 'Duplicate') {
    return 'secondary';
  } else if (status === 'Validated' || status === 'RepairScheduled') {
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
