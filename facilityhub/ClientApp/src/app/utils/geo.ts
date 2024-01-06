import * as Leaflet from 'leaflet';

export const getLayers = (): Leaflet.Layer[] => {
  return [
    new Leaflet.TileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; OpenStreetMap'
    } as Leaflet.TileLayerOptions),
  ] as Leaflet.Layer[];
};
