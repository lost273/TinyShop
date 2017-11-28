$(document).ready(function () {
    $('.form-control').on('change', function () {
        if (!isNaN(parseFloat($(this).val()))) {
            //replace the comma with a period for the multiplication operation
            var quantity = parseFloat($('#quantity').val().replace(',', '.'));
            var cost = parseFloat($('#cost').val().replace(',', '.'));
            //replace the period with a comma for the validation
            $('#total').val((quantity * cost).toFixed(2).replace('.', ','));
        }
        //replace dot to comma in the input element
        if ($(this).val() == $('#cost').val()) {
            $('#cost').val($('#cost').val().replace('.', ','));
        }
    });
});
