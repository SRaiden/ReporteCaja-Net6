function Validar() {
    var FechaDesde = document.getElementById("desde").value;
    var FechaHasta = document.getElementById("hasta").value;


    var rad = 0;
    const radioButtons = document.querySelectorAll('input[name="rbReporte"]');
    for (const a of radioButtons) {
        if (a.checked) {
            rad = 1;
            break;
        }
    }

    if (rad == 0) {
        alert("Ingrese un tipo de Reporte");
        return false;
    }

    if (FechaDesde == "") {
        alert("Ingrese un Periodo valido");
        return false;
    }

    if (FechaHasta == "") {
        alert("Ingrese un Periodo valido");
        return false;
    }

    return true;
}