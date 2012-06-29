$(document).ready(function () {
    resourceCenterNav();
    resourceCenterSplashNav();

    $('#content table').find('img').click(function () {

        $('#item').lightbox(null, $('#library-items'), 399, 415, 'item', function (box) {
            $.post("/Resource", {}, function (result) {
                
                box.setVal('item-lightbox-section', result.Category);
                box.find('#item-lightbox-image img').attr('src', result.ThumbnailUrl);
                box.setVal('item-lightbox-title', result.Title);
                box.setVal('item-lightbox-description', result.Description);
                box.find('#resourceId').val(result.ResourceId);
            });
        });

        //lightbox('#item', $('#library-items'), 399, 415, 'item');
        //itemLightbox($(this).attr('resourceId')); 
    });

    $('#library-items').css('background-image', "url('/Content/Images/resource-center-bookshelf.png')");
    if (navigator.userAgent.indexOf("Firefox") != -1) {
        $('div#library-items table tr:eq(0) td').height(120);
        $('div#library-items table tr:eq(1) td').height(130);
        $('div#library-items table tr:eq(2) td').height(130);

        $('div#library-items table tr:eq(0) img').css('position', 'relative');
        $('div#library-items table tr:eq(0) img').css('top', '35px');


        $('div#library-items table tr:eq(1) img').css('position', 'relative');
        $('div#library-items table tr:eq(1) img').css('top', '75px');

        $('div#library-items table tr:eq(2) img').css('position', 'relative');
        $('div#library-items table tr:eq(2) img').css('top', '75px');
    } else {
        $('div#library-items table tr:eq(0) td').height(120);
        $('div#library-items table tr:eq(1) td').height(130);
        $('div#library-items table tr:eq(2) td').height(130);

        $('div#library-items table tr:eq(0) img').css('position', 'relative');
        $('div#library-items table tr:eq(0) img').css('top', '18px');


        $('div#library-items table tr:eq(1) img').css('position', 'relative');
        $('div#library-items table tr:eq(1) img').css('top', '34px');

        $('div#library-items table tr:eq(2) img').css('position', 'relative');
        $('div#library-items table tr:eq(2) img').css('top', '20px');
    }
    $('#glow').hide();
    setInterval(function () {
        $('#glow').fadeIn(1000, function () {
            $('#glow').fadeOut(1000);
        });
    }, 3000);
    console.log(urlData[1]);
    if (urlData[1] == 'List') {

        $('#ferrisblock').height(20);
    } else {
        $('#ferrisblock').height(6);
    }
    //$('#ferris-wheel-ad').css('position', 'relative').css('top', '2px');
});


/*
 *  Set up the Resource Center navigation bar
 */
function resourceCenterNav () {

    var textNodes = getTextNodesIn($('#resource-center-navigation span a'));
    $(textNodes).remove();

    // Wait until the images are loaded, and size the containing divs
    $('#resource-center-navigation span img').load(function() {
        $(this).parent().width($(this).width());
        $(this).parent().find('img:eq(1)').hide();
    });

    var textNodes = getTextNodesIn($('#resource-center-navigation span a'));
    $(textNodes).remove();

    // Unbind any existing events (there shouldnt be)
    // and swap the images on hover
    // @TODO    See if you can do this with CSS without reslicing images

    $("#resource-center-navigation span").each(function () {
            $(this).find('img:eq(1)').hide();
            $(this).find('img:eq(0)').show();
    });

    $("#resource-center-navigation span").unbind()
    .hover(function () {
        $(this).parent().find('span').each(function () {
            $(this).find('img:eq(1)').hide();
            $(this).find('img:eq(0)').show();
        });
        $(this).find('img:eq(0)').hide();
        $(this).find('img:eq(1)').show();
    })
    // Click event for each element
    // @TODO    Find out if this will just be a full page reload
    //          If so, just use anchor hrefs
    .click(function () {
        //loadResourceCenterSection($(this).attr('name'), 1);
        loadLibrary(1, $(this).attr('name'));
    });
}

