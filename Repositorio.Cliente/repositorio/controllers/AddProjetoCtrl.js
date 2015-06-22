app.controller('AddProjetoCtrl', function ($scope, $mdDialog, apiService, nome, clientesService, areasService, $mdToast) {

    $scope.projeto = {
        nome: nome,
        cliente: null,
        dataIni: "0000-00-00",
        dataFim: "0000-00-00",
        descricao: "",
        area: null
    };

    $scope.cancelar = function () {
        $mdDialog.cancel();
    };

    function doisDigitos(valor) {
        if (0 <= valor && valor < 10) return "0" + valor.toString();
        return valor.toString();
    }

    //Adiciona o projeto no servidor
    $scope.adicionar = function () {

        // Verifica se não existem erros no formulário
        if ($scope.addProjetoForm.$valid) {

            var erro = false;

            // Verifica se o campo de descrição não contém apenas espaços em branco
            var aux = $scope.projeto.descricao.replace(/\s/g, '');
            if (aux.length == 0) {
                $scope.projeto.descricao = "";
                $scope.addProjetoForm.descricao.$touched;
                erro = true;
            } else {
                $scope.projeto.descricao = $scope.projeto.descricao.replace(/\s{2,}/g, ' ');
            }

            
            /*Converte as datas num formato que o banco reconheça*/
            var data = $scope.dataIni;
            $scope.projeto.dataIni = data.getUTCFullYear() + "-" + doisDigitos(1 + data.getUTCMonth()) + "-" + doisDigitos(data.getUTCDate());

            // verifica se existe um data fim
            if ($scope.dataFim) {
                data = $scope.dataFim;
                $scope.projeto.dataFim = data.getUTCFullYear() + "-" + doisDigitos(1 + data.getUTCMonth()) + "-" + doisDigitos(data.getUTCDate());
            }

            // Verifica se alguma área foi informada e se o campo não contém apenas espaços em branco
            if ($scope.projeto.area == null && $scope.searchTextArea != null && $scope.searchTextArea != "") {
                aux = $scope.searchTextArea.replace(/\s/g, '');
                if (aux.length > 0) {
                    $scope.projeto.area = $scope.searchTextArea.toLowerCase().replace(/\s{2,}/g, ' ');
                }
            }

            // Verifica se algum cliente foi informado e se o campo não contém apenas espaços em branco
            if ($scope.projeto.cliente == null && $scope.searchTextCliente != null && $scope.searchTextCliente != "") {
                aux = $scope.searchTextCliente.replace(/\s/g, '');
                if (aux.length > 0) {
                    $scope.projeto.cliente = $scope.searchTextCliente.toLowerCase().replace(/\s{2,}/g, ' ');
                }
            }

            if (!erro) {

                //Substitui espaços duplos por espaços simples
                $scope.projeto.nome = $scope.projeto.nome.replace(/\s{2,}/g, ' ');

                apiService.projetos.save($scope.projeto, function (data) {

                    //Exibe mensagem de que projeto foi adicionado com sucesso
                    $mdToast.show(
                        $mdToast.simple()
                        .content('Projeto adicionado com sucesso!')
                        .position('bottom left')
                        .hideDelay(5000)
                    );

                    //Envia o projeto adicionado pra página que chamou o modal
                    $mdDialog.hide(data);
                });
            }
            
        } else {

            $scope.addProjetoForm.nome.$touched = true;
            $scope.addProjetoForm.descricao.$touched = true;
            $scope.addProjetoForm.dataIni.$touched = true;

            // Exibe mensagem de que existem erros no formulário
            $mdToast.show(
                $mdToast.simple()
                .content('Existem erros no preenchimento dos dados!')
                .position('bottom left')
                .hideDelay(5000)
            );
        }
    };

    //Autocomplete de clientes
    var clientesCadastrados = clientesService.getClientes();

    var clientes = [];
    clientesCadastrados.$promise.then(function (data) {
        var cont = 0;
        data.forEach(function (cliente) {
            clientes[cont] = { value: cliente.nome, display: cliente.nome.toLowerCase() };
            cont++;
        });
    });

    $scope.searchTextCliente = null;
    $scope.querySearchCliente = function (query) {
        var results = query ? clientes.filter(createFilterFor(query)) : [];
        return results;
    };

    function createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);
        return function filterFn(obj) {
            return (obj.display.indexOf(lowercaseQuery) === 0);
        };
    }

    //Autocomplete de áreas
    var areasCadastradas = areasService.getAreas();

    var areas = [];
    areasCadastradas.$promise.then(function (data) {
        var cont = 0;
        data.forEach(function (area) {
            areas[cont] = { value: area.nome, display: area.nome.toLowerCase() };
            cont++;
        });
    });

    $scope.searchTextArea = null;
    $scope.querySearchArea = function (query) {
        var results = query ? areas.filter(createFilterFor(query)) : [];
        return results;
    };
});