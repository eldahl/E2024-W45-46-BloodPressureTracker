FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

COPY . .

RUN chmod +x run-ef-update-database.sh
ENTRYPOINT ["/bin/sh", "/app/run-ef-update-database.sh"]
