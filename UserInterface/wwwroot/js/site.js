// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function () {
    var domPaginacion = document.querySelectorAll('.paginationjs');
    if (domPaginacion.length > 0) {
        var mostrarPaginacion = function (pNumPage) {
            $("table.paginationjs tbody tr[data-page]").hide();
            $("table.paginationjs tbody tr[data-page='" + pNumPage + "']").show();
            $("ul.paginationjs").attr("data-pageactive", pNumPage);
            $("ul.paginationjs li[data-typepage='Item']").removeClass("active");
            $("ul.paginationjs li[data-typepage='Item'][data-page='" + pNumPage + "']").addClass("active");
        }
        mostrarPaginacion(1);
        $("ul.paginationjs .page-item").click(function () {
            if ($(this).attr("data-typepage") == "Item") {
                var page = parseInt($(this).attr("data-page"));
                if (isNaN(page)) {
                    page = 1;
                }
                mostrarPaginacion(page);
            }
            else {
                var pageActivo = parseInt($("ul.paginationjs").attr("data-pageactive"));
                if (isNaN(pageActivo)) {
                    pageActivo = 1;
                }
                var numPage = parseInt($("ul.paginationjs").attr("data-numpage"));
                if (isNaN(numPage)) {
                    numPage = 1;
                }
                if ($(this).attr("data-typepage") == "Previous") {
                    if (pageActivo > 1) {
                        var page = pageActivo - 1;
                        mostrarPaginacion(page);
                    }
                }
                else if ($(this).attr("data-typepage") == "Next") {
                    if (pageActivo < numPage) {
                        var page = pageActivo + 1;
                        mostrarPaginacion(page);
                    }
                }
            }
        });
    }
})();

function cerrarAlertaDespuesDeTiempo(alertaId, tiempo) {
    setTimeout(() => {
        document.getElementById(alertaId).classList.add('fade'); // Agregar clase fade para animación de cierre
        setTimeout(() => {
            document.getElementById(alertaId).remove(); // Eliminar la alerta del DOM después de la animación
        }, 500); // 500 milisegundos para que termine la animación de cierre
    }, tiempo); // Tiempo en milisegundos antes de cerrar la alerta automáticamente
}

// Llamar a la función para cerrar la alerta después de 5 segundos (5000 milisegundos)
cerrarAlertaDespuesDeTiempo('alertaDanger', 3000); // Para la alerta de Danger
cerrarAlertaDespuesDeTiempo('alertaPrimary', 3000); // Para la alerta de Primary
