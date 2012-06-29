/* 
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

function doForm(theForm, submitURL, callback) {
    var formData = {};
    
    theForm.find('input, select').each(function() {
        formData[$(this).attr('name')] = $(this).val();
    });
    
    // Send request
    
        // Callback
        callback();
}
