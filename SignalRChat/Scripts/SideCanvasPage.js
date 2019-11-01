
function offCanvasSide() {
    $("#offCanvasSide").css("visibility", "visible")
    $("#offCanvasSide").removeClass("mui-transitioning")
    $("#offCanvasSide").css({
        "transform": "translate3d(0px, 0px, 0px)",
        "transition-duration": "500ms"
    })
    setTimeout(function () {
        $("#offCanvasWrapper").addClass("mui-active")
        $("#offCanvasSide").addClass("mui-transitioning mui-active")
    }, 500)
}
function noCanvasSide() {
    var CanvasSideWidth = Math.ceil($("#offCanvasSide").width()) * -1;
    $("#offCanvasSide").css("visibility", "visible")
    $("#offCanvasSide").removeClass("mui-transitioning")
    $("#offCanvasSide").css({
        "transform": "translate3d(" + CanvasSideWidth + "px, 0px, 0px)",
        "transition-duration": "500ms"
    })
    setTimeout(function () {
        $("#offCanvasSide").addClass("mui-transitioning")
        $("#offCanvasWrapper,#offCanvasSide").removeClass("mui-active")
    }, 500)
}
function CanvasSide(x) {
    var CanvasSideNum = parseFloat($('#offCanvasSide').css("transform").replace(/[^0-9\-,.]/g, '').split(',')[4]) + x;
    var CanvasSideWidth = Math.ceil($("#offCanvasSide").width()) * -1;
    $("#offCanvasSide").css("visibility", "visible")
    $("#offCanvasSide").removeClass("mui-transitioning")
    if (CanvasSideNum < CanvasSideWidth) {
        $("#offCanvasSide").addClass("mui-transitioning")
        $("#offCanvasWrapper,#offCanvasSide").removeClass("mui-active")
        return;
    } else if (CanvasSideNum > 0) {
        $("#offCanvasWrapper").addClass("mui-active")
        $("#offCanvasSide").addClass("mui-transitioning mui-active")
        return;
    }
    $("#offCanvasSide").css({
        "transform": "translate3d(" + CanvasSideNum + "px, 0px, 0px)",
        "transition-duration": "0ms"
    })
}

$(function () {
    $("#offCanvasSide").css({ "transform": "translate3d(" + Math.ceil($("#offCanvasSide").width()) * -1 + "px, 0px, 0px)" })
    if (false) {
        var startX = 0, moveEndX = 0, moveEndX1 = 0;
        $("#offCanvasWrapper").on("mousedown", function (e) {
            startX = 0, moveEndX = 0, moveEndX1 = 0;
            //记录按下时的位置
            startX = e.pageX;

            $("#offCanvasWrapper").bind("mousemove", function (e) {
                if (!e.defaultPrevented) {
                    e.preventDefault();
                }
                moveEndX = e.pageX;
                var x = moveEndX1 == 0 ? 0 : moveEndX - moveEndX1;
                moveEndX1 = moveEndX;
                CanvasSide(x)
            })
        })
        $("#offCanvasWrapper").on("mouseup", function () {
            $("#offCanvasWrapper").unbind("mousemove");
            var CanvasSideNum = parseFloat($('#offCanvasSide').css("transform").replace(/[^0-9\-,.]/g, '').split(',')[4])
            var CanvasSideWidth = Math.ceil($("#offCanvasSide").width()) * -1;
            if (CanvasSideNum >= CanvasSideWidth * 0.75) {
                offCanvasSide();
            } else {
                noCanvasSide();
            }
        })
        $("#offCanvasWrapper").on("touchstart", function (e) {
            startX = 0, moveEndX = 0, moveEndX1 = 0;
            //记录按下时的位置
            startX = e.originalEvent.changedTouches[0].pageX;
        })
        $("#offCanvasWrapper").on("touchmove", function (e) {
            if (!e.defaultPrevented) {
                e.preventDefault();
            }
            moveEndX = e.originalEvent.changedTouches[0].pageX;
            var x = moveEndX1 == 0 ? 0 : moveEndX - moveEndX1;
            moveEndX1 = moveEndX;
            CanvasSide(x)
        })
        $("#offCanvasWrapper").on("touchend", function (e) {
            var CanvasSideNum = parseFloat($('#offCanvasSide').css("transform").replace(/[^0-9\-,.]/g, '').split(',')[4])
            var CanvasSideWidth = Math.ceil($("#offCanvasSide").width()) * -1;

            if (CanvasSideNum >= CanvasSideWidth * 0.75) {
                offCanvasSide();
            } else {
                noCanvasSide();
            }
        })
    }
})

