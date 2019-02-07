FROM microsoft/aspnetcore-build
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "Riganti.Selenium.Coordinator.Service.dll"]
