import { Component, OnInit } from '@angular/core';
import { FacilitiesService } from '../../services';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { getLayers } from '../../utils';
import * as Leaflet from 'leaflet';

@Component({
  selector: 'fh-dashboard-facilities',
  templateUrl: './facilities.component.html',
  styleUrl: './facilities.component.scss'
})
export class FacilitiesComponent implements OnInit {
  facilities: any[] = [];

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
}
