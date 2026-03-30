var session = "";
var authorizationCode = "";
var sessionID = "";
var authScheme = [];
var preid = "USER";
var voiceFormValue = "";
var userName = "";
var errorDivId = "USERerror";
var PushNotificationCode = "329";
var codeVerfierInterval = "";
var TimerInterval = "";
var Fido2Options = "";
var QrCode = "";
var client_id = "";
var clientType = "";
var redirectUrl = "";
var state = "";
var ErrorConstant = "";
var LoginControllerUrl = "";
var modal = null;
var selectAuthScheme = null;
var currentAuth = "";
var base64Data = "";

var journeyToken = "";

var activateSuspendedAccount = null;
var VerifierCode = "";

var mediaStream;
var mediaRecorder;
var recordedChunks = [];
var audioContext;
var analyser;
var audiocanvas;
var audiocanvasnum;
var audiocanvasCtx;
var audiocanvasCtxnum;
var audioPlayback;
var audioPlaybacknum;
var base64audio;

var authenticationScheme = "";
var currentAuthScheme = "";
var authenticationMethods = null;

let countdownTimer = null;
let countdownValue = 3;
let captured = 0;
let detectionid = null;
let timeoutId = null;
let videoStream = null;
let isCountingDown = false;
let isCaptured = false;
let lastDetectionValid = false;

const countdownEl = document.getElementById("countdown");
const countdownText = countdownEl.querySelector(".countdown-text");
const faceInstructions = document.getElementById("faceinstructions");

$(document).ready(function () {

    $(".face-capture-container").hide();

    $(".use-webcam-link").on("click", function (e) {
        e.preventDefault();

        $(".camera-consent-container").hide();

        $(".face-capture-container").show();

        startVideo();
    });

});

function closeTimeoutModal() {
    window.location.reload();
}

function showRetryOrOtherOptions() {
    const htmlContent = `
        <div style="text-align: left;">
            <p style="font-size: 14px;">We sent push notification on your MyTrust mobile app ,but we didn't get response ,Please check your internet connection in mobile and click on the 'Resend notification' button to resend notification.</p>
        </div>
    `;

    swal({
        title: "Request Time Out",
        text: htmlContent,
        html: true,
        showConfirmButton: true,
        confirmButtonText: "Retry",
        showCancelButton: true,
        cancelButtonText: "Try another way",
        closeOnConfirm: false,
        closeOnCancel: false,
        allowOutsideClick: true
    }, function (isConfirm) {
        if (isConfirm) {
            retryLogin();
        } else {
            tryOtherAuth();
        }
    });
}

function retryLogin() {
    document.getElementById("requestTimeoutModal").style.display = "none";
    clearInterval(codeVerfierInterval);
    sendCodeAgain();
}

function tryOtherAuth() {
    // Logic to show other authentication options
    document.getElementById("requestTimeoutModal").style.display = "none";
    showOptions();
}

function getqrcode() {
    try {
        jQuery.ajax({
            type: "GET",
            url: LoginControllerUrl + "/GetQrCode",
            cache: false,
            beforeSend: function () {
                $('#overlay').show();
            },
            complete: function () {
                $('#overlay').hide();
            },
            error: ErrorHandler,
            success: function (response) {
                document.getElementById('appQrCode').textContent = response;
            },
            failure: function () { alert("Failure!!"); }
        });
    }
    catch (e) {
        alert(e.message);
    }
}

function showOptions() {

    const schemas = window.authSchemas;
    let htmlContent = `
<div style="text-align:left;">
    <h3 style="font-size:25px; margin-bottom:15px;">Other Sign-In options:</h3>`;

    for (const key in schemas) {
        const schema = schemas[key];

        if (schema) {
            htmlContent += `
<div onclick="changeAuthScheme('${key}')" style="padding:10px; border:1px solid #ddd; border-radius:5px; cursor:pointer; margin:10px 0;">
    <i class="fa ${schema.Icon || ''}" style="margin-right:10px; color:#777;"></i>
    <strong style="font-size:14px;">${schema.Title || ''}</strong><br>
    <small>${schema.Description || ''}</small>
</div>`;
        }
    }

    htmlContent += '</div>';

    swal({
        text: htmlContent,
        title: "",
        html: true,
        showCancelButton: true,
        cancelButtonText: "Cancel",
        confirmButtonText: "Ok"
    }, function () {
        window.location.reload();
    });
}

async function init() {
    if (method == "get") {
        var buff = window.atob(target).split("?");
        if (buff[0].startsWith("/Saml2/")) {
            let buff1 = buff[0].split("/");
            client_id = buff1[3];
            clientType = 2;
        }
        else {
            const urlParams = new URLSearchParams(buff[1])
            client_id = urlParams.get('client_id');
            redirectUrl = urlParams.get('redirect_uri');
            state = urlParams.get('state');
            clientType = 1;
        }
    }
    else {
        var buff = window.atob(target);
        var jsonObj = JSON.parse(buff);
        var buff1 = jsonObj.entityEndpoint.split("/");
        client_id = buff1[3];
        clientType = 2;
    }

    LoginControllerUrl = window.location.pathname;

    const checkUser = document.getElementById("checkUser");
    checkUser.addEventListener("click", VerifyUser);

    const checkPwdBtn = document.getElementById("checkPassword");
    checkPwdBtn.addEventListener("click", authenticate);

    const checkTotpBtn = document.getElementById("checkTOTP");
    checkTotpBtn.addEventListener("click", authenticate);

    //const checkPinBtn = document.getElementById("checkPin");
    //checkPinBtn.addEventListener("click", authenticate);

    modal = document.querySelector(".modal");
    var closeButton = document.querySelector(".close-button");
    var closeButton1 = document.querySelector(".close-btn");
    var cancelButton = document.getElementById('cancelAuthModalBtn');
    var confirmButton = document.getElementById('confirmAuthScheme');
    function toggleModal() {
        modal.classList.toggle("show-modal");
    }
    function AuthModalToggle() {
        clearInterval(codeVerfierInterval);
        deleteCookies();
        authSchemaModal.classList.toggle("show-modal");
    }
    function ConfirmAuthScheme() {
        changeAuthScheme(document.getElementById("authSchemaSelect").value);
    }
    function accountSuspendActivatetoggleModal() {
        activateSuspendedAccount.classList.toggle("show-modal");
    }
    closeButton.addEventListener("click", toggleModal);
    closeButton1.addEventListener("click", toggleModal);
    cancelButton.addEventListener("click", AuthModalToggle);
    confirmButton.addEventListener("click", ConfirmAuthScheme);

    activateSuspendedAccount = document.getElementById("activatesubscriber");
    authenticationMethods = document.getElementById("authenticationmethods");
    var closeButtons = document.querySelector(".cls-btn1");
    var closeButtons1 = document.querySelector(".cls-btn2");
    closeButtons.addEventListener("click", accountSuspendActivatetoggleModal);
    closeButtons1.addEventListener("click", accountSuspendActivatetoggleModal);

    document.getElementById("retryRequest").addEventListener("click", handleRetryModal);
    document.getElementById("tryAnotherWay").addEventListener("click", handleTryAnotherWayModal);
    document.getElementById("closeTimeoutModal").addEventListener("click", handleTimeoutCancelModal);

    function handleRetryModal() {
        document.getElementById("customTimeoutModal").classList.remove("active");
        clearInterval(codeVerfierInterval);
        sendCodeAgain();
    }

    function handleTryAnotherWayModal() {
        document.getElementById("customTimeoutModal").classList.remove("active");
        showOptions();
    }

    function handleTimeoutCancelModal() {
        document.getElementById("customTimeoutModal").classList.remove("active");
        window.location.reload();
    }

    document.getElementById("wrongCredentialRetryRequest").addEventListener("click", handleWrongCredentialRetryModal);
    document.getElementById("wrongCredentialCloseTimeoutModal").addEventListener("click", handleWrongCredentialTimeoutCancelModal);

    function handleWrongCredentialRetryModal() {
        document.getElementById("wrongCredentialModal").classList.remove("active");
        clearInterval(codeVerfierInterval);
        sendCodeAgain();
    }

    function handleWrongCredentialTryAnotherWayModal() {
        document.getElementById("wrongCredentialModal").classList.remove("active");
        showOptions();
    }

    function handleWrongCredentialTimeoutCancelModal() {
        document.getElementById("wrongCredentialModal").classList.remove("active");
        window.location.reload();
    }

    closeButton.addEventListener("click", toggleModal);
    closeButton1.addEventListener("click", toggleModal);

    await Promise.all([
        faceapi.nets.tinyFaceDetector.loadFromUri('./models'),
        faceapi.nets.faceLandmark68Net.loadFromUri('./models'),
        faceapi.nets.faceRecognitionNet.loadFromUri('./models'),
        faceapi.nets.faceExpressionNet.loadFromUri('./models')
    ]);

    $('#networkOverlay').hide();

    deleteCookies();
}

