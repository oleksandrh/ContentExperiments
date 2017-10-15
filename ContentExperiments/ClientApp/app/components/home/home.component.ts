import { Component, ViewChild, ElementRef, NgZone } from '@angular/core';
import { FormsModule } from '@angular/forms';
import * as $ from 'jquery';
import { SafeResourceUrl, DomSanitizer } from '@angular/platform-browser';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Http } from '@angular/http';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})

export class HomeComponent {
    src: SafeResourceUrl;
    _browserPortlet: String = "#browser-portlet";
    sanitizerLocal: DomSanitizer;
    modelA: Fields;
    modelB: Fields;
    @ViewChild('iframe') iframe: any;
    @BlockUI('frame') blockIFrame: NgBlockUI;


    constructor(private sanitizer: DomSanitizer, public zone: NgZone, public http: Http) {
        this.sanitizerLocal = sanitizer;
    }
    loadPage(url: String) {
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
            this.zone.run(() => this.modelA = new Fields(this.getSpecificPath($(e.target)), e.target.outerHTML));
            this.zone.run(() => this.modelB = new Fields(this.getSpecificPath($(e.target)), e.target.outerHTML));
        });
    }
    save(url: String) {
        $.post("/home/save", { selector: this.modelA.selectedElementSelector, modelA: this.modelA.selectedElementHtml, modelB: this.modelB.selectedElementHtml, url: url }).done(r => {})
        //this.http.post("/home/save", JSON.stringify({ modelA: this.modelA, modelB: this.modelB, page: url })).subscribe(r => { });
    }
    getSpecificPath(elem) {
        if (elem.length != 1) throw 'Requires one element.';

        var path, node = elem;
        while (node.length) {
            var realNode = node[0], name = realNode.localName;
            if (!name) break;
            name = name.toLowerCase();

            var parent = node.parent();

            var siblings = parent.children(name);
            if (siblings.length > 1) {
                name += ':eq(' + siblings.index(realNode) + ')';
            }

            path = name + (path ? '>' + path : '');
            node = parent;
        }

        return path;
    };
}

export class Fields {
    constructor(selectedElementSelector: String, selectedElementHtml: String) {
        this.selectedElementSelector = selectedElementSelector;
        this.selectedElementHtml = selectedElementHtml;
    }
    selectedElementSelector: String;
    selectedElementHtml: String;
}


