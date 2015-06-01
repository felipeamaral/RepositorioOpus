app.factory('apiService', function ($resource) {
    return {
        getImagens: $resource('http://localhost:53412/api/snippet/:qntd/:pageNumber'),
        downloadFiles: $resource('http://localhost:53412/api/snippet/:id/files/download'),
    };
});