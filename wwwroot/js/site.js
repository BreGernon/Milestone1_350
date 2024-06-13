$(function () {
    console.log("Page is ready");

    //$(".game-button").click(function (event) {
    $(document).on("click", ".game-button", function (event) {
        event.preventDefault();

        var buttonNumber = $(this).val();
        console.log("Game button number " + buttonNumber + " was clicked");
        doButtonUpdate(buttonNumber);
    });
});

function doButtonUpdate(buttonNumber) {
    $.ajax({
        dataType: "html",
        method: 'POST',
        url: '/Minesweeper/ShowOneButton',
        data: {
            "buttonNumber": buttonNumber
        },
        success: function (data) {
            console.log("Success:", data);
            $('#button-' + buttonNumber).html(data);
        }
    });
};