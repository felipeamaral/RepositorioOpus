'use strict'

visualizacao.controller('VisualizacaoCtrl', function ($scope, $rootScope) {

    $scope.style = window.parent.angular.element(window.frameElement).scope().aceSessions[1].getValue();
    $scope.script = window.parent.angular.element(window.frameElement).scope().aceSessions[2].getValue();

    document.getElementById("body").innerHTML = window.parent.angular.element(window.frameElement).scope().aceSessions[0].getValue();

    /*$scope.$watch(
        function () {
            return window.parent.angular.element(window.frameElement).scope().aceSessions[0].getValue();
        }, function (newValue, oldValue) {
            console.log("entrou");
            document.getElementById("body").innerHTML = newValue;
        });*/
});