$(document).scroll(function () {
    var y = $(this).scrollTop();
    if (y > 800) {
        $('#bi-widget-scrolltext').fadeIn();


    } else {
        $('#bi-widget-scrolltext').fadeOut();
    }
});