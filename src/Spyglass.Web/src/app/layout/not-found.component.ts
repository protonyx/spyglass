import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'sg-not-found',
  template: `
    <div class="card">
      <div class="card-header">
        404 Page Not Found
      </div>
      <div class="card-block">
        <div class="card-text">
          Hey! It looks like this page doesn't exist yet.
        </div>
      </div>
    </div>
  `,
  styles: [`
    :host {
      text-align: center;
    }
  `]
})
export class NotFoundComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
