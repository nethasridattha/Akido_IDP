const coerceToArrayBuffer = function (thing, name) {
    if (typeof thing === "string") {
        // base64url to base64
        thing = thing.replaceAll("-", "+").replaceAll("_", "/");

        // base64 to Uint8Array
        const str = window.atob(thing);
        const bytes = new Uint8Array(str.length);

        for (let i = 0; i < str.length; i++) {
            // charCodeAt is required here because atob() returns a binary string
            bytes[i] = str.charCodeAt(i);
        }

        thing = bytes;
    }

    // Array to Uint8Array
    if (Array.isArray(thing)) {
        thing = new Uint8Array(thing);
    }

    // Uint8Array to ArrayBuffer
    if (thing instanceof Uint8Array) {
        thing = thing.buffer;
    }

    // error if none of the above worked
    if (!(thing instanceof ArrayBuffer)) {
        throw new TypeError(`could not coerce '${name}' to ArrayBuffer`);
    }

    return thing;
};


const coerceToBase64Url = function (thing) {
    // Array or ArrayBuffer to Uint8Array
    if (Array.isArray(thing)) {
        thing = Uint8Array.from(thing);
    }

    if (thing instanceof ArrayBuffer) {
        thing = new Uint8Array(thing);
    }

    // Uint8Array to base64
    if (thing instanceof Uint8Array) {
        let str = "";
        const len = thing.byteLength;

        for (let i = 0; i < len; i++) {
            str += String.fromCharCode(thing[i]);
        }

        thing = window.btoa(str);
    }

    if (typeof thing !== "string") {
        throw new Error("could not coerce to string");
    }

    // base64 to base64url
    thing = thing.replaceAll("+", "-")
        .replaceAll("/", "_")
        .replace(/=*$/g, ""); // regex is fine here (no replaceAll equivalent)

    return thing;
};


function detectFIDOSupport() {
    if (typeof window.PublicKeyCredential !== "function") {
        document.getElementById("register-form").disabled = true;
        alert("");
    }
}


/**
 * Get a form value
 * @param {any} selector
 */
function value(selector) {
    const el = document.querySelector(selector);

    if (el.type === "checkbox") {
        return el.checked;
    }

    return el.value;
}