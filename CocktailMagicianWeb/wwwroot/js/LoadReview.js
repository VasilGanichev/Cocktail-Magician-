$('#Load-Reviews').click(function (e)
{
    e.preventDefault();
    const thisBtn = $(this);
    const bar = $(this).data('bar')
    console.log(thisBtn);
    console.log(bar);
    $.ajax({
        url: '/Reviews/LoadBarReviews',
        data: { id: bar },
        type: "get",
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
})