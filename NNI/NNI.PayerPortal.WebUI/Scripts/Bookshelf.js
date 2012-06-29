var bookshelf = bookshelf || {
    populate: function(container, items) {
        var  item = null,
        newItem = null;
        
        $('#container').find('li').remove();
        
        for (var i in items) {
            item = items[i];
               
            // Add a line item for the asset
            newItem  = '<li name="' + i + '">'
            + '<a href=#><img src="' 
            + item.imageURL + '"></a></li>';
               
            $(newItem).appendTo(container);
               
            $(newItem).find('a').click(function() {
                itemLightBox( $(this).parent().attr('name') );
            });
              
        }
    }  
}