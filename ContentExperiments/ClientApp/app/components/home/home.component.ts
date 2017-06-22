import { Component, ViewChild, ElementRef } from '@angular/core';
import * as $ from 'jquery';
import { SafeResourceUrl, DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    src: SafeResourceUrl;
    @ViewChild('frame') frame: ElementRef;

    constructor(private sanitizer: DomSanitizer) {
        this.src = this.sanitizer.bypassSecurityTrustResourceUrl("/Scrapping/GetUrl?encoding=UTF8&url=https://habrahabr.ru/");
    }

    highlightHover = function (element) {

    }

    unhighlightHover = function (element) {

    }

    handlerIn(e) {
        var self = $(this);
        var hover = self.find(".hover");
        if (hover.length == 0) {
            var parents = self.parents(".hover");
            for (var i = 0; i < parents.length; i++) {
                $(parents[i]).removeClass("hover");
            }
            self.addClass("hover");
        }
    }

    handlerOut(e) {
        var self = $(this);
        if (self.hasClass("hover")) {
            var parent = self.parent();
            var hovers = parent.find(".hover");
            for (var i = 0; i < hovers.length; i++) {
                $(hovers[i]).removeClass("hover");
            }
            self.removeClass("hover");
        }
    }

    onLoad() {
        $("iframe").contents().find("head")
            .append("<link type='text/css' rel='stylesheet' href='/css/frame.css'>");
        $("iframe").contents().find("*")
            .hover(this.handlerIn, this.handlerOut);

    }
}
