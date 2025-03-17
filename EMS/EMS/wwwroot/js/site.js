$(".Falcustom-file-input").on("change", function () {

    var falfileName = $(this).val().split("\\").pop();

    document.getElementById('HeadLPreviewPhoto').src = window.URL.createObjectURL(this.files[0]);

    document.getElementById('HeadLPhotoUrl').value = falfileName;

});

$(".Farcustom-file-input").on("change", function () {

    var farfileName = $(this).val().split("\\").pop();

    document.getElementById('HeadRPreviewPhoto').src = window.URL.createObjectURL(this.files[0]);

    document.getElementById('HeadRPhotoUrl').value = farfileName;

});


$(".Logincustom-file-input").on("change", function () {

    var loginfileName = $(this).val().split("\\").pop();

    document.getElementById('LoginPreviewPhoto').src = window.URL.createObjectURL(this.files[0]);

    document.getElementById('LoginPhotoUrl').value = loginfileName;

});


$(".custom-file-input").on("change", function () {

    var loginfileName = $(this).val().split("\\").pop();

    document.getElementById('PreviewPhoto').src = window.URL.createObjectURL(this.files[0]);

    document.getElementById('PhotoUrl').value = loginfileName;

});

$(".sigcustom-file-input").on("change", function () {

    var fileNamesig = $(this).val().split("\\").pop();

    document.getElementById('PreviewPhotosig').src = window.URL.createObjectURL(this.files[0]);

    document.getElementById('PhotoUrlsig').value = fileNamesig;

});

$(".Paymentcustom-file-input").on("change", function () {

    var payfile = $(this).val().split("\\").pop();

    document.getElementById('PaymentPreview').src = window.URL.createObjectURL(this.files[0]);

    document.getElementById('TranjPhotoUrl').value = payfile;

});

