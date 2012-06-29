var console = console || {
    log: function (thing) {

    }
}

var urlData = [];
(function () {
    var parts = document.location.href.split("/");
    for (var i = 3; i < parts.length; i++) {
        urlData[i - 3] = parts[i];
    }
})();

/*
 *  Initialize
 */
$(document).ready(function () {

    test();

    $('#lightbox-overlay').hide();

    $('a[href=#').live({ 'click': function (e) {
            e.preventDefault();
        }
    });

    $.getJSON("/Resource/TopPicks", function (result) {
        for (var i in result) {
            $('#top-picks a:eq(' + i + ')').attr('ResourceId', result[i]['ResourceId']);
            $('#top-picks a:eq(' + i + ')').text(result[i]['Title']);
        }
        // Item Lightbox click
        $('#top-picks a').click(function () {
            itemLightbox($(this).attr('ResourceId'));
        });
    });

    rolloverNavigation();

    $('input[type=checkbox]').imageCheck(
        '/Content/Images/input-checkbox-2.png',
        '/Content/Images/input-checkbox-2-checked.png',
        22,
        20
    );

    $('input[type=radio]').imageCheck(
        '/Content/Images/input-radio.png',
        '/Content/Images/input-radio-checked.png',
        20,
        19
    );

    $('div.lightbox').hide();

    $('div.lightbox-button-text a').live({
        'click': function () {
            lightbox($('#request-item'), $('#library-items'), 399, 415, 'item');
        }
    });

    $('#ncqa-link a').live({
        'click': function (e) {
            e.preventDefault();
            lightbox($('#external-link-lightbox'), null, 399, 150, 'item');

            $('#external-link-lightbox').lightbox(null, 399, 150, 'item', function (box) {

            });
        }
    });

    preferencesLightbox();

    $('div.lightbox-submit a, #footer-privacy').live({ 'click': function () {
        pLightbox($('div#privacy.lightbox'), $('#content'), 1122, 417, 'privacy');
    }
    });

});

function test() {
    $('#video-player-volume-slider').hide();
    $('#video-player-button-volume').mouseover(function () {
        $('#video-player-volume-slider').show();
    });

    $('#video-player-volume-slider').mouseleave(function () {
        $('#video-player-volume-slider').hide();
    });

    if (urlData[1] == 'MyLibrary') {
        $('div.lightbox-button-text a:eq(1)').text('Remove From My Library');
    }

    if (urlData[1] == 'Video') {
        $('#ferrisblock').height(20);
    }
}

function itemLightbox(itemId) {
    $('#item').lightbox(null, $('#library-items'), 399, 415, 'item', function (box) {
        $.getJSON("/Resource/MyLibraryItem/?resourceId=" + itemId, function (result) {
            box.setVal('item-lightbox-section', result.Category);
            box.find('#item-lightbox-image img').attr('src', result.ThumbnailUrl);
            box.setVal('item-lightbox-title', result.Title);
            box.setVal('item-lightbox-description', result.Description);
            box.find('#resourceId').val(result.ResourceId);

            box.find('.lightbox-button-text a:eq(0)').click(function () {
                window.open( result.ResourceUrl );
            });

            box.find('.lightbox-button-text a:eq(1)').click(function () {

            });

            box.find('.lightbox-button-text a:eq(2)').click(function () {
                itemRequestLightbox($(this).closest('.lightbox').find('#ResourceId').val());
            });
        });
    });
}

function itemRequestLightbox() {
    $('#request-item').lightbox(null, 399, 514, 'item', function (box) {

        $(box).find('form').makeAjax(
            function (form) {
                return {
                    "ResourceId": form.getVal('ResourceId'),
                    "OrderResource": form.getVal("OrderResource"),
                    "RequestMeeting": form.getVal("RequestMeeting"),
                    "UseEmail": form.getVal("UseEmail"),
                    "Email": form.getVal("Email"),
                    "UsePhone": form.getVal("UsePhone"),
                    "Phone": form.getVal("Phone1") + form.getVal("Phone2") + form.getVal("Phone3") ,
                    "ContactTime": form.getVal("ContactTime"),
                    "Error": "",
                    "ErrorFields": []
                };
            },
            function (form, result) {

                $(form).closeLightbox();
            }
        );

    });
}