function loadLibrary(page, category) {

    loadLibrary.page = page;
    loadLibrary.category = category;

    $.getJSON("/Resource/List?category=" + category + '&page=' + page, function (result) {

        $("#library-pagination a").remove();
        for (i = 1; i < (result.PagingInfo.TotalItems / result.PagingInfo.ItemsPerPage); i++) {
            var newLink = $("<a href='#'>" + i + "</a>");
            newLink.appendTo($('#library-pagination')).click(function () {
                loadLibrary($(this).text(), loadLibrary.sortBy, loadLibrary.sortDirection);
            });
        }

        for (var i in result.Items) {
            var item = result.Items[i];
            $('.library-item img:eq(' + i + ')').attr('ResourceId', item.ResourceId);
            $('.library-item img:eq(' + i + ')').attr('src', item.ThumbnailUrl.replace('~', ''));
        }
    });
}

function resourceItemLightbox(itemId) {
    $('#item').lightbox(null, $('#library-items'), 399, 415, 'item', function (box) {
        $.getJSON("/Resource/ListItem/?resourceId=" + itemId, function (result) {
            box.setVal('item-lightbox-section', result.Category);
            box.find('#item-lightbox-image img').attr('src', result.ThumbnailUrl);
            box.setVal('item-lightbox-title', result.Title);
            box.setVal('item-lightbox-description', result.Description);
            box.find('#resourceId').val(result.ResourceId);

            box.find('.lightbox-button-text a:eq(0)').click(function () {
                window.open(result.ResourceUrl);
            });

            box.find('.lightbox-button-text a:eq(1)').click(function () {

            });

            box.find('.lightbox-button-text a:eq(2)').click(function () {
                itemRequestLightbox($(this).closest('.lightbox').find('#ResourceId').val());
            });
        });
    });
}

/*
 *  Splash image navigation for Resource Center
 *  @TODO   Change this and add coordinates map
 */
function resourceCenterSplashNav() {

    $('#preview-layers-hover img').hide();

    $('#resource-center-preview > img:gt(0)').hide();
    $('#resource-center-preview img').hide();


    $("#resource-center-navigation span")
    .mouseenter(function () {

        var linkName = $(this).attr('name')
        console.log(linkName);
        $('#resource-center-preview img').hide();
        $('#resource-center-preview img[name=' + linkName + ']').show();
    });

    $('#preview-layers div').mouseenter(function () {
        var name = $(this).attr('id');

        $('#preview-layers-hover').css('background-image', "url('/Content/Images/resource-center-" + name + ".png')");


        //$('#preview-layers-hover img[name!=' + name + ']:visible').hide();
        //$('#preview-layers-hover img[name=' + name + ']:not(:visible)').show();


        $('#resource-center-navigation span[name!=' + name.replace('preview-', '') + ']').each(function () {
            $(this).find('img:eq(1)').hide();
            $(this).find('img:eq(0)').show();
        });
        $('#resource-center-navigation span[name=' + name.replace('preview-', '') + '] img:eq(1)').show();
        $('#resource-center-navigation span[name=' + name.replace('preview-', '') + '] img:eq(0)').hide();

    });

    $('#preview-layers div').mouseout(function () {

        //resource-center-background-blank.png
        $('#preview-layers-hover').css('background-image', "url('/Content/Images/resource-center-background-blank.png')");

    }).click(function () {
        document.location.href = "/Resource/List";
    });   ;
}

function loadResourceCenter(category, page) {
    
}

/*
 *  Query and display a resource center section.
 *  Pass in the name of the section and page for pagination
 */
function loadResourceCenterSectionBak(section, page) {
    // Clone elements we're going to need to clone again
    var ul = $('#resource-center-library-items ul:eq(0)'),
        li = ul.find('li:eq(0)').clone(),
        pageContainer = $('div#resource-center-library-pagination'),
        pageLink = $('div#resource-center-library-pagination a:eq(0)').clone();
    
    // Query the results from the server
    $.getJSON('ResourceCenter', function (result) {

        // Remove existing library items
        var item = {};
        ul.find('li').remove();
        pageContainer.find('a').remove();

        // Add items to the container list
        for (var i in result.items) {
            item = result.items[i];
            line = li.clone()
            .find('img:eq(0)')
            .attr({
                'src': item.thumbnail,
                'resourceId': item.id
            })
            .closest('li')
            .appendTo(ul);
        }

        // Set up the pagination
        for (i = 1; i <= result.pages; i++) {
            pageContainer.append(
                pageLink.clone().attr('pageNum', i).text(i).after("&nbsp;&nbsp;")
            );
        }

        // Bind lightbox click event for items
        ul.find('img').click(function () {
            lightbox('#item');
            //itemLightbox($(this).attr('resourceId')); 
        });
    });
}
