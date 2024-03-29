﻿jQuery.validator.addMethod("numreq", function (value, element, params) {

    var isValid = false;

    if (value != '') {

        var expression = /\bREQ\d{12}$/g;
        var isValid = expression.test(value);

    } else {
        isValid = true;
    }

    return isValid;

}, jQuery.validator.format("Número REQ inválido."));

jQuery.extend(jQuery.validator.methods, {
    date: function (value, element) {
        return this.optional(element) || /^\d\d?\/\d\d?\/\d\d\d?\d?$/.test(value);
    },
    number: function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value);
    },
    range: function (value, element, param) {
        return (Globalize.parseFloat(value) >= Globalize.parseFloat(param[0]) && Globalize.parseFloat(value) <= Globalize.parseFloat(param[1]));
    }
});