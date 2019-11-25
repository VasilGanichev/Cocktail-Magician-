$('#cocktailTable').on('click', '#unhide', function (event) {
    event.preventDefault();
    const cocktail = $(this).data('cocktail');
    console.log(cocktail)
    console.log(cocktail)
    const thisBtn = $(this);
    console.log(thisBtn)
    $.ajax({
        url: '/Cocktails/UnhideCocktail',
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({ Id: cocktail }),
        method: 'POST',
        success: function () {
            console.log(thisBtn)
            $(thisBtn).replaceWith($('<a id="hide" data-cocktail=" ' + cocktail + '" href="" >Hide</a>'))
        }
    })
});

$('#cocktailTable').on('click', '#hide', function (event) {
    event.preventDefault();
    const cocktail = $(this).data('cocktail');
    console.log(cocktail)
    const thisBtn = $(this);
    console.log(thisBtn)
    $.ajax({
        url: '/Cocktails/HideCocktail',
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({ Id: cocktail }),
        method: 'POST',
        success: function () {
            console.log(thisBtn)
            $(thisBtn).replaceWith($('<a id="unhide" data-cocktail=" ' + cocktail + '" href="" >Unhide</a>'))
        }
    })
});

