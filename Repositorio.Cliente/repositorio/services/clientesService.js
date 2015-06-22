app.factory('clientesService', function (apiService) {
    return {
        // Retorna todos os projetos cadastrados
        getClientes: function () {
            var aux = apiService.clientes.query();

            return aux;
        }
    };
});