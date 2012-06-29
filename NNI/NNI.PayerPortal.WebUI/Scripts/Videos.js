
//$('#ferrisblock').height(10);

/*
var video = {},
videoPlayer = {
    "seekingTime": false
};

$(document).ready(function() {  
    _V_('video-embed').ready(function() {
        video = this;       
        videoPlayer.initialize();
    });   
});
*/

var videoList = {};

function getVideos(page) {
    var oneItem = $('.video-browser-item:eq(0)').clone(),
    newItem = {},
    videoRecord = {};
    
    $('.video-browser-item').remove();

    $.getJSON("/Resource/Video/?page=" + page, function (results) {
        for (var i in results) {
            videoList[results[i]['ResourceId']] = results[i];
            videoRecord = results[i];

            newItem = oneItem.clone();
            newItem.find('img').attr('src', videoRecord.ThumnbailUrl);
            newItem.find('.video-browser-item-text').text(videoRecord.Title);
            newItem.attr('videoId', videoRecord.ResourceId);
            $('#video-browser-middle').append(newItem);
        }
    });
}


videoPlayer.initialize = function() {
    // Unbind it all
    $('#video-player img').unbind();
    videoPlayer.initDomEvents();
    videoPlayer.initPlayerEvents();
    
};

videoPlayer.initDomEvents = function() {
    
    $('#video-player-button-pause').hide();
    
    // Map play button, rewind and fast forward
    $('#video-player-button-play').click(function() {
        video.play();
    });
    $('#video-player-button-pause').click(function() {
        video.pause();
    });
    $('#video-player-button-rewind').click(function() {
        video.pause();
        video.currentTime(0);
    });
    $('#video-player-button-fast-forward').click(function() {
        video.pause();
        video.currentTime( video.duration() );
    });
    $('#video-player-button-maximize').click(function() {
        video.requestFullScreen();
    });
    
    // Volume slider layer
    $('#video-player-button-volume').mouseover(function() {
        $('#video-player-volume-slider').show();
    });
    
    $('#video-player-volume-slider').mouseleave(function() {
        $('#video-player-volume-slider').hide(); 
    });
    
    // Volume slider click
    $('#video-player-volume-slider').click(function(e) {
        $('#video-player-volume-slider-fill').width( e.offsetX );
        videoPlayer.updateVolumeFromSlider();
    });
    
    // Time slider click
    $('#video-player-time-slider').click(function(e) {
        $('#video-player-time-slider-handle').width( e.offsetX );
        videoPlayer.updateFromSlider();
    });
    
    // Video browser item click
    $('div.video-browser-item').live({
        "click": function() {
            $.getJSON('Video', function(result) {
                videoPlayer.loadVideo(result.videoUrl, result.posterUrl);
                $('#video-player-title').text(result.title);
                $('#video-player-subtitle').text(result.subTitle);
            });
        }
    });
    
    // Time slider mouse slide
    $('#time-slider-handle-img').mousedown(function(e) {
        
        videoPlayer.initDomEvents.originalX = e.pageX;
        videoPlayer.initDomEvents.originalWidth = 
        $('#video-player-time-slider-handle')
        .width();
        videoPlayer.seekingTime = true;
        
        e.preventDefault();
        
        video.pause();
        
        $(window).mouseup(function() {
            videoPlayer.updateFromSlider();
            $(window).unbind("mousemove"); 
        });
        
        $(window).mousemove(function(e2) {
            
            e.preventDefault();
            
            var x = e2.pageX,
            offset = x - videoPlayer.initDomEvents.originalX,
            newWidth = videoPlayer.initDomEvents.originalWidth + offset,
            sliderHandle = $('#time-slider-handle-img');

            // Set the new slider width
            if ( newWidth > sliderHandle.width() 
                && newWidth <= $('#video-player-time-slider').width() ) {
                $('#video-player-time-slider-handle').width(newWidth);
            } else if ( newWidth < sliderHandle.width() ) {
                $('#video-player-time-slider-handle').width( sliderHandle.width() ); 
            } else {
                $('#video-player-time-slider-handle').width( $('#video-player-time-slider').width() );
            }
                 
            videoPlayer.updateFromSlider();
        });   
    });  
};

videoPlayer.initPlayerEvents = function() {
    
    var convertTime = function(seconds) {
        var minutes = Math.floor(Math.round(seconds)/60); 
        seconds = Math.round(seconds % 60);
        if (String(seconds).length === 1) {
            seconds = "0" + seconds;
        }
        return minutes + ':' + seconds;
    };
    
    // Time update for time and progress bar
    video.addEvent('timeupdate', function() {
        $('#video-player-time-text').text(
            convertTime(video.currentTime()) + " / " + convertTime(video.duration())
            );
        videoPlayer.updateSlider();
    });
    
    // Volume updates
    video.addEvent('volumechange', function() {
        videoPlayer.updateVolumeSlider();
    });
    
    // Toggle play on/off
    video.addEvent('play', function() {
        $('#video-player-button-play').hide();
        $('#video-player-button-pause').show();
    });
    
    video.addEvent('pause', function() {
        $('#video-player-button-pause').hide();
        $('#video-player-button-play').show();
    });
};

videoPlayer.updateSlider = function() {
    var   width = $('#video-player-time-slider').width(),
    position =  video.currentTime() / video.duration(),
    positionX = width * position;
    $('#video-player-time-slider-handle').width(positionX);
};

videoPlayer.updateVolumeSlider = function() {
    var     width = $('#video-player-volume-slider').width(),
    position =  video.volume(),
    positionX = width * position;
    $('#video-player-volume-slider-fill').width(positionX);
};

videoPlayer.updateFromSlider = function() {
    var maxX = $('#video-player-time-slider').width(),
    currentX =  $('#video-player-time-slider-handle').width(),
    newTime = (currentX / maxX) * video.duration();
        
    video.currentTime( newTime );
    video.play();
};

videoPlayer.updateVolumeFromSlider = function() {
    var maxX = $('#video-player-volume-slider').width(),
    currentX =  $('#video-player-volume-slider-fill').width(),
    newVolume = currentX / maxX;
    video.volume(newVolume);
};

videoPlayer.loadVideo = function(fileUrl, posterUrl) {
    video.src(fileUrl);
    $('#video').attr('poster', posterUrl);
};
