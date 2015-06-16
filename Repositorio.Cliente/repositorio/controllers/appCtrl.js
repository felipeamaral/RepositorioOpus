app.controller('appCtrl', function ($scope, $timeout, $mdSidenav, $mdUtil, $log, $state) {

    $scope.toggleLeft = buildToggler('left');    
    $scope.demo = {};
    $scope.iconMenu = "menu";

    var valorBusca = "";

    // Escuta se a barra lateral está aberta
    $scope.$watch(
        function () {
            return $mdSidenav('left').isOpen();
        },
        function (newValue, oldValue) {
            if (newValue == false) {
                $scope.iconMenu = "menu";
            }
        }
    );


    //Fica escutando pra saber qual o placeholder da barra de busca
    $scope.$on('placeholder', function (event, args) {
        $scope.placeholder = args.place;
    });

    $scope.changeIcon = function () {
        if ($scope.iconMenu === "menu") {
            $scope.iconMenu = "arrow_back";
        } else {
            $scope.iconMenu = "menu";
        }

        $scope.toggleLeft();
    };

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
        }, 1);

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