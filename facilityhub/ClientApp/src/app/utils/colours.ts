export const getIssueColour = (status: string): string => {
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
