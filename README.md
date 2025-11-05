# Dotnet HTTP Server Template v1.0.0

## Features

* Modular controllers (Web + API)
* Serve HTML from `templates/`
* Serve static files from `static/` (CSS, JS, images)
* Console logging with timestamps
* Ready for API extensions

## Installation

```bash
dotnet new install https://github.com/aaron-fredrick/dotnet-httpserver-template/releases/latest/download/template.zip
```

## Usage

```bash
dotnet new af-httpserver -n MyServer
cd MyServer
dotnet run
```

Your server will run at http://localhost:5566