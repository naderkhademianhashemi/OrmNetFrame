
// JavaScript source code

$(document).ready(function () {
    $("input[type=checkbox]:checked").parent().parent().parent().css({ "background-color": "gold" });
});

function check_CodePersonel() {
    var CdPrsnl = $('[id$=txtCodePerson]').val();
    if (isNaN(CdPrsnl) || CdPrsnl.length < 5 ||
        CdPrsnl.length > 8) {
        alert('لطفا یک شماره پرسنلی معتبر وارد کنید  ');
        $('[id$=txtCodePerson]').val('');
        return false;
    }
    else {
        return true;
    }
}

