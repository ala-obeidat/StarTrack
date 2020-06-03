function DataTable(tableId, isServer, isCheckBoxEmplemented, deleteMessage, deleteMultiMessage, deleteConfirmMessage, canChangeLength) {


    //If there is any enabled feature of add ,edit or delete then the caller should implement the specific feature function

    this.addItemCallBack = function () {

    };

    this.editItemCallBack = function () {

    };

    this.deleteItemCallBack = function () {

    };

    this.deleteItemsCallBack = function () {

    }

    this.getSettingsCallBack = function () {

    };

    this.tableDrawCallBack = function () {

    };

    this.clickItemCallBack = function () {

    };


    //This function will just calls when the client click in the add, edit or delete.


    //You can call any of these functions to do it's operation in the grid
    //-------------------- bindGrid
    //-------------------- reload
    //-------------------- addItem
    //-------------------- updateItem
    //-------------------- removeItem

    this.autoDeleteHandling = true;
    this.autoHandleCheckChange = true;
    this.deleteMultiItems = false;
    this.withFlag = false;
    this.paging = false;
    this.actionName = '';
    this.controllerName = '';
    this.attributes;
    this.largeColumns = { width: "0%", columns: [] };
    this.totalRecords = 0;
    this.checkedIds = [];
    var resources = {};
    var _hiddenColumns = [];
    var _nonSortingColumns = [];
    var _linkColumns = [];
    var _checkboxesColumns = [];
    var _radiobuttonColumns = [];
    var _isBinded = false;
    var _url = "";
    var _selectedItems = [];
    var _selectedIds = [];
    
    this.displayLength = 10;
    this.displayStart = 0;
    this.encodeUrl = false;
    this._EnabledeselectAll = true;

    /// <summary>if you want to disabel print button if datatable has no recored </summary>
    this.btnPrintId = '';
    this.overridePageSettings = false;

    var _selectedObjects = [];
    var _isServerGrid = isServer !== false;
    var _btnPrintId = '';
    var _btnExportIds = [];
    this.bindGrid = function (selectedItems) {
        if (_isBinded) {
            if (isServer !== false) {
                if (selectedItems)
                    this.reload(selectedItems);
                else
                    this.reload([]);
            }
            else {

                _selectedIds = [];

                if (selectedItems) {
                    _selectedItems = selectedItems;
                    for (var i = 0; i < _selectedItems.length; i++)
                        if (_selectedIds.indexOf(_selectedItems[i][0]) == -1)
                            _selectedIds.push(_selectedItems[i][0]);
                }
                else
                    _selectedItems = [];
            }
        }
        else {

            _selectedIds = [];

            if (selectedItems) {
                _selectedItems = selectedItems;
                for (var i = 0; i < _selectedItems.length; i++)
                    if (_selectedIds.indexOf(_selectedItems[i][0]) == -1)
                        _selectedIds.push(_selectedItems[i][0]);
            }
            else
                _selectedItems = [];

            _isBinded = true;
            resources.LengthMenu = $('#lengthMenuLoc').val();
            resources.ShowingCount = $('#showingCountLoc').val();
            resources.Processing = $('#processing').val();
            resources.EmptyTable = $('#emptyTableLoc').val();
            resources.DeleteMessage = $('#deleteMessage').val();
            resources.DeleteConfirmMessage = $('#deleteConfirmMessage').val();
            resources.Yes = $('#yes').val();
            resources.No = $('#no').val();
            resources.SortDescinding = $('#SortDescinding').val();
            resources.Sorting = $('#Sorting').val();
            resources.SortAscinding = $('#SortAscinding').val();
            if (this.controllerName && this.controllerName != "")
                _url = "/" + this.controllerName + "/" + this.actionName;
            else
                _url = this.actionName;
            _nonSortingColumns = [];
            for (var i = 0; i < this.attributes.length; i++) {
                if (!this.attributes[i].HasSort) {
                    _nonSortingColumns.push(i);
                }
                if (this.attributes[i].IsHidden) {
                    _hiddenColumns.push(i);
                }
                else
                    if (this.attributes[i].IsLink) {
                        _linkColumns.push(i);
                    }
                    else
                        if (this.attributes[i].IsCheckbox) {
                            _checkboxesColumns.push(i);
                        }
                        else
                            if (this.attributes[i].IsRadioButton) {
                                _radiobuttonColumns.push(i);
                            }
            }
            _btnPrintId = this.btnPrintId;
            _btnExportIds = this.btnExportIds;
            var funcSettings = this.getSettingsCallBack;
            var tableDrawCallBack = this.tableDrawCallBack;
            var lAutoHandleCheckChange = this.autoHandleCheckChange;
            var _method = this.method ? this.method : "GET";
            var _encodeUrl = this.encodeUrl ? this.encodeUrl : false;
            var table = isServer !== false ? $('#' + tableId).DataTable({
                "paging": this.paging,
                "processing": true,
                "serverSide": true,
                "bLengthChange": canChangeLength ? true : false,
                "lengthMenu": [[10, 50, 200], [10, 50, 200]],
                "iDisplayLength": canChangeLength && !this.overridePageSettings ? parseInt($('#gridPageSize').val()) : this.displayLength,
                "iDisplayStart": this.displayStart,
                "bFilter": false,
                "sAjaxSource": _url,
                "bSort": _nonSortingColumns.length != this.attributes.length,
                "fnServerData": function (sSource, aoData, fnCallback, oSettings) {

                    if (funcSettings) {
                        aoData.push({ "name": "Settings", "value": JSON.stringify(funcSettings()) });
                    }
                    oSettings.jqXHR = ajaxCall(getRequestModel(false, sSource, (_method == 'POST' ? parseParamsArray(aoData) : aoData), function (data) {
                        if (data.ErrorCode) {
                            if (data.ErrorCode == 5 || data.ErrorCode == 503) {
                                showLoader();
                                window.location.href = _baseUrl + 'Home/NoAccess';
                            }
                            else {
                                $('#' + tableId).parent().parent().hide();
                                showAlert(data.aaData, null, null, _errorText);
                            }
                        }
                        else {
                            fnCallback(data);
                            $('#' + tableId).parent().parent().show();
                        }
                    }, null, _method, _method == 'GET', _encodeUrl));

                },
                "fnDrawCallback": function (oSettings) {
                    releaseButtons();
                    tableDrawCallBack();
                    this.totalRecords = oSettings._iRecordsTotal;
                    if (_btnPrintId && _btnPrintId != '' && $('#' + _btnPrintId))
                        $('#' + _btnPrintId).prop('disabled', oSettings._iRecordsTotal == 0);
                    if (_btnExportIds && _btnExportIds.length > 0) {
                        for (var j = 0; j < _btnExportIds.length; j++) {
                            if ($('#' + _btnExportIds[j])) {
                                if ($('#' + _btnExportIds[j]).is('a')) {
                                    if (oSettings._iRecordsTotal == 0)
                                        $('#' + _btnExportIds[j]).addClass('not-allowed');
                                    else
                                        $('#' + _btnExportIds[j]).removeClass('not-allowed');
                                }
                                else
                                    $('#' + _btnExportIds[j]).prop('disabled', oSettings._iRecordsTotal == 0);
                            }
                        }
                    }
                    if (oSettings._iRecordsTotal < 1) {
                        if (isCheckBoxEmplemented)
                            $('#' + tableId + 'cb-all').parent().hide();
                    }
                    else {
                        if (oSettings._iRecordsTotal == 1)
                            $('#' + tableId + 'cb-all').next().addClass('not-allowed').addClass('not-selected-checkbox');
                        else
                            $('#' + tableId + 'cb-all').next().removeClass('not-allowed').removeClass('not-selected-checkbox');
                        $('#' + tableId + 'cb-all').parent().show();
                        if ($('.call-radiobutton.not-allowed.low-opacity')) {
                            $('.call-radiobutton.not-allowed.low-opacity').click(function () {
                                if ($(this).prop('checked') && ($(this).val() == _emptyGuidTxt || $(this).val()=='0')) {
                                    showSide('Error', _canNotSelectThisItem);
                                    $(this).prop('checked', false);
                                }
                            });
                        }

                        if ($('.not-allowed.not-selected-checkbox.checkbox')) {
                            $('.not-allowed.not-selected-checkbox.checkbox').click(function () {
                                if ($("#" + $(this).attr("for")).val() == _emptyGuidTxt) {
                                    showSide('Error', _canNotSelectThisItem);
                                    $(this).prop('checked', false);
                                }
                            });
                        }
                    }


                    $("#" + tableId + " .call-checkbox").on('change', function () {
                        var item = $('#' + tableId).DataTable().row($(this).parents('tr')[0]).data();
                        if ($(this).prop('checked')) {
                            if (item[0] != _emptyGuidTxt && _selectedIds.indexOf(item[0]) == -1) {
                                _selectedItems.push(item);
                                _selectedIds.push(item[0]);
                            }
                        }
                        else {
                            for (var i = 0; i < _selectedIds.length; i++) {
                                if (_selectedIds[i] == item[0]) {
                                    _selectedIds.splice(i, 1);
                                    _selectedItems.splice(i, 1);

                                    for (var i = 0; i < _selectedObjects.length; i++)
                                        if (_selectedObjects[i].id == item[0])
                                            _selectedObjects.splice(i, 1);

                                    break;
                                }
                            }
                        }


                        checkEditEnableInt();
                        checkDeleteEnableInt();
                    });


                    if (lAutoHandleCheckChange || _selectedIds.length > 0) {

                        var oTable = $('#' + tableId).dataTable();

                        var rowcollection = $("#" + tableId + " .call-checkbox");
                        rowcollection.each(function (index, elem) {

                            for (var i = 0; i < _selectedIds.length; i++)
                                if (_selectedIds[i] == $(elem).val())
                                    $(elem).prop('checked', true);

                            if (lAutoHandleCheckChange) {

                                for (var i = 0; i < _selectedObjects.length; i++)
                                    if (_selectedObjects[i].id == $(elem).val())
                                        $(elem).prop('checked', true);

                                $(elem).change(function () {
                                    if ($(elem).prop('checked')) {

                                        for (var i = 0; i < _selectedObjects.length; i++)
                                            if (_selectedObjects[i].id == $(elem).val())
                                                return;

                                        var object = {};
                                        object.id = $(elem).val();
                                        object.text = $('#' + tableId).DataTable().row(index).data()[2];
                                        if (this.withFlag)
                                            object.SecurityDegree = $('#' + tableId).DataTable().row(index).data()[3];
                                        _selectedObjects.push(object);
                                    }
                                    else {
                                        for (var i = 0; i < _selectedObjects.length; i++)
                                            if (_selectedObjects[i].id == $(elem).val())
                                                _selectedObjects.splice(i, 1);
                                    }
                                });
                            }
                        });
                    }

                    checkEditEnableInt();
                    checkDeleteEnableInt();
                    addSortTitles(resources);
                },
                "aoColumnDefs": [{

                    "aTargets": _hiddenColumns,
                    "bVisible": false


                },
                {
                    'orderable': false,
                    'aTargets': _nonSortingColumns,
                },
                           {
                               "mRender": function (data, type, row) {
                                   return getLink(data, row[0]);
                               },
                               "aTargets": _linkColumns,
                               "bVisible": true
                           }, {
                               "mRender": function (data, type, row) {
                                   return getCheckbox(data, row[0]);
                               },
                               "aTargets": _checkboxesColumns,
                               "bVisible": true
                           },
                                   {
                                       "mRender": function (data, type, row) {
                                           return getRadioButton(data, row[0]);
                                       },
                                       "aTargets": _radiobuttonColumns,
                                       "bVisible": true
                                   },
                                {
                                    "aTargets": this.largeColumns.columns,
                                    "width": this.largeColumns.width
                                }


                ],
                "organisationList": this.organisationList,
                "oLanguage": {
                    "sLengthMenu": resources.LengthMenu,
                    "sInfo": resources.ShowingCount,
                    "sProcessing": resources.Processing,
                    "sEmptyTable": resources.EmptyTable,
                    "sInfoEmpty": "",
                    "sSearch": "",
                    "oPaginate": {
                        "sPrevious": "",
                        "sNext": ""
                    }
                },
            }) :
                 $('#' + tableId).DataTable({
                     "processing": true,
                     "paging": this.paging,
                     "serverSide": false,
                     "bLengthChange": false,
                     "iDisplayLength": canChangeLength && !this.overridePageSettings ? parseInt($('#gridPageSize').val()) : this.displayLength,
                     "bSort": false,
                     "bFilter": false,
                     "fnDrawCallback": function (oSettings) {
                         releaseButtons();
                         tableDrawCallBack();
                         if ($('#' + tableId + ' tr').length < 2) {
                             $('#' + tableId + 'cb-all').parent().hide();
                         }
                         else {
                             if ($('#' + tableId + ' tr').length == 2)
                                 $('#' + tableId + 'cb-all').next().addClass('not-allowed').addClass('not-selected-checkbox');
                             else
                                 $('#' + tableId + 'cb-all').next().removeClass('not-allowed').removeClass('not-selected-checkbox');
                             $('#' + tableId + 'cb-all').parent().show();

                             if ($('.call-radiobutton.not-allowed.low-opacity')) {
                                 $('.call-radiobutton.not-allowed.low-opacity').click(function () {
                                     if ($(this).prop('checked') && ($(this).val() == _emptyGuidTxt || $(this).val() =='0')) {
                                         showSide('Error', _canNotSelectThisItem);
                                         $(this).prop('checked', false);
                                     }
                                 });
                             }


                             //if ($('.not-allowed.not-selected-checkbox.checkbox')) {
                             //    $('.not-allowed.not-selected-checkbox.checkbox').unbind("click");
                             //    $('.not-allowed.not-selected-checkbox.checkbox').click(function () {
                             //        if ($("#" + $(this).attr("for")).val() == _emptyGuidTxt) {
                             //            showSide('Error', _canNotSelectThisItem);
                             //            $(this).prop('checked', false);
                             //        }
                             //    });
                             //}
                         }




                         //$("#" + tableId + " .call-checkbox").on('change', function () {
                         //    var item = $('#' + tableId).DataTable().row($(this).parents('tr')[0]).data();
                         //    if ($(this).prop('checked')) {
                         //        _selectedItems.push(item);
                         //        _selectedIds.push(item[0]);
                         //    }
                         //    else {
                         //        for (var i = 0; i < _selectedIds.length; i++) {
                         //            if (_selectedIds[i] == item[0]) {
                         //                _selectedIds.splice(i, 1);
                         //                _selectedItems.splice(i, 1);
                         //            }
                         //        }
                         //    }
                         //});



                         //if (lAutoHandleCheckChange || _selectedIds.length > 0) {

                         //    var oTable = $('#' + tableId).dataTable();
                         //    var rowcollection = $("#" + tableId + " .call-checkbox");
                         //    rowcollection.each(function (index, elem) {

                         //        $(elem).prop('checked', '');

                         //        for (var i = 0; i < _selectedIds.length; i++)
                         //            if (_selectedIds[i] == $(elem).val())
                         //                $(elem).prop('checked', true);

                         //        if (lAutoHandleCheckChange) {

                         //            for (var i = 0; i < _selectedObjects.length; i++)
                         //                if (_selectedObjects[i].id == $(elem).val())
                         //                    $(elem).prop('checked', true);

                         //            $(elem).change(function () {
                         //                if ($(elem).prop('checked')) {

                         //                    for (var i = 0; i < _selectedObjects.length; i++)
                         //                        if (_selectedObjects[i].id == $(elem).val())
                         //                            return;

                         //                    var object = {};
                         //                    object.id = $(elem).val();
                         //                    object.text = $('#' + tableId).DataTable().row($(elem).parent().parent()).data()[2];
                         //                    if (this.withFlag)
                         //                        object.SecurityDegree = $('#' + tableId).DataTable().row($(elem).parent().parent()).data()[3];
                         //                    _selectedObjects.push(object);
                         //                }
                         //                else {
                         //                    for (var i = 0; i < _selectedObjects.length; i++)
                         //                        if (_selectedObjects[i].id == $(elem).val())
                         //                            _selectedObjects.splice(i, 1);
                         //                }
                         //            });
                         //        }
                         //    });
                         //}


                         if (lAutoHandleCheckChange) {

                             var oTable = $('#' + tableId).dataTable();
                             var rowcollection = $("#" + tableId + " .call-checkbox");
                             rowcollection.each(function (index, elem) {

                                 $(elem).prop('checked', '');

                                 //for (var i = 0; i < _selectedIds.length; i++)
                                 //    if (_selectedIds[i] == $(elem).val())
                                 //        $(elem).prop('checked', true);

                                 if (lAutoHandleCheckChange) {

                                     for (var i = 0; i < _selectedObjects.length; i++)
                                         if (_selectedObjects[i].id == $(elem).val())
                                             $(elem).prop('checked', true);

                                     $(elem).change(function () {
                                         if ($(elem).prop('checked')) {

                                             for (var i = 0; i < _selectedObjects.length; i++)
                                                 if (_selectedObjects[i].id == $(elem).val())
                                                     return;

                                             var object = {};
                                             object.id = $(elem).val();
                                             object.text = $('#' + tableId).DataTable().row($(elem).parent().parent()).data()[2];
                                             if (this.withFlag)
                                                 object.SecurityDegree = $('#' + tableId).DataTable().row($(elem).parent().parent()).data()[3];
                                             _selectedObjects.push(object);
                                         }
                                         else {
                                             for (var i = 0; i < _selectedObjects.length; i++)
                                                 if (_selectedObjects[i].id == $(elem).val())
                                                     _selectedObjects.splice(i, 1);
                                         }
                                     });
                                 }
                             });
                         }
                     },
                     "columnDefs": [{

                         "targets": _hiddenColumns,
                         "visible": false


                     },
                                     {
                                         "mRender": function (data, type, row) {
                                             return getLink(data, row[0]);
                                         },
                                         "targets": _linkColumns,
                                         "visible": true
                                     }, {
                                         "mRender": function (data, type, row) {
                                             return getCheckbox(data, row[0]);
                                         },
                                         "targets": _checkboxesColumns,
                                         "visible": true
                                     }, {
                                         "mRender": function (data, type, row) {
                                             return getRadioButton(data, row[0]);
                                         },
                                         "targets": _radiobuttonColumns,
                                         "visible": true
                                     }

                                     //,
                                     //{
                                     //    "mRender": function (data, type, row) {
                                     //        return getCheckboxDefault(data,row[0],isCheckBoxEmplemented);
                                     //    },
                                     //    "targets": 1,
                                     //    "visible": true
                                     //}



                     ],
                     "oLanguage": {
                         "sLengthMenu": resources.LengthMenu,
                         "sInfo": resources.ShowingCount,
                         "sProcessing": resources.Processing,
                         "sEmptyTable": resources.EmptyTable,
                         "sInfoEmpty": "",
                         "sSearch": "",
                         "oPaginate": {
                             "sPrevious": "",
                             "sNext": ""
                         }
                     },
                 });


            this.reload = function (selectedItems) {
                this.SearchSettings = {};
                var oTable = $('#' + tableId).DataTable();

                if (selectedItems) {
                    _selectedIds = [];
                    if (selectedItems) {
                        _selectedItems = selectedItems;
                        for (var i = 0; i < _selectedItems.length; i++)
                            if (_selectedIds.indexOf(_selectedItems[i][0]) == -1)
                                _selectedIds.push(_selectedItems[i][0]);
                    }
                    else
                        _selectedItems = [];

                    if (this.autoHandleCheckChange) {
                        _selectedObjects = new Array();
                        $('#' + tableId + "cb-all").prop('checked', '');
                    }
                }

                oTable.draw();
            };
            this.destroy = function (innerDivHtml) {
                if (!innerDivHtml)
                    innerDivHtml = '<div id="' + tableId + '"></div>';
                $('#' + tableId).parent().parent().parent().parent().parent().html(innerDivHtml);
                this.reload = null;
            };
            this.getRowData = function (index) {
                var RowData = $('#' + tableId).DataTable().row(index).data();
                var objectDataRow = {};
                if (RowData)
                    for (var i = 0; i < this.attributes.length; i++) {
                        objectDataRow[this.attributes[i].Name] = RowData[i];
                    }
                return objectDataRow;
            };
            // This function disable add button on the grid 
            this.disableAddButton = function () {
                $('#Addbutton' + tableId).prop('disabled', 'disabled');
            }

            // This function enable add button on the grid 
            this.enableAddButton = function () {
                $('#Addbutton' + tableId).prop('disabled', '');
            }

            this.hideEditButton = function () {
                $('#Editbutton' + tableId).hide();
            };
            this.showEditButton = function () {
                $('#Editbutton' + tableId).show();
            };

            this.hideDeleteButton = function () {
                $('#button' + tableId).hide();
            };
            this.showDeleteButton = function () {
                $('#button' + tableId).show();
            };

            // This function disable everything the grid 
            this.disableAll = function () {
                if ($('#button' + tableId))
                    $('#button' + tableId).prop('disabled', true);
                if ($('#Addbutton' + tableId))
                    $('#Addbutton' + tableId).prop('disabled', true);
                if ($('#Editbutton' + tableId))
                    $('#Editbutton' + tableId).prop('disabled', true);
                var rowcollection = $("#" + tableId + " .call-checkbox");
                if ($("#" + tableId + "cb-all"))
                    $("#" + tableId + "cb-all").prop('disabled', true);
                rowcollection.each(function (index, elem) {
                    $(elem).prop('disabled', true);
                });
                this.deselectAll();
            }

            // This function enable everything the grid 
            this.enableAll = function () {
                if ($('#button' + tableId))
                    $('#button' + tableId).prop('disabled', false);
                if ($('#Addbutton' + tableId))
                    $('#Addbutton' + tableId).prop('disabled', false);
                if ($('#Editbutton' + tableId))
                    $('#Editbutton' + tableId).prop('disabled', false);
                var rowcollection = $("#" + tableId + " .call-checkbox");
                if ($("#" + tableId + "cb-all"))
                    $("#" + tableId + "cb-all").prop('disabled', false);
                rowcollection.each(function (index, elem) {
                    $(elem).prop('disabled', false);
                });
            }
            // This function disable Delete button on the grid 
            this.disableDeleteButton = function () {
                $('#button' + tableId).prop('disabled', 'disabled');
            }
            // This function enable Delete button on the grid 
            this.enableDeleteButton = function () {
                $('#button' + tableId).prop('disabled', '');
            }

            $('#' + tableId + ' thead tr').blur();

            var clickItemCallbackPointer = this.clickItemCallBack;

            $('#' + tableId + ' tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');

                }
                else {
                    $('#' + tableId).DataTable().$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }

                clickItemCallbackPointer($('#' + tableId).DataTable().row(this).data());
            });

            var autoDeleteConfirm = this.autoDeleteConfirm;

            $('#button' + tableId).click(function () {
                _gridActionType = 3;
                var resultDeleteMessage = ''
                var numberChecked = 0;
                var noDeleteConfirm = $('#button' + tableId).data('nodeleteconfirm');
                var rowcollection = $("#" + tableId + " .call-checkbox");
                rowcollection.each(function (index, elem) {
                    if ($(elem).prop('checked'))
                        numberChecked++;
                });
                if (numberChecked == 1)
                    resultDeleteMessage = deleteMessage;
                else
                    if (deleteMultiMessage && deleteMultiMessage != '')
                        resultDeleteMessage = deleteMultiMessage;
                    else
                        resultDeleteMessage = deleteMessage;
                if (autoDeleteConfirm !== false) {
                    if (noDeleteConfirm)
                        deleteRecord();
                    else {
                        var title = deleteConfirmMessage ? deleteConfirmMessage : resources.DeleteConfirmMessage;
                        pageTitleText = _pageTitleText;
                        if ((!pageTitleText || pageTitleText == '') && $("#main-tabs a.active") && $("#main-tabs a.active").text())
                            pageTitleText = $("#main-tabs a.active").text().trim().replace(/\s*\(.*?\)\s*/g, '');
                        title += _elementFromTxt + pageTitleText;
                        mConfirm((resultDeleteMessage && resultDeleteMessage != '') ? resultDeleteMessage : resources.DeleteMessage, deleteRecord, title);
                    }
                }
                else
                    if (this.deleteMultiItems) {
                        funcDeleteItemsCallBack();
                    } else {

                        funcDeleteItemCallBack();

                    }
                _gridActionType = 0;
            });


            if (this.deleteItemCallBack) {

                var funcDeleteItemCallBack = this.deleteItemCallBack;

                function deleteRecord() {
                    if (isCheckBoxEmplemented) {
                        funcDeleteItemCallBack(getCheckBoxValus().split(','));
                    }
                    else
                        funcDeleteItemCallBack($('#' + tableId + '').DataTable().row('.selected').data()[0]);
                }
            }


            function mConfirm(message, callBack, title) {

                showConfirm2(message, resources.Yes, resources.No, callBack, null, title);
            }




            if (this.deleteItemsCallBack) {
                var funcDeleteItemsCallBack = this.deleteItemsCallBack;

                function deleteRecords() {
                    funcDeleteItemsCallBack();
                }
            }




            $('#checkAll').click(function () {
                var oTable = $('#' + tableId).dataTable();
                var rowcollection = $("#" + tableId + " .call-checkbox");
                var all = $('#checkAll').prop('checked');
                rowcollection.each(function (index, elem) {
                    $(elem).prop('checked', all);
                })
            });


            if (this.addItemCallBack) {

                var funcAddItemCallBack = this.addItemCallBack

                $('#Addbutton' + tableId).click(function () {
                    $('#Addbutton' + tableId).blur();
                    _gridActionType = 1;
                    funcAddItemCallBack();
                    _gridActionType = 0;
                });
            }
            if (this.editItemCallBack) {
                var funcEditItemCallBack = this.editItemCallBack;
                var atts = this.attributes;
                $('#Editbutton' + tableId).click(function () {
                    var id = $("#" + tableId + " .call-checkbox:checked").val();
                    $('#' + tableId).dataTable().$('input[type=checkbox][value="' + id + '"]').parent().parent().addClass('check-active');
                    var RowData = $('#' + tableId).DataTable().row('.check-active').data();
                    $('#' + tableId).dataTable().$('input[type=checkbox][value="' + id + '"]').parent().parent().removeClass('check-active');
                    if ((!id || id == '') && !RowData) {
                        _gridActionType = 2;
                        funcEditItemCallBack();
                        _gridActionType = 0;
                    }
                    else {
                        var objectDataRow = {};
                        for (var i = 0; i < atts.length; i++)
                            objectDataRow[atts[i].Name] = RowData[i];
                        _gridActionType = 2;
                        funcEditItemCallBack(objectDataRow);
                        _gridActionType = 0;
                    }
                });
            }
        }
    }


    function updateRow(resultObject) {
        var id = getCheckBoxValus();
        var data = [];
        for (var i = 0; i < this.attributes.length; i++) {
            data[i] = resultObject[this.attributes[i].Name];
        }

        $('#' + tableId).dataTable().$('input[type=checkbox][value="' + id + '"]').parent().parent().addClass('check-active');
        $('#' + tableId).DataTable().row('.check-active').data(data);
        $('#' + tableId).dataTable().$('input[type=checkbox][value="' + id + '"]').parent().parent().removeClass('check-active');

        releaseButtons();
    }
    function releaseButtons() {
        if (isCheckBoxEmplemented) {
            $('#Editbutton' + tableId).attr('disabled', 'disabled');
            $('#Editbutton' + tableId).removeClass('btn-add-active');
            $('#button' + tableId).attr('disabled', 'disabled');
            $('#button' + tableId).removeClass('btn-delete-active');
            $('#' + tableId + 'cb-all').prop('checked', false);

        }
    }
    this.updateItem = updateRow;

    function insertRow(resultObject) {

        var data = [];
        for (var i = 0; i < this.attributes.length; i++) {
            data[i] = resultObject[this.attributes[i].Name];
        }
        var oTable = $('#' + tableId).dataTable();
        oTable.fnAddData(data);
        releaseButtons();
    }

    this.appendRow = function (row) {
        $('#' + tableId).dataTable().fnAddData(row);
    }

    this.addItem = insertRow;

    //function removeRow() {

    //    $('#' + tableId).dataTable().$('tr.selected').remove();
    //    var rows = $('#' + tableId + '_info').html().split(' ');
    //    if (rows[3] == rows[5]) {
    //        rows[3] = parseInt(rows[3]) - 1;
    //    }
    //    rows[5] = parseInt(rows[5]) - 1;
    //    $('#' + tableId + '_info').html(rows.join(' '));
    //}

    function removeRow(id) {
        if (!id)
            var id = getCheckBoxValus().split(',');
        if (isCheckBoxEmplemented && id.length != 36) {

            for (var i = 0; i < id.length; i++) {
                if (_isServerGrid)
                    $('#' + tableId).dataTable().$('input[type=checkbox][value="' + id[i] + '"]').parent().parent().remove();
                else {
                    $('#' + tableId).dataTable().$('input[type=checkbox][value="' + id[i] + '"]').parent().parent().addClass('check-active');
                    $('#' + tableId).DataTable().row('.check-active').remove().draw(false);
                }
                var rows = $('#' + tableId + '_info').html().split(' ');
                if (rows[3] == rows[5]) {
                    rows[3] = parseInt(rows[3]) - 1;
                }
                rows[5] = parseInt(rows[5]) - 1;
                if ($.isNumeric(rows[3]) && $.isNumeric(rows[5]))
                    $('#' + tableId + '_info').html(rows.join(' '));
                else
                    $('#' + tableId + '_info').html('');
            }
        }
        else {
            if (_isServerGrid)
                $('#' + tableId).dataTable().$('input[type=checkbox][value="' + id + '"]').parent().parent().remove();
            else {
                $('#' + tableId).dataTable().$('input[type=checkbox][value="' + id + '"]').parent().parent().addClass('check-active');
                $('#' + tableId).DataTable().row('.check-active').remove().draw(false);
            }
        }
        releaseButtons();

        for (var i = 0; i < _selectedObjects.length; i++)
            if (_selectedObjects[i].id == id)
                _selectedObjects.splice(i, 1);


        _selectedItems = [];
        _selectedIds = [];
    }

    this.removeItem = removeRow;
    this.removeAllItems = function () {
        $('#' + tableId).DataTable().clear().draw();
    }
    this.hideSelectOptions = function () {
        if ($("#" + tableId + " .checkbox"))
            $("#" + tableId + " .checkbox").addClass('hidden');
        if ($('input[name="' + tableId + '-radiobutton"]'))
            $('input[name="' + tableId + '-radiobutton"]').addClass('hidden');
    }
    this.showSelectOptions = function () {
        if ($("#" + tableId + " .checkbox"))
            $("#" + tableId + " .checkbox").removeClass('hidden');
        if ($('input[name="' + tableId + '-radiobutton"]'))
            $('input[name="' + tableId + '-radiobutton"]').removeClass('hidden');
    }
    function getCheckBoxValus() {
        if (_isServerGrid)
            return _selectedIds.join();
        var selectedGroups = new Array();
        var oTable = $('#' + tableId).dataTable();
        var rowcollection = $("#" + tableId + " .call-checkbox");
        rowcollection.each(function (index, elem) {
            if ($(elem).prop('checked'))
                selectedGroups[$(elem).val()] = 1;
            else
                delete selectedGroups[$(elem).val()];
        });
        return Object.keys(selectedGroups).join();
    }

    function getUnCheckedCheckBoxValus() {
        if (_isServerGrid)
            return _selectedIds.join();
        var selectedGroups = new Array();
        var oTable = $('#' + tableId).dataTable();
        var rowcollection = $("#" + tableId + " .call-checkbox");
        rowcollection.each(function (index, elem) {
            if ($(elem).prop('checked'))
                delete selectedGroups[$(elem).val()];
            else
                selectedGroups[$(elem).val()] = 1;
        });
        return Object.keys(selectedGroups).join();
    }

    this.getUnCheckedCheckBoxValus = getUnCheckedCheckBoxValus;

    this.getCheckBoxValus = getCheckBoxValus;

    this.checkEditEnable = checkEditEnableInt;


    function checkEditEnableInt(disableDelete, selector) {

        var numberChecked = 0;
        var oTable = $('#' + tableId).dataTable();
        var rowcollection = $("#" + tableId + " .call-checkbox");
        var count = 0;
        rowcollection.each(function (index, elem) {
            count++;
            if ($(elem).prop('checked') && $(elem).val() == _emptyGuidTxt) {
                //showSide('Error', _canNotSelectThisItem);
                $(elem).prop('checked', false);
            }
            if ($(elem).prop('checked'))
                numberChecked++;
        });
        if (count == numberChecked)
            $('#' + tableId + 'cb-all').prop('checked', true);
        if (numberChecked == 1) {
            $('#Editbutton' + tableId).removeAttr('disabled');
            $('#Editbutton' + tableId).addClass('btn-add-active');
        }
        else {
            if (disableDelete && numberChecked != 0) {
                this.deselectAll();
                $(selector).prop('checked', true);
                $('#Editbutton' + tableId).removeAttr('disabled');
                $('#Editbutton' + tableId).addClass('btn-add-active');
            }
            else {
                $('#Editbutton' + tableId).attr('disabled', 'disabled');
                $('#Editbutton' + tableId).removeClass('btn-add-active');
            }
        }

    }

    this.checkDeleteEnable = checkDeleteEnableInt;

    function addSortTitles(resources) {
        $('th.sorting_asc').attr('title', resources.SortDescinding);
        $('th.sorting').attr('title', resources.Sorting);
        $('th.sorting_desc').attr('title', resources.SortAscinding);
    }

    function checkDeleteEnableInt() {
        var oTable = $('#' + tableId).dataTable();
        var rowcollection = $("#" + tableId + " .call-checkbox");

        rowcollection.each(function (index, elem) {
            if ($(elem).val() == _emptyGuidTxt)
                $(elem).prop('checked', false);
        });
        if ($("#" + tableId + " .call-checkbox:checked").length > 0) {
            $('#button' + tableId).removeAttr('disabled');
            $('#button' + tableId).addClass('btn-delete-active');
        }
        else {
            $('#button' + tableId).attr('disabled', 'disabled');
            $('#button' + tableId).removeClass('btn-delete-active');
        }
    }

    this.getCheckBoxObjects = function (withFlag) {
        if (_isServerGrid) {
            var selectedObjects = new Array();
            for (var i = 0; i < _selectedItems.length; i++) {
                var object = {};
                object.id = _selectedItems[i][0];
                object.text = _selectedItems[i][2];
                if (withFlag)
                    object.AttachmentFlag = _selectedItems[i][3];

                selectedObjects.push(object);
            }

            $('#' + tableId + "cb-all").prop('checked', '');
            return removeDuplicates(selectedObjects, "id");
        }
        if (this.autoHandleCheckChange) {
            var result = _selectedObjects.slice(0);
            if (_isServerGrid) {
                _selectedObjects = new Array();
                $('#' + tableId + "cb-all").prop('checked', '');
            }
            return removeDuplicates(result, "id");
        }
        else {
            var selectedObjects = new Array();
            var oTable = $('#' + tableId).dataTable();
            var rowcollection = $("#" + tableId + " .call-checkbox");
            rowcollection.each(function (index, elem) {
                if ($(elem).prop('checked')) {
                    var object = {};
                    object.id = $(elem).val();
                    object.text = $('#' + tableId).DataTable().row(index).data()[2];
                    if (withFlag)
                        object.AttachmentFlag = $('#' + tableId).DataTable().row(index).data()[3];
                    selectedObjects.push(object);
                }

            });
            $('#' + tableId + "cb-all").prop('checked', '');
            return removeDuplicates(selectedObjects, "id");
        }
    }

    this.getRadioButtonSelected = function () {
        var model = {};
        debugger; 
        var selected = $('input[name="' + tableId + '-radiobutton"]:checked');
        if (selected.length > 0) {
            var id = selected.val();
            var text = selected.parent().next()[0].innerText;
            model.id = id;
            model.text = text;
        }
        return model;
    };


    this.getCheckBoxFullObjects = function () {
        if (_isServerGrid)
            return _selectedItems;

        var selectedObjects = new Array();
        var oTable = $('#' + tableId).dataTable();
        var rowcollection = $("#" + tableId + " .call-checkbox");
        rowcollection.each(function (index, elem) {
            debugger;
            if ($(elem).prop('checked')) {
                var object = $('#' + tableId).DataTable().row($(this).parents("tr")[0]).data();

                selectedObjects.push(object);
            }
        });
        return selectedObjects;
    }

    //Returns Table paging info object {end: 2, length: 10, page: 0, pages: 1, recordsDisplay: 2, recordsTotal: 2, start: 0}
    this.getTablePagingInfo = function () {
        return $('#' + tableId).DataTable().page.info();
    }
    this.getTableDataRows = function (processRow) {
        var records = $('#' + tableId + ' tbody tr');
        var recordsInfo = [];
        if (records.length > 0) {
            for (var i = 0; i < records.length ; i++) {
                var rowData = $('#' + tableId).DataTable().row(i).data();
                if (rowData) {
                    var rowDataArray = [];
                    for (var j = 0; j < rowData.length; j++) {
                        if (processRow)
                            rowDataArray.push(processRow(rowData[j], j));
                        else
                            rowDataArray.push(rowData[j]);
                    }
                    recordsInfo.push(rowDataArray);
                }
            }
        }
        return recordsInfo;
    }

    this.getDataTableFullObjects = function () {
        var selectedObjects = new Array();
        var rowcollection = $("#" + tableId + " .call-checkbox");
        rowcollection.each(function (index, elem) {
            var object = $('#' + tableId).DataTable().row(index).data();
            selectedObjects.push(object);
        });
        return selectedObjects;
    }

    this.getTableData = function () {
        var selectedObjects = new Array();
        var rowsCount = this.getTablePagingInfo().length;
        for (var i = 0; i < rowsCount; i++) {
            var object = $('#' + tableId).DataTable().row(i).data();
            if (object)
                selectedObjects.push(object);
        }
        return selectedObjects;
    }

    this.selectAll = function (execludeIds) {

        if (!execludeIds && $("#" + tableId + "cb-all"))
            $("#" + tableId + "cb-all").prop('checked', true);
        var rowcollection = $("#" + tableId + " .call-checkbox");
        rowcollection.each(function (index, elem) {
            if (!execludeIds || execludeIds.indexOf($(elem).val()) == -1) {
                if (!$(elem).prop('checked'))
                    $(elem).prop('checked', true).change();

                var item = $('#' + tableId).DataTable().row(index).data();

                var object = {};
                object.id = $(elem).val();
                object.text = item[2];

                //_selectedItems.push(item);
                //_selectedIds.push(item[0]);


                if (this.withFlag)
                    object.SecurityDegree = $('#' + tableId).DataTable().row(index).data()[3];
                _selectedObjects.push(object);
            }
        });
    }

    this.deselectAll = function (deSelctAllRecords) {
        debugger;
        if ($("#" + tableId + "cb-all"))
            $("#" + tableId + "cb-all").prop('checked', false);

        var rowcollection = $("#" + tableId + " .call-checkbox");
        rowcollection.each(function (index, elem) {
            if ($(elem).prop('checked'))
                $(elem).prop('checked', false).change();
            for (var i = 0; i < _selectedObjects.length; i++)
                if (_selectedObjects[i].id === $(elem).val())
                    _selectedObjects.splice(i, 1);
        });
        if (this._EnabledeselectAll === true || (deSelctAllRecords) ) {
           _selectedIds = [];
           _selectedItems = [];
           _selectedObjects = [];
        }  
    }
    //this.EnableDeselectAllrows = function (enable) {
    //    debugger;
    //    if (!enable)
    //        this._EnabledeselectAll = enable;

    //}
    function getLink(columnData, id) {
        var data = columnData;
        data = '<a href=./View/' + id + ' >' + columnData + '</a>';
        return data;
    }



    function getCheckbox(columnData, id) {
        //var data = '<input id="' + id + '" class="call-checkbox" ' + (columnData == "true" ? ' checked="checked" ' : '') + ' type="checkbox" value="' + id + '" />';
        var data = '<input class="call-checkbox"  id="' + id + tableId + '" type="checkbox" ' + (columnData == "true" ? ' checked="checked" ' : '') + ' type="checkbox" value="' + id + '"/>';
        if (id == _emptyGuidTxt)
            data += '<label class="not-allowed not-selected-checkbox checkbox" for="' + id + tableId + '">  </label>';
        else
            data += '<label class="checkbox" for="' + id + tableId + '">  </label>';
        return data;
    }
    function getCheckboxDefault(columnData, id, isDefined) {
        if (!isDefined)
            return columnData;
        //var data = '<input id="' + id + '" class="call-checkbox" ' + (columnData == "true" ? ' checked="checked" ' : '') + ' type="checkbox" value="' + id + '" />';
        var data = '<input class="call-checkbox"  id="' + id + tableId + '" type="checkbox" value="' + id + '"/>' +
                    '<label class="checkbox" for="' + id + tableId + '">  </label>';
        return data;
    }
    function getRadioButton(columnData, id) {
        ////
        var data = '<input class="call-radiobutton" name="';
        if (id == _emptyGuidTxt|| id=='0')
            data = '<input class="call-radiobutton not-allowed low-opacity" name="';
        data += tableId + '-radiobutton"  id="' + id + tableId + '" type="radio" ' + (columnData == "true" ? ' checked="checked" ' : '') + ' value="' + id + '"/>' +
                   '<label class="" for="' + id + tableId + '">  </label>';
        return data;
    }

    function setCheckedIds(array) {
        if (array != null)
            checkedIds = array;
    }


}