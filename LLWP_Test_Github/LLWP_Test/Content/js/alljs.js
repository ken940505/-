
$('.access_li').css('width', $('#box').width() / 3);

var items = document.querySelectorAll('#colla1')

for (var i = 0; i < items.length; i++) {
    items[i].addEventListener("click", function () {
        var className = this.getElementsByClassName('in collapse show')[0];
        if (typeof className == "undefined") {
            this.getElementsByTagName('img')[0].src = '../Content/images/accordion_icn_down.png';
        } else {
            this.getElementsByTagName('img')[0].src = '../Content/images/accordion_icn_open.png';
        }
    })
}

$('.carousel').carousel({
    interval: 4000
})

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

var colla2items = document.querySelectorAll('#colla2')

for (var i = 0; i < colla2items.length; i++) {
    colla2items[i].addEventListener("click", function () {
        var className = this.getElementsByClassName('in collapse show')[0];
        if (typeof className == "undefined") {
            this.getElementsByTagName('img')[0].src = '../Content/images/accordion_icn_down_orange.png';
        } else {
            this.getElementsByTagName('img')[0].src = '../Content/images/accordion_icn_open_orange.png';
        }
    })
}

//有hover效果的版本
$(function () {
    //關於顯示月曆組數、按鈕功能的變數們
    let setCount = 1; //顯示月曆組數，預設為顯示第一組
    let lastSetCount = 3; //月曆組數有多少組，目前有3組，lastSetCount為最後的setCount

    //關於抓取入住/退房日期的變數們
    let dateClickCount = 0; //紀錄點擊日期的次數(目前設定2次為一個週期)
    let InDate_month;
    let InDate_day;
    let OutDate_month;
    let OutDate_day;
    //mouseover的開關狀態，以區別click事件
    var isMouseover = false;
    var isClick = false;

    //初始化預設9-12月隱藏
    $("#iset2m9,#iset2m10,#iset3m11,#iset3m12").hide();

    //按日曆格子，2次為一個週期，第3次要回到第1次的功能
    $(".day").on({
        "mouseover": function () {
            //滑鼠按第二次後不會有hover效果
            if (dateClickCount < 2) {
                isMouseover = true;
                $(this).css({ "background-color": "#9e8415", "color": "#F5F1ED" });
            }
        },
        "mouseout": function () {
            //滑鼠按第二次後不會有hover效果，當isMouseover=true才會執行
            if (dateClickCount < 2 && isMouseover)
                $(this).css({ "background-color": "#F5F1ED", "color": "#3B3B3B" });
        },
        "click": function () {
            dateClickCount++;
            isClick = true;
            isMouseover = false;
            if (dateClickCount >= 3) {
                dateClickCount = 1;
                $("#i-InDate-year,#i-InDate-month,#i-InDate-day,#i-OutDate-year,#i-OutDate-month,#i-OutDate-day").text("");
            }

            if (isClick) {
                switch (dateClickCount) {
                    case 1:
                        //重設日曆格子樣式
                        $(".day").css({ "background-color": "#F5F1ED", "color": "#3B3B3B" });
                        //dateClickCount=1，點第1下抓入房日期
                        //console.log("dateClickCount" + dateClickCount);

                        InDate_month = parseInt($(this).parent().parent().children("p").children("span").text());
                        InDate_day = parseInt($(this).text());
                        $("#i-InDate-year").text("2020 年");
                        $("#i-InDate-month").text(` ${InDate_month} 月`);
                        $("#i-InDate-day").text(` ${InDate_day} 日`);
                        //console.log("InDate_month: " + InDate_month);
                        //console.log("InDate_day: " + InDate_day);
                        //入住顏色動態
                        $(this).css({ "background-color": "#9e8415", "color": "#F5F1ED" });
                        break;
                    case 2:
                        //console.log("dateClickCount" + dateClickCount);
                        //dateClickCount=2，點第2下抓退房日期//抓退房日期
                        OutDate_month = parseInt($(this).parent().parent().children("p").children("span").text());
                        OutDate_day = parseInt($(this).text());
                        if (InDate_month === OutDate_month && OutDate_day > InDate_day) {
                            $("#i-OutDate-year").text("2020 年");
                            $("#i-OutDate-month").text(` ${OutDate_month} 月`);
                            $("#i-OutDate-day").text(` ${OutDate_day} 日`);
                            //退房顏色動態
                            $(this).css({ "background-color": "#9e8415", "color": "#F5F1ED" });
                        }
                        else if (OutDate_month > InDate_month) {
                            $("#i-OutDate-year").text("2020 年");
                            $("#i-OutDate-month").text(` ${OutDate_month} 月`);
                            $("#i-OutDate-day").text(` ${OutDate_day} 日`);
                            //退房顏色動態
                            $(this).css({ "background-color": "#9e8415", "color": "#F5F1ED" });
                        }
                        else {
                            alert("請重新選取日期");
                            dateClickCount = 0;
                            $("#i-InDate-year,#i-InDate-month,#i-InDate-day,#i-OutDate-year,#i-OutDate-month,#i-OutDate-day").text("");
                            //重設日曆格子樣式
                            $(".day").css({ "background-color": "#F5F1ED", "color": "#3B3B3B" });
                        }
                        //console.log("OutDate_month: " + OutDate_month);
                        //console.log("OutDate_day: " + OutDate_day);
                        break;
                }
            }
        }
    });

    $("#inext").on("click", function () {
        setCount++;
        switch (setCount) {
            case 2:
                //當setCount=2，切換至第2組月曆(9、10月)
                //console.log("setCount: " + setCount);
                $("#iset1m7,#iset1m8").hide();
                $("#iset2m9,#iset2m10").show();
                $("#iprevious").css("cursor", "pointer");
                break;
            case 3:
                //當setCount=3，切換至第3組月曆(11、12月)
                //console.log("setCount: " + setCount);
                $("#iset2m9,#iset2m10").hide();
                $("#iset3m11,#iset3m12").show();
                $("#inext").css("cursor", "not-allowed");
                break;
            case 4:
                //當setCount=4，讓setCount變回3，控制setCount不大於3
                setCount = 3;
                //console.log("setCount: " + setCount);
                break;
        }
    })

    $("#iprevious").on("click", function () {
        setCount--;
        switch (setCount) {
            case 0:
                //當setCount=1，讓setCount變回0，控制setCount不小於1(至少要有1組月曆)
                setCount = 1;
                //console.log("setCount: " + setCount);
                break;
            case 1:
                //當setCount=1，切換至第1組月曆(7、8月)
                //console.log("setCount: " + setCount);
                $("#iset2m9,#iset2m10").hide();
                $("#iset1m7,#iset1m8").show();
                $("#iprevious").css("cursor", "not-allowed");
                $("#inext").css("cursor", "pointer");
                break;
            case 2:
                //當setCount=2，切換至第2組月曆(9、10月)
                //console.log("setCount: " + setCount);
                $("#iset3m11,#iset3m12").hide();
                $("#iset2m9,#iset2m10").show();
                break;
        }
    })
});

