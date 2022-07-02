$(document).ready(function () {
    $('#autoWidth,#autoWidth1,#autoWidth2,#autoWidth3').lightSlider({
        autoWidth: true,
        loop_continue: true,
        onSliderLoad: function () {
            $('#autoWidth,#autoWidth1,#autoWidth2,#autoWidth3').removeClass('cS-hidden');
        }
    });
});
