using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using GoogleApi.Entities.Maps.Elevation.Request;
using GoogleApi.Entities.Maps.Elevation.Response;
using GoogleApi.Entities.Maps.Geocoding;
using GoogleApi.Entities.Maps.Geocoding.Address.Request;
using GoogleApi.Entities.Maps.Geocoding.Location.Request;
using GoogleApi.Entities.Maps.Geocoding.Place.Request;
using GoogleApi.Entities.Maps.Geocoding.PlusCode.Request;
using GoogleApi.Entities.Maps.Geocoding.PlusCode.Response;
using GoogleApi.Entities.Maps.Geolocation.Request;
using GoogleApi.Entities.Maps.Geolocation.Response;
using GoogleApi.Entities.Maps.Roads.NearestRoads.Request;
using GoogleApi.Entities.Maps.Roads.NearestRoads.Response;
using GoogleApi.Entities.Maps.Roads.SnapToRoads.Request;
using GoogleApi.Entities.Maps.Roads.SnapToRoads.Response;
using GoogleApi.Entities.Maps.Roads.SpeedLimits.Request;
using GoogleApi.Entities.Maps.Roads.SpeedLimits.Response;
using GoogleApi.Entities.Maps.StaticMaps.Request;
using GoogleApi.Entities.Maps.StaticMaps.Response;
using GoogleApi.Entities.Maps.StreetView.Request;
using GoogleApi.Entities.Maps.StreetView.Response;
using GoogleApi.Entities.Maps.TimeZone.Request;
using GoogleApi.Entities.Maps.TimeZone.Response;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using GoogleApi.Extensions;

namespace GoogleApi.UnitTests.Extensions;

[TestFixture]
public class ServiceCollectionExtensionTests
{
    private IServiceProvider provider;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var services = new ServiceCollection();
        services
            .AddGoogleApiClients();

        provider = services
            .BuildServiceProvider();
    }

    [Test]
    public void ResolveGoogleApiClientTest()
    {
        var httpClientFactory = provider
            .GetRequiredService<IHttpClientFactory>();

        var httpClient = httpClientFactory
            .CreateClient(nameof(GoogleApi));

        Assert.IsInstanceOf<HttpClient>(httpClient);

        var expected = new MediaTypeWithQualityHeaderValue("application/json");
        Assert.Contains(expected, httpClient.DefaultRequestHeaders.Accept.ToArray());

        Assert.AreEqual(httpClient.Timeout, TimeSpan.FromSeconds(30));

        var defaultHttpClientHandler = HttpClientFactory.GetDefaultHttpClientHandler();

        var hasGZip = defaultHttpClientHandler.AutomaticDecompression.HasFlag(DecompressionMethods.GZip);
        Assert.True(hasGZip);

        var hasDeflate = defaultHttpClientHandler.AutomaticDecompression.HasFlag(DecompressionMethods.Deflate);
        Assert.True(hasDeflate);
    }


    [Test]
    public void ResolveMapsDirectionsApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.DirectionsApi>();

        Assert.IsInstanceOf<HttpEngine<DirectionsRequest, DirectionsResponse>>(result);
    }

    [Test]
    public void ResolveMapsDistanceMatrixApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.DistanceMatrixApi>();

        Assert.IsInstanceOf<HttpEngine<DistanceMatrixRequest, DistanceMatrixResponse>>(result);
    }

    [Test]
    public void ResolveMapsElevationApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.ElevationApi>();

        Assert.IsInstanceOf<HttpEngine<ElevationRequest, ElevationResponse>>(result);
    }

    [Test]
    public void ResolveMapsGeolocationApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.GeolocationApi>();

        Assert.IsInstanceOf<HttpEngine<GeolocationRequest, GeolocationResponse>>(result);
    }

    [Test]
    public void ResolveMapsGeocodeAddressGeocodeApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.Geocode.AddressGeocodeApi>();

        Assert.IsInstanceOf<HttpEngine<AddressGeocodeRequest, GeocodeResponse>>(result);
    }

    [Test]
    public void ResolveMapsGeocodeLocationGeocodeApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.Geocode.LocationGeocodeApi>();

        Assert.IsInstanceOf<HttpEngine<LocationGeocodeRequest, GeocodeResponse>>(result);
    }

    [Test]
    public void ResolveMapsGeocodePlaceGeoCodeApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.Geocode.PlaceGeocodeApi>();

        Assert.IsInstanceOf<HttpEngine<PlaceGeocodeRequest, GeocodeResponse>>(result);
    }

    [Test]
    public void ResolveMapsGeocodePlusCodeGeocodeApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.Geocode.PlusCodeGeocodeApi>();

        Assert.IsInstanceOf<HttpEngine<PlusCodeGeocodeRequest, PlusCodeGeocodeResponse>>(result);
    }

    [Test]
    public void ResolveMapsRoadsNearestRoadsApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.Roads.NearestRoadsApi>();

        Assert.IsInstanceOf<HttpEngine<NearestRoadsRequest, NearestRoadsResponse>>(result);
    }

    [Test]
    public void ResolveMapsRoadsSnapToRoadApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.Roads.SnapToRoadApi>();

        Assert.IsInstanceOf<HttpEngine<SnapToRoadsRequest, SnapToRoadsResponse>>(result);
    }

    [Test]
    public void ResolveMapsRoadsSpeedLimitsApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.Roads.SpeedLimitsApi>();

        Assert.IsInstanceOf<HttpEngine<SpeedLimitsRequest, SpeedLimitsResponse>>(result);
    }

    [Test]
    public void ResolveMapsStreetViewApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.StreetViewApi>();

        Assert.IsInstanceOf<HttpEngine<StreetViewRequest, StreetViewResponse>>(result);
    }

    [Test]
    public void ResolveMapsStaticMapsApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.StaticMapsApi>();

        Assert.IsInstanceOf<HttpEngine<StaticMapsRequest, StaticMapsResponse>>(result);
    }

    [Test]
    public void ResolveMapsTimeZoneApi()
    {
        var result = provider
            .GetRequiredService<GoogleMaps.TimeZoneApi>();

        Assert.IsInstanceOf<HttpEngine<TimeZoneRequest, TimeZoneResponse>>(result);
    }


}