import { Component, OnInit } from '@angular/core';
import { DocumentRes, IssueRes } from '../../components';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FileDownloadService, IssuesService } from '../../services';
import { cilArrowLeft, cilCloudDownload, cilCloudUpload, cilNoteAdd, cilTrash, cilUserPlus } from '@coreui/icons';

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
  documents: Array<DocumentRes> = [];

  hasError = false;
  errorMessage?: string;

  constructor(title: Title,  private route: ActivatedRoute, private location: Location,
              private downloadService: FileDownloadService, private issueService: IssuesService) {
    title.setTitle('Issue Details | Facility Hub');
  }

  async ngOnInit() {
    this.isLoading = true;

    try {
      const issueId = this.route.snapshot.params['issueId'];

      this.issue = await this.issueService.getOne(issueId);
      this.documents = await this.issueService.getOneDocuments(issueId);

      this.issueId = issueId;
    } finally {
      this.isLoading = false;
    }
  }

  goBack() {
    this.location.back();
  }
}
