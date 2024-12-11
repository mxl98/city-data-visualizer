# Montreal City Data Visualizer

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
![Build Status](https://github.com/mxl98/city-data-visualizer/actions/workflows/dotnet-build.yml/badge.svg)


| Package                          | NuGet           |
| ---------------------------------|:---------------:|
| CSVHelper                        | [![NuGet CsvHelper](https://img.shields.io/nuget/v/CsvHelper.svg?style=flat)](https://www.nuget.org/packages/CsvHelper/) |
| MySql.Data                       | [![NuGet MySql.Data](https://img.shields.io/nuget/v/MySql.Data.svg?style=flat)](https://www.nuget.org/packages/MySql.Data/) |
| Microsoft.EntityFrameworkCore    | [![NuGet Microsoft.EntityFrameworkCore](https://img.shields.io/nuget/v/Microsoft.EntityFrameworkCore.svg?style=flat)](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/) |
| Pomelo.EntityFrameworkCore.MySql | [![NuGet Pomelo.EntityFrameworkCore.MySql](https://img.shields.io/nuget/v/Pomelo.EntityFrameworkCore.MySql.svg?style=flat)](https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/) |
| DotNetEnv                        | [![NuGet DotNMetEnv](https://img.shields.io/nuget/v/DotNetEnv.svg?style=flat)](https://www.nuget.org/packages/DotNetEnv/) |
| Quartz.NET                       | [![NuGet Quartz](https://img.shields.io/nuget/v/Quartz.svg?style=flat)](https://www.nuget.org/packages/Quartz/) |
| Quartz.Extensions.DependencyInjection | [![NuGet Quartz.Extensions.DependencyInjection](https://img.shields.io/nuget/v/Quartz.Extensions.DependencyInjection.svg?style=flat)](https://www.nuget.org/packages/Quartz.Extensions.DependencyInjection/) |
| Quartz.Extensions.Hosting | [![NuGet Quartz.Extensions.Hosting](https://img.shields.io/nuget/v/Quartz.Extensions.Hosting.svg?style=flat)](https://www.nuget.org/packages/Quartz.Extensions.Hosting/) |

## Overview

This app is intended to be a web API combined with a modern user interface for easy data visualization. The open data belongs to the city of Montreal (https://donnees.montreal.ca/).
Data model names are in french as to match with the language of the data itself.

Current features:
* Automated database updates
* Lists all pools (piscines)
* Filter

Planned (upcoming) features:
* Accessibility settings

## Stack

* Angular 18 - frontend
* .NET Core 8 - backend
* MySQL - database

## Data Sources

[sourceurls.json](https://github.com/mxl98/city-data-visualizer/blob/main/WebApi/static/sourceurls.json)