var facebookLoginController = angular.module("FacebookLoginController", []);

facebookLoginController.controller('FacebookLoginController', ['$scope', '$window', '$facebook',
    function ($scope, $window, $facebook) {

        $scope.isLoggedIn = false;
        $scope.login = function () { $facebook.login().then(refresh); }
        $scope.logout = function () { $facebook.logout().then(refresh); }

        function refresh() {
            $facebook.api("/me").then(
              function (response) {
                  $scope.welcomeMsg = "Welcome " + response.name;
                  $scope.isLoggedIn = true;
                  console.log("Logged in here!");
                  console.log(response);
              },
              function (err) {
                  $scope.welcomeMsg = "Please log in";
              });
        }

        refresh();
}]);