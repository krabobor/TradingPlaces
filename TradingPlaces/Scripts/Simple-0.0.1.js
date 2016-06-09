$(function () {
    $("img").mouseover(function () {
        $(this).animate({
            height: '+=30', width: '+=30'
        }, { "duration": "fast" });
    });
    $("img").mouseout(function () {
        $(this).animate({
            height: '-=30', width: '-=30'
        }, { "duration": "fast" });
    });
});
