    function permite(elEvento, permitidos) {
        // Variables que definen los caracteres permitidos
        var numeros = "0123456789";
        var letras = " áéíóúabcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ";

        var guion = "-";
        var K = "Kk";
        var arroba = "@";
        var punto = ".";
        var underline = "_";
        var coma = ",";

        var rut = numeros + guion + K;
        var texto = numeros + letras + coma + punto + underline + guion;
        var email = texto + arroba + punto + guion + underline;

        // 8 = BackSpace, 46 = Supr, 37 = flecha izquierda, 39 = flecha derecha    
        var teclas_especiales = [8];

        // Seleccionar los caracteres a partir del parámetro de la función
        switch (permitidos) {
            case 'numeros':
                permitidos = numeros;
                break;
            case 'letras':
                permitidos = letras;
                break;
            case 'texto':
                permitidos = texto;
                break;
            case 'rut':
                permitidos = rut;
                break;
            case 'email':
                permitidos = email;
                break;
        }

        // Obtener la tecla pulsada 
        var evento = elEvento || window.event;
        var codigoCaracter = evento.charCode || evento.keyCode;
        var caracter = String.fromCharCode(codigoCaracter);
        // Comprobar si la tecla pulsada es alguna de las teclas especiales
        // (teclas de borrado y flechas horizontales)
        var tecla_especial = false;
        for (var i in teclas_especiales) {
            if (codigoCaracter == teclas_especiales[i]) {
                tecla_especial = true;
                break;
            }
        }
        // Comprobar si la tecla pulsada se encuentra en los caracteres permitidos
        // o si es una tecla especial
        return permitidos.indexOf(caracter) != -1 || tecla_especial;
        //return permitidos.indexOf(caracter) != -1 && tecla_especial == false;
        //return permitidos.indexOf(caracter) != -1;
    }