function preferencesLightbox() {
        $('#preferences-link').click(function () {
            $('#preferences').lightbox(null, 399, 514, 'preferences', function (box) {

            $(box).find('form').makeAjax(
                function (form) {
                    return {
                        "OldPassword": form.getVal("OldPassword"),
                        "NewPassword": form.getVal("NewPassword"),
                        "ConfirmNewPassword": form.getVal("ConfirmNewPassword"),
                        "OldEmail": form.getVal("OldEmail"),
                        "NewEmail": form.getVal("NewEmail"),
                        "ConfirmNewEmail": form.getVal("ConfirmNewEmail"),
                        "ChangeEmail": form.getVal("ChangeEmail"),
                        "ChangePassword": form.getVal("ChangePassword")
                    };
                },
                function (form, result) {
                    console.log(result);
                    $(form).closeLightbox();
                }
            );

        });
    });
}


/*
*  Initialize the navigation rollovers
*  @TODO can probably just use CSS for this, div:hover background-image
*/
function rolloverNavigation() {
    $('#navigation div img').hide();

    var divName = urlData[0];

    if (divName == 'Resource' ) {
        if (urlData[1] == 'MyLibrary') {
            divName = 'Resource-MyLibrary';
        } else if (urlData[1] == 'Video') {
            divName = 'Resource-Video';
        }
    }
    
    console.log(divName);

    console.log($('#navigation div[name=' + divName + '] img'));
    $('#navigation div[name=' + divName + '] img').show();

    $("#navigation div[name!=" + divName + "]").unbind()
    .hover(function () {
        //console.log( $(this).closest('div').attr('name') + "/" + urlData[0] );
        
        $('#navigation div[name!=' + divName + '] img').hide();

        $(this).find('img').show();
        
        
    }).mouseleave(function() {
        $(this).find('img').hide();
        $('#navigation div[name=' + divName + '] img').show();
    });
}

function lightbox(element, overElement, width, height, background) {

    $('#lightbox-overlay').show();

    var backgrounds = {
        'item': "url('/Content/Images/item-lightbox-bg.png')",
        'preferences': "url('/Content/Images/user-preferences-lightbox-bg.png')",
        'privacy': "url('/Content/Images/privacy-lightbox-bg.png')"
    };

    var box = $(element).clone();
    var useWindow = false;

    lightbox.lastz++;
    console.log(overElement);

    if (overElement == null) {
        useWindow = true;
        overElement = window;
        //overElement = $('#content');
    }


    console.log(overElement);

    if ( $(box).find('form').length > 0 ) {
        //doForm($(box).find('form'));
    }

    $(box).appendTo ($('body') ).show();

    var closeButton = $(box).find('a:eq(0)');

    closeButton.click(function (e) {
        e.preventDefault();
        // Hide the containing lightbox
        $(this).closest('div.lightbox').remove();
        $('#lightbox-overlay').hide();
    });

    var top = 0, left = 0;

    console.log("Use Window " + useWindow);

    if (useWindow === false) {
        /*top = $(overElement).offset().top + $(overElement).position().top;
        left = $(overElement).offset().left + $(overElement).position().left; */
        top = $(overElement).offset().top;
        left = $(overElement).offset().left;
    } else {

    }

    $(box).css('position', 'absolute');

    $(box).css({
        'position': 'absolute',
        //'top': ( top + $(overElement).height() / 2 ) - ( $(box).height() / 2 ),
        //'left': ( left + $(overElement).width() / 2) - ( $(box).height() / 2),
        
        'background-image': backgrounds[background],
        'max-height': height + 'px',
        'max-width': (width - 29 - 29) + 'px'
    }).width(width).height(height);

    $(box).css({
         'top': ( top + ($(overElement).height() / 2) - ($(box).height() / 2 ) ),
        'left': ( left + ($(overElement).width() / 2) - ( $(box).width() / 2 ) ),
        'z-index': '15'
    });

    $(box).find('div.lightbox-submit').css("top", $(box).height() - 42)
        .width( $(box).outerWidth() - 29 - 29 );

    return $(box);

}





