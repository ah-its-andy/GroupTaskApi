FROM mcr.microsoft.com/dotnet/core/sdk:2.2

WORKDIR /opt/gtapi/

ADD https://github.com/standardcore/GroupTaskApi/archive/0.1.0.tar.gz /opt/gtapi/GroupTaskApi.tar.gz

RUN ["tar", "-xzvf", "/opt/gtapi/GroupTaskApi.tar.gz"]

RUN ["dotnet", "restore", "/opt/gtapi/GroupTaskApi-0.1.0/GroupTaskApi/GroupTaskApi.csproj"]
RUN ["dotnet", "build", "/opt/gtapi/GroupTaskApi-0.1.0/GroupTaskApi/GroupTaskApi.csproj"]

ENTRYPOINT ["dotnet", "run", "-p", "/opt/gtapi/GroupTaskApi-0.1.0/GroupTaskApi/GroupTaskApi.csproj"]

EXPOSE 5000