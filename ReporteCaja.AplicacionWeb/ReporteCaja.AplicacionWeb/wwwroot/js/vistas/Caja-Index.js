const modelBase = {
    idUsuario: 0,
    idSucursal: 0,
    idEmpresa: 0,
    tipo: "",
    FechaDesde: "",
    FechaHasta: ""
};

let tablaData;
let tablaDataVer;
let tablaDataPedido;

// PARA LOGIN
var empLogin = $("#cboEmpresas").val();
var sucLogin = $("#cboSucursales").val();
var userLogin = $("#userLogin").val();
var rolLogin = $("#rolLogin").val();


$(document).ready(function () {
    $("#rbCaja").prop("checked", true);

    // Obtener la fecha de hoy
    const hoy = new Date();

    // Calcular la fecha de 30 días antes
    const fecha30DiasAntes = new Date(hoy);
    fecha30DiasAntes.setDate(hoy.getDate() - 30);

    // Formatear la fecha como YYYY-MM-DD
    const fechaFormateada = fecha30DiasAntes.toISOString().split('T')[0];
    const fechahoyFormateada = hoy.toISOString().split('T')[0];

    // Establecer la fecha en el campo de fecha
    $('#txtDesde').val(fechaFormateada);
    $('#txtHasta').val(fechahoyFormateada);

    if (empLogin == null && sucLogin == null) { // SOY SUPERADMIN
        fetch("/Home/ObtenerTodasEmpresas")
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                if (responseJson.length > 0) {
                    responseJson.sort((a, b) => a.nombreEmpresa.localeCompare(b.nombreEmpresa));
                    responseJson.forEach((item) => {
                        $("#cboEmpresas").append(
                            $("<option>").val(item.id).text(item.nombreEmpresa)
                        )
                    })
                }
            })
        if (empLogin == null) empLogin = "7";
    }
    if (empLogin != null && sucLogin == null) { // SOY ADMIN DE SUCURSALES

        fetch(`/Home/ObtenerSucursalEmpresa?idEmpresa=${empLogin}`, {
            method: "GET"
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                $('#cboSucursales').empty();
                //$("#cboSucursales").append(
                //    $("<option>").val(0).text("- TODAS LAS SUCURSALES -") // OPTION DE ADMINISTRADOR
                //)
                if (responseJson.length > 0) {
                    responseJson.sort((a, b) => a.nombreSucursal.localeCompare(b.nombreSucursal));
                    responseJson.forEach((item) => {
                        $("#cboSucursales").append(
                            $("<option>").val(item.id).text(item.nombreSucursal)
                        )
                    })
                }
            })
    }

});

$("#cboEmpresas").on("change", function () {
    var optionEmpresa = $(this).find(":selected").val()
    cargarSucursales(optionEmpresa)

});

function cargarSucursales(idEmpresa) {
    fetch(`/Home/ObtenerSucursalEmpresa?idEmpresa=${idEmpresa}`, {  // ELIMINAR USUARIO
        method: "GET"
    })
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => { // creamos un response a la bbdd
            $('#cboSucursales').empty();
            $("#cboSucursales").append(
                $("<option>").val(0).text("- TODAS LAS SUCURSALES -"), // OPTION DE ADMINISTRADOR
            )
            if (responseJson.length > 0) { // encontro datos en la bbdd?
                responseJson.sort((a, b) => a.nombreSucursal.localeCompare(b.nombreSucursal));
                responseJson.forEach((item) => {
                    $("#cboSucursales").append( // hacemos llamado a la etiqueta cbroRol
                        $("<option>").val(item.id).text(item.nombreSucursal)
                    )
                })
            }
        });
};

$("#cboEmpresas").on("change", function () {
    busqueda();
});

$("#cboSucursales").on("change", function () {
    busqueda();
});

$('input[type=radio][name=rbReporte]').change(function () {
    busqueda();
});

$("#txtDesde").blur(function () {
    busqueda();
});

$("#txtHasta").blur(function () {
    busqueda();

});

