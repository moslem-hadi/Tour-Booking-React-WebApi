$(document).ready(function () {
    $('ul.acitem').slideUp('fast');
    $('ul.collapsible li a.containert').click(function () {
        $('ul.acitem').slideUp('fast')
        $(this).next('ul.acitem').slideToggle('fast');
        return false;
    });
});

function OpenPopup() {
    window.open("popupfilelist.aspx", "List", "scrollbars=no,resizable=no,width=900,height=600");
    return false;
}

function validate(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}



$(document).ready(function () {
    $("#chbNew").change(function () {
        if ($("#chbHot").attr('checked') == 'checked') {
            $("#chbHot").removeAttr('checked');
        }
    });

    $("#chbHot").change(function () {
        if ($("#chbNew").attr('checked') == 'checked') {
            $("#chbNew").removeAttr('checked');
        }
    });
});


$(document).ready(function () {
    $('.dropdown').click(function () {
        $('.dropdown-menu').slideToggle();
    });
    $('#main').click(function () {
        $('.dropdown-menu').slideUp();
    });
});






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






function clock() {
    var today = new Date();
    var hours = today.getHours();
    var minutes = today.getMinutes();
    var seconds = today.getSeconds();
    var time_holder; // holds the time

    //if the first radio button is checked display 12-hours format time

    // add a leading zero if less than 10
    hours = ((hours < 10) ? "0" + hours : hours);
    minutes = ((minutes < 10) ? "0" + minutes : minutes);
    seconds = ((seconds < 10) ? "0" + seconds : seconds);

    time_holder = hours + ":" + minutes + ":" + seconds;

    document.getElementById('jsClock').innerHTML = time_holder;





    // keep the clock ticking
    setTimeout("clock()", 1000);
}
// end hiding -->


function addCommas(nStr) {
    x = ('' + nStr).replace(/,/g, '');
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x)) {
        x = x.replace(rgx, '$1' + ',' + '$2');
    }
    return x;
}
