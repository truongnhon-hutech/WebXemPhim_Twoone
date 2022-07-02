$(document).ready(function () {
    $('#autoWidth4,#autoWidth5').lightSlider({
        autoWidth: true,
        loop_continue: true,
        onSliderLoad: function () {
            $('#autoWidth4,#autoWidth5').removeClass('cS-hidden');
        }
    });
});