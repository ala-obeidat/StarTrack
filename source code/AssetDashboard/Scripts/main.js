var _pendingInterval = {};
$(document).ready(function () {
    hideLoader();
});
function toggleDiv(id) { 
    if ($('.' + id).hasClass('hidden')) {
        $('.' + id).removeClass('hidden');
    }
    else {
        $('.' + id).addClass('hidden');
    }
}
function checkDate(date) {
    if (date == '0001-01-01')
        return '';
    return date;
}
function checkInt(value) {
    if (!value || value.length == 0 || value.trim() == '') {
        return '0';
    }
    else
        return value;
}
function checkSelectName(value,selectId) {
    if (!value || value.length == 0 || value == '' || value == 0) {
        return '';
    }
    else {
        return $("#" + selectId + " option[value='" + value + "']").text();
    }
}
function removeErrors(selector) {
    $(selector + ' .error-span').each(function () {
        $(this).remove();
    });
    $(selector + ' .error').each(function () {
        $(this).removeClass('error');
    });
}
function isValid(selector) {
    var valid = true;
    var errorMessages = [];
    removeErrors(selector);
    $(selector + ' .required').each(function () {
        if (!this || !this.value || this.value.trim() === '' || this.value.trim() == '0.00') {
            valid = false;
            $(this).addClass('error');
            $('<span title="' + _requiredTxt + '" class="error-span" >' + _requiredTxt + '</span>').insertAfter(this);
            $(this).focus(function () {
                $(this).removeClass('error');
            });
        }
    });
    $(selector + ' .required2').each(function () {
        if (!this || !this.value || this.value.trim() === '' || this.value.trim() == '0.00') {
            valid = false;
            $(this).addClass('error');
            $(this).focus(function () {
                $(this).removeClass('error');
            });
        }
    });

    $(selector + ' .file-required').each(function () {
        if (!this || !this.files || this.files.length === 0) {
            var targetImage = $(this).data('target');
            if ($('#' + targetImage).attr('src').split('/')[$('#' + targetImage).attr('src').split('/').length - 1] === "0") {
                valid = false;
                var errorMessage = $(this).data('message');
                if (errorMessage)
                    errorMessages.push(errorMessage);
            }
        }
    });
    if (errorMessages.length) {
        alert(errorMessages.join('\n') + '\n ...');
    }
    return valid;
}
function readURL(input) {
    if (input.files && input.files[0]) {
        var targetId = $(input).data('target');
        var previewId = $(input).data('preview');
        for (i = 0; i < input.files.length; i++) {
            var reader = new FileReader();

            reader.onload = function (event) {
                if (targetId)
                    $('#' + targetId).attr('src', event.target.result);
                else {
                    var appendHtml = '<div data-imageId="' + (i + 99) + '" class="image-items image-item-added" id="itemImage_' + (i + 99) + '">';
                    appendHtml += '<img width="150" height="150" src="' + event.target.result + '" />';
                    appendHtml += '</div>';
                    $(appendHtml).appendTo(document.getElementById(previewId));
                }
            }
            reader.readAsDataURL(input.files[i]);
        }
        if (previewId && !document.getElementById('deleteAllImages'))
            $('<input id="deleteAllImages" onclick="removeImage(' + (i + 99) + ',true)" class="btn btn-danger image-item-added" title="' + _deleteTxt + '" value="x" />').appendTo(document.getElementById(previewId));
    }
}
function redirectWithBaseUrl(url) {
    if ((url.indexOf('en/') > -1 || url.indexOf('ar/') > -1))
        location.href = _baseUrl + url;
    else
        location.href = _baseUrl + _lan + '/' + url;
}
var _alerted = false;
function listPending() {
    console.log("List pending", new Date());
    if (_orderPage) {
        console.log("Order page");
        clearInterval(_pendingInterval);
    }
    else
        ajaxCall(getRequestModel(false, _baseUrl + 'Order/ListPending', null, function (result) {
            if (result.ErrorCode == 0) {
                if (result.Data.OrdersCount > 0) {
                    clearInterval(_pendingInterval);
                    if (document.hasFocus()) {
                        console.log("Already focused");
                        $(window).on("blur", function (e) {
                            var audio = new Audio(_baseUrl + 'assets/sound/recording.mp3');
                            audio.loop = true;
                            audio.play();
                        });
                        showAlert(result.Message, null, function () {
                            redirectWithBaseUrl('Order/Index');
                        });
                        _alerted = true;
                    }
                    else {
                        console.log("blur");
                        var audio = new Audio(_baseUrl + 'assets/sound/recording.mp3');
                        audio.loop = true;
                        audio.play();
                        $(window).on("focus", function (e) {
                            if (!_alerted) {
                                _alerted = true;
                                showAlert(result.Message, null, function () {
                                    redirectWithBaseUrl('Order/Index');
                                });
                            } else {
                                var audio = new Audio(_baseUrl + 'assets/sound/recording.mp3');
                                audio.loop = true;
                                audio.play();
                            }
                        });
                    }
                };
            }
        }, null, 'GET'));
}
function encodeWithBase64(code, key) {
    /// <summary>Encrypt string with Base64 and token as Sold</summary>
    /// <param name="code" type="string"> Code to be encode</param> 
    /// <param name="key" type="string"> Sold to be added with Code to get encoding string</param> 
    /// <returns> Encrypted base64 string encoding </returns>

    if (!code || code == '')
        return code;
    var result = [];
    var tempCode = code;
    while (tempCode.length > 0) {
        if (tempCode.length > 2) {
            result.push(tempCode.substring(0, 3));
            tempCode = tempCode.substring(3, tempCode.length);
        }
        else {
            result.push(tempCode.substring(0, tempCode.length));
            tempCode = '';
        }
    }
    var encryptionText = window.btoa(result.join(key));
    return reverseString(encryptionText);
}

