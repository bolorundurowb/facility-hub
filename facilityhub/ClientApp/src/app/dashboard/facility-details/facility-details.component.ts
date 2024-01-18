import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { FacilitiesService, FileDownloadService, NotificationService } from '../../services';
import { ActivatedRoute } from '@angular/router';
import * as Leaflet from 'leaflet';
import { getLayers } from '../../utils';
import { Location } from '@angular/common';
import { cilArrowLeft, cilCloudDownload, cilCloudUpload, cilNoteAdd, cilTrash, cilUserPlus } from '@coreui/icons';
import { DocumentRes, DocumentType, LocationRes, TenantRes } from '../../components';
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
  emailAddress?: string;
  startsAt?: Date;
  endsAt?: Date;
  paidAt?: Date;
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
  issues: Array<any> = [];

  hasError = false;
  errorMessage?: string;

  isNewDocModalVisible = false;
  isUploadingDoc = false;
  newDocPayload: FacilityDocumentUploadPayload = {};

  isNewTenantModalVisible = false;
  isCreatingTenant = false;
  newTenantPayload: FacilityTenantPayload = {};

  constructor(title: Title, private facilityService: FacilitiesService, private route: ActivatedRoute,
              private location: Location, private notificationService: NotificationService,
              private downloadService: FileDownloadService) {
    title.setTitle('Facility Details | Facility Hub');
  }

  goBack() {
    this.location.back();
  }

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

  showDocumentModal() {
    this.isNewDocModalVisible = true;
  }

  uploadDocument() {
    this.isUploadingDoc = true;

    this.facilityService.uploadDocument(this.facilityId!, this.newDocPayload)
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
}
