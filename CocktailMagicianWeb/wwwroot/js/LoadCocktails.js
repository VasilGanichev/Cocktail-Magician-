$('#load-more').ready().click( function (e) {
    let loadedCocktails = $('.hakunaMatata').length;
    var url = '/Bar/LoadMoreCocktails';

    const barId = $(this).data('id')
    url = url + `?id=${barId}&Loaded=${loadedCocktails}`
    console.log(loadedCocktails)
    console.log(barId);
    e.preventDefault()
    $.ajax({
        url: url,
        success: function (responseData) {
            let data = responseData;
            console.log(responseData)
            console.log(responseData.length)
            for (let i = 0; i < responseData.length; i++) {
                const cocktail = responseData[i];
                $('#InputCocktails').append($(`
                              <p class="hakunaMatata">${cocktail}</p>
                             `));
            }
            if (data.length == 0) {
                $('#load-more').remove();
            }
        }
    })
})