//For printing the page ..
function printThePageNow() {
    if (document.getElementById("myButtonForPrinting").onclick) {
        document.getElementById("cont").style.display = "block";
        document.getElementById("modal-dialog").style.display = "none";
        window.print();
        location.replace("studentTable.html");
    }
}

$(window).on('load', function() {
    document.getElementById("cont").style.display = "none";
    $('#exampleModalCenter').modal('show');
});