$(document).ready(function () {
    $(document).on("click", "#query-btn", function () {
        const version = $('#Version').val();

        $.ajax({
            url: "/Filter",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            dataType: "HTML",
            data: JSON.stringify({
                "Version": version
            }),
            success: function (html) {
                $('#software-container').html(html);
            },
            error: function (jqXHR, exception) {
                let msg;

                if (jqXHR.status === 0) {
                    msg = "Not connect.\n Verify Network.";
                } else if (jqXHR.status === 404) {
                    msg = "Requested page not found. [404]";
                } else if (jqXHR.status === 500) {
                    msg = "Internal Server Error [500].";
                } else if (exception === "parsererror") {
                    msg = "Requested JSON parse failed.";
                } else if (exception === "timeout") {
                    msg = "Time out error.";
                } else if (exception === "abort") {
                    msg = "Ajax request aborted.";
                } else {
                    msg = `Uncaught Error.\n${jqXHR.responseText}`;
                }

                Console.group();
                Console.log(msg);
                Console.groupEnd();
            }
        });
    });

    $(document).on("keyup", "#Version", function () {
        const version = $(this).val();

        if (version.length > 0) {
            enable();
        } else {
            disable();
        }
    });
});

const enable = function () {
    const btn = $("#query-btn");
    btn.removeClass("disabled");
    btn.prop("disabled", false);
}

const disable = function () {
    const btn = $("#query-btn");
    btn.addClass("disabled");
    btn.prop("disabled", true);
}