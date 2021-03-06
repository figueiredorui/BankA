'use strict';
'use strict';

app.service("AccountsService", function ($http, AppSettings) {

    var urlBase = AppSettings.ApiUrl + 'Accounts';

    this.getAll = function () {
        return $http.get(urlBase + '');
    }

    this.get = function (id) {
        return $http.get(urlBase + '/' + id);
    }

    this.getBanks = function () {
        return $http.get(urlBase + '/Banks');
    }

    this.getSummary = function () {
        return $http.get(urlBase + '/Summary');
    }

    this.add = function (account) {
        return $http.post(urlBase, account);
    }

    this.update = function (account) {
        return $http.put(urlBase + '/' + account.AccountID, account);
    }

    this.delete = function (id) {
        return $http.delete(urlBase + '/' + id);
    }

});
