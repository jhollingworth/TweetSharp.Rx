var loadingHTML = '<div style="width:400px; height:600px"><img src="/content/loading.gif" /></div>';

function CreateTimelineView(data) {
      var toReturn = '';
      $.each(data, function(i, item) {
          toReturn += '<div class="twit">'
            + '<img src="' + item.User.ProfileImageUrl + '" class="twitPic" />'
            + '<div class="twitText">' + item.User.Name 
            + ' said '
            + replaceAtWithHyperlinks(
               replaceHashWithHyperlinks(
                 replaceURLWithHTMLLinks(item.Text)
                                        )
                                     ) + '</div></div>';
      });
      return toReturn;
}


function isArray(obj) {
    return obj.constructor == Array;
}

function limitChars(textid, limit, infodiv) {
    var text = $('#' + textid).val();
    var textlength = text.length;
    if (textlength > limit) {
        $('#' + infodiv).html('You cannot write more then ' + limit + ' characters!');
        $('#' + textid).val(text.substr(0, limit));
        return false;
    }
    else {
        $('#' + infodiv).html('You have ' + (limit - textlength) + ' characters left.');
        return true;
    }
}

function replaceURLWithHTMLLinks(text) { 
  var exp = /(\b(https?):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])/ig; 
   return text.replace(exp,"<a href='$1' target='_blank'>$1</a>");
}

function replaceAtWithHyperlinks(text) {
    var exp = /\@([a-zA-Z_]+)/ig;
    return text.replace(exp, "<a href='/Updates/$1'>@$1</a>");
}

function replaceHashWithHyperlinks(text) {
    var exp = /\#([a-zA-Z]+)/ig;
    return text.replace(exp, "<a href='http://search.twitter.com/search?q=%23$1' target='_blank'>#$1</a>");
}


