$('.updateCocktail').on('click', '.cancel', function (e) {
    e.preventDefault()
    const cocktail = $('#cocktail').data('cocktail')
    const ingredient = $(this).siblings('#list').children('.ingredient').data('ingredient')
    console.log(cocktail)
    console.log(ingredient)
    $(this).parent('div').empty()

    $.ajax({
        url: '/Ingredients/RemoveIngredient',
        data: { cocktail: cocktail, ingredient: ingredient },
        method: 'GET',
        success: function () {
            $(this).parent('div').empty()
        }
    })
})

jQuery.fn.multiselect = function () {
    $(this).each(function () {
        var checkboxes = $(this).find("input:checkbox");
        checkboxes.each(function () {
            var checkbox = $(this);
            // Highlight pre-selected checkboxes
            if (checkbox.prop("checked"))
                checkbox.parent().addClass("multiselect-on");

            // Highlight checkboxes that the user selects
            checkbox.click(function () {
                if (checkbox.prop("checked"))
                    checkbox.parent().addClass("multiselect-on");
                else
                    checkbox.parent().removeClass("multiselect-on");
            });
        });
    });
};


$('#save').click(function (e) {
    e.preventDefault()
    const cocktail = $('#cocktail').data('cocktail')
    const bars = $("input:checkbox")
    let filteredBars = []
    console.log(bars)
    bars.each(function () {
        var bar = $(this)
        console.log(bar)
        if (bar.prop("checked")) {
            filteredBars.push(bar.data('bar'))
        }
        console.log(bar.text())
    })
    console.log(filteredBars)
    jQuery.ajaxSettings.traditional = true
    $.ajax({
        url: '/Cocktails/UpdateBarCocktailPairs',
        data: { cocktailName: cocktail, currentlyCheckedBars: filteredBars },
        method: 'GET'
    })
})