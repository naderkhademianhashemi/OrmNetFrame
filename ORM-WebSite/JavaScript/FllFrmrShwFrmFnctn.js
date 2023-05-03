// JavaScript source code

function checkAll_Form_Instance(objRef) {

    var inputList = $('input[id$=chk_Form_Instance]');
    for (var i = 0; i < inputList.length; i++) {
        
        var row = inputList[i].parentNode.parentNode;
        if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
            if (objRef.checked) {
               
                row.style.backgroundColor = "gold";
                inputList[i].checked = true;
            }
            else {
               
                if (row.rowIndex % 2 == 0) {
                   
                    row.style.backgroundColor = "#E7E7FF";
                }
                else {
                    row.style.backgroundColor = "#F7F7F7";
                }
                inputList[i].checked = false;
            }
        }
    }
}
function IsNumeric(obj) {
    var sText = obj.value;
    var ValidChars = "0123456789";
    var ValidChars1 = ".";
    var IsNumber = true;
    var Char;
    var numdot = 0;

    for (i = 0; i < sText.length && IsNumber == true; i++) {
        Char = sText.charAt(i);
        if (ValidChars.indexOf(Char) == -1 && ValidChars1.indexOf(Char) == -1) {
            IsNumber = false;
        }
        if (ValidChars1.indexOf(Char) >= 0)
            numdot++;
    }
    if (numdot > 1)
        IsNumber = false;
    return IsNumber;

}

function checkAll(objRef) {
    //chkSelectAll

    var inputList = $('input[id$=Chk01]');

    for (var i = 0; i < inputList.length; i++) {
        //Get the Cell To find out ColumnIndex
        var row = inputList[i].parentNode.parentNode;
        if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
            if (objRef.checked) {
                //If the header checkbox is checked
                //check all checkboxes
                //and highlight all rows
                row.style.backgroundColor = "gold";
                inputList[i].checked = true;
            }
            else {
                //If the header checkbox is checked
                //uncheck all checkboxes
                //and change rowcolor back to original 
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "#E7E7FF";
                }
                else {
                    row.style.backgroundColor = "#F7F7F7";
                }
                inputList[i].checked = false;
            }
        }
    }
}


function check_Grid() {
    var chk_Form_InstanceArray = $('input[id$=chk_Form_Instance]');
    var messageAlert = 'به منظور گرفتن خروجی ، ابتدا می بایست سطری را از جدول پاسخ ها انتخاب کنید';

    var flagChecked = false;

    for (var i = 0; i < chk_Form_InstanceArray.length; i++) {
        if (chk_Form_InstanceArray[i].checked) {
            flagChecked = true;
        }
    }

    if (flagChecked == false) {
        alert(messageAlert);
        return false;
    }
    else {
        return true;
    }
}

$(document).ready(function () {
    $('input[type=text]').focus(function () {
        $(this).select();
    });
});

document.querySelector("input[id*='txtCount']").addEventListener("keypress", function (evt) {
    if (evt.which < 48 || evt.which > 57) {
        evt.preventDefault();
    }
});



