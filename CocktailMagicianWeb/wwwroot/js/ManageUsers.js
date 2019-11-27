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
            toastr.options.positionClass = "toast-top-center";
            toastr.success('User promoted.')
            const a = (thisBtn.parent().find(userId))
            $(thisBtn).replaceWith($('<button type="submit" class="btn demote" style="background-color:#ff0000; color:white" data-url="/CocktailMagician/demote" data-userid="' + userId + '">Demote</button>'))
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
            toastr.options.positionClass = "toast-top-center";
            toastr.success('User demoted.')
            $(thisBtn).replaceWith($('<button type="submit" class="btn promote" style="background-color:#ff0000; color:white" data-url="/CocktailMagician/promote" data-userid="' + userId + '">Promote</button>'))
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
            toastr.options.positionClass = "toast-top-center";
            toastr.success('User banned')
            $(thisBtn).replaceWith($('<button id="remove-ban" type="submit" class="btn" style="background-color:#ff0000; color:white" data-url="/CocktailMagician/removeban" data-userid="' + data + '" data-status="' + status + '">Remove Ban</button>'))
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
            toastr.options.positionClass = "toast-top-center";
            toastr.success('Ban removed.')
            $(thisBtn).replaceWith($('<button id="ban" type="submit" class="btn" style="background-color:#ff0000; color:white" data-url="/CocktailMagician/ban" data-userid="' + data + '" data-status="' + status + '">Ban User</button>'))
            $('#' + status + '').replaceWith('<td id="' + status + '">Active</td>')
        }
    })
})