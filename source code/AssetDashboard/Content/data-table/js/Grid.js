function addDataTable(id, model) {
    var gridHtml = '<div class="center-div"><div>';
    if (model.IsAddEnabled || model.IsEditEnabled || model.IsDeleteEnabled) {

        gridHtml += "<div>";

        if (model.IsAddEnabled)
            gridHtml += '<input type="submit" class="btn-add left-float" id="Addbutton' + id + '" value="' + (model.AddText ? model.AddText : _AddNewText) + '"' + (model.AddMaxWidth ? ('style="max-width:' + model.AddMaxWidth + 'px;"') : '') + '/>';

        if (model.IsEditEnabled) {
            gridHtml += '<input type="submit" class="btn-edit btn-add-active left-float" id="Editbutton' + id + '" value="' + (model.EditText ? model.EditText : _EditText) + '" />';
        }


        if (model.IsDeleteEnabled)
            gridHtml += '<input type="submit" class="btn-delete btn-delete-active right-float" ' + (model.NoDeleteConfirm ? 'data-nodeleteconfirm="true"' : '') + ' value="' + (model.DeleteText ? model.DeleteText : _DeleteText) + '" id="button' + id + '" />';

        gridHtml += "</div><br />";

    }

    gridHtml += '<div class="center-table"><table id="' + id + '" class="display table-responsive mGrid"><thead><tr>';

    //////////////////////
    if (model.MultiSelected) {
        var checkBoxObject = {};
        checkBoxObject.Name = 'checkbox';
        checkBoxObject.Text = '<input  id="' + id + 'cb-all" type="checkbox" /><label class="checkbox" for="' + id + 'cb-all"></label>';
        checkBoxObject.IsCheckbox = true;
        model.Attributes.splice(1, 0, checkBoxObject);
    }
    ////////////////////////
    var attributes = model.Attributes;


    for (var i = 0; i < attributes.length; i++) {
        if (attributes[i].Title)
            gridHtml += '<th title="' + attributes[i].Title + '">' + attributes[i].Text + '</th>';
        else
            gridHtml += '<th>' + attributes[i].Text + '</th>';

    }

    gridHtml += '</tr></thead><tbody></tbody></table></div></div>';

    $("#" + id).replaceWith(gridHtml);

    var dataTable = new DataTable(id, model.IsServer, model.MultiSelected, model.DeleteMessage, model.DeleteMultiMessage, model.DeleteConfirmMessage, model.CanChangeLength);
    dataTable.attributes = model.Attributes;
    if (model.MultiSelected) {
        $('#' + id + 'cb-all').on('click', function () {
            $('#button' + id).attr('disabled', 'disabled');
            $('#button' + id).removeClass('btn-add-active');
            if ($('#' + id + 'cb-all').prop('checked')) {
                dataTable.selectAll();
                dataTable.checkDeleteEnable();
            }
            else {
                dataTable.deselectAll();
                dataTable.checkDeleteEnable();
            }
            $('#Editbutton' + id).attr('disabled', 'disabled');
            $('#Editbutton' + id).removeClass('btn-add-active');
        });
    }
    if (model.IsEditEnabled || model.IsDeleteEnabled)
        $('#' + id).delegate(".call-checkbox", "click", function () {
            if (model.MultiSelected)
                $('#' + id + 'cb-all').prop('checked', false);
            dataTable.checkEditEnable(!model.IsDeleteEnabled, this);
            dataTable.checkDeleteEnable();
        });
    if (model.IsEditEnabled) {
        $('#Editbutton' + id).attr('disabled', 'disabled');
        $('#Editbutton' + id).removeClass('btn-add-active');

    }
    if (model.IsDeleteEnabled) {
        $('#button' + id).attr('disabled', 'disabled');
        $('#button' + id).removeClass('btn-delete-active');
    }
    dataTable.SearchSettings = {};
    return dataTable;
}
