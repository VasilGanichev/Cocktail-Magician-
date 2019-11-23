$('#Ingredients').on('click', '#addIngredient', function () {
    let ingredients = $('.drop').length + 1;
    console.log(ingredients)
    const button = $(this)
    if (ingredients === 10) {
        button.replaceWith(` <div class="holder">
        Ingredient ${ingredients}:
        <select id="drop${ingredients}" class="drop btn btn-danger dropdown-toggle" data-position="${ingredients}">
        <option value="alcohol">Alcohol</option>
        <option value="sweetener">Sweetener</option>
        <option value="juice">Juice</option>
        <option value="herb">Herb</option>
        </select> 
        </div>
        A cocktail can contain maximum 10 ingredients`)
    }
    console.log(ingredients);
    button.replaceWith(
        `<div class="holder">
        Ingredient ${ingredients}:
        <select id="drop$${ingredients}" class ="drop btn btn-danger dropdown-toggle" data-position="${ingredients}">
        <option value="alcohol">Alcohol</option>
        <option value="sweetener">Sweetener</option>
        <option value="juice">Juice</option>
        <option value="herb">Herb</option>
        </select> 
        </div>
        <button id="addIngredient" class="btn" style ="background-color:#ff0000; color:white"> Add Ingredient</button>`)
    const thisSelect = $('.drop').last();
    console.log(thisSelect)

    $.ajax({
        url: '/Ingredients/GetIngedientsByType',
        data: { type: 'alcohol' },
        cache: true,
        type: 'GET',
        success: function (responseData) {
            console.log(responseData)
            if (responseData.length === 0) {
                thisSelect.siblings().remove()
                thisSelect.parent().append(`<text> There are no existing alchohols yet.</text>`)
                console.log(thisSelect)
            }
            else {
                thisSelect.siblings().remove()
                let options = []
                for (let i = 0; i < responseData.length; i++) {
                    options[i] = `<option value="${responseData[i].name}"> ${responseData[i].name} </option>`
                }
                console.log(` <select> ${options}  </select>`)
                console.log(thisSelect)
                thisSelect.parent()
                    .append(` <select class ="btn btn-danger dropdown-toggle" name="Ingredients"> ${options}  </select>`)
                    .append('<input name="Quantities" id="element1" data-val="true"  data-val-required="A date is required." value="0" placeholder="mililiters..." class="form-control col-md-2 required"></input>')
                    .append('<span asp-validation-for="@Model.Quantities" class="text-danger"></span>')
                    .append('<button type="button" class= "cancel btn"> <i id="element2" class="fa fa-close"></i></button>')

                //Remove current form validation information
                $("form")
                    .removeData("validator")
                    .removeData("unobtrusiveValidation");

                //Parse the form again
                $.validator
                    .unobtrusive
                    .parse("form");

                $(function () {
                    $("form").validate();
                });
            }
        }
    })
})

