$(document).ready(function () {
    const aboutDialog = $("#aboutDialog");
    aboutDialog.removeClass("hidden");
});

$("#closeAboutBtn").click(() => {
    $("#aboutDialog").addClass("hidden");
    $("#aboutOverlay").removeClass("fixed");
    $("#aboutBtn").removeClass("bg-zinc-600");
});

$("#aboutBtn").click(() => {
    $("#aboutDialog").removeClass("hidden");
    $("#aboutOverlay").toggleClass("fixed");
    $("#aboutBtn").toggleClass("bg-zinc-600 hover:bg-zinc-400");
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
