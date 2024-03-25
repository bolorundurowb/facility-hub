import { Component, OnInit } from '@angular/core';
import { DocumentRes, DocumentUploadPayload, IssueRes, IssueStatus, IssueTransitions } from '../../components';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { AuthService, FileDownloadService, IssuesService, NotificationService } from '../../services';
import {
  cilArrowLeft,
  cilChevronDoubleRight,
  cilCloudDownload,
  cilCloudUpload,
  cilNoteAdd,
  cilObjectUngroup,
  cilTrash,
  cilUserPlus
} from '@coreui/icons';
import { mapDocumentTypeToText, mapIssueStatusToColour, mapIssueStatusToText } from '../../utils';
import { HttpEventType } from '@angular/common/http';

interface TransitionIssuePayload {
  notes?: string;
  repairerName?: string;
  repairerPhoneNumber?: string;
}

@Component({
  selector: 'fh-dashboard-issue-details',
  templateUrl: './issue-details.component.html',
  styleUrl: './issue-details.component.scss'
})
export class IssueDetailsComponent implements OnInit {
  isLoading = true;
  icons = {
    cilCloudUpload,
    cilNoteAdd,
    cilCloudDownload,
    cilTrash,
    cilArrowLeft,
    cilUserPlus,
    cilChevronDoubleRight,
    cilObjectUngroup
  };

  issueId?: string;
  issue?: IssueRes;
  isTenant = true;
  documents: Array<DocumentRes> = [];

  hasError = false;
  errorMessage?: string;

  isNewDocModalVisible = false;
  isUploadingDoc = false;
  newDocPayload: DocumentUploadPayload = {};

  isTransitioning = false;
  isTransitionModalVisible = false;
  transitionPayload: TransitionIssuePayload = {};

  isDuplicateModalVisible = false;

  constructor(title: Title, private route: ActivatedRoute, private location: Location, private authService: AuthService,
              private downloadService: FileDownloadService, private issueService: IssuesService,
              private notificationService: NotificationService) {
    title.setTitle('Issue Details | Facility Hub');
  }

  protected readonly mapDocumentTypeToText = mapDocumentTypeToText;

  protected readonly getIssueColour = mapIssueStatusToColour;

  async ngOnInit() {
    this.isLoading = true;

    try {
      const issueId = this.route.snapshot.params['issueId'];

      this.issue = await this.issueService.getOne(issueId);
      this.documents = await this.issueService.getOneDocuments(issueId);

      this.issueId = issueId;
      this.isTenant = this.issue?.filedById === this.authService.getUser().id;
    } finally {
      this.isLoading = false;
    }
  }

  goBack() {
    this.location.back();
  }

  showDocumentModal() {
    this.isNewDocModalVisible = true;
  }

  dismissDocumentModal() {
    this.isNewDocModalVisible = false;
    this.newDocPayload = {};
  }

  documentSelected(event: any) {
    this.newDocPayload.file = event.target.files[0];
  }

  uploadDocument() {
    this.isUploadingDoc = true;
    this.hasError = false;

    this.issueService.uploadDocument(this.issueId!, this.newDocPayload)
      .subscribe({
        next: (event) => {
          if (event.type === HttpEventType.Response) {
            const document = event.body as DocumentRes;
            this.documents.unshift(document);

            this.dismissDocumentModal();

            this.notificationService.showSuccess('Document uploaded successfully');
            this.isUploadingDoc = false;
          }
        }, error: (err) => {
          this.errorMessage = err as string;
          this.hasError = true;
          this.isUploadingDoc = false;
        }
      });
  }

  async downloadFile(document: DocumentRes) {
    this.downloadService.downloadFile(
      document.url,
      `${document.id}-${document.fileName}`
    );
  }

  async deleteDocument(document: DocumentRes) {
    try {
      const response = await this.issueService.deleteDocument(this.issueId!, document.id);

      const documentIndex = this.documents.findIndex((doc) => {
        return doc.id === document.id;
      });
      this.documents.splice(documentIndex, 1);

      this.notificationService.showSuccess(response.message);
    } catch (e) {
      this.notificationService.showError(e as string);
    }
  }

  transitionAvailable(): boolean {
    return !([ IssueStatus.DUPLICATE, IssueStatus.CLOSED ].includes(this.issue?.status!));
  }

  canAdminTransition(): boolean {
    return !([ IssueStatus.DUPLICATE, IssueStatus.REPAIRED, IssueStatus.CLOSED ].includes(this.issue?.status!));
  }

  canTenantTransition(): boolean {
    return this.issue?.status === IssueStatus.REPAIRED;
  }

  showTransitionModal() {
    this.isTransitionModalVisible = true;
  }

  dismissTransitionModal() {
    this.isTransitionModalVisible = false;
    this.transitionPayload = {};
  }

  async transitionIssue() {
    this.isTransitioning = true;

    try {
      const response = await this.issueService.transition(this.issueId!, this.getTransitionStatus(), this.transitionPayload);

      this.dismissTransitionModal();
      this.notificationService.showSuccess('Issue status successfully updated');

      this.issue = response;
    } catch (e) {
      this.errorMessage = e as string;
      this.hasError = true;
    } finally {
      this.isTransitioning = false;
    }
  }

  getTransitionButtonText(): string {
    const transition = this.getTransitionStatus();

    if (transition === IssueTransitions.VALIDATE) {
      return 'Mark As Validated';
    }

    if (transition === IssueTransitions.SCHEDULE_REPAIR) {
      return 'Schedule Repair';
    }

    if (transition === IssueTransitions.MARK_REPAIRED) {
      return 'Mark As Repaired';
    }

    if (transition === IssueTransitions.CLOSE) {
      return 'Confirm Repair Done';
    }

    throw new Error('Unknown issue transition');
  }

  canBeMarkedAsDuplicated(): boolean {
    return this.issue?.status === 'Filed';
  }

  showDuplicateModal() {
    this.isDuplicateModalVisible = true;
  }

  dismissDuplicateModal() {
    this.isDuplicateModalVisible = false;
    this.transitionPayload = {};
  }

  async markAsDuplicate() {
    this.isTransitioning = true;

    try {
      const response = await this.issueService.transition(this.issueId!, IssueTransitions.MARK_DUPLICATED, this.transitionPayload);

      this.dismissTransitionModal();
      this.notificationService.showSuccess('Issue marked as duplicate');

      this.issue = response;
    } catch (e) {
      this.errorMessage = e as string;
      this.hasError = true;
    } finally {
      this.isTransitioning = false;
    }
  }

  private getTransitionStatus(): IssueTransitions {
    if (this.issue?.status === IssueStatus.FILED) {
      return IssueTransitions.VALIDATE;
    }

    if (this.issue?.status === IssueStatus.VALIDATED) {
      return IssueTransitions.SCHEDULE_REPAIR;
    }

    if (this.issue?.status === IssueStatus.REPAIR_SCHEDULED) {
      return IssueTransitions.MARK_REPAIRED;
    }

    if (this.issue?.status === IssueStatus.REPAIRED) {
      return IssueTransitions.CLOSE;
    }

    throw new Error();
  }

  protected readonly mapIssueStatusToText = mapIssueStatusToText;
}
