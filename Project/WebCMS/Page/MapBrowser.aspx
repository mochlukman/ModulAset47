﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MapBrowser.aspx.cs" Inherits="Page_MapBrowser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta name="viewport" content="initial-scale=1.0, user-scalable=no">
  <meta charset="utf-8">
  <title>Marker Labels</title>
  <style>
    /* Always set the map height explicitly to define the size of the div
       * element that contains the map. */
    #map
    {
      height: 100%;
    }
    /* Optional: Makes the sample page fill the window. */
    html, body
    {
      height: 100%;
      margin: 0;
      padding: 0;
    }
  </style>
  <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAz3pcwEEXq1tPxznbaTJOOpBZYFyU48_0"></script>
  <script>
    // In the following example, markers appear when the user clicks on the map.
    // Each marker is labeled with a single alphabetical character.
    var labels = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    var labelIndex = 0;

    function initialize() {
      var bangalore = { lat: 12.97, lng: 77.59 };
      var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 12,
        center: bangalore
      });

      // This event listener calls addMarker() when the map is clicked.
      google.maps.event.addListener(map, 'click', function (event) {
        addMarker(event.latLng, map);
      });

      // Add a marker at the center of the map.
      addMarker(bangalore, map);
    }

    // Adds a marker to the map.
    function addMarker(location, map) {
      // Add the marker at the clicked location, and add the next-available label
      // from the array of alphabetical characters.
      var marker = new google.maps.Marker({
        position: location,
        label: labels[labelIndex++ % labels.length],
        map: map
      });
    }

    google.maps.event.addDomListener(window, 'load', initialize);
  </script>
</head>
<body>
  <div id="map">
  </div>
</body>
</html>
