<h3>Developers:</h3>
Vladislav Dimitrov and Vasil Ganichev

<h3>Technologies:</h3>

<h5>Backend</h5>
ASP.NET Core MVC 2.2
ASP.NET Core Identity system
EF Core 2.2
SQL Server 2017

<h5>Frontend</h5>
Jquery
Toastr
Font Awesome
Bootstrap

<h3>Architecture</h3>
<h5>We have used the Onion architecture where we have 3 main layers + prject for unit tests</h5>
- WebApp layer - holds all the ASP logic and the user interface
- Service layer - holds all the services between the User interface (app layer) and the data layer
- Data layer - holds all the project data, DBContext + models
- Tests project - holds all application unit tests

<h3>Design principles</h3>
We have followed the OOP and SOLID design principles.

<h3>Project purpose</h3>
ASP.NET Core MVC application – Cocktail Magician.

Cocktail Magician allows creation of recipes for innovative, exotic, awesome cocktails and
follows their distribution and success in amazing bars.

<h3>Conclusion</h3>
The code should be easily maintainable and the project should be easily extensible.