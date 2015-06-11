'use strict'

visualizacao.controller('VisualizacaoCtrl', function ($scope, $rootScope) { 
     

    //Atualiza os códigos da pagina html
    window.atualiza = function () {
        document.getElementById("style").innerHTML = window.parent.angular.element(window.frameElement).scope().aceSessions[1].getValue();
        document.getElementById("body").innerHTML = window.parent.angular.element(window.frameElement).scope().aceSessions[0].getValue();


        var script = document.createElement('script');
        script[(script.innerText === undefined ? "textContent" : "innerText")] = window.parent.angular.element(window.frameElement).scope().aceSessions[2].getValue();
        document.documentElement.appendChild(script);
    };

    

    // Envia um screenshot da tela
    window.sendImage = function (idComponente) {
        var screen = $("#body");

        html2canvas(screen, {
            onrendered: function (canvas) {

                var image = canvas.toDataURL("image/png");
                image = image.replace('data:image/png;base64,', '');

                // Envia imagem pra web api
                $.ajax({
                    type: 'POST',
                    url: 'http://localhost:53412/api/snippet/' + idComponente + '/imagem/upload',
                    data: { img: image }
                }).done(function (data) { });
            }
        });
    };

});