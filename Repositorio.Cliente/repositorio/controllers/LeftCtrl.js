app.controller('LeftCtrl', function ($scope, $state, $mdDialog) {

    //Controla o icone das opções primarias do menu
    $scope.iconSnippets = "add";
    $scope.iconTemas = "add";
    $scope.iconProjs = "add";
    $scope.iconPlugins = "add";
    $scope.iconTemplates = "add";

    //Controla se mostra as opções secundárias de um menu
    $scope.snippetsOptions = false;
    $scope.temasOptions = false;
    $scope.projsOptions = false;
    $scope.pluginsOptions = false;
    $scope.templatesOptions = false;

    $scope.toggleTemplates = function (esconde) {
        if (esconde) {
            $scope.iconTemplates = "add";
            $scope.templatesOptions = false;
        } else {
            $scope.iconTemplates = $scope.iconTemplates == "add" ? "remove" : "add";
            $scope.templatesOptions = $scope.templatesOptions == true ? false : true;
            $scope.togglePlugins(true);
            $scope.toggleProjs(true);
            $scope.toggleSnippets(true);
            $scope.toggleTemas(true);
        }
    };

    $scope.toggleTemas = function (esconde) {
        if (esconde) {
            $scope.iconTemas = "add";
            $scope.temasOptions = false;
        } else {
            $scope.iconTemas = $scope.iconTemas == "add" ? "remove" : "add";
            $scope.temasOptions = $scope.temasOptions == true ? false : true;
            $scope.togglePlugins(true);
            $scope.toggleProjs(true);
            $scope.toggleSnippets(true);
            $scope.toggleTemplates(true);
        }
    };
    
    $scope.toggleSnippets = function (esconde) {
        if (esconde) {
            $scope.iconSnippets = "add";
            $scope.snippetsOptions = false;
        } else {
            $scope.iconSnippets = $scope.iconSnippets == "add" ? "remove" : "add";
            $scope.snippetsOptions = $scope.snippetsOptions == true ? false : true;
            $scope.togglePlugins(true);
            $scope.toggleProjs(true);
            $scope.toggleTemas(true);
            $scope.toggleTemplates(true);
        }
    };

    $scope.toggleProjs = function (esconde) {
        if (esconde) {
            $scope.iconProjs = "add";
            $scope.projsOptions = false;
        } else {
            $scope.iconProjs = $scope.iconProjs == "add" ? "remove" : "add";
            $scope.projsOptions = $scope.projsOptions == true ? false : true;
            $scope.togglePlugins(true);
            $scope.toggleSnippets(true);
            $scope.toggleTemas(true);
            $scope.toggleTemplates(true);
        }
    };

    $scope.togglePlugins = function (esconde) {
        if (esconde) {
            $scope.iconPlugins = "add";
            $scope.pluginsOptions = false;
        } else {
            $scope.iconPlugins = $scope.iconPlugins == "add" ? "remove" : "add";
            $scope.pluginsOptions = $scope.pluginsOptions == true ? false : true;
            $scope.toggleProjs(true);
            $scope.toggleSnippets(true);
            $scope.toggleTemas(true);
            $scope.toggleTemplates(true);
        }
    };

    $scope.goTo = function (status) {
        $state.go(status);
        $scope.toggleLeft();
        setTimeout(function () {
            $scope.toggleProjs(true);
            $scope.toggleSnippets(true);
            $scope.toggleTemas(true);
            $scope.togglePlugins(true);
            $scope.toggleTemplates(true);
        }, 200);
    };

    $scope.addProjeto = function (ev) {

        $scope.$emit('modal', null);

        $mdDialog.show({
            controller: 'AddProjetoCtrl',
            templateUrl: 'repositorio/templates/AddProjetoModal.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            locals: {
                nome: null
            }
        })
        .then(function (projeto) {
            $scope.toggleProjs(true);
            //ok
        });
    };

    // Abre o modal que gera o template com .NET e angular material
    $scope.gerarMaterialTemplate = function (ev) {

        // Faz com que o menu lateral seja escondido
        $scope.$emit('modal', null);

        // Chama o modal de geração do template
        $mdDialog.show({
            controller: 'materialTemplateCtrl',
            templateUrl: 'repositorio/templates/MaterialDesignModal.html',
            parent: angular.element(document.body),
            targetEvent: ev
        }).then(function () {
            $scope.toggleTemplates(true);
        });
    };

    // Abre o modal que gera o template com .NET e angular
    $scope.gerarAngularTemplate = function (ev) {

        // Faz com que o menu lateral seja escondido
        $scope.$emit('modal', null);

        // Chama o modal de geração do template
        $mdDialog.show({
            controller: 'angularTemplateCtrl',
            templateUrl: 'repositorio/templates/AngularModal.html',
            parent: angular.element(document.body),
            targetEvent: ev
        }).then(function () {
            $scope.toggleTemplates(true);
        });
    };

});