$('#Ingredients').on('click', '#addIngredient', function () {
    let ingredients = $('.drop').length + 1;
    const button = $(this)
    if (ingredients === 10) {
        button.replaceWith(` <div class="holder">
        Ingredient ${ingredients}:
        <select id="drop${ingredients}" class="drop" data-position="${ingredients}">
        <option value="alcohol">Alcohol</option>
        <option value="sweetener">Sweetener</option>
        <option value="juice">Juice</option>
        <option value="herb">Herb</option>
        </select> 
        </div>
        <br>
        A cocktail can contain maximum 10 ingredients`)
    }
    console.log(ingredients);
    button.replaceWith(
        `<div class="holder">
        Ingredient ${ingredients}:
        <select id="drop$${ingredients}" class ="drop" data-position="${ingredients}">
        <option value="alcohol">Alcohol</option>
        <option value="sweetener">Sweetener</option>
        <option value="juice">Juice</option>
        <option value="herb">Herb</option>
        </select> 
        </div>
        <br>
        <button id="addIngredient" class="btn" style ="background-color:#ff0000; color:white"> Add Ingredient</button>`)
    const thisSelect = $('.drop').last();
    console.log(thisSelect)

    $.ajax({
        url: '/Cocktails/GetIngedientsByType',
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
                thisSelect.parent().append(` <select name="Ingredients"> ${options}  </select>`).append('<input name="Quantities" placeholder="mililiters..." class="form-control col-md-3"></input>')
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
            url: '/Cocktails/GetIngedientsByType',
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
                    thisSelect.parent().append(` <select name="Ingredients"> ${options}  </select>`)
                        .append('<input name="Quantities" placeholder="mililiters/spoon..." class="form-control col-md-3"></input>')

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
            url: '/Cocktails/GetIngedientsByType',
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
                    thisSelect.parent().append(`<select name="Ingredients"> ${options}  </select>`).append('<input name="Quantities" placeholder="mililiters..." class="form-control col-md-3"></input>')
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
            url: '/Cocktails/GetIngedientsByType',
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
                    thisSelect.parent().append(` <select name="Ingredients"> ${options}  </select>`).append('<input name="Quantities" placeholder="stalk..." class="form-control col-md-3"></input>')
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
            url: '/Cocktails/GetIngedientsByType',
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
                    thisSelect.parent().append(`<select name="Ingredients"> ${options}  </select>`).append('<input name="Quantities" placeholder="mililiters..." class="form-control col-md-3"></input>')
                }
            }
        })
    }
});

$('#Ingredients').on('click', '#addCocktailToBars', function (e) {
    e.preventDefault()
    const btn = $('#addCocktailToBars')
    $.ajax({
        url: '/Cocktails/GetBars',
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
                    options[i] = `<option value="${responseData[i].name}"> ${responseData[i].name} </option>`
                }
                console.log(` <select> ${options}  </select>`)
                btn.replaceWith(`<select multiple name="Bars"> ${options}  </select>`)
            }
        }
    })
})