function reverseString(str) {
    //Use the split() method to return a new array
    var splitString = str.split("");

    //Use the reverse() method to reverse the new created array
    var reverseArray = splitString.reverse();

    //Use the join() method to join all elements of the array into a string
    var joinArray = reverseArray.join("");

    //Return the reversed string
    return joinArray;
}

function getRequestModel(hasLoader, requestURL, requestData, success, error, type, NoJSONstringify, encodeUrl, dataType) {
    /// <summary>Get request model for ajaxCall</summary>
    /// <param name="hasLoader" type="bool"> if you want to show loader or not. </param>
    /// <param name="requestURL" type="string"> for request url. </param>
    /// <param name="requestData" type="object"> for request data. </param>
    /// <param name="success" type="function"> success function come with data. </param>
    /// <param name="error" type="function"> error function come with data. </param>
    /// <param name="type" type="string"> method type [ POST or GET ] default is POST</param>
    /// <param name="NoJSONstringify" type="bool"> if you do not want to use JSON.stringify </param>
    /// <param name="encodeUrl" type="bool"> if you want to encodeURI </param>
    /// <param name="dataType" type="string"> if you want to specify dataType, default is json </param>
    /// <returns>result model for ajaxCall function</returns>
    var requestModel =
    {
        hasLoader: hasLoader,
        requestURL: requestURL,
        requestData: requestData,
        success: success,
        error: error,
        type: type,
        NoJSONstringify: NoJSONstringify,
        dataType: dataType
    };
    return requestModel;
}
function sirilizeObject(model) {
    return JSON.stringify(model);
}
function ajaxCall(model) {
    /// <summary>Make an ajax request</summary>
    /// <param name="model" type="Object"> model.hasLoader (bool) if you want to show loader or not,
    /// model.requestURL (string) for request url,
    /// model.requestData (object) for request data,
    /// model.success (function) success function come with data.
    /// model.error (function) error function come with data. 
    /// model.type (string) method type [ POST or GET ] default is POST
    /// model.dataType (string) ajax dataType default is json
    /// model.contentType (string) ajax contentType default is application/json; charset=utf-8
    /// model.NoJSONstringify (bool) if you do not want to use JSON.stringify</param> 
    /// model.encodeUrl (bool) if you want to encodeURI </param> 
    /// <returns> xhr object. </returns>

    if (!model.type)
        model.type = "POST";

    var requestData = ((!model.requestData || model.requestData == null || model.requestData == undefined || model.requestData == '' || model.NoJSONstringify) ? model.requestData : sirilizeObject(model.requestData));
    if (model.encodeUrl)
        requestData = encodeURIComponent(requestData);
    if (model.hasLoader)
        showLoader();
    if (!model.dataType && model.dataType != false)
        model.dataType = "json";

    if (!model.contentType && model.contentType != false) {
        model.contentType = "application/json; charset=utf-8";
    }
    else
        model.processData = false;
    var ajaxObject = {
        url: model.requestURL,
        type: model.type,

        cache: false,
        beforeSend: function (request) {
            //request.setRequestHeader("RANTOKEN", _OLE5TOKENR);
            //request.setRequestHeader("RANTOKENKEY", _OLE5TOKENRKEY);
            model.requestData = requestData;
            request.RequstModel = model;
        },
        data: requestData,
        dataType: model.dataType,
        traditional: true,
        contentType: model.contentType,
        success: function (data) {
            if (model.hasLoader)
                hideLoader();
            if (model.success)
                model.success(data);

        },
        error: function (data) {
            debugger;
            if (model.hasLoader)
                hideLoader();

            if (model.error)
                model.error(data);
            else {
                if (showAlert)
                    showAlert(_generalErrorMessage, null, null, _errorText);
                else
                    alert(_generalErrorMessage);
            }

        },
    };
    if (model.processData == false)
        ajaxObject.processData = false;
    var xhr = $.ajax(ajaxObject);
    return xhr;
}

