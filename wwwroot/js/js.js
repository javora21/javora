$('.carousel').carousel();

$("#images-load-form").submit(function (event) {


    event.preventDefault();
    var formData = new FormData();
    var filedata = document.getElementsByName("uploads");
    var i = 0, len = filedata[0].files.length, file;
    for (i; i < len; i++) {
        file = filedata[0].files[i];
        formData.append("formData", file);
    }
    $.ajax({
        url: 'loadphoto',
        type: 'POST',
        data: formData,
        async: false,
        cache: false,
        contentType: false,
        processData: false,
        headers: { "Accept": "application/json"},
        success: function (returndata) {
            for (var i = 0; i < returndata.length; i++) {
                $('#image-list').append(
                    '<div><img class="image-list-item" src="' + returndata[i].path + '" />'
                );
            }
            
        }
    });

    return false;
});

$("#main-image").on('input', function (event) {

    var reader = new FileReader();
    reader.readAsDataURL(event.currentTarget.files[0]);
    reader.onload = function () {
        $('#main-image-view').html(
            '<img class="image-list-item" src="' + reader.result + '" />'
        );
    }  

});
$("#file-loader").on('input', function (event) {

    var reader = new FileReader();
    reader.readAsDataURL(event.currentTarget.files[0]);
    reader.onload = function () {
        $('#file-view').html(
            '<p>' + event.currentTarget.files[0].name + '</p>'
        );
    }

});




