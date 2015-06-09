app.factory('projetosService', function (apiService) {
    return {
        // Retorna todos os projetos cadastrados
        getProjetos: function () {
            var aux = apiService.projetos.query();

            return aux;
        }
    };
});