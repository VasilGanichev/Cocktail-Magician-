$('#load-more-Bars').ready().click(function (e) {
    let loadedCocktails = $('.matataHakuna').length;
    var url = '/Cocktails/LoadMoreBars';

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
                $('#InputBars').append($(`
                              <p class="matataHakuna">${cocktail}</p>
                             `));
            }
            if (data.length == 0) {
                $('#load-more-Bars').remove();
            }
        }
    })
})