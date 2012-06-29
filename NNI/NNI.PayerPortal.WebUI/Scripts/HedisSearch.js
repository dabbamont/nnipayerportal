/*
 *  Initialize HEDIS search on document load
 */
$(document).ready(function () {
    // Hide the results container
    $('#hedis-results').hide();

    //$('#hedis-results').leftScrollbar();

    // Bind click event for submit button
    $('#hedis-search-submit input[type=submit]').click(function () {
        hedisSearch();
    });

    // Bind click event for the Add Plan button
    $('#add-plan a').click(function () {
        /**
        *  @TODO make this things scroll to the bottom when the
        *  height is more than the max height
        */
        var searchTable = $('#hedis-form table:eq(0)'),
            planLine = searchTable.find('tr:eq(0)').clone();

        if (searchTable.find('tr').length >= 10) {
            return 0;
        }

        // Append the line
        searchTable.append(planLine);

        planLine.find('select:eq(0)').focus();

        // Number the first cell
        $(planLine).find('td:eq(0)').text(searchTable.find('tr').length + ".");
    });

});

// Build data from the search form
function readHedisForm() {
    var data = [],
        item = {};
        
    // Loop through the table and grab the values of the select boxes
    $('#hedis-form table tr').each(function() {
        item = {};
        $(this).find('select').each(function() {
            item[ $(this).attr('name') ] = $(this).val();
        });
        data.push(item);
    });
    return data;
}

/*
 *  Perform the actual search and render the results
 */
function hedisSearch() {
    
    var resultElement = $('div#hedis-single-result:eq(0)').clone(),
        tableRow = resultElement.find('tbody tr:eq(0)').clone(),
        oneResultElement = {},
        oneRow = {},
        tBody = {},
        stateResults,
        oneRecord,
        data = readHedisForm();
    
    // Hide the search form
    $('#hedis-form').hide();
    
    // Remove the element we cloned for the state results
    $('div#hedis-single-result').remove();
    
    // Query the server and render the results
    $.getJSON('/Plan/HedisSearch', function (result) {

        // Build the element for each state
        for (var state in result) {
            stateResults = result[state];
            oneResultElement = resultElement.clone();
            oneResultElement.find('h1').text(state);
            oneResultElement.find('tbody tr').remove();
            tBody = oneResultElement.find('tbody');

            // Add the plan lines for each state
            for (var i in stateResults) {
                oneRecord = stateResults[i];
                oneRow = tableRow.clone();
                oneRow.find('td#plan-name').text(oneRecord.PlanName);
                oneRow.find('td#plan-type').text(oneRecord.PlanType);
                oneRow.find('td#plan-grade').text(oneRecord.Grade);
                tBody.append(oneRow);
            }

            // Append the state result to the main results container
            $('#hedis-result-set').append(oneResultElement);
        }

        $('#hedis-results tbody tr:even td').css('background-color', '#D9F0F9');

        // Show the results
        $('#hedis-results').show();

    });
}