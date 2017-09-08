/* 
* popPanel V1.1 
* author: Z
* Date: 2017-1-19 
* 弹出窗口插件  点击事件外移
*/ 
;(function($){
	var defaults = {
		popMain:".pop-panel",
		txt:"",
		width:"820"
	};
    $.fn.popPanel = function (options) {	//plugin name
		return this.each(function(){
			var opts = $.extend({},defaults,options);
			var pMain = $(opts.popMain);
			var pCon = pMain.find(".pop-con");

			var Title = opts.title
			var Txt = opts.txt;
			var pWidth = opts.width;

			pMain.css({width:pWidth,marginLeft:-pWidth/2})

			function showMaskLayer(){
				$("body").append("<div class='mask'></div>");
			}
			function showPanel(){
				if($(".mask").length<1){showMaskLayer();}
				var pClose = $(".js-close");
				var mLayer = $(".mask");
				
				if(Txt){
					pCon.html(Txt)
				}

				mLayer.fadeIn(200);
				pMain.fadeIn(200);

				mLayer.on("close",function () {
					hidePanel()
				})
				// mLayer.on("click",function () {
				// 	mLayer.trigger("close")
				// })
				pClose.on("click",function () {
					mLayer.trigger("close")
				})
			}
			function hidePanel(){
				var mLayer = $(".mask");
				pMain.fadeOut(200);
				mLayer.fadeOut(200);
			}
			showPanel()		
		});			
   };  
})(jQuery);

$(function(){


	$(".placeholder").each(function(i) {
        var defaultval = $(".placeholder").eq(i).val();
        $(this).bind({
            focus: function() {
                if ($(this).val() == defaultval) {
                    $(this).val("")
                }
            },
            blur: function() {
                if ($(this).val() == "") {
                    $(this).val(defaultval)
                }
            }
        })

    });

});

var emojidata = {
	1:"[微笑]", 2:"[撇嘴]",3:"[色]", 4:"[发呆]", 5:"[得意]", 6:"[流泪]", 7:"[害羞]", 8:"[闭嘴]", 9:"[睡]", 10:"[大哭]", 11:"[尴尬]", 12:"[发怒]", 13:"[调皮]", 14:"[呲牙]", 15:"[惊讶]", 16:"[难过]", 17:"[冷汗]", 18:"[抓狂]", 19:"[吐]", 20:"[偷笑]", 21:"[愉快]", 22:"[白眼]", 23:"[傲慢]", 24:"[困]", 25:"[惊恐]", 26:"[流汗]", 27:"[憨笑]", 28:"[悠闲]", 29:"[奋斗]", 30:"[咒骂]", 31:"[疑问]", 32:"[嘘]", 33:"[晕]", 34:"[衰]", 35:"[骷髅]", 36:"[敲打]", 37:"[再见]", 38:"[擦汗]", 39:"[抠鼻]", 40:"[鼓掌]", 41:"[坏笑]", 42:"[左哼哼]", 43:"[右哼哼]", 44:"[哈欠]", 45:"[鄙视]", 46:"[委屈]", 47:"[快哭了]", 48:"[阴险]", 49:"[亲亲]", 50:"[可怜]", 51:"[菜刀]", 52:"[西瓜]", 53:"[啤酒]", 54:"[咖啡]", 55:"[猪头]", 56:"[玫瑰]", 57:"[凋谢]", 58:"[嘴唇]", 59:"[爱心]", 60:"[心碎]", 61:"[蛋糕]", 62:"[炸弹]", 63:"[便便]", 64:"[月亮]", 65:"[太阳]", 66:"[拥抱]", 67:"[强]", 68:"[弱]", 69:"[握手]", 70:"[胜利]", 71:"[抱拳]", 72:"[勾引]", 73:"[拳头]", 74:"[OK]", 75:"[跳跳]", 76:"[发抖]", 77:"[怄火]", 78:"[转圈]", 79:"😄", 80:"😷", 81:"😂", 82:"😝", 83:"😳", 84:"😱", 85:"😔", 86:"😒", 87:"[嘿哈]", 88:"[捂脸]", 89:"[奸笑]", 90:"[机智]", 91:"[皱眉]", 92:"[耶]", 93:"👻", 94:"🙏", 95:"💪", 96:"🎉", 97:"[礼物]", 98:"[红包]", 99:"[鸡]"
    }
