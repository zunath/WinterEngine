jQuery(document).ready(function($){	
	$("#gazpo-slider").tabs({fx:{opacity: "toggle"}}).tabs("rotate", 5000, true);
		$("#gazpo-slider").hover(
			function() {
				$("#gazpo-slider").tabs("rotate",0,true);
			},
			function() {
				$("#gazpo-slider").tabs("rotate",5000,true);
			}
		);	
	$(".carousel-posts").jCarouselLite({
       		scroll: 1,
			auto: 8000
   		});	
	$('.main-menu li:has(ul)').addClass('submenu');		
});