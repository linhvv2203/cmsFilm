/***************************/
//@Author: Adrian "yEnS" Mato Gondelle
//@website: www.yensdesign.com
//@email: yensamg@gmail.com
//@license: Feel free to use it, but keep this credits please!					
/***************************/

$().ready(function(){
	//SETUP Mp3 files player example
	var playerMp3 = new SWFObject("player.swf","myplayer1","0","0","9");
	playerMp3.addVariable("logo","css/images/logo.jpg");
	playerMp3.addVariable("file","songs/BraveHeart.mp3");
	playerMp3.addVariable("icons","false");
	playerMp3.write("player1");
	//create a javascript object to allow us send events to the flash player
	var player1 = document.getElementById("myplayer1");
	var mute1 = 0;
	var status1 = $("#status1");
	
	//EVENTS for Mp3 files player
	$("#play1").click(function(){
		player1.sendEvent("PLAY","true");
		status1.text("PLAYING...");
	});
	$("#pause1").click(function(){
		player1.sendEvent("PLAY","false");
		status1.text("PAUSED");
	});
	$("#mute1").click(function(){
		if(mute1 == 0){
			player1.sendEvent("mute","true");
			mute1 = 1;
		}
		else{
			player1.sendEvent("mute","false");
			mute1 = 0;
		}
	});
	
	//Setup FLV files player example
	var playerFlv = new SWFObject("player.swf","myplayer2","400","300","9");
	playerFlv.addVariable("screencolor","white");
	playerFlv.addVariable("image","videos/preview.jpg");
	playerFlv.addVariable("file","videos/video.flv");
	playerFlv.write("player2");
	//create a javascript object to allow us send events to the flash player
	var player2 = document.getElementById("myplayer2");
	var mute2 = 0;
	
	//EVENTS for FLV files player
	$("#play2").click(function(){
		player2.sendEvent("PLAY","true");
	});
	$("#pause2").click(function(){
		player2.sendEvent("PLAY","false");
	});
	$("#mute2").click(function(){
		if(mute2 == 0){
			player2.sendEvent("mute","true");
			mute2 = 1;
		}
		else{
			player2.sendEvent("mute","false");
			mute2 = 0;
		}
	});

});