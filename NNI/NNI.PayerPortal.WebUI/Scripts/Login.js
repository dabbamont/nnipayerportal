/*
 *  Only executed on login page
 */
$(document).ready(function () {

    $('input[type=submit]').click(function () {
        //document.location.href = "/";
    });

    // Image rotation
    splashPageRotation();

    // Hide the login error
    $('#login-error').hide();
    
    loginForm();
    lightboxForgotPassword();
    lightboxRequestAccess();

});


function loginForm() {
    $('#login').makeAjax(
        function (form) {
            console.log(form);
            return {
                Email: form.getVal('UserName'),
                Password: form.getVal('Password'),
                RememberMe: form.getVal('RememberMe'),
                ChangePassword: 'false',
                ForwardUrl: '',
                Error: ''
            };
        },
        function (form, result) {
            console.log(result);
            if (result.ForwardUrl != '') {
                document.location.href = result.ForwardUrl;
            } else if (result.ChangePassword == "true") {
                newPasswordLightbox();
            } else if (result.Error != "") {
                $(form).highlightErrors(result.Error, result.ErrorFields);
            }
        }
    );
}

function lightboxForgotPassword() {
    $('#forgot-password').click(function () {
        $('#request-temporary-password').lightbox(null, 399, 415, 'item', function (box) {

            $(box).find('form').makeAjax(
                function (form) {
                    return {
                        Email: form.getVal('Email')
                };
            },
                function (form, result) {
                    if (result.Error != "") {
                        $(form).highlightErrors(result.Error, result.ErrorFields);
                    }
                }
            );

        });
    });
}

function lightboxRequestAccess() {
    $('#request-access').click(function () {
        $('#lightbox-request-access').lightbox(null, 399, 415, 'item', function (box) {

            $(box).find('form').makeAjax(
                function (form) {
                    return {
                        FirstName: form.getVal("FirstName"),
                        LastName: form.getVal("LastName"),
                        Email: form.getVal("Email"),
                        Phone: form.getVal("Phone1") + form.getVal("Phone2") + form.getVal("Phone3"),
                        Organization: form.getVal("Organization"),
                        Error: ''
                    };
                },
                function (form, result) {
                    if (result.Error != "") {
                        $(form).highlightErrors(result.Error, result.ErrorFields);
                    } else {
                        $(form).lightboxClose();
                    }
                }
            );

        });
    });
}

function splashPageRotation() {
    
    splashPageRotation.imageCount = $('#image-rotation img').length;
    splashPageRotation.current = 0;
    
    $('#image-rotation img:gt(0)').hide();
    $('#splash-dots img').attr('src', '/Content/Images/dot-empty.png');
    $('#splash-dots img:eq(' + splashPageRotation.current + ')').attr('src', '/Content/Images/dot-filled.png');
 
    setInterval( function() {
        if (splashPageRotation.current === splashPageRotation.imageCount - 1) {
            splashPageRotation.current = 0;
        } else {
            splashPageRotation.current++;
        }
        
        $('#image-rotation img').hide();
        $('#image-rotation img:eq(' + splashPageRotation.current + ')').show();
        $('#splash-dots img').attr('src', '/Content/Images/dot-empty.png');
        $('#splash-dots img:eq(' + splashPageRotation.current + ')').attr('src', '/Content/Images/dot-filled.png');

    }, 5000);
}
