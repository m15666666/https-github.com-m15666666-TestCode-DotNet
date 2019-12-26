window.onload = function () {
    var el = document.getElementById('content');
    console.log('window.onload~');
    HelloWorld.Hello();
};
var HelloWorld = /** @class */ (function () {
    function HelloWorld() {
    }
    HelloWorld.Hello = function () {
        console.log('Hello~');
    };
    return HelloWorld;
}());
//# sourceMappingURL=index.js.map