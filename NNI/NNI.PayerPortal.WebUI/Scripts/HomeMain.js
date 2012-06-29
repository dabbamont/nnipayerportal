/*
 *  This executes on every page other than login
 */
$(document).ready(function () {

    // Initialize the main navigation bar
    //rolloverNavigation();
    $('#home-highlights > div').hide();
    var r = Math.floor((Math.random() * 2) + 1) - 1;
    console.log(r);
    $('#home-highlights > div:eq(' + r + ')').show();

    $('.highlight-bottom span').hide();
    $('.highlight').mouseenter(function () {
        console.log($(this).attr("rollover"));
        $(this).find('.highlight-bottom').css("background-image", $(this).find('.highlight-bottom').attr("rollover"));
        $(this).find('.highlight-bottom span').show();
    }).mouseleave(function () {
        $(this).find('.highlight-bottom').css("background-image", $(this).find('.highlight-bottom').attr("normal"));
        $(this).find('.highlight-bottom span').hide();
    });


    // Test for resource center navigation
    // @TODO replace this and move it to ResourceCenter.js
    $('#resource-center-navigation img').click(function () {
        //document.location.href = 'http://localhost/Views/HomeFullMockup/ResourceCenterLibrary.aspx';
    });

    splashPageRotation();
    //rolloverHighlights();

    $('#ferris-wheel-ad').hide();
});




/*
 *  Image rotator for the main splash page
 */
function splashPageRotation() {
    
    // Count the images and initialize the current image
    splashPageRotation.imageCount = $('#splash img').length;
    splashPageRotation.current = 0;
    
    // Hide all the images except the first one
    $('#splash img:gt(0)').hide();
    $('#splash-dots img').attr('src', '/Content/Images/dot-empty.png');
    $('#splash-dots img:eq(' + splashPageRotation.current + ')').attr('src', '/Content/Images/dot-filled.png');

    // Initialize the rotation
    setInterval(function () {
        // Loop through images, returning to first when complete
        if (splashPageRotation.current === splashPageRotation.imageCount - 1) {
            splashPageRotation.current = 0;
        } else {
            splashPageRotation.current++;
        }

        // Hide all the images and then show the current one
        $('#splash img').hide();
        $('#splash img:eq(' + splashPageRotation.current + ')').show();
       
        $('#splash-dots img').attr('src', '/Content/Images/dot-empty.png');
        $('#splash-dots img:eq(' + splashPageRotation.current + ')').attr('src', '/Content/Images/dot-filled.png');
    }, 5000);    // Images rotate every 5 seconds (5000)
}

/*
 *  Rollover images and ajax loading for home page highlights section
 *  @TODO   Move this into home page specific JS file
 *  @TODO   Just use CSS for rollovers
 *  @TODO   Re-slice images to fix dividers
 */
function rolloverHighlights() {

    var textNodes = getTextNodesIn($('#home-highlights'));

   

    $(textNodes).remove();

    // Hide all the mouseover images
    $("#home-highlights span").each(function () {
        $(this).find('img:eq(1)').hide();
        //$(this).width($(this).find('img:eq(0)').width());
    });
    
    
    
    // Initialize the rollovers
    $("#home-highlights span")
    .hover(function() {
        $(this).find('img:eq(0)').hide();
        $(this).find('img:eq(1)').show();
        $(this).mouseleave(function() {
            $(this).find('img:eq(1)').hide();
            $(this).find('img:eq(0)').show();
        });
    });
    
}


/*
 *  Build the top picks sidebar
 */
function createTopPicks() {
    var ct = 0;
    jQuery.getJSON('TopPicks', function(result) {
       var pick, 
       ct = 0,
       anchor;
       
       // Apply attributes to each top pick container
       // @TODO make this just use URLs to library/videos instead of lightboxes
       for(var i in result) {
           pick = result[i];
           anchor = $('#top-picks a:eq(' + ct + ')');
           anchor.attr({
               "resourceId": pick.id,
               "resourceType": pick.type
           });
           anchor.text(pick.title);
           ct++;     
       }
    });
}





