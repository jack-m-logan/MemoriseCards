$(document).ready(function () {
    const aboutDialog = $("#aboutDialog");
    aboutDialog.removeClass("hidden");
});

$("#closeAboutBtn").click(() => {
    $("#aboutDialog").addClass("hidden");
    $("#overlay").removeClass("fixed");
    $("#aboutBtn").removeClass("bg-zinc-600");
    $("#titleBanner").removeClass("hidden");
});

$("#aboutBtn").click(() => {
    $("#aboutDialog").removeClass("hidden");
    $("#overlay").toggleClass("fixed");
    $("#aboutBtn").toggleClass("bg-zinc-600 hover:bg-zinc-400");
    $("#titleBanner").addClass("hidden");
});

$("#practiceBtn").click(() => {
    $("#practiceBtn").toggleClass("bg-zinc-600 hover:bg-zinc-400");
});

$("#matrixBtn").click(() => {
    $("#matrixDropdown").toggleClass("hidden");
    $("#matrixBtn").toggleClass("bg-zinc-600 hover:bg-zinc-400");
});

$(document).click((e) => {
    if (!$(e.target).closest("#matrixBtn").length) {
        $("#matrixDropdown").addClass("hidden");
        $("#matrixBtn").removeClass("bg-zinc-600").addClass("hover:bg-zinc-400");
    }
});

$("#newDeckBtn, #editDeckBtn").click(() => {
    $("#newDeckBtn, #editDeckBtn").toggleClass("bg-zinc-600 hover:bg-zinc-400");
});
