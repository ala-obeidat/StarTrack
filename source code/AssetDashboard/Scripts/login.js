function checkSubmit() {
    var encryptedPassword = encodeWithBase64(document.getElementById('password').value, document.getElementById('Key').value);
    document.getElementById('password').value = encryptedPassword;
};
function redirectWithBaseUrl(url) {
    if ((url.indexOf('en/') > -1 || url.indexOf('ar/') > -1))
        location.href = _baseUrl + url;
    else
        location.href = _baseUrl + _lan + '/' + url;
}
function encodeWithBase64(code, key) {
    /// <summary>Encrypt string with Base64 and token as Sold</summary>
    /// <param name="code" type="string"> Code to be encode</param> 
    /// <param name="key" type="string"> Sold to be added with Code to get encoding string</param> 
    /// <returns> Encrypted base64 string encoding </returns>

    if (!code || code == '')
        return code;
    var result = [];
    var tempCode = code;
    while (tempCode.length > 0) {
        if (tempCode.length > 2) {
            result.push(tempCode.substring(0, 3));
            tempCode = tempCode.substring(3, tempCode.length);
        }
        else {
            result.push(tempCode.substring(0, tempCode.length));
            tempCode = '';
        }
    }
    var encryptionText = window.btoa(result.join(key));
    return reverseString(encryptionText);
}

function reverseString(str) {
    //Use the split() method to return a new array
    var splitString = str.split("");

    //Use the reverse() method to reverse the new created array
    var reverseArray = splitString.reverse();

    //Use the join() method to join all elements of the array into a string
    var joinArray = reverseArray.join("");

    //Return the reversed string
    return joinArray;
}