function model() {
    const modelo = structuredClone(modelBase);
    modelo["idUsuario"] = parseInt(userLogin);
    modelo["idEmpresa"] = parseInt($("#cboEmpresas").val());

    if (sucLogin == null) { // soy algun admin
        modelo["idSucursal"] = parseInt($("#cboSucursales").val())
    } else { // soy user
        modelo["idSucursal"] = sucLogin;
    }

    if ($('#rbCaja').prop('checked')) modelo["tipo"] = "caja"
    else if ($('#rbProductos').prop('checked')) modelo["tipo"] = "productos"
    else if ($('#rbTickets').prop('checked')) modelo["tipo"] = "tickets"
    else if ($('#rbFormasPago').prop('checked')) modelo["tipo"] = "formasPago"
    else modelo["tipo"] = "cierreZ"

    modelo["FechaDesde"] = $("#txtDesde").val()
    modelo["FechaHasta"] = $("#txtHasta").val()

    return modelo;
}

function busqueda() {

    var modeloObtenido = model();

    fetch("/Home/ObtenerTabla", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(modeloObtenido)
    })
    .then(response => {
        $("#modalData").find("div.modal-content").LoadingOverlay("hide");
        return response.ok ? response.json() : Promise.reject(response);
    })
    .then(responseJson => {
        if ((modeloObtenido["tipo"] == "caja" && responseJson.length === 0) || (modeloObtenido["tipo"] == "productos" && responseJson.productos.length === 0) ||
            (modeloObtenido["tipo"] == "tickets" && responseJson.sfDetallesTicketsDiarios.length === 0) || (modeloObtenido["tipo"] == "formasPago" && responseJson.sfCajaDetalleFormaPago.length === 0) ||
            (modeloObtenido["tipo"] == "cierreZ" && responseJson.sfListaCierreZ.length === 0)) {
            swal("Lo sentimos!", "No hay datos encontrados", "error")
        }
        else {
            if (modeloObtenido["tipo"] == "caja") { // RADIO CAJA

                // HEAD
                {
                    const tablaEncabezado = $("#tbdata thead"); // Suponiendo que el id de la tabla es "TBDATA"
                    tablaEncabezado.empty();              // Limpia el contenido actual de la tabla

                    // Agrega el nuevo encabezado
                    const nuevoEncabezado = `
                    <tr style="font-size:25px;">
                        <th style="text-align: center;background-color:#17202A;color:#E9F7EF;width:300px"><strong style="font-size: medium;">${responseJson[0].nombreSucursal}</strong></th>
                        <th style="text-align: right;background-color:#17202A;color:#E9F7EF;" colspan='2'>
                        </th>
                    </tr>`;

                    //<th style="text-align: right;background-color:#17202A;color:#E9F7EF;" colspan='2'>
                    //    <button class="btn_imprimir btn btn-primary" id="btn_imprimir" style="position:relative; min-width:100px;margin-left:0px;" value="1" type="button" onclick="enviarPDF()">
                    //        Imprimir
                    //    </button>
                    //</th>

                    tablaEncabezado.append(nuevoEncabezado);
                }

                // BODY
                {

                    const tablaCuerpo = $("#tbdata tbody"); // Suponiendo que el id de la tabla es "TBDATA"
                    tablaCuerpo.empty();              // Limpia el contenido actual de la tabla

                    // Recorre los datos y agrega filas al cuerpo de la tabla
                    responseJson.forEach(items => {
                        // Agrega fila principal con NroCierre, Fecha y Hora
                        const filaPrincipal = `
                        <tr>
                            <td style="text-align: center;vertical-align: middle; padding-top: 10px;">
                                <strong><b>Nro Cierre: ${items.sfCajaCabeceraCaja.nroCierreCabecera}</b></strong><br />
                                <strong><b>Fecha: ${items.sfCajaCabeceraCaja.fechaCabecera}</b></strong><br />
                                <strong><b>Hora: ${items.sfCajaCabeceraCaja.horaCabecera}</b></strong><br />
                            </td>
                            <th style="border-bottom: solid;">
                                <strong>Descripcion</strong>
                            </th>
                            <th style="border-bottom: solid;text-align: right;">
                                <strong>Total</strong>
                            </th>
                        </tr>`;
                        tablaCuerpo.append(filaPrincipal);

                        // Recorre los subelementos si existen en responseJson.items
                        if (items.sfCajaDetalleCaja != null) {
                            items.sfCajaDetalleCaja.forEach(ite => {
                                let filaSubitem = '';

                                filaSubitem = `
                                <tr>
                                    <td></td>
                                    <td style="text-align: left;">${ite.descripcionDetalle}</td>
                                    <td style="text-align: right; ">${ite.totalDetalle}</td>
                                </tr>`;
                                tablaCuerpo.append(filaSubitem);

                            });

                            filaSubitem = `
                                    <tr>
                                        <td colspan="2" align="right" style=" border-bottom:dotted"><strong>SubTotal : $</strong></td>
                                        <td style="text-align:right ;border-top: solid; border-bottom:dotted">${items.sfCajaCabeceraCaja.totalXCierreDetalle}</td>
                                    </tr>`;
                            tablaCuerpo.append(filaSubitem);
                        }
                    });
                }

                // FOOT
                {

                    const pieTabla = $("#tbdata tfoot"); // Suponiendo que el pie de la tabla está dentro de un elemento tfoot
                    pieTabla.empty();

                    // Agrega fila de total en el pie de la tabla
                    const filaTotal = `
                    <tr style="border-bottom:solid;border-top: solid">
                        <td colspan="2" align="right"><strong>Total</strong></td>
                        <td style="text-align:right"><strong>${responseJson[0].totalFinalCaja}</strong></td>
                    </tr>`;
                    pieTabla.append(filaTotal);
                }

            }
            else if (modeloObtenido["tipo"] == "productos") {

                // HEAD
                {
                    const tablaEncabezado = $("#tbdata thead"); // Suponiendo que el id de la tabla es "TBDATA"
                    tablaEncabezado.empty();              // Limpia el contenido actual de la tabla

                    // Agrega el nuevo encabezado
                    const nuevoEncabezado = `
                    <tr style="font-size:25px;">
                        <th style="text-align: center;background-color:#17202A;color:#E9F7EF;width:300px"><strong style="font-size: medium;">${responseJson.nombreSucursal}</strong></th>
                        <th style="text-align: right;background-color:#17202A;color:#E9F7EF;" colspan='5'>
                        </th>
                    </tr>`;

                    //<th style="text-align: right;background-color:#17202A;color:#E9F7EF;" colspan='5'>
                    //    <div style="display:none;"></div>
                    //    <button class="btn_imprimir" id="btn_imprimir" style="position:relative; min-width:100px;margin-left:0px;" value="2" type="button">
                    //        Imprimir
                    //    </button>
                    //</th>

                    tablaEncabezado.append(nuevoEncabezado);
                }

                // BODY
                {

                    const tablaCuerpo = $("#tbdata tbody"); // Suponiendo que el id de la tabla es "TBDATA"
                    tablaCuerpo.empty();                    // Limpia el contenido actual de la tabla

                    const filaPrincipal = `
                        <tr>
                            <td style="text-align:center; "><strong>Codigo</strong></td>
                            <td style="text-align:left;" colspan="2"><strong>Descripcion</strong></td>
                            <td style="text-align: center;"><strong>Cant.</strong></td>
                            <td style="text-align: right;" colspan="2"><strong>Subtotal</strong></td>
                        </tr>`
                    tablaCuerpo.append(filaPrincipal);

                    // Recorre los datos y agrega filas al cuerpo de la tabla
                    responseJson.productos.forEach(items => {
                        // Agrega fila principal con NroCierre, Fecha y Hora
                        const filaPrincipal = `
                        <tr>
                            <td style="text-align:center;">${items.codigo}</td>
                            <td style="text-align:left;">${items.nombre}</td>
                            <td></td>
                            <td style="text-align: center;">${items.cantidad}</td>
                            <td></td>
                            <td style="text-align: right;">${items.subTotal}</td>
                        </tr>`;
                        tablaCuerpo.append(filaPrincipal);

                    });
                }

                // FOOT
                {

                    const pieTabla = $("#tbdata tfoot"); // Suponiendo que el pie de la tabla está dentro de un elemento tfoot
                    pieTabla.empty();

                    // Agrega fila de total en el pie de la tabla
                    const filaTotal = `
                    <tr>
                        <td colspan="2"></td>
                        <td colspan="4" style="padding: 0; border-bottom: solid"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td><strong>Cantidad</strong></td>
                        <td style="text-align: center;">${responseJson.cantidadTotal}</td>
                        <td><strong>Total $</strong></td>
                        <td style="text-align: right;">${responseJson.total}</td>
                    </tr>`;
                    pieTabla.append(filaTotal);
                }
            }
            else if (modeloObtenido["tipo"] == "tickets") {

                // HEAD
                {

                    const tablaEncabezado = $("#tbdata thead"); // Suponiendo que el id de la tabla es "TBDATA"
                    tablaEncabezado.empty();              // Limpia el contenido actual de la tabla

                    // Agrega el nuevo encabezado
                    const nuevoEncabezado = `
                    <tr style="font-size:25px;">
                        <th style="text-align: center;background-color:#17202A;color:#E9F7EF;width:300px"><strong style="font-size: medium;">${responseJson.nombreSucursal}</strong></th>
                        <th style="text-align: right;background-color:#17202A;color:#E9F7EF;" colspan='7'>
                        </th>
                    </tr>`;

                    //<th style="text-align: right;background-color:#17202A;color:#E9F7EF;" colspan='7'>
                    //    <div style="display:none;"></div>
                    //    <button class="btn_imprimir" id="btn_imprimir" style="position:relative; min-width:100px;margin-left:0px;" value="3" type="button">
                    //        Imprimir
                    //    </button>
                    //</th>

                    tablaEncabezado.append(nuevoEncabezado);
                }

                // BODY
                {

                    const tablaCuerpo = $("#tbdata tbody"); // Suponiendo que el id de la tabla es "TBDATA"
                    tablaCuerpo.empty();                    // Limpia el contenido actual de la tabla

                    const filaPrincipal = `
                        <tr></tr>
                    <tr>
                        <td colspan="2"></td>
                        <td style="background: #C8C7C7; text-align: center;" colspan="2"><strong>Delivery</strong></td>
                        <td style="background: #C8C7C7; text-align: center;" colspan="2"><strong>Mostrador</strong></td>
                        <td style="background: #C8C7C7; text-align: center;" colspan="2"><strong>Total</strong></td>
                    </tr>

                    <tr>
                        <td style="text-align: left;" colspan="2"><strong>Fecha</strong></td>
                        <td style="text-align: center;"><strong>Cant.</strong></td>
                        <td style="text-align: center;"><strong>Total</strong></td>
                        <td style="text-align: center;"><strong>Cant.</strong></td>
                        <td style="text-align: center;"><strong>Total</strong></td>
                        <td style="text-align: center;"><strong>Cant.</strong></td>
                        <td style="text-align: center;"><strong>Total</strong></td>
                    </tr>

                    <tr>
                        <td style="padding:0; border-bottom: solid" colspan="8"></td>
                    </tr>`
                    tablaCuerpo.append(filaPrincipal);

                    // Recorre los datos y agrega filas al cuerpo de la tabla
                    responseJson.sfDetallesTicketsDiarios.forEach(items => {
                        // Agrega fila principal con NroCierre, Fecha y Hora
                        const filaPrincipal = `
                         <tr>
                            <td style="text-align:left;" colspan="2">${items.fecha}</td>
                            <td style="text-align: center;">${items.cantidadDelivery}</td>
                            <td style="text-align: right;">${items.totalDelivery}</td>
                            <td style="text-align: center;">${items.cantidadMostrador}</td>
                            <td style="text-align: right;">${items.totalMostrador}</td>
                            <td style="text-align: center;">${items.cantidad}</td>
                            <td style="text-align: right;">${items.total}</td>
                        </tr>
                        `;
                        tablaCuerpo.append(filaPrincipal);

                    });
                }

                // FOOT
                {
                    const pieTabla = $("#tbdata tfoot"); // Suponiendo que el pie de la tabla está dentro de un elemento tfoot
                    pieTabla.empty();

                    // Agrega fila de total en el pie de la tabla
                    const filaTotal = `
                     <tr>
                        <td colspan="2"></td>
                        <td colspan="6" style="padding: 0; border-bottom: solid"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><b>TOTALES: </b></td>
                        <td style="text-align: center;">${responseJson.cantidadDeliveryTotal}</td>
                        <td style="text-align: right;">${responseJson.totalDeliveryTotal}</td>
                        <td style="text-align: center;">${responseJson.cantidadMostradorTotal}</td>
                        <td style="text-align: right;">${responseJson.totalMostradorTotal}</td>
                        <td style="text-align: center;">${responseJson.cantidadTotal}</td>
                        <td style="text-align: right;">${responseJson.totalTotal}</td>
                    </tr>
                    `;
                    pieTabla.append(filaTotal);
                }
            }
            else if (modeloObtenido["tipo"] == "formasPago") {

                // HEAD
                {

                    const tablaEncabezado = $("#tbdata thead"); // Suponiendo que el id de la tabla es "TBDATA"
                    tablaEncabezado.empty();              // Limpia el contenido actual de la tabla

                    // Agrega el nuevo encabezado
                    const nuevoEncabezado = `
                    <tr style="font-size:25px;">
                        <th style="text-align: center;background-color:#17202A;color:#E9F7EF;width:300px"><strong style="font-size: medium;">${responseJson.nombreSucursal}</strong></th>
                        <th style="text-align: right;background-color:#17202A;color:#E9F7EF;" colspan='3'>
                        </th>
                    </tr>`;

                    //<th style="text-align: right;background-color:#17202A;color:#E9F7EF;" colspan='3'>
                    //    <div style="display:none;"></div>
                    //    <button class="btn_imprimir" id="btn_imprimir" style="position:relative; min-width:100px;margin-left:0px;" value="4" type="button">
                    //        Imprimir
                    //    </button>
                    //</th>

                    tablaEncabezado.append(nuevoEncabezado);
                }

                // BODY
                {

                    const tablaCuerpo = $("#tbdata tbody"); // Suponiendo que el id de la tabla es "TBDATA"
                    tablaCuerpo.empty();                    // Limpia el contenido actual de la tabla

                    const filaPrincipal = `
                        <tr></tr>
                    <tr>
                        <td style="text-align: left;" colspan="2"><strong>Descripcion</strong></td>
                        <td style="text-align: center;"><strong>Cant</strong></td>
                        <td style="text-align: center;"><strong>Total</strong></td>
                    </tr>`
                    tablaCuerpo.append(filaPrincipal);

                    // Recorre los datos y agrega filas al cuerpo de la tabla
                    responseJson.sfCajaDetalleFormaPago.forEach(items => {
                        // Agrega fila principal con NroCierre, Fecha y Hora
                        const filaPrincipal = `
                         <tr>
                            <td style="text-align: left;" colspan="2">${items.descripcion}</td>
                            <td style="text-align: center;">${items.cantidad}</td>
                            <td style="text-align: right;">${items.total}</td>
                        </tr>
                        `;
                        tablaCuerpo.append(filaPrincipal);

                    });
                }

                // FOOT
                {
                    const pieTabla = $("#tbdata tfoot"); // Suponiendo que el pie de la tabla está dentro de un elemento tfoot
                    pieTabla.empty();

                    // Agrega fila de total en el pie de la tabla
                    const filaTotal = `
                    <tr>
                        <td colspan="2"></td>
                        <td style="padding:0; border-bottom: solid" colspan="2"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><strong>TOTALES:</strong></td>
                        <td style="text-align: center;">${responseJson.cantidad}</td>
                        <td style="text-align: right;">${responseJson.total}</td>
                    </tr> `;
                    pieTabla.append(filaTotal);
                }
            }
            else if (modeloObtenido["tipo"] == "cierreZ") {

                // HEAD
                {

                    const tablaEncabezado = $("#tbdata thead");     // Suponiendo que el id de la tabla es "TBDATA"
                    tablaEncabezado.empty();                        // Limpia el contenido actual de la tabla

                    // Agrega el nuevo encabezado
                    const nuevoEncabezado = `
                    <tr style="font-size:25px;">
                        <th style="text-align: center;background-color:#17202A;color:#E9F7EF;width:300px"><strong style="font-size: medium;">${responseJson.nombreSucursal}</strong></th>
                        <th style="text-align: right;background-color:#17202A;color:#E9F7EF;" colspan='4'>
                        </th>
                    </tr>`;

                    //<th style="text-align: right;background-color:#17202A;color:#E9F7EF;" colspan='4'>
                    //    <div style="display:none;"></div>
                    //    <button class="btn_imprimir" id="btn_imprimir" style="position:relative; min-width:100px;margin-left:0px;" value="5" type="button">
                    //        Imprimir
                    //    </button>
                    //</th>

                    tablaEncabezado.append(nuevoEncabezado);
                }

                // BODY
                {

                    const tablaCuerpo = $("#tbdata tbody"); // Suponiendo que el id de la tabla es "TBDATA"
                    tablaCuerpo.empty();                    // Limpia el contenido actual de la tabla

                    // Recorre los datos y agrega filas al cuerpo de la tabla
                    responseJson.sfListaCierreZ.forEach(items => {
                        // Agrega fila principal con NroCierre, Fecha y Hora
                        const filaPrincipal = `
                        <tr>
                            <td style="background: #C8C7C7; text-align: left;" colspan="5"><strong>Ticket: ${items.numero}</strong></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="padding:0; border-bottom: solid" colspan="4"></td>
                        </tr>

                        <tr>
                            <td style="text-align: left; "><strong> Fecha: ${items.fecha}</strong></td>
                            <td style="border-left: solid; text-align: center; " colspan="2"><strong>Tickets</strong></td>
                            <td style="text-align: center; border-left: solid; border-right: solid;" colspan="2"><strong>Notas de Credito</strong></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="border-left: solid; text-align: center;" colspan="2"><strong>Emitidos: </strong>${items.t_Emitido}</td>
                            <td style="border-left: solid; border-right: solid; text-align: center;" colspan="2"><strong>Emitidos: </strong>${items.nC_Emitido}</td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="border-left: solid; text-align: center;" colspan="2"><strong>Cancelados: </strong>${items.t_Cancelado}</td>
                            <td style="border-left: solid; border-right: solid; text-align: center;" colspan="2"><strong>Cancelados: </strong>${items.nC_Cancelado}</td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="border-left: solid; text-align: center;" colspan="2"><strong>Excento: </strong>${items.t_Exento}</td>
                            <td style="border-left: solid; border-right: solid; text-align: center;" colspan="2"><strong>Excento: </strong>${items.nC_Gravado}</td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="border-left: solid; text-align: center;" colspan="2"><strong>Gravado: </strong>${items.t_Gravado}</td>
                            <td style="border-left: solid; border-right: solid; text-align: center;" colspan="2"><strong>Gravado: </strong>${items.nC_Exento}</td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="border-left: solid; text-align: center;" colspan="2"><strong>IVA: </strong>${items.t_IVA}</td>
                            <td style="border-left: solid; border-right: solid; text-align: center;" colspan="2"><strong>IVA: </strong>${items.nC_IVA}</td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="border-left: solid; text-align: center;" colspan="2"><strong>No Gravado: </strong>${items.t_NoGrabado}</td>
                            <td style="border-left: solid; border-right: solid; text-align: center;" colspan="2"><strong>No Gravado: </strong>${items.nC_NoGrabado}</td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="border-left: solid; text-align: center;" colspan="2"><strong>Total Tributos: </strong>${items.t_TotalTributos}</td>
                            <td style="border-left: solid; border-right: solid; text-align: center;" colspan="2"><strong>Total Tributos: </strong>${items.nC_TotalTributos}</td>
                        </tr>

                        <tr>
                            <td></td>
                            <td style="border-left: solid; text-align: center;" colspan="2"><strong>Total: </strong>${items.t_Total}</td>
                            <td style="border-left: solid; border-right: solid; text-align: center;" colspan="2"><strong>Total: </strong>${items.nC_Total}</td>
                        </tr>

                        <tr>
                            <td style="padding:0;"></td>
                            <td style="padding:0; border-bottom: solid" colspan="4"></td>
                        </tr>

                        <tr>
                            <td style="border-bottom:dotted" colspan="5"></td>
                        </tr>

                        <tr></tr>
                        `;
                        tablaCuerpo.append(filaPrincipal);

                    });
                }

                // FOOT
                {
                    const pieTabla = $("#tbdata tfoot"); // NO HAY PIE EN CIERRE Z
                    pieTabla.empty();
                }
            }
        }
    });
}

function enviarPDF() {
    var modelo = model();
    var valorBoton = $("#btn_imprimir").val();
    $("#btn_imprimir").attr("href", `/Home/MostrarPDFPedido?elemento=${valorBoton}&model=${modelo}`)
}


