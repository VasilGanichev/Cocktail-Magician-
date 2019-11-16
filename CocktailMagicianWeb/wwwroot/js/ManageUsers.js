$('#the-clicking-table').on('click', '.promote', function (event) {
    event.preventDefault();
    const url = $(this).data('url');
    const userId = $(this).data('userid');
    const thisBtn = $(this);
    $.ajax({
        url: url,
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({ userId: userId }),
        method: 'POST',
        success: function () {
            toastr.success('User promoted.')
            console.log(thisBtn)
            const a = (thisBtn.parent().find(userId))
            console.log(a.find('tr'))
            $(thisBtn).replaceWith($('<button type="submit" class="btn btn-info demote" data-url="/CocktailMagician/demote" data-userid="' + userId + '">Demote</button>'))
            $('#' + userId + '').replaceWith('<td id="' + userId + '">CocktailMagician</td>')
        }
    })

});

$('#the-clicking-table').on('click', '.demote', function (event) {
    event.preventDefault();
    const url = $(this).data('url');
    const userId = $(this).data('userid');
    const thisBtn = this;

    $.ajax({
        url: url,
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({ userId: userId }),
        method: 'POST',
        success: function () {
            toastr.success('User demoted.')
            console.log(thisBtn)
            $(thisBtn).replaceWith($('<button type="submit" class="btn btn-info promote" data-url="/CocktailMagician/promote" data-userid="' + userId + '">Promote</button>'))
            $('#' + userId + '').replaceWith('<td id="' + userId + '">BarCrawler</td>')
        }
    })
})

$('#the-clicking-table').on('click', '#ban', function (event) {
    event.preventDefault();
    const url = $(this).data('url');
    const data = $(this).data('userid');
    const status = $(this).data('status');
    const thisBtn = this;

    $.ajax({
        url: url,
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({ userId: data }),
        method: 'POST',
        success: function () {
            console.log(status)
            toastr.success('User banned')
            $(thisBtn).replaceWith($('<button id="remove-ban" type="submit" class="btn btn-info" data-url="/CocktailMagician/removeban" data-userid="' + data + '" data-status="' + status + '">Remove Ban</button>'))
            $('#' + status + '').replaceWith('<td id="' + status + '">Banned</td>')
        }
    })
})

$('#the-clicking-table').on('click', '#remove-ban', function (event) {
    event.preventDefault();
    const url = $(this).data('url');
    const data = $(this).data('userid');
    const status = $(this).data('status');
    const thisBtn = this;

    $.ajax({
        url: url,
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({ userId: data }),
        method: 'POST',
        success: function () {
            toastr.success('Ban removed.')
            $(thisBtn).replaceWith($('<button id="ban" type="submit" class="btn btn-info" data-url="/CocktailMagician/ban" data-userid="' + data + '" data-status="' + status + '">Ban User</button>'))
            $('#' + status + '').replaceWith('<td id="' + status + '">Active</td>')
        }
    })
})