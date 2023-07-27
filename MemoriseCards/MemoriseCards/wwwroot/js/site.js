$(document).ready(function () {
    const aboutDialog = $("#aboutDialog");
    aboutDialog.removeClass("hidden");
});

const closeDropDown = (button, dropdown) => {
    $(document).click((e) => {
        if (!$(e.target).closest(`#${button}`).length) {
            $(`#${dropdown}`).addClass("hidden");
            $(`#${button}`).removeClass("bg-zinc-600").addClass("hover:bg-zinc-400");
        }
    });
};

const openDialog = (button, dialog, overlay) => {
    $(`#${button}`).click(() => {
        $(`#${overlay}, #${dialog}`).removeClass("hidden");
        $(`#${overlay}`).toggleClass("fixed");
        $(`#${button}`).toggleClass("bg-zinc-600 hover:bg-zinc-400");
    });
};

const closeDialog = (eventButton, closeButton, dialog, overlay) => {
    $(`#${eventButton}`).click(() => {
        $(`#${dialog}`).addClass("hidden");
        $(`#${overlay}`).removeClass("fixed");
        $(`#${closeButton}`).removeClass("bg-zinc-600");
    });
};

closeDropDown("matrixBtn", "matrixDropdown");

$("#practiceBtn").click(() => {
    $("#practiceBtn").toggleClass("bg-zinc-600 hover:bg-zinc-400");
});

$("#matrixBtn").click(() => {
    $("#matrixDropdown").toggleClass("hidden");
    $("#matrixBtn").toggleClass("bg-zinc-600 hover:bg-zinc-400");
});

openDialog("aboutBtn", "aboutDialog", "aboutOverlay");
openDialog("newDeckBtn", "newDeckDialog", "newDeckOverlay");
openDialog("editDeckBtn", "editDeckDialog", "editDeckOverlay");

closeDialog("closeNewDeckBtn", "newDeckBtn", "newDeckDialog", "newDeckOverlay");
closeDialog("closeEditDeckBtn", "editDeckBtn", "editDeckDialog", "editDeckOverlay");
closeDialog("closeAboutBtn", "aboutBtn", "aboutDialog", "aboutOverlay");

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
};

function populateEditDeckDropdown() {
    $.ajax({
        url: "/Deck/GetDecksForDropdown",
        type: "GET",
        success: function (response) {
            const editDeckDropDown = $("#editDeckDropDown");
            editDeckDropDown.empty(); 

            for (const deck of response) {
                editDeckDropDown.append(`<option value="${deck.id}">${deck.name}</option>`);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching decks:", error);
            console.log(xhr.responseText);
        }
    });
};

$("#editDeckBtn").click(() => {
    populateEditDeckDropdown();
});

// TODO modularise code, export functions, import into scripts for each page
