function deleteReview(reviewNumber) {
    var antiForgeryToken = $('#antiForgeryForm input[name=__RequestVerificationToken]').val();
    var data = { reviewNumber: reviewNumber.toString() }
    $.ajax({
        type: "DELETE",
        url: "/api/Reviews",
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

function editReview(reviewNumber) {
    var reviewInfo = document.getElementById(`#editReviewInfo${reviewNumber}`);
    if (reviewInfo.disabled == true) {
        reviewInfo.disabled = false;
    }
    else {
        reviewInfo.disabled = true;
    }
    var buttonToEdin = document.getElementById(`#sendEditButton${reviewNumber}`);
    if (buttonToEdin.style.display == "none") {
        buttonToEdin.style.display = "block";
    }
    else {
        buttonToEdin.style.display = "none";
    }
};

function sendEditedReview(reviewNumber) {
    var reviewInfo = document.getElementById(`#editReviewInfo${reviewNumber}`).value;
    var antiForgeryToken = $('#antiForgeryForm input[name=__RequestVerificationToken]').val();
    var data = { reviewInfo: reviewInfo.toString(), reviewNumber: reviewNumber.toString() }
    $.ajax({
        type: "PUT",
        url: "/api/Reviews",
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