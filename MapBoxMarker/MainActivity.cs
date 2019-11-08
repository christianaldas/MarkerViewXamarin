using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using static Com.Mapbox.Mapboxsdk.Maps.MapboxMap;
using Com.Mapbox.Mapboxsdk.Geometry;
using Com.Mapbox.Mapboxsdk.Plugins.Markerview;
using Com.Mapbox.Mapboxsdk;
using Com.Mapbox.Mapboxsdk.Maps;
using Com.Mapbox.Mapboxsdk.Location;
using Com.Mapbox.Android.Core.Permissions;
using Com.Mapbox.Mapboxsdk.Location.Modes;
using Android.Views;
using Com.Mapbox.Mapboxsdk.Camera;
using System;

namespace MapBoxMarker
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity,
                                IOnMapLongClickListener,
                                IOnMapClickListener,
                                IOnMapReadyCallback,
                                Style.IOnStyleLoaded,
        IOnLocationCameraTransitionListener
    {
        private MarkerViewManager markerViewManager;
        private MapView mapView;
        private MapboxMap mapboxMap;
        private Style style;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Mapbox.GetInstance(this, "pk.eyJ1IjoiY2hyaXN0aWFuYWxkYXMiLCJhIjoiY2syY2d5enFsMmxrNzNjcGVoM3M2Zm1wbyJ9.S1cunJFG6xz4dJ7t2usGoA");
            SetContentView(Resource.Layout.activity_main);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            mapView = FindViewById<MapView>(Resource.Id.mapView);
            mapView.OnCreate(savedInstanceState);
            mapView.GetMapAsync(this);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool OnMapLongClick(LatLng p0)
        {
            throw new System.NotImplementedException();
        }

        public bool OnMapClick(LatLng p0)
        {
            throw new System.NotImplementedException();
        }

        public void OnMapReady(MapboxMap map)
        {
            this.mapboxMap = map;
            map.SetStyle(Style.MAPBOX_STREETS, this);
            map.UiSettings.ZoomGesturesEnabled = true;
            map.UiSettings.ScrollGesturesEnabled = true;
            map.UiSettings.SetAllGesturesEnabled(true);
        }

        public void OnStyleLoaded(Style style)
        {
            this.style = style;
            MostrarUbicacionTiempoReal();
            FindViewById<View>(Resource.Id.fabStyles).Click += MainActivity_Click;
            mapboxMap.MoveCamera(CameraUpdateFactory.ZoomTo(2.0));
            markerViewManager = new MarkerViewManager(mapView, mapboxMap);

            createCustomMarker();
            //createRandomMarkers()

            //mapboxMap.addOnMapLongClickListener(this@MarkerViewActivity)
            //mapboxMap.addOnMapClickListener(this@MarkerViewActivity)
        }

        private void createCustomMarker()
        {
            // create a custom animation marker view
            var customView = createCustomAnimationView();
            marker = MarkerView(LatLng(), customView)
        marker?.let {
                markerViewManager?.addMarker(it)
        }
        }

        private View createCustomAnimationView()
        {
            val customView = LayoutInflater.from(this).inflate(R.layout.marker_view, null)
        }

        private void MainActivity_Click(object sender, System.EventArgs e)
        {
            mapboxMap.SetStyle(Style.DARK);

        }

        private void MostrarUbicacionTiempoReal()
        {
            if (PermissionsManager.AreLocationPermissionsGranted(this))
            {
                LocationComponent locationComponent = mapboxMap.LocationComponent;
                locationComponent.ActivateLocationComponent(LocationComponentActivationOptions.InvokeBuilder(this, this.style).Build());
                locationComponent.LocationComponentEnabled = true;
                locationComponent.SetCameraMode(CameraMode.TRACKING, this);
                locationComponent.RenderMode = RenderMode.COMPASS;
            }
        }

        public void OnLocationCameraTransitionCanceled([IntDef(Type = "Com.Mapbox.Mapboxsdk.Location.Modes.CameraMode", Fields = new[] { "NONE", "NONE_COMPASS", "NONE_GPS", "TRACKING", "TRACKING_COMPASS", "TRACKING_GPS", "TRACKING_GPS_NORTH" })] int p0)
        {
            //throw new System.NotImplementedException();
        }

        public void OnLocationCameraTransitionFinished([IntDef(Type = "Com.Mapbox.Mapboxsdk.Location.Modes.CameraMode", Fields = new[] { "NONE", "NONE_COMPASS", "NONE_GPS", "TRACKING", "TRACKING_COMPASS", "TRACKING_GPS", "TRACKING_GPS_NORTH" })] int p0)
        {
            //throw new System.NotImplementedException();
        }
    }
}