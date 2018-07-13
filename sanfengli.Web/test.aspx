<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="sanfengli.Web.test" %>

<!DOCTYPE html>
<html>
<script src="Scripts/jquery-1.8.3.min.js"></script>
<body>
    <p id="demo">点击这个按钮，获得您的位置：</p>
    <button onclick="getLocation()">试一下</button>
    <button onclick="qq_position()">QQ地图</button>
    <div id="mapholder"></div>

    <p id="baidu_geo"></p>
    <p id="google_geo"></p>
    <script>
        var x = document.getElementById("demo");
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition2, showError);
            }
            else { x.innerHTML = "Geolocation is not supported by this browser."; }
        }

        function showPosition1(position) {
            var latlon = position.coords.latitude + "," + position.coords.longitude;

            var img_url = "http://maps.googleapis.com/maps/api/staticmap?center="
                + latlon + "&zoom=14&size=400x300&sensor=false";
            document.getElementById("mapholder").innerHTML = "<img src='" + img_url + "' />";
        }

        function showPosition(position) {
            var latlon = position.coords.latitude + ',' + position.coords.longitude;

            //baidu 
            var url = "http://api.map.baidu.com/geocoder/v2/?ak=C93b5178d7a8ebdb830b9b557abce78b&callback=renderReverse&location=" + latlon + "&output=json&pois=0";
            $.ajax({
                type: "GET",
                dataType: "jsonp",
                url: url,
                beforeSend: function () {
                    $("#baidu_geo").html('正在定位...');
                },
                success: function (json) {
                    if (json.status == 0) {
                        $("#baidu_geo").html(json.result.formatted_address);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $("#baidu_geo").html(latlon + "地址位置获取失败");
                }
            });
        };

        function showPosition2(position) {
            var latlon = position.coords.latitude + ',' + position.coords.longitude;

            //google 
            var url = 'http://maps.google.cn/maps/api/geocode/json?latlng=' + latlon + '&language=CN';
            $.ajax({
                type: "GET",
                url: url,
                beforeSend: function () {
                    $("#google_geo").html('正在定位...');
                },
                success: function (json) {
                    if (json.status == 'OK') {
                        var results = json.results;
                        $.each(results, function (index, array) {
                            if (index == 0) {
                                $("#google_geo").html(array['formatted_address']);
                            }
                        });
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $("#google_geo").html(latlon + "地址位置获取失败");
                }
            });
        }
        function showError(error) {
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    x.innerHTML = "User denied the request for Geolocation."
                    break;
                case error.POSITION_UNAVAILABLE:
                    x.innerHTML = "Location information is unavailable."
                    break;
                case error.TIMEOUT:
                    x.innerHTML = "The request to get user location timed out."
                    break;
                case error.UNKNOWN_ERROR:
                    x.innerHTML = "An unknown error occurred."
                    break;
            }
        }
    </script>

    <script  type="text/javascript" src="https://3gimg.qq.com/lightmap/components/geolocation/geolocation.min.js" ></script> 
<script>
        function qq_position() {
            var geolocation = new qq.maps.Geolocation("OB4BZ-D4W3U-B7VVO-4PJWW-6TKDJ-WPB77", "myapp");
            if (geolocation) {
                var options = { timeout: 8000 };
                geolocation.getLocation(showPosition, showErr, options);
            } else {
                alert("定位尚未加载");
            }
        }
        function showPosition(position) {
            alert(position.addr)
            alert(position);
        }
        ;
        function showErr(err) {
            //所有可能的错误
            alert(err);
        }
        ;
</script> 
</body>
</html>
