// JavaScript source code

function confirm_delete1() {
    alert("شما نمیتوانید کاربر مدیر سایت را حذف کنید");
    return false;
}
function confirm_delete() {
    if (confirm("آیا برای حذف این کاربر اطمینان دارید؟ ") == true)
        return true;
    else
        return false;
}
function confirm_Admin_Grant() {
    var txtUserVal = $('[id$=txtUser]').val();
    if (txtUserVal.length < 1) {
        alert('وارد کردن نام کاربری  یا انتخاب سطر از لیست  کاربران الزامی است.');
        return false;
    }
    if (confirm("آیا از اختصاص دسترسی ارشداطمینان دارید؟ ") == true)
        return true;
    else {
        $('[id$=txtUser]').val('');
        return false;
    }
}
function confirm_Admin_Revoke() {
    var txtValUsrToFllFrmrClntID = $('[id$=txt_UserToFillFormer]').val();
    if (txtValUsrToFllFrmrClntID.length < 1) {
        alert('وارد کردن نام کاربری یا انتخاب سطر از لیست کاربران ارشد الزامی است.');
        return false;
    }
    if (confirm("آیا از منع دسترسی ارشد اطمینان دارید؟ ") == true)
        return true;
    else {
        $('[id$=txt_UserToFillFormer]').val('');
        return false;
    }
}
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