var _checkCancel = true;

function showDialog(containerId, okText, cancelText, okFunction, cancelFunction, width, height, title, noFooter, withOutSelect, hideOkButton, hideCancelButton) {
    if ($("#" + containerId).children().length == 0)
        return;

    if (typeof (hideOkButton) == 'undefined' || hideOkButton == null)
        hideOkButton = false;
    if (typeof (hideCancelButton) == 'undefined' || hideCancelButton == null)
        hideCancelButton = false;
    
    removeErrors('#' + containerId);
    _checkCancel = true;
    var pre = document.createElement('div');
    //custom style.
    //pre.style.maxHeight = "600px";
    pre.style.overflowWrap = "break-word";
    //pre.style.margin = "-16px -16px -16px 0";
    //pre.style.paddingBottom = "24px";
    title = checkTitle(title, containerId);
    $("#" + containerId).children().detach().appendTo(pre);

    alertify.defaults.glossary.title = title ? title : "&nbsp;";
    $(".ajs-header").html(title ? title : "&nbsp;");

    //show as confirm
    var dialog = alertify.confirm(pre, function () {
        if (okFunction() == false) {
            _lastClose = function () {
                $(pre).children().detach().appendTo("#" + containerId);
            }
            return false;
        }
        $(pre).children().detach().appendTo("#" + containerId);

    }, function () {
        if (false && cancelText == _cancelText && _checkCancel) {
            _checkCancel = false;
            showConfirm2(_cancelWarningMsg, _yesText, _noText, function () {
                closeDialog(true);
                _checkCancel = true;
            }, function () {
                _checkCancel = true;
            }, _cancelWarningTitle)

            return false;
        }
        else {
            if (cancelFunction) {
                cancelFunction();

                if (hideOkButton) {
                    $('button.ajs-button.ajs-ok').removeClass('hidden');
                }
                if (hideCancelButton) {
                    $('button.ajs-button.ajs-cancel').removeClass('hidden');
                }
            }
            $(pre).children().detach().appendTo("#" + containerId);
        }
    }).set('resizable', true).resizeTo(width == undefined ? 400 : width, height == undefined ? 350 : height).set('autoReset', false).setting('labels', { 'ok': okText, 'cancel': cancelText });

    if (withOutSelect) {
        setTimeout(function () {
            if ($(document.activeElement).prop("tagName") != 'INPUT' && $('.ajs-ok').css('display') == 'none')
                $(document.activeElement).blur();
        }, 800);
    }
    else
        checkFirstElement();
    if (noFooter)
        dialog.set('frameless', true);
    else
        dialog.set('frameless', false);

    if (hideOkButton) {
        $('button.ajs-button.ajs-ok').addClass('hidden');
    }
    if (hideCancelButton) {
        $('button.ajs-button.ajs-cancel').addClass('hidden');
    }

    dialog.set('closableByDimmer', false);
}



function implementConfirmN(dialogNumber, okText, cancelText, okFunction, cancelFunction, title) {
    //define a new dialog
    if (!title)
        title = "&nbsp;";

    var isTriggered = false;

    alertify.dialog('confirm' + dialogNumber, function factory() {
        return {
            main: function (message) {
                this.message = message;
            },
            setup: function () {
                return {
                    buttons: [
                             {
                                 /* button label */
                                 text: okText,
                                 /*bind a keyboard key to the button */
                                 //key: 13,
                                 /* indicate if closing the dialog should trigger this button action */
                                 invokeOnClose: false,
                                 /* custom button class name  */
                                 className: alertify.defaults.theme.ok
                             }, {
                                 /* button label */
                                 text: cancelText,
                                 /* indicate if closing the dialog should trigger this button action */
                                 invokeOnClose: true,
                                 /* custom button class name  */
                                 className: alertify.defaults.theme.cancel
                             }
                    ],
                    focus: { element: 0 },
                    options: {
                        maximizable: false
                    },
                };
            },
            build: function () {
                $(this.elements.buttons.primary.children[0]).on('click', function () {
                    isTriggered = true;
                    if (okFunction)
                        isTriggered = okFunction();
                    return isTriggered;
                });

                $(this.elements.buttons.primary.children[1]).on('click', function () {
                    isTriggered = true;
                    if (cancelFunction)
                        isTriggered = cancelFunction();
                    return isTriggered;
                });

                this.setHeader(title);
            },
            hooks: {
                onclose: function () {
                    if (cancelFunction && isTriggered === false)
                        return cancelFunction(true);
                }
            },
            prepare: function () {
                this.setContent(this.message);
            }
        }
    });
}


