scrollTo = function (el, offeset) {
    pos = (el && el.size() > 0) ? el.offset().top : 0;
    jQuery('html,body').animate({
        scrollTop: pos + (offeset ? offeset : 0)
    }, 'slow');
}
var handleGoToTop = function () {
    $('.footer-tools').on('click', '.go-top', function (e) {
        scrollTo();
        e.preventDefault();
    });
}
handleGoToTop();