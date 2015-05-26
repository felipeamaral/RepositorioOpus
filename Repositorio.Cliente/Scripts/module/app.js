var app = angular.module('app', ['ngMaterial', 'ui.router']);

app.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {


     // Unmatched url
    $urlRouterProvider.otherwise('/Repositorio/Home');

    // States
    $stateProvider
        .state('home', {
            url: "/Repositorio/Home",
            templateUrl: "Templates/Snippets.html"
        })
        .state('projetos', {
            url: "/Repositorio/Projetos",
            templateUrl: "Templates/Projetos.html"
        })
        .state('snippets', {
            url: "/Repositorio/Snippets",
            templateUrl: "Templates/Snippets.html"
        })
});

app.controller('appCtrl', function ($scope, $timeout, $mdSidenav, $mdUtil, $log) {
    $scope.toggleLeft = buildToggler('left');
    $scope.toggleRight = buildToggler('right');
    $scope.demo = {};
    /**
     * Build handler to open/close a SideNav; when animation finishes
     * report completion in console
     */
    function buildToggler(navID) {
        var debounceFn = $mdUtil.debounce(function () {
            $mdSidenav(navID)
              .toggle()
              .then(function () {
                  $log.debug("toggle " + navID + " is done");
              });
        }, 300);

        return debounceFn;
    }
});

app.controller('leftCtrl', function ($scope, $timeout, $mdSidenav, $log) {
    $scope.close = function () {
        $mdSidenav('left').close()
          .then(function () {
              $log.debug("close LEFT is done");
          });
    };
});

app.controller('rightCtrl', function ($scope, $timeout, $mdSidenav, $log){
    $scope.close = function () {
        $mdSidenav('right').close()
          .then(function () {
              $log.debug("close RIGHT is done");
          });
    };
});

app.controller('ListCtrl', function ($scope, $mdDialog) {
    $scope.project = [
      { name: 'Projeto 1', wanted: true },
      { name: 'Projeto 2', wanted: false },
      { name: 'Projeto 3', wanted: true },
      { name: 'Projeto 4', wanted: false }
    ];
});