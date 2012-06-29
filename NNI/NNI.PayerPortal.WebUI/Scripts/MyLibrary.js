$(document).ready(function () {
    $('#content table').find('img').click(function () {
        itemLightbox( $(this).attr('ResourceId');

    });
    if (navigator.userAgent.indexOf("Firefox") != -1) {
        $('div#library-items table tr:eq(0) td').height(120);
        $('div#library-items table tr:eq(1) td').height(130);
        $('div#library-items table tr:eq(2) td').height(130);

        $('div#library-items table tr:eq(0) img').css('position', 'relative');
        $('div#library-items table tr:eq(0) img').css('top', '20px');

        $('div#library-items table tr:eq(1) img').css('position', 'relative');
        $('div#library-items table tr:eq(1) img').css('top', '60px');

        $('div#library-items table tr:eq(2) img').css('position', 'relative');
        $('div#library-items table tr:eq(2) img').css('top', '70px');
    } else {

        $('div#library-items table tr:eq(0) td').height(120);
        $('div#library-items table tr:eq(1) td').height(130);
        $('div#library-items table tr:eq(2) td').height(130);

        $('div#library-items table tr:eq(1) img').css('position', 'relative');
        $('div#library-items table tr:eq(1) img').css('top', '22px');

        $('div#library-items table tr:eq(2) img').css('position', 'relative');
        $('div#library-items table tr:eq(2) img').css('top', '18px');
    }
    $('#ferrisblock').height(10);

    libraryLinks();
    loadLibrary(1, "Date", "Descending");

});

// sortBy Date || Title 
// sortDirection Ascending || Descending
function loadLibrary(page, sortBy, sortDirection) {
   
   loadLibrary.page = page;

   loadLibrary.sortBy = sortBy;
   loadLibrary.sortDirection = sortDirection;

   $.getJSON("/Resource/MyLibrary?sortBy=" + sortBy + '&page=' + page + '&sortDirection=' + sortDirection, function (result) {
       // @TODO render pagination

       $("#library-pagination a").remove();
       for (i = 1; i < (result.PagingInfo.TotalItems / result.PagingInfo.ItemsPerPage); i++) {
           var newLink = $("<a href='#'>" + i + "</a>");
           newLink.appendTo($('#library-pagination')).click(function () {
               loadLibrary( $(this).text(), loadLibrary.sortBy, loadLibrary.sortDirection);
           });
       }

       for (var i in result.Items) {
           var item = result.Items[i];
           $('.library-item img:eq(' + i + ')').attr('ResourceId', item.ResourceId);
           $('.library-item img:eq(' + i + ')').attr('src', item.ThumbnailUrl.replace('~', ''));
       }
   });
}



function libraryLinks() {
    $('#library-sorting a').click(function () {
        loadLibrary(loadLibrary.page, $(this).attr("sortBy"), $(this).attr("sortDirection"));
    });
}

