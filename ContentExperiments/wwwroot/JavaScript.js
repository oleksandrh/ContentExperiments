var abTests = @abTests;
var domain = "http://localhost:14255";
var selectedAbTest;
(function () {
    
    for (var i = 0; i < abTests.length; i++) {
        if (abTests[i].Url == window.location.href) {
            selectedAbTest = abTests[i];
        }
    }
    if (selectedAbTest) {
        selectedAbTest.PageViews++;
        var stateB = (selectedAbTest.PageViews % 2 == 0);
        if (stateB) {
            $(selectedAbTest.Selector).replaceWith(selectedAbTest.HtmlB);
        } else {
            $(selectedAbTest.Selector).replaceWith(selectedAbTest.HtmlA);
        }
        $.get(domain + "/api/ABTest/RegisterPageviews?pageviews=" + selectedAbTest.PageViews + "&id=" + selectedAbTest.Id);
        $(document).on('click', selectedAbTest.Selector.substring(0, selectedAbTest.Selector.indexOf("a") + 1), function () {
            if (stateB) {
                $.get(domain + "/api/ABTest/click?state=B&id=" + selectedAbTest.Id);
            } else {
                $.get(domain + "/api/ABTest/click?state=A&id=" + selectedAbTest.Id);
            }
        });
    }
})();