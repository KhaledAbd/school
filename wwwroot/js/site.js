// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    $('#addAbsent').click(function () {
        var table = document.getElementById("myTable");
        var checkbox = table.getElementsByClassName("myCheckbox");
        var absents = [];
        for (var i = 1; i <= checkbox.length; i++) {
            if (checkbox[i-1].checked) {
                absents.push({
                    "StudentId": parseInt(table.rows[i].cells[3].innerHTML),
                    "AbsentCheck": true
                });
            }
            else {
                absents.push({
                    "StudentId": parseInt(table.rows[i].cells[3].innerHTML),
                    "AbsentCheck": false
                });
            }
        }

        $.ajax({
            method: "POST",
            url:'/api/AbsentApi/setAbsent',
            data: JSON.stringify(absents),
            contentType: 'application/json',
            success: function (data) {
                console.log(data.isInsert);
            },
            error: function (error) {
                alert(error.statusCode);
            }  
        });
    });

    $('#addStudent').click(function () {
        var name = document.getElementById("recipient-name").value;
        var student = {
            StudentName: name
        };

        $.ajax({
            method: 'POST',
            url: '/api/AbsentApi/setStudent',
            data: JSON.stringify(student),
            contentType: 'application/json',
            success: function (data) {
                console.log(data.isInsert);
            },
            error: function (error) {
                alert(error.statusCode);
            }
        });
    });
});