$('#Load-Bars-Reviews').click(function (e)
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
$('#Load-Cocktails-Reviews').click(function (e) {
    e.preventDefault();
    const thisCocktailBtn = $(this);
    const cocktail = $(this).data('cocktail')
    console.log(thisCocktailBtn);
    console.log(cocktail);
    $.ajax({
        url: '/Reviews/LoadCocktailsReviews',
        data: { id: cocktail },
        type: "get",
        success: function (responseData) {
            console.log(responseData)
            if (responseData.length === 0) {
                thisCocktailBtn.replaceWith('<text> No reviews found on this bar!</text>')
                console.log(thisCocktailBtn)

            }
            else {
                console.log(responseData)
                thisCocktailBtn.replaceWith(responseData)
            }
        }

    })
})