function navRWD() {
    var para = document.querySelector(".navClass-RWD");
    para.classList.toggle("navRWDClass");
}

$(function () {
    let navPosition = $("#navbar").offset().top;

    $(window).scroll(function () {
        let scrollTop = $(this).scrollTop();
        if (scrollTop > navPosition)
            $("#navbar").addClass("fixed-nav");
        else
            $("#navbar").removeClass("fixed-nav");
    });
});

//$(function () {
//    var nav = $("#navbar");
//    if (nav.length) {
//        navPosition = nav.offset().top;
//    }

//    $(window).scroll(function () {
//        let scrollTop = $(this).scrollTop();
//        if (scrollTop > navPosition)
//            $("#navbar").addClass("fixed-nav");
//        else
//            $("#navbar").removeClass("fixed-nav");
//    });
//});

$(document).ready(function () {
    $(".tab-content > li").last().hide(); //隱藏.tab-content下一層的最後一個li

    $(".tabs li").click(function () { //點擊.tabs li時，執行以下動作
        const type = $(this).data('type'); //.tabs li的data('type')設為

        if (type === 1) {
            $(".tab-content > li").last().hide();
            $(".tab-content > li").first().show();
            $('.tab-content > li').css('background-color', '#b1d3c5')  //下一層的li全部改為#b1d3c5
        }

        if (type === 2) {
            $(".tab-content > li").first().hide();
            $(".tab-content > li").last().show();
            $('.tab-content > li').css('background-color', '#e1f1e7')//下一層的li全部改為#e1f1e7
            $('.tabs > li').last().css('background-color', '#e1f1e7')//.tabs下一層的最後一個li改為#e1f1e7
        }

    })
})

$(document).ready(function () {
    $("#pTwoAct").hide();

    $('#peopleNum').change(function () {  //change事件 
        var p1 = $(this).children('option:selected').val(); //selected的值 

        if (p1 == 1) {
            $("#pOneAct").show();
            $("#pTwoAct").hide();
        }

        if (p1 == 2) {
            $("#pOneAct").hide();
            $("#pTwoAct").show();

        }
    })
})

$(document).ready(function () {
    $("#petTry").hide();

    $('.petTry').change(function () {  //change事件 
        var p1 = $(this).children('option:selected').val(); //selected的值 

        if (p1 == 1) {
            $("#petTry").show();
        }

        if (p1 == 2) {
            $("#petTry").hide();

        }
    })
})
//活動
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

function openClass(evt, className) {
    var i, x, tablinks;
    x = document.getElementsByClassName("class");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablink");
    for (i = 0; i < x.length; i++) {
        tablinks[i].classList.remove("actselectbtncolor");
    }
    document.getElementById(className).style.display = "block";
    evt.currentTarget.classList.add("actselectbtncolor");
    
}
var mybtn = document.getElementsByClassName("testbtn")[0];
    mybtn.click();
//var mybtn = document.getElementsByClassName("testbtn")[0];
//mybtn.click();

function openClass2(evt, className) {
    var i, x, tablinks;
    x = document.getElementsByClassName("class2");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablink2");
    for (i = 0; i < x.length; i++) {
        tablinks[i].classList.remove("actselectbtncolor2");
    }
    document.getElementById(className).style.display = "block";
    evt.currentTarget.classList.add("actselectbtncolor2");
    
}
var mybtn2 = document.getElementsByClassName("testbtn2")[0];
    mybtn2.click();


$(function () {
    var date_now = new Date();
    var year = date_now.getFullYear();
    var month = date_now.getMonth() + 1 < 10 ? "0" + (date_now.getMonth() + 1) : (date_now.getMonth() + 1);
    var date = date_now.getDate() < 10 ? "0" + date_now.getDate() : date_now.getDate();
    var month1 = date_now.getMonth() + 2 < 10 ? "0" + (date_now.getMonth() + 1) : (date_now.getMonth() + 1);
    $("#day").attr("min", year + "-" + month + "-" + date);
})