function encodeAndDecodeMessages() {
    const [encodeBtnEl, decodeBtnEl] = document.querySelectorAll("div button");
    const [encodeTextareaEl, decodeTextareaEl] = document.querySelectorAll("div textarea");

    function operateMessage(text, asciiDiff) {
        return text
            .split('')
            .map((char) => {
                const currentValue = char.charCodeAt(0);
                return String.fromCharCode(currentValue + asciiDiff);
            })
            .join('');
    }

    function encodeMessageHandler() {
        decodeTextareaEl.value = operateMessage(encodeTextareaEl.value, 1);
        encodeTextareaEl.value = '';
    }

    function decodeMessageHandler() {
        decodeTextareaEl.value = operateMessage(decodeTextareaEl.value, -1);
    }

    encodeBtnEl.addEventListener('click', encodeMessageHandler);
    decodeBtnEl.addEventListener('click', decodeMessageHandler);
}