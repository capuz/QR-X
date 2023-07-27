function renderAlert() {
    //PartialView _Alert
    $('#alertContainer').load('/Alerts/Render');
}

function obtenerMotivosRechazo(muestrasId) {
    //se trabaja con modal PartialView _ModalConfirmarRechazo
    $.ajax({
        url: '/Rechazos/ObtenerMotivos',
        type: 'POST',
        data: { muestrasId: muestrasId },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.responseText + " Ha ocurrido un error.");
        },
        success: function (data) {
            var motivosRechazos = $(".modal-body #motivosRechazo");

            motivosRechazos.find('option').remove().end()
            .append($("<option />").val('').text('[ Seleccione ]'));

            $.each(data, function (i, value) {
                motivosRechazos.append($("<option />").val(value.Id).text(value.Nombre));
            });
        }
    });

}