$(".toggle-password").on("click", function () {
    $(this).toggleClass("fa-eye fa-eye-slash");

    const target = $(this).attr("toggle");
    const input = $(target);

    input.toggleClass("pwd");
});

$(document).on("keydown", "input", function (event) {
    if (event.key === "Enter") {
        event.preventDefault();

        if (!currentAuthScheme) {
            VerifyUser();
        }
        else if (currentAuthScheme === "TOTP" || currentAuthScheme === "PASSWORD") {
            authenticate();
        }
    }
});

function show() {
    if (currentAuthScheme == "") {
        $("#uaeidDialog").hide();
    }
    var id = authScheme.pop();
    //var id = "PASSWORD";
    currentAuthScheme = id;
    currentAuth = id;
    if (id == undefined) {

        $('#overlay').hide();
        $(':button').prop('disabled', true);
        var url = LoginControllerUrl + window.location.search
        $("#PUSH_NOTIFICATION").css("display", "none");
        $("#WEB_FACE").css("display", "none");
        $("#QRCODE").css("display", "none");
        $("#TOTP").css("display", "none");
        $("#PASSWORD").css("display", "none");  
        $(id).css("display", "none");
        $("#LoginSuccessContainer").css("display", "flex");
        setTimeout(() => {
            postForm(url, {
                username: userName,
                target: target,
                method: method
            });
        }, 2000); 


    } else {

        //set voice phrase value
        if (id == "VOICE_TI" || id == "VOICE_DIGIT_RECOGNISATION" ||
            id == "VOICE_TPD_RECOGNISATION") {
            setElementValues(id);
            id = "VOICE";
        }

        if (id == "PUSH_NOTIFICATION" || id == "UAEKYCFACE") {
            id = "PUSH_NOTIFICATION";
            document.getElementById("PushNotificationCode").innerText = PushNotificationCode;
            setCookies();
            codeVerfierInterval = setTimeout(isUserVerifiedCode, 500);
            startTimer1();
        }


        if (id === "ICP") {

            showLoder();
            setTimeout(() => {
                document.getElementById('controls-container').style.display = 'none';
                document.getElementById('uae-kyc-container').style.display = 'block';
                hideLoder();
            }, 3000);


            const UAEKYC = window.UAEKYC;
            if (!UAEKYC) {
                console.error("UAEKYC SDK not loaded");
                return;
            }

            const { startJourney } = UAEKYC;

            startJourney({
                journeyToken: journeyToken,
                language: "en",
                theme: "system",
                apiDomain: "uaekyc-api.digitaltrusttech.com",
                privacyPolicyUrl: "https://example.ae/privacy",
                logoUrl: appLogoUrl,
                accentColor: "#CFB16C"
            }, handleJourneyComplete);

            return;
        }


        //set qr code
        if (id == "QRCODE" || id == "WALLET") {
            id = "QRCODE";

            var imgElement = document.createElement("img");
            imgElement.src = QrCode;
            imgElement.classList.add("qr-code", "img-thumbnail", "img-responsive", "qr-image");

            document.getElementById("QRCODEdata").innerHTML = "";
            document.getElementById("QRCODEdata").appendChild(imgElement);

            setCookies();
            isUserVerifiedWalletQRCode();
        }

        if (id == "MOBILE_TOTP") {
            setCookies();
            codeVerfierInterval = setTimeout(isUserVerifiedCode, 500);
        }
        if (preid) {
            var div1 = $("#" + preid)
            div1.css("display", "none");
        }
        if (id === "WEB_FACE") {
            //Promise.all([
            //    faceapi.nets.tinyFaceDetector.loadFromUri('./models'),
            //    faceapi.nets.faceLandmark68Net.loadFromUri('./models'),
            //    faceapi.nets.faceRecognitionNet.loadFromUri('./models'),
            //    faceapi.nets.faceExpressionNet.loadFromUri('./models')

            //]).then(startVideo)
            //startVideo();
        }
        id = id.toUpperCase();
        errorDivId = id + "error";
        var userNameid = id + "userName";
        preid = id;
        var div = $("#" + id)
        if (div.length) {
            div.css("display", "flex");
        }
        else
            swal("Error", "The " + id + " Authentication Scheme is not available")
    }
}

function handleJourneyComplete(result) {
    const { Status, ErrorCode } = window.UAEKYC;

    console.log("Journey Status:", Status[result.status]);

    if (result.code) {
        console.log("Error Code:", ErrorCode[result.code]);
    }

    switch (result.status) {
        case Status.Success:
            authenticateUserAfterKYC();
            break;

        case Status.FaceVerificationFailed:
            swal("Error", "Face Verification Failed", "error");
            $("#uaeidDialog").show();
            break;

        default:
            $("#uaeidDialog").show();
    }
}

