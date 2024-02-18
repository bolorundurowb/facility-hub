import { Component, OnInit } from '@angular/core';
import { DocumentRes, DocumentUploadPayload, IssueRes } from '../../components';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { AuthService, FileDownloadService, IssuesService, NotificationService } from '../../services';
import { cilArrowLeft, cilCloudDownload, cilCloudUpload, cilNoteAdd, cilTrash, cilUserPlus } from '@coreui/icons';
import { getIssueColour } from '../../utils';
import { HttpEventType } from '@angular/common/http';

@Component({
  selector: 'fh-dashboard-issue-details',
  templateUrl: './issue-details.component.html',
  styleUrl: './issue-details.component.scss'
})
export  class IssueDetailsComponent implements OnInit {
  isLoading = true;
  icons = { cilCloudUpload, cilNoteAdd, cilCloudDownload, cilTrash, cilArrowLeft, cilUserPlus };

  issueId?: string;
  issue?: IssueRes;
  isTenant = true;
  documents: Array<DocumentRes> = [];

  hasError = false;
  errorMessage?: string;

  isNewDocModalVisible = false;
  isUploadingDoc = false;
  newDocPayload: DocumentUploadPayload = {};

  constructor(title: Title,  private route: ActivatedRoute, private location: Location, private authService: AuthService,
              private downloadService: FileDownloadService, private issueService: IssuesService,
              private notificationService: NotificationService) {
    title.setTitle('Issue Details | Facility Hub');
  }

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

  protected readonly getIssueColour = getIssueColour;

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
            console.log(document);
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
}
