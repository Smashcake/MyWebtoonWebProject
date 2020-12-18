function deleteComment(commentNumber) {
    var antiForgeryToken = $('#antiForgeryForm input[name=__RequestVerificationToken]').val();
    var data = { commentNumber: commentNumber.toString() }
    $.ajax({
        type: "DELETE",
        url: "/api/Comments",
        data: JSON.stringify(data),
        headers: {
            "X-CSRF-TOKEN": antiForgeryToken
        },
        success: function (data) {
            location.reload();
        },
        contentType: 'application/json',
    });
};

function editComment(commentNumber) {
    var commentInfo = document.getElementById(`#editCommentInfo${commentNumber}`);
    if (commentInfo.disabled == true) {
        commentInfo.disabled = false;
    }
    else {
        commentInfo.disabled = true;
    }
    var buttonToEdin = document.getElementById(`#sendEditButton${commentNumber}`);
    if (buttonToEdin.style.display == "none") {
        buttonToEdin.style.display = "block";
    }
    else {
        buttonToEdin.style.display = "none";
    }
};

function sendEditedComment(commentNumber) {
    var commentInfo = document.getElementById(`#editCommentInfo${commentNumber}`).value;
    var antiForgeryToken = $('#antiForgeryForm input[name=__RequestVerificationToken]').val();
    var data = { commentInfo: commentInfo.toString(), commentNumber: commentNumber.toString() }
    $.ajax({
        type: "PUT",
        url: "/api/Comments",
        data: JSON.stringify(data),
        headers: {
            "X-CSRF-TOKEN": antiForgeryToken
        },
        success: function (data) {
            location.reload();
        },
        contentType: 'application/json',
    });
};
