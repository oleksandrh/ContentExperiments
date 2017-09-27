import { Component, ViewChild, ElementRef } from '@angular/core';
import * as $ from 'jquery';
import { SafeResourceUrl, DomSanitizer } from '@angular/platform-browser';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    src: SafeResourceUrl;
    _browserPortlet: String = "#browser-portlet";
    sanitizerLocal: DomSanitizer;

    @ViewChild('iframe') iframe: any;
    @BlockUI('frame') blockIFrame: NgBlockUI;
    

    constructor(private sanitizer: DomSanitizer) {
        this.sanitizerLocal = sanitizer;
    }
    loadPage(url:String) {
        this.src = this.sanitizerLocal.bypassSecurityTrustResourceUrl(`/Scrapping/GetUrl?encoding=UTF8&url=${url}`);
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

    // browser control
    blockPortlet() {
        this.blockIFrame.start('Loading...');
    }

    unblockPortlet() {
        this.blockIFrame.stop();
    }

    reloadPageClick() {
        this.blockPortlet();
        $("iframe").get(0).contentDocument.location.reload(true);
    }

    goBackPageClick() {
            this.blockPortlet();
            $("iframe").get(0).contentWindow.history.back();
    }

    goForwardPageClick() {
        this.blockPortlet();
        $("iframe").get(0).contentWindow.history.forward();
    }

    onLoad() {
        $("iframe").contents().find("head")
            .append("<link type='text/css' rel='stylesheet' href='/css/frame.css'>");
        $("iframe").contents().find("*")
            .hover(this.handlerIn, this.handlerOut);
        this.unblockPortlet();
        $("iframe").contents().click((e) => {
            e.preventDefault();
            setTimeout(() => {
                console.log($("iframe").get(0).contentWindow.location.href); 
            }, 2000);
        });
    }
}
