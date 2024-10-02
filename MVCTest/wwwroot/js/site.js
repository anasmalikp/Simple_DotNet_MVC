// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    var placeholderElement = $('#placeholderHere');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        console.log(url);
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        })
    })

    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        console.log(actionUrl);
        var sentData = form.serialize();
        $.post(actionUrl, sentData).done(function (data) {
            placeholderElement.find('.modal').modal('hide');

            location.reload();
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error: " + textStatus, errorThrown);
        })
    })

    placeholderElement.on('click', '[data-dismiss="modal"]', function () {
        placeholderElement.find('.modal').modal('hide');
    })
})  

$(function () {
    $('button[data-target="deleteUser"]').click(function (event) {
        var url = $(this).data('url');
        $.post(url).done(function () {
            location.reload();
        })
    })
})