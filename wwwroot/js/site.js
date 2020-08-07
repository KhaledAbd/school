// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var clsId;
var transBtn;
function setStage(stageId, stageName) {
    localStorage.setItem('stageId', stageId);
    localStorage.setItem('stageName', stageName);

}

function setClass(classId, className) {
    localStorage.setItem('classId', classId);
    localStorage.setItem('className', className);

}

function sendClass(e) {
    var button = $(e);
    clsId = button.val;
}

function deleteStd(e) {
    var btn = $(e);
    $.ajax({
        method: 'DELETE',
        url: '/api/student/delete/' + btn.parents('td').parent()[0].cells[3].innerHTML,
        contentType: 'application/json',
        success: function (data) {
            if (data.isDelete) {
                alertify.success('تم حذف بنجاح');
            } else {
                alert('هناك مشكله عاود تشغيل البرنامج')
            }
        }
     })
}
function transferStd(e) {
    transBtn = $(e);
}
function changeName(e) {
    transBtn = $(e);
    $('#oldName').val(transBtn.parents('td').parent()[0].cells[2].innerHTML);
}

$(document).ready(function () {

    $('#stageTable').append += localStorage.getItem('stageName');
    $('#classTable').append += localStorage.getItem('className');
    //$('#schoolTable').val()
    $('#stageName').val(localStorage.getItem('stageName'));
    $('#Stage').val(localStorage.getItem('stageId'));

    $('.table tbody').on('click', '.myDeleteButton', function () {
        $(this).closest('tr').remove();
    });
    $('#addAbsent').click(function () {
        var table = document.getElementById("myTable");
        var checkbox = table.getElementsByClassName("myCheckbox");
        var absents = [];
        for (var i = 1; i <= checkbox.length; i++) {
            if (checkbox[i - 1].checked) {
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
        alertify.confirm('يرجى التأكد من الغياب', function () {
            $.ajax({
                method: "POST",
                url: '/api/absent/setAbsent',
                data: JSON.stringify(absents),
                contentType: 'application/json',
                success: function (data) {
                    if (data.isInsert) {
                        alertify.success('تم الاضافة بنجاح');
                    } else {
                        alertify.error('لقد تم اخذ الغياب بالفعل');
                    }
                },
                error: function (error) {
                    console.log(error);
                }
            })
            $('#addAbsent').off("click");

        });
        $('.ajs-header').text('');
        $('.ajs-ok').text('موافق');
        $('.ajs-cancel').text('رافض');
    });

    $('#addStudent').click(function () {
        var name = document.getElementById("recipient-name").value;
        var student = {
            StudentName: name,
            ClassFk: parseInt(localStorage.getItem('classId')),
            TimeAbsent: 0,
            IsReject: false,
            TimeAbsentDaily: 0,
            NumOfReject: 0
        };

        $.ajax({
            method: 'POST',
            url: '/api/absent/setStudent',
            data: JSON.stringify(student),
            contentType: 'application/json',
            success: function (data) {
                if (data.isInsert) {
                    location.reload();
                    alertify.success(' تم  اضافه  ' + data.isInsert);
                } else {
                    alertify.error('يرجى المحاوله مره اخرى');
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
    $('#transfer').click(function () {
        var std = {
            "StudentId": parseInt(transBtn.parents('td').parent()[0].cells[3].innerHTML),
            "ClassFk": parseInt($('#inlineFormCustomSelectPref').val()),
        };
        $.ajax({
            method: 'PUT',
            url: '/api/student/transfer/' + transBtn.parents('td').parent()[0].cells[3].innerHTML,
            data: JSON.stringify(std),
            contentType: 'application/json',
            success: function (data) {
                if (data.isUpdate) {
                    alertify.success('تم نقل الطالب بنجاح الفصل' + data.isUpdate);
                    $('#closeTrans').click();
                    transBtn.parents('td').parent()[0].remove();
                } else {
                    alertify.error('هناك مشكله عاود تشغيل البرنامج');
                }
            },
            error: function (error) {
                console.log(error);
            }
        }) 
    }) 
    $('#changeName').click(function () {
        std = {
            "StudentId": parseInt(transBtn.parents('td').parent()[0].cells[3].innerHTML),
            "StudentName": $('#stdName').val()
        };
        $.ajax({
            method: 'PUT',
            url: '/api/student/edit/' + transBtn.parents('td').parent()[0].cells[3].innerHTML,
            data: JSON.stringify(std),
            contentType: 'application/json',
            success: function (data) {
                if (data.isUpdate) {
                    alertify.success('تم تعديل بنجاح');
                    transBtn.parents('td').parent()[0].cells[2].innerHTML = data.isUpdate;
                    $('#cancelModel').click();
                } else {
                    alertify.error('هناك مشكله عاود تشغيل البرنامج');
                }
            },
            error: function (error) {
                console.log(error);
            }
        })
    });

    $('#saveFirstButton').click(function () {
        var username = $("#new-user-name-editing").val();
        if (username.length < 5) {
            alertify.error("من فضلك ادخل اسم مستخد صحيح");
            return;
        }
        $.ajax({
            method: 'PUT',
            url: '/api/account/change/' + $('#old-user-name-editing').val(),
            data: "\" " + username + " \"",
            contentType: 'application/json',
            success: function (data) {
                if (data.isUpdate) {
                    alertify.success('تم تعديل بنجاح' + data.isUpdate);
                    $('#cancelModel').click();
                    window.setTimeout(function () { location.replace("/account/logout")}, 4000);
                } else {
                    alertify.error('هناك مشكله عاود تشغيل البرنامج');
                }
            },
            error: function (error) {
                console.log(error);
            }
        })
    });

    $('#saveSecondButton').click(function () {
        var oldPassword = $('#old-password-editing').val();
        var newPassword = $('#new-password-editing').val();
        var confirmPassword = $('#confirm-password-editing').val();

        if (newPassword.localeCompare(confirmPassword) < 0) {
            alertify.alert('تاكد من تطابق كلمة المرور');
            return;
        }
        $.ajax({
            method: 'PUT',
            url: '/api/account/changepassword',
            data: JSON.stringify({
                "newPassword": newPassword,
                "oldPassword": oldPassword
            }),
            contentType: 'application/json',
            success: function (data) {
                if (data.isUpdate) {
                    alertify.success('تم تعديل بنجاح');
                    $('#closingSecondButton').click();
                    window.setTimeout(function () {
                        location.replace("/account/logout");
                    }, 4000);
                } else {
                    alertify.error('هناك مشكله عاود تشغيل البرنامج');
                }
            },
            error: function (error) {
                console.log(error);
            }
        })
    });
    $("#myInputForSearching").on("keyup", function () {
    var value = $(this).val();
    $("#myTable tr").filter(function (index) {
    if(index != 0)
    $(this).toggle($(this).children("td").text().indexOf(value) > -1)
    });
    });

});
$(document).keypress(function (e) {
    if ($("#exampleModalForSetting").hasClass('in') && (e.keycode == 13 || e.which == 13)) {
        alert("Enter is pressed");
    }
});