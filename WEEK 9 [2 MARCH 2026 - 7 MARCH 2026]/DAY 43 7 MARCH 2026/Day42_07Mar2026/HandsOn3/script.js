function checkWeather()
{
    let city = document.getElementById("cityInput").value;
    let display = document.getElementById("weatherDisplay");
    let body = document.getElementById("body");

    let weather;
    let temp;

    // Mock weather conditions
    if(city.toLowerCase() === "paris")
    {
        weather = "Sunny ☀️";
        temp = "22°C";
    }
    else if(city.toLowerCase() === "london")
    {
        weather = "Rainy 🌧";
        temp = "16°C";
    }
    else if(city.toLowerCase() === "tokyo")
    {
        weather = "Cloudy ☁️";
        temp = "19°C";
    }
    else
    {
        weather = "Sunny ☀️";
        temp = "25°C";
    }

    // Display result
    display.innerHTML = 
        "City: " + city + "<br>" +
        "Temperature: " + temp + "<br>" +
        "Weather: " + weather;

    // Change background based on weather
    if(weather.includes("Sunny"))
        body.style.background = "#FFD54F";

    else if(weather.includes("Rainy"))
        body.style.background = "#64B5F6";

    else if(weather.includes("Cloudy"))
        body.style.background = "#B0BEC5";
}