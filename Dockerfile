FROM microsoft/dotnet:1.1.0-sdk-projectjson
COPY . /app
WORKDIR /app/src/KpiManager
 
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
 
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS="http://*:5000"
ENV ASPNETCORE_ENVIRONMENT=development
 
ENTRYPOINT ["dotnet", "run"]/