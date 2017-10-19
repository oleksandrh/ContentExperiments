import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';

@Component({
    selector: 'scripts',
    templateUrl: './scripts.component.html',
    styleUrls: ['./scripts.component.css']
})
export class ScriptsComponent implements OnInit {
    script: String;

    ngOnInit() {
        $.get("/api/abtest/getScript").done(script => {
            this.script = script;
        });
    }
}
