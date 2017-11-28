$(document).ready(function () {
    $('.form-control').on('change', function () {
        if (!isNaN(parseFloat($(this).val()))) {
            var quantity = parseFloat($('#quantity').val().replace(',', '.'));
            var cost = parseFloat($('#cost').val().replace(',', '.'));
            $('#total').val((quantity * cost).toFixed(2).replace('.', ','));
        }
    });
});
