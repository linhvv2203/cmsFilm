/* 
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */



$(function() {
	// if the function argument is given to overlay,
	// it is assumed to be the onBeforeLoad event listener
	$('.lnk-sub-menu').click(function(){
		if($(this).hasClass('lnk-sub-show')) {
		
			$(this).removeClass('lnk-sub-show');
			$(this).parent().find('ul.sub-menu').slideUp(150);
		
		} else {
			
			$('.lnk-sub-menu').removeClass('lnk-sub-show')
			$('ul.sub-menu').slideUp(150);
			$(this).addClass('lnk-sub-show');
			$(this).parent().find('ul.sub-menu').slideDown(150);
		
		}
    });
	
});


