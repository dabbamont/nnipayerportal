jQuery.fn.extend({
    'lightbox': function (overElement, width, height, background, callback) {

        $('#lightbox-overlay').show();

        var backgrounds = {
            'item': "url('/Content/Images/item-lightbox-bg.png')",
            'preferences': "url('/Content/Images/user-preferences-lightbox-bg.png')",
            'privacy': "url('/Content/Images/privacy-lightbox-bg.png')"
        };

        var box = $(this).clone();
        var useWindow = false;

        lightbox.lastz++;

        if (overElement == null) {
            useWindow = true;
            overElement = window;
        }

        $(box).appendTo($('body')).show();

        var closeButton = $(box).find('a:eq(0)');

        closeButton.click(function (e) {
            e.preventDefault();
            // Hide the containing lightbox
            $(this).closest('div.lightbox').remove();
            $('#lightbox-overlay').hide();
        });

        var top = 0, left = 0;

        if (useWindow === false) {
            top = $(overElement).offset().top;
            left = $(overElement).offset().left;
        }

        // Position lightbox
        // Don't change this without checking in all browsers because it's wacky

        $(box).css('position', 'absolute');

        $(box).css({
            'position': 'absolute',
            'background-image': backgrounds[background],
            'max-height': height + 'px',
            'max-width': (width - 29 - 29) + 'px'
        }).width(width).height(height);

        $(box).css({
            'top': (top + ($(overElement).height() / 2) - ($(box).height() / 2)),
            'left': (left + ($(overElement).width() / 2) - ($(box).width() / 2)),
            'z-index': '15'
        });

        $(box).find('div.lightbox-submit').css("top", $(box).height() - 42)
        .width($(box).outerWidth() - 29 - 29);

        if (callback !== null) {
            callback($(box));
        }

        return $(box);

    },
    'closeLightbox': function () {
        $(this).closest('div.lightbox').remove();
        $('#lightbox-overlay').hide();
    },
    'doError': function () {
        $(this).noError().text($(this).text() + ' *').addClass('error');
        return $(this);
    },
    'noError': function () {
        $(this).removeClass('error').text($(this).text().replace('*', ''));
        return $(this);
    },
    'doSubmit': function (data, callback) {
        var form = $(this);

        $.ajax({
            url: $(this).attr('action'),
            type: "POST",
            data: JSON.stringify(data),
            processData: false,
            contentType: 'application/json; charset= utf-8',
            success: function (result) {
                callback(form, result);
            }
        });
        return $(this);
    },
    'makeAjax': function (dataFunction, callback) {

        $(this).submit(function (e) {

            var data = dataFunction($(this));
            console.log(data);
            $(this).doSubmit(data, function (form, result) {
                console.log(result);
                callback(form, result);
            });
            return false;
        });
        return $(this);
    },
    "getVal": function (elName) {
        var el = $(this).find('[name=' + elName + ']');

        if (el.attr('type') == 'checkbox' || el.attr('type') == 'radio') {
            return (el.attr('checked') == 'checked');
        }
        return $(this).find('[name=' + elName + ']').val();
    },
    "setVal": function (elId, value) {
        $(this).find('#' + elId).text(value);
        return $(this);
    },
    "showError": function (errorMessage) {
        $(this).find('#errorText').text(errorMessage).show();
        return $(this);
    },
    "highlightErrors": function (errorMessage, errorFields) {
        for (var i in errorFields) {
            $(this).find('#label' + errorFields[i]).doError();
            $(this).showError(errorMessage);
        }
        return $(this);
    }
});

// Request access link
    $('#request-access').click(function () {
        //lightbox($('#lightbox-request-access'));
        lightbox($('#lightbox-request-access'), null, 399, 415, 'item');

        $('#lightbox-request-access').lightbox(null, 399, 415, 'item', function (box) {
            box.find('form').submit(function (e) {
                e.preventDefault();

                var data = {
                    FirstName: box.find('#ra-FirstName').val(),
                    LastName: box.find('#ra-LastName').val(),
                    Email: box.find('#ra-Email').val(),
                    Phone: box.find('#ra-Phone').val(),
                    Organization: box.find('#ra-Organization').val(),
                    Error: ''
                };

                $(this).doSubmit(data, function(result) {
                    if (result.Error != "") {
                        box.showError(result.Error);
                    }
                    else {
                        lightboxConfirm('Message here');
                    }
                });
            });

        });
    });