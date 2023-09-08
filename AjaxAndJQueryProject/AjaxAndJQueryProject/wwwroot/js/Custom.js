﻿$(document).ready(function () {
   // AddEmployee();
    ShowData();
   // alert("ok");
});

function ShowData() {
    $.ajax({
        url: '/Ajax/EmployeeList', // 'U' in 'url' should be lowercase
        type: 'Get', // 'GET' should be uppercase
        dataType: 'json', // 'dataType' should be lowercase
        contentType: 'application/json;charset=utf-8;',
        success: function (result, status, xhr) {
            var object = '';
            $.each(result, function (index, item) { 
                object += '<tr>';
                object += '<td>' + item.id + '</td>' 
                object += '<td>' + item.name + '</td>'
                object += '<td>' + item.state + '</td>'
                object += '<td>' + item.city + '</td>'
                object += '<td>' + item.salary + '</td>' 
                object += '<td><a href="#" class="btn btn-primary" onclick="Edit('+item.id+')" >Edit</a> || <a href="#" class="btn btn-danger" onclick="Delete(' + item.id + ')">Delete</a></td>';
                object += '</tr>';
            });
            $('#table_data').html(object);
        },
        error: function () {
            alert("Data can't be retrieved"); 
        }
    });
}

$('#btnAddEmployee').click(function () {
    $('#EmployeeMadal').modal('show');
});
function AddEmployee() {
    var objData = {
        Name: $('#Name').val(),
        State: $('#State').val(),
        City: $('#City').val(),
        Salary: $('#Salary').val()
    }
    $.ajax({
        url: '/Ajax/AddEmployee',
        type: 'Post',
        data: objData,
        contentType: 'application/x-www-form-urlencoded;charset=utf-8',
        dataType: 'json',
        success: function () {
            ShowData();
            HideModalPopUp();
            ClearTextBox();
            alert('Data Saved');
        },
        error: function () {
            alert("Data couldn't be Saved")
        }

    });
    function HideModalPopUp() {
        $('#EmployeeMadal').modal('hide');
    }
    function ClearTextBox() {
        $('#Name').val('');
        $('#State').val('');
        $('#City').val('');
        $('#Salary').val('');
    }
}
function Delete(id) {
    if (confirm('Are You Sure, You Want to delete this Record?')) {

        $.ajax({
            url: '/Ajax/Delete?id=' + id,
            success: function () {
                alert('Record Deleted Successfully');
                ShowData();
            },
            error: function () {
                alert("Record couldn't be Deleted")
            }
        })
    }
}
function Edit(id) {
    if (confirm('Are You Sure, You Want to Edit this Record?')) {

        $.ajax({
            url: '/Ajax/Edit?id=' + id,
            success: function () {
                alert('Record Edited Successfully');
                ShowData();
            },
            error: function () {
                alert("Record couldn't be Edited")
            }
        })
    }
}



