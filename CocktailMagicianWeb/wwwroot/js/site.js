// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#createBarBtn').click(function (e) {
    e.preventDefault();
    var serialized = $('#createBarForm').serialize();
    var url = $(this).data('url');
    var picture = $('#files').serialize();

    console.log(picture);



    $.ajax({
        url: url,
        data: {
             bar: $('#createBarForm').serialize() , picture: $('#pictureField').val() 
        },
        type: "post",
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