$('#Load-Bars-Reviews').click(function (e)
{
    e.preventDefault();
    const thisBtn = $(this);
    const bar = $(this).data('bar')
    const commentBox = $('commentBox')
    console.log(thisBtn);
    console.log(bar);
    $.ajax({
        url: '/Reviews/LoadBarReviews',
        data: { id: bar },
        type: "get",
        success: function (responseData) {
            console.log(responseData.length)
            if (responseData.length === 108) {
                thisBtn.replaceWith('<text> No reviews found on this bar!</text>')
                console.log(thisBtn)

            }
            else {
                console.log(responseData);
                $('#commentBox').html(responseData);
                thisBtn.remove();
            }
        }

    })
})
$('#Load-Cocktails-Reviews').ready().click(function (e) {
    e.preventDefault();
    const thisCocktailBtn = $(this);
    const cocktail = $(this).data('cocktail')
    console.log(thisCocktailBtn);
    console.log(cocktail);
    const commentBox = $('#cocktailCommentBox')
    $.ajax({
        url: '/Reviews/LoadCoctailReviews',
        data: { id: cocktail },
        type: "get",
        success: function (response) {
            console.log(response.length)
            if (response.length === 108) {
                thisCocktailBtn.replaceWith('<text> No reviews found on this cocktail!</text>')
                console.log(thisCocktailBtn)

            }
            else {
                $('#cocktailCommentBox').html(response);
                thisCocktailBtn.remove();
            }
        }

    })
})