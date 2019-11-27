//// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.

//$('#createBarBtn').click(function (e) {
//    e.preventDefault();
//    var serialized = $('#createBarForm').serialize();
//    var url = '/Bar/CreateBar';
//    var fdata = new FormData();
//    console.log(serialized)
//  //  var fileInput1 = $('#barImage')[0].files[0];
//    var file_data = $('#barImage').prop('files')[0];
//    fdata.append("picture", file_data);
//    url = url + `?${serialized}`

//    $.ajax({
//        url: url,
//        method: 'POST',
//        data: fdata,
//        contentType: false,
//        processData: false,
//        success: function (responseData) {

//            if (responseData.length === 0) {
                
//                console.log('asd')

//            }
//            else {
             
//            }
//        }

//    })
//});
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    var $googleCustomSearchCocktailImages = $('#googleCustomSearch-cocktailImages');
    var $cocktailNameField = $('#cocktailNameField');
    $googleCustomSearchCocktailImages.attr('disabled', true);
    var $googleSuggestionsModal = $('#google-suggestions-modal');
    var $googleSuggestionsModalBody = $googleSuggestionsModal.find('.modal-body');

    $('#googleCustomSearch-cocktailImages').click(function (e) {
        e.preventDefault();
        var queryParams = $('#cocktailNameField').val().trim().split(" ");
        var $modal = $('#google-suggestions-modal');
        var url = $(this).data('url') + '?'; //queryParams=[${queryParams}]`;

        queryParams.forEach(param => {
            url = url + `queryParams=${param}&`;
        });
        url = url.slice(0, -1);
        console.log(url);

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                if (data.error) {
                    toastr.error(data.message);
                }
                else {
                    debugger;
                    $modal.find('.modal-body').html(data);
                    $modal.modal('toggle');
                }
            },
        });
    });

    $cocktailNameField.on('input', function () {
        if ($cocktailNameField.val() && $cocktailNameField.val().length >= 2) {
            $googleCustomSearchCocktailImages.attr('disabled', false);
        }
        else {
            $googleCustomSearchCocktailImages.attr('disabled', true);
        }
    });


    $googleSuggestionsModal.on('hidden.bs.modal', function () {
        var selectedUrl = $googleSuggestionsModalBody.data('selectedUrl');
        console.log(selectedUrl);

        $('#files').val() = selectedUrl;
    });
});
$('#createEvent').click(function (e) {

    debugger;
    e.preventDefault();
    var $targetElement = $('#barEventForm').find('.modal-body');
    var url = $(this).data('url');
    console.log(url);

    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            if (data.error) {
                toastr.error(data.message);
            }
            else {
                $('#barEventForm').modal('show');
                $targetElement.html(data);
            }
        },
    })
});

function emailValidation(value) {
    return value.indexOf(`@`) >= 0;
}

$('.getDirectionsBtn').click(function (e) {
    debugger;
    $('#directionsMapModal').modal('show');

    var lat = $(this).data('lat');
    var lng = $(this).data('lng');
    var travelMode = $('#active-travel-mode').data('mode');

    $('#directionsMapModal').attr('data-lat', lat);
    $('#directionsMapModal').attr('data-lng', lng);

    setTimeout(function () {
        initDirections(lat, lng, travelMode);
    }, 100);
});

$('#closeModalBtn').on('click', function () {
    debugger;
    console.log($('#directionsMapModal'));
    console.log($('#directionsMapModal').data('long'));
    console.log($('#directionsMapModal').data('lat'));
    $('#directionsMapModal').find('.modal-body').children().remove();
    $('#directionsMapModal').removeData('lat');
    $('#directionsMapModal').removeData('lng');
});


$('.travelModeIcon').click(function (e) {
    debugger;

    changeTravelMode($(this));

    var travelMode = $(this).data('mode');

    var lat = $('#directionsMapModal').data('lat');
    var lng = $('#directionsMapModal').data('lng');
    $('#directionsMapModal').attr('data-lng', lng);
    initDirections(lat, lng, travelMode);
});

function changeTravelMode($element) {
    debugger;
    $('#active-travel-mode').removeAttr('id');

    var idToAdd = 'active-travel-mode';
    $element.attr('id', idToAdd);
};

function changeTravelModeByData(travelMode) {
    debugger;
    $('#active-travel-mode').removeAttr('id');

    var idToAdd = 'active-travel-mode';
    var travelModes = $('.travelModeIcon');

    $.each(travelModes, function (index, value) {

        debugger;
        console.log(value);

        if (value.dataset.mode == travelMode) {
            value.setAttribute('id', idToAdd);
        }
    });
};


