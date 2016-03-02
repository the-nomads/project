var facebookLoginController = angular.module("FacebookLoginController", []);

facebookLoginController.controller('FacebookLoginController', ['$scope', '$window', '$facebook',
    function ($scope, $window, $facebook) {

        $scope.isLoggedIn = false;
        $scope.login = function () { $facebook.login().then(refresh); }
        $scope.logout = function () { $facebook.logout().then(refresh); }

        $scope.wall = [];

        function refresh() {
            $facebook.api("/me").then(
              function (user) {
                  $scope.welcomeMsg = "Welcome " + user.name;
                  $scope.isLoggedIn = true;
                  console.log("User");
                  console.log(user);
                  $facebook.api("/" + user.id + "/feed").then(function (userWall) {
                      console.log(userWall);
                      $scope.wall = userWall.data;
                      //$scope.$apply();
                  });
              },
              function (err) {
                  $scope.welcomeMsg = "Please log in";
              });
        }

        refresh();
}]);