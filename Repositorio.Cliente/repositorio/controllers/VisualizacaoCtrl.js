'use strict'

visualizacao.controller('VisualizacaoCtrl', function ($scope, $rootScope) { 
     

    window.atualiza = function () {
        document.getElementById("style").innerHTML = window.parent.angular.element(window.frameElement).scope().aceSessions[1].getValue();
        document.getElementById("body").innerHTML = window.parent.angular.element(window.frameElement).scope().aceSessions[0].getValue();


        var script = document.createElement('script');
        script[(script.innerText === undefined ? "textContent" : "innerText")] = window.parent.angular.element(window.frameElement).scope().aceSessions[2].getValue();
        document.documentElement.appendChild(script);
    }

});