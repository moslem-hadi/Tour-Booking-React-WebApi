$(document).ready(function () {
    $('li a.containert').click(function () {
        $('ul.acitem').slideUp('fast')
        $(this).next('ul.acitem').slideToggle('fast');
        return false;
    });
    
});




function validate(evt) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}


function Count(text, max) {
    var maxlength = max;
    var object = document.getElementById(text.id)
    if (object.value.length > maxlength) {
        object.focus();
        object.value = text.value.substring(0, maxlength);
        object.scrollTop = object.scrollHeight;
        return false;
    }
    return true;
}




function addCommas(nStr) {
    x = ('' + nStr).replace(/,/g, '');
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x)) {
        x = x.replace(rgx, '$1' + ',' + '$2');
    }
    return x;
}



function intFormat(number) {
    var regex = /(\d)((\d{3},?)+)$/;
    number = number.split(',').join('');

    while (regex.test(number)) {
        number = number.replace(regex, '$1,$2');
    }
    return number;
}

function numFormat(number) {
    var pointReg = /([\d,\.]*)\.(\d*)$/, f;
    if (pointReg.test(number)) {
        f = RegExp.$2;
        return intFormat(RegExp.$1) + '.' + f;
    }
    return intFormat(number);
}









s1 = new Array("", "يک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه")
s2 = new Array("ده", "يازده", "دوازده", "سيزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده")
s3 = new Array("", "", "بيست", "سي", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود")
s4 = new Array("", "صد", "دويست", "سيصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد")
function convert(z, elname) {
    z = z.replace(/,/g, "");
    z = parseInt(z);
    if (z == 0) { result = "صفر" } else {
        result = ""
        convert2(z)
    }
    //alert("."+result) 

    if (result == "Error") {
        document.getElementById(elname).innerHTML = "";
    }
    else {
        document.getElementById(elname).innerHTML = result + " تومان";

    }
}

function convert2(y) {
    if (y > 999999999 && y < 1000000000000)
    { bghb = (y % 1000000000); temp = y - bghb; bil = temp / 1000000000; convert3r(bil); result = result + " ميليارد"; if (bghb != 0) { result = result + " و "; convert2(bghb); } }
    else if (y > 999999 && y <= 999999999)
    { bghm = (y % 1000000); temp = y - bghm; mil = temp / 1000000; convert3r(mil); result = result + " ميليون"; if (bghm != 0) { result = result + " و "; convert2(bghm); } }
    else if (y > 999 && y <= 999999) { bghh = (y % 1000); temp = y - bghh; hez = temp / 1000; convert3r(hez); result = result + " هزار"; if (bghh != 0) { result = result + " و "; convert2(bghh); } }
    else if (y <= 999) convert3r(y); else result = "Error";
}

function convert3r(x) {
    bgh = (x % 100); temp = x - bgh; sad = temp / 100;
    if (bgh == 0) { result = result + s4[sad] }
    else
    {
        if (x > 100) result = result + s4[sad] + " و ";
        if (bgh < 10) { result = result + s1[bgh] }
        else if (bgh < 20) { bgh2 = (bgh % 10); result = result + s2[bgh2] }
        else {
            bgh2 = (bgh % 10); temp = bgh - bgh2; dah = temp / 10;
            if (bgh2 == 0) { result = result + s3[dah] }
            else { result = result + s3[dah] + " و " + s1[bgh2] }
        }
    }
}

