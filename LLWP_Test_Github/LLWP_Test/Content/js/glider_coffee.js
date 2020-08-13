window.addEventListener('load', function () {
    new Glider(document.querySelector('.coffee_glider'), {
        slidesToShow: 5,
        slidesToScroll: 5,
        draggable: true,
        dots: '#dots',
        arrows: {
            prev: '.glider-prev',
            next: '.glider-next'
        }
    });
});