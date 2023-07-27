
$(document).ready(function() {

    $('.input-daterange').datepicker({
        format: "dd/mm/yyyy",
        endDate: "now",
        language: "es",
        keyboardNavigation: false,
        autoclose: true,
        todayHighlight: true,
        orientation: "bottom auto",
    });
});