$('#Ingredients').on('change', 'select', function (e) {
    if ($(this).val() === 'sweetener') {
        const position = $(this).data('position');
        const thisSelect = $(this)
        const type = 'sweetener'
        console.log(position)
        e.preventDefault()
        $.ajax({
            url: '/Ingredients/GetIngedientsByType',
            data: { type: type },
            cache: true,
            type: 'GET',
            success: function (responseData) {
                console.log(responseData)
                if (responseData.length === 0) {
                    thisSelect.siblings().remove()
                    thisSelect.parent().append(`<text> There are no existing ${type}s yet.</text>`)
                }
                else {
                    thisSelect.next().remove()
                    thisSelect.siblings().remove()
                    let options = []
                    for (let i = 0; i < responseData.length; i++) {
                        options[i] = `<option value="${responseData[i].name}"> ${responseData[i].name} </option>`
                    }
                    console.log(` <select> ${options}  </select>`)
                    console.log(thisSelect)
                    thisSelect.parent().append(`<select class =" btn btn-danger dropdown-toggle" name="Ingredients"> ${options}  </select>`)
                        .append('<input name="Quantities"  data-val="true"  data-val-required="A date is required." id="element1" placeholder="mililiters/spoon..." class="form-control col-md-2 quantities"></input>')
                        .append('<span asp-validation-for="@Model.Quantities" class="text-danger"></span>')
                        .append('<button type="button" class= "cancel btn"> <i id="element2" class="fa fa-close"></i></button>')
                }
                $('.quantities').each(function () {
                    $(this).rules("add",
                        {
                            required: true
                        })
                });
                if ($('#Ingredients').validate().form()) {
                    console.log("validates");
                } else {
                    console.log("does not validate");
                }
            }
        })
    }

    if ($(this).val() === 'alcohol') {
        const position = $(this).data('position');
        console.log(position)
        const thisSelect = $(this)
        const type = $(this).val()
        e.preventDefault()
        $.ajax({
            url: '/Ingredients/GetIngedientsByType',
            data: { type: type },
            cache: true,
            type: 'GET',
            success: function (responseData) {
                console.log(responseData)
                if (responseData.length === 0) {
                    thisSelect.siblings().remove()
                    thisSelect.parent().append(`<text> There are no existing ${type}s yet.</text>`)
                }
                else {
                    thisSelect.next().remove()
                    thisSelect.siblings().remove()
                    let options = []
                    for (let i = 0; i < responseData.length; i++) {
                        options[i] = `<option value="${responseData[i].name}"> ${responseData[i].name} </option>`
                    }
                    console.log(` <select> ${options}  </select>`)
                    console.log(thisSelect)
                    thisSelect.parent()
                        .append(`<select class =" btn btn-danger dropdown-toggle" name="Ingredients"> ${options}  </select>`)
                        .append('<input name="Quantities"  data-val="true"  data-val-required="A date is required." id="element1" placeholder="mililiters..." class="form-control col-md-2 quantities"></input>')
                        .append('<span asp-validation-for="@Model.Quantities" class="text-danger"></span>')
                        .append('<button type="button" class= "cancel btn"> <i id="element2" class="fa fa-close"></i></button>')
                }
                $('.quantities').each(function () {
                    $(this).rules("add",
                        {
                            required: true
                        })
                });
                if ($('#Ingredients').validate().form()) {
                    console.log("validates");
                } else {
                    console.log("does not validate");
                }
            }
        })
    }

    if ($(this).val() === 'herb') {
        const position = $(this).data('position');
        console.log(position)
        const thisSelect = $(this)
        const type = $(this).val()
        e.preventDefault()
        $.ajax({
            url: '/Ingredients/GetIngedientsByType',
            data: { type: type },
            cache: true,
            type: 'GET',
            success: function (responseData) {
                console.log(responseData)
                if (responseData.length === 0) {
                    thisSelect.siblings().remove()
                    thisSelect.parent().append(`<text> There are no existing ${type}s yet.</text>`)
                }
                else {
                    thisSelect.next().remove()
                    thisSelect.siblings().remove()
                    let options = []
                    for (let i = 0; i < responseData.length; i++) {
                        options[i] = `<option value="${responseData[i].name}"> ${responseData[i].name} </option>`
                    }
                    console.log(` <select> ${options}  </select>`)
                    console.log(thisSelect)
                    thisSelect.parent()
                        .append(` <select class =" btn btn-danger dropdown-toggle" name="Ingredients"> ${options}  </select>`)
                        .append('<input name="Quantities"  data-val="true"  data-val-required="A date is required." id="element1" placeholder="stalk..." class="form-control col-md-2 quantities"></input>')
                        .append('<span asp-validation-for="@Model.Quantities" class="text-danger"></span>')
                        .append('<button type="button" class= "cancel btn"> <i id="element2" class="fa fa-close"></i></button>')
                }
                $('.quantities').each(function () {
                    $(this).rules("add",
                        {
                            required: true
                        })
                });
                if ($('#Ingredients').validate().form()) {
                    console.log("validates");
                } else {
                    console.log("does not validate");
                }
            }
        })
    }

    if ($(this).val() === 'juice') {
        const position = $(this).data('position');
        console.log(position)
        const thisSelect = $(this)
        const type = $(this).val()
        e.preventDefault()
        $.ajax({
            url: '/Ingredients/GetIngedientsByType',
            data: { type: type },
            cache: true,
            type: 'GET',
            success: function (responseData) {
                console.log(responseData)
                if (responseData.length === 0) {
                    thisSelect.next().remove()
                    thisSelect.siblings().remove()
                    thisSelect.parent().append(`<text> There are no existing ${type}s yet.</text>`)
                }
                else {
                    thisSelect.next().remove()
                    thisSelect.siblings().remove()
                    let options = []
                    for (let i = 0; i < responseData.length; i++) {
                        options[i] = `<option value="${responseData[i].name}"> ${responseData[i].name} </option>`
                    }
                    console.log(` <select> ${options}  </select>`)
                    console.log(thisSelect)
                    thisSelect.parent()
                        .append(`<select class =" btn btn-danger dropdown-toggle" name="Ingredients"> ${options}  </select>`)
                        .append('<input name="Quantities"  data-val="true"  data-val-required="A date is required." id="element1" placeholder="mililiters..." class="form-control col-md-2 quantities"></input>')
                        .append('<span asp-validation-for="@Model.Quantities" class="text-danger"></span>')
                        .append('<button type="button" class= "cancel btn"> <i id="element2" class="fa fa-close"></i></button>')
                }
                $('.quantities').each(function () {
                    $(this).rules("add",
                        {
                            required: true
                        })
                });
                if ($('#Ingredients').validate().form()) {
                    console.log("validates");
                } else {
                    console.log("does not validate");
                }
            }
        })
    }
});

$('#Ingredients').on('click', '#addCocktailToBars', function (e) {
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
                    options[i] = `<label> <input class="checkbox" type="checkbox" data-bar="${responseData[i].name}" value="${responseData[i].name}"> ${responseData[i].name} </input></label>`
                }
                console.log(` <select> ${options} </select>`)
                btn.replaceWith(`<div class="multiselect"> ${options.join('')} </div>
                                <button id="save" type="button" class="btn" style="background-color:#ff0000; color:white"> Save </button>`)
            }
        }
    })
})

$('.addCocktail').on('click', '.cancel', function () {
    $(this).parent('div').empty()
})