# CommonModelFilterSwagger
Applying custom filter to common model using swagger.

What I use in the project:\
ASP.NET Core Web API .NET 5.0\
Swashbuckle.AspNetCore 6.1.4\
Swashbuckle.AspNetCore.SwaggerGen 6.1.4

User Guide:\
You should add [SwaggerIgnore] above your properties you want to ignore.\
If you don't want to ignore property in some controller you should add [SwaggerIgnore(true)] capability.

For now, I'm checking manually because the property I want is fixed. You can customize it if you want.\
"if (actionName.StartsWith("Create"))" in SwaggerSkipPropertyFilter.cs.

![alt text](https://i.ibb.co/PNjx0fL/Filter.png)

The source I used in making the schema filter(Thanks for this):\
https://github.com/domaindrivendev/Swashbuckle.WebApi/issues/1230
