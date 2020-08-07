/* This is function to toggle the navbar with the body */
document.getElementById('checkMyBar').addEventListener('change', function() {
    if (this.checked) {
        document.getElementById("myDiv").style.visibility = "hidden";
    } else {
        document.getElementById("myDiv").style.visibility = "visible";
    }
});

/************************************************************************************************************
 *************************************************** AJAX ***************************************************
 ************************************************************************************************************ */
/*
function functionForSearchingInTable() {
    var input, filter, table, tr, td, i, txtValue;

    input = document.getElementById("myInputForSearching");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}
//=======================================================================================================================================
*/