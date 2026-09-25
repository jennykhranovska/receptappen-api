# Receptappen — Backend (ASP.NET Core)

Kort beskrivning
---------------
Detta är backend-delen för Receptappen. Ett enkelt ASP.NET Core Web API som använder SQLite för lagring och sparar uppladdade bilder i wwwroot/uploads.

Krav
------
- .NET SDK: net10.0 (se receptappen-api.csproj: TargetFramework: net10.0)
- dotnet-ef (om du behöver köra EF-migrationer manuellt):
  - dotnet tool install --global dotnet-ef

Snabba startsteg från en ren klon
---------------------------------
1. Öppna en terminal och gå till projektmappen (mappen som innehåller receptappen-api.csproj):

   cd /path/to/receptappen-api

2. Återställ paket:

   dotnet restore

3. Databasen (SQLite):

   - Projektet använder en lokal SQLite-fil recipes.db (Data Source=recipes.db).
   - Om recipes.db inte finns i repo (eller du föredrar att skapa den från migrationer) kör:

	 dotnet ef database update

	 (Det kräver att dotnet-ef är installerat globalt eller att du kör via dotnet tool.)

4. Starta backend:

   dotnet run

5. Backend är tillgänglig lokalt enligt launchSettings.json. Standard HTTP-adress:

   http://localhost:5008

   (HTTPS-profiler finns också i launchSettings, se Properties/launchSettings.json.)

Frontend
---------
Frontend körs separat (i ett annat projekt/repo). I utveckling körs frontend vanligtvis på:

  http://localhost:5173

API-endpoints (kort)
---------------------
- GET /api/Recipes
  - Returnerar en JSON-array med recept.

- POST /api/Recipes
  - Skapar ett nytt recept. Förväntar sig JSON i request body med Recipe-fälten (Name, Category, CookingTime, Ingredients, Instructions, ImagePath).
  - Notera: endpointen returnerar 200 OK med det skapade objektet.

- PUT /api/Recipes/{id}
  - Uppdaterar ett befintligt recept. Skicka id i URL och ett Recipe-objekt i body.

- POST /api/Recipes/{id}/image
  - Tar emot en multipart/form-data med fältnamnet "image" (IFormFile image).
  - Filen sparas i wwwroot/uploads och Recipe.ImagePath sätts till "/uploads/{filnamn}".
  - Se Controllers/RecipesController.cs för detaljer.

Uppladdade bilder
------------------
Uppladdade bilder sparas fysiskt under projektets wwwroot/uploads. De är åtkomliga via:

  http://localhost:5008/uploads/{filnamn}

Viktiga konfigurationsnoteringar
--------------------------------
- CORS: Program.cs tillåter origin http://localhost:5173 och http://localhost:8081. Om frontend körs på annan origin måste CORS justeras.
- Databas: Program.cs använder "Data Source=recipes.db". Projektet innehåller migrations under Migrations/ om du behöver skapa DB från migrationer.
- Static files: wwwroot mappen används för att serva statiska filer.

Behöver du mer?
----------------
Om du vill kan jag lägga till en kort instruktion i README för hur man kör migrations automatiskt vid uppstart eller uppdatera CORS för fler origin. Jag ändrar inget i koden utan visar bara förslag om du vill.
