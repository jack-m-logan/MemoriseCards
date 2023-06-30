const aboutBtn = $("#aboutBtn");
const titleBanner = $("#titleBanner");

$(document).ready(function () {
    const aboutDialog = $("#aboutDialog");
    aboutDialog.removeClass("hidden");
});

$("#closeAboutBtn").click(() => {
    $("#aboutDialog").addClass("hidden");
    $("#overlay").removeClass("fixed");
    aboutBtn.removeClass("bg-zinc-500");
    titleBanner.removeClass("hidden");
});

$("#aboutBtn").click(() => {
    $("#aboutDialog").removeClass("hidden");
    $("#overlay").toggleClass("fixed");
    aboutBtn.toggleClass("bg-zinc-500");
    titleBanner.addClass("hidden");
});

$("#matrixBtn").click(() => {
    $("#matrixBtn").toggleClass("bg-zinc-500");
});

$("#practiceBtn").click(() => {
    $("#practiceBtn").toggleClass("bg-zinc-500");
});
