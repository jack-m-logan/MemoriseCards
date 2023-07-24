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

$("#newDeckBtn").click(() => {
    $("#newDeckOverlay, #newDeckDialog").removeClass("hidden");
    $("#newDeckOverlay").toggleClass("fixed");
    $("#newDeckBtn").toggleClass("bg-zinc-600 hover:bg-zinc-400");
});

$("#closeNewDeckBtn").click(() => {
    $("#newDeckDialog").addClass("hidden");
    $("#newDeckOverlay").removeClass("fixed");
    $("#newDeckBtn").removeClass("bg-zinc-600");
});

$("#createNewDeckBtn").click(() => {
    const deckName = $("#nameNewDeck").val();
    const model = { name: deckName };
    createNewDeck(model);
});

function createNewDeck(model) {
    console.log(model);

    $.ajax({
        url: "/Deck/CreateNewDeck",
        type: "POST",
        data: model,
        success: function (response) {
            console.log("Deck created successfully!");
        },
        error: function (xhr, status, error) {
            console.error("Error creating deck:", error);
            console.log(xhr.responseText);    
        }
    });
}

