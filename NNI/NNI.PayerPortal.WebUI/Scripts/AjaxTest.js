var newTests = {};

function testAjax(num) {


    //for (var i in tests) {
        var test = tests[num];

        if (test.data) {
            newTests[test.url] = {};
            for (var testName in test.data) {
                newTests[test.url][testName] = {
                    request: test.data[testName]
                };
                console.log("GETTING " + test.url + " -> " + testName);
                $.ajax({
                    url: test.url,
                    type: "POST",
                    data: JSON.stringify(test.data[testName]),
                    processData: false,
                    async: false,
                    contentType: 'application/json; charset= utf-8',
                    success: function (result) {
                        console.log("GOT " + test.url + " -> " + testName);
                        newTests[test.url][testName]['response'] = result;
                    }
                });
            }

        } else {
            newTests[test.url] = {};
            console.log("GETTING " + test.url);
            $.ajax({
                url: test.url,
                dataType: 'json',
                async: false,
                success: function (result) {
                    newTests[test.url] = result;
                }
            });
        }

    //}

    console.log(newTests[num]);
}

var tests = {
    1: {
        url: "/Account/LogOn/",
        data: {
            valid: {
                Email: "dan@novo.com",
                Password: "HAPPy777!",
                RememberMe: "true",
                ChangePassword: 'false',
                ForwardUrl: '',
                Error: ''
            }
        }
    },
    2: {
        url: "/Account/Preferences/",
        data: {
            changeEmailValid: {
                "OldPassword": "HAPPy777!",
                "NewPassword": "HAPPy888!",
                "ConfirmNewPassword": "HAPPy888!",
                "OldEmail": "dan@novo.com",
                "NewEmail": "dan2@novo.com",
                "ConfirmNewEmail": "dan2@novo.com",
                "ChangeEmail": "true",
                "ChangePassword": "false"
            }
        }
    },
    3: {
        url: "/Resource/RequestResource",
        data: {
            valid: {
                "ResourceId": 1,
                "OrderResource": "true",
                "RequestMeeting": "true",
                "UseEmail": "true",
                "Email": "dan@novo.com",
                "UsePhone": "true",
                "Phone": "2019256059",
                "ContactTime": "Morning",
                "Error": "",
                "ErrorFields": ""
            }
        }
    },
    4: {
        url: "/Account/NewPassword/",
        data: {
            valid: {
                
            }
        }
    },
    5: {
        url: "/Account/RequestTemporaryPassword/",
        data: {
            valid: {
                Email: "dan@novo.com"
            }
        }
    },
    6: {
        url: "/Account/RequestAccess",
        data: {
            valid: {
                FirstName: "FirstName",
                LastName: "LastName",
                Email: "Email",
                Phone: "2019256059",
                Organization: "Organization",
                Error: ''
            }
        }
    },
    7: {
        url: "/Meeting/",
        data: {
            valid: {
                "OrderResource": "true",
                "AskQuestion": "false",
                "HaveDiscussion": "true"
            }
        }
    },
    8: {
        url: "/Resource/TopPicks/"
    },
    9: {
        url: "/Resource/MyLibrary?sortBy=&page=&sortDirection="
    },
    10: {
        url: "/Resource",
        data: {
            valid: {
                "ResourceId": ""
            }
        }
    },
    11: {
        url: "/Resource/List?category=&page="
    }
};

console.log(tests);