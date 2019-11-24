$('#searchBtn').mouseenter(function () {
    var text = $(this).data('info');
    console.log(1)
    toastr.options.timeOut = 0;
    toastr.options.extendedTimeOut = 0;
    toastr.options.positionClass = "toast-top-center";
    toastr.info("Search!");
});
$('#searchBtn').mouseleave(function () {
    toastr.remove();
});

$('#searchBtn').mouseleave(function () {
    toastr.remove();
});