function ajaxCallUpload(model) {
    /// <summary>Make an ajax request to upload file</summary>
    /// <param name="model" type="Object"> model.hasLoader (bool) if you want to show loader or not,
    /// model.requestURL (string) for request url,
    /// model.Data (object) for request data,
    /// model.success (function) success function come with data.
    /// model.error (function) error function come with data.</param>  
    /// <returns> NULL </returns>
    var requestModel = model;
    showConfirm(_removeFirstRow, _yesText, _noText, function () {
        requestModel.Data.append('EXCLUDE_FIRST_ROW', 1);
        ajaxCallUploadAfterCheck(requestModel);
    }, function () {
        requestModel.Data.append('EXCLUDE_FIRST_ROW', 0);
        ajaxCallUploadAfterCheck(requestModel);
    }, _noteText);


}
function ajaxCallUploadAfterCheck(model) {
    if (model.hasLoader)
        showLoader();
    $.ajax({
        url: model.requestURL,
        type: 'POST',
        data: model.Data,
        cache: false,
        dataType: 'json',
        beforeSend: function (request) {
            //request.setRequestHeader("RANTOKEN", _OLE5TOKENR);
            //request.setRequestHeader("RANTOKENKEY", _OLE5TOKENRKEY);
        },
        processData: false, // Don't process the files
        contentType: false, // Set content type to false as jQuery will tell the server its a query string request
        success: function (data, textStatus, jqXHR) {
            if (data.ErrorCode == 1213) {
                reloginAfterAjax();
                return;
            }
            if (data.ErrorCode == 9100) {
                location.reload();
                return;
            }
            if (model.hasLoader)
                hideLoader();
            if (model.success)
                model.success(data);
        },
        error: function (data) {
            if (model.hasLoader)
                hideLoader();

            if (model.requestURL != _baseUrl + 'Download/GetNotificationsPubop') {

                if (model.error)
                    handelError(data, function () { model.error(data); });
                else
                    handelError(data);
            }
            else
                if (model.error)
                    model.error(data);
                else {
                    if (showAlert)
                        showAlert(null, null, null, _errorText);
                    else
                        alert(_generalErrorMessage);
                }
        }
    });
}

