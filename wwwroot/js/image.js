$(document).ready(function () {
    // When a new file is selected
    $('#imgInput').change(function (event) {
        var file = event.target.files[0];
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#selectedImage').attr('src', e.target.result).show();
        }
        reader.readAsDataURL(file);
    });
});