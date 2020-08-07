/*
    This is file javascript for (helloPage.html) page.
*/

//This is for the setting icon to open the modal.
$(document).keypress(function(e) {
    if ($("#exampleModalForSetting").hasClass('in') && (e.keycode == 13 || e.which == 13)) {
        alert("Enter is pressed");
    }
})

//====================== upload photo ===================


/*  ==========================================
    SHOW UPLOADED IMAGE
* ========================================== */
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function(e) {
            $('#imageResult')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

//======================================= close popup after click the button ====================================

$(document).ready(function() {
    $(".btn").click(function() {
        //$("#exampleModalForSecondButton").modal('show');
        $("#exampleModalForFirstButton").modal('hide');
    });


    document.getElementById("saveFirstButton").onclick = function() {
        Command: toastr["info"]("", "تمّت العمليةُ بنجاحٍ")
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": true,
            "progressBar": false,
            "positionClass": "toast-bottom-center",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "100",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeOut",
            "hideMethod": "fadeIn"
        }
    };
});

$(document).ready(function() {
    $(".btn").click(function() {
        //$("#exampleModalForSecondButton").modal('show');
        $("#exampleModalForSecondButton").modal('hide');
    });

    var passwordBeroreEditing = document.getElementById("old-password-editing").value,
        passwordAfterEditing = document.getElementById("new-password-editing").value;

    if (passwordAfterEditing == passwordBeroreEditing) {
        document.getElementById("saveSecondButton").onclick = function() {
            Command: toastr["info"]("", "تمّت العمليةُ بنجاحٍ")
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": true,
                "progressBar": false,
                "positionClass": "toast-bottom-center",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "100",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeOut",
                "hideMethod": "fadeIn"
            };
        }
    } else if (passwordAfterEditing !== passwordBeroreEditing) {
        document.getElementById("saveSecondButton").onclick = function() {
            Command: toastr["warning"]("", "يُوجَد خطأ في تطابُق كلمتيّ المُرور")
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": true,
                "progressBar": false,
                "positionClass": "toast-bottom-center",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "100",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeOut",
                "hideMethod": "fadeIn"
            };
        }
    } else if (passwordAfterEditing == "" || passwordBeroreEditing == "") {
        document.getElementById("saveSecondButton").onclick = function() {
            Command: toastr["error"]("", "إحدى الخانيْن فارغتان")
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": true,
                "progressBar": false,
                "positionClass": "toast-bottom-center",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "100",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeOut",
                "hideMethod": "fadeIn"
            };
        }
    }
});