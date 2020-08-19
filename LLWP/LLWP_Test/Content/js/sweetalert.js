document.getElementById("sure-check").addEventListener("click", function () {
    Swal.fire({
        title: '付款成功',
        text: '將為您跳轉至會員中心',
        imageUrl: '../Content/images/alertCheck.png',
        imageWidth: 400,
        imageHeight: 200,
        imageAlt: 'Custom image',

    });

    setTimeout(function () { window.location.href = "/Members/MemberProfile" }, 5000);

      

});
function actsurecheck() {
    Swal.fire({
        title: '付款成功',
        text: '將為您跳轉至活動頁面',
        imageUrl: '../Content/images/actshopping1.jpg',
        imageWidth: 400,
        imageHeight: 200,
        imageAlt: 'Custom image1',

    });

    setTimeout(function () { window.location.href = "/Activity/Index" }, 5000);
}


