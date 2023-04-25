function nospaces(t) {
    if (t.value.match(/\s/g)) {
        t.value = t.value.replace(/\s/g, '-');
    } 
}

function setshort(val) {
    document.getElementById('txtShort').value = val.replace(/\s/g, '-');
}