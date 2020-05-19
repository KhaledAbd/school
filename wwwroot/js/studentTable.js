//======================================================================================================================================
$(function() {
    $('#toggle-one').bootstrapToggle();
})

//============================================================================================================================================
//For deleting any row in the table..

$('.table tbody').on('click', '.myDeleteButton', function() {
    $(this).closest('tr').remove();
});


//=======================================================================================================================================
document.getElementById("searchInput").focus();

//=======================================================================================================================================
//This function for table..

/************************************************************************************************************
 *************************************************** AJAX ***************************************************
 ************************************************************************************************************ */
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