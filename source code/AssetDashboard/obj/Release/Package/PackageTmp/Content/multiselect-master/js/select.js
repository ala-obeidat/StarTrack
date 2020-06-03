var selectMultiple;
$(document).ready(function () {
    if (document.getElementsByClassName('slim-select').length) {
        selectMultiple = new SlimSelect({
            select: '.slim-select',
            closeOnSelect: false,
            hideSelectedOption: true,
            allowDeselect: true,
            deselectLabel: '<span title="clear" class="red">✖</span>',
            onChange: selectMultipleChange
        });
    }
});
function getMulitSelected() {
    return selectMultiple.selected();
}