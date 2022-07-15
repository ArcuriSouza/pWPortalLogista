# pWPortalLogista

`Real Empreendimentos - Portal do Logista`


#### CLI New Project
dotnet new web -o pWPortalLogista --framework netcoreapp3.1

dotnet add package Npgsql --version 3.1.10
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 3.1.18
dotnet add package Microsoft.EntityFrameworkCore --version 3.1.18
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 3.1.18

dotnet new page -n Index -na pWPortalLogista.Pages -o Pages
dotnet new page -n Index -na pWPortalLogista.Pages.Home -o Pages/Home
dotnet new page -n Index -na pWPortalLogista.Pages.Login -o Pages/Login
dotnet new page -n Index -na pWPortalLogista.Pages.Login -o Pages/

pgsql.realemp.com.br -> Porta:5432
pgsql01.kinghost.net -> Porta:5432
pgsql22-farm10.kinghost.net -> Porta:5432

dotnet ef dbcontext scaffold "Host=XXXXXX;Port=XXXXXX;Database=XXXXXX;User Id=XXXXXX;Password=XXXXXX;" Npgsql.EntityFrameworkCore.PostgreSQL -o Models -f -c PLDbContext

#### Closed XML (to read Excel files)
dotnet add package ClosedXML