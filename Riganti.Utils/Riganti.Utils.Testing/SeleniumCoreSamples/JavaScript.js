


function ExecuteScript() {
    var a = (arguments || [{}, null, null]);
    var element = a[0];
    var propertyName = a[1];
    var propertyValue = a[2];
    if (!element.hasOwnProperty(propertyName)) {
        throw "Element has not property '" + propertyName+ "'.";
    }
    element[propertyName] = propertyValue;
}