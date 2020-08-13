document.getElementById("sure-check").addEventListener("click", function () {
    Swal.fire({
        title: '付款成功',
        text: '將為您跳轉至會員中心',
        imageUrl: '../Content/images/alertCheck.png',
        imageWidth: 400,
        imageHeight: 200,
        imageAlt: 'Custom image',
    })
});

