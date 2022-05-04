How to compile:
Get latest of this repo. Open with Visual Studio 2022
Go to Build from the top menu or hit CTRL + SHIFT + B

To run the Azure deployed api, go to following endpoint from your browser (chrome)

https://tasksmgmtapi.azurewebsites.net/task

This will list out all the existing tasks (100)

There is also a postman collection, Geico.postman_collection.json, included as part of this repo. Import that file in Postman and you should be able to hit GET, PUT, or POST endpoints. 

To run the api on local box, there are two ways one run the api.
	1) Set Geico project as startup project and hit the run button. Swagger UI should show up in your browser. Hit GET, PUT, or POST endpoints to test.
	2) Set the GeicoUnitTests as startup project and run the unit tests.

The API uses in memory collection so no db setup is necessary.

