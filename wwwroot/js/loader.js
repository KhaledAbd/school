function myFunctionForLoading() {
    myVar = setTimeout(showPage, 5200);
}

function showPage() {
    document.getElementById("loader-container").style.display = "none";
    document.getElementById("myDiv").style.display = "block";
}