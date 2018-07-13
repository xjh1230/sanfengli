var rowHtml = '<div class="list-col">{item}</div>';
var itemHtml = '<div class="list-col-box js-list-select" mval="{MediaId}"><div class="list-col-con"><h5>更新于 {updateTime}</h5>{mainbody}</div><div class="list-col-mask"><i></i></div></div>';
var lv1Html = '<div class="menu-selected-pre"><h6>{title}</h6><div class="menu-selected-pre-img"><img src="{thumb_url}" alt=""></div><a href="{url}"  target="_blank" class="menu-selected-pre-mask"><span class="menu-selected-pre-hint">预览文章</span></a></div>';
var lv2Html = '<div class="menu-selected-pre2"><div class="menu-selected-pre-img2"><img src="{thumb_url}" alt=""></div><h6>{title}</h6><a href="{url}"  target="_blank" class="menu-selected-pre-mask"><span class="menu-selected-pre-hint">预览文章</span></a></div>';
function createNewsItem(list) {
    var result = '';
    if (list!=undefined&&list.length > 0) {
        var len = Math.ceil(list.length / 2);
        var j = 0;
        for (var i = 0; i < len; i++) {
            var obj1 =list[j];
            var obj2=list[j+1];
            var mainbody1 = newsHtml(obj1.NewsContent);
            var item = itemHtml.replace('{updateTime}', obj1.UpdateTime).replace('{MediaId}', obj1.MediaId).replace('{mainbody}', mainbody1);
            if (obj2 != undefined) {
                var mainbody2 = newsHtml(obj2.NewsContent);
                item += itemHtml.replace('{updateTime}', obj2.UpdateTime).replace('{MediaId}', obj2.MediaId).replace('{mainbody}', mainbody2);
            }
            result += rowHtml.replace('{item}', item);
            j += 2;
        }

    }
    return result;
}

function newsHtml(jsonstr) {
    var obj=jQuery.parseJSON(jsonstr);
    var htmlStr = lv1Html.replace('{title}', obj.news_item[0].title).replace('{thumb_url}', obj.news_item[0].thumb_url).replace('{url}', obj.news_item[0].url);
    for (var i = 1; i < obj.news_item.length; i++) {
        htmlStr += lv2Html.replace('{title}', obj.news_item[i].title).replace('{thumb_url}', obj.news_item[i].thumb_url).replace('{url}', obj.news_item[i].url);
    }
    return htmlStr;
}

function msgimgHtml(list) {
    var t = '';
    if (list!=undefined&&list.length > 0) {
        for (var i = 0; i < list.length; i++) {
            t += '<div class="select-item pic-wp" mval="' + list[i].MediaId + '"><p class="pic-view" ><img src="' + list[i].MUrl + '" alt="' + list[i].MName + '"></p><div class="mytw_mask">√</div></div >';
        }
    }
    return t;
}