// jQuery ajax használatával datatable feltöltés
$(document).ready(function () {
    $("#datatable").dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/home/GetList",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": 'addressId', "name": 'AddressId', "autoWidth": true },
            { "data": 'addressLine1', "name": 'AddressLine1', "autoWidth": true },
            { "data": 'addressLine2', "name": 'AddressLine2', "autoWidth": true },
            { "data": 'city', "name": 'City', "autoWidth": true },
            { "data": 'stateProvinceId', "name": 'StateProvinceID', "autoWidth": true },
            { "data": 'postalCode', "name": 'PostalCode', "autoWidth": true },
            { "data": 'rowguid', "name": 'rowguid', "autoWidth": true },
            { "data": 'modifiedDate', "name": 'ModifiedDate', "autoWidth": true }
        ]
    });
});

//https://codewithmukesh.com/blog/jquery-datatable-in-aspnet-core/