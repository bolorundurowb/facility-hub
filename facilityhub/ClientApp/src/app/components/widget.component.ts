import { Component, Input } from '@angular/core';

@Component({
  selector: 'fh-widget',
  template: `
    <div class="widget" [ngStyle]="{ 'background-color': colour}">
      <div class="title">
        {{ title }}
      </div>
      <div class="value">
        {{ value }}
      </div>
    </div>
  `,
  styles: `
    .widget {
      height: 8.75rem;
      width: 100%;
      border-radius: 0.75rem;
      padding: 1rem;
      margin: 1rem;
    }

    .widget .title {
      font-size: 1.1rem;
    }

    .widget .value {
      font-weight: bolder;
      text-align: end;
      margin-top: 0.25rem;
      font-size: 4rem;
    }
  `
})
export class WidgetComponent {
  @Input() colour?: string;
  @Input() title?: string;
  @Input() value: any;
}
