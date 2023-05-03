// JavaScript source code
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
function Check_Click(objRef) {
    //Get the Row based on checkbox
    var row = objRef.parentNode.parentNode;
    if (objRef.checked) {
        //If checked change color to Aqua
        row.style.backgroundColor = "gold";
    }
    else {
        //If not checked change back to original color
        if (row.rowIndex % 2 == 0) {
            //Alternating Row Color
            row.style.backgroundColor = "#E7E7FF";
        }
        else {
            row.style.backgroundColor = "#F7F7F7";
        }

    }

    //Get the reference of GridView
    var GridView = row.parentNode;

    //Get all input elements in Gridview
    var inputList = GridView.getElementsByTagName("input");

    for (var i = 0; i < inputList.length; i++) {
        //The First element is the Header Checkbox
        var headerCheckBox = inputList[0];

        //Based on all or none checkboxes
        //are checked check/uncheck Header Checkbox
        var checked = true;
        if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
            if (!inputList[i].checked) {
                checked = false;
                break;
            }
        }
    }
    headerCheckBox.checked = checked;

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

function checkAll_Form_Instance(objRef) {

    var inputList = $('input[id$=chk_Form_Instance]');
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

function MouseEvents(objRef, evt) {
    var checkbox = objRef.getElementsByTagName("input")[0];
    if (evt.type == "mouseover") {
        objRef.style.backgroundColor = "brown";
    }
    else {
        if (checkbox.checked) {
            objRef.style.backgroundColor = "gold";
        }
        else if (evt.type == "mouseout") {
            if (objRef.rowIndex % 2 == 0) {
                //Alternating Row Color
                objRef.style.backgroundColor = "#E7E7FF";
            }
            else {
                objRef.style.backgroundColor = "#F7F7F7";
            }

        }
    }
}


