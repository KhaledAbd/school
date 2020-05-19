/*
    This is file javascript for (helloPage.html) page.
*/

//This is for the setting icon to open the modal.
$(document).keypress(function(e) {
    if ($("#exampleModalForSetting").hasClass('in') && (e.keycode == 13 || e.which == 13)) {
        alert("Enter is pressed");
    }
})