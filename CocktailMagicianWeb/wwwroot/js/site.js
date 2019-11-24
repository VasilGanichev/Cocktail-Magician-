//// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.

//$('#createBarBtn').click(function (e) {
//    e.preventDefault();
//    var serialized = $('#createBarForm').serialize();
//    var url = '/Bar/CreateBar';
//    var fdata = new FormData();
//    console.log(serialized)
//  //  var fileInput1 = $('#barImage')[0].files[0];
//    var file_data = $('#barImage').prop('files')[0];
//    fdata.append("picture", file_data);
//    url = url + `?${serialized}`

//    $.ajax({
//        url: url,
//        method: 'POST',
//        data: fdata,
//        contentType: false,
//        processData: false,
//        success: function (responseData) {

//            if (responseData.length === 0) {
                
//                console.log('asd')

//            }
//            else {
             
//            }
//        }

//    })
//});