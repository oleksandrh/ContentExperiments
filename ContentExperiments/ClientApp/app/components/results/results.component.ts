import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { Http } from '@angular/http';

@Component({
    selector: 'results',
    templateUrl: './results.component.html',
    styleUrls: ['results.component.css']
})
export class ResultsComponent implements OnInit {
    results: any;
    selectedABTest: any;
    constructor(public http: Http) {

    }
    ngOnInit() {
        this.http.get("http://localhost:14255/api/abtest/getABtests")
            // Call map on the response observable to get the parsed people object
            .map(res => res.json())
            // Subscribe to the observable to get the parsed people object and attach it to the
            // component
            .subscribe(results => {
                this.results = results
            });
    }
    deleteRow(id: number) {
        this.http.get("http://localhost:14255/api/abtest/delete?id=" + id)
            .subscribe(results => { });
        this.results = this.results.filter(el => { return el.id != id });
    }
}
