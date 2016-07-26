$(document).ready(function () {
    $('#UserName').focus();

    var tzos = new Date().getTimezoneOffset();
    $('#TimezoneOffsetInMinute').val(-tzos);
});