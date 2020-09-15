var paymentCheckedInput = $("#paymentCheckedInput").val();

if (paymentCheckedInput == "true")
    paysurecheck();
else if (paymentCheckedInput == "false")
    paid();

function paysurecheck() {
    Swal.fire({
        title: '付款成功',
        text: '將為您跳轉至會員中心',
        imageUrl: '../images/alertCheck.png',
        imageWidth: 400,
        imageHeight: 200,
        imageAlt: 'Custom image',
    });

    setTimeout(function () { window.location.href = "/Members/MemberProfile" }, 5000);
}

//function actsurecheck() {
//    Swal.fire({
//        title: '付款成功',
//        text: '將為您跳轉至活動頁面',
//        imageUrl: '../images/actshopping1.jpg',
//        imageWidth: 400,
//        imageHeight: 200,
//        imageAlt: 'Custom image1',

//    });

//    setTimeout(function () { window.location.href = "/Activity/Index" }, 5000);
//}


function actsurecheck() {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            cancelButton: 'btn btn-danger mr5',
            confirmButton: 'btn btn-success ml5'
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: '確定是否購買',
        text: "購買後恕不得取消退款!",
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: '取消!',
        confirmButtonText: '確定購買!',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            $("#stripe-button").click();
        }
    });
}

function paid() {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            cancelButton: 'btn btn-danger mr5',
            confirmButton: 'btn btn-success ml5'
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: '已購買，請進行信用卡付款',
        text: "信用卡付款後在我的活動查看已購買活動",
        icon: 'success',
        reverseButtons: true,
        confirmButtonText: '回到活動首頁'
    }).then((result) => {
        $("#actsure-check").click();
        setTimeout(function () { window.location.href = "/Activity/Index" }, 5000);
    });
}