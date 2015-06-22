app.factory('areasService', function (apiService) {
    return {
        // Retorna todos os projetos cadastrados
        getAreas: function () {
            var aux = apiService.areas.query();

            return aux;
        }
    };
});