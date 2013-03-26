# MVC Master Layout Share #
## Package information ##
This package allows you to share MVC Views between different projects / applications that live on the same server. Typically, this might be used on an Intranet where many small applications all live within the same all encompassing look-and-feel. This allows you to create one default Layout.cshtml file (or multiple) that you can call from your client applications. By extension, it also allows you to share other content, such as images, scripts & stylesheets (but hey, you could just link to them before anyway...)

## Source Code ##
For more information on the project, to see any outstanding bugs and feature implementations you can fork us on GitHub at: 
https://github.com/Amadiere/MVCMasterLayoutShare

## Example ##
Included are two projects:

* MVCMasterLayoutShare.Web.Primary : This is a default "Intranet" MVC4 project with minor modifications and an additional theme.
* MVCMasterLayoutShare.Web.Secondary: This is a default "Empty" MVC4 project with a simple controller & index method which use the theme from the primary project.

## Setup & Usage ##

### Master layout project ###

* Choose a web application to be your master project. This must be hosted on the same box and you should have a URI to the filesystem root for that project. 
* The project can be an existing project, or one specifically designated for this purpose.
* All the links to CSS/Scripts must work from other projects (e.g. not using project root of '~/', but things such as '/Shared/' or 'http://'.
* Any view within the master layout project must have: `@inherits System.Web.Mvc.WebViewPage` at the very top.
* Any view within the master layout project using part of the @Html helper functions (or others for that matter), should reference the appropriate DLL at the top - e.g. `@using System.Web.Mvc.Html`.

### Secondary projects ###

* Global.asax.cs should contain the following in the `Application_Start`:

    MasterLayoutVirtualPathProvider.Register();
    ViewEngines.Engines.Add(new MasterLayoutRazorViewEngine());

* Web.config should contain a setting for the `MasterLayoutPath`:

    <add key="MasterLayoutPath" value="C:\Users\Alex\Code\MVCMasterLayoutShare\MVCMasterLayoutShare.Web.Primary\" />