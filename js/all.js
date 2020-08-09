function navRWD(){ 
    var para = document.querySelector(".navClass-RWD"); 
    para.classList.toggle("navRWDClass"); 
}

$(function(){                 
    let navPosition=$("#navbar").offset().top;
    
    $(window).scroll(function(){
         let scrollTop=$(this).scrollTop();
         console.log("scrollTop="+scrollTop);
         if(scrollTop>navPosition)
            $("#navbar").addClass("fixed-nav");
        else
        $("#navbar").removeClass("fixed-nav");
    });
});

$(document).ready(function(){
    $(".tab-content > li").last().hide(); //隱藏.tab-content下一層的最後一個li

    $(".tabs li").click(function() { //點擊.tabs li時，執行以下動作
        const type = $(this).data('type'); //.tabs li的data('type')設為

        if (type === 1) {
            $(".tab-content > li").last().hide();
            $(".tab-content > li").first().show();
            $('.tab-content > li').css('background-color','#b1d3c5')  //下一層的li全部改為#b1d3c5
        }

        if (type === 2) {
            $(".tab-content > li").first().hide();
            $(".tab-content > li").last().show();
            $('.tab-content > li').css('background-color','#e1f1e7')//下一層的li全部改為#e1f1e7
            $('.tabs > li').last().css('background-color','#e1f1e7')//.tabs下一層的最後一個li改為#e1f1e7
        }
        
    })
})

$(document).ready(function(){
    $("#pTwoAct").hide();

  $('#peopleNum').change(function(){  //change事件 
  var p1=$(this).children('option:selected').val(); //selected的值 

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

$(document).ready(function(){
    $("#petTry").hide();

  $('.petTry').change(function(){  //change事件 
  var p1=$(this).children('option:selected').val(); //selected的值 

  if (p1 == 1) {
            $("#petTry").show();
        }

  if (p1 == 2) {
    $("#petTry").hide();

        }
  }) 
})
document.getElementById("sure-check").addEventListener("click",function(){
    Swal.fire({
title: '付款成功',
text: '將為您跳轉至會員中心',
imageUrl: './images/alertCheck.png',
imageWidth: 400,
imageHeight: 200,
imageAlt: 'Custom image',
})
});
  $(document).ready(function(){
      $(".tab-content > li").last().hide(); //隱藏.tab-content下一層的最後一個li

      $(".tabs li").click(function() { //點擊.tabs li時，執行以下動作
          const type = $(this).data('type'); //.tabs li的data('type')設為

          if (type === 1) {
              $(".tab-content > li").last().hide();
              $(".tab-content > li").first().show();
              $('.tab-content > li').css('background-color','#b1d3c5')  //下一層的li全部改為#b1d3c5
          }

          if (type === 2) {
              $(".tab-content > li").first().hide();
              $(".tab-content > li").last().show();
              $('.tab-content > li').css('background-color','#e1f1e7')//下一層的li全部改為#e1f1e7
              $('.tabs > li').last().css('background-color','#e1f1e7')//.tabs下一層的最後一個li改為#e1f1e7
          }
          
      })
  })

  $(document).ready(function(){
      $("#pTwoAct").hide();

    $('#peopleNum').change(function(){  //change事件 
    var p1=$(this).children('option:selected').val(); //selected的值 

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

  $(document).ready(function(){
      $("#petTry").hide();

    $('.petTry').change(function(){  //change事件 
    var p1=$(this).children('option:selected').val(); //selected的值 

    if (p1 == 1) {
              $("#petTry").show();
          }

    if (p1 == 2) {
      $("#petTry").hide();

          }
    }) 
  })

