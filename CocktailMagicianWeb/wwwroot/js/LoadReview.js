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
                let reviews = []
                for (let i = 0; i < responseData.length; i++) {
                    reviews[i] = `<td>${responseData[i].userName} </td>
                    <td>${responseData[i].rating}</td>
                    <td>${responseData[i].comment}</td>`

                }
                thisBtn.replaceWith(`<table class="table table-hover">
                 <th>User:</th>
                 <th>Rating:</th>
                 <th>Comment:</th>
                 ${reviews}
                 </table>`)
            }
        }

    })
})