function authenticateUserAfterKYC() {
    $.ajax({
        type: "POST",
        url: LoginControllerUrl + "/AuthenticatUser",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({
            authenticationScheme: "ICP",
            authenticationData: "ICP",
            journeyToken: journeyToken,
            documentNumber: ""
        }),
        beforeSend: showLoder,
        complete: hideLoder,
        success: function (response) {
            console.log("Authenticate response:", response);

            if (response.success) {
                show();
            } else {
                swal("Authentication Failed", response.message, "error");
            }
        },
        error: ErrorHandler
    });
}

function ValidateNumber(userName) {

    if (userName.startsWith("+91")) {
        if (userName.length !== 13) return null;
    }
    else if (userName.startsWith("91")) {
        if (userName.length !== 12) return null;
        userName = "+" + userName;
    }

    else if (userName.startsWith("+971")) {
        if (userName.length !== 13) return null;
    }
    else if (userName.startsWith("971")) {
        if (userName.length !== 12) return null;
        userName = "+" + userName;
    }

    else if (userName.startsWith("0")) {

        if (userName.length === 10) {
            userName = userName.replace(/^0/, "+971");
        }
        else if (userName.length === 11) {
            userName = userName.replace(/^0/, "+91");
        }
        else {
            return null;
        }
    }

    else if (userName.length === 9) {
        userName = "+971" + userName;
    }
    else if (userName.length === 10) {
        userName = "+91" + userName;
    }
    else {
        return null;
    }

    return userName;
}

function VerifyUser() {
    if (!navigator.cookieEnabled) {
        document.getElementById("browserErrordiv").style.display = "flex"
        document.getElementById("browserError").innerHTML = "Your browser cookies option is disabled.\ Please enable cookies option \n Go browser setting -> Cookies and other site data -> Enable 'Allow all cookies'!";
        return false;
    }
    document.getElementById("USERerror").textContent = "";

    userName = document.getElementById("UserName").value;
    userName = userName.trim();

    const rememberMeElement = document.getElementById("rememberMe");

    let isChecked = rememberMeElement ? rememberMeElement.checked : false;

    if (!userName) {
        document.getElementById("USERerror").textContent = "Please enter the value";
        return true;

    } else {

        var type = getInputType(userName);
        if (type == 1) {

            userName = ValidateNumber(userName)
            if (userName == null) {
                var element = document.getElementById("username");
                document.getElementById("USERerror").textContent = "Invalid Mobile Number";
                return true;
            }
        }

        var userAgent = navigator.userAgent;
        var typeOfDevice = getDeviceType();

        $.ajax({
            type: 'POST',
            url: LoginControllerUrl + "/VerifyUser",
            dataType: 'json',
            data: {
                userInput: userName,
                type: type,
                clientId: client_id,
                clientType: clientType,
                userAgent: userAgent,
                typeOfDevice: typeOfDevice,
                rememberUser: isChecked
            },
            beforeSend: showLoder,
            complete: hideLoder,
            error: ErrorHandler,
            success: function (result, status, xhr) {
                if (result.success) {
                    userName = result.result.userName
                    if (result.result.mobileUser) {
                        authenticationmethods.classList.toggle("show-modal");
                    }
                    else {
                        result.result.authenticationSchemes.forEach(element => {
                            if (element == "PUSH_NOTIFICATION" || element == "UAEKYCFACE") {
                                PushNotificationCode = result.result.randomCode;
                            }
                            if (element == "ICP") {
                                journeyToken = result.result.journeyToken;
                            }
                            if (element == "FIDO2") {
                                Fido2Options = result.result.fido2Options;
                            }
                            if (element == "QRCODE" || element == "WALLET") {
                                QrCode = result.result.qrCode;
                                if (element == "WALLET") {
                                    VerifierCode = result.result.verifierCode;
                                }
                            }
                            authScheme.push(element);
                        });

                        $('.user-email-box').text(userName);
                        authScheme.reverse();
                        show();
                    }

                } else {

                    if (result.errorCode == 100) {
                        swal({ type: 'info', title: "Invalid Client", text: result.message }, function (isConfirm) {
                            if (isConfirm) {
                                clearInterval(codeVerfierInterval);
                                deleteCookies();
                                //if (redirectUrl == "")
                                //    window.location.href = LoginControllerUrl + "/Error?error=Invalid Client&error_description=" + result.message
                                //else
                                //    window.location.href = redirectUrl + "?error=Invalid Client&error_description=" + result.message + "&state=" + state
                                if (!redirectUrl) {
                                    const errorUrl = new URL("/Error", LoginControllerUrl);
                                    errorUrl.searchParams.set("error", "Invalid Client");
                                    errorUrl.searchParams.set("error_description", result.message);
                                    window.location.href = errorUrl.href;
                                } else {
                                    const url = new URL(redirectUrl);
                                    errorUrl.searchParams.set("error", "Invalid Client");
                                    errorUrl.searchParams.set("error_description", result.message);
                                    url.searchParams.set("state", state || "");
                                    window.location.href = url.href;
                                }
                            }
                        });
                    }
                    else if (result.errorCode == 101) {
                        swal({ type: 'info', title: "Inactive Client", text: result.message }, function (isConfirm) {
                            if (isConfirm) {
                                clearInterval(codeVerfierInterval);
                                deleteCookies();
                                //if (redirectUrl == "")
                                //    window.location.href = LoginControllerUrl + "/Error?error=Inactive Client&error_description=" + result.message
                                //else
                                //    window.location.href = redirectUrl + "?error=Inactive Client&error_description=" + result.message + "&state=" + state
                                if (!redirectUrl) {
                                    const errorUrl = new URL("/Error", LoginControllerUrl);
                                    errorUrl.searchParams.set("error", "inactive_client");
                                    errorUrl.searchParams.set("error_description", result.message || "");
                                    window.location.href = errorUrl.href;
                                } else {
                                    const url = new URL(redirectUrl);
                                    url.searchParams.set("error", "inactive_client");
                                    url.searchParams.set("error_description", result.message || "");
                                    url.searchParams.set("state", state || "");
                                    window.location.href = url.href;
                                }
                            }
                        });
                    }
                    else if (result.errorCode == 103) {
                        if (client_id != "ONBN53C1ydOLfvrJllgxuj9PwcyrpR5aOg5idWxnEXXwSqFe") {
                            swal({
                                title: result.message,
                                text: "Your Account is Suspended for " + result.accountlocktime + " hours ,Please contact Administrator ",
                                button: "Close",
                                icon: "info",
                                showCancelButton: true,
                                confirmButtonText: "Self Activation Guidelines",
                                cancelButtonText: "Cancel",
                                closeOnConfirm: true,
                                closeOnCancel: true
                            }, function (isConfirm) {
                                if (isConfirm) {
                                    activateSuspendedAccount.classList.toggle("show-modal");
                                } else {
                                    swal.close();
                                }
                            });
                        }
                        else {
                            swal({ type: 'info', title: "Account Suspended", text: result.message }, function (isConfirm) {
                                if (isConfirm) {
                                    clearInterval(codeVerfierInterval);
                                    deleteCookies();
                                }
                            });
                        }
                    }

                    else if (result.errorCode == 104) {
                        swal({ type: 'info', title: "Inactive Account", text: result.message }, function (isConfirm) {
                            if (isConfirm) {
                                clearInterval(codeVerfierInterval);
                                deleteCookies();
                            }
                        });
                    }
                    else if (result.errorCode == 105) {

                        swal({
                            type: 'info',
                            title: result.message,
                            text: "Click on a button to proceed",
                            confirmButtonText: "Ok",
                            closeOnConfirm: false
                        }, function (isConfirm) {
                            if (isConfirm) {
                                clearInterval(codeVerfierInterval);
                                deleteCookies();
                                window.location.reload();
                            }
                        });

                    }
                    else {
                        if (result.errorCode == 102 && client_id != "ONBN53C1ydOLfvrJllgxuj9PwcyrpR5aOg5idWxnEXXwSqFe") {
                            swal({
                                title: result.message,
                                text: "Do you want to see registration guidelines",
                                button: "Close",
                                icon: "info",
                                showCancelButton: true,
                                confirmButtonText: "Registration Guidelines",
                                cancelButtonText: "Cancel",
                                closeOnConfirm: true,
                                closeOnCancel: true
                            }, function (isConfirm) {
                                if (isConfirm) {
                                    modal.classList.toggle("show-modal");
                                } else {
                                    swal.close();
                                }
                            });

                        } else {
                            document.getElementById("USERerror").textContent = result.message;
                        }


                    }
                    return false;

                }
            }
        })
    }
}

