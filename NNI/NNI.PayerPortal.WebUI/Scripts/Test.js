/* 
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

var responses = {
    ".Account.Logon": {
        UserName: "UserName",
        Password: "Password",
        RememberMe: "RememberMe",
        ChangePassword: 'false',
        ForwardUrl: "/",
        Error: ''
    },
    ".Account.NewPassword": {
        Email: "Email",
        OldPassword: "OldPassword",
        NewPassword: "NewPassword",
        ChangePassword: 'false',
        Error: ''
    },
    ".Account.RequestTemporaryPassword": {
        Email: "Email",
        ChangePassword: 'false',
        TemporaryPassword: '',
        Error: ''
    },
    ".Account.RequestAccess": {
        FirstName: "FirstName",
        LastName: "LastName",
        Email: "Email",
        Phone: "Phone",
        Organization: "Organization",
        Error: ''
    },
    ".Account.SetPreferences": {
        "OldPassword": "OldPassword",
        "NewPassword": "NewPassword",
        "ConfirmNewPassword": "ConfirmNewPassword",
        "OldEmail": "OldEmail",
        "NewEmail": "NewEmail",
        "ConfirmNewEmail": "ConfirmNewEmail"
    },
    ".Resource.TopPicks": [
        {
            "title": "Ever feel as if the costs of diabetes management just keep coming around?",
            "url": "/Views/HomeFullMockup/Library.aspx"
        },
        {
            "title": "Ever feel as if the costs of diabetes management just keep coming around?",
            "url": "/Views/HomeFullMockup/Library.aspx"
        },
        {
            "title": "Ever feel as if the costs of diabetes management just keep coming around?",
            "url": "/Views/HomeFullMockup/Library.aspx"
        },
        {
            "title": "Ever feel as if the costs of diabetes management just keep coming around?",
            "url": "/Views/HomeFullMockup/Library.aspx"
        },
        {
            "title": "Ever feel as if the costs of diabetes management just keep coming around?",
            "url": "/Views/HomeFullMockup/Library.aspx"
        }
        ],
    "Highlights": [
        {
            "url": "/Views/HomeFullMockup/Library.aspx",
            "image1": "/Content/Images/highlights-1.png",
            "image2": "/Content/Images/highlights-1-rollover.png"
        },
        {
            "url": "/Views/HomeFullMockup/Library.aspx",
            "image1": "/Content/Images/highlights-2.png",
            "image2": "/Content/Images/highlights-2-rollover.png"
        },
        {
            "url": "/Views/HomeFullMockup/Library.aspx",
            "image1": "/Content/Images/highlights-3.png",
            "image2": "/Content/Images/highlights-3-rollover.png"
        }
        ],
    "ResourceCenter": {
        "pages": 10,
        "items": [
            {
                "id": "1",
                "url": "http://url.com",
                "thumbnail": "/Content/Images/library-thumb.png"
            },
            {
                "id": "1",
                "url": "http://url.com",
                "thumbnail": "/Content/Images/library-thumb.png"
            },
            {
                "id": "1",
                "url": "http://url.com",
                "thumbnail": "/Content/Images/library-thumb.png"
            },
            {
                "id": "1",
                "url": "http://url.com",
                "thumbnail": "/Content/Images/library-thumb.png"
            },
            {
                "id": "1",
                "url": "http://url.com",
                "thumbnail": "/Content/Images/library-thumb.png"
            },
            {
                "id": "1",
                "url": "http://url.com",
                "thumbnail": "/Content/Images/library-thumb.png"
            },
            {
                "id": "1",
                "url": "http://url.com",
                "thumbnail": "/Content/Images/library-thumb.png"
            },
            {
                "id": "1",
                "url": "http://url.com",
                "thumbnail": "/Content/Images/library-thumb.png"
            },
            {
                "id": "1",
                "url": "http://url.com",
                "thumbnail": "/Content/Images/library-thumb.png"
            },
            {
                "id": "1",
                "url": "http://url.com",
                "thumbnail": "/Content/Images/library-thumb.png"
            },
            {
                "id": "1",
                "url": "http://url.com",
                "thumbnail": "/Content/Images/library-thumb.png"
            },
            {
                "id": "1",
                "url": "http://url.com",
                "thumbnail": "/Content/Images/library-thumb.png"
            },
            ]
    },
    'HedisSearch': {
        'New York': [
                {
                    "planName": "A plan name",
                    "state": "A state",
                    "planType": "A plan type",
                    "planGrade": "Excellent"
                },
                {
                    "planName": "A plan name",
                    "state": "A state",
                    "planType": "A plan type",
                    "planGrade": "Excellent"
                },
                {
                    "planName": "A plan name",
                    "state": "A state",
                    "planType": "A plan type",
                    "planGrade": "Excellent"
                }

            ]
    },
    "Videos": [
            {
                "id": "999",
                "thumbnail": "/Content/Images/video-browser-thumbnail.png",
                "title": "Video Text"
            },
            {
                "id": "999",
                "thumbnail": "/Content/Images/video-browser-thumbnail.png",
                "title": "Video Text"
            },
            {
                "id": "999",
                "thumbnail": "/Content/Images/video-browser-thumbnail.png",
                "title": "Video Text"
            },
            {
                "id": "999",
                "thumbnail": "/Content/Images/video-browser-thumbnail.png",
                "title": "Video Text"
            },
            {
                "id": "999",
                "thumbnail": "/Content/Images/video-browser-thumbnail.png",
                "title": "Video Text"
            }
        ],
    "Video": {
        "videoUrl": "http://video-js.zencoder.com/oceans-clip.mp4",
        "posterUrl": "http://video-js.zencoder.com/oceans-clip.jpg",
        "title": "title here",
        "subTitle": "subtitle here"
    },
    "Resource": {
        "id": "12",
        "section": "Diabetes Management",
        "image": "/Content/Images/item-lightbox-image.png",
        "title": "The Title Goes Here",
        "description": "The description goes in here",
        "note": ""
    },
    "Login": {
        "error": "",
        "forwardUrl": "http://localhost/Views/HomeFullMockup/HomeMain.aspx",
        "changePassword": ""
    }
};


(function() {

    var originalGet = jQuery.getJSON;
    
    
    
    jQuery.getJSON = function(url, callback) {
      
      callback( responses[url] );
      
      //originalGet(url, callback);
    };
    
})();

(function () {
    jQuery.post = function(url, data, callback) {
        
        for (var i = 0; i < 10; i++) {
            url = url.replace('/', '.', 20);
        }

        console.log({
            "PostUrl": url,
            "PostData": data,
            "Response": responses[url]
        });
        callback( responses[url] );
    };
})();
