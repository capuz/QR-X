(function () {
    $(document).on('click', 'form .prevent-default, input:submit .prevent-default, button:submit .prevent-default', function (e) {
        e.preventDefault();
    });

    /// Create 

    $('#btnCreateOrganism').click(function () {
        var url = BASE_PATH + 'Organismo/Create';
        $.get(url)
            .success(function (response) {
                $('#OrganismModal .modal-content').html(response);
                $(":file").filestyle({
                    buttonName: "btn-primary",
                    buttonText: "Examinar",
                    placeholder: "Ruta archivo"
                });
                $('#OrganismModal').modal('show');
            })
        .error(function (response) {
            $('#OrganismModal .modal-content').html(response.responseText);
            $('#OrganismModal').modal('show');
            $(":file").filestyle({
                buttonName: "btn-primary",
                buttonText: "Examinar",
                placeholder: "Ruta archivo"
            })
        })
    });

    $(document).on('click', '#btnSaveNewOrganism', function (e) {
        var $form = $("#CreateOrganismForm");
            var url = $form.attr('action');
            var data = $form.serializeObject();
            $.post(url, data)
         .then(function (response) {
            $('#OrganismList').html(response);
            $('#OrganismModal').modal('hide');
            $(":file").filestyle('destroy');
            swal(
                'Registro insertado correctamente!',
                '',
                'success'
                )
         })
        .fail(function (response, b, c) {
            $('#OrganismModal .modal-content').html(response.responseText);
                $(":file").filestyle({
                buttonName: "btn-primary",
                buttonText: "Examinar",
                placeholder: "Ruta archivo"
            })
        })
    });

    ///Edit

    $(document).on('click', '.edit-action', function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        $.get(url)
            .then(function (response) {
                $('#OrganismModal .modal-content').html(response);
                $(":file").filestyle({
                    buttonName: "btn-primary",
                    buttonText: "Examinar",
                    placeholder: "Ruta archivo"
                });
                $('#OrganismModal').modal('show');
            })
         .fail(function (response) {
             $('#OrganismModal .modal-content').html(response.responseText);
             $('#OrganismModal').modal('show');
             $(":file").filestyle({
                 buttonName: "btn-primary",
                 buttonText: "Examinar",
                 placeholder: "Ruta archivo"
             })
         })
    });



    $(document).on('click', '#btnEditOrganism', function (e) {
        var text = $('#txtSearch').val();
        var $form = $("#EditOrganismForm");
        var url = $form.attr('action')+"?text="+text;
        var data = $form.serializeObject();
        $.post(url, data)
        .then(function (response) {
            $('#OrganismList').html(response);
            $('#OrganismModal').modal('hide');
        })
         .fail(function (response, b, c) {
             $('#OrganismModal .modal-content').html(response.responseText);
             $('#OrganismModal').modal('show');
         })
        .done(function () {
            $(":file").filestyle('destroy');
            swal(
                  'Registro actualizado correctamente!',
                  '',
                  'success'
                )
             .catch(swal.noop)
        })
       
    
    });

    // Delete

    $(document).on('click', '.delete-action', function (e) {
        e.preventDefault();
        var text = $('#txtSearch').val();
        var url = $(this).attr('href') + "?text=" + text;;
        swal({
            title: '¿Esta seguro de desactivar el Organismo?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si',
            cancelButtonText:'No'
        })
        .then(function (response) {
            $.post(url)
                .then(function (response) {
                    $('#OrganismList').html(response);
                })
            .fail(function (response, b, c) {
                $('#OrganismModal .modal-content').html(response.responseText);
                $('#OrganismModal').modal('show');
            })
            .done(function () {
                $(":file").filestyle('destroy');
                swal(
                         'Desactivado!',
                         'Registro desactivado correctamente.',
                         'success')
                .catch(swal.noop)
            })
        })
        .catch(swal.noop);
    });

    $('#btnSearch').click(function () {
        var text = $('#txtSearch').val();
        var url = BASE_PATH + 'Organismo/Search?text=' + text;
        $.get(url)
            .then(function (response) {
                $('#OrganismList').html(response);
            })
            .fail(function (response) {
                $('#OrganismModal .modal-content').html(response.responseText);
                $('#OrganismModal').modal('show');
            })
    });

    $(document).on('keyup', '.countChar', function (e) {
        var $text = $(this);
        var length = $text.val().length;
        var maxLength = $text.attr("maxlength");
        var name = $text.attr("name");
        var divName = "#cc{0}".format(name);
        var text = "{0} caracteres de {1} m&aacute;ximo".format(length, maxLength);
        $(divName).html(text);
    });

    $(document).on('change', '#fuIcono', function (e) {

        var filename = $('#fuIcono').val().split('\\').pop();
        if (filename != "") {
            if (window.FormData !== undefined) {
                var fileUpload = $("#fuIcono").get(0);
                var files = fileUpload.files;
                var fileData = new FormData();
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }
                fileData.append('imagen', 'Icono');
                $.ajax({
                    url: BASE_PATH + 'Organismo/Upload',
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fileData,
                    success: function (result) {
                        $('#Icono').val(result.fname);
                    },
                    error: function (response, b, c) {
                        $('#OrganismModal .modal-content').html(response.responseText);
                        $(":file").filestyle({
                            buttonName: "btn-primary",
                            buttonText: "Examinar",
                            placeholder: "Ruta archivo"
                        })
                    }

                })
            }
        }
    });

    $(document).on('change', '#fuLogo', function (e) {

        var filename = $('#fuLogo').val().split('\\').pop();
        if (filename != "") {
            if (window.FormData !== undefined) {
                var fileUpload = $("#fuLogo").get(0);
                var files = fileUpload.files;
                var fileData = new FormData();
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }
                fileData.append('imagen', 'Logo');
                $.ajax({
                    url: BASE_PATH + 'Organismo/Upload',
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fileData,
                    success: function (result) {
                        $('#Logo').val(result.fname);
                    },
                    error: function (response, b, c) {
                        $('#OrganismModal .modal-content').html(response.responseText);
                        $(":file").filestyle({
                            buttonName: "btn-primary",
                            buttonText: "Examinar",
                            placeholder: "Ruta archivo"
                        })
                    }

                })
            }
        }
    });

})();