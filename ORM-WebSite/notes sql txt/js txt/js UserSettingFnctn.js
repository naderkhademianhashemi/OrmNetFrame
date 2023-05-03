// JavaScript source code

    UsersInGroups();
    function UsersInGroups() {
            var grd = document.getElementById('<%=this.GridView1.ClientID%>');
    var Inputs = grd.getElementsByTagName('input');
    for (var i = 0; i < Inputs.length; i++) {
                if (Inputs[i].type == 'checkbox' && Inputs[i].checked == true)
    Inputs[i].parentNode.parentNode.parentNode.style.backgroundColor = "gold";
            }
        }
    function check_CodePersonel() {
            var id = '<%=this.txtCodePerson.ClientID%>';
    var CodePersonel = document.getElementById(id);

    if (isNaN(CodePersonel.value) || CodePersonel.value.length < 5 ||
                CodePersonel.value.length > 8) {
        alert('لطفا یک شماره پرسنلی معتبر وارد کنید  ');
    CodePersonel.value = "";
    return false;
            }
    else {

                return true;
            }
        }

