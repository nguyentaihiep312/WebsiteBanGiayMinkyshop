$("#carousel-home .owl-carousel").on("initialized.owl.carousel", function () {
    setTimeout(function () {
        $("#carousel-home .owl-carousel .owl-item.active .owl-slide-animated").addClass("is-transitioned");
        $("section").show();
    }, 200);
});

var $owlCarousel = $("#carousel-home .owl-carousel").owlCarousel({ items: 1, loop: !0, nav: !1, dots: !0, responsive: { 0: { dots: !1 }, 767: { dots: !1 }, 768: { dots: !0 } } });

$owlCarousel.on("changed.owl.carousel", function (e) {
    $(".owl-slide-animated").removeClass("is-transitioned");
    const $currentOwlItem = $(".owl-item").eq(e.item.index);
    $currentOwlItem.find(".owl-slide-animated").addClass("is-transitioned");
});

$owlCarousel.on("resize.owl.carousel", function () {
    setTimeout(function () {
    }, 50);
});