// jQuery load
jQuery(function ($) {

    paceOptions = {
        eventLag: {
            lagThreshold: 10
        }
    }

    //Register a handler to be called when the first Ajax request begins    
    $(document).ajaxStart(function () {
        Pace.restart();
    });

    // Disable browser cache Ajax.ActionLinks
    $.ajaxSetup({ cache: false });

    // Pagination
    $('.btn-pagination').click(function () {
        blockUI('Carregando...');
        return true;
    });

    // Initialize tooltip Bootstrap    
    $('[data-toggle="tooltip"]').tooltip({
        trigger: 'hover'
    });

    $('[data-toggle="tooltip"]').click(function () {
        $(this).tooltip("hide");
    });

    // Bootstrap Datetimepicker
    $('.datepicker').datetimepicker({
        format: 'DD/MM/YYYY',
        locale: 'pt-BR'
    });

    $('.datetimepicker').datetimepicker({
        locale: 'pt-BR'
    });

    // jQuery Mask Plugin
    $('.datepicker-mask').mask("00/00/0000", { placeholder: "__/__/____" });
    $('.datetimepicker-mask').mask('00/00/0000 00:00:00', { placeholder: "__/__/____ __:__:__" });
    $('.matricula-caixa').mask("C000000", { placeholder: "C123456" });
    $('.matricula-prestador').mask("P000000", { placeholder: "P123456" });
    $('.numero-req').mask("REQ000000000000", { placeholder: "REQ000000000000" });


    //Initialize Select2 Elements
    function matchStart(params, data) {

        // If there are no search terms, return all of the data
        if ($.trim(params.term) === '') { return data; }

        // Do not display the item if there is no 'text' property
        if (typeof data.text === 'undefined') { return null; }

        // `params.term` is the user's search term
        // `data.id` should be checked against
        // `data.text` should be checked against
        var q = params.term.toLowerCase();
        if (data.text.toLowerCase().indexOf(q) > -1 || data.id.toLowerCase().indexOf(q) > -1) {
            return $.extend({}, data, true);
        }

        // Return `null` if the term should not be displayed
        return null;
    }
    $(".select2").select2({
        dropdownAutoWidth: false,
        width: '100%',
        language: 'pt-BR',
        matcher: matchStart
    });

    // Corrige bug jquery validate com plugin Select2
    //$("select").on("select2:close", function (e) {
    //    $(this).valid();
    //});

    $("select").on("select2:unselect", function (evt) {
        if (!evt.params.originalEvent) {
            return;
        }
        evt.params.originalEvent.stopPropagation();
    });

    // Corrige bug jquery validate input files
    $('input[type="file"]').change(function () {
        $(this).valid();
    });

    // Função To Top
    slideToTop();   

});


// Functions
function slideToTop() {

    var $slideToTop = $('<div />')

    $slideToTop.html('<i class="fa fa-chevron-up"></i>')

    $slideToTop.css({
        position: 'fixed',
        bottom: '20px',
        right: '25px',
        width: '40px',
        height: '40px',
        color: '#eee',
        'font-size': '',
        'line-height': '40px',
        'text-align': 'center',
        'background-color': '#222d32',
        cursor: 'pointer',
        'border-radius': '5px',
        'z-index': '99999',
        opacity: '.7',
        'display': 'none'
    })

    $slideToTop.on('mouseenter', function () {
        $(this).css('opacity', '1')
    })

    $slideToTop.on('mouseout', function () {
        $(this).css('opacity', '.7')
    })

    $('.wrapper').append($slideToTop)

    $(window).scroll(function () {
        if ($(window).scrollTop() >= 150) {
            if (!$($slideToTop).is(':visible')) {
                $($slideToTop).fadeIn(500)
            }
        } else {
            $($slideToTop).fadeOut(500)
        }
    })

    $($slideToTop).click(function () {
        $('html, body').animate({
            scrollTop: 0
        }, 500)
    })
}

function blockUI(texto) {
    $.blockUI({
        message: '<i class="fa fa-circle-o-notch fa-spin fa-fw"></i> ' + texto,
        css: {
            fontSize: '22px',
            border: "2px solid #999",
            padding: "18px 0",
            backgroundColor: "#fff",
            "-webkit-border-radius": "5px",
            "-moz-border-radius": "5px",
            "border-radius": "5px",
            opacity: .9,
            color: "#222",
            zIndex: 99999
        },
        overlayCSS: {
            backgroundColor: 'transparent',
            zIndex: 99999
        }
    })
}

function unblockUI() {
    $.unblockUI()
}

function errorSummary(data) {

    var message = '';
    var messagesList = '';

    for (var i = 0, length = data.length; i < length; i++) {
        message = data[i];
        messagesList += '<li>' + message + '</li>';
    }

    var html = '<div class="alert alert-danger">';
    html += '<button type="button" class="close" data-dismiss="alert">×</button>';
    html += '<h4>Ops! Alguma coisa não deu certo:</h4>';
    html += '<ul>';
    html += messagesList;
    html += '</ul>';
    html += '</div>';

    return html;
}

function errorSweetAlert(data) {

    var message = '';
    var messagesList = '';

    for (var i = 0, length = data.length; i < length; i++) {
        message = data[i];
        messagesList += '<li>' + message + '</li>';
    }

    var html = '';
    html += '<ul>';
    html += messagesList;
    html += '</ul>';
    html += '</div>';

    return html;
}

function alertError(text) {
    return '<div class="alert alert-danger">' + text + '</div>';
}
