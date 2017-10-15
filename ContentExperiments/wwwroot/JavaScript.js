var selector = @selector;
var htmlA = @htmlA;
var htmlB = @htmlB;
var pageviews = @pageviews;
var id = @id;
var domain = "http://localhost:14255";
(function () {
    pageviews++
    if (pageviews % 2 == 0) {
        $(selector).replaceWith(htmlB);
    } else {
        $(selector).replaceWith(htmlA);
    }
    $.get(domain + "/api/ABTest/RegisterPageviews?pageviews=" + pageviews + "&id=" + id);
    $(document).on('click', selector, function () {
        if (pageviews % 2 == 0) {
            $.get(domain + "/api/ABTest/click?state=B&id=" + id);
        } else {
            $.get(domain + "/api/ABTest/click?state=A&id=" + id);
        }
    });
})();