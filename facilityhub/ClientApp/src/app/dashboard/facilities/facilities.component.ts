import { Component, OnInit } from '@angular/core';
import { FacilitiesService } from '../../services';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { getLayers } from '../../utils';
import * as Leaflet from 'leaflet';
import { cilPlus } from '@coreui/icons';

interface NewFacilityPayload {
  name?: string;
  address?: string;
  location?: {
    longitude?: number;
    latitude?: number;
  }
}

@Component({
  selector: 'fh-dashboard-facilities',
  templateUrl: './facilities.component.html',
  styleUrl: './facilities.component.scss'
})
export class FacilitiesComponent implements OnInit {
  facilities: any[] = [];
  icons = { cilPlus };

  isNewModalVisible = false;
  isCreatingFacility = false;
  newFacilityPayload: NewFacilityPayload = {};
  newFacilityMapLayers: Leaflet.Layer[] = [];

  hasError = false;
  errorMessage: string | undefined;

  constructor(title: Title, private facilitiesService: FacilitiesService, private router: Router) {
    title.setTitle('Facilities | Facility Hub');
  }

  async ngOnInit() {
    this.facilities = await this.facilitiesService.getAll();
  }

  async goToDetails(facilityId: string) {
    await this.router.navigate([ 'dashboard', 'facilities', facilityId ]);
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
      zoom: 10,
      center: new Leaflet.LatLng(facility.location.latitude, facility.location.longitude)
    };
  }

  showNewFacilityModal() {
    this.isNewModalVisible = true;
    this.newFacilityMapLayers = getLayers();
  }

  dismissNewFacilityModal() {
    if (this.isCreatingFacility) {
      return;
    }

    this.isNewModalVisible = false;
    this.newFacilityPayload = {};
  }

  async createFacility() {
    this.isCreatingFacility = true;

    try {
      const response = await this.facilitiesService.create(this.newFacilityPayload);
      this.facilities.push(response);
      this.dismissNewFacilityModal();
    }  catch (e: any) {
      this.errorMessage = e.message;
      this.hasError = true;
    } finally {
      this.isCreatingFacility = false;
    }
  }

  getNewFacilityMapOptions(): Leaflet.MapOptions {
    return {
      maxZoom: 18,
      minZoom: 3,
      zoom: 15,
      center: new Leaflet.LatLng(7.2008237, 5.5532686)
    };
  };

  mapClicked(event: any) {
    const alreadyHasMarker = this.newFacilityMapLayers.some(x => x instanceof Leaflet.Marker);

    if (alreadyHasMarker) {
      return;
    }

    const marker = new Leaflet.Marker(
      event.latlng,
      {
        draggable: true,
        icon: new Leaflet.Icon({
          iconSize: [ 50, 50 ],
          iconAnchor: [ 0, 0 ],
          iconUrl: 'assets/icons/marker.png',
        })
      });
    marker.on('dragend', (dragEvent) => {
      const latLng: Leaflet.LatLng = dragEvent.target._latlng;
      this.newFacilityPayload.location = {
        longitude: latLng.lng,
        latitude: latLng.lat
      };
    });
    this.newFacilityMapLayers = [ ...this.newFacilityMapLayers, marker ];
  }
}
