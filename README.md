# Software Architectures Final Project

**Contents’r’us** is a technology provider specializing in both headless and traditional Content Management Systems (CMS). Over the past several years, they’ve built a robust client base by delivering flexible, easy-to-integrate solutions for businesses of all sizes. The cornerstone of their offering is a solution derived from the open-source Piranha CMS.

The objective of this assignment is to **analyze**, **redesign**, and **implement** architectural changes based on one of three possible scenarios, each reflecting strategic choices and long-term goals for the company. Our group choose the following scenarion:

> ### **Scenario 3**: Event-Driven Extensions & Secure Integrations
> **Vision**: Transform the CMS into a modern, **asynchronous** platform that supports inbound and outbound data flows, integrates easily with external services, and leverages secure messaging.
> **Strategic Goals**:
>   1. Introduce a custom publish/subscribe model for domain events (e.g., content creation, updates, deletions) - where the admins may define if a given model will be published and/or receive information through subscription.
>   2. Support inbound events from authorized external publishers, allowing third parties to trigger CMS actions. have a way to configure the external publishers.
>   3. Ensure on all message flows (inbound and outbound) authenticity and integrity, and provide a clear setup process for end users to configure keys, endpoints, and permissions.

## Quick Start Guide

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [Node.js](https://nodejs.org/en/download/)

### Setting Up the Project
1. Clone the repository:
```bash
git clone git@github.com:zegameiro/AS_Final_Assignment.git
```

2. Open 2 terminal windows in one, remain in the root directory and run:
```bash
docker compose up --build
```

2. Wait for the **RabbitMQ** container to start and then in the other terminal window navigate to the `/examples/MvcWeb` directory and execute the following command:
```bash
dotnet run --framework net9.0
```

4. If every works correctly you should see the message in the terminal thats executing the DOTNET application:
```bash
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: <root_path>
```
> This means that the application is accessible through the URL [http://localhost:5000](http://localhost:5000). If you navigate to the [localhost:5000/manager/](http://localhost:5000/manager/) you should see a login page and the credentials are:
> - username: `admin`
> - password: `password`

5. If you want to run the demo application developed, you can open another terminal window and navigate to the `/demo_app` directory and execute the following commands:
```bash
npm install # install dependencies
npm start
```

6. If everything works correctly, you should see the message in the terminal that the demo application is running:
```bash

> demo_app@1.0.0 start
> node server.js

Server is running on http://localhost:3000
```
> You can now access the demo application at [http://localhost:3000](http://localhost:3000).

## Files Changed/Added

- **Piranha.Data.EF**
    - [Db.cs](/data/Piranha.Data.EF/Db.cs): Already existed;
    - [IDd.cs](/data/Piranha.Data.EF/IDb.cs): Already existed;
    - [PiranhaEFExtensions.cs](/data/Piranha.Data.EF/Extensions/PiranhaEFExtensions.cs): Already existed;
    - [SubscriptionRepository.cs](/data/Piranha.Data.EF/Repositories/SubscriptionRepository.cs): New file created by our group;
    - [KeyRepository.cs](/data/Piranha.Data.EF/Repositories/KeyRepository.cs): New file created by our group;
    - [PageRepository.cs](/data/Piranha.Data.EF/Repositories/PageRepository.cs): Already existed;
    - [MediaRepository.cs](/data/Piranha.Data.EF/Repositories/MediaRepository.cs): Already existed;
    - [Media.cs](/data/Piranha.Data.EF/Data/Media.cs): Already existed;
    - [Page.cs](/data/Piranha.Data.EF/Data/Page.cs): Already existed;
- New Migrations were applied in the **Piranha.Data.EF.SQLite Project**
- **Piranha**
    - [Api.cs](/core/Piranha/Api.cs): Already existed;
    - [App.cs](/core/Piranha/App.cs): Already existed;
    - [PiranhaStartupExtensions.cs](/core/Piranha/Extensions/PiranhaStartupExtensions.cs): Already existed;
    - [Subscription.cs](/core/Piranha/Models/Subscription.cs): New file created by our group;
    - [Key.cs](/core/Piranha/Models/Key.cs): New file created by our group;
    - [Event.cs](/core/Piranha/Models/Event.cs): New file created by our group;
    - [MediaBase.cs](/core/Piranha/Models/MediaBase.cs): Already existed;
    - [PageBase.cs](/core/Piranha/Models/PageBase.cs): Already existed;
    - [ISubscriptionRepository.cs](/core/Piranha/Repositories/IRepository/ISubscriptionRepository.cs): New file created by our group;
    - [IKeyRepository.cs](/core/Piranha/Repositories/IRepository/IKeyRepository.cs): New file created by our group;
    - [ISubscriptionService.cs](/core/Piranha/Services/IService/ISubscriptionService.cs): New file created by our group;
    - [IKeyService.cs](/core/Piranha/Services/IService/IKeyService.cs): New file created by our group;
    - [INotificationService.cs](/core/Piranha/Services/IService/INotificationService.cs): New file created by our group;
    - [SubscriptionService.cs](/core/Piranha/Services/Internal/SubscriptionService.cs): New file created by our group;
    - [KeyService.cs](/core/Piranha/Services/Internal/KeyService.cs): New file created by our group;
    - [NotificationService.cs](/core/Piranha/Services/Internal/NotificationService.cs): New file created by our group;
    - [MediaService.cs](/core/Piranha/Services/Internal/MediaService.cs): Already existed;
    - [PageService.cs](/core/Piranha/Services/Internal/PageService.cs): Already existed;
    - [Events Directory](/core/Piranha/Events/): All the files in this directory were created by our group;
- **Piranha.Manager**
    - [KeyApiController.cs](/core/Piranha.Manager/Controllers/KeyApiController.cs): New file created by our group;
    - [SubscriptionApiController.cs](/core/Piranha.Manager/Controllers/SubscriptionApiController.cs): New file created by our group;
    - [MediaService.cs](/core/Piranha.Manager/Services/MediaService.cs): Already existed;
    - [PageService.cs](/core/Piranha.Manager/Services/PageService.cs): Already existed;
    - [Menu.cs](/core/Piranha.Manager/Menu.cs): Already existed;
    - [MediaListModel.cs](/core/Piranha.Manager/Models/MediaListModel.cs): Already existed;
    - [piranha.media.js](/core/Piranha.Manager/assets/src/js/piranha.media.js): Already existed;
    - [piranha.pageedit.js](/core/Piranha.Manager/assets/src/js/piranha.pageedit.js): Already existed;
    - [piranha.js](/core/Piranha.Manager/assets/dist/js/piranha.js): Already existed;
    - [piranha.preview.js](/core/Piranha.Manager/assets/src/js/piranha.preview.js): Already existed;
    - [_PageSettings.cshtml](/core/Piranha.Manager/Areas/Manager/Pages/Partial/_PageSettings.cshtml): Already existed;
    - [_PreviewModal.cshtml](/core/Piranha.Manager/Areas/Manager/Shared/Partial/_PreviewModal.cshtml): Already existed;
    - [Events.cshtml](/core/Piranha.Manager/Areas/Manager/Pages/Events.cshtml): New file created by our group;
    - [Evets.cs](/core/Piranha.Manager/Areas/Manager/Pages/Events.cs): New file created by our group;
    - [Subscriptions directory](/core/Piranha.Manager/Areas/Manager/Pages/Subscriptions/): All the files in this directory were created by our group;
    - [Keys directory](/core/Piranha.Manager/Areas/Manager/Pages/Keys/): All the files in this directory were created by our group;
- **Demo Application**
    - [demo_app/](/demo_app/): This directory was created by our group and contains the demo application files;

## Architecure

![Architecture Diagram](/docs/images/AS_Final_Assignment_arch.png)

## Deliverables

- The report for the first delivery can be found [here](/docs/1st_delivery/Group_Assignment_1st_part_g04.pdf) and the presentation for the first delivery can be found [here](/docs/1st_delivery/Group_1st_Assignment_g04_presentation.pdf).

- The report for the final and second delivery can be found [here](#) and the presentation for the final and second delivery can be found [here](/docs/2nd_delivery/AS_Final_Assignment.pdf).

## Authors

- [Daniel Madureira](https://github.com/Dan1m4D)
- [João Andrade](https://github.com/WildBunnie)
- [José Gameiro](https://github.com/zegameiro)
- [Tomás Victal](https://github.com/fungame2270)


