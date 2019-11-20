// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#createBarBtn').click(function (e) {
    e.preventDefault();
    var serialized = $('#createBarForm').serialize();
    var url = $(this).data('url');
    console.log(serialized)

    var fdata = new FormData();

    var fileInput1 = $('#barImage')[0].files[0];
    fdata.append("picture", fileInput1);
    var data = JSON.stringify({
        'barmodel': serialized,
        'picturemodel': fdata
    });

    $.ajax({
        url: url,
        data: data,
        dataType: 'json',
        type: 'POST',
        processData: false,
        contentType: false,
        success: function (responseData) {
            console.log(responseData)
            if (responseData.length === 0) {
                thisBtn.replaceWith('<text> No reviews found on this bar!</text>')
                console.log(thisBtn)

            }
            else {
                console.log(responseData)
                thisBtn.replaceWith(responseData)
            }
        }

    })
});