function changeAuthScheme(authenticationScheme) {
    swal.close();
    $.ajax({
        url: LoginControllerUrl + "/ChangeAuthScheme",
        type: 'GET',
        data: { authScheme: authenticationScheme },
        beforeSend: showLoder,
        complete: hideLoder,
        error: ErrorHandler,
        success: function (response) {
            authScheme = [];
            response.result.authenticationSchemes.forEach(element => {
                if (element == "WALLET") {
                    QrCode = response.result.qrCode;
                    if (element == "WALLET") {
                        VerifierCode = response.result.verifierCode;
                    }
                }
                authScheme.push(element);
            });
            authScheme.reverse();
            show();
        }
    });
}

function changeAssistedAuthScheme(authenticationScheme) {
    $.ajax({
        url: LoginControllerUrl + "/ChangeAuthScheme",
        type: 'GET',
        data: { authScheme: authenticationScheme },
        beforeSend: showLoder,
        complete: hideLoder,
        error: ErrorHandler,
        success: function (response) {
            authScheme = [];
            response.result.authenticationSchemes.forEach(element => {
                if (element == "WALLET") {
                    QrCode = response.result.qrCode;
                    if (element == "WALLET") {
                        VerifierCode = response.result.verifierCode;
                    }
                }
                authScheme.push(element);
            });
            authScheme.reverse();
            show();
        }
    });
}

function authenticate(FormID) {
    if (!navigator.cookieEnabled) {
        document.getElementById("browserErrordiv").style.display = "flex"
        document.getElementById("browserError").innerHTML = "Your browser cookies option is disabled.\ Please enable cookies option \n Go browser setting -> Cookies and other site data -> Enable 'Allow all cookies'!";
        return false;
    }
    if (currentAuthScheme == "PASSWORD") {
        document.getElementById("PASSWORDerror").textContent = "";
    } else if (currentAuthScheme == "TOTP") {
        document.getElementById("TOTPerror").textContent = "";
    }
    var password = "";
    if (currentAuthScheme == "TOTP") {
        password = document.getElementById("UserTOTP").value;
    }
    if (currentAuthScheme == "PASSWORD") {
        password = document.getElementById("UserPASSWORD").value;
    }
    if (!password) {

        //var id = form.get("authScheme").toLowerCase();
        //var element = document.getElementById(id);
        //element.classList.add("invalid");
        //document.getElementById("password").style.boxShadow = "0px 10px 10px -1px #bfd01f";
        //document.getElementById(errorDivId).className = "danger";
        //document.getElementById(errorDivId).style.visibility = "visible";
        //var msg = (id != "email") ? id : id + " otp";
        //document.getElementById(errorDivId).innerHTML = "Please enter " + msg
        if (currentAuthScheme == "PASSWORD") {
            document.getElementById("PASSWORDerror").textContent = "Please enter password";
        } else if (currentAuthScheme == "TOTP") {
            document.getElementById("TOTPerror").textContent = "Please enter totp";
        }
        return false;

    }
    else {
        $.ajax({
            type: 'POST',
            url: LoginControllerUrl + "/AuthenticatUser",
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify({
                authenticationScheme: currentAuthScheme,
                authenticationData: password
            }),
            beforeSend: showLoder,
            complete: hideLoder,
            error: ErrorHandler,
            success: function (result, status, xhr) {
                if (result.success) {
                    if (result && result.result && result.result.randomCode) {
                        PushNotificationCode = result.result.randomCode;
                    }
                    //if (document.getElementById(errorDivId).style.visibility == "visible") {
                    //    document.getElementById(errorDivId).style.visibility = "hidden"
                    //    document.getElementById(errorDivId).className = "info"
                    //}
                    show();
                }
                else {
                    if (result.errorCode == 106) {
                        swal({ type: 'info', title: result.message, text: "Click on 'Ok' button for login again!" }, function (isConfirm) {
                            if (isConfirm) {
                                deleteCookies();
                                window.location.reload();
                            }
                        });
                    }
                    else if (result.errorCode == 103) {
                        swal({ type: 'info', title: "Account Suspended", text: result.message }, function (isConfirm) {
                            if (isConfirm) {
                                clearInterval(codeVerfierInterval);
                                deleteCookies();
                                window.location.reload();
                            }
                        });
                    } else {
                        //document.getElementById(errorDivId).className = "danger";
                        //document.getElementById(errorDivId).style.visibility = "visible"
                        //document.getElementById(errorDivId).innerHTML = result.message;
                        //document.getElementById("USERerror").value = result.message;
                        if (currentAuthScheme == "PASSWORD") {
                            document.getElementById("PASSWORDerror").textContent = result.message;
                        } else if (currentAuthScheme == "TOTP") {
                            document.getElementById("TOTPerror").textContent = result.message;
                        }
                    }
                    return false;
                }
            }

        })
    }
}

