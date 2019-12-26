window.onload = () => {
    var el = document.getElementById('content');
    console.log('window.onload~');
    HelloWorld.Hello();
};

class HelloWorld {
    static Hello(): void {
        console.log('Hello~');
    }
}