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
    var txtUserClientID = '<%=this.txtUser.ClientID%>';
    var txtUser = document.getElementById(txtUserClientID);
    if (txtUser.value.length < 1) {
        alert('وارد کردن نام کاربری  یا انتخاب سطر از لیست  کاربران الزامی است.');
        return false;
    }
    if (confirm("آیا از اختصاص دسترسی ارشداطمینان دارید؟ ") == true)
        return true;
    else {

        txtUser.value = "";
        return false;
    }
}
function confirm_Admin_Revoke() {
    var txt_UserToFillFormerClientID = '<%=this.txt_UserToFillFormer.ClientID%>';
    var txt_UserToFill = document.getElementById(txt_UserToFillFormerClientID);
    if (txt_UserToFill.value.length < 1) {
        alert('وارد کردن نام کاربری یا انتخاب سطر از لیست کاربران ارشد الزامی است.');
        return false;
    }
    if (confirm("آیا از منع دسترسی ارشد اطمینان دارید؟ ") == true)
        return true;
    else {
        txt_UserToFill.value = "";
        return false;
    }
}
function check_CodePersonel() {
    var id = '<%=this.txtCodePerson.ClientID%>';
    var CodePersonel = document.getElementById(id);

    if (isNaN(CodePersonel.value) || CodePersonel.value.length < 5) {
        alert(' برای جستجو براساس شماره پرسنلی ، لطفا یک عدد حداقل 5 رقمی وارد کنید ');
        CodePersonel.value = "";
        return false;
    }
    else {
        return true;
    }
}