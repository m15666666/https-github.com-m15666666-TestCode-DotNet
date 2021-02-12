FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
COPY [".", "."]

# 导出端口
EXPOSE 80
#ListenPortOfData
EXPOSE 1283
#ListenPortOfControl
EXPOSE 1284


# 运行
ENTRYPOINT exec dotnet TestK8s.WebApp.dll