function isUserVerifiedCode() {

    if (!navigator.cookieEnabled) {
        document.getElementById("browserErrordiv").style.display = "flex"
        document.getElementById("browserError").innerHTML = "Your browser cookies option is disabled.\ Please enable cookies option \n Go browser setting -> Cookies and other site data -> Enable 'Allow all cookies'!";
        return false;
    }

    $.ajax({
        type: 'post',
        url: LoginControllerUrl + "/IsUserVerifiedCode",
        success: function (result, status, xhr) {
            if (result.success && result.status == "success") {
                clearInterval(codeVerfierInterval);
                stopTimer();
                deleteCookies();
                const verifiedemail = $(".userEmail").html();
                $("#verifiedUserEmail").html(verifiedemail);
                show();

            }
            else {

                if (!result.success && result.status == "failed") {

                    stopTimer();

                    if (result.errorCode == 107 || result.errorCode == 106) {
                        swal({ type: 'info', title: result.message, text: "Click on 'Ok' button for login again!" }, function (isConfirm) {
                            if (isConfirm) {
                                clearInterval(codeVerfierInterval);
                                deleteCookies();
                                window.location.reload();
                            }
                        });
                    } else {
                        document.getElementById("wrongCredentialTitle").innerText = result.message;

                        document.getElementById("wrongCredentialBody").innerText =
                            result.message +
                            "\nDo you want to reauthenticate?\nPlease click on the 'Resend notification' button to resend notification.";

                        document.getElementById("wrongCredentialModal").classList.add("active");
                    }
                }
                if (result.status == "stop") {

                    clearInterval(codeVerfierInterval);
                    stopTimer();
                    document.getElementById("customTimeoutModal").classList.add("active");
                }
            }
        },
        complete: function (data) {
            if (data.status == 200 && data.responseJSON.success && data.responseJSON.status == "pending") {
                codeVerfierInterval = setTimeout(isUserVerifiedCode, 500);
            }
        },
        error: ErrorHandler
    })

};

function isUserVerifiedQRCode() {
    if (!navigator.cookieEnabled) {
        document.getElementById("browserErrordiv").style.display = "flex";
        document.getElementById("browserError").innerHTML = "Your browser cookies option is disabled. Please enable cookies option \n Go browser setting -> Cookies and other site data -> Enable 'Allow all cookies'!";
        return false;
    }

    $.ajax({
        type: 'post',
        url: LoginControllerUrl + "/IsUserVerifiedQRCode",
        success: function (result, status, xhr) {
            if (result.success && result.status == "success") {
                deleteCookies();
                show();
            } else {
                if (!result.success && result.status == "failed") {
                    if (result.errorCode == 107 || result.errorCode == 106) {
                        swal(
                            { type: 'info', title: result.message, text: "Click on 'Ok' button for login again!" },
                            function (isConfirm) {
                                if (isConfirm) {
                                    deleteCookies();
                                    window.location.reload();
                                }
                            }
                        );
                    }
                }
            }
        },
        complete: function (data) {
            if (data.status == 200 && data.responseJSON.success && data.responseJSON.status == "pending") {
                codeVerfierInterval = setTimeout(isUserVerifiedQRCode, 500);
            }
        },
        error: ErrorHandler
    });
}

function isUserVerifiedWalletQRCode() {
    if (!navigator.cookieEnabled) {
        document.getElementById("browserErrordiv").style.display = "flex";
        document.getElementById("browserError").innerHTML = "Your browser cookies option is disabled. Please enable cookies option \n Go browser setting -> Cookies and other site data -> Enable 'Allow all cookies'!";
        return false;
    }

    $.ajax({
        type: 'post',
        url: LoginControllerUrl + "/IsUserVerified",
        success: function (result, status, xhr) {
            if (result && result.success) {
                if (result.result && result.result.randomCode) {
                    PushNotificationCode = result.result.randomCode;
                }
                show();
            } else {
                swal(
                    {
                        type: 'info', title: result.message,
                        text: "Do you want to reauthenticate!\nPlease click on the Rescan",
                        showCancelButton: true,
                        confirmButtonText: "Rescan",
                        cancelButtonText: "Cancel",
                        closeOnConfirm: true,
                        closeOnCancel: true
                    },
                    function (isConfirm) {
                        if (isConfirm) {
                            generateQrCodeAgain();
                        }
                        else {
                            deleteCookies();
                            if (redirectUrl == "")
                                window.location.href = LoginControllerUrl + "/Error?error=access_denied&error_description=user not authenticated"
                            else
                                window.location.href = redirectUrl + "?error=access_denied&error_description=user not authenticated&state=" + state
                        }
                    }
                );
            }
        },
        error: ErrorHandler
    });
}

