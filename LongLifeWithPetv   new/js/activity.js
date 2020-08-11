$(document).ready(function () {
    //設定標題欄
    document.title = "活動總覽";
    //視窗的拖動
    var bool = false;
    var offsetX = 0;
    var offsetY = 0;
    $("#title").mousedown(function () {
        bool = true;
        offsetX = event.offsetX;
        offsetY = event.offsetY;
        $("#ten").css('cursor', 'move');
    })
        .mouseup(function () {
            bool = false;
        })
    $(document).mousemove(function (e) {
        if (!bool)
            return;
        var x = event.clientX - offsetX;
        var y = event.clientY - offsetY;
        $("#main").css("left", x);
        $("#main").css("top", y);
    })
    //視窗的關閉
    $("#img").click(function () {
        $("#main").removeClass("show");
        $("#main").addClass("none");
        // $("#open").addClass("show");
    });
    $("#open").click(function () {
        $("#main").removeClass("none");
        $("#main").addClass("show");
    });
});

