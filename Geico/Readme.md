How to compile:
Get latest of this repo. Open with Visual Studio 2022,
Go to Build from the top menu or hit CTRL + SHIFT + B

To run the Azure deployed api, go to following endpoint from your browser (chrome).

https://tasksmgmtapi.azurewebsites.net/task

This will list out all the existing tasks for inspection. After inserting and updating existing tasks, this endpoint can be used to see all.  Ideally, this endpoint needs to have pagination to limit the data returned.

There is a postman collection, Geico.postman_collection.json, included as part of this repo. Import that file in Postman and you should be able to hit GET, PUT, or POST endpoints. 

Please look at the FreedomStore/FreedomStore.md to setup the db.  Set that up before running the API.

To run the api on local box, there are two ways one can run the api.
	1) Set Geico project as startup project and hit the run button. Swagger UI should show up in your browser. Hit GET, PUT, or POST endpoints to test.

	2) Set the GeicoUnitTests as startup project and run the unit tests.
