app.service('EditorIdService', function () {

    var id = -1;

    return {
        getID: function () {
            return id;
        },
        setID: function (idNovo) {
            id = idNovo;
        }
    };
});