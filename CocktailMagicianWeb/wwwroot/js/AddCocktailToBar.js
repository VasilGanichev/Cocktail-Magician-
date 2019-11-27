$('#addCocktail').click(function (e) {
    e.preventDefault();
    const btnadd = $('#addCocktail')
    $.ajax({
        url: '/Cocktails/GetCocktails',
        cache: true,
        method: 'GET',
        success: function (responseData) {
            console.log(responseData)
            if (responseData.length === 0) {
                btnadd.replaceWith('<text>There are no cocktails yet!</text>')
            } else {
                let cocktailOptions = []
                for (var i = 0; i < responseData.length; i++) {
                    cocktailOptions[i] = `<option value="${responseData[i].name}">${responseData[i].name}</option>`
                }
                console.log(` <select> ${cocktailOptions}  </select>`)
                btnadd.replaceWith(`<select class="btn" multiple name="Cocktails"> ${cocktailOptions}  </select>`)
            }
        }
    })
})
