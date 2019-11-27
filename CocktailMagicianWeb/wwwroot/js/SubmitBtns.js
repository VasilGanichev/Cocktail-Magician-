$('#Submit').mouseenter(function () {
    let name = $('#name').val()
    const type = $('.type').data('type')
    let controller = ''
    if (type === 'cocktail') {
        controller = type + 's';
    }
    else if (type === 'ingredient') {
        controller = type + 's';
    }
    else if (type == 'bar') {
        controller = type
    }
    const url = `/${controller}/NameExists`
    $.ajax({
        url: url,
        data: { name: name },
        success: function (responseData) {
            if (responseData === true) {
                $('#Submit').prop('disabled', true);
                toastr.options.timeOut = 2000;
                toastr.options.preventDuplicates = true
                toastr.options.positionClass = "toast-top-center";
                toastr.warning(`You cannot create the ${type} as this name is alraedy taken.`);
            }
            else {
                toastr.options.timeOut = 0;
                toastr.options.positionClass = "toast-top-center";
                toastr.options.preventDuplicates = true
                function restore() {
                    toastr.remove();
                    $('#Submit').prop('disabled', false);
                }
                setTimeout(restore, 2000)

                toastr.success(`You can create the ${type}.`);
                toastr.clear($(this).closest('.toast'));
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

$('#name').change(function () {
    console.log(1)
    toastr.remove();
    $('#Submit').prop('disabled', false);
})