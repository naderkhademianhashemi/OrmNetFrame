
$(document).ready(function () {

    $('input[id$=RSelectAll').click(function () {
        var Inputs = $('input[id$=cbR]');
        for (var iCount = 0; iCount < Inputs.length; ++iCount) {
            if ($(this).is(':checked')) {
                Inputs[iCount].checked = true;
            }
            else {
                Inputs[iCount].checked = false;
            }
        }
    })
        
});





   