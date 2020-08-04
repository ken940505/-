$(function(){
    var date_now = new Date();
    var year = date_now.getFullYear();
    var month = date_now.getMonth()+1 < 10 ? "0"+(date_now.getMonth()+1) : (date_now.getMonth()+1);
    var date = date_now.getDate() < 10 ? "0"+date_now.getDate() : date_now.getDate();
    var month1 = date_now.getMonth()+2 < 10 ? "0"+(date_now.getMonth()+1) : (date_now.getMonth()+1);
    $("#day").attr("min",year+"-"+month+"-"+date);       
})