function sendCodeAgain() {

    if (!navigator.cookieEnabled) {
        document.getElementById("browserErrordiv").style.display = "flex"
        document.getElementById("browserError").innerHTML = "Your browser cookies option is disabled.\ Please enable cookies option \n Go browser setting -> Cookies and other site data -> Enable 'Allow all cookies'!";
        return false;
    }

    $.ajax({
        type: 'post',
        url: LoginControllerUrl + "/SendPushNotification",
        beforeSend: showLoder,
        complete: hideLoder,
        error: ErrorHandler,
        success: function (result, status, xhr) {
            if (result.success) {

                if (result.errorCode == 108) {

                    swal({
                        title: "Success!",
                        text: "You are already AUTHENTICATED!",
                        button: "Close",
                        icon: "success",
                        timer: 5000,
                        buttons: {
                            confirm: {
                                text: "OK",
                                value: true,
                                visible: true,
                                className: "",
                                closeModal: true
                            },
                            cancel: {
                                text: "Cancel",
                                value: false,
                                visible: true,
                                className: "",
                                closeModal: true,
                            }
                        }
                    }, function (isConfirm) {
                        if (isConfirm) {
                            clearInterval(codeVerfierInterval);
                            deleteCookies();
                            show();
                        } else {
                            swal.close();
                            clearInterval(codeVerfierInterval);
                            deleteCookies();
                            show();
                        }
                    });


                } else {

                    swal({
                        title: "Success!",
                        text: "Code Send Successfully!",
                        timer: 2000,
                        type: "success",
                        showConfirmButton: false
                    });

                    if (document.getElementById(errorDivId).style.visibility == "visible") {
                        document.getElementById(errorDivId).className = "info";
                        document.getElementById(errorDivId).style.visibility = "hidden"
                        document.getElementById(errorDivId).innerHTML = "";
                    }
                    document.getElementById("PushNotificationCode").innerText = result.randomCode;
                    codeVerfierInterval = setTimeout(isUserVerifiedCode, 500);
                    setCookies();
                    startTimer1();
                }
            }
            else {

                if (result.errorCode == 107 || result.errorCode == 106) {

                    swal({
                        title: result.message,
                        text: "Click on 'Ok' button for login again!",
                        button: "Close",
                        icon: "info",
                        timer: 10000,
                        buttons: {
                            confirm: {
                                text: "OK",
                                value: true,
                                visible: true,
                                className: "",
                                closeModal: true
                            },
                            cancel: {
                                text: "Cancel",
                                value: false,
                                visible: true,
                                className: "",
                                closeModal: true,
                            }
                        }
                    }, function (isConfirm) {
                        if (isConfirm) {
                            deleteCookies();
                            window.location.reload();
                        } else {
                            swal.close();
                            deleteCookies();
                            window.location.reload();
                        }
                    });
                }
                else if (result.errorCode == 103) {

                    swal({
                        title: "Account Suspended",
                        text: result.message,
                        button: "Close",
                        icon: "info",
                        timer: 10000,
                        buttons: {
                            confirm: {
                                text: "OK",
                                value: true,
                                visible: true,
                                className: "",
                                closeModal: true
                            },
                            cancel: {
                                text: "Cancel",
                                value: false,
                                visible: true,
                                className: "",
                                closeModal: true,
                            }
                        }
                    }, function (isConfirm) {
                        if (isConfirm) {
                            clearInterval(codeVerfierInterval);
                            deleteCookies();
                            //if (redirectUrl == "")
                            //    window.location.href = LoginControllerUrl + "/Error?error=account_suspended&error_description=" + result.message
                            //else
                            //    window.location.href = redirectUrl + "?error=account_suspended&error_description=" + result.message + "&state=" + state
                            if (!redirectUrl) {

                                const errorUrl = new URL("/Error", LoginControllerUrl);
                                errorUrl.searchParams.set("error", "account_suspended");
                                errorUrl.searchParams.set("error_description", result.message || "");

                                window.location.href = errorUrl.href;

                            } else {

                                const url = new URL(redirectUrl);
                                url.searchParams.set("error", "account_suspended");
                                url.searchParams.set("error_description", result.message || "");
                                url.searchParams.set("state", state || "");

                                window.location.href = url.href;
                            }
                        } else {
                            swal.close();
                            clearInterval(codeVerfierInterval);
                            deleteCookies();
                            //if (redirectUrl == "")
                            //    window.location.href = LoginControllerUrl + "/Error?error=account_suspended&error_description=" + result.message
                            //else
                            //    window.location.href = redirectUrl + "?error=account_suspended&error_description=" + result.message + "&state=" + state
                            if (!redirectUrl) {

                                const errorUrl = new URL("/Error", LoginControllerUrl);
                                errorUrl.searchParams.set("error", "account_suspended");
                                errorUrl.searchParams.set("error_description", result.message || "");

                                window.location.href = errorUrl.href;

                            } else {

                                const url = new URL(redirectUrl);
                                url.searchParams.set("error", "account_suspended");
                                url.searchParams.set("error_description", result.message || "");
                                url.searchParams.set("state", state || "");

                                window.location.href = url.href;
                            }
                        }
                    });
                }
                else {
                    swal({
                        title: result.message,
                        text: `Do you want to reauthenticate!\nPlease click on the 'Resend notification' button\n to resend notification.`,
                        type: "error",
                        showCancelButton: true,
                        confirmButtonText: "Resend Notification",
                        cancelButtonText: "Cancel",
                        closeOnConfirm: false,
                        closeOnCancel: true
                    }, function (isConfirm) {
                        if (isConfirm) {
                            sendCodeAgain();
                        }
                        else {
                            clearInterval(codeVerfierInterval);

                            deleteCookies();
                            //if (redirectUrl == "")
                            //    window.location.href = LoginControllerUrl + "/Error?error=access_denied&error_description=user not authenticated"
                            //else
                            //    window.location.href = redirectUrl + "?error=access_denied&error_description=user not authenticated&state=" + state

                            if (!redirectUrl) {

                                const errorUrl = new URL("/Error", LoginControllerUrl);
                                errorUrl.searchParams.set("error", "access_denied");
                                errorUrl.searchParams.set("error_description", "user not authenticated");

                                window.location.href = errorUrl.href;

                            } else {

                                const url = new URL(redirectUrl);
                                url.searchParams.set("error", "access_denied");
                                url.searchParams.set("error_description", "user not authenticated");
                                url.searchParams.set("state", state || "");

                                window.location.href = url.href;
                            }
                        }
                    });
                }
            }
        }

    })
}

function generateQrCodeAgain() {

    if (!navigator.cookieEnabled) {
        document.getElementById("browserErrordiv").style.display = "flex"
        document.getElementById("browserError").innerHTML = "Your browser cookies option is disabled.\ Please enable cookies option \n Go browser setting -> Cookies and other site data -> Enable 'Allow all cookies'!";
        return false;
    }

    $.ajax({
        type: 'get',
        url: LoginControllerUrl + "/GenerateQrCode",
        beforeSend: showLoder,
        complete: hideLoder,
        success: function (result, status, xhr) {
            if (result.success) {
                var imgElement = document.createElement("img");
                imgElement.src = result.result;
                imgElement.classList.add("qr-code", "img-thumbnail", "img-responsive", "qr-image");
                document.getElementById("QRCODEdata").innerHTML = "";
                document.getElementById("QRCODEdata").appendChild(imgElement);
                isUserVerifiedWalletQRCode();
            }
            else {
                swal(
                    {
                        type: 'info', title: result.message,
                        text: "Do you want to reauthenticate!\nPlease click on the Rescan",
                        showCancelButton: true,
                        confirmButtonText: "Rescan",
                        cancelButtonText: "Cancel",
                        closeOnConfirm: true,
                        closeOnCancel: true
                    },
                    function (isConfirm) {
                        if (isConfirm) {
                            generateQrCodeAgain();
                        }
                        else {
                            deleteCookies();
                            //if (redirectUrl == "")
                            //    window.location.href = LoginControllerUrl + "/Error?error=access_denied&error_description=user not authenticated"
                            //else
                            //    window.location.href = redirectUrl + "?error=access_denied&error_description=user not authenticated&state=" + state

                            if (!redirectUrl) {

                                const errorUrl = new URL("/Error", LoginControllerUrl);
                                errorUrl.searchParams.set("error", "access_denied");
                                errorUrl.searchParams.set("error_description", "user not authenticated");

                                window.location.href = errorUrl.href;

                            } else {

                                const url = new URL(redirectUrl);
                                url.searchParams.set("error", "access_denied");
                                url.searchParams.set("error_description", "user not authenticated");
                                url.searchParams.set("state", state || "");

                                window.location.href = url.href;
                            }
                        }
                    }
                );
            }
        },
        error: ErrorHandler
    })
}

