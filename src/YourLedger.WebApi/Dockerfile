#GET SDK FROM DOCKER HUB

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env
WORKDIR /App
COPY *.csproj ./

#THIS WILL GET ALL OF THE NUGGET PACKAGES AND PUT THEM IN THE CD
RUN dotnet restore
#COPY REST OF THE FILES
COPY . ./
RUN dotnet publish -c Release -o out

#GET THE RUNTIME IMAGE
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /App
EXPOSE 80
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "YourLedger.WebApi.dll"]

