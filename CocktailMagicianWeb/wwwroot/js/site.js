//// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.

//$('#createBarBtn').click(function (e) {
//    e.preventDefault();
//    var serialized = $('#createBarForm').serializeArray();
//    var url = '/Bar/CreateBar';
//    var fdata = new FormData();

//    var fileInput1 = $('#barImage')[0].files[0];
//    fdata.append("picture", fileInput1);
//    //serialized.NewPicture = fdata
//    console.log(serialized)
//    console.log(fdata)
//    //url = url + `?barmodel=${serialized}`

//    $.ajax({
//        url: url,
//        method: 'POST',
//        data: serialized,
//        dataType: 'json',
//        success: function (responseData) {

//            if (responseData.length === 0) {
                
//                console.log('asd')

//            }
//            else {
             
//            }
//        }

//    })
//});