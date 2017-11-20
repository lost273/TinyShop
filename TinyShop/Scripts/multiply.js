$(document).ready(function () {
    $('.form-control').on('change', function () {
        if (!isNaN(parseFloat($(this).val()))) {
            $('#total').val((parseFloat($('#quantity').val()) * parseFloat($('#cost').val())).toFixed(2));
        }
    });
});