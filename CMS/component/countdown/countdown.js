function countdown(yr, m, d, hr, min,offid) {
    var tz = +3.5;
    var montharray = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
    var theyear = yr;
    var themonth = m;
    var theday = d;
    var thehour = hr;
    var theminute = min;
    var thoffid = offid;
    var today = new Date();
    var todayy = today.getYear();
    if (todayy < 1000) {
        todayy += 1900;
    }
    var todaym = today.getMonth();
    var todayd = today.getDate();
    var todayh = today.getHours();
    var todaymin = today.getMinutes();
    var todaysec = today.getSeconds();
    var todaystring1 = montharray[todaym] + " " + todayd + ", " + todayy + " " + todayh + ":" + todaymin + ":" + todaysec;
    var todaystring = Date.parse(todaystring1) + (tz * 1000 * 60 * 60);
    var futurestring1 = (montharray[m - 1] + " " + d + ", " + yr + " " + hr + ":" + min);
    var futurestring = Date.parse(futurestring1) - (today.getTimezoneOffset() * (1000 * 60));
    var dd = futurestring - todaystring;
    var dday = Math.floor(dd / (60 * 60 * 1000 * 24) * 1);
    var dhour = Math.floor((dd % (60 * 60 * 1000 * 24)) / (60 * 60 * 1000) * 1);
    var dmin = Math.floor(((dd % (60 * 60 * 1000 * 24)) % (60 * 60 * 1000)) / (60 * 1000) * 1);
    var dsec = Math.floor((((dd % (60 * 60 * 1000 * 24)) % (60 * 60 * 1000)) % (60 * 1000)) / 1000 * 1);
    if (dday <= 0 && dhour <= 0 && dmin <= 0 && dsec <= 0) {
        $("#countdown_" + thoffid).css({display:'none'});
        return;
    }
    else {
        elm = $("#countdown_" + thoffid);
        $(elm).html('');
        $('<ul class="timer">\n\
                <li>' + dsec + '<span>ثانیه</span></li>\n\
                <li>' + dmin + '<span>دقیقه</span></li>\n\
                <li>' + dhour + '<span>ساعت</span></li>\n\
                <li>' + dday + '<span>روز</span></li>\n\
            </ul>').appendTo(elm);
        setTimeout(function() {
            countdown(theyear, themonth, theday, thehour, theminute,thoffid);
        }, 1000);
    }
}