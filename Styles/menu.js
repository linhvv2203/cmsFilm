
//$(document).bind('touchmove', touch).bind('touchend', touchend).bind('scroll', touch);
$(document).ready(function(){   
   $('#clip_menu').click(function(e){
					$(".wrap_menu").slideToggle(1);
					$("#header").toggleClass("left275");
					$("#body").toggleClass("left275");
					$(this).toggleClass("_hd-btn-menu");
					
					if($(this).hasClass('_hd-btn-menu')) {

                                             $("#wrapper").height($("#menu_cate").height());
                                             var target_top = $("#wrapper").offset().top;
                                            $('html, body').animate({scrollTop:target_top}, 0);


                               } else {

                                             $("#wrapper").height("auto");

                               }
					
					
				})
          });   