function showLoder() {
    $('#overlay').show();
}

function hideLoder() {
    $('#overlay').hide();
}

function ErrorHandler(xhr, status, error) {

    switch (xhr.status) {
        case 400:
            document.getElementById(errorDivId).className = "danger";
            document.getElementById(errorDivId).style.visibility = "visible"
            document.getElementById(errorDivId).textContent = xhr.responseJSON.message
            break;
        case 403:
            swal("Forbidden", "You do not have access to this resource", "error");
            break;
        case 404:
            swal("Abort", "The resource you requested could not be found", "error");
            break;

        case 500:
            swal({ title: "Server Error", text: "Internal Server error Occurred", type: "error" }, function (isConfirm) {
                if (isConfirm) {
                    deleteCookies();
                    window.location.reload();
                }
            });
            break;

        case 502:
            swal("Bad Gateway", "Invalid response", "error");
            break;
        case 503:
            swal("Service unavailable", "The Service is currently unavailable, please try after sometime.", "error");
            break;
        default:
            errors = ["Bad Request", "Bad Gateway", "Not Found", "Internal Server Error", "Forbidden", "Unauthorized", "Service Unavailable"]
            if (xhr.readyState == 0 && xhr.status == 0 && xhr.responseJSON == undefined) {
                if (!errors.includes(error)) {

                    if (window.navigator.onLine) {
                        if (error == "") {
                            error = "Something went wrong, Please Try later";
                        }

                        swal({ type: 'error', title: "Server Down", text: error }, function (isConfirm) {
                            if (isConfirm) {
                                deleteCookies();
                                window.location.reload();
                            }
                        });
                    } else {
                        swal({ type: 'info', title: "No Internet", text: "Check your network connection!" }, function (isConfirm) {
                            if (isConfirm) {
                                deleteCookies();
                                window.location.reload();
                            }
                        });
                    }
                } else {
                    swal({
                        type: 'error', title: "Error", text: "Something went wrong, please try later"
                    }, function (isConfirm) {
                        if (isConfirm) {
                            window.location.reload();
                        }
                    });
                }
            }
            break;
    }
}

function postForm(path, params, method) {
    clearInterval(codeVerfierInterval);

    method = method || 'post';

    var form = document.createElement('form');
    form.setAttribute('method', method);
    form.setAttribute('action', path);

    for (var key in params) {
        if (params.hasOwnProperty(key)) {
            var hiddenField = document.createElement('input');
            hiddenField.setAttribute('type', 'hidden');
            hiddenField.setAttribute('name', key);
            hiddenField.setAttribute('value', params[key]);

            form.appendChild(hiddenField);
        }
    }

    document.body.appendChild(form);
    form.submit();
    $('#overlay1').show();
}

function ForgotPassword() {

    var username = document.getElementById("PASSWORDuserName").value;
    var userType = getInputType(username);
    //window.location.href = LoginControllerUrl + "/ForgotPassword/" + username + "?UserType=" + userType
    var url = new URL(LoginControllerUrl + "/ForgotPassword/" + username, window.location.origin);

    url.searchParams.append("UserType", userType);

    window.location.href = url.toString();
}

function getInputType(userName) {
    var length = userName.length;

    // Email
    if (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(userName))
        return 2;

    // India (+91)
    else if (/^(\+91|91|0)?[6-9]\d{9}$/.test(userName))
        return 1;

    // Uganda (+256)
    else if (/^(\+256|256|0)?\d{9}$/.test(userName))
        return 1;

    // UAE (+971)
    else if (/^(\+971|971|0)?5\d{8}$/.test(userName))
        return 1;

    // Emirates ID
    else if (/^784-?\d{4}-?\d{7}-?\d$/.test(userName))
        return 6;

    // 14-character alphanumeric
    else if (length === 14 && /^[A-Z0-9]{14}$/.test(userName))
        return 3;

    else if (/^[A-Z]{1,4}[0-9]{5,16}$/.test(userName))
        return 4;

    else
        return 5;
}

function getDeviceType() {
    const ua = navigator.userAgent;
    if (/(tablet|ipad|playbook|silk)|(android(?!.*mobi))/i.test(ua)) {
        return "tablet";
    }
    if (
        /Mobile|iP(hone|od)|Android|BlackBerry|IEMobile|Kindle|Silk-Accelerated|(hpw|web)OS|Opera M(obi|ini)/.test(
            ua
        )
    ) {
        return "mobile";
    }
    return "desktop";
};

function setCookies() {
    var expires = (new Date(Date.now() + 60 * 1000)).toGMTString();
    document.cookie = "NotificationVerifierValidTime=yes; expires=" + expires + ";path=/;"
}

function deleteCookies() {
    document.cookie = "NotificationVerifierValidTime=yes; expires=" + new Date(0).toUTCString();
}

function startTimer1() {
    var timerdiv = document.getElementById("timerDiv");
    var display = document.getElementById('timer');

    timerdiv.style.visibility = "visible";
    var timer = 60;

    TimerInterval = setInterval(function () {

        display.textContent = (timer >= 10) ? timer : "0" + timer;

        if (--timer < -1) {
            timerdiv.style.visibility = "hidden";
            display.textContent = "";
            clearInterval(TimerInterval);
        }
    }, 1000);
}

function startTimer2() {
    var timerdiv = document.getElementById("timerDiv");
    var display = document.getElementById('timer');

    timerdiv.style.visibility = "visible";
    var timer = 60;

    TimerInterval = setInterval(function () {

        display.textContent = (timer >= 10) ? timer : "0" + timer;

        if (--timer < -1) {
            timerdiv.style.visibility = "hidden";
            display.textContent = "";
            clearInterval(TimerInterval);
        }
    }, 1000);
}

function stopTimer() {
    //document.getElementById("timerDiv").style.visibility = "hidden";
    document.getElementById('timer').textContent = "";
    clearInterval(TimerInterval);
}

