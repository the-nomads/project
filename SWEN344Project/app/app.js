
var angApp = angular.module('angApp', [
    'ngRoute',
    'HomeController',
]);


angApp.config(['$routeProvider',
    function ($routeProvider) {

        $routeProvider.
            when('/', {
                redirectTo: '/home',
                title: 'Home'
			}).
            when('/home', {
                templateUrl: 'app/components/home/homeView.html',
				controller: 'HomeController',
                title: 'Home',
            }).
            when('/anotherPage', {
                templateUrl: 'app/components/anotherPage/anotherPageView.html',
                title: 'Another Page',
            });
    }
]);

angApp.run(['$rootScope', function ($rootScope) {
    // $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
        // $rootScope.title = current.$$route.title;
    // });
}]);