function pLightbox(element, overElement, width, height, background) {

    var backgrounds = {
        'item': "url('/Content/Images/item-lightbox-bg.png')",
        'preferences': "url('/Content/Images/user-preferences-lightbox-bg.png')",
        'privacy': "url('/Content/Images/privacy-lightbox-bg.png')"
    };
    $('#lightbox-overlay').show();
    var box = $(element).clone();
    var useWindow = false;

    lightbox.lastz++;
    console.log(overElement);

    if (overElement == null) {
        useWindow = true;
        overElement = window;
        //overElement = $('#content');
    }


    console.log(overElement);

    if ($(box).find('form').length > 0) {
        //doForm($(box).find('form'));
    }

    $(box).appendTo($('#content')).show();

    var closeButton = $(box).find('a:eq(0)');

    closeButton.click(function (e) {
        e.preventDefault();
        // Hide the containing lightbox
        $(this).closest('div.lightbox').remove();
        $('#lightbox-overlay').hide();
    });

    var top = 0, left = 0;

    console.log("Use Window " + useWindow);

    if (useWindow === false) {
        /*top = $(overElement).offset().top + $(overElement).position().top;
        left = $(overElement).offset().left + $(overElement).position().left; */
        top = $(overElement).offset().top;
        left = $(overElement).offset().left;
    } else {

    }

    $(box).css('position', 'absolute');

    $(box).css({
        'position': 'absolute',
        //'top': ( top + $(overElement).height() / 2 ) - ( $(box).height() / 2 ),
        //'left': ( left + $(overElement).width() / 2) - ( $(box).height() / 2),

        'background-image': backgrounds[background],
        'max-height': height + 'px',
        'max-width': (width) + 'px'
    }).width(width).height(height);

    if (urlData[0] == 'Account') {

        $(box).css({
            'top': $('#image-rotation').offset().top + 'px;',
            'left': $('#image-rotation').offset().left + 'px;'
        });

    } else {

    }
    if (urlData[0] == 'Account') {

        $(box).css({
            'top': '156px',
            'left': '18px',
            'z-index': '100'
        });
    } else {
        $(box).css({
            'top': '20px',
            'left': '-40px',
            'z-index': '100'
        });
    }

    console.log({
        'top': $('#image-rotation').offset().top + 'px;',
        'left': $('#image-rotation').offset().left + 'px;'
    });

    //$(box).top($('#image-rotation').position().top);
    //$(box).left($('#image-rotation').position().left);

    $(box).find('div.lightbox-submit').css("top", $(box).height() - 42)
        .width($(box).outerWidth() - 29 - 29);

    if (urlData[0] != 'Account') {
    
        $(box).css('z-index', '150');

    }
}


jQuery.fn.imageCheck = function(imgUnchecked, imgChecked, width, height) {
    var wrap = '<div class="checkbox-wrap"></div>',
        image;
        
    $(this).wrap(wrap);
    
    $(this).each(function() {
        if ( $(this).prop('checked') ) {
            image = imgChecked;
        } else {
            image = imgUnchecked;
        }
        $(this).hide().closest('div.checkbox-wrap')
        
        .css('background-image', "url('" + image + "')")
        .css('width', width)
        .css('height', height)
        .click(function(e){
            var checkbox =  $(this).find('input');
            e.preventDefault();
            checkbox.prop( 'checked', !checkbox.prop('checked') );
            if ( checkbox.prop('checked') ) {
                image = imgChecked;
            } else {
                image = imgUnchecked;
            }
            $(this).css('background-image', "url('" + image  + "')");
        });
    });
    
    return $(this);
};


jQuery.fn.imageRadio = function(imgUnchecked, imgChecked, width, height) {
    var wrap = '<div class="checkbox-wrap"></div>',
        image;
        
    $(this).wrap(wrap);
    
    $(this).each(function() {
        if ( $(this).prop('checked') ) {
            image = imgChecked;
        } else {
            image = imgUnchecked;
        }
        $(this).hide().closest('div.checkbox-wrap')
        
        .css('background-image', "url('" + image + "')")
        .css('width', width)
        .css('height', height)
        .click(function(e){
            var checkbox =  $(this).find('input');
            console.log(checkbox);
            e.preventDefault();
            checkbox.prop( 'checked', !checkbox.prop('checked') );
            if ( checkbox.prop('checked') ) {
                image = imgChecked;
            } else {
                image = imgUnchecked;
            }
            $(this).css('background-image', "url('" + image  + "')");
        });
    });
    
    return $(this);
};

var getTextNodesIn = function (el) {
    return $(el).find(":not(iframe)").andSelf().contents().filter(function () {
        return this.nodeType == 3;
    });
};
