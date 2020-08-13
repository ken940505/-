window.addEventListener('load', function () {
    new Glider(document.querySelector('.env_glider'), {
        slidesToShow: 3,
        slidesToScroll: 1,
        draggable: true,
        dots: '#dots',
        arrows: {
            prev: '.glider-prev',
            next: '.glider-next'
        }
    });
})
