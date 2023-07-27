(function () {

    $(document).on('click', 'form .prevent-default, input:submit .prevent-default, button:submit .prevent-default', function (e) {
        e.preventDefault();
    });

    $("table td:empty").html('Sin informacón')

    //// Create 

    $('#btnCreateNotice').click(function () {
        var url = BASE_PATH + 'Noticia/Create';
        $.get(url)
            .success(function (response) {
                $('#NoticeModal .modal-content').html(response);
                intializeTinymce();
                $(":file").filestyle({
                    buttonName: "btn-primary",
                    buttonText: "Examinar",
                    placeholder: "Ruta archivo"
                });
                $('#NoticeModal').modal('show');
            });
    });

    $(document).on('click', '#btnSaveNewNotice', function (e) {
        tinyMCE.triggerSave();
        var $form = $("#CreateNoticeForm");
        var url = $form.attr('action');
        var data = $form.serializeObject();
        $.post(url, data)
        .then(function (response) {
            $('#NoticiaList').html(response);
            $('#NoticeModal').modal('hide');
            $(":file").filestyle('destroy');
            swal(
                    'Registro insertado correctamente!',
                    '',
                    'success'
                )
        })
         .fail(function (response, b, c) {
             tinymce.remove("#Cuerpo");
             $('#NoticeModal .modal-content').html(response.responseText);
             intializeTinymce();
             $(":file").filestyle({
                 buttonName: "btn-primary",
                 buttonText: "Examinar",
                 placeholder: "Ruta archivo"
             })
             $('#NoticeModal').modal('show');
         })
    });

    $(document).on('click', '#btnSavePublishNewNotice', function (e) {

            tinyMCE.triggerSave();
            var dt = new Date();
            var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
            $('#FechaPublicacion').val(time);
            var $form = $("#CreateNoticeForm");
            var url = $form.attr('action');
            var data = $form.serializeObject();
            $.post(url, data)
            .then(function (response) {
                $('#NoticiaList').html(response);
                $('#NoticeModal').modal('hide');
                $(":file").filestyle('destroy');
                swal(
                      'Registro insertado correctamente!',
                      '',
                      'success'
                    )
            })
            .fail(function (response, b, c) {
                tinymce.remove("#Cuerpo");
                $('#NoticeModal .modal-content').html(response.responseText);
                intializeTinymce();
                $(":file").filestyle({
                    buttonName: "btn-primary",
                    buttonText: "Examinar",
                    placeholder: "Ruta archivo"
                })
            })
    });

    //// Edit 

    $(document).on('click', '.edit-action', function (e) {
        e.preventDefault();
        var url = $(this).attr('href');

        $.get(url)
            .then(function (response) {
                $('#NoticeModal .modal-content').html(response);
                intializeTinymce();
                $(":file").filestyle({
                    buttonName: "btn-primary",
                    buttonText: "Examinar",
                    placeholder: "Ruta archivo"
                });
                $('#NoticeModal').modal('show');
            })
            .fail(function (response, b, c) {
                tinymce.remove("#Cuerpo");
                $('#NoticeModal .modal-content').html(response.responseText);
                intializeTinymce();
                $(":file").filestyle({
                    buttonName: "btn-primary",
                    buttonText: "Examinar",
                    placeholder: "Ruta archivo"
                })
                $('#NoticeModal').modal('show');
            })
    });

    $(document).on('click', '#btnUpdateNotice', function (e) {
        var $form = $("#EditNoticeForm");
        var url = $form.attr('action');
        var data = $form.serializeObject();
        $.post(url, data)
                    .then(function (response) {
                        $('#NoticiaList').html(response);
                        $('#NoticeModal').modal('hide');
                        $(":file").filestyle('destroy');
                        swal(
                                'Registro actualizado correctamente!',
                                '',
                                'success'
                            )
                    })
                    .fail(function (response, b, c) {
                        tinymce.remove("#Cuerpo");
                        $('#NoticeModal .modal-content').html(response.responseText);
                        intializeTinymce();
                        $(":file").filestyle({
                            buttonName: "btn-primary",
                            buttonText: "Examinar",
                            placeholder: "Ruta archivo"
                        })
                        $('#NoticeModal').modal('show');
                    })
    });


    $(document).on('click', '.delete-action', function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        swal({
            title: '¿Esta seguro de desactivar la noticia?',
            text: "Esta acción quitará la noticia de las publicaciones",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si',
            cancelButtonText : 'No'
        })
        .then(function (response) {
            $.post(url)
                .then(function (response) {
                    $('#NoticiaList').html(response);
                    $(":file").filestyle('destroy');
                    swal(
                         'Desactivado!',
                         'Registro desactivado correctamente.',
                         'success')
                })
            .fail(function (response, b, c) {
                $('#NoticeModal .modal-content').html(response.responseText);
                $('#NoticeModal').modal('show');
            })
        })
         .catch(swal.noop);
    });

    ///Publish

    $(document).on('click', '.publish-action', function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        swal({
            title: '¿Esta seguro de publicar la noticia?',
            text: "Esta acción publicará la noticia seleccionada",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si',
            cancelButtonText: 'No'
        })
        .then(function (response) {
            $.post(url)
                .then(function (response) {
                    $('#NoticiaList').html(response);
                    $(":file").filestyle('destroy');
                    swal(
                         'Publicado!',
                         'Registro publicado correctamente.',
                         'success')
                })
            .fail(function (response, b, c) {
                $('#NoticeModal .modal-content').html(response.responseText);
                $('#NoticeModal').modal('show');
            })
        })
         .catch(swal.noop);
    });

    $(document).on('click', '#btnEditPublishNotice', function (e) {
        e.preventDefault();
        var id = $('#IdNoticia').val();
        var url = BASE_PATH + 'Noticia/Publish?id={0}'.format(id);
        swal({
            title: '¿Esta seguro de publicar la noticia?',
            text: "Esta acción publicará la noticia seleccionada",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si',
            cancelButtonText: 'No'
        })
        .then(function (response) {
            $.post(url)
               .then(function (response) {
                   $('#NoticiaList').html(response);
                   $(":file").filestyle('destroy');
                   swal(
                        'Publicado!',
                        'Registro publicado correctamente.',
                        'success')
               })
               .fail(function (response, b, c) {
                   $('#NoticeModal .modal-content').html(response.responseText);
                   $('#NoticeModal').modal('show');
               })
        })
         .catch(swal.noop);
       

    });
       

    //Preview

    $(document).on('click', '#btnPreviewNotice', function (e) {
        tinyMCE.triggerSave();
        e.preventDefault();
        var $form = $("#CreateNoticeForm");
        var url = BASE_PATH + 'Noticia/Preview';
        var data = $form.serializeObject();
        $('#NoticeModal').data('bs.modal', null);
        $('#NoticeModal').modal('hide');
        $.post(url, data)
                .then(function (response) {
                    $('#NoticeModal .modal-content').html(response);
                    $('.modal-backdrop').removeClass("modal-backdrop");
                    $('#NoticeModal').modal('show');

                })
    });

    $(document).on('click', '#btnEditPreviewNotice', function (e) {
        tinyMCE.triggerSave();
        e.preventDefault();
        var $form = $("#EditNoticeForm");
        var url = BASE_PATH + 'Noticia/Preview';
        var data = $form.serializeObject();
        $('#NoticeModal').data('bs.modal', null);
        $('#NoticeModal').modal('hide');
        $.post(url, data)
                .then(function (response) {
                    $('#NoticeModal .modal-content').html(response);
                    $('.modal-backdrop').removeClass("modal-backdrop");
                    $('#NoticeModal').modal('show');

                })
    });


    $(document).on('click', '#btnBackPreview', function (e) {
        e.preventDefault();  
        var $form = $("#BackPreviewNoticeForm");
        var url = $form.attr('action');
        var data = $form.serializeObject();
        $('#NoticeModal').data('bs.modal', null);
        //$('#NoticeModal').modal('toggle');
        $('#NoticeModal').modal('hide');
        $.post(url, data)
               .then(function (response) {
                   $('#NoticeModal .modal-content').html(response);
                   intializeTinymce();
                   $(":file").filestyle({
                       buttonName: "btn-primary",
                       buttonText: "Examinar",
                       placeholder: "Ruta archivo"
                   });
                   $('.modal-backdrop').removeClass("modal-backdrop");
                   $('#NoticeModal').modal('show');
               })
                .fail(function (response, b, c) {
                    $('#NoticeModal .modal-content').html(response.responseText);
                })
    });
        

    function intializeTinymce() {
        tinymce.init({
            selector: '#Cuerpo',
            language: 'es',
            menubar: false,
            plugins: "textcolor colorpicker paste",
            paste_preprocess: function (plugin, args) {
                var maxLength = 8000;
                var length = args.content.length;
                if (length > maxLength)
                {
                    var truncatetext = args.content.substring(0, maxLength);
                    var truncateLength = truncatetext.length;
                    args.content = truncatetext;
                }
            },
            toolbar: [
                'forecolor backcolor formatselect fontselect fontsizeselect',
                'bold italic underline strikethrough alignleft aligncenter alignright alignjustify bullist numlist outdent indent'
            ],
            init_instance_callback: function (editor) {
                editor.on('click', function (e) {

                });
            },
            setup: function (ed) {
                var maxLength = parseInt($('#' + (ed.id)).attr("maxlength"));
                ed.on('keydown', function (e) {
                    var $text = tinyMCE.activeEditor.getContent();
                    var length = $text.length;
                    if (length >= maxLength && e.keyCode != 8) {
                        e.preventDefault();
                        e.stopPropagation();
                        return false;
                    }
                })
                ed.on('keyup', function (e) {
                    
                    var $text = tinyMCE.activeEditor.getContent();
                    var length = $text.length;
                    var text = "{0} caracteres de {1} m&aacute;ximo".format(length, maxLength);
                    $('#ccCuerpo').html(text);
                   
                })
            },
            });
    };


    $(document).on('keyup', '.countChar', function (e) {
        var $text = $(this);
        var length = $text.val().length;
        var maxLength = $text.attr("maxlength");
        var name = $text.attr("name");
        var divName = "#cc{0}".format(name);
        var text = "{0} caracteres de {1} m&aacute;ximo".format(length, maxLength);
        $(divName).html(text);
    });

    $(document).on('keypress', '.prevent-invalid-char', function (e) {

        return permite (e, 'texto')
    });

    $('#NoticeModal').on('hide.bs.modal', function () {
        tinyMCE.editors = [];
    });

    $(document).on('change', '#fuImagen', function (e) {

        var filename = $('input[type=file]').val().split('\\').pop();
        if (filename != "") {
            if (window.FormData !== undefined) {
                var fileUpload = $("#fuImagen").get(0);
                var files = fileUpload.files;
                var fileData = new FormData();
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }
                fileData.append('imagen', 'noticia');
                $.ajax({
                    url: BASE_PATH + 'Noticia/Upload',
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fileData,
                    success: function (result) {
                        $('#Imagen').val(result.fname);
                    },
                    error: function (response, b, c) {
                        tinymce.remove("#Cuerpo");
                        $('#NoticeModal .modal-content').html(response.responseText);
                        intializeTinymce();
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