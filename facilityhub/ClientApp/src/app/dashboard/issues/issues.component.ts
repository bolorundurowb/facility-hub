import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { IssuesService } from '../../services';
import { cilLightbulb } from '@coreui/icons';
import { IssueRes } from '../../components';
import { Router } from '@angular/router';

@Component({
  selector: 'fh-dashboard-issues',
  templateUrl: './issues.component.html',
  styleUrl: './issues.component.scss'
})
export class IssuesComponent implements OnInit {
  isLoading = true;

  issues: Array<IssueRes> = [];
  icons = { cilLightbulb };

  constructor(title: Title, private issueService: IssuesService, private router: Router) {
    title.setTitle('Issues | Facility Hub');
  }

  async ngOnInit() {
    this.issues = await this.issueService.getAll();
    this.isLoading = false;
  }

  async goToDetails(issue: IssueRes) {
    await this.router.navigate([ 'dashboard', 'issues', issue.id ]);
  }
}
