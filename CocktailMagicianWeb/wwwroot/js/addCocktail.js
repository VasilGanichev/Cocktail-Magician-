

$('#Ingredients').on('click', '#addIngredient', function () {
    let ingredients = $('.drop').length + 1;
    console.log(ingredients)
    const button = $(this)
    if (ingredients === 10) {
        button.replaceWith(` <div class="holder">
        <text class="count">Ingredient ${ingredients}: </text>
        <div>
        <select id="drop${ingredients}" class="drop btn dropdown-toggle" data-position="${ingredients}" style="background:white; border-color:black; padding-top:2px; padding-bottom:2px;">
        <option value="alcohol">Alcohol</option>
        <option value="sweetener">Sweetener</option>
        <option value="juice">Juice</option>
        <option value="herb">Herb</option>
        </select> 
        </div>
        </div>
        A cocktail can contain maximum 10 ingredients`)
    }
    console.log(ingredients);
    button.replaceWith(
        `<div class="holder">
        <text class="count">Ingredient ${ingredients}: </text>
        <div>
        <select id="drop$${ingredients}" class ="drop btn dropdown-toggle" data-position="${ingredients}" style="background:white; border-color:black; padding-top:2px; padding-bottom:2px;">
        <option value="alcohol">Alcohol</option>
        <option value="sweetener">Sweetener</option>
        <option value="juice">Juice</option>
        <option value="herb">Herb</option>
        </select> 
        </div>
        </div>
        <button id="addIngredient" class="btn" style ="background-color:#ff0000; color:white"> <i class="fa fa-plus-circle"></i></button>`)
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
                    .append(` <select class ="btn dropdown-toggle" name="Ingredients" style="background:white; border-color:black; padding-top:2px; padding-bottom:2px;"> ${options}  </select>`)
                    .append('<input name="Quantities" id="element1" value="0" placeholder="mililiters..." class="btn col-md-2" style=" background:white; border-color:gray; padding-top:2px; padding-bottom:2px;"></input>')
                    .append('<button type="button" class= "cancel btn"> <i id="element2" class="fa fa-close"></i></button>')
            }
        }
    })
})

$('#Ingredients').on('change', 'select', function (e) {
    if ($(this).val() === 'sweetener') {
        e.preventDefault()
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
                    thisSelect.parent()
                        .append(`<select class =" btn dropdown-toggle" name="Ingredients" style="background:white; border-color:black; padding-top:2px; padding-bottom:2px;"> ${options}  </select>`)
                        .append('<input name="Quantities" value="0"  id="element1" placeholder="mililiters/spoon..." class="btn col-md-2" style=" background:white; border-color:gray; padding-top:2px; padding-bottom:2px;"></input>')
                        .append('<button type="button" class= "cancel btn"> <i id="element2" class="fa fa-close"></i></button>')
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
                        .append(`<select class =" btn dropdown-toggle" name="Ingredients" style="background:white; border-color:black; padding-top:2px; padding-bottom:2px;"> ${options}  </select>`)
                        .append('<input name="Quantities" value="0" id="element1" placeholder="mililiters..." class="btn col-md-2" style=" background:white; border-color:gray; padding-top:2px; padding-bottom:2px;"></input>')
                        .append('<button type="button" class= "cancel btn"> <i id="element2" class="fa fa-close"></i></button>')
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
                        .append(` <select class =" btn dropdown-toggle" name="Ingredients" style="background:white; border-color:black; padding-top:2px; padding-bottom:2px;"> ${options}  </select>`)
                        .append('<input name="Quantities" value="0" id="element1" placeholder="stalk..." class="btn col-md-2" style=" background:white; border-color:gray; padding-top:2px; padding-bottom:2px;"></input>')
                        .append('<button type="button" class= "cancel btn"> <i id="element2" class="fa fa-close"></i></button>')
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
                        .append(`<select class =" btn dropdown-toggle" name="Ingredients" style="background:white; border-color:black; padding-top:2px; padding-bottom:2px;"> ${options}  </select>`)
                        .append('<input name="Quantities" value="0" id="element1" placeholder="mililiters..." class="btn col-md-2" style=" background:white; border-color:gray; padding-top:2px; padding-bottom:2px;"></input>')
                        .append('<button type="button" class= "cancel btn"> <i id="element2" class="fa fa-close"></i></button>')
                }
            }
        })
    }
});

$('.addCocktail').on('click', '.cancel', function () {
    $(this).parent().parent('div').empty()
    var ingredientCount = 1
    $('.count').each(function () {
        $(this).text(`Ingredient ${ingredientCount}:`)
        ingredientCount++
        console.log($(this).text())
    })
})

$('#Submit').mouseenter(function () {
    const cocktail = $('#cocktail').val()
    console.log(cocktail)
    $.ajax({
        url: '/Cocktails/NameExists',
        data: { name: cocktail },
        success: function (responseData) {
            if (responseData === true) {
                $('#Submit').prop('disabled', true);
                toastr.options.timeOut = 2000;
                toastr.options.extendedTimeOut = 0;
                //toastr.options ={
                //    "preventDuplicates": true
                //}
                toastr.options.positionClass = "toast-top-center";
                toastr.warning('You cannot create the cocktail as this name is alraedy taken.');
                //function restore() {
                //    toastr.remove();
                //    $('#Submit').prop('disabled', false);
                //}
                //setTimeout(restore, 2000)
            }
            else {
                toastr.options.timeOut = 0;
                toastr.options.extendedTimeOut = 0;
                toastr.options.positionClass = "toast-top-center";
                toastr.options.prevenOpenDuplicates = true;
                //function restore() {
                //    toastr.remove();
                //    $('#Submit').prop('disabled', false);
                //}
                //setTimeout(restore, 2000)
                toastr.success('You can create the cocktail.');
            }
        }
    })
})

$('#Submit').on('keyup keypress', function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode === 13) {
        e.preventDefault();
        return false;
    }
});

$('#cocktail').change(function () {
    console.log(1)
    toastr.remove();
    $('#Submit').prop('disabled', false);
})