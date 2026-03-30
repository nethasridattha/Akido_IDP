document.getElementById("FIDO2Form").addEventListener("submit", handleSignInSubmit);

async function handleSignInSubmit(event, Fido2Options) {
    event.preventDefault();

    $("#overlay2").show();

    const errorEl = document.getElementById("FIDO2error");
    if (!errorEl.classList.contains("hide")) {
        errorEl.classList.remove("show");
        errorEl.classList.add("hide");
    }

    if (typeof Fido2Options !== "string") {
        Fido2Options = this.FIDOOption.value;
    }

    const publicKeyOptions = JSON.parse(Fido2Options);

    const challenge = publicKeyOptions.challenge
        .replaceAll("-", "+")
        .replaceAll("_", "/");

    publicKeyOptions.challenge = Uint8Array.from(
        atob(challenge),
        c => c.charCodeAt(0)
    );

    publicKeyOptions.allowCredentials.forEach(function (listItem) {
        const fixedId = listItem.id
            .replaceAll("_", "/")
            .replaceAll("-", "+");

        listItem.id = Uint8Array.from(
            atob(fixedId),
            c => c.charCodeAt(0)
        );
    });

    let credential;

    try {
        credential = await navigator.credentials.get({ publicKey: publicKeyOptions });
        $("#overlay2").show();

        try {
            const response = await verifyAssertionWithServer(credential, publicKeyOptions);

            if (!response.success) {
                $("#overlay2").hide();
                showError(response.message);
            } else {
                $("#overlay2").hide();
                $("#overlay1").show();

                $("#registerBtn").attr("disabled", true).css("opacity", "0.5");
                $("#cancle").attr("disabled", true).css("opacity", "0.5");

                show();
            }

        } catch (e) {
            $("#overlay2").hide();
            showError("Something went wrong.! try again.");
        }

    } catch (err) {
        $("#overlay2").hide();

        swal({
            title: "Authentication Failed",
            text: "The operation either timed out or was cancled. We couldn’t verify you or the security key you use. please try again",
            button: "Close",
            icon: "error",
            timer: 4000,
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
                    closeModal: true
                }
            }
        }, function (isConfirm) {
            if (isConfirm) {
                document.getElementById("registerBtn").innerText = "Try Again";
                document.getElementById("cancle").style.display = "block";
            } else {
                swal.close();
                document.getElementById("registerBtn").innerText = "Try Again";
                document.getElementById("cancle").style.display = "block";
            }
        });
    }
}


$("#cancle").click(function () {
    if (redirectUrl === "") {
        window.location.href = LoginControllerUrl + "/Error?error=access_denied&error_description=user not authenticate";
    } else {
        window.location.href = redirectUrl + "?error=access_denied&error_description=user not authenticate";
    }
});


function showError(msg) {
    document.getElementById("registerBtn").innerText = "Try Again";
    document.getElementById("cancle").style.display = "block";

    const errorEl = document.getElementById("FIDO2error");
    errorEl.classList.remove("hide");
    errorEl.classList.add("show");
    errorEl.innerText = msg;
}


async function verifyAssertionWithServer(assertedCredential, publicKeyOptions) {
    const authData = new Uint8Array(assertedCredential.response.authenticatorData);
    const clientDataJSON = new Uint8Array(assertedCredential.response.clientDataJSON);
    const rawId = new Uint8Array(assertedCredential.rawId);
    const sig = new Uint8Array(assertedCredential.response.signature);

    const FidoDATA = {
        id: assertedCredential.id,
        rawId: coerceToBase64Url(rawId),
        type: assertedCredential.type,
        extensions: assertedCredential.getClientExtensionResults(),
        response: {
            authenticatorData: coerceToBase64Url(authData),
            clientDataJson: coerceToBase64Url(clientDataJSON),
            signature: coerceToBase64Url(sig)
        }
    };

    const data = {
        authenticationScheme: "FIDO2",
        authenticationData: JSON.stringify(FidoDATA)
    };

    let response;

    try {
        const res = await fetch(LoginControllerUrl + "/AuthenticatUser", {
            method: "POST",
            body: JSON.stringify(data),
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            }
        });

        if (!res.ok) {
            response = await res.json();
            throw response;
        } else {
            response = await res.json();
        }

    } catch (e) {
        throw e;
    }

    return response;
}