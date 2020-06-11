function AutoClosingSuccessAlert(text, Delay) {
    $('.alert_placeholders').val('');
    $('.alert_placeholders').html('<div class="alert alert-success alert-block fade in">' +
                                 '<button data-dismiss="alert" class="close close-sm Float-Left" type="button"><i class="fa fa-times"></i> </button>' +
                                 '<h5 style=" text-align:center"><i class="fa fa-check"></i>' + text + '</h5></div>');
    $(".alert_placeholders").fadeTo(Delay, 500).fadeOut(500, function () {
        $(".alert_placeholders").fadeOut(500);
    });
}

function AutoClosingErrorAlert(text, Delay) {
    $('.alert_placeholders').val('');
    $('.alert_placeholders').html('<div class="alert alert-block alert-danger fade in">' +
                                '<button data-dismiss="alert" class="close close-sm Float-Left" type="button"><i class="fa fa-times"></i> </button>' +
                                '<h5 style=" text-align:center">' + text + '</h5></div>');
    $(".alert_placeholders").fadeTo(Delay, 500).fadeOut(500, function () {
        $(".alert_placeholders").fadeOut(500);
    });
}

function AutoClosingWarningAlert(text, Delay) {
    $('.alert_placeholders').val('');
    $('.alert_placeholders').html('<div class="alert alert-block alert-warning fade in">' +
                                '<button data-dismiss="alert" class="close close-sm Float-Left" type="button"><i class="fa fa-times"></i> </button>' +
                                '<h5 style="text-align:center">' + text + '</h5></div>');
    $(".alert_placeholders").fadeTo(Delay, 500).fadeOut(500, function () {
        $(".alert_placeholders").fadeOut(500);
    });
}