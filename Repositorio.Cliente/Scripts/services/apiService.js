app.factory('apiService', function ($resource) {
    return {
        getImagens: $resource('http://localhost:53412/api/snippet/:qntd/:pageNumber'),
    };
});