var _dialogCounter = 1;
function showDialog2(containerId, okText, cancelText, okFunction, cancelFunction, width, height, title, noFooter, withOutSelect, hideOkButton) {
    
    if ($("#" + containerId).children().length == 0)
        return;

    if (typeof (hideOkButton) == 'undefined' || hideOkButton == null)
        hideOkButton = false;


    removeErrors('#' + containerId);
    title = checkTitle(title, containerId);
    //$(document).keypress(function (e) {
    //    if (e.which == 13) {
    //        var okButtons = $(".alertify:not(.ajs-hidden) .ajs-button.ajs-ok");
    //        if (okButtons.length > 0) {
    //            $(okButtons[okButtons.length - 1]).click();
    //        }
    //    }
    //});

    _checkCancel = true;

    implementConfirmN(++_dialogCounter, okText, cancelText, function () {
        if (okFunction() == false) {
            _lastClose = function () {
                $(pre).children().detach().appendTo("#" + containerId);
            }
            return false;
        }
        $(pre).children().detach().appendTo("#" + containerId);
    }, function (isFromClose) {
        if (false && cancelText == _cancelText && _checkCancel && !isFromClose) {

            _checkCancel = false;

            showConfirm2(_cancelWarningMsg, _yesText, _noText, function () {
                $(pre).children().detach().appendTo("#" + containerId);
                closeDialog(true);
                _checkCancel = true;

            }, function () {
                _checkCancel = true;
            }, _cancelWarningTitle)

            return false;
        }
        else {

            if (cancelFunction)
                cancelFunction();
            $(pre).children().detach().appendTo("#" + containerId);

            _checkCancel = true;
        }
    }, title);


    //alertify.confirm2("Browser dialogs made easy!");


    var pre = document.createElement('div');
    //custom style.
    //pre.style.maxHeight = "600px";
    pre.style.overflowWrap = "break-word";
    //pre.style.margin = "-16px -16px -16px 0";
    //pre.style.paddingBottom = "24px";

    $("#" + containerId).children().detach().appendTo(pre);

    //alertify.defaults.glossary.title = title ? title : "&nbsp;";
    //$(".ajs-header").html(title ? title : "&nbsp;");

    //show as confirm
    var dialog = alertify["confirm" + _dialogCounter](pre, function () {

    }, function () {

    }).set('resizable', true).resizeTo(width == undefined ? 400 : width, height == undefined ? 350 : height).set('autoReset', false).setting('labels', { 'ok': okText, 'cancel': cancelText });

    if (hideOkButton){
        $(".ajs-cancel").focus();
        $(".ajs-ok").addClass('hidden');
    }
    else
        $(".ajs-ok").focus();
    if (withOutSelect) {
        setTimeout(function () {
            if ($(document.activeElement).prop("tagName") != 'INPUT' && $('.ajs-ok').css('display') == 'none')
                $(document.activeElement).blur();
        }, 800);
    }
    else
        checkFirstElement();
    if (noFooter)
        dialog.set('frameless', true);
    else
        dialog.set('frameless', false);

    dialog.set('closableByDimmer', false);

    $($(".ajs-content")[$(".ajs-content").length - 1]).append(pre);
}

function showConfirm(message, okText, cancelText, okFunction, cancelFunction, title) {
    alertify.defaults.glossary.title = title ? title : "&nbsp;";
    $(".ajs-header").html(title ? title : "&nbsp;");
    alertify.confirm(message,
 function () {
     return okFunction();
 },
 function () {
     if (cancelFunction)
         return cancelFunction();
 }).setting('labels', { 'ok': okText, 'cancel': cancelText }).set('frameless', false).set('closableByDimmer', false);;

}

function showConfirm2(message, okText, cancelText, okFunction, cancelFunction, title) {

    implementConfirmN(++_dialogCounter, okText, cancelText, function () {
        return okFunction();
    }, function (isClose) {
        if (cancelFunction)
            return cancelFunction(isClose);
    }, title);


    //alertify.defaults.glossary.title = title ? title : "&nbsp;";
    //$(".ajs-header").html(title ? title : "&nbsp;");
    alertify["confirm" + _dialogCounter](message,
 function () {

 },
 function () {

 }).setting('labels', { 'ok': okText, 'cancel': cancelText }).set('frameless', false).set('closableByDimmer', false);

}

