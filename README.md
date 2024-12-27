# About
A simple service meant to aggregate and display livability-related pricing data for Sweden. E.g electricity, real estate, groceries, gas.

# Stack
* C# .NET with Blazor for the overall app platform.
* D3 for the choropleth maps.
* SQLite for data storage.

# Structure
Each topic that pricing information is to be fetched for has at the very least a service. Sometimes a client is involved too.
A combination of screen-scraping and network requests are used to produce the data required to map prices to regions.

A main service called "data collector" calls each pricing data service and stores the responses into an SQlite database. Said main service
is run manually once a day or so through an admin-only endpoint with simple token auth.

When a user visits the page data is fetched from the db and displayed on maps & tables.

# Running
The `https` action in Visual Studion on the `Priskartan` solution can be used to get the app started.

To run the data collection action, you can run a `curl` command using the dummy auth token `test`, i.e:
`curl https://localhost:7149/admin/run-data-collection -H "Authorization: test"`.

# To-dos
* Move the auth token out of `appsettings.json` and into an env variable (that in a real/production environment would be set in Azure app secrets or similar).
* Add unit tests with xUnit.
* Upgrade the UI so that the tables are toggle-able and there are hover/click actions for the regions on the maps (displaying the price for each).
* Replace exception handling with the `Result` object based pattern.
* Add a logger.
* Support Swedish and English in the UI.