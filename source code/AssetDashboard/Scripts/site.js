$(document).ready(function () {

    $.ajax({
        url: "Locations/ListLocations",
        success: function (result) {
            for (var i = 0; i < result.Data.length; i++) {
                var location = result.Data[i];
                var section = document.createElement('SECTION');
                $(section).addClass('locaion-item');
                var header = document.createElement('HEADER');
                $(header).html(location.Name);
                $(section).append(header);
                var bodyDiv = document.createElement('DIV');
                var allDiv = document.createElement('DIV');
                var inDiv = document.createElement('DIV');
                var outDiv = document.createElement('DIV');
                var missingDiv = document.createElement('DIV');
                $(allDiv).html('<span>ALL</span> <br /> <span>' + location.TotalAsset+'</span>');
                $(inDiv).html('<span>IN</span> <br /> <span>' + location.InAsset+'</span>');
                $(outDiv).html('<span>OUT</span> <br /> <span>' + location.OutAsset + '</span>');
                $(missingDiv).html('<span>MISSING</span> <br /> <span>0</span>');
                $(bodyDiv).append(allDiv);
                $(bodyDiv).append(inDiv);
                $(bodyDiv).append(outDiv);
                $(bodyDiv).append(missingDiv);
                $("#locations").append(section);
                $("#locations").append(bodyDiv);
            } 
        }
    });
});
 