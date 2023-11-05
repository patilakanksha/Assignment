document.addEventListener('DOMContentLoaded', function () {
    var errorMessage = '@ViewBag.ErrorMessage';
    if (errorMessage && errorMessage.length > 0) {
        var toast = new bootstrap.Toast(document.getElementById('errorToast'));
        toast.show();

        // Automatically hide the toast after 5 seconds
        setTimeout(function () {
            toast.hide();
        }, 5000); // 5000 milliseconds (5 seconds)
    }
});