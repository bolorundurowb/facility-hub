import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import {
  FacilitiesService,
  FileDownloadService,
  InvitationsService,
  IssuesService,
  NotificationService
} from '../../services';
import { ActivatedRoute, Router } from '@angular/router';
import * as Leaflet from 'leaflet';
import { getLayers, mapDocumentTypeToText, mapIssueStatusToColour, mapIssueStatusToText } from '../../utils';
import { Location } from '@angular/common';
import { cilArrowLeft, cilCloudDownload, cilCloudUpload, cilNoteAdd, cilTrash, cilUserPlus } from '@coreui/icons';
import { DocumentRes, DocumentType, IssueRes, LocationRes, TenantRes } from '../../components';
import { HttpEventType } from '@angular/common/http';

interface FacilityDetailsDto {
  id: string;
  name: string;
  address: string;
  isTenant: boolean,
  location?: LocationRes,
  tenant?: TenantRes
}

interface FacilityDocumentUploadPayload {
  type?: DocumentType,
  file?: any;
}

interface FacilityTenantPayload {
  name?: string;
  emailAddress?: string;
  phoneNumber?: string;
  startsAt?: Date;
  endsAt?: Date;
  paidAt?: Date;
}

interface FacilityIssuePayload {
  facilityId?: string;
  occurredAt?: Date;
  description?: string;
  location?: string;
  remedialAction?: string;
}

@Component({
  selector: 'fh-dashboard-facility-details',
  templateUrl: './facility-details.component.html',
  styleUrl: './facility-details.component.scss'
})
export class FacilityDetailsComponent implements OnInit {
  isLoading = true;
  icons = { cilCloudUpload, cilNoteAdd, cilCloudDownload, cilTrash, cilArrowLeft, cilUserPlus };

  facilityId?: string;
  facility?: FacilityDetailsDto;
  documents: Array<DocumentRes> = [];
  issues: Array<IssueRes> = [];

  hasError = false;
  errorMessage?: string;

  isNewDocModalVisible = false;
  isUploadingDoc = false;
  newDocPayload: FacilityDocumentUploadPayload = {};

  isNewTenantModalVisible = false;
  isCreatingTenant = false;
  newTenantPayload: FacilityTenantPayload = {};

  isReportModalVisible = false;
  isFilingReport = false;
  newIssuePayload: FacilityIssuePayload = {};

  constructor(title: Title, private facilityService: FacilitiesService, private route: ActivatedRoute,
              private location: Location, private notificationService: NotificationService,
              private downloadService: FileDownloadService, private invitationService: InvitationsService,
              private issueService: IssuesService, private router: Router) {
    title.setTitle('Facility Details | Facility Hub');
  }

  protected readonly mapDocumentTypeToText = mapDocumentTypeToText;

  async ngOnInit() {
    this.isLoading = true;

    try {
      const facilityId = this.route.snapshot.params['facilityId'];

      this.facility = await this.facilityService.getOne(facilityId);
      this.documents = await this.facilityService.getOneDocuments(facilityId);
      this.issues = await this.facilityService.getOneIssues(facilityId);

      this.facilityId = facilityId;
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

  uploadDocument() {
    this.isUploadingDoc = true;
    this.hasError = false;

    this.facilityService.uploadDocument(this.facilityId!, this.newDocPayload)
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

  dismissDocumentModal() {
    this.isNewDocModalVisible = false;
    this.newDocPayload = {};
  }

  documentSelected(event: any) {
    this.newDocPayload.file = event.target.files[0];
  }

  getMapOptions(facility: any): Leaflet.MapOptions {
    return {
      layers: [
        ...getLayers(),
        new Leaflet.Marker(
          new Leaflet.LatLng(facility.location.latitude, facility.location.longitude),
          {
            icon: new Leaflet.Icon({
              iconSize: [ 50, 50 ],
              iconAnchor: [ 0, 0 ],
              iconUrl: 'assets/icons/marker.png',
            })
          })
      ],
      minZoom: 10,
      maxZoom: 18,
      zoom: 17,
      center: new Leaflet.LatLng(facility.location.latitude, facility.location.longitude)
    };
  }

  async downloadFile(document: DocumentRes) {
    this.downloadService.downloadFile(
      document.url,
      `${document.id}-${document.fileName}`
    );
  }

  async deleteDocument(document: DocumentRes) {
    try {
      const response = await this.facilityService.deleteDocument(this.facilityId!, document.id);

      const documentIndex = this.documents.findIndex((doc) => {
        return doc.id === document.id;
      });
      this.documents.splice(documentIndex, 1);

      this.notificationService.showSuccess(response.message);
    } catch (e) {
      this.notificationService.showError(e as string);
    }
  }

  showNewTenantModal() {
    this.isNewTenantModalVisible = true;
  }

  dismissNewTenantModal() {
    this.isNewTenantModalVisible = false;
    this.newTenantPayload = {};
  }

  async inviteTenant() {
    this.isCreatingTenant = true;
    this.hasError = false;

    try {
      const response = await this.invitationService.inviteTenant(this.facilityId!, this.newTenantPayload);

      if (this.facility) {
        this.facility.tenant = response;
      }

      this.dismissNewTenantModal();
      this.notificationService.showSuccess('Tenant successfully registered');
    } catch (e) {
      this.errorMessage = e as string;
      this.hasError = true;
    } finally {
      this.isCreatingTenant = false;
    }
  }

  showNewIssueModal() {
    this.isReportModalVisible = true;
  }

  dismissNewIssueModal() {
    this.isReportModalVisible = false;
    this.newIssuePayload = {};
  }

  async reportIssue() {
    this.isFilingReport = true;
    this.hasError = false;

    try {
      const response = await this.issueService.report(this.facilityId!, this.newIssuePayload);
      this.issues.unshift(response)

      this.dismissNewIssueModal();
      this.notificationService.showSuccess('Report successfully filed');
    } catch (e) {
      this.errorMessage = e as string;
      this.hasError = true;
    } finally {
      this.isFilingReport = false;
    }
  }

  async goToDetails(issue: IssueRes) {
    await this.router.navigate([ 'dashboard', 'issues', issue.id ]);
  }

  protected readonly getIssueColour = mapIssueStatusToColour;
  protected readonly mapIssueStatusToText = mapIssueStatusToText;
}
