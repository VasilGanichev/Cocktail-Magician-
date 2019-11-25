$('.updateCocktail').on('click', '.cancel', function (e) {
    e.preventDefault()
    const cocktail = $('#cocktail').data('cocktail')
    const ingredient = $(this).siblings('#list').children('.ingredient').data('ingredient')
    console.log(cocktail)
    console.log(ingredient)
    $(this).parent().parent('div').empty()
    var ingredientCount = 1
    $('.count').each(function () {
        $(this).text(`Ingredient ${ingredientCount}:`)
        ingredientCount++
       console.log($(this).text())
    })

    $.ajax({
        url: '/Ingredients/RemoveIngredient',
        data: { cocktail: cocktail, ingredient: ingredient },
        method: 'GET',
        success: function () {
            $(this).closest('.count').empty()
        }
    })
})


$('#addCocktailToBars').on('click', function (e) {
    e.preventDefault()
    const btn = $('#addCocktailToBars')
    $.ajax({
        url: '/Bar/GetBars',
        cache: false,
        type: 'GET',
        success: function (responseData) {
            console.log(responseData)
            if (responseData.length === 0) {
                btn.replaceWith(`<text> There are no existing Bars yet.</text>`)
            }
            else {
                let options = []
                for (let i = 0; i < responseData.length; i++) {
                    options[i] = `<label name="Bars"> <input class="checkbox" type="checkbox" data-bar="${responseData[i].name}" value="${responseData[i].name}"> ${responseData[i].name} </input></label>`
                }
                btn.replaceWith(`<div class="multiselect"> ${options.join('')} </div>
                                <button id="save" type="button" class="btn" style="background-color:#ff0000; color:white"> Save </button>`)
            }
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
$('#save').on('click', function (e) {
    e.preventDefault()
    const cocktail = $('#cocktail').data('cocktail')
    console.log(cocktail)
    const bars = $("input:checkbox")
    let filteredBars = []
    console.log(bars)
    bars.each(function () {
        var bar = $(this)
        console.log(bar)
        if (bar.prop("checked")) {
            filteredBars.push(bar.data('bar'))
        }
    })
    console.log(filteredBars)
    jQuery.ajaxSettings.traditional = true
    $.ajax({
        url: '/Cocktails/UpdateBarCocktailPairs',
        data: { cocktailName: cocktail, currentlyCheckedBars: filteredBars },
        method: 'GET',
        success: function () {
            toastr.remove();
            toastr.options.timeOut = 2000;
            toastr.options.positionClass = "toast-top-center";
            toastr.success('Changes saved.');
        }
    })
})

$('#save').mouseenter(function () {
    toastr.options.timeOut = 0;
    toastr.options.extendedTimeOut = 0;
    toastr.options.positionClass = "toast-top-center";
    toastr.info('By clicking "Save", you will save the selected changes.');
});
$('#save').mouseleave(function () {
    toastr.remove();
});