function showPrompt(message, okText, cancelText, okFunction, cancelFunction, title) {
    alertify.defaults.glossary.title = title ? title : "&nbsp;";
    $(".ajs-header").html(title ? title : "&nbsp;");
    alertify.prompt(message, "",
 function (e, result) {
     return okFunction(result);
 },
 function () {
     if (cancelFunction)
         return cancelFunction();
 }).setting('labels', { 'ok': okText, 'cancel': cancelText, 'closableByDimmer': false });

}

function showAlert(message, okText, okFunction, title, width, height) {
    if (!message)
        message = _generalErrorMessage;

    if (!okText)
        okText = _okText;
    alertify.defaults.glossary.title = title ? title : _noteText;
    $(".ajs-header").html(title ? title : _noteText);
    alertify
.alert(message, function () {
    if (okFunction)
        return okFunction();
}).set('label', okText).set('resizable', true).set('closableByDimmer', false).resizeTo(width == undefined ? 400 : width, height == undefined ? 200 : height).set('autoReset', false);

}

var _lastClose;

function closeDialog(number) {

    _checkCancel = false;

    if (_lastClose)
        _lastClose();

    if (number) {

        var closes = $(".alertify:not(.ajs-hidden) .ajs-close");
        if (closes.length > 0)
            $(closes[closes.length - 2]).trigger('click');

    }
    else {

        if ($(".ajs-close"))
            $(".ajs-close").trigger('click');

        alertify.closeAll();
    }
}

function showSide(type, message) {
    if (type) {
        if (type == "Success") {
            if (message)
                alertify.success(message);
            else
                alertify.success(_generalSuccess);
        }
        else
            if (type == "Error") {
                if (message)
                    alertify.error(message);
                else
                    alertify.error(_generalErrorMessage);
            }
            else
                alertify.message(message);
    }
    else
        alertify.success(_generalSuccess);
}

function checkTitle(title, containerId) {
    var result = title;
    var pageTitleText = _pageTitleText;
    if ((!pageTitleText || pageTitleText == '') && $("#main-tabs a.active") && $("#main-tabs a.active").text() && pageTitleText.indexOf($("#main-tabs a.active").text().trim().replace(/\s*\(.*?\)\s*/g, '')) == -1)
        pageTitleText = $("#main-tabs a.active").text().trim().replace(/\s*\(.*?\)\s*/g, '');
    if (!title || title == _addTxt || title == _editTxt) {
        switch (_gridActionType) {
            case 1:
                if (pageTitleText && pageTitleText != '')
                    result = _addNewElementTxt + pageTitleText;
                break;
            case 2:
                if (pageTitleText && pageTitleText != '')
                    result = _editElementTxt + pageTitleText;
                break;
        }

    }

    if ($("#main-tabs a.active")) {
        var tabText = $("#main-tabs a.active").text().trim().replace(/\s*\(.*?\)\s*/g, '');
        if (result && tabText != _documentText && tabText != _propertiesText && result.indexOf(tabText) == -1)
            result += ' - ' + tabText;
    }
    if (containerId && containerId != '' && $('#' + containerId + ' input[type=text]:not([readonly]):not([disabled])') && $('#' + containerId + ' input[type=text]:not([readonly]):not([disabled])').val())
        result += getTitleUsername($('#' + containerId + ' input[type=text]:not([readonly]):not([disabled])').val());
    return result;
}
function focusFirstElement() {
    $('button.ajs-button.ajs-ok').blur();
    if ($('.ajs-content:last input[type=text]:not([readonly]):not([disabled]):not(.datepicker1):not(.datepicker2)')[0] && $($('.ajs-content:last input[type=text]:not([readonly]):not([disabled]):not(.datepicker1):not(.datepicker2)')[0])) {
        setTimeout(function () {
            var elmArray = $('.ajs-content:last input[type=text]:not([readonly]):not([disabled]):not(.datepicker1):not(.datepicker2)');
            if (elmArray.length > 0)
                $(elmArray[0]).focus();
        }, 200);
    }
}
function checkFirstElement() {
    setTimeout(function () {
        focusFirstElement();
    }, 600);
}
