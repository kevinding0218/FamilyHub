import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';

@Component({
  selector: 'app-jq-datatable-demo',
  templateUrl: './jq-datatable-demo.component.html',
  styleUrls: ['./jq-datatable-demo.component.scss']
})
export class JqDatatableDemoComponent implements OnInit {
  public REST_ROOT = 'https://jsonplaceholder.typicode.com';

  options = {
    dom: 'Bfrtip',
    ajax: (data, callback, settings) => {
      this.http.get(this.REST_ROOT + '/posts')
        .pipe(
          map((response: any) => (response.data || response)),
          catchError(this.handleError),
      )
        .subscribe((response) => {
          console.log('data from rest endpoint', response);
          callback({
            aaData: (<Array<any>>response).slice(0, 100)
          });
        });
    },
    columns: [
      { data: 'userId' },
      { data: 'id' },
      { data: 'title' },
      { data: 'body' },
    ]
  };

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  private handleError(error: any) {
    // In a real world app, we might use a remote logging infrastructure
    // We'd also dig deeper into the error to get a better message
    const errMsg = (error.message) ? error.message :
      error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    console.error(errMsg); // log to console instead
    return Observable.throw(errMsg);
  }
}
