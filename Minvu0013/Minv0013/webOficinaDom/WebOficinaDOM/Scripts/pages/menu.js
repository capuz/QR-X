(function () {


    $(document).on('click', '.prevent-default', function (e) {
        e.preventDefault();
    });
    function setMaxLength() {

        $("#MenuModal input[data-val-length-max],textarea[data-val-length-max]").each(function () {
            var $this = $(this);
            var data = $this.data();
            $this.attr("maxlength", data.valLengthMax);
        });
    };
    $('#MenuModal').on('shown.bs.modal', function () {

        setMaxLength();
    });
    // Create 
    $('#btnCreateMenu').click(function (e) {

        var url = BASE_PATH + 'menu/create';
        $.get(url)
            .success(function (response) {
                $('#MenuModal .modal-content').html(response);
                $('#MenuModal').modal('show');
            })
           .error(function (response) {
               $('#MenuModal .modal-content').html(response.responseText);
               $('#MenuModal').modal('show');
           });
    });

    $(document).on('click', '#btnCreateNewMenu', function (e) {
        e.preventDefault();
        var $form = $("#CreateMenuForm");
        var url = $form.attr('action');
        var data = $form.serializeObject();

        $.post(url, data)
            .then(function (response) {
                $('#MenuList').html(response);
                $('#MenuModal').modal('hide');
            })
            .fail(function (response, b, c) {
                $('#MenuModal .modal-content').html(response.responseText);
                setMaxLength();
            })
            .done(function (response, b, c) {
                swal(
                 '',
                 'Opción de Menú creada exitosamente',
                 'success'
               )
            });
    });

    // Edit
    $(document).on('click', '.edit-action', function (e) {
        var url = $(this).attr('href');

        $.get(url)
            .then(function (response) {
                $('#MenuModal .modal-content').html(response);
                $('#MenuModal').modal('show');
            })
            .fail(function (xhr, status, error) {
                $('#MenuModal .modal-content').html(response.responseText);
                $('#MenuModal').modal('show');
            });
    });

    $(document).on('click', '#btnEditarMenu', function (e) {
        var $form = $("#EditMenuForm");
        var url = $form.attr('action');
        var data = $form.serializeObject();

        $.post(url, data)
            .then(function (response) {
                $('#MenuList').html(response);
                $('#MenuModal').modal('hide');
            })
            .fail(function (response) {
                $('#MenuModal .modal-content').html(response.responseText);
                setMaxLength();
            })
            .done(function (response, b, c) {
                swal(
                       '',
                       'Opción de Menú editada exitosamente',
                       'success'
                     )
            });
    });

    // Delete
    $(document).on('click', '.delete-action', function (e) {
        var that = this;

        if ($(that).data('padre') == 1) {
            swal('',
                 'No es posible eliminar un Menú Padre',
                 'info'
                 )
            return false;
        }
        var confirm = swal({
            type: 'warning',
            title: 'Eliminar Menú',
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
                        $('#MenuList').html(response);
                    })
                    .fail(function (response) {
                        swal(
                            'Error',
                            'Hubo un error durante el procesamiento',
                            'error'
                        );
                    }).done(function (response, b, c) {
                        swal(
                               '',
                               'Opción de Menú eliminada exitosamente',
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
