app.controller('appCtrl', function ($scope, $timeout, $mdSidenav, $mdUtil, $log) {

    $scope.toggleLeft = buildToggler('left');
    $scope.toggleRight = buildToggler('right');
    $scope.demo = {};

    var valorBusca = "";

    //Fica escutando pra saber qual o placeholder da barra de busca
    $scope.$on('placeholder', function (event, args) {
        $scope.placeholder = args.place;
    });

    $scope.keyPress = function (event) {
        if (event.which === 13) {
            valorBusca = $scope.valorBusca;
            $scope.$broadcast('busca', { valor: $scope.valorBusca });
        }
    }

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

    // Esconde a barra de busca
    $scope.closeSearch = function () {
        // Limpa a barra de busca pra próxima pesquisa
        $scope.valorBusca = "";
        $("#searchToolBar").addClass("hidden");
        $("#menuToolBar").removeClass("hidden");
    };

    // Abre a barra de busca
    $scope.openSearch = function () {
        $scope.valorBusca = valorBusca;

        $("#menuToolBar").addClass("hidden");
        $("#searchToolBar").removeClass("hidden");
    };
});


app.controller('leftCtrl', function ($scope, $timeout, $mdSidenav, $log) {
    $scope.close = function () {
        $mdSidenav('left').close()
          .then(function () {
              $log.debug("close LEFT is done");
          });
    };
});

app.controller('rightCtrl', function ($scope, $timeout, $mdSidenav, $log) {
    $scope.close = function () {
        $mdSidenav('right').close()
          .then(function () {
              $log.debug("close RIGHT is done");
          });
    };
});
