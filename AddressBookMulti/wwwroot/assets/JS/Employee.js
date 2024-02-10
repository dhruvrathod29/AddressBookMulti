$(document).ready(function () {
    // Initialize DataTable
    filldata();
});
function filldata() {
    $.ajax({
        type: 'GET',
        url: "/EMP_Employee/EMP_Employee/FillData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            debugger;
            var html = '';
            for (var i = 0; i < result.data.length; i++) {
                html += '<tr>';
                html += '<td>' + result.data[i]['employeeName'] + '</td>';
                html += '<td>' + result.data[i]['address'] + '</td>';
                html += '<td>' + result.data[i]['dateOfBirth'] + '</td>';
                html += '<td class="text-center"><a class="btn btn-default btn-sm" data-toggle="modal" href="#"><i class="fa fa-edit"></i></a> <a class="btn btn-default btn-sm" data-toggle="modal" onclick="btnDelete(' + result.data[i]["employeeID"] + ')"> <i class="fa-solid fa-trash"></i></a> </td>'
                //html += '<td><a href="#" onclick="return getbyID(' + result.data[i]['employeeID'] + ')">Edit</a> | <a href="#" onclick="Delele(' + result.data[i]['employeeID'] + ')">Delete</a></td>';
                html += '</tr>';
            }
            $('#body').html(html);
        },
        error: function (error) {
            console.log('Error' + error);
        }
    });
}

function OnSuccess(response) {
    $('#dataTableData').DataTable({
        data: response,
        columns: [
            {
                data: 'EmployeeID',
                render: function (data, type, row, data) {
                    return row.employeeid
                }
            }
        ]
    });
}

$('#btnAddEmployee').click(function () {
    $('#EmployeeModel').modal('show');
})


function clearControl() {
    debugger;
    $('#Name').val('');
   $('#Address').val('');
    
}


function AddEmployee() {
    debugger;
    var EmployeeName = $('#Name').val();
    var Address = $('#Address').val();

    var formdata = {
        EmployeeName: EmployeeName,
        Address: Address
    }

    $.ajax({
        type: "POST",
        url: "/EMP_Employee/EMP_Employee/Add",
        data: formdata,
        //data: {
        //    "EmployeeName": EmployeeName,
        //    "Address": Address
        //},
        success: function (respone) {
            debugger;
            if (respone != null) {
                $('#EmployeeModel').modal('hide');
                filldata();
                debugger;
                if (respone.success == true) {
                    toastr.success(respone.message, '', { timeOut: 3000 });
                    clearControl();
                }
                else {
                    toastr.error(respone.message, '', { timeOut: 3000 });
                }
            }

        },
        error: function (req, status, error) {
            $('#EmployeeModel').modal('hide');
            toastr.error('record not insert successful! ', '', { timeOut: 3000 });
        }
    });
}

//function btnDelete(employeeID) {
//    $.ajax({
//        type: "POST",
//        url: "/EMP_Employee/EMP_Employee/Delete?employeeID=" + employeeID,
//        success: function (respone) {
//            filldata();
//            if (respone != null) {
//                toastr.success(respone.message, '', { timeOut: 3000 });
//                clearControl();
//            }
//            else {
//                toastr.error(respone.message, '', { timeOut: 3000 });
//            }
//        },
//        error: function () {
//            $('#EmployeeModel').modal('hide');
//            toastr.error('record not insert successful! ', '', { timeOut: 3000 });
//        }
//    });
//}



function btnDelete(employeeID) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't to Delete Record!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/EMP_Employee/EMP_Employee/Delete?employeeID=" + employeeID,
                success: function (response) {
                    filldata();
                    if (response != null) {
                        Swal.fire({
                            title: "Deleted!",
                            text: "Your file has been deleted.",
                            icon: "success"
                        });
                        clearControl();
                    } else {
                        Swal.fire({
                            title: "Error!",
                            text: "Failed to delete record.",
                            icon: "error"
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        title: "Error!",
                        text: "Failed to delete record.",
                        icon: "error"
                    });
                }
            });
        }
    });

}