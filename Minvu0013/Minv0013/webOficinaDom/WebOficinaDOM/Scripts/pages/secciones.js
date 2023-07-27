(function () {

    $(document).on('click', '.prevent-default', function (e) {
        e.preventDefault();
    });

    function setMaxLength() {

        $("#SeccionModal input[data-val-length-max],textarea[data-val-length-max]").each(function () {
            var $this = $(this);
            var data = $this.data();
            $this.attr("maxlength", data.valLengthMax);
        });
    };
    $('#SeccionModal').on('shown.bs.modal', function () {
        setMaxLength();
    });
    //Edit Logo
    $(document).on('click', '#btnEditLogoDOM', function (e) {
        var form = $("#LogoDomForm");

        var formdata = false;
        if (window.FormData) {
            formdata = new FormData(form[0]);
        }

        var url = form.attr('action');

        $.ajax({
            url: url,
            data: formdata ? formdata : form.serialize(),
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (response) {
                $("#ImageLogoDom").attr("src", response.UriLogo.replace('~/', '') + '?' + new Date().getTime());
                swal('',
                     'Logo DOM actualizado exitosamente',
                     'success')
            },
            error: function (response) {
                $('#LogoDomForm').html(response.responseText);
                $("#fileLogoDom").filestyle({
                    buttonName: "btn-primary",
                    buttonText: "Examinar",
                    placeholder: "Ruta archivo"
                });
            }
        });

    });

    // Create Principal
    $('#btnCreateContenidoPrincipal').click(function (e) {

        var url = BASE_PATH + 'Seccion/CreateContenidoPrincipal';
        var rowCount = $('#ContenidoPrincipalList .table >tbody >tr').length;
        var maximo = 4;
        $.getJSON("Seccion/GetMaximoContenidoPrincipal", function (data) {
            maximo = data;
        })

        if (rowCount >= maximo) {
            swal('',
             'No es posible crear más de {0} contenidos principales'.format(maximo),
             'error'
             )
        }
        else {
            $.get(url)
                .success(function (response) {
                    $('#SeccionModal .modal-content').html(response);
                    $("#fileCreate").filestyle({
                        buttonName: "btn-primary",
                        buttonText: "Examinar",
                        placeholder: "Ruta archivo"
                    });
                    $('#SeccionModal').modal('show');
                })
           .error(function (response) {
               $('#MenuModal .modal-content').html(response.responseText);
               $('#MenuModal').modal('show');
           });
        }
    });

    $(document).on('click', '#btnCreateNewContenidoPrincipal', function (e) {
        e.preventDefault();
        var form = $("#CreateContenidoPrincipalForm");
        var url = form.attr('action');

        var formdata = false;
        if (window.FormData) {
            formdata = new FormData(form[0]);
        }

        $.ajax({
            url: url,
            data: formdata ? formdata : form.serializeObject(),
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (response) {
                $('#ContenidoPrincipalList').html(response);

                $('#SeccionModal').modal('hide');

                $("#fileCreate").filestyle('destroy');

                swal('',
                    'Contenido principal creado exitosamente',
                    'success'
                    )
            },
            error: function (response, b, c) {
                $('#SeccionModal .modal-content').html(response.responseText);
                $("#fileCreate").filestyle({
                    buttonName: "btn-primary",
                    buttonText: "Examinar",
                    placeholder: "Ruta archivo"
                });
                setMaxLength();
            }
        });

    });
    // Edit Principal
    $(document).on('click', '.edit-action-1', function (e) {
        var url = $(this).attr('href');

        $.get(url)
            .then(function (response) {
                $('#SeccionModal .modal-content').html(response);
                $("#fileEdit").filestyle({
                    buttonName: "btn-primary",
                    buttonText: "Examinar",
                    placeholder: "Ruta archivo"
                });
                $('#SeccionModal').modal('show');
            })

            .fail(function (response) {
                alert(response.statusText);
                alert(response.responseText);
            });
    });
    $(document).on('click', '#btnEditContenidoPrincipal', function (e) {
        var form = $("#EditSeccionPrincipalForm");
        var url = form.attr('action');

        var formdata = false;
        if (window.FormData) {
            formdata = new FormData(form[0]);
        }

        $.ajax({
            url: url,
            data: formdata ? formdata : form.serializeObject(),
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (response) {
                $('#ContenidoPrincipalList').html(response);
                $('#SeccionModal').modal('hide');

                $("#fileEdit").filestyle('destroy');

                swal('',
                    'Contenido principal editado exitosamente',
                    'success'
                    )
            },
            error: function (response, b, c) {
                $('#SeccionModal .modal-content').html(response.responseText);
                $("#fileEdit").filestyle({
                    buttonName: "btn-primary",
                    buttonText: "Examinar",
                    placeholder: "Ruta archivo"
                });
                setMaxLength();
            }
        });
    });

    // Delete Principal
    $(document).on('click', '.delete-action-1', function (e) {
        var that = this;

        var confirm = swal({
            type: 'warning',
            title: 'Eliminar Contenido principal',
            html: '¿Confirma la eliminación?',
            showCloseButton: true,
            showCancelButton: true,
            confirmButtonText: 'Confirmar',
            cancelButtonText: 'Cancelar'
        }).then(function (confirm) {
            if (confirm) {
                var url = $(that).attr('href');

                $.post(url)
                    .then(function (response) {
                        $('#ContenidoPrincipalList').html(response);
                    })
                    .fail(function (xhr, ajaxOptions, thrownError) {
                        swal(
                            'Error',
                             xhr.responseJSON.exMessage,
                            'error'
                        );
                    }).done(function (response, b, c) {
                        swal(
                               '',
                               'Contenido principal eliminado exitosamente',
                               'success'
                             )
                    });
            }
        });

    });

    // Create Secundario
    $('#btnCreateContenidoSecundario').click(function (e) {

        var url = BASE_PATH + 'Seccion/CreateContenidoSecundario';
        $.get(url)
            .success(function (response) {
                $('#SeccionModal .modal-content').html(response);
                $('#SeccionModal').modal('show');
            });
    });

    $(document).on('click', '#btnCreateNewContenidoSecundario', function (e) {
        e.preventDefault();
        var form = $("#CreateContenidoSecundarioForm");
        var url = form.attr('action');
        var data = form.serializeObject();

        $.post(url, data)
            .then(function (response) {
                $('#ContenidoSecundarioList').html(response);
                $('#SeccionModal').modal('hide');
            })
            .fail(function (response, b, c) {
                $('#SeccionModal .modal-content').html(response.responseText);
                setMaxLength();
            })
            .done(function (response, b, c) {
                swal(
                 '',
                 'Contenido Secundario creada exitosamente',
                 'success'
               )
            });
    });
    // Edit Secundario
    $(document).on('click', '.edit-action-2', function (e) {
        var url = $(this).attr('href');

        $.get(url)
            .then(function (response) {
                $('#SeccionModal .modal-content').html(response);
                $('#SeccionModal').modal('show');
            })

    });

    $(document).on('click', '#btnEditContenidoSecundario', function (e) {
        var form = $("#EditContenidoSecundarioForm");
        var url = form.attr('action');
        var data = form.serializeObject();

        $.post(url, data)
            .then(function (response) {
                $('#ContenidoSecundarioList').html(response);
                $('#SeccionModal').modal('hide');
            })
            .fail(function (response) {
                $('#SeccionModal .modal-content').html(response.responseText);
                setMaxLength();
            })
            .done(function (response, b, c) {
                swal(
                       '',
                       'Contenido secundario editado exitosamente',
                       'success'
                     )
            });
    });

    // Delete Secundario
    $(document).on('click', '.delete-action-2', function (e) {
        var that = this;

        var confirm = swal({
            type: 'warning',
            title: 'Eliminar Contenido secundario',
            html: '¿Confirma la eliminación?',
            showCloseButton: true,
            showCancelButton: true,
            confirmButtonText: 'Confirmar',
            cancelButtonText: 'Cancelar'
        }).then(function (confirm) {
            if (confirm) {
                var url = $(that).attr('href');

                $.post(url)
                    .then(function (response) {
                        $('#ContenidoSecundarioList').html(response);
                    })
                   .fail(function (xhr, ajaxOptions, thrownError) {
                       swal(
                           'Error',
                            xhr.responseJSON.exMessage,
                           'error'
                       )
                   }).done(function (response, b, c) {
                       swal(
                              '',
                              'Contenido secundario eliminado exitosamente',
                              'success'
                            )
                   });
            }
        });

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
    $(document).on('keypress', '.prevent-invalid-char', function (e) {
        return permite(e, 'texto');
    });
})();