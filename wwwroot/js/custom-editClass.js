var myNewClassName = document.getElementById("myNewClassName").focus();

//This is for the setting icon to open the modal.
$(document).keypress(function(e) {
    if ($("#exampleModalForSetting").hasClass('in') && (e.keycode == 13 || e.which == 13)) {
        alert("Enter is pressed");
    }
})

/* This is function to toggle the navbar with the body */
document.getElementById('checkMyBar').addEventListener('change', function() {
    if (this.checked) {
        document.getElementById("myDiv").style.visibility = "hidden";
    } else {
        document.getElementById("myDiv").style.visibility = "visible";
    }
});