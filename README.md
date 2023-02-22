# api_outlook_calendar

Integrate Microsoft Outlook:
- Give user permissions to the API to access the Outlook calendar
- Create events
- Edit events
- List events
- Get event by Id
- Delete events

## Tech Stack

- Microsoft Graph
- C# .Net 6

## Credentials

Please add the credentials created in the [portal azure](https://portal.azure.com/#home) in the following file `api_outlook_calendar/Files/credentials.json`.

- email: AdeleV@8cwt56.onmicrosoft.com
- password: Toh69217

## Generate OAuth

In order to generate the tokens, click [here](https://localhost:7046/api/OAuth) if you are running the project in local, otherwise click [here]() for production.

## Token File

The token file will be updated every time that the `OAuth` method is called.

## Postman Collection

The collection contains all the methods available at the moment. Click [here](https://github.com/joxedanielc/api_outlook_calendar/files/10788133/Calendar.Outlook.postman_collection.txt) to download it, please change the file extension from `txt` to `json` before importing into Postman.


## Swagger Page

Click [here](https://localhost:7046/swagger/v1/swagger.html) to check swagger documentation.
