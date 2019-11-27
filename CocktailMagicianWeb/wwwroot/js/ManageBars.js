$('#barTable').on('click', '#unhide', function (event) {
    event.preventDefault();
    const bar = $(this).data('bar');
    const thisBtn = $(this);
    $.ajax({
        url: '/Bar/UnhideBar',
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({ Id: bar }),
        method: 'POST',
        success: function () {
            $(thisBtn).replaceWith($('<a id="hide" data-bar=" ' + bar + '" href="" >Hide</a>'))
        }
    })
});

$('#barTable').on('click', '#hide', function (event) {
    event.preventDefault();
    const bar = $(this).data('bar');
    const thisBtn = $(this);
    $.ajax({
        url: '/Bar/HideBar',
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({ Id: bar }),
        method: 'POST',
        success: function () {
            $(thisBtn).replaceWith($('<a id="unhide" data-bar=" ' + bar + '" href="" >Unhide</a>'))
        }
    })
});

