//require(['knockout', 'i18n!/Localizations/nls/localizations.js', 'domReady!', '/signalr/hubs'], function (localizations) {
//    //example : localizations['SAVE_SUCCESSFULLY']

//    var ko = arguments[0];

//    var msgArr = [];
//    var viewModel = {
//        msgArr: ko.observableArray(msgArr)
//    };

//    ko.applyBindings(viewModel);

//    var signalR = $.connection.ServiceCenterHub;
//    if (signalR!=undefined) {
//        signalR.client.addNewMessageToPage = function (message) {
//            viewModel.msgArr.push(message);
//        };
//        $.connection.hub.start().done(function () {
//            //signalR.server.registerConnection();
//        });
//    }
//});