function startVideo() {

    navigator.getUserMedia(
        { video: {} },
        stream => {
            video.srcObject = stream;
            videoStream = stream;
        },
        err => {
            console.error(err)
            if (err.name === 'NotFoundError' || err.name === 'DevicesNotFoundError') {

                swal({ type: 'info', title: "Camera Not Found", text: 'No camera device found.' }, function (isConfirm) {
                    if (isConfirm) {

                    }
                });

            } else if (err.name === 'NotAllowedError' || err.name === 'PermissionDeniedError') {

                swal({ type: 'info', title: "Camera Permission", text: 'Permission to access camera was denied.' }, function (isConfirm) {
                    if (isConfirm) {

                    }
                });
            }
            else {
                alert();
                swal({ type: 'info', title: "Camera Error", text: 'Error accessing camera: ' + err.message }, function (isConfirm) {
                    if (isConfirm) {

                    }
                });
            }
        }
    );

    document.getElementById('video').style.display = 'block';
    timeoutId = setTimeout(() => {
        clearInterval(detectionid);
        if (videoStream) {
            videoStream.getTracks().forEach(track => track.stop());

        }
        video.style.display = 'none';

        setTimeout(() => {
            document.getElementById('faceinstructions').style.display = 'none';
            /*document.getElementById('camblock').style.display = 'block';*/
            /*document.getElementById('recapturediv').style.display = 'block';*/
            swal({
                type: 'info',
                title: "Timeout",
                text: 'Time Limit Exceeded',
                showCancelButton: true,
                confirmButtonText: "Retry",
                cancelButtonText: "Cancel"
            }, function (isConfirm) {
                if (isConfirm) {
                    startVideo();
                } else {
                    location.reload();
                }
            });
        }, 100)
    }, 20000);
}

function stopVideo() {
    if (videoStream) {
        videoStream.getTracks().forEach(track => track.stop());
        videoStream = null;
    }

    if (detectionid) {
        clearInterval(detectionid);
        detectionid = null;
    }

    clearTimeout(timeoutId);
    resetCountdown();

    video.style.display = "none";
}


video.addEventListener('play', () => {

    const videoCanvas = document.createElement('canvas');
    videoCanvas.width = 330;
    videoCanvas.height = 233;
    const videoCtx = videoCanvas.getContext('2d');

    detectionid = setInterval(async () => {

        if (isCaptured) return;

        const detections = await faceapi.detectAllFaces(
            video,
            new faceapi.TinyFaceDetectorOptions({ scoreThreshold: 0.5 })
        );

        if (detections.length === 1) {
            faceInstructions.style.display = "none";

            if (!isCountingDown) {
                lastDetectionValid = true;
                startCountdown(captureFaceAndAuthenticate);
            }
        }

        else {
            faceInstructions.style.display = "block";
            faceInstructions.innerText =
                detections.length > 1
                    ? "Multiple faces detected. Please stay alone."
                    : "No face detected. Look into the camera.";

            if (isCountingDown) {
                resetCountdown();
            }

            lastDetectionValid = false;
        }

    }, 300);
});

const observer = new MutationObserver(mutationsList => {
    for (let mutation of mutationsList) {
        if (mutation.type === 'attributes' && mutation.attributeName === 'style') {
            const visibility = video.style.display;
            if (visibility === 'none') {
                setTimeout(() => {
                }, 500);

            }
        }
    }
});

observer.observe(video, { attributes: true });

function showProcessing() {
    document.getElementById('processing').classList.remove('hidden');
}

function hideProcessing() {
    document.getElementById('processing').classList.add('hidden');
}

function AssistedAuthentication(method) {
    authenticationmethods.classList.toggle("show-modal");
    changeAssistedAuthScheme(method);
}

function checkOnetoNEnabled() {
    $.ajax({
        url: LoginControllerUrl + "/IsOnetoNEnabledForClient",
        type: 'GET',
        data: { clientId: client_id },
        beforeSend: showLoder,
        complete: hideLoder,
        error: ErrorHandler,
        success: function (response) {
            if (response.success) {
                isOnetoNEnabled = response.result;
                if (isOnetoNEnabled) {
                    authScheme = ["WEB_FACE"];
                    authScheme.reverse();
                    show();
                }
            }
            else {
                swal(
                    { type: 'info', title: result.message, text: "Click on 'Ok' button for login again!" },
                    function (isConfirm) {
                        if (isConfirm) {
                            deleteCookies();
                            window.location.reload();
                        }
                    }
                );
            }
        }
    });
}

function changeUser() {
    $.ajax({
        url: LoginControllerUrl + "/ChangeUser",
        type: 'POST',
        success: function () {
            location.reload();
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function getInitials(name) {
    let parts = name.trim().split(" ");

    if (parts.length === 1) {
        return parts[0].substring(0, 2).toUpperCase();
    } else {
        return parts.map(p => p.charAt(0)).join("").toUpperCase();
    }
}

function authenticateFace(base64Data) {

    $.ajax({
        type: 'POST',
        url: LoginControllerUrl + "/AuthenticatUser",
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({
            authenticationScheme: "WEB_FACE",
            authenticationData: base64Data,
            clientId: client_id
        }),
        success: function (result) {
            if (result.success) {
                show();
            } else {
                swal({
                    type: 'info',
                    title: "Face Authentication Failed",
                    text: result.message,
                    showCancelButton: true,
                    confirmButtonText: "Retry",
                    cancelButtonText: "Cancel"
                }, function (isConfirm) {
                    hideProcessing();
                    captured = 0;
                    if (isConfirm) {
                        resetFaceState();
                        hideProcessing();
                        startVideo();
                    } else {
                        location.reload();
                    }
                });
            }
        },
        error: function () {
            hideProcessing();
            captured = 0;
            swal("Error", "Face authentication failed", "error");
        }
    });
}

function startCountdown(captureCallback) {
    if (isCountingDown || isCaptured) return;

    isCountingDown = true;
    let count = 3;

    countdownText.innerText = count;
    countdownEl.classList.remove("hidden");

    countdownTimer = setInterval(() => {

        if (!lastDetectionValid) {
            resetCountdown();
            return;
        }

        count--;
        countdownText.innerText = count;

        if (count === 0) {
            clearInterval(countdownTimer);
            countdownTimer = null;

            countdownEl.classList.add("hidden");
            isCountingDown = false;
            isCaptured = true;

            captureCallback();
        }

    }, 1000);
}

function resetCountdown() {
    if (countdownTimer) {
        clearInterval(countdownTimer);
        countdownTimer = null;
    }
    isCountingDown = false;
    lastDetectionValid = false;
    countdownEl.classList.add("hidden");
}

function captureFaceAndAuthenticate() {
    const videoCanvas = document.createElement("canvas");
    videoCanvas.width = 330;
    videoCanvas.height = 233;

    const ctx = videoCanvas.getContext("2d");
    ctx.drawImage(video, 0, 0, videoCanvas.width, videoCanvas.height);

    const base64Image = videoCanvas
        .toDataURL("image/png")
        .replace("data:image/png;base64,", "");

    stopVideo();
    showProcessing();

    authenticateFace(base64Image);
}

function resetFaceState() {
    isCaptured = false;
    isCountingDown = false;
    lastDetectionValid = false;

    resetCountdown();

    if (detectionid) {
        clearInterval(detectionid);
        detectionid = null;
    }
}

init();