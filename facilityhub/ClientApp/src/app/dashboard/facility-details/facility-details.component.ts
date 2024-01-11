import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { FacilitiesService } from '../../services';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'fh-dashboard-facility-details',
  templateUrl: './facility-details.component.html',
  styleUrl: './facility-details.component.scss'
})
export class FacilityDetailsComponent implements OnInit {
  isLoading = true;

  facilityId?: string;
  facility?: any;
  documents: Array<any> = [];
  issues: Array<any> = [];

  constructor(title: Title, private facilityService: FacilitiesService, private route: ActivatedRoute) {
    title.setTitle('Facility Details | Facility Hub');
  }

  async ngOnInit() {
    try {
      const facilityId = this.route.snapshot.params['facilityId'];

      this.facility = await this.facilityService.getOne(facilityId);
      this.documents = await this.facilityService.getOneDocuments(facilityId);
      this.issues = await this.facilityService.getOneIssues(facilityId);

      this.facilityId = facilityId;
    }
    finally {
      this.isLoading = false;
    }
  }
}
