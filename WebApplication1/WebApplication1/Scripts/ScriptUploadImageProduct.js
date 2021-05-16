function ShowImageProduct(imageUpload, previewImage) {
    if (imageUpload.files && imageUpload.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUpload.files[0]);
    }
}
function ShowImageProduct2(imageUpload2, previewImage2) {
    if (imageUpload2.files && imageUpload2.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage2).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUpload2.files[0]);
    }
}
function ShowImageProduct3(imageUpload3, previewImage3) {
    if (imageUpload3.files && imageUpload3.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage3).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUpload3.files[0]);
    }
}
function ShowImageProduct4(imageUpload4, previewImage4) {
    if (imageUpload4.files && imageUpload4.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage4).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUpload4.files[0]);
    }
}
function ShowImageProduct5(imageUpload5, previewImage5) {
    if (imageUpload5.files && imageUpload5.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage5).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUpload5.files[0]);
    }
}