'use strict';

app.service("RulesService", function ($http, AppSettings) {

    var urlBase = AppSettings.ApiUrl + 'Rules';

    this.getAll = function () {
        return $http.get(urlBase + '');
    }

    this.get = function (id) {
        return $http.get(urlBase + '/' + id);
    }

    this.add = function (rule) {
        return $http.post(urlBase, rule);
    }

    this.update = function (rule) {
        return $http.put(urlBase + '/' + rule.RuleID, rule);
    }

    this.delete = function (id) {
        return $http.delete(urlBase + '/' + id);
    }
    
    this.getTags = function () {
        return $http.get(urlBase + '/Tags');
    }
    
    this.getGroups = function () {
        return $http.get(urlBase + '/Groups');
    }

});