// 0 => None
// 1 => Add
// 2 => Edit
// 3 => Delete
var _gridActionType = 0;
var _pageTitleText = '';
function showLoader() {
    $("#o-m-loader").removeClass("hidden");
}

function hideLoader() {
    $("#o-m-loader").addClass("hidden");
}
function handelError(data, errorFunction) {
    /// <summary>Handle end session in ajax.</summary>
    /// <param name="data" type="object">error data object.</param>
    /// <returns type="bool">false if session not end and add data error object in console log; true if session end and show relogin dialog</returns>

    return true;
}
function handleLongText(text) {
    if (typeof (text) != 'undefined' && text != '' && text.length > 20)
        if (_isRtl)
            return '...' + text.substring(0, 17);
        else
            return text.substring(0, 17) + '...';
    return text || '';
}
function sendPostRequest(redirectUrl, formId, successFun, additinalModel) {
    /// <summary>Make an ajax request</summary>
    /// <param name="redirectUrl" type="string">[optional] redirect URL after success</param> 
    /// <param name="formId" type="string">[optional] if there are more than form in the page then pass form Id attribute </param> 
    /// <param name="successFun" type="function">[optional] if you want to do something with resutl in success insted of redirect Url</param> 
    /// <returns> </returns>

    var selector = formId ? ('#' + formId) : 'form';
    if (isValid(selector)) {
        var action =  $(selector).attr('action');
        var modelArray = $(selector).serializeArray();
        var model = {};
        var requestData = {};
        var formData = new FormData();
        for (var i = 1; i < modelArray.length; i++) {
            model[modelArray[i].name] = modelArray[i].value;
        }

        if (additinalModel && additinalModel.length) {
            for (var j = 0; j < additinalModel.length; j++) {
                model[additinalModel[j].name] = additinalModel[j].value;
            }
        }

        var filesInputNames = $(selector + ' input[type=file]');
        console.log('Request Model', model);
        requestData = model;
        if (filesInputNames.length) {
            for (var j = 0; j < filesInputNames.length; j++) {
                var files = $(filesInputNames[j]).prop('files');
                var keyName = $(filesInputNames[j]).attr('name');
                if (files.length) {
                    if (files.length == 1)
                        model[keyName] = files[0];
                    else {
                        for (var f = 0; f < files.length; f++) {
                            formData.append(keyName, files[f], files[f].name);
                        }
                    }
                }
            }
            $.each(model, function (key, value) {
                formData.append(key, value);
            });
            requestData = formData;
        }
        var requestModel = getRequestModel(true, action, requestData, function (data) {
            if (data.ErrorCode == 0) {
                showSide();
                if (successFun)
                    successFun(data.Data);
                else {
                    if (redirectUrl) {
                        showLoader();
                        location.href = _baseUrl + redirectUrl;
                    }
                }
            }
            else
                showAlert(data.Message, null, null, _errorText);
        });
        if (filesInputNames.length) {
            requestModel.contentType = false;
            requestModel.NoJSONstringify = true;
        }
        ajaxCall(requestModel);
    }
}
function sendGetRequest(controller, action, id) {
    /// <summary>Make an ajax get request</summary>
    /// <param name="controller" type="string"> redirect controller name</param> 
    /// <param name="action" type="string"> redirect action name</param> 
    /// <param name="id" type="string"> URL Id parameter</param> 
    /// <returns> </returns>
    var action = _baseUrl + controller + '/' + action + '?id=' + id;
    ajaxCall(getRequestModel(true, action, null, function (data) {
        if (data.ErrorCode == 0) {
            if (data.ActionUrl) {
                showAlert(data.Message, null, function () {
                    showLoader();
                    location.href = _baseUrl + controller + '/' + data.ActionUrl;
                });
            }
            else
                showSide();
        }
        else
            showAlert(data.Message, null, null, _errorText);
    }, null, 'GET'));
}