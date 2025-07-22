FROM mcr.microsoft.com/devcontainers/dotnet:dev-10.0-preview
USER vscode
COPY ../certs /usr/local/share/ca-certificates/
RUN sudo update-ca-certificates