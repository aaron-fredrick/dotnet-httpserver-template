# Dotnet HTTP Server Template v1.0.0

## Features

* Modular controllers (Web + API)
* Serve HTML from `templates/`
* Serve static files from `static/` (CSS, JS, images)
* Console logging with timestamps
* Ready for API extensions

## Installation

### \[OPTION 1] Install from local .nupkg (recommended)

Download the .nupkg from GitHub and install locally:

#### \[Option A]

Use curll to download the lates

```bash
curl -L -o DotnetHttpServerTemplate.nupkg \
  https://github.com/aaron-fredrick/dotnet-httpserver-template/releases/latest/download/DotnetHttpServerTemplate.nupkg
```

#### \[Option B]

Use browser link to download the latest, __[Latest Release](https://github.com/aaron-fredrick/dotnet-httpserver-template/releases/latest/download/DotnetHttpServerTemplate.nupkg)__

#### Then,

```bash
# Install the template
dotnet new install ./DotnetHttpServerTemplate.nupkg
```

### \[OPTION 2] Install via cloned repo (for development)

Clone the repository and install locally from the folder:

```bash
git clone https://github.com/aaron-fredrick/dotnet-httpserver-template.git
cd dotnet-httpserver-template

# Pack the template manually (optional)
dotnet pack --configuration Release -o ./artifacts

# Install from local .nupkg
dotnet new install ./artifacts/DotnetHttpServerTemplate.1.0.0.nupkg
```

## Usage

```bash
dotnet new af-httpserver -n MyServer
cd MyServer
dotnet run
```

Your server will run at <http://localhost:5566>
