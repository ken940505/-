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