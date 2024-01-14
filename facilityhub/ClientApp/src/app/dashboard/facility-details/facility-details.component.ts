import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { FacilitiesService } from '../../services';
import { ActivatedRoute } from '@angular/router';
import * as Leaflet from 'leaflet';
import { getLayers } from '../../utils';
import { Location } from '@angular/common'
import { cilArrowLeft, cilCloudDownload, cilCloudUpload, cilNoteAdd, cilTrash } from '@coreui/icons';

@Component({
  selector: 'fh-dashboard-facility-details',
  templateUrl: './facility-details.component.html',
  styleUrl: './facility-details.component.scss'
})
export class FacilityDetailsComponent implements OnInit {
  isLoading = true;
  icons = { cilCloudUpload, cilNoteAdd, cilCloudDownload, cilTrash, cilArrowLeft };

  facilityId?: string;
  facility?: any;
  documents: Array<any> = [];
  issues: Array<any> = [];

  constructor(title: Title, private facilityService: FacilitiesService, private route: ActivatedRoute, private location: Location) {
    title.setTitle('Facility Details | Facility Hub');
  }

   goBack() {
this.location.back();
  }

  